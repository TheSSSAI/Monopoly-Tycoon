using System;

namespace MonopolyTycoon.Presentation.Features.MainMenu.Views
{
    /// <summary>
    /// Contract for the Main Menu view. This interface allows the MainMenuPresenter
    /// to be completely decoupled from the Unity scene and GameObjects.
    /// </summary>
    public interface IMainMenuView
    {
        /// <summary>
        /// Event fired when the user clicks the 'New Game' button.
        /// </summary>
        event Action OnNewGameClicked;

        /// <summary>
        /// Event fired when the user clicks the 'Load Game' button.
        /// </summary>
        event Action OnLoadGameClicked;

        /// <summary>
        /// Event fired when the user clicks the 'Settings' button.
        /// </summary>
        event Action OnSettingsClicked;

        /// <summary>
        /// Event fired when the user clicks the 'Quit' button.
        /// </summary>
        event Action OnQuitClicked;

        /// <summary>
        /// Displays an update notification on the main menu.
        /// </summary>
        /// <param name="version">The new version string to display.</param>
        /// <param name="onUpdateClicked">The action to perform when the user clicks the notification.</param>
        void ShowUpdateNotification(string version, Action onUpdateClicked);
    }
}