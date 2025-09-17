using System;
using System.Linq;
using MonopolyTycoon.Application.Abstractions;
using MonopolyTycoon.Domain.Objects;
using MonopolyTycoon.Presentation.Core;
using MonopolyTycoon.Presentation.Events;
using MonopolyTycoon.Presentation.Features.CommonUI.Views;
using VContainer;
using System.Threading.Tasks;

namespace MonopolyTycoon.Presentation.Features.CommonUI.Presenters
{
    public class PropertyManagementPresenter : IDisposable
    {
        private readonly IPropertyManagementView view;
        private readonly IEventBus eventBus;
        private readonly IViewManager viewManager;
        private readonly ITurnManagementService turnManagementService;

        private GameState currentGameState;
        private PlayerState humanPlayerState;

        [Inject]
        public PropertyManagementPresenter(
            IPropertyManagementView view,
            IEventBus eventBus,
            IViewManager viewManager,
            ITurnManagementService turnManagementService)
        {
            this.view = view;
            this.eventBus = eventBus;
            this.viewManager = viewManager;
            this.turnManagementService = turnManagementService;
        }

        public void Initialize(GameState initialState)
        {
            eventBus.Subscribe<GameStateUpdatedEvent>(OnGameStateUpdated);
            view.OnBuildHouseClicked += HandleBuildHouseClicked;
            view.OnSellHouseClicked += HandleSellHouseClicked;
            view.OnMortgageClicked += HandleMortgageClicked;
            view.OnUnmortgageClicked += HandleUnmortgageClicked;
            view.OnCloseClicked += HandleCloseClicked;

            UpdateView(initialState);
        }

        private void OnGameStateUpdated(GameStateUpdatedEvent e)
        {
            // Refresh the view if the game state changes while it's open
            UpdateView(e.NewGameState);
        }

        private void UpdateView(GameState gameState)
        {
            currentGameState = gameState;
            humanPlayerState = gameState.PlayerStates.First(p => p.IsHuman);

            view.SetPlayerCash(humanPlayerState.Cash);
            view.ClearProperties();

            var ownedProperties = humanPlayerState.PropertiesOwned
                .Select(id => currentGameState.BoardState.Properties[id])
                .OrderBy(p => p.ColorGroup)
                .ThenBy(p => p.BoardIndex);

            foreach (var property in ownedProperties)
            {
                // Logic to determine if actions are possible for this property
                // This is a complex set of rules derived from requirements
                bool canBuild = CanBuildOn(property);
                bool canSell = CanSellFrom(property);
                bool canMortgage = CanMortgage(property);
                bool canUnmortgage = CanUnmortgage(property);

                view.AddProperty(property.Id, property.Name, property.Houses, property.IsMortgaged, canBuild, canSell, canMortgage, canUnmortgage);
            }
        }

        // Fulfills REQ-1-053, REQ-1-054, US-033, US-034
        private bool CanBuildOn(Property property)
        {
            if (property.IsMortgaged || property.Houses >= 5) return false;

            var monopolyProperties = currentGameState.BoardState.GetPropertiesInGroup(property.ColorGroup);
            bool ownsMonopoly = monopolyProperties.All(p => humanPlayerState.PropertiesOwned.Contains(p.Id));
            if (!ownsMonopoly) return false;

            bool anyMortgagedInGroup = monopolyProperties.Any(p => p.IsMortgaged);
            if (anyMortgagedInGroup) return false;
            
            // Even building rule
            int minHouses = monopolyProperties.Min(p => p.Houses);
            if (property.Houses > minHouses) return false;

            // Bank has houses/hotels
            if (property.Houses == 4 && currentGameState.BankState.Hotels < 1) return false;
            if (property.Houses < 4 && currentGameState.BankState.Houses < 1) return false;

            return humanPlayerState.Cash >= property.HouseCost;
        }

        private bool CanSellFrom(Property property)
        {
            if (property.Houses == 0) return false;
            
            var monopolyProperties = currentGameState.BoardState.GetPropertiesInGroup(property.ColorGroup);
            int maxHouses = monopolyProperties.Max(p => p.Houses);

            // Even selling rule (must sell from most developed first)
            return property.Houses == maxHouses;
        }
        
        // Fulfills REQ-1-057, US-038
        private bool CanMortgage(Property property)
        {
            if (property.IsMortgaged) return false;
            
            // Cannot mortgage if any property in its color group has buildings
            var monopolyProperties = currentGameState.BoardState.GetPropertiesInGroup(property.ColorGroup);
            return !monopolyProperties.Any(p => p.Houses > 0);
        }
        
        // Fulfills REQ-1-058, US-039
        private bool CanUnmortgage(Property property)
        {
            if (!property.IsMortgaged) return false;

            decimal unmortgageCost = property.MortgageValue * 1.1m;
            return humanPlayerState.Cash >= unmortgageCost;
        }

        private async Task ExecutePlayerAction(PlayerAction action)
        {
            try
            {
                await turnManagementService.ExecutePlayerActionAsync(action);
                // The view will be updated automatically by the GameStateUpdatedEvent
            }
            catch (Exception e)
            {
                Debug.LogError($"Action failed: {e.Message}");
                // Show an error to the user
                await viewManager.ShowErrorDialogAsync("Action Failed", "The requested action could not be completed.");
            }
        }

        private async void HandleBuildHouseClicked(string propertyId) => await ExecutePlayerAction(PlayerAction.BuildHouse(propertyId));
        private async void HandleSellHouseClicked(string propertyId) => await ExecutePlayerAction(PlayerAction.SellHouse(propertyId));
        private async void HandleMortgageClicked(string propertyId) => await ExecutePlayerAction(PlayerAction.MortgageProperty(propertyId));
        private async void HandleUnmortgageClicked(string propertyId) => await ExecutePlayerAction(PlayerAction.UnmortgageProperty(propertyId));
        private void HandleCloseClicked() => viewManager.CloseView(this);
        
        public void Dispose()
        {
            eventBus.Unsubscribe<GameStateUpdatedEvent>(OnGameStateUpdated);
            if (view != null)
            {
                view.OnBuildHouseClicked -= HandleBuildHouseClicked;
                view.OnSellHouseClicked -= HandleSellHouseClicked;
                view.OnMortgageClicked -= HandleMortgageClicked;
                view.OnUnmortgageClicked -= HandleUnmortgageClicked;
                view.OnCloseClicked -= HandleCloseClicked;
            }
        }
    }
}