using UnityEngine;

namespace MonopolyTycoon.Presentation.Features.GameBoard.Views
{
    /// <summary>
    /// Represents the visual game piece for a player on the board.
    /// This component is attached to the token prefab.
    /// </summary>
    public class TokenView : MonoBehaviour
    {
        /// <summary>
        /// The unique identifier of the player this token represents.
        /// </summary>
        public string PlayerId { get; private set; }

        private bool _isInitialized = false;

        /// <summary>
        /// Initializes the token with the player's ID.
        /// </summary>
        /// <param name="playerId">The unique ID of the player.</param>
        public void Initialize(string playerId)
        {
            if (_isInitialized)
            {
                Debug.LogWarning($"[TokenView] Token for player {PlayerId} is already initialized. Re-initializing for {playerId}.");
            }
            
            PlayerId = playerId;
            gameObject.name = $"Token_{PlayerId}";
            _isInitialized = true;
        }
    }
}