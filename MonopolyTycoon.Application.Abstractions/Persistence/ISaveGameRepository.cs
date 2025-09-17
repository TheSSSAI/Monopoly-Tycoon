using MonopolyTycoon.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonopolyTycoon.Application.Abstractions.Persistence
{
    /// <summary>
    /// Defines the contract for a repository that manages the persistence of game states.
    /// This abstraction decouples the application's core logic from the specific implementation
    /// of how game saves are stored (e.g., local JSON files, database, cloud storage).
    /// </summary>
    public interface ISaveGameRepository
    {
        /// <summary>
        /// Asynchronously saves the provided game state to a specified slot.
        /// This operation should be atomic; it either completes successfully or fails without
        /// corrupting the existing save file in that slot.
        /// </summary>
        /// <param name="state">The complete <see cref="GameState"/> object to be persisted.</param>
        /// <param name="slot">The integer identifier for the save slot (e.g., 1-5).</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains true if the save was successful; otherwise, false.</returns>
        /// <remarks>
        /// REQ-1-087: The system shall serialize the GameState object into a versioned JSON format for saving.
        /// REQ-1-088: The system must implement a checksum or hash validation mechanism for all save files.
        /// </remarks>
        Task<bool> SaveAsync(GameState state, int slot);

        /// <summary>
        /// Asynchronously loads a game state from a specified slot.
        /// The implementation is responsible for validating the file's integrity before deserialization.
        /// </summary>
        /// <param name="slot">The integer identifier for the save slot to load from.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains the deserialized <see cref="GameState"/> object.
        /// </returns>
        /// <exception cref="Domain.Common.Exceptions.SaveSlotNotFoundException">Thrown if no save file exists for the specified slot.</exception>
        /// <exception cref="Domain.Common.Exceptions.DataCorruptionException">Thrown if the save file fails an integrity check (e.g., checksum mismatch).</exception>
        /// <exception cref="Domain.Common.Exceptions.DataMigrationException">Thrown if the save file is an older version and migration fails.</exception>
        /// <remarks>
        /// REQ-1-015: The system shall ensure that the time elapsed from initiating a 'Load Game' action... shall not exceed 10 seconds.
        /// REQ-1-090: The system must include a data migration mechanism to handle older versions of save files.
        /// </remarks>
        Task<GameState> LoadAsync(int slot);

        /// <summary>
        /// Asynchronously retrieves metadata for all available save game slots.
        /// This method is used to populate the 'Load Game' screen without loading the entire game state for each slot.
        /// The implementation is responsible for performing integrity and version checks.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains a list of <see cref="SaveGameMetadata"/> objects, one for each potential save slot.
        /// </returns>
        /// <remarks>
        /// REQ-1-088: When displaying the list of saved games, the system must check the integrity and version compatibility of each file.
        /// </remarks>
        Task<List<SaveGameMetadata>> ListSavesAsync();

        /// <summary>
        /// Asynchronously deletes all existing save game files.
        /// This is a destructive operation used by the in-game settings menu.
        /// </summary>
        /// <returns>A task that represents the completion of the asynchronous delete operation.</returns>
        /// <remarks>
        /// REQ-1-080: The settings menu shall contain data management options to ... 'Delete Saved Games'.
        /// </remarks>
        Task DeleteAllAsync();
    }
}