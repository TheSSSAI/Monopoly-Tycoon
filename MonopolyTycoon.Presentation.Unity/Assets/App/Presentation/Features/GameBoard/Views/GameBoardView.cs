using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using MonopolyTycoon.Application.Abstractions;
using MonopolyTycoon.Presentation.Features.GameBoard.Views;

namespace MonopolyTycoon.Presentation.Features.GameBoard.Views
{
    public class GameBoardView : MonoBehaviour, IGameBoardView
    {
        [Header("References")]
        [SerializeField] private Transform[] _tilePositions;
        [SerializeField] private Transform _tokenParent;

        [Header("Configuration")]
        [SerializeField] private float _moveSpeed = 5.0f;
        [SerializeField] private float _hopHeight = 0.5f;
        [SerializeField] private float _hopDuration = 0.2f;

        private readonly Dictionary<string, TokenView> _tokenViews = new();
        private IAssetProvider _assetProvider; // Injected by a scene context installer

        [Inject]
        public void Construct(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async Task<TokenView> InstantiateTokenAsync(string playerId, string tokenAddressableKey)
        {
            if (_tokenViews.ContainsKey(playerId))
            {
                Debug.LogWarning($"[GameBoardView] Token for player {playerId} already exists.");
                return _tokenViews[playerId];
            }

            var tokenPrefab = await _assetProvider.LoadAssetAsync<GameObject>(tokenAddressableKey);
            if (tokenPrefab == null)
            {
                Debug.LogError($"[GameBoardView] Failed to load token prefab with key: {tokenAddressableKey}");
                return null;
            }

            var tokenInstance = Instantiate(tokenPrefab, _tokenParent);
            var tokenView = tokenInstance.GetComponent<TokenView>();
            if (tokenView == null)
            {
                Debug.LogError($"[GameBoardView] Token prefab {tokenAddressableKey} is missing TokenView component.");
                Destroy(tokenInstance);
                return null;
            }
            
            tokenView.Initialize(playerId);
            _tokenViews[playerId] = tokenView;
            
            SetTokenPosition(tokenView, 0); // Start at GO
            return tokenView;
        }

        public async Task AnimateTokenToPositionAsync(string playerId, int startTileIndex, int endTileIndex)
        {
            if (!_tokenViews.TryGetValue(playerId, out var tokenView))
            {
                Debug.LogError($"[GameBoardView] Cannot animate token for unknown player ID: {playerId}");
                return;
            }

            int currentTile = startTileIndex;
            while (currentTile != endTileIndex)
            {
                int nextTile = (currentTile + 1) % _tilePositions.Length;
                await AnimateSingleHopAsync(tokenView, _tilePositions[currentTile].position, _tilePositions[nextTile].position);
                currentTile = nextTile;
            }
            
            // Final landing sound/effect could be triggered here
        }
        
        public void UpdatePropertyVisuals(int tileIndex, int houseCount, bool isMortgaged)
        {
            // This is a stub for a more complex visual system.
            // In a full implementation, this would involve instantiating/destroying house/hotel models
            // and applying a "mortgaged" overlay/shader to the property tile.
            Debug.Log($"[GameBoardView] Updating visuals for tile {tileIndex}: Houses={houseCount}, Mortgaged={isMortgaged}");
        }

        public void SetTokenPosition(string playerId, int tileIndex)
        {
            if (_tokenViews.TryGetValue(playerId, out var tokenView))
            {
                SetTokenPosition(tokenView, tileIndex);
            }
        }
        
        private void SetTokenPosition(TokenView tokenView, int tileIndex)
        {
            if (tileIndex < 0 || tileIndex >= _tilePositions.Length)
            {
                Debug.LogError($"[GameBoardView] Invalid tile index: {tileIndex}");
                return;
            }
            tokenView.transform.position = _tilePositions[tileIndex].position;
            tokenView.transform.rotation = _tilePositions[tileIndex].rotation;
        }
        
        private async Task AnimateSingleHopAsync(TokenView tokenView, Vector3 startPos, Vector3 endPos)
        {
            float elapsedTime = 0f;
            
            Vector3 peakPos = (startPos + endPos) / 2f + Vector3.up * _hopHeight;

            while (elapsedTime < _hopDuration)
            {
                float t = elapsedTime / _hopDuration;
                
                // Quadratic Bezier curve for the hop
                Vector3 m1 = Vector3.Lerp(startPos, peakPos, t);
                Vector3 m2 = Vector3.Lerp(peakPos, endPos, t);
                tokenView.transform.position = Vector3.Lerp(m1, m2, t);

                // Simple look-ahead rotation
                Vector3 direction = (endPos - startPos).normalized;
                if(direction != Vector3.zero)
                {
                    tokenView.transform.rotation = Quaternion.LookRotation(direction);
                }

                elapsedTime += Time.deltaTime;
                await Task.Yield(); // Wait for the next frame
            }

            tokenView.transform.position = endPos;
        }
    }
}