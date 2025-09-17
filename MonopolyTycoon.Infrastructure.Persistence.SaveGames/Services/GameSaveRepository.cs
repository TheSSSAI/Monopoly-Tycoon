using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MonopolyTycoon.Application.Abstractions;
using MonopolyTycoon.Domain.Entities;
using MonopolyTycoon.Infrastructure.Persistence.SaveGames.Abstractions;
using MonopolyTycoon.Infrastructure.Persistence.SaveGames.Exceptions;
using MonopolyTycoon.Infrastructure.Persistence.SaveGames.Persistence;

namespace MonopolyTycoon.Infrastructure.Persistence.SaveGames.Services
{
    /// <summary>
    /// Implements the repository for saving and loading game state to the local file system.
    /// This class handles serialization, checksum validation, and data migration orchestration.
    /// </summary>
    public class GameSaveRepository : ISaveGameRepository
    {
        private readonly ILogger<GameSaveRepository> _logger;
        private readonly ISaveFilePathProvider _pathProvider;
        private readonly IDataMigrationManager _migrationManager;
        private static readonly string CurrentAppVersion = typeof(GameSaveRepository).Assembly.GetName().Version?.ToString() ?? "1.0.0";

        private static readonly JsonSerializerOptions SerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true // For human readability of save files
        };

        public GameSaveRepository(
            ILogger<GameSaveRepository> logger,
            ISaveFilePathProvider pathProvider,
            IDataMigrationManager migrationManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _pathProvider = pathProvider ?? throw new ArgumentNullException(nameof(pathProvider));
            _migrationManager = migrationManager ?? throw new ArgumentNullException(nameof(migrationManager));
        }

        /// <inheritdoc/>
        public async Task SaveAsync(GameState state, int slot, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(state);
            if (slot <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(slot), "Save slot must be a positive integer.");
            }

            var filePath = _pathProvider.GetSaveFilePath(slot);
            var tempFilePath = filePath + ".tmp";

            _logger.LogInformation("Attempting to save game to slot {Slot} at {FilePath}", slot, filePath);

