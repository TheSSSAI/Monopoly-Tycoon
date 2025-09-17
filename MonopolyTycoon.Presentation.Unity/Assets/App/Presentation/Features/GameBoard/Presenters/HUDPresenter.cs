using MonopolyTycoon.Application.Abstractions;
using MonopolyTycoon.Presentation.Core;
using MonopolyTycoon.Presentation.Events;
using MonopolyTycoon.Presentation.Features.GameBoard.Views;
using System;
using System.Linq;
using VContainer;
using MonopolyTycoon.Domain.Objects;

namespace MonopolyTycoon.Presentation.Features.GameBoard.Presenters
{
    public class HUDPresenter : IDisposable
    {
        private readonly IHUDView view;
        private readonly IEventBus eventBus;
        private readonly IViewManager viewManager;
        private readonly ITurnManagementService turnManagementService;

        private GameState currentGameState;

        [Inject]
        public HUDPresenter(IHUDView view, IEventBus eventBus, IViewManager viewManager, ITurnManagementService turnManagementService)
        {
            this.view = view;
            this.eventBus = eventBus;
            this.viewManager = viewManager;
            this.turnManagementService = turnManagementService;
        }

        public void Initialize(GameState initialGameState)
        {
            eventBus.Subscribe<GameStateUpdatedEvent>(OnGameStateUpdated);
            view.OnManagePropertiesClicked += HandleManagePropertiesClicked;
            view.OnRollDiceClicked += HandleRollDiceClicked;
            
            UpdateHUD(initialGameState);
        }

        private void OnGameStateUpdated(GameStateUpdatedEvent e)
        {
            UpdateHUD(e.NewGameState);
        }

        private void UpdateHUD(GameState gameState)
        {
            currentGameState = gameState;
            if (currentGameState == null) return;
            
            // Fulfills REQ-1-071: Display essential information for every player
            var playerStates = currentGameState.PlayerStates.OrderBy(p => p.PlayerId).ToList();
            view.SetupPlayerPanels(playerStates.Count);
            
            for (int i = 0; i < playerStates.Count; i++)
            {
                var player = playerStates[i];
                view.UpdatePlayerPanel(
                    i,
                    player.PlayerName,
                    $"${player.Cash:N0}",
                    player.TokenId, // Assuming this is a key for a sprite asset
                    player.Status == PlayerStatus.Bankrupt
                );
            }

            // Fulfills REQ-1-071: Clear visual indicator for the current player's turn
            int activePlayerIndex = playerStates.FindIndex(p => p.PlayerId == currentGameState.ActivePlayerId);
            view.SetActivePlayerHighlight(activePlayerIndex);

            // Handle button states based on game phase
            var activePlayer = playerStates[activePlayerIndex];
            bool isHumanTurn = activePlayer.IsHuman;
            // This logic depends on a GamePhase property in GameState
            // Assuming such a property exists based on REQ-1-038
            bool canManageProperties = isHumanTurn && currentGameState.CurrentPhase == GamePhase.PreRollManagement;
            bool canRollDice = isHumanTurn && currentGameState.CurrentPhase == GamePhase.PreRollManagement;

            view.SetManagePropertiesButtonInteractable(canManageProperties);
            view.SetRollDiceButtonInteractable(canRollDice);
        }

        private async void HandleManagePropertiesClicked()
        {
            // Fulfills US-052
            await viewManager.ShowViewAsync<PropertyManagementPresenter>("PropertyManagementView", currentGameState);
        }

        private async void HandleRollDiceClicked()
        {
            // Fulfills US-015
            view.SetRollDiceButtonInteractable(false); // Prevent double clicks
            var action = new PlayerAction(PlayerActionType.RollDice);
            await turnManagementService.ExecutePlayerActionAsync(action);
            // The HUD will be updated via the resulting GameStateUpdatedEvent
        }

        public void Dispose()
        {
            eventBus.Unsubscribe<GameStateUpdatedEvent>(OnGameStateUpdated);
            if (view != null)
            {
                view.OnManagePropertiesClicked -= HandleManagePropertiesClicked;
                view.OnRollDiceClicked -= HandleRollDiceClicked;
            }
        }
    }
}