using MonopolyTycoon.Application.Abstractions;
using MonopolyTycoon.Application.DataObjects;
using MonopolyTycoon.Presentation.Shared.Events;
using MonopolyTycoon.Presentation.Shared.Services;
using System;
using UniRx;
using VContainer.Unity;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace MonopolyTycoon.Presentation.Features.PropertyManagement.Presenters
{
    public class PropertyManagementPresenter : IInitializable, IDisposable
    {
        private readonly IPropertyManagementView _view;
        private readonly IEventBus _eventBus;
        private readonly IPropertyActionService _propertyActionService;
        private readonly IGameSessionService _gameSessionService;
        private readonly IViewManager _viewManager;
        private readonly CompositeDisposable _disposables = new();

        public PropertyManagementPresenter(IPropertyManagementView view, IEventBus eventBus, IPropertyActionService propertyActionService, IGameSessionService gameSessionService, IViewManager viewManager)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _propertyActionService = propertyActionService ?? throw new ArgumentNullException(nameof(propertyActionService));
            _gameSessionService = gameSessionService ?? throw new ArgumentNullException(nameof(gameSessionService));
            _viewManager = viewManager ?? throw new ArgumentNullException(nameof(viewManager));
        }

        public void Initialize()
        {
            _view.OnBuildHouseRequested.Subscribe(propId => HandleAction(() => _propertyActionService.BuildHouseAsync(propId))).AddTo(_disposables);
            _view.OnSellHouseRequested.Subscribe(propId => HandleAction(() => _propertyActionService.SellHouseAsync(propId))).AddTo(_disposables);
            _view.OnMortgagePropertyRequested.Subscribe(propId => HandleAction(() => _propertyActionService.MortgagePropertyAsync(propId))).AddTo(_disposables);
            _view.OnUnmortgagePropertyRequested.Subscribe(propId => HandleAction(() => _propertyActionService.UnmortgagePropertyAsync(propId))).AddTo(_disposables);
            _view.OnCloseView.Subscribe(_ => _viewManager.HideScreenAsync(Screen.PropertyManagement).Forget()).AddTo(_disposables);

            _eventBus.On<GameStateUpdatedEvent>()
                .Subscribe(e => RefreshView(e.NewState))
                .AddTo(_disposables);

            RefreshView(_gameSessionService.GetCurrentGameState());
        }

        private void RefreshView(GameStateDTO gameState)
        {
            if (gameState == null) return;

            var humanPlayer = gameState.Players.FirstOrDefault(p => p.IsHuman);
            if (humanPlayer == null) return;

            var viewModel = new PlayerAssetViewModel
            {
                PlayerCash = humanPlayer.Cash,
                Properties = gameState.BoardState.Properties
                    .Where(p => p.OwnerId == humanPlayer.PlayerId)
                    .Select(p => new PropertyViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        ColorGroup = p.ColorGroup,
                        Houses = p.Houses,
                        HasHotel = p.HasHotel,
                        IsMortgaged = p.IsMortgaged,
                        // Presentation logic to determine if actions are possible
                        CanBuild = CanBuild(p, humanPlayer, gameState),
                        CanSell = p.Houses > 0 || p.HasHotel,
                        CanMortgage = !p.IsMortgaged && p.Houses == 0 && !p.HasHotel,
                        CanUnmortgage = p.IsMortgaged && humanPlayer.Cash >= (p.MortgageValue * 1.1m)
                    }).ToList()
            };

            _view.DisplayAssets(viewModel);
        }

        private bool CanBuild(PropertyDTO property, PlayerDTO player, GameStateDTO state)
        {
            // REQ-1-053: Must own the whole color group.
            var colorGroupProperties = state.BoardState.Properties.Where(p => p.ColorGroup == property.ColorGroup && p.ColorGroup != "Utility" && p.ColorGroup != "Railroad").ToList();
            if (colorGroupProperties.Any(p => p.OwnerId != player.PlayerId)) return false;
            
            // Cannot build on mortgaged properties in the set
            if (colorGroupProperties.Any(p => p.IsMortgaged)) return false;

            // REQ-1-054: Even building rule
            int minHouses = colorGroupProperties.Min(p => p.Houses);
            if (property.Houses > minHouses) return false;

            // Cannot build past a hotel
            if (property.HasHotel) return false;

            // REQ-1-055: Building shortage
            if (property.Houses == 4) // Trying to build a hotel
            {
                if (state.BankState.HotelsAvailable <= 0) return false;
            }
            else // Trying to build a house
            {
                if (state.BankState.HousesAvailable <= 0) return false;
            }

            // Check funds
            return player.Cash >= property.HouseCost;
        }

        private async void HandleAction(Func<UniTask<ApplicationResult>> action)
        {
            _view.SetActionsEnabled(false);
            try
            {
                var result = await action();
                if (!result.IsSuccess)
                {
                    _view.ShowError(result.ErrorMessage);
                }
                // On success, the GameStateUpdatedEvent will refresh the view.
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"An error occurred in PropertyManagementPresenter: {ex.Message}");
                _view.ShowError("An unexpected error occurred.");
            }
            finally
            {
                // Re-enable actions after a brief delay to prevent spamming
                await UniTask.Delay(250);
                if(_view != null) _view.SetActionsEnabled(true);
            }
        }
        
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}