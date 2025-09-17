using System;
using MonopolyTycoon.Presentation.Core;

namespace MonopolyTycoon.Presentation.Features.MainMenu.Views
{
    /// <summary>
    /// Defines the contract for the main menu view.
    /// This view is the player's first interaction point with the application.
    /// </summary>
    public interface IMainMenuView : IView
    {
        /// <summary>
        /// Event fired when the user clicks the "New Game" button.
        /// </summary>
        event Action OnNewGameClicked;

        /// <summary>
        /// Event fired when the user clicks the "Load Game" button.
        /// </summary>
        event Action OnLoadGameClicked;

        /// <summary>
        /// Event fired when the user clicks the "Settings" button.
        /// </summary>
        event Action OnSettingsClicked;
        
        /// <summary>
        /// Event fired when the user clicks the "Quit" button.
        /// </summary>
        event Action OnQuitClicked;

        /// <summary>
        /// Displays a notification for available updates.
        /// </summary>
        /// <param name="downloadUrl">The URL to the download page.</param>
        void ShowUpdateNotification(string downloadUrl);
    }
}