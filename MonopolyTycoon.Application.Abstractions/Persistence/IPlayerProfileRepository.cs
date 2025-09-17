using MonopolyTycoon.Domain.Entities;
using System.Threading.Tasks;

namespace MonopolyTycoon.Application.Abstractions.Persistence
{
    /// <summary>
    /// Defines the contract for a repository responsible for managing player profiles.
    /// This abstraction decouples application logic from the specific implementation of player profile storage.
    /// </summary>
    public interface IPlayerProfileRepository
    {
        /// <summary>
        /// Asynchronously retrieves a player profile by its display name. If no profile exists with that name,
        /// it creates a new one and persists it before returning it. This operation must be atomic.
        /// </summary>
        /// <param name="displayName">The display name of the player profile to find or create.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the existing or newly created <see cref="PlayerProfile"/>.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown if the display name is invalid (e.g., null, empty, or fails validation rules).</exception>
        Task<PlayerProfile> GetOrCreateProfileAsync(string displayName);
    }
}