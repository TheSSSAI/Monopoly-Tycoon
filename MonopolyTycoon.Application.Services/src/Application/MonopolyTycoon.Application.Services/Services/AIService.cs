using Microsoft.Extensions.Logging;
using MonopolyTycoon.Application.Abstractions.Services;
using MonopolyTycoon.Domain.Abstractions.AI;
using MonopolyTycoon.Domain.Abstractions.Events;
using MonopolyTycoon.Domain.Abstractions.Services;
using MonopolyTycoon.Domain.Entities;
using MonopolyTycoon.Domain.Enums;
using MonopolyTycoon.Domain.Objects;

namespace MonopolyTycoon.Application.Services.Services;

public class AIService : IAIService
{
    private readonly IGameSessionService _sessionService;
    private readonly IAIController _aiController;
    private readonly IRuleEngine _ruleEngine;
    private readonly IEventBus _eventBus;
    private readonly ILogger<AIService> _logger;

    public AIService(
        IGameSessionService sessionService,
        IAIController aiController,
        IRuleEngine ruleEngine,
        IEventBus eventBus,
        ILogger<AIService> logger)
    {
        _sessionService = sessionService;
        _aiController = aiController;
        _ruleEngine = ruleEngine;
        _eventBus = eventBus;
        _logger = logger;
    }

    public async Task ExecuteTurnAsync(CancellationToken cancellationToken = default)
    {
        var gameState = _sessionService.GetCurrentGameState();
        var aiPlayer = gameState.GetCurrentPlayer();

        if (aiPlayer is null || aiPlayer.IsHuman)
        {
            _logger.LogWarning("ExecuteTurnAsync called, but the current player is not a valid AI. PlayerId: {PlayerId}", gameState.ActivePlayerId);
            return;
        }

        _logger.LogInformation("Executing turn for AI Player: {PlayerName} ({Difficulty})", aiPlayer.Name, aiPlayer.AiDifficulty);

        try
        {
            // Main AI turn loop
            while (!cancellationToken.IsCancellationRequested)
            {
                var action = await _aiController.GetNextActionAsync(gameState, cancellationToken);

                if (action is null || action.IsEndTurnAction())
                {
                    _logger.LogInformation("AI {PlayerName} has decided to end its turn.", aiPlayer.Name);
                    break;
                }

                var validationResult = _ruleEngine.ValidateAction(gameState, action);

                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("AI {PlayerName} proposed an invalid action: {ActionType}. Reason: {Reason}. AI will skip this action.",
                        aiPlayer.Name, action.GetType().Name, validationResult.Reason);
                    // In a real scenario, we might want to penalize the AI or have it re-evaluate.
                    // For now, we just skip the invalid action and let it propose another.
                    continue;
                }
                
                _logger.LogInformation("AI {PlayerName} is performing action: {ActionType}", aiPlayer.Name, action.GetType().Name);
                gameState = _ruleEngine.ApplyAction(gameState, action);

                // Update the session's game state with the new state after the action
                _sessionService.SetCurrentGameState(gameState);
                await _eventBus.PublishAsync(new GameStateUpdatedEvent(gameState), cancellationToken);

                // Apply a delay to make AI turns watchable, respecting game speed settings.
                await ApplyTurnDelay(gameState.GameSpeed, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred during AI player {PlayerName}'s turn. The turn will be forcefully ended to maintain game stability.", aiPlayer.Name);
            // In a real-world scenario, we might try to recover or just end the turn.
            // For stability, we log the error and let the turn end.
        }
        finally
        {
            _logger.LogInformation("Finished turn for AI Player: {PlayerName}", aiPlayer.Name);
        }
    }

    public async Task<TradeDecision> EvaluateTradeProposalAsync(TradeOffer tradeOffer, CancellationToken cancellationToken = default)
    {
        var gameState = _sessionService.GetCurrentGameState();
        var targetPlayer = gameState.Players.FirstOrDefault(p => p.Id == tradeOffer.TargetPlayerId);

        if (targetPlayer is null || targetPlayer.IsHuman)
        {
            _logger.LogError("EvaluateTradeProposalAsync called for a non-AI or non-existent player. TargetPlayerId: {PlayerId}", tradeOffer.TargetPlayerId);
            return TradeDecision.Declined;
        }

        _logger.LogInformation("Forwarding trade proposal to AI {PlayerName} for evaluation.", targetPlayer.Name);

        try
        {
            var decision = await _aiController.EvaluateTradeAsync(gameState, tradeOffer, cancellationToken);
            _logger.LogInformation("AI {PlayerName} has evaluated the trade and decided to {Decision}", targetPlayer.Name, decision);
            return decision;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while AI {PlayerName} was evaluating a trade. Defaulting to decline.", targetPlayer.Name);
            return TradeDecision.Declined;
        }
    }
    
    private static async Task ApplyTurnDelay(GameSpeed gameSpeed, CancellationToken cancellationToken)
    {
        int delayMs = gameSpeed switch
        {
            GameSpeed.Normal => 1000,
            GameSpeed.Fast => 300,
            GameSpeed.Instant => 0,
            _ => 1000
        };

        if (delayMs > 0)
        {
            await Task.Delay(delayMs, cancellationToken);
        }
    }
}