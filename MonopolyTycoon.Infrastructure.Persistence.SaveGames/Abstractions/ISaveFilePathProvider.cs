namespace MonopolyTycoon.Infrastructure.Persistence.SaveGames.Abstractions;

/// <summary>
/// Defines the contract for a service that provides the physical file paths for save games.
/// This abstraction decouples the repository from the underlying file system and path resolution logic,
/// improving testability and configurability.
/// </summary>
public interface ISaveFilePathProvider
{
    /// <summary>
    /// Returns the full, absolute path for the save file corresponding to the given slot number.
    /// </summary>
    /// <param name="slot">The desired save slot number (must be a positive integer).</param>
    /// <returns>The absolute file path for the specified save slot.</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">Thrown if the slot number is not a positive integer.</exception>
    string GetSaveFilePath(int slot);

    /// <summary>
    /// Returns the full, absolute path to the directory where all save files are stored.
    /// This method ensures the directory exists, creating it if necessary.
    /// </summary>
    /// <returns>The absolute directory path for save games.</returns>
    string GetSaveDirectory();
}