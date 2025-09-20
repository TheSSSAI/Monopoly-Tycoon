using System;
using System.Collections.Generic;

namespace MonopolyTycoon.Presentation.Features.HUD.Views
{
    // A simple DTO for passing player info to the view.
    public record PlayerHUDViewModel(string PlayerId, string Name, string Cash, object TokenSprite, bool IsBankrupt);

    /// <summary>
    /// Contract for the Heads-Up Display (HUD) view. It defines methods to update
    /// the visual state of the HUD without exposing Unity-specific components.
    /// Fulfills REQ-1-071 and US-049.
    /// </summary>
    public interface IHUDView
    {
        /// <summary>
        /// Event fired when the user clicks the button to manage their properties.
        /// </summary>
        event Action OnManagePropertiesClicked;

        /// <summary>
        /// Event fired when the user clicks the button to initiate a trade.
        /// </summary>
        event Action OnTradeClicked;

        /// <summary>
        /// Event fired when the user clicks the button to roll the dice.
        /// </summary>
        event Action OnRollDiceClicked;
        
        /// <summary>
        /// Event fired when the user clicks the settings/pause button.
        /// </summary>
        event Action OnSettingsClicked;

        /// <summary>
        /// Initializes the HUD with the players for the current game.
        /// </summary>
        /// <param name="players">A list of view models for each player in the game.</param>
        void SetupPlayerPanels(IReadOnlyList<PlayerHUDViewModel> players);

        /// <summary>
        /// Updates the displayed information for all players.
        /// </summary>
        /// <param name="players">A list containing the latest state for each player.</param>
        void UpdatePlayerDisplays(IReadOnlyList<PlayerHUDViewModel> players);

        /// <summary>
        /// Sets the visual indicator for the currently active player.
        /// </summary>
        /// <param name="activePlayerId">The unique ID of the player whose turn it is.</param>
        void SetActivePlayer(string activePlayerId);

        /// <summary>
        /// Sets the enabled/disabled state of player action buttons.
        /// </summary>
        /// <param name="canRoll">Whether the 'Roll Dice' button should be enabled.</param>
        /// <param name="canManage">Whether management buttons (Trade, Properties) should be enabled.</param>
        void SetActionButtonsState(bool canRoll, bool canManage);

        /// <summary>
        /// Displays a non-intrusive, auto-dismissing notification.
        /// Used for events like AI-to-AI trades (US-043).
        /// </summary>
        /// <param name="message">The text to display in the notification.</param>
        /// <param name="duration">How long the notification should be visible in seconds.</param>
        void ShowNotification(string message, float duration = 3.0f);
    }
}