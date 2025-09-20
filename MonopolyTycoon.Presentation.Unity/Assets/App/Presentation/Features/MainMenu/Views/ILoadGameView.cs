using System;
using System.Collections.Generic;
using MonopolyTycoon.Presentation.Features.LoadGame.ViewModels;

namespace MonopolyTycoon.Presentation.Features.MainMenu.Views
{
    /// <summary>
    /// Contract for the Load Game view. This interface allows the presenter
    /// to interact with the view without any knowledge of its Unity implementation.
    /// </summary>
    public interface ILoadGameView
    {
        /// <summary>
        /// Event fired when the user requests to load a game from a specific slot.
        /// </summary>
        event Action<int> OnLoadGameRequested;

        /// <summary>
        /// Event fired when the user requests to delete a game from a specific slot.
        /// </summary>
        event Action<int> OnDeleteGameRequested;

        /// <summary>
        /// Event fired when the user clicks the back button to return to the main menu.
        /// </summary>
        event Action OnBackRequested;

        /// <summary>
        /// Populates the view with the metadata for all available save slots.
        /// </summary>
        /// <param name="saveSlots">A list of metadata objects, one for each save slot.</param>
        void DisplaySaveSlots(IReadOnlyList<SaveGameMetadata> saveSlots);

        /// <summary>
        /// Shows a loading indicator.
        /// </summary>
        void ShowLoading();

        /// <summary>
        /// Hides the loading indicator.
        /// </summary>
        void HideLoading();

        /// <summary>
        /// Shows an error message to the user.
        /// </summary>
        /// <param name="message">The error message to display.</param>
        void ShowError(string message);
    }
}