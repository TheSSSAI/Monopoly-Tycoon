using UnityEngine;

namespace MonopolyTycoon.Presentation.Features.GameBoard.Views
{
    /// <summary>
    /// A MonoBehaviour component attached to a player token prefab.
    /// It holds identifying information and can be used to control
    /// token-specific visual effects or animations.
    /// </summary>
    public class TokenView : MonoBehaviour
    {
        [SerializeField]
        private string _playerId;

        public string PlayerId => _playerId;

        public void Initialize(string playerId)
        {
            _playerId = playerId;
            gameObject.name = $"Token_{playerId}";
        }
    }
}