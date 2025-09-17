using System;

namespace MonopolyTycoon.Application.Abstractions.Persistence
{
    /// <summary>
    /// A Data Transfer Object representing a single entry in the list of top scores.
    /// Implemented as a record for immutability and value-based equality.
    /// </summary>
    /// <param name="PlayerName">The name of the player who achieved the score.</param>
    /// <param name="FinalNetWorth">The final net worth, which serves as the score.</param>
    /// <param name="GameDuration">The duration of the game.</param>
    /// <param name="TotalTurns">The total number of turns in the game.</param>
    /// <param name="EndTimestamp">The date and time the game concluded.</param>
    public record TopScore(
        string PlayerName,
        decimal FinalNetWorth,
        TimeSpan GameDuration,
        int TotalTurns,
        DateTime EndTimestamp
    );
}