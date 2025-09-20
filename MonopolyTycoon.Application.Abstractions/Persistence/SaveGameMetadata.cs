namespace MonopolyTycoon.Application.Abstractions.Persistence;

/// <summary>
/// A lightweight Data Transfer Object used to convey summary information about a saved game file
/// without loading the entire game state. This is used to populate the 'Load Game' screen.
/// </summary>
/// <param name="SlotNumber">The unique identifier for the save slot (e.g., 1-5).</param>
/// <param name="Status">Communicates the integrity status of the save file to the UI (e.g., Valid, Corrupted).</param>
/// <param name="PlayerName">The name of the human player's profile associated with the save.</param>
/// <param name="SaveTimestamp">The date and time the game was saved.</param>
/// <param name="TurnNumber">The turn number at which the game was saved.</param>
public record SaveGameMetadata(
    int SlotNumber,
    SaveStatus Status,
    string PlayerName,
    DateTime SaveTimestamp,
    int TurnNumber
);