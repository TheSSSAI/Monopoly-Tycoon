using System;

namespace MonopolyTycoon.Presentation.Features.LoadGame.ViewModels
{
    /// <summary>
    /// Represents the status of a save game slot.
    /// Used by the presentation layer to determine how to render the slot in the UI.
    /// </summary>
    public enum SaveStatus
    {
        /// <summary>
        /// The slot is empty and available for a new save.
        /// </summary>
        Empty,
        /// <summary>
        /// The slot contains a valid, loadable save file.
        /// </summary>
        Valid,
        /// <summary>
        /// The save file's checksum validation failed, indicating it is corrupt.
        /// </summary>
        Corrupted,
        /// <summary>
        /// The save file was created with an older, incompatible version of the game.
        /// </summary>
        IncompatibleVersion
    }

    /// <summary>
    /// A view-specific data model used to populate the list of save slots in the Load Game UI.
    /// This is a projection of data received from the Application layer, formatted for display.
    /// Fulfills requirements from US-062 and US-063.
    /// </summary>
    public class SaveGameMetadata
    {
        /// <summary>
        /// The numerical identifier for the save slot (e.g., 1-5).
        /// </summary>
        public int SlotNumber { get; set; }

        /// <summary>
        /// The profile name of the player who created the save.
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        /// The date and time the game was saved, formatted for display.
        /// </summary>
        public string SaveTimestamp { get; set; }
        
        /// <summary>
        /// The turn number when the game was saved.
        /// </summary>
        public int TurnNumber { get; set; }

        /// <summary>
        /// The integrity and compatibility status of the save file.
        /// </summary>
        public SaveStatus Status { get; set; }
    }
}