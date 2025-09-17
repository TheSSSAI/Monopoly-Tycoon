using Microsoft.Extensions.Logging;
using MonopolyTycoon.Domain.AI.BehaviorNodes.Actions;
using MonopolyTycoon.Domain.AI.BehaviorNodes.Conditions;
using MonopolyTycoon.Domain.AI.Interfaces;
using MonopolyTycoon.Domain.Core;
using MonopolyTycoon.Domain.Core.Enums;
using MonopolyTycoon.Domain.Core.Events;
using Panda;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace MonopolyTycoon.Domain.AI
{
    public class AIBehaviorTreeExecutor : IAIController
    {
        private readonly ILogger<AIBehaviorTreeExecutor> _logger;
        private readonly Dictionary<string, Panda.Tree> _compiledTrees = new();
        private const string TradeEvaluationTreeName = "trade_evaluation";
        private const string AuctionBiddingTreeName = "auction_bidding";
        private const string DefaultTurnActionTreeName = "default_turn_action";


        public AIBehaviorTreeExecutor(ILogger<AIBehaviorTreeExecutor> logger)
        {
            _logger = logger;
            LoadAndCompileBehaviorTrees();
        }

        public PlayerAction GetNextAction(GameState state, Guid aiPlayerId, AIParameters parameters, TurnPhase phase)
        {
            try
            {
                var context = new AIContext(state, aiPlayerId, parameters, phase);
                string treeName = parameters.DifficultyLevel.ToLowerInvariant();
                
                if (!_compiledTrees.TryGetValue(treeName, out var behaviorTree))
                {
                    _logger.LogWarning("Behavior tree for difficulty '{Difficulty}' not found. Using default.", treeName);
                    treeName = DefaultTurnActionTreeName;
                    behaviorTree = _compiledTrees[treeName];
                }

                _logger.LogDebug("Executing '{TreeName}' behavior tree for AI Player {PlayerId} during {Phase} phase.", 
                    treeName, aiPlayerId, phase);

                ExecuteTree(behaviorTree, context);

                if (context.ResultAction == null)
                {
                    _logger.LogWarning("AI Player {PlayerId} failed to decide on an action. Defaulting to EndTurnAction.", aiPlayerId);
                    return new EndTurnAction();
                }
                
                _logger.LogInformation("AI Player {PlayerId} decided on action: {ActionType}", aiPlayerId, context.ResultAction.GetType().Name);
                return context.ResultAction;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred during AI action selection for player {PlayerId}. Defaulting to EndTurnAction.", aiPlayerId);
                return new EndTurnAction();
            }
        }

        public TradeDecision EvaluateTrade(GameState state, Guid aiPlayerId, AIParameters parameters, TradeOffer offer)
        {
             try
            {
                var context = new AIContext(state, aiPlayerId, parameters, TurnPhase.PreRoll, offer);
                
                if (!_compiledTrees.TryGetValue(TradeEvaluationTreeName, out var behaviorTree))
                {
                    _logger.LogError("Trade evaluation behavior tree not found. Defaulting to Decline.");
                    return TradeDecision.Decline;
                }

                _logger.LogDebug("Executing trade evaluation tree for AI Player {PlayerId}.", aiPlayerId);
                ExecuteTree(behaviorTree, context);

                var decision = context.TradeDecisionResult;
                _logger.LogInformation("AI Player {PlayerId} evaluated trade offer and decided to {Decision}.", aiPlayerId, decision);
                return decision;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred during AI trade evaluation for player {PlayerId}. Defaulting to Decline.", aiPlayerId);
                return TradeDecision.Decline;
            }
        }

        public BidDecision GetAuctionBid(GameState state, Guid aiPlayerId, AIParameters parameters, AuctionState auctionState)
        {
            try
            {
                var context = new AIContext(state, aiPlayerId, parameters, TurnPhase.Action, auctionState: auctionState);

                if (!_compiledTrees.TryGetValue(AuctionBiddingTreeName, out var behaviorTree))
                {
                    _logger.LogError("Auction bidding behavior tree not found. Defaulting to Pass.");
                    return BidDecision.Pass();
                }
                
                _logger.LogDebug("Executing auction bidding tree for AI Player {PlayerId}.", aiPlayerId);
                ExecuteTree(behaviorTree, context);

                var decision = context.BidDecisionResult ?? BidDecision.Pass();
                _logger.LogInformation("AI Player {PlayerId} decided on auction action: {Decision}.", aiPlayerId, 
                    decision.ShouldPass ? "Pass" : $"Bid ${decision.Amount}");
                return decision;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred during AI auction bidding for player {PlayerId}. Defaulting to Pass.", aiPlayerId);
                return BidDecision.Pass();
            }
        }

        private void ExecuteTree(Panda.Tree behaviorTree, AIContext context)
        {
            // Instantiate all node provider classes with the current context
            var propertyActions = new PropertyManagementActions(context);
            var tradingActions = new TradingActions(context);
            var turnActions = new TurnManagementActions(context);
            var playerConditions = new PlayerStateConditions(context);
            var tradingConditions = new TradingConditions(context);

            // Bind the instances to the BT executor
            var executor = new Panda.Executor();
            executor.Bind(propertyActions);
            executor.Bind(tradingActions);
            executor.Bind(turnActions);
            executor.Bind(playerConditions);
            executor.Bind(tradingConditions);

            // Tick the tree until it's done
            executor.Start(behaviorTree);
            while (!executor.Done)
            {
                executor.Tick();
            }
        }

        private void LoadAndCompileBehaviorTrees()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames = assembly.GetManifestResourceNames();
            
            _logger.LogInformation("Scanning assembly for embedded behavior trees...");

            foreach (var resourceName in resourceNames)
            {
                if (resourceName.EndsWith(".bt", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        using var stream = assembly.GetManifestResourceStream(resourceName);
                        if (stream == null)
                        {
                            _logger.LogWarning("Could not find embedded resource stream for '{ResourceName}'.", resourceName);
                            continue;
                        }
                        
                        using var reader = new StreamReader(stream, Encoding.UTF8);
                        string script = reader.ReadToEnd();

                        var tree = new Panda.Tree(script);
                        if (tree.HasError)
                        {
                            _logger.LogError("Failed to compile behavior tree '{ResourceName}': {Error}", resourceName, tree.Error);
                            continue;
                        }
                        
                        // Extract a key from the resource name, e.g., "MonopolyTycoon.Domain.AI.BehaviorTrees.easy.bt" -> "easy"
                        string key = Path.GetFileNameWithoutExtension(resourceName).ToLowerInvariant();
                        _compiledTrees[key] = tree;
                        _logger.LogInformation("Successfully compiled and loaded behavior tree: {Key}", key);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "An exception occurred while loading and compiling behavior tree '{ResourceName}'.", resourceName);
                    }
                }
            }

            // Create some hardcoded trees for specific scenarios if not found in resources
            EnsureSpecializedTreesExist();
        }

        private void EnsureSpecializedTreesExist()
        {
            if (!_compiledTrees.ContainsKey(DefaultTurnActionTreeName))
            {
                _logger.LogInformation("Creating default fallback turn action tree.");
                // A very simple tree that just ends the turn.
                var defaultScript = "sequence( EndTurn )";
                var tree = new Panda.Tree(defaultScript);
                if (tree.HasError)
                {
                     _logger.LogError("Failed to compile default turn action tree: {Error}", tree.Error);
                }
                else
                {
                    _compiledTrees[DefaultTurnActionTreeName] = tree;
                }
            }

            if (!_compiledTrees.ContainsKey(TradeEvaluationTreeName))
            {
                 _logger.LogInformation("Creating default trade evaluation tree.");
                // A tree that evaluates the trade and decides.
                var tradeScript = "selector( sequence( ShouldAcceptTrade, AcceptTrade ), DeclineTrade )";
                var tree = new Panda.Tree(tradeScript);
                if (tree.HasError)
                {
                    _logger.LogError("Failed to compile default trade evaluation tree: {Error}", tree.Error);
                }
                else
                {
                    _compiledTrees[TradeEvaluationTreeName] = tree;
                }
            }

            if (!_compiledTrees.ContainsKey(AuctionBiddingTreeName))
            {
                _logger.LogInformation("Creating default auction bidding tree.");
                // A tree that decides whether to bid or pass.
                var auctionScript = "selector( sequence( ShouldPlaceBid, PlaceBid ), PassAuction )";
                var tree = new Panda.Tree(auctionScript);
                if (tree.HasError)
                {
                    _logger.LogError("Failed to compile default auction bidding tree: {Error}", tree.Error);
                }
                else
                {
                    _compiledTrees[AuctionBiddingTreeName] = tree;
                }
            }
        }
    }
}