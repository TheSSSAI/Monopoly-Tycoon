using System;
using System.Collections.Generic;
using MonopolyTycoon.Presentation.Core;

namespace MonopolyTycoon.Presentation.Features.MainMenu.Views
{
    /// <summary>
    /// Defines the contract for the view that displays saved game slots.
    /// Supports User Stories US-062 and US-063.
    /// </summary>
    public interface ILoadGameView : IView
    {
        /// <summary>
        /// Event fired when the user clicks to load a specific save slot.
        /// The payload is the slot number (e.g., 0-4).
        /// </summary>
        event Action<int> OnLoadSlotClicked;
        
        /// <summary>
        /// Event fired when the user clicks the back button to return to the main menu.
        /// </summary>
        event Action OnBackClicked;

        /// <summary>
        /// Populates the view with the list of save slots and their statuses.
        /// </summary>
        /// <param name="saveSlots">A list of view models representing each save slot.</param>
        void DisplaySaveSlots(List<SaveSlotViewModel> saveSlots);
    }

    /// <summary>
    /// ViewModel containing the data for a single save slot in the UI.
    /// </summary>
    public class SaveSlotViewModel
    {
        public int SlotNumber { get; set; }
        public SaveSlotStatus Status { get; set; }
        public string PlayerName { get; set; }
        public string SaveTimestamp { get; set; } // Formatted string
        public int TurnNumber { get; set; }
        public string PlayerCashFormatted { get; set; }
    }

    /// <summary>
    /// Represents the status of a save game slot.
    /// </summary>
    public enum SaveSlotStatus
    {
        Empty,
        Valid,
        Corrupted,
        IncompatibleVersion
    }
}