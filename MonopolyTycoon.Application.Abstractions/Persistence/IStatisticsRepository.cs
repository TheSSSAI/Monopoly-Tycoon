using MonopolyTycoon.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonopolyTycoon.Application.Abstractions.Persistence
{
    /// <summary>
    /// Defines the contract for a repository that manages player statistics and game results.
    /// This abstraction decouples the application from the specific database technology (e.g., SQLite).
    /// </summary>
    public interface IStatisticsRepository
    {
        /// <summary>
        /// Ensures the statistics database is initialized, creating it and its schema if necessary.
        /// This method should also handle recovery from backups if the primary database is corrupt.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MonopolyTycoon.Domain.Common.Exceptions.UnrecoverableDataException">
        /// Thrown if the database is corrupt and cannot be recovered from backups.
        /// </exception>
        Task InitializeDatabaseAsync();

        /// <summary>
        /// Atomically updates the persistent player statistics based on the result of a completed game.
        /// This involves updating aggregate stats and inserting a new game history record.
        /// </summary>
        /// <param name="result">The <see cref="GameResult"/> object containing all data from the completed game.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="MonopolyTycoon.Domain.Common.Exceptions.StatisticsUpdateException">
        /// Thrown if the database operation fails and cannot be completed.
        /// </exception>
        Task UpdatePlayerStatisticsAsync(GameResult result);

        /// <summary>
        /// Retrieves the list of top 10 scores from the persistence layer.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> that represents the asynchronous operation. 
        /// The task result contains a <see cref="List{T}"/> of <see cref="TopScore"/> objects, ordered by rank.</returns>
        Task<List<TopScore>> GetTopScoresAsync();

        /// <summary>
        /// Permanently deletes all player statistics and high score data.
        /// This action is irreversible and fulfills the "Reset Statistics" user setting (REQ-1-080).
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task ResetStatisticsDataAsync();
    }
}