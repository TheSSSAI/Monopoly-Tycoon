using MonopolyTycoon.Application.Abstractions;
using MonopolyTycoon.Presentation.Events;
using MonopolyTycoon.Presentation.Features.HUD.Views;
using System;
using System.Linq;
using VContainer.Unity;

namespace MonopolyTycoon.Presentation.Features.HUD.Presenters
{
    public class HUDPresenter : IInitializable, IDisposable
    {
        private readonly IHUDView _view;
        private readonly IEventBus _eventBus;
        private readonly IGameSessionService _gameSessionService;
        private readonly IViewManager _viewManager;
        private readonly ITurnManagementService _turnManagementService;

        public HUDPresenter(IHUDView view, IEventBus eventBus, IGameSessionService gameSessionService, IViewManager viewManager, ITurnManagementService turnManagementService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _gameSessionService = gameSessionService ?? throw new ArgumentNullException(nameof(gameSessionService));
            _viewManager = viewManager ?? throw new ArgumentNullException(nameof(viewManager));
            _turnManagementService = turnManagementService ?? throw new ArgumentNullException(nameof(turnManagementService));
        }

        public void Initialize()
        {
            _eventBus.Subscribe<GameStateUpdatedEvent>(OnGameStateUpdated);
            _view.OnManagePropertiesClicked += OnManagePropertiesClicked;
            _view.OnRollDiceClicked += OnRollDiceClicked;

            var initialGameState = _gameSessionService.GetCurrentGameState();
            if (initialGameState != null)
            {
                // REQ-1-071: The HUD must display essential information for every player...
                _view.SetupPlayerPanels(initialGameState.Players.Select(p => new PlayerHUDModel
                {
                    PlayerId = p.Id,
                    PlayerName = p.Name,
                    TokenId = p.TokenId,
                    Cash = p.Cash,
                    IsHuman = p.IsHuman
                }).ToList());
                
                UpdateHUD(initialGameState);
            }
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<GameStateUpdatedEvent>(OnGameStateUpdated);
            _view.OnManagePropertiesClicked -= OnManagePropertiesClicked;
            _view.OnRollDiceClicked -= OnRollDiceClicked;
        }
        
        private void OnGameStateUpdated(GameStateUpdatedEvent e)
        {
            UpdateHUD(e.NewState);
        }

        private void UpdateHUD(Application.DataObjects.GameStateDTO state)
        {
            foreach (var player in state.Players)
            {
                _view.UpdatePlayerCash(player.Id, player.Cash);
                _view.UpdatePlayerStatus(player.Id, player.Status.ToString());
            }

            // REQ-1-071: It must also feature a clear visual indicator to show which player's turn it is.
            _view.SetActivePlayer(state.ActivePlayerId);

            // REQ-1-029 & REQ-1-085: Enable/disable controls based on turn phase.
            var activePlayer = state.Players.Find(p => p.Id == state.ActivePlayerId);
            bool isHumanTurn = activePlayer != null && activePlayer.IsHuman;
            bool isPreRollPhase = state.CurrentTurnPhase == Application.DataObjects.TurnPhase.PreRoll;

            _view.SetControlsEnabled(isHumanTurn && isPreRollPhase);
        }

        private void OnManagePropertiesClicked()
        {
            // US-052: Access a dedicated interface to manage all my properties
            _viewManager.ShowScreen(Screen.PropertyManagement);
        }

        private async void OnRollDiceClicked()
        {
            // US-015: Roll two six-sided dice to start my move
            _view.SetControlsEnabled(false); // Disable controls immediately after click
            await _turnManagementService.PlayerRollDiceAsync();
        }
    }
}