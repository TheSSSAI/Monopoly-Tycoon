using MonopolyTycoon.Presentation.Features.PropertyManagement.Views;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonopolyTycoon.Presentation.Features.PropertyManagement
{
    public class PropertyManagementView : MonoBehaviour, IPropertyManagementView
    {
        [Header("UI References")]
        [SerializeField]
        private TextMeshProUGUI playerCashText;

        [SerializeField]
        private GameObject propertyCardPrefab; // Prefab for displaying a single property

        [SerializeField]
        private Transform propertyListContainer;

        [SerializeField]
        private Button closeButton;

        public event System.Action<string> OnBuildHouseRequested;
        public event System.Action<string> OnSellHouseRequested;
        public event System.Action<string> OnMortgageRequested;
        public event System.Action<string> OnUnmortgageRequested;
        public event System.Action OnCloseRequested;
        
        private void Awake()
        {
            closeButton.onClick.AddListener(() => OnCloseRequested?.Invoke());
        }
        
        private void OnDestroy()
        {
            closeButton.onClick.RemoveAllListeners();
        }

        public void DisplayAssets(PlayerAssetViewModel viewModel)
        {
            playerCashText.text = viewModel.PlayerCash;

            // Simple clear and rebuild - pooling would be better for performance
            foreach (Transform child in propertyListContainer)
            {
                Destroy(child.gameObject);
            }

            if (viewModel.Properties.Count == 0)
            {
                // Display "No properties owned" message
                // This would be another UI element to enable.
            }
            else
            {
                foreach (var propertyVm in viewModel.Properties)
                {
                    var cardInstance = Instantiate(propertyCardPrefab, propertyListContainer);
                    var cardView = cardInstance.GetComponent<PropertyCardView>();
                    if (cardView != null)
                    {
                        cardView.Populate(propertyVm);
                        
                        // Wire up events from the card view to this view's main events
                        cardView.OnBuildHouseClicked += () => OnBuildHouseRequested?.Invoke(propertyVm.PropertyId);
                        cardView.OnSellHouseClicked += () => OnSellHouseRequested?.Invoke(propertyVm.PropertyId);
                        cardView.OnMortgageClicked += () => OnMortgageRequested?.Invoke(propertyVm.PropertyId);
                        cardView.OnUnmortgageClicked += () => OnUnmortgageRequested?.Invoke(propertyVm.PropertyId);
                    }
                }
            }
        }
        
        public void ShowError(string message)
        {
            // In a real implementation, this would trigger a non-intrusive toast notification
            Debug.Log($"[PropertyManagementView] Error: {message}");
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        // A nested or separate component for the property card UI element
        // This is a simplified example.
        public class PropertyCardView : MonoBehaviour
        {
            // References to UI elements on the prefab
            public TextMeshProUGUI PropertyNameText;
            public Image PropertyColorStripe;
            public Button BuildHouseButton;
            public Button SellHouseButton;
            public Button MortgageButton;
            public Button UnmortgageButton;
            public TextMeshProUGUI HouseCountText;
            public Image MortgagedOverlay;
            
            public event System.Action OnBuildHouseClicked;
            public event System.Action OnSellHouseClicked;
            public event System.Action OnMortgageClicked;
            public event System.Action OnUnmortgageClicked;
            
            private void Awake()
            {
                BuildHouseButton.onClick.AddListener(() => OnBuildHouseClicked?.Invoke());
                SellHouseButton.onClick.AddListener(() => OnSellHouseClicked?.Invoke());
                MortgageButton.onClick.AddListener(() => OnMortgageClicked?.Invoke());
                UnmortgageButton.onClick.AddListener(() => OnUnmortgageClicked?.Invoke());
            }

            public void Populate(PropertyViewModel vm)
            {
                PropertyNameText.text = vm.PropertyName;
                PropertyColorStripe.color = vm.ColorGroup;
                HouseCountText.text = $"Houses: {vm.HouseCount}";
                MortgagedOverlay.enabled = vm.IsMortgaged;
                
                BuildHouseButton.interactable = vm.CanBuild;
                SellHouseButton.interactable = vm.CanSell;
                MortgageButton.interactable = vm.CanMortgage;
                UnmortgageButton.interactable = vm.CanUnmortgage;
            }
        }
    }
}