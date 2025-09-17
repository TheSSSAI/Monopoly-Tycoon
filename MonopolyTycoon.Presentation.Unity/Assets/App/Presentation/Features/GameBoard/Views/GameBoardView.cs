using MonopolyTycoon.Presentation.Features.CommonUI.ScriptableObjects;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;

namespace MonopolyTycoon.Presentation.Features.GameBoard.Views
{
    /// <summary>
    /// Concrete MonoBehaviour implementation of IGameBoardView.
    /// This class is a passive view responsible for all visual representation on the 3D game board.
    /// It receives commands from the GameBoardPresenter and translates them into visual actions,
    /// such as moving tokens and updating property visuals.
    /// Fulfills visual aspects of REQ-1-005, REQ-1-017, REQ-1-072.
    /// </summary>
    public class GameBoardView : MonoBehaviour, IGameBoardView
    {
        [Header("Board Configuration")]
        [SerializeField, Tooltip("An array of 40 transforms representing the center of each board space, starting with GO as index 0.")]
        private Transform[] _spaceWaypoints;

        [Header("Asset Prefabs")]
        [SerializeField] private GameObject[] _tokenPrefabs;
        [SerializeField] private GameObject _housePrefab;
        [SerializeField] private GameObject _hotelPrefab;
        [SerializeField] private GameObject _ownershipMarkerPrefab;

        [Header("Animation Settings")]
        [SerializeField] private float _normalMoveSpeed = 5.0f;
        [SerializeField] private float _fastMoveSpeed = 15.0f;
        [SerializeField] private float _tokenHoverHeight = 0.2f;

        private Dictionary<Guid, GameObject> _tokenInstances = new Dictionary<Guid, GameObject>();
        private Dictionary<int, GameObject> _ownershipMarkers = new Dictionary<int, GameObject>();
        private Dictionary<int, List<GameObject>> _buildingInstances = new Dictionary<int, List<GameObject>>();
        // TODO: Implement object pooling for buildings for performance optimization.

        public void InitializeBoard(IEnumerable<(Guid playerId, int tokenId, string playerName)> players)
        {
            // Clear any existing state from a previous game.
            foreach (var token in _tokenInstances.Values) Destroy(token);
            _tokenInstances.Clear();

            foreach (var marker in _ownershipMarkers.Values) Destroy(marker);
            _ownershipMarkers.Clear();

            foreach (var buildingList in _buildingInstances.Values)
            {
                foreach (var building in buildingList) Destroy(building);
            }
            _buildingInstances.Clear();

            // Instantiate tokens for each player.
            Transform goSpace = _spaceWaypoints[0];
            foreach (var player in players)
            {
                if (player.tokenId < 0 || player.tokenId >= _tokenPrefabs.Length)
                {
                    Debug.LogError($"Invalid tokenId {player.tokenId} for player {player.playerName}. Using default.");
                    player.tokenId = 0;
                }

                GameObject tokenPrefab = _tokenPrefabs[player.tokenId];
                GameObject tokenInstance = Instantiate(tokenPrefab, goSpace.position + new Vector3(0, _tokenHoverHeight, 0), goSpace.rotation);
                tokenInstance.name = $"Token_{player.playerName}";
                _tokenInstances.Add(player.playerId, tokenInstance);
            }
        }

        public async Task AnimateMoveTokenAsync(Guid playerId, int[] path, float speedMultiplier)
        {
            if (!_tokenInstances.TryGetValue(playerId, out var token))
            {
                Debug.LogError($"Could not find token for playerId {playerId}.");
                return;
            }

            float moveSpeed = _normalMoveSpeed * speedMultiplier;

            foreach (var spaceIndex in path)
            {
                if (spaceIndex < 0 || spaceIndex >= _spaceWaypoints.Length)
                {
                    Debug.LogError($"Invalid space index {spaceIndex} in movement path.");
                    continue;
                }

                Vector3 targetPosition = _spaceWaypoints[spaceIndex].position + new Vector3(0, _tokenHoverHeight, 0);
                
                // TODO: Add sound effect for moving over a space.

                while (Vector3.Distance(token.transform.position, targetPosition) > 0.01f)
                {
                    token.transform.position = Vector3.MoveTowards(token.transform.position, targetPosition, moveSpeed * Time.deltaTime);
                    await Task.Yield(); // Wait for next frame
                }
                token.transform.position = targetPosition; // Snap to final position
            }

            // TODO: Add sound effect for landing on final space.
        }

