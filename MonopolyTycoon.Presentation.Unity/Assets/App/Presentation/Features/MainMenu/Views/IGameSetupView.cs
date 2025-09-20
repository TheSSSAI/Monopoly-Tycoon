using System;
using System.Collections.Generic;

namespace MonopolyTycoon.Presentation.Features.MainMenu.Views
{
    /// <summary>
    /// Defines the contract for the Game Setup screen view.
    /// </summary>
    public interface IGameSetupView
    {
        #region Events

        /// <summary>
        /// Fired when the user confirms their settings and wants to start the new game.
        /// </summary>
        event Action<GameSetupViewModel> OnStartGameRequested;

        /// <summary>
        /// Fired when the user requests to go back to the main menu.
        /// </summary>
        event Action OnBackRequested;

        #endregion

        #region Methods

        /// <summary>
        /// Sets the initial state of the view with default values.
        /// </summary>
        /// <param name="availableTokens">A list of available token keys for the player to choose from.</param>
        void SetInitialState(List<string> availableTokens);

        /// <summary>
        /// Displays a validation error message for the player name field.
        /// </summary>
        /// <param name="message">The error message to display. An empty or null string clears the message.</param>
        void SetPlayerNameError(string message);

        /// <summary>
        /// Updates the UI to show the correct number of configuration sections for AI opponents.
        /// </summary>
        /// <param name="count">The number of AI opponents to configure (1, 2, or 3).</param>
        void SetAIOpponentCount(int count);

        #endregion
    }

    /// <summary>
    /// ViewModel representing the final configuration chosen by the player on the setup screen.
    /// </summary>
    public class GameSetupViewModel
    {
        public string PlayerName { get; set; }
        public string SelectedTokenKey { get; set; }
        public List<AIOpponentSettings> AIOpponents { get; set; } = new();
    }

    public class AIOpponentSettings
    {
        public string Difficulty { get; set; } // e.g., "Easy", "Medium", "Hard"
    }
}