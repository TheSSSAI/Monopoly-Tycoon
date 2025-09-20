using System.Threading.Tasks;

namespace MonopolyTycoon.Presentation.Features.GameBoard.Views
{
    /// <summary>
    /// Contract for the GameBoard's View component. It exposes methods for the 
    /// GameBoardPresenter to command, abstracting away the underlying Unity 
    /// GameObject and component details.
    /// </summary>
    public interface IGameBoardView
    {
        /// <summary>
        /// Executes a visual animation of a player's token moving along the board path to a new tile.
        /// The returned Task should complete when the animation finishes.
        /// Fulfills REQ-1-017 and US-016.
        /// </summary>
        /// <param name="playerId">The unique identifier for the player token to move.</param>
        /// <param name="startTileIndex">The starting position on the board (0-39).</param>
        /// <param name="endTileIndex">The target position on the board (0-39).</param>
        /// <returns>A task that completes when the animation is finished.</returns>
        Task AnimateTokenMovementAsync(string playerId, int startTileIndex, int endTileIndex);

        /// <summary>
        /// Instantiates, removes, or updates the 3D models for houses and hotels on a specific property tile.
        /// </summary>
        /// <param name="tileIndex">The board index of the property.</param>
        /// <param name="houseCount">The number of houses to display (0-4).</param>
        /// <param name="hasHotel">Whether to display a hotel (replaces houses).</param>
        void UpdatePropertyVisuals(int tileIndex, int houseCount, bool hasHotel);

        /// <summary>
        /// Updates the visual ownership indicator for a specific property tile.
        /// Fulfills US-050.
        /// </summary>
        /// <param name="tileIndex">The board index of the property.</param>
        /// <param name="ownerId">The ID of the owning player, or null if unowned.</param>
        /// <param name="isMortgaged">Whether the property is mortgaged.</param>
        void SetPropertyOwnership(int tileIndex, string ownerId, bool isMortgaged);
        
        /// <summary>
        /// Plays the dice roll animation.
        /// </summary>
        /// <param name="result1">The result of the first die.</param>
        /// <param name="result2">The result of the second die.</param>
        /// <returns>A task that completes when the animation is finished.</returns>
        Task AnimateDiceRollAsync(int result1, int result2);
    }
}