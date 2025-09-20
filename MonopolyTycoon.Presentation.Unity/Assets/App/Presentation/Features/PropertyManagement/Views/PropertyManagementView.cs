using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MonopolyTycoon.Presentation.Features.PropertyManagement.ViewModels;
using MonopolyTycoon.Presentation.Features.PropertyManagement.Views;

namespace MonopolyTycoon.Presentation.Features.PropertyManagement.Views
{
    public class PropertyManagementView : MonoBehaviour, IPropertyManagementView
    {
        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI _playerCashText;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Transform _propertyCardContainer;
        [SerializeField] private GameObject _propertyCardPrefab;
        [SerializeField] private TextMeshProUGUI _noPropertiesText;

        private List<PropertyCardView> _propertyCards = new();

        public event Action<string> OnBuildHouseRequested;
        public event Action<string> OnSellHouseRequested;
        public event Action<string> OnMortgageRequested;
        public event Action<string> OnUnmortgageRequested;
        public event Action OnClose;
        
        private void Awake()
        {
            _closeButton.onClick.AddListener(() => OnClose?.Invoke());
        }

        public void DisplayAssets(PlayerAssetViewModel viewModel)
        {
            if (viewModel == null) return;
            
            _playerCashText.text = viewModel.PlayerCashFormatted;

            bool hasProperties = viewModel.PropertiesByGroup.Count > 0;
            _noPropertiesText.gameObject.SetActive(!hasProperties);
            _propertyCardContainer.gameObject.SetActive(hasProperties);

            // This is a simple pooling mechanism. A more robust solution might be better.
            // Ensure we have enough card views, creating more if needed.
            int requiredCards = 0;
            foreach (var group in viewModel.PropertiesByGroup.Values) requiredCards += group.Count;

            while (_propertyCards.Count < requiredCards)
            {
                var cardInstance = Instantiate(_propertyCardPrefab, _propertyCardContainer);
                var cardView = cardInstance.GetComponent<PropertyCardView>();
                _propertyCards.Add(cardView);
            }

            // Hide unused cards
            for (int i = requiredCards; i < _propertyCards.Count; i++)
            {
                _propertyCards[i].gameObject.SetActive(false);
            }
            
            // Populate and show used cards
            int cardIndex = 0;
            foreach (var group in viewModel.PropertiesByGroup)
            {
                foreach (var property in group.Value)
                {
                    var cardView = _propertyCards[cardIndex];
                    cardView.gameObject.SetActive(true);
                    cardView.Populate(property);
                    
                    // Wire up events from the card view to the main view's events
                    // This avoids the Presenter needing to know about individual cards
                    cardView.OnBuildHouse.RemoveAllListeners();
                    cardView.OnBuildHouse.AddListener(() => OnBuildHouseRequested?.Invoke(property.PropertyId));
                    // ... wire up other events similarly for Sell, Mortgage, Unmortgage
                    
                    cardIndex++;
                }
            }
        }

        public void ShowError(string message)
        {
            // In a real project, this would trigger a non-intrusive toast/notification
            Debug.LogWarning($"[PropertyManagementView] Error: {message}");
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveAllListeners();
        }
    }
}