using MonopolyTycoon.Application.Contracts.ViewModels;
using MonopolyTycoon.Presentation.Features.CommonUI.Presenters;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace MonopolyTycoon.Presentation.Features.CommonUI.Views
{
    public class PropertyManagementView : MonoBehaviour, IPropertyManagementView
    {
        [Header("UI References")]
        [SerializeField] private Transform _propertyCardContainer;
        [SerializeField] private GameObject _propertyCardPrefab;
        [SerializeField] private Button _closeButton;

        public event Action OnCloseRequested;
        public event Action<string> OnBuildHouseRequested;
        public event Action<string> OnSellHouseRequested;
        public event Action<string> OnMortgageRequested;
        public event Action<string> OnUnmortgageRequested;
        public event Action<string> OnInitiateTradeRequested;
        
        private List<PropertyCardView> _propertyCards = new();

        [Inject]
        private void Construct(PropertyManagementPresenter presenter)
        {
            presenter.SetView(this);
        }

        private void Awake()
        {
            _closeButton.onClick.AddListener(() => OnCloseRequested?.Invoke());
        }

        public void DisplayProperties(IEnumerable<PlayerPropertyViewModel> properties)
        {
            ClearProperties();
            
            foreach (var propertyData in properties)
            {
                var cardInstance = Instantiate(_propertyCardPrefab, _propertyCardContainer);
                var cardView = cardInstance.GetComponent<PropertyCardView>();
                if (cardView != null)
                {
                    cardView.Initialize(propertyData);
                    cardView.OnBuildHouseClicked += () => OnBuildHouseRequested?.Invoke(propertyData.PropertyId);
                    cardView.OnSellHouseClicked += () => OnSellHouseRequested?.Invoke(propertyData.PropertyId);
                    cardView.OnMortgageClicked += () => OnMortgageRequested?.Invoke(propertyData.PropertyId);
                    cardView.OnUnmortgageClicked += () => OnUnmortgageRequested?.Invoke(propertyData.PropertyId);
                    cardView.OnTradeClicked += () => OnInitiateTradeRequested?.Invoke(propertyData.PropertyId);
                    _propertyCards.Add(cardView);
                }
            }
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void ClearProperties()
        {
            foreach (var card in _propertyCards)
            {
                card.OnBuildHouseClicked = null;
                card.OnSellHouseClicked = null;
                card.OnMortgageClicked = null;
                card.OnUnmortgageClicked = null;
                card.OnTradeClicked = null;
            }
            _propertyCards.Clear();

            foreach (Transform child in _propertyCardContainer)
            {
                Destroy(child.gameObject);
            }
        }
        
        private void OnDestroy()
        {
            _closeButton.onClick.RemoveAllListeners();
        }
    }
}