            try
            {
                // Ensure the save directory exists
                var directory = Path.GetDirectoryName(filePath);
                if (directory != null)
                {
                    Directory.CreateDirectory(directory);
                }
                
                var gameStateJson = JsonSerializer.Serialize(state, SerializerOptions);
                var checksum = ComputeChecksum(gameStateJson);

                var wrapper = new SaveFileWrapper
                {
                    Version = CurrentAppVersion,
                    Checksum = checksum,
                    GameStateData = gameStateJson,
                    SaveTimestamp = DateTime.UtcNow
                };

                var finalJson = JsonSerializer.Serialize(wrapper, SerializerOptions);

                // Atomic write operation: write to temp file first
                await File.WriteAllTextAsync(tempFilePath, finalJson, Encoding.UTF8, cancellationToken);

                // Then move/rename to replace the original file
                File.Move(tempFilePath, filePath, true);

                _logger.LogInformation("Successfully saved game to slot {Slot}.", slot);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save game to slot {Slot}. An unexpected error occurred.", slot);
                // Clean up the temporary file if it exists
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
                throw; // Re-throw the exception to be handled by the application layer
            }
        }

        /// <inheritdoc/>
        public async Task<GameState?> LoadAsync(int slot, CancellationToken cancellationToken = default)
        {
            if (slot <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(slot), "Save slot must be a positive integer.");
            }

            var filePath = _pathProvider.GetSaveFilePath(slot);

            if (!File.Exists(filePath))
            {
                _logger.LogWarning("Attempted to load from slot {Slot}, but file does not exist.", slot);
                return null;
            }

            _logger.LogInformation("Loading game from slot {Slot} at {FilePath}", slot, filePath);

            try
            {
                var fileContent = await File.ReadAllTextAsync(filePath, Encoding.UTF8, cancellationToken);
                var wrapper = JsonSerializer.Deserialize<SaveFileWrapper>(fileContent, SerializerOptions);

                if (wrapper is null || string.IsNullOrWhiteSpace(wrapper.GameStateData))
                {
                    throw new SaveFileCorruptedException($"Save file for slot {slot} is malformed or empty.");
                }

                // 1. Checksum Validation
                var calculatedChecksum = ComputeChecksum(wrapper.GameStateData);
                if (!string.Equals(calculatedChecksum, wrapper.Checksum, StringComparison.OrdinalIgnoreCase))
                {
                    throw new SaveFileCorruptedException($"Checksum mismatch for save slot {slot}. The file may be corrupted.");
                }

                // 2. Version Check & Migration
                var gameStateJson = wrapper.GameStateData;
                if (wrapper.Version != CurrentAppVersion)
                {
                    _logger.LogWarning("Save file version mismatch. Slot {Slot} is version {SaveVersion}, current is {AppVersion}. Attempting migration.",
                        slot, wrapper.Version, CurrentAppVersion);
                    
                    gameStateJson = await _migrationManager.MigrateAsync(gameStateJson, wrapper.Version, cancellationToken);
                }

                // 3. Final Deserialization
                var gameState = JsonSerializer.Deserialize<GameState>(gameStateJson, SerializerOptions);
                if (gameState is null)
                {
                    // This could happen if migration results in invalid JSON
                    throw new SaveFileCorruptedException($"Failed to deserialize game state for slot {slot} after validation and migration.");
                }

                _logger.LogInformation("Successfully loaded game from slot {Slot}.", slot);
                return gameState;
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Failed to parse save file for slot {Slot}. File is likely corrupt.", slot);
                throw new SaveFileCorruptedException($"Failed to parse save file for slot {slot}. See inner exception for details.", jsonEx);
            }
            catch (SaveFileCorruptedException)
            {
                // Re-throw to allow specific handling upstream
                throw;
            }
            catch (UnsupportedSaveVersionException)
            {
                // Re-throw to allow specific handling upstream
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while loading game from slot {Slot}.", slot);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<SaveGameMetadata>> ListSavesAsync(CancellationToken cancellationToken = default)
        {
            var saveDirectory = _pathProvider.GetSaveDirectory();
            var maxSlots = 5; // As per REQ-1-086
            var metadataList = new List<SaveGameMetadata>();

            for (int i = 1; i <= maxSlots; i++)
            {
                var filePath = _pathProvider.GetSaveFilePath(i);
                var metadata = new SaveGameMetadata { Slot = i, Status = SaveStatus.Empty };

                if (File.Exists(filePath))
                {
                    try
                    {
                        var fileContent = await File.ReadAllTextAsync(filePath, Encoding.UTF8, cancellationToken);
                        var wrapper = JsonSerializer.Deserialize<SaveFileWrapper>(fileContent, SerializerOptions);

                        if (wrapper is null || string.IsNullOrWhiteSpace(wrapper.GameStateData))
                        {
                            metadata.Status = SaveStatus.Corrupted;
                        }
                        else
                        {
                            var calculatedChecksum = ComputeChecksum(wrapper.GameStateData);
                            if (!string.Equals(calculatedChecksum, wrapper.Checksum, StringComparison.OrdinalIgnoreCase))
                            {
                                metadata.Status = SaveStatus.Corrupted;
                            }
                            else if (!_migrationManager.IsVersionSupported(wrapper.Version))
                            {
                                metadata.Status = SaveStatus.Incompatible;
                            }
                            else
                            {
                                metadata.Status = SaveStatus.Valid;
                                metadata.SaveTimestamp = wrapper.SaveTimestamp;
                                // Partially deserialize GameStateData to get metadata without loading the whole object
                                using var doc = JsonDocument.Parse(wrapper.GameStateData);
                                var root = doc.RootElement;
                                if (root.TryGetProperty("gameMetadata", out var gameMetadataElement) &&
                                    gameMetadataElement.TryGetProperty("currentTurnNumber", out var turnElement))
                                {
                                    metadata.TurnNumber = turnElement.GetInt32();
                                }
                                if (root.TryGetProperty("playerStates", out var playersElement) && playersElement.EnumerateArray().Any())
                                {
                                    var humanPlayer = playersElement.EnumerateArray()
                                        .FirstOrDefault(p => p.TryGetProperty("isHuman", out var isHuman) && isHuman.GetBoolean());
                                    if(humanPlayer.TryGetProperty("playerName", out var nameElement))
                                    {
                                        metadata.PlayerName = nameElement.GetString() ?? "Player";
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Could not read metadata for save slot {Slot}. Marking as corrupted.", i);
                        metadata.Status = SaveStatus.Corrupted;
                    }
                }
                
                metadataList.Add(metadata);
            }

            return metadataList;
        }
        
        /// <inheritdoc/>
        public Task DeleteAsync(int slot, CancellationToken cancellationToken = default)
        {
            if (slot <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(slot), "Save slot must be a positive integer.");
            }

            var filePath = _pathProvider.GetSaveFilePath(slot);

            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    _logger.LogInformation("Deleted save file for slot {Slot}.", slot);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete save file for slot {Slot}.", slot);
                throw;
            }
            
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            var saveDirectory = _pathProvider.GetSaveDirectory();
            try
            {
                if (Directory.Exists(saveDirectory))
                {
                    var files = Directory.GetFiles(saveDirectory, "save_slot_*.json");
                    foreach (var file in files)
                    {
                        File.Delete(file);
                    }
                    _logger.LogInformation("Deleted all save files from {Directory}.", saveDirectory);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete all save files from {Directory}.", saveDirectory);
                throw;
            }

            return Task.CompletedTask;
        }


        private string ComputeChecksum(string data)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(data);
            var hash = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
}