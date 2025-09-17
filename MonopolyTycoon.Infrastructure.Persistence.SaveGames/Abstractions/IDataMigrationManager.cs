using System.Threading.Tasks;

namespace MonopolyTycoon.Infrastructure.Persistence.SaveGames.Abstractions;

/// <summary>
/// Defines the contract for a service that can upgrade save file data from an older
/// application version to the current version's schema.
/// This fulfills the requirement for backward compatibility (REQ-1-090).
/// </summary>
public interface IDataMigrationManager
{
    /// <summary>
    /// Asynchronously transforms the provided raw JSON string of a game state through a series
    /// of migration steps to match the current data schema.
    /// </summary>
    /// <param name="rawGameStateJson">The raw JSON content of the GameState object from an older save file.</param>
    /// <param name="sourceVersion">The version of the application that created the save file (e.g., "1.0.0").</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the migrated
    /// raw JSON string, ready for deserialization into the current GameState model.
    /// </returns>
    /// <exception cref="MonopolyTycoon.Infrastructure.Persistence.SaveGames.Exceptions.UnsupportedSaveVersionException">
    /// Thrown if there is no defined migration path from the specified <paramref name="sourceVersion"/>.
    /// </exception>
    Task<string> MigrateAsync(string rawGameStateJson, string sourceVersion);
}