        public void SetTokenPosition(Guid playerId, int spaceIndex)
        {
            if (!_tokenInstances.TryGetValue(playerId, out var token)) return;
            if (spaceIndex < 0 || spaceIndex >= _spaceWaypoints.Length) return;

            token.transform.position = _spaceWaypoints[spaceIndex].position + new Vector3(0, _tokenHoverHeight, 0);
        }

        public void UpdateOwnershipMarker(int propertyId, Color ownerColor, bool isMortgaged)
        {
            if (propertyId < 0 || propertyId >= _spaceWaypoints.Length) return;

            if (!_ownershipMarkers.TryGetValue(propertyId, out var marker))
            {
                // Instantiate a new marker if one doesn't exist for this property.
                marker = Instantiate(_ownershipMarkerPrefab, _spaceWaypoints[propertyId]);
                _ownershipMarkers[propertyId] = marker;
            }
            
            var renderer = marker.GetComponentInChildren<Renderer>();
            if(renderer != null)
            {
                // Adjust color and transparency for mortgaged state.
                Color finalColor = ownerColor;
                if (isMortgaged)
                {
                    finalColor.a = 0.5f; // Make it semi-transparent
                }
                renderer.material.color = finalColor;
            }
            marker.SetActive(true);
        }

        public void RemoveOwnershipMarker(int propertyId)
        {
            if (_ownershipMarkers.TryGetValue(propertyId, out var marker))
            {
                marker.SetActive(false); // Or Destroy(marker) and remove from dictionary
            }
        }

        public void UpdateBuildingVisuals(int propertyId, int houseCount, bool hasHotel)
        {
            if (propertyId < 0 || propertyId >= _spaceWaypoints.Length) return;

            // Clear existing buildings for this property
            if (_buildingInstances.TryGetValue(propertyId, out var existingBuildings))
            {
                foreach (var building in existingBuildings)
                {
                    Destroy(building);
                }
                existingBuildings.Clear();
            }
            else
            {
                _buildingInstances[propertyId] = new List<GameObject>();
            }

            // Get the anchor point for placing buildings on this property space.
            // This assumes a child object named "BuildingAnchor" exists on the space waypoint prefab.
            Transform buildingAnchor = _spaceWaypoints[propertyId].Find("BuildingAnchor");
            if (buildingAnchor == null)
            {
                Debug.LogWarning($"Property space {propertyId} is missing a 'BuildingAnchor' child object. Using waypoint transform as fallback.");
                buildingAnchor = _spaceWaypoints[propertyId];
            }

            // Add new buildings
            if (hasHotel)
            {
                GameObject hotel = Instantiate(_hotelPrefab, buildingAnchor);
                _buildingInstances[propertyId].Add(hotel);
            }
            else
            {
                // Simple horizontal placement logic for houses. A more complex system might be needed for curved properties.
                float houseWidth = 0.2f; // Assume a width for spacing.
                float startOffset = -(houseCount - 1) * houseWidth / 2.0f;

                for (int i = 0; i < houseCount; i++)
                {
                    Vector3 offset = new Vector3(startOffset + i * houseWidth, 0, 0);
                    GameObject house = Instantiate(_housePrefab, buildingAnchor);
                    house.transform.localPosition = offset;
                    _buildingInstances[propertyId].Add(house);
                }
            }
        }
    }
}