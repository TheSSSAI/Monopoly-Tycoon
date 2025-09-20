using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonopolyTycoon.Presentation.Features.HUD.ViewModels;
using MonopolyTycoon.Presentation.Features.HUD.Views;

namespace MonopolyTycoon.Presentation.Features.HUD.Views
{
    public class HUDView : MonoBehaviour, IHUDView
    {
        [Header("Player Panel References")]
        [SerializeField] private GameObject _playerPanelPrefab;
        [SerializeField] private Transform _playerPanelContainer;

        [Header("Action Button References")]
        [SerializeField] private Button _managePropertiesButton;
        [SerializeField] private Button _proposeTradeButton;
        [SerializeField] private Button _rollDiceButton;

        private readonly Dictionary<string, PlayerHUDPanelView> _playerPanels = new();

        public event Action OnManagePropertiesClicked;
        public event Action OnProposeTradeClicked;
        public event Action OnRollDiceClicked;
        
        private void Awake()
        {
            _managePropertiesButton.onClick.AddListener(() => OnManagePropertiesClicked?.Invoke());
            _proposeTradeButton.onClick.AddListener(() => OnProposeTradeClicked?.Invoke());
            _rollDiceButton.onClick.AddListener(() => OnRollDiceClicked?.Invoke());
        }

        public void Initialize(List<PlayerHUDViewModel> initialPlayerData)
        {
            // Clear any existing panels
            foreach (Transform child in _playerPanelContainer)
            {
                Destroy(child.gameObject);
            }
            _playerPanels.Clear();

            // Create and populate panels for each player
            foreach (var playerData in initialPlayerData)
            {
                var panelInstance = Instantiate(_playerPanelPrefab, _playerPanelContainer);
                var panelView = panelInstance.GetComponent<PlayerHUDPanelView>();
                if (panelView != null)
                {
                    panelView.UpdateDisplay(playerData);
                    _playerPanels[playerData.PlayerId] = panelView;
                }
                else
                {
                    Debug.LogError("[HUDView] Player Panel Prefab is missing the PlayerHUDPanelView component.");
                }
            }
        }
        
        public void UpdateDisplay(HUDViewModel viewModel)
        {
            if (viewModel == null) return;
            
            foreach (var playerData in viewModel.Players)
            {
                if (_playerPanels.TryGetValue(playerData.PlayerId, out var panelView))
                {
                    panelView.UpdateDisplay(playerData);
                }
            }
            
            SetTurnHighlight(viewModel.ActivePlayerId);
        }
        
        public void SetActionButtonState(bool canManage, bool canTrade, bool canRoll)
        {
            _managePropertiesButton.interactable = canManage;
            _proposeTradeButton.interactable = canTrade;
            _rollDiceButton.interactable = canRoll;
        }

        private void SetTurnHighlight(string activePlayerId)
        {
            foreach (var kvp in _playerPanels)
            {
                bool isActive = kvp.Key == activePlayerId;
                kvp.Value.SetHighlight(isActive);
            }
        }

        private void OnDestroy()
        {
            _managePropertiesButton.onClick.RemoveAllListeners();
            _proposeTradeButton.onClick.RemoveAllListeners();
            _rollDiceButton.onClick.RemoveAllListeners();
        }
    }
}