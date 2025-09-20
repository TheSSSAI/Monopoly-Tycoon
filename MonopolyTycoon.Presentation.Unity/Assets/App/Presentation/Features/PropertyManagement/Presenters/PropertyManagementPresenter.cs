using MonopolyTycoon.Application.Abstractions;
using MonopolyTycoon.Application.DataObjects;
using MonopolyTycoon.Presentation.Events;
using MonopolyTycoon.Presentation.Features.PropertyManagement.Views;
using System;
using System.Linq;
using System.Threading.Tasks;
using VContainer.Unity;

namespace MonopolyTycoon.Presentation.Features.PropertyManagement.Presenters
{
    public class PropertyManagementPresenter : IInitializable, IDisposable
    {
        private readonly IPropertyManagementView _view;
        private readonly IEventBus _eventBus;
        private readonly IGameSessionService _gameSessionService;
        private readonly IPropertyActionService _propertyActionService;
        private readonly IViewManager _viewManager;

        private string _humanPlayerId;

        public PropertyManagementPresenter(
            IPropertyManagementView view, 
            IEventBus eventBus, 
            IGameSessionService gameSessionService,
            IPropertyActionService propertyActionService,
            IViewManager viewManager)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _gameSessionService = gameSessionService ?? throw new ArgumentNullException(nameof(gameSessionService));
            _propertyActionService = propertyActionService ?? throw new ArgumentNullException(nameof(propertyActionService));
            _viewManager = viewManager ?? throw new ArgumentNullException(nameof(viewManager));
        }

        public void Initialize()
        {
            _eventBus.Subscribe<GameStateUpdatedEvent>(OnGameStateUpdated);
            _view.OnBuildHouseRequested += OnBuildHouseRequested;
            _view.OnSellHouseRequested += OnSellHouseRequested;
            _view.OnMortgageRequested += OnMortgageRequested;
            _view.OnUnmortgageRequested += OnUnmortgageRequested;
            _view.OnCloseViewClicked += OnCloseViewClicked;

            var initialGameState = _gameSessionService.GetCurrentGameState();
            if (initialGameState != null)
            {
                UpdateView(initialGameState);
            }
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<GameStateUpdatedEvent>(OnGameStateUpdated);
            _view.OnBuildHouseRequested -= OnBuildHouseRequested;
            _view.OnSellHouseRequested -= OnSellHouseRequested;
            _view.OnMortgageRequested -= OnMortgageRequested;
            _view.OnUnmortgageRequested -= OnUnmortgageRequested;
            _view.OnCloseViewClicked -= OnCloseViewClicked;
        }

        private void OnGameStateUpdated(GameStateUpdatedEvent e)
        {
            UpdateView(e.NewState);
        }

        private void UpdateView(GameStateDTO state)
        {
            var humanPlayer = state.Players.FirstOrDefault(p => p.IsHuman);
            if (humanPlayer == null) return;
            _humanPlayerId = humanPlayer.Id;
            
            var ownedProperties = state.Board.Properties.Where(p => p.OwnerId == _humanPlayerId).ToList();
            
            // US-052: Efficiently view the status of all my properties
            var viewModel = new PlayerAssetViewModel
            {
                CurrentCash = humanPlayer.Cash,
                Properties = ownedProperties.Select(p => new PropertyCardViewModel
                {
                    PropertyId = p.Id,
                    DisplayName = p.Name,
                    DevelopmentLevel = p.DevelopmentLevel,
                    IsMortgaged = p.IsMortgaged,
                    // The Application layer will determine if actions are valid.
                    // The presenter could also pre-calculate this for the view.
                }).ToList()
            };

            _view.DisplayAssets(viewModel);
        }

        private async Task HandleAction(Func<Task<ServiceResult>> action)
        {
            _view.SetLoading(true);
            try
            {
                var result = await action();
                if (!result.IsSuccess)
                {
                    // AC-006: A non-intrusive notification informs the player
                    _view.ShowError(result.ErrorMessage);
                }
                // Successful actions will trigger a GameStateUpdatedEvent, which will refresh the view.
            }
            catch (Exception ex)
            {
                // In a real app, log this
                _view.ShowError("An unexpected error occurred.");
            }
            finally
            {
                _view.SetLoading(false);
            }
        }

        private async void OnBuildHouseRequested(string propertyId)
        {
            // US-033: Build houses on properties
            // US-034: Enforce even building rule
            await HandleAction(() => _propertyActionService.BuildHouseAsync(propertyId));
        }

        private async void OnSellHouseRequested(string propertyId)
        {
            await HandleAction(() => _propertyActionService.SellHouseAsync(propertyId));
        }

        private async void OnMortgageRequested(string propertyId)
        {
            // US-038: Mortgage an undeveloped property
            await HandleAction(() => _propertyActionService.MortgagePropertyAsync(propertyId));
        }

        private async void OnUnmortgageRequested(string propertyId)
        {
            // US-039: Unmortgage a property
            await HandleAction(() => _propertyActionService.UnmortgagePropertyAsync(propertyId));
        }

        private void OnCloseViewClicked()
        {
            _viewManager.HideScreen(Screen.PropertyManagement);
        }
    }
}