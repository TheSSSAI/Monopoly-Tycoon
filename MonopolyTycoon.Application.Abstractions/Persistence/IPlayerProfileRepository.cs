using System.Threading.Tasks;
using MonopolyTycoon.Domain.Entities;

namespace MonopolyTycoon.Application.Abstractions.Persistence
{
    /// <summary>
    /// Defines the contract for a repository responsible for managing player profiles.
    /// This abstraction decouples application services from the specific database
    /// technology used to store player identity information.
    /// Fulfills requirements: REQ-1-032, REQ-1-033, REQ-1-089.
    /// </summary>
    public interface IPlayerProfileRepository
    {
        /// <summary>
        /// Asynchronously retrieves a player profile by its display name. If no profile
        /// exists with the given name, a new one is created, persisted, and returned.
        /// This operation must be atomic.
        /// </summary>
        /// <param name="displayName">The display name of the player profile to find or create.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains
        /// the existing or newly created <see cref="PlayerProfile"/>.
        /// </returns>
        /// <exception cref="System.ArgumentException">Thrown if the display name is invalid.</exception>
        Task<PlayerProfile> GetOrCreateProfileAsync(string displayName);
    }
}