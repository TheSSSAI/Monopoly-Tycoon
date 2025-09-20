using MonopolyTycoon.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonopolyTycoon.Application.Abstractions.Persistence
{
    /// <summary>
    /// Defines the contract for a repository responsible for persisting and retrieving the game's state.
    /// This abstraction decouples the application logic from the specific implementation of game saving
    /// (e.g., JSON files, database).
    /// </summary>
    public interface ISaveGameRepository
    {
        /// <summary>
        /// Asynchronously saves the provided game state to a specified storage slot.
        /// The operation must be atomic.
        /// </summary>
        /// <param name="state">The complete game state object to be persisted.</param>
        /// <param name="slot">The identifier for the save slot (e.g., 1-5).</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains true if the save was successful; otherwise, false.</returns>
        /// <remarks>
        /// Fulfills requirement REQ-1-087. The implementation is expected to serialize the GameState,
        /// calculate a checksum (REQ-1-088), and write it to a versioned file.
        /// </remarks>
        Task<bool> SaveAsync(GameState state, int slot);

        /// <summary>
        /// Asynchronously retrieves and deserializes a GameState object from the specified storage slot.
        /// </summary>
        /// <param name="slot">The identifier for the save slot from which to load the game state.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the loaded <see cref="GameState"/>
        /// if the slot contains a valid, compatible save; otherwise, returns null if the slot is empty.
        /// </returns>
        /// <exception cref="Domain.Common.Exceptions.DataCorruptionException">Thrown if the save file fails an integrity check (e.g., checksum mismatch).</exception>
        /// <exception cref="Domain.Common.Exceptions.DataMigrationException">Thrown if the save file is from an older version and the migration process fails.</exception>
        /// <remarks>
        /// Fulfills requirements REQ-1-087, REQ-1-088, and REQ-1-090.
        /// Implementations must handle version checking and may delegate to a migration manager.
        /// </remarks>
        Task<GameState?> LoadAsync(int slot);

        /// <summary>
        /// Asynchronously scans the storage medium and returns metadata for all available save games.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a list of <see cref="SaveGameMetadata"/>
        /// for all save slots.
        /// </returns>
        /// <remarks>
        /// Fulfills requirement REQ-1-088 and is critical for the 'Load Game' screen UI. The implementation
        /// is responsible for performing integrity checks and setting the <see cref="SaveGameMetadata.Status"/>
        /// on each object accordingly.
        /// </remarks>
        Task<List<SaveGameMetadata>> ListSavesAsync();

        /// <summary>
        /// Asynchronously deletes all save game files from the persistence storage.
        /// </summary>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        /// <remarks>
        /// Fulfills the 'Delete Saved Games' user setting from REQ-1-080.
        /// The implementation should handle cases where the save directory does not exist gracefully.
        /// </remarks>
        Task DeleteAllAsync();
    }
}