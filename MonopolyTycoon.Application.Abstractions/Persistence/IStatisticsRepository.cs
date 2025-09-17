using MonopolyTycoon.Application.Abstractions.Persistence;
using MonopolyTycoon.Domain.Models;

namespace MonopolyTycoon.Application.Abstractions.Persistence;

/// <summary>
/// Defines the contract for persisting and retrieving player statistics and game results.
/// This abstraction decouples the application from the underlying database technology (e.g., SQLite).
/// </summary>
public interface IStatisticsRepository
{
    /// <summary>
    /// Handles the creation and schema setup of the statistics database on application startup.
    /// This method is responsible for ensuring the database is ready for use.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MonopolyTycoon.Domain.Common.Exceptions.UnrecoverableDataException">
    /// Thrown if the database cannot be initialized or recovered.
    /// </exception>
    Task InitializeDatabaseAsync();

    /// <summary>
    /// Asynchronously performs an atomic/transactional operation to update a player's aggregate statistics
    /// and record a new game result at the end of a game.
    /// </summary>
    /// <param name="result">The completed game's result object, containing all necessary data for the update.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="MonopolyTycoon.Domain.Common.Exceptions.StatisticsUpdateException">
    /// Thrown if the database update operation fails.
    /// </exception>
    Task UpdatePlayerStatisticsAsync(GameResult result);

    /// <summary>
    /// Asynchronously queries the database and returns an ordered list of the top scores.
    /// </summary>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. The task result contains
    /// a <see cref="List{T}"/> of <see cref="TopScore"/> objects. Returns an empty list if no scores exist
    /// or if the database is inaccessible.
    /// </returns>
    Task<List<TopScore>> GetTopScoresAsync();
    
    /// <summary>
    /// Asynchronously retrieves the aggregate statistics for a given player profile.
    /// </summary>
    /// <param name="profileId">The unique identifier of the player profile.</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. The task result contains
    /// the <see cref="PlayerStatistic"/> object for the player, or null if not found.
    /// </returns>
    Task<PlayerStatistic?> GetStatisticsAsync(Guid profileId);

    /// <summary>
    /// Asynchronously deletes all player statistics data, including game results and high scores.
    /// This method is used to fulfill the 'Reset Statistics' user setting (REQ-1-080).
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task ResetStatisticsDataAsync();
}