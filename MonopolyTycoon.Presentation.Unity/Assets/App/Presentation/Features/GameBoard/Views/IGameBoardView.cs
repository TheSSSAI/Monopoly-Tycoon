using System.Threading.Tasks;
using UnityEngine;
using MonopolyTycoon.Presentation.Core;
using System.Collections.Generic;

namespace MonopolyTycoon.Presentation.Features.GameBoard.Views
{
    /// <summary>
    /// Defines the contract for the view that manages the 3D game board and its visual elements.
    /// This includes player tokens, houses, hotels, and ownership indicators.
    /// </summary>
    public interface IGameBoardView : IView
    {
        /// <summary>
        /// Asynchronously animates a player's token moving from a start space to an end space.
        /// </summary>
        /// <param name="playerId">The ID of the player whose token is moving.</param>
        /// <param name="path">A list of board space indices representing the path of movement.</param>
        /// <param name="moveType">The type of movement, which may dictate a special animation.</param>
        /// <returns>A task that completes when the movement animation is finished.</returns>
        Task AnimateTokenMovementAsync(int playerId, List<int> path, TokenMoveType moveType);

        /// <summary>
        /// Instantiates or updates the visual representation of houses on a property.
        /// </summary>
        /// <param name="propertySpaceIndex">The board index of the property.</param>
        /// <param name="houseCount">The number of houses to display (0-4). A count of 5 indicates a hotel.</param>
        /// <returns>A task that completes when the visual update is finished.</returns>
        Task SetBuildingVisualsAsync(int propertySpaceIndex, int houseCount);

        /// <summary>
        /// Sets the visual indicator for property ownership.
        /// </summary>
        /// <param name="propertySpaceIndex">The board index of the property.</param>
        /// <param name="ownerId">The ID of the owning player, or a special value for unowned.</param>
        /// <param name="isMortgaged">Whether the property is mortgaged, which affects the indicator's appearance.</param>
        void SetOwnershipIndicator(int propertySpaceIndex, int? ownerId, bool isMortgaged);
        
        /// <summary>
        /// Initializes the board with player tokens at the start of the game.
        /// </summary>
        /// <param name="playerTokens">A dictionary mapping player IDs to their chosen token identifiers.</param>
        void InitializePlayerTokens(Dictionary<int, string> playerTokens);

        /// <summary>
        /// Triggers the dice roll animation.
        /// </summary>
        /// <returns>A tuple containing the results of the two dice.</returns>
        Task<(int, int)> AnimateDiceRollAsync();
    }
    
    /// <summary>
    /// Specifies the type of token movement for animation purposes.
    /// </summary>
    public enum TokenMoveType
    {
        Standard,
        GoToJail,
        CardEffect
    }
}