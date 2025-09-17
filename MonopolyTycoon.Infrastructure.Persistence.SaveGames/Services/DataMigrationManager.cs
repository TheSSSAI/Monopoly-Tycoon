using Microsoft.Extensions.Logging;
using MonopolyTycoon.Infrastructure.Persistence.SaveGames.Abstractions;
using MonopolyTycoon.Infrastructure.Persistence.SaveGames.Exceptions;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MonopolyTycoon.Infrastructure.Persistence.SaveGames.Services
{
    /// <summary>
    /// Provides logic to upgrade save file data from older application versions to the current version schema.
    /// This adheres to REQ-1-090 for backward compatibility.
    /// </summary>
    public class DataMigrationManager : IDataMigrationManager
    {
        private readonly ILogger<DataMigrationManager> _logger;
        private static readonly string CurrentVersion = typeof(DataMigrationManager).Assembly.GetName().Version?.ToString(3) ?? "1.0.0";

        // The migration steps are ordered by the version they migrate *from*.
        // The value is a function that takes the JSON of a given version and transforms it to the next.
        private readonly SortedDictionary<Version, Func<JsonNode, JsonNode>> _migrationSteps;

        public DataMigrationManager(ILogger<DataMigrationManager> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _migrationSteps = new SortedDictionary<Version, Func<JsonNode, JsonNode>>
            {
                // To add a migration from v1.0.0 to v1.1.0, you would add an entry like this:
                // [new Version("1.0.0")] = MigrateFrom1_0_0To1_1_0,
                // This collection is intentionally empty for the initial release, but the logic is in place.
            };
        }

        public Task<string> MigrateAsync(string rawGameStateJson, string sourceVersionString)
        {
            if (string.IsNullOrWhiteSpace(rawGameStateJson))
            {
                throw new ArgumentException("Raw game state JSON cannot be null or empty.", nameof(rawGameStateJson));
            }

            if (!Version.TryParse(sourceVersionString, out var sourceVersion))
            {
                throw new ArgumentException($"Source version '{sourceVersionString}' is not a valid version string.", nameof(sourceVersionString));
            }
            
            var currentVersion = new Version(CurrentVersion);

            if (sourceVersion == currentVersion)
            {
                _logger.LogDebug("Save file version {SourceVersion} matches current version {CurrentVersion}. No migration needed.", sourceVersion, currentVersion);
                return Task.FromResult(rawGameStateJson);
            }

            if (sourceVersion > currentVersion)
            {
                _logger.LogError("Save file version {SourceVersion} is newer than the application version {CurrentVersion}. Migration is not supported.", sourceVersion, currentVersion);
                throw new UnsupportedSaveVersionException($"Cannot load save file. Its version ({sourceVersion}) is newer than the application version ({currentVersion}).");
            }

            _logger.LogWarning("Starting data migration for save file from version {SourceVersion} to {CurrentVersion}.", sourceVersion, currentVersion);

            try
            {
                var jsonNode = JsonNode.Parse(rawGameStateJson);
                if (jsonNode == null)
                {
                    throw new SaveFileCorruptedException("Failed to parse game state JSON into a valid structure.");
                }

                var migrationChain = new List<Version>();
                foreach (var availableMigrationVersion in _migrationSteps.Keys)
                {
                    if (availableMigrationVersion >= sourceVersion)
                    {
                        migrationChain.Add(availableMigrationVersion);
                    }
                }
                
                migrationChain.Sort();

                var currentMigrationVersion = sourceVersion;
                foreach (var stepVersion in migrationChain)
                {
                    if (currentMigrationVersion != stepVersion)
                    {
                        // This indicates a gap in the migration path.
                        throw new UnsupportedSaveVersionException($"No migration path found from version {currentMigrationVersion} to {stepVersion}. Cannot upgrade save file.");
                    }
                    
                    _logger.LogInformation("Applying migration from version {MigrationVersion}...", stepVersion);
                    var migrationFunc = _migrationSteps[stepVersion];
                    jsonNode = migrationFunc(jsonNode);

                    // Infer the next version from the next step, or assume current if it's the last one.
                    var nextStepIndex = migrationChain.IndexOf(stepVersion) + 1;
                    if (nextStepIndex < migrationChain.Count)
                    {
                        currentMigrationVersion = migrationChain[nextStepIndex];
                    }
                    else
                    {
                        // This is a simplification; a more robust system might store the "to" version with the delegate.
                        // For now, we assume migrations are linear and the final one brings it close to current.
                    }
                }

                var migratedJson = jsonNode.ToJsonString(new JsonSerializerOptions { WriteIndented = true });
                _logger.LogInformation("Data migration completed successfully.");
                return Task.FromResult(migratedJson);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Failed to parse save file JSON during migration from version {SourceVersion}.", sourceVersion);
                throw new SaveFileCorruptedException("Save file is corrupt and could not be parsed for migration.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "An unexpected error occurred during data migration from version {SourceVersion}.", sourceVersion);
                throw; // Re-throw unexpected exceptions to be caught by the global handler.
            }
        }
        
        // Example of a future migration function. It's defined here to show the pattern but not added to the dictionary.
        // private JsonNode MigrateFrom1_0_0To1_1_0(JsonNode root)
        // {
        //     _logger.LogDebug("Executing migration: Rename 'Player.Money' to 'Player.Cash'.");
        //     var playerStates = root["playerStates"]?.AsArray();
        //     if (playerStates != null)
        //     {
        //         foreach (var playerNode in playerStates)
        //         {
        //             var playerObj = playerNode?.AsObject();
        //             if (playerObj != null && playerObj.ContainsKey("money"))
        //             {
        //                 var moneyNode = playerObj["money"];
        //                 playerObj.Remove("money");
        //                 playerObj["cash"] = moneyNode;
        //             }
        //         }
        //     }
        //     return root;
        // }
    }
}