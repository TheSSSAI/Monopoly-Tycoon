using Microsoft.Extensions.Logging;
using MonopolyTycoon.Application.Abstractions.Events;
using MonopolyTycoon.Application.Abstractions.Services;
using MonopolyTycoon.Application.Services.Exceptions;
using MonopolyTycoon.Domain.Abstractions;
using MonopolyTycoon.Domain.Abstractions.Enums;
using MonopolyTycoon.Domain.Abstractions.Models;
using MonopolyTycoon.Domain.Abstractions.Repositories;
using MonopolyTycoon.Domain.Entities;
using MonopolyTycoon.Domain.Objects;

namespace MonopolyTycoon.Application.Services.Services;

public class TradeOrchestrationService : ITradeOrchestrationService
{
    private readonly IGameSessionService _gameSessionService;
    private readonly IRuleEngine _ruleEngine;
    private readonly IAIService _aiService;
    private readonly IEventBus _eventBus;
    private readonly ILogger<TradeOrchestrationService> _logger;

    public TradeOrchestrationService(
        IGameSessionService gameSessionService,
        IRuleEngine ruleEngine,
        IAIService aiService,
        IEventBus eventBus,
        ILogger<TradeOrchestrationService> logger)
    {
        _gameSessionService = gameSessionService ?? throw new ArgumentNullException(nameof(gameSessionService));
        _ruleEngine = ruleEngine ?? throw new ArgumentNullException(nameof(ruleEngine));
        _aiService = aiService ?? throw new ArgumentNullException(nameof(aiService));
        _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<TradeResult> ProposeTradeAsync(TradeProposal proposal, CancellationToken cancellationToken = default)
    {
        var gameState = _gameSessionService.GetCurrentGameState();
        if (gameState is null)
        {
            throw new InvalidOperationException("Cannot propose a trade without an active game session.");
        }

        _logger.LogInformation(
            "Trade proposed by Player '{OfferingPlayerId}' to Player '{TargetPlayerId}'.",
            proposal.OfferingPlayerId,
            proposal.TargetPlayerId);

        var validationResult = _ruleEngine.ValidateTradeProposal(gameState, proposal);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning(
                "Trade proposal failed validation: {Reason}",
                validationResult.Reason);
            return TradeResult.Invalid;
        }

        var targetPlayer = gameState.GetPlayerById(proposal.TargetPlayerId);
        if (targetPlayer is null)
        {
            _logger.LogError("Target player with ID '{TargetPlayerId}' not found in game state.", proposal.TargetPlayerId);
            return TradeResult.Invalid;
        }

        TradeResult decision;
        if (targetPlayer.IsHuman)
        {
            // For Human players, this method is called AFTER they have accepted through the UI.
            // The UI response would have triggered the call. We assume acceptance and proceed to execution.
            // A more robust system might have a pending trade state, but we follow the stateless service model.
            decision = TradeResult.Accepted;
        }
        else
        {
            // For AI players, we ask them to evaluate the trade now.
            decision = await _aiService.EvaluateTradeProposalAsync(gameState, proposal, cancellationToken);
        }

        _logger.LogInformation("Trade proposal evaluation result: {Decision}", decision);

        if (decision == TradeResult.Accepted)
        {
            await ExecuteTradeAsync(gameState, proposal);
        }

        return decision;
    }

    public async Task RespondToAiTradeAsync(TradeProposal proposal, TradeUserResponse response, CancellationToken cancellationToken = default)
    {
        var gameState = _gameSessionService.GetCurrentGameState();
        if (gameState is null)
        {
            throw new InvalidOperationException("Cannot respond to a trade without an active game session.");
        }
        
        _logger.LogInformation(
            "Human player responded with '{Response}' to trade proposal from AI '{OfferingPlayerId}'.",
            response,
            proposal.OfferingPlayerId);

        switch (response)
        {
            case TradeUserResponse.Accept:
                // Re-validate the trade before execution to ensure the state hasn't changed in a way that invalidates it.
                var validationResult = _ruleEngine.ValidateTradeProposal(gameState, proposal);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("AI trade proposal became invalid before acceptance: {Reason}. Aborting.", validationResult.Reason);
                    // Optionally publish an event to inform the UI that the trade failed.
                    await _eventBus.PublishAsync(new TradeFailedEvent(proposal, validationResult.Reason), cancellationToken);
                    return;
                }
                await ExecuteTradeAsync(gameState, proposal);
                break;
            case TradeUserResponse.Decline:
                // No action needed, the trade is simply not executed.
                // An event could be published if UI needs to know about the decline confirmation.
                break;
            case TradeUserResponse.ProposeCounterOffer:
                // This response is handled by the Presentation layer. It closes the initial
                // proposal dialog and opens the full trading UI, which will then call
                // ProposeTradeAsync with a new proposal. This service does not manage that UI flow.
                _logger.LogInformation("Player chose to propose a counter-offer. Awaiting new proposal from UI.");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(response), response, "Unknown trade response.");
        }
    }

    private async Task ExecuteTradeAsync(GameState currentGameState, TradeProposal proposal)
    {
        try
        {
            _logger.LogInformation("Executing accepted trade between '{OfferingPlayerId}' and '{TargetPlayerId}'.",
                proposal.OfferingPlayerId,
                proposal.TargetPlayerId);

            var tradeAction = new PlayerAction.Trade(
                proposal.OfferingPlayerId, 
                proposal.TargetPlayerId, 
                proposal.PropertiesOffered, 
                proposal.CashOffered,
                proposal.CardsOffered,
                proposal.PropertiesRequested, 
                proposal.CashRequested,
                proposal.CardsRequested
                );

            var newGameState = _ruleEngine.ApplyAction(currentGameState, tradeAction);
            
            _gameSessionService.UpdateGameState(newGameState);

            var offeringPlayer = newGameState.GetPlayerById(proposal.OfferingPlayerId);
            var targetPlayer = newGameState.GetPlayerById(proposal.TargetPlayerId);
            
            // This event is for notifying the UI about the specifics of the trade completion.
            // It's particularly useful for notifying the human player about AI-to-AI trades.
            await _eventBus.PublishAsync(new TradeCompletedEvent(
                offeringPlayer?.Name ?? "Unknown", 
                targetPlayer?.Name ?? "Unknown",
                proposal));

            // This is a general event to notify all subscribers that the game state has changed.
            await _eventBus.PublishAsync(new GameStateUpdatedEvent(newGameState));
            
            _logger.LogInformation("Trade executed successfully and game state updated.");
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "A critical error occurred while executing a trade. The game state may be inconsistent.");
            // In a real-world scenario, you might want to try and revert to a previous state or flag the game as corrupted.
            // For now, we re-throw to indicate a catastrophic failure.
            throw new SessionManagementException("Failed to execute trade due to a system error. The game session is now in an unstable state.", ex);
        }
    }
}