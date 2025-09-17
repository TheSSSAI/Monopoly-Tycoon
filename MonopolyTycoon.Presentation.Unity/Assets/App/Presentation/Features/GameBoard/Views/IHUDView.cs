using System;
using System.Collections.Generic;
using MonopolyTycoon.Presentation.Core;

namespace MonopolyTycoon.Presentation.Features.GameBoard.Views
{
    /// <summary>
    /// Defines the contract for the main game's Heads-Up Display (HUD).
    /// The HUD is responsible for showing at-a-glance information for all players.
    /// Fulfills requirement REQ-1-071 and supports User Story US-049.
    /// </summary>
    public interface IHUDView : IView
    {
        /// <summary>
        /// Event fired when the user clicks the "Manage Properties" button.
        /// </summary>
        event Action OnManagePropertiesClicked;

        /// <summary>
        /// Event fired when the user clicks the "Roll Dice" button.
        /// </summary>
        event Action OnRollDiceClicked;
        
        /// <summary>
        /// Event fired when the user clicks the in-game settings/pause button.
        /// </summary>
        event Action OnSettingsClicked;
        
        /// <summary>
        /// Initializes the HUD with data for all players in the game.
        /// </summary>
        /// <param name="playerViewModels">A list of view models, one for each player.</param>
        void InitializePlayerPanels(List<PlayerHUDViewModel> playerViewModels);
        
        /// <summary>
        /// Updates the cash display for a specific player.
        /// </summary>
        /// <param name="playerId">The ID of the player to update.</param>
        /// <param name="formattedCash">The player's new cash total, pre-formatted as a string.</param>
        void UpdatePlayerCash(int playerId, string formattedCash);
        
        /// <summary>
        /// Sets the visual indicator for the active player's turn.
        /// </summary>
        /// <param name="activePlayerId">The ID of the player whose turn it is.</param>
        void SetActivePlayer(int activePlayerId);
        
        /// <summary>
        /// Updates a player's panel to reflect their bankrupt status.
        /// </summary>
        /// <param name="playerId">The ID of the player who is now bankrupt.</param>
        void SetPlayerBankrupt(int playerId);

        /// <summary>
        /// Sets the enabled/disabled state of the Roll Dice button.
        /// </summary>
        /// <param name="isEnabled">True to enable, false to disable.</param>
        void SetRollDiceButtonState(bool isEnabled);
    }
    
    /// <summary>
    /// ViewModel containing the initial static data for a player's HUD panel.
    /// </summary>
    public class PlayerHUDViewModel
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string TokenId { get; set; } // Key to look up the token icon sprite
    }
}