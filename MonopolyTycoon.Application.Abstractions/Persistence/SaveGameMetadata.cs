using System;

namespace MonopolyTycoon.Application.Abstractions.Persistence
{
    /// <summary>
    /// A lightweight Data Transfer Object (DTO) used to convey summary information
    /// about a saved game file without loading the entire game state.
    /// </summary>
    /// <param name="SlotNumber">The unique identifier for the save slot (e.g., 1-5).</param>
    /// <param name="SaveTimestamp">The date and time the game was saved.</param>
    /// <param name="Status">The integrity status of the save file (e.g., Valid, Corrupted, Empty).</param>
    /// <param name="PlayerName">The name of the human player in the saved game.</param>
    /// <param name="TurnNumber">The turn number at the time of the save.</param>
    public record SaveGameMetadata(
        int SlotNumber,
        DateTime SaveTimestamp,
        SaveStatus Status,
        string PlayerName,
        int TurnNumber
    );
}