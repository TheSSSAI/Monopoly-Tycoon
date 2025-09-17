using MonopolyTycoon.Application.Contracts;
using MonopolyTycoon.Domain.Events;
using MonopolyTycoon.Presentation.Core;
using MonopolyTycoon.Presentation.Features.GameBoard.Views;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace MonopolyTycoon.Presentation.Features.GameBoard.Presenters
{
    public class GameBoardPresenter : MonoBehaviour
    {
        [Inject]
        private readonly IEventBus _eventBus;
        [Inject]
        private readonly ILoggerAdapter<GameBoardPresenter> _logger;

        private IGameBoardView _view;
        private GameState _currentGameState;

        private void Awake()
        {
            _view = GetComponent<IGameBoardView>();
            if (_view == null)
            {
                _logger.LogError("GameBoardPresenter requires an IGameBoardView component on the same GameObject.");
                enabled = false;
            }
        }

        private void Start()
        {
            _eventBus.Subscribe<GameStateUpdatedEvent>(OnGameStateUpdated);
            _logger.LogInformation("GameBoardPresenter initialized and subscribed to GameStateUpdatedEvent.");
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<GameStateUpdatedEvent>(OnGameStateUpdated);
            _logger.LogInformation("GameBoardPresenter destroyed and unsubscribed from events.");
        }

        private async void OnGameStateUpdated(GameStateUpdatedEvent e)
        {
            _logger.LogInformation("Received GameStateUpdatedEvent with context: {ChangeContext}", e.ChangeContext);
            var previousState = _currentGameState;
            _currentGameState = e.NewGameState;

            await ProcessVisualUpdates(previousState, _currentGameState);
        }

        private async Task ProcessVisualUpdates(GameState oldState, GameState newState)
        {
            if (oldState == null)
            {
                // This is the first state update, just render the initial state.
                _view.InitializeBoard(newState);
                return;
            }

            // In a full implementation, we would have a queue of animations.
            // For now, we'll process them sequentially with awaits.

            // 1. Process player movement
            for (int i = 0; i < newState.PlayerStates.Count; i++)
            {
                var oldPlayerState = oldState.PlayerStates.FirstOrDefault(p => p.PlayerId == newState.PlayerStates[i].PlayerId);
                if (oldPlayerState != null && oldPlayerState.CurrentPosition != newState.PlayerStates[i].CurrentPosition)
                {
                    _logger.LogInformation("Animating token move for Player {PlayerId} from {StartPosition} to {EndPosition}",
                        newState.PlayerStates[i].PlayerId, oldPlayerState.CurrentPosition, newState.PlayerStates[i].CurrentPosition);
                    await _view.AnimateTokenMoveAsync(
                        newState.PlayerStates[i].PlayerId, 
                        oldPlayerState.CurrentPosition, 
                        newState.PlayerStates[i].CurrentPosition);
                }
            }

            // 2. Process property ownership and development changes
            foreach (var newPropertyState in newState.BoardState.Properties)
            {
                var oldPropertyState = oldState.BoardState.Properties.FirstOrDefault(p => p.Id == newPropertyState.Id);
                if (oldPropertyState == null) continue;

                // Ownership change
                if (oldPropertyState.OwnerId != newPropertyState.OwnerId)
                {
                    _logger.LogInformation("Updating ownership for property {PropertyId} to Player {OwnerId}", 
                        newPropertyState.Id, newPropertyState.OwnerId);
                    _view.UpdateOwnershipVisual(newPropertyState.Id, newPropertyState.OwnerId, newPropertyState.IsMortgaged);
                }
                
                // Mortgage status change
                else if(oldPropertyState.IsMortgaged != newPropertyState.IsMortgaged)
                {
                    _logger.LogInformation("Updating mortgage status for property {PropertyId} to {IsMortgaged}",
                        newPropertyState.Id, newPropertyState.IsMortgaged);
                     _view.UpdateOwnershipVisual(newPropertyState.Id, newPropertyState.OwnerId, newPropertyState.IsMortgaged);
                }

                // Building level change
                if (oldPropertyState.BuildingLevel != newPropertyState.BuildingLevel)
                {
                    _logger.LogInformation("Updating building level for property {PropertyId} to {BuildingLevel}",
                        newPropertyState.Id, newPropertyState.BuildingLevel);
                    await _view.UpdateBuildingVisualsAsync(newPropertyState.Id, newPropertyState.BuildingLevel);
                }
            }
            
            // Further updates for dice rolls, etc., would be handled here.
            _logger.LogInformation("Visual updates for turn {TurnNumber} complete.", newState.GameMetadata.CurrentTurnNumber);
        }
    }
}