using Microsoft.Extensions.Logging;
using MonopolyTycoon.Application.Abstractions.Services;
using MonopolyTycoon.Application.Services.Exceptions;
using MonopolyTycoon.Domain.Abstractions.Services;
using MonopolyTycoon.Domain.DomainEvents;
using MonopolyTycoon.Domain.Entities;
using MonopolyTycoon.Domain.Enums;
using MonopolyTycoon.Domain.Objects;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MonopolyTycoon.Application.Services.Services
{
    /// <summary>
    /// Manages the progression of a player's turn through distinct phases and orchestrates action execution.
    /// This service acts as the state machine for the game's turn-based flow.
    /// </summary>
    public class TurnManagementService : ITurnManagementService
    {
        private readonly IGameSessionService _gameSessionService;
        private readonly IRuleEngine _ruleEngine;
        private readonly IAIService _aiService;
        private readonly IEventBus _eventBus;
        private readonly ILogger<TurnManagementService> _logger;

        public TurnManagementService(
            IGameSessionService gameSessionService,
            IRuleEngine ruleEngine,
            IAIService aiService,
            IEventBus eventBus,
            ILogger<TurnManagementService> logger)
        {
            _gameSessionService = gameSessionService;
            _ruleEngine = ruleEngine;
            _aiService = aiService;
            _eventBus = eventBus;
            _logger = logger;
        }

        public async Task ExecutePlayerActionAsync(PlayerAction action, CancellationToken cancellationToken = default)
        {
            var gameState = _gameSessionService.GetCurrentGameState() 
                ?? throw new InvalidOperationException("Cannot execute action: No active game session found.");

            var currentPlayer = gameState.Players.FirstOrDefault(p => p.Id == gameState.CurrentPlayerId);
            if (currentPlayer == null)
            {
                _logger.LogError("Critical state error: CurrentPlayerId {PlayerId} does not exist in the game.", gameState.CurrentPlayerId);
                throw new SessionManagementException($"Player with ID {gameState.CurrentPlayerId} not found in the current session.");
            }

            // Application-level validation: Ensure the action is for the current player
            if (action.PlayerId != currentPlayer.Id)
            {
                _logger.LogWarning("Invalid action attempt: Action for player {ActionPlayerId} but current turn is for {CurrentPlayerId}.", 
                    action.PlayerId, currentPlayer.Id);
                throw new InvalidActionException("It is not your turn to perform an action.");
            }

            // Application-level validation: Ensure the action is valid for the current phase (REQ-1-039)
            if (!_ruleEngine.IsActionAllowedInPhase(action, gameState.CurrentTurnPhase))
            {
                _logger.LogWarning("Invalid action '{ActionType}' attempted during phase '{Phase}' by player {PlayerId}.", 
                    action.GetType().Name, gameState.CurrentTurnPhase, action.PlayerId);
                throw new InvalidActionException($"The action '{action.GetType().Name}' is not allowed during the '{gameState.CurrentTurnPhase}' phase.");
            }
            
            // Domain-level validation: Delegate to the rule engine for complex game rule validation
            var validationResult = _ruleEngine.ValidateAction(gameState, action);
            if (!validationResult.IsValid)
            {
                _logger.LogInformation("Action '{ActionType}' by player {PlayerId} failed validation: {Reason}",
                    action.GetType().Name, action.PlayerId, validationResult.Reason);
                throw new InvalidActionException(validationResult.Reason ?? "The action is not valid according to game rules.");
            }

            _logger.LogInformation("Action '{ActionType}' by player {PlayerId} validated successfully. Applying action.",
                action.GetType().Name, action.PlayerId);

            // Apply the action and get the new state
            var nextState = _ruleEngine.ApplyAction(gameState, action);
            
            // Update the authoritative game state
            _gameSessionService.UpdateCurrentGameState(nextState);

            // Notify the system of the state change
            await _eventBus.PublishAsync(new GameStateUpdatedEvent(nextState), cancellationToken);

            _logger.LogDebug("GameState updated and event published after action '{ActionType}'.", action.GetType().Name);
        }

        public async Task EndTurnAsync(CancellationToken cancellationToken = default)
        {
            var gameState = _gameSessionService.GetCurrentGameState()
                ?? throw new InvalidOperationException("Cannot end turn: No active game session found.");

            // Validate that the turn is in a state that can be ended.
            if (gameState.CurrentTurnPhase is TurnPhase.PreRollManagement or TurnPhase.AwaitingPlayerDecision)
            {
                _logger.LogWarning("Attempted to end turn during an invalid phase: {Phase}", gameState.CurrentTurnPhase);
                throw new InvalidActionException($"Cannot end the turn during the '{gameState.CurrentTurnPhase}' phase.");
            }
            
            _logger.LogInformation("Ending turn for player {PlayerId}. Advancing to next player.", gameState.CurrentPlayerId);
            
            // Delegate turn advancement logic to the rule engine
            var nextState = _ruleEngine.AdvanceTurn(gameState);
            _gameSessionService.UpdateCurrentGameState(nextState);

            var newPlayer = nextState.Players.First(p => p.Id == nextState.CurrentPlayerId);

            _logger.LogInformation("Turn advanced. New active player is {PlayerName} ({PlayerId}).", newPlayer.Name, newPlayer.Id);

            // Notify system of the turn change
            await _eventBus.PublishAsync(new TurnChangedEvent(newPlayer.Id, newPlayer.Name, newPlayer.IsHuman), cancellationToken);

            // If the new player is an AI, delegate control to the AI Service (REQ-1-004)
            if (!newPlayer.IsHuman)
            {
                try
                {
                    _logger.LogDebug("Delegating turn execution to AIService for player {PlayerName}.", newPlayer.Name);
                    await _aiService.ExecuteTurnAsync(nextState, cancellationToken);
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, "An unhandled exception occurred during AI turn execution for player {PlayerName}. The AI will forfeit its turn.", newPlayer.Name);
                    // In a real-world scenario, we might want to attempt recovery or gracefully end the AI's turn
                    // For now, we log and proceed, which effectively skips the rest of the AI's turn.
                    await EndTurnAsync(cancellationToken); // Recursively end the failed turn to move to the next player.
                }
            }
        }
    }
}