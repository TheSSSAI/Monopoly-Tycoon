using MonopolyTycoon.Domain.AI.Common;
using MonopolyTycoon.Domain.Entities;
using MonopolyTycoon.Domain.Enums;
using MonopolyTycoon.Domain.ValueObjects;
using Panda;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonopolyTycoon.Domain.AI.BehaviorNodes.Actions
{
    public class TradingActions
    {
        private readonly AIContext _context;

        public TradingActions(AIContext context)
        {
            _context = context;
        }

        [Task]
        public void ProposeTradeToCompleteMonopoly()
        {
            var aiPlayer = _context.CurrentPlayerState;
            var board = _context.GameState.Board;
            var otherPlayers = _context.GameState.Players.Where(p => p.Id != aiPlayer.Id && p.Status == PlayerStatus.Active).ToList();

            var potentialMonopolies = board.GetAllColorGroups()
                .Select(group => new
                {
                    Color = group.Key,
                    OwnedByAI = group.Value.Count(p => p.Owner == aiPlayer.Id),
                    TotalInSet = group.Value.Count
                })
                .Where(m => m.OwnedByAI > 0 && m.OwnedByAI < m.TotalInSet)
                .OrderByDescending(m => m.OwnedByAI) // Prioritize sets where AI is closest to completion
                .ToList();

            foreach (var monopolyInfo in potentialMonopolies)
            {
                var neededProperties = board.GetPropertiesInColorGroup(monopolyInfo.Color)
                    .Where(p => p.Owner != aiPlayer.Id && p.Owner != null)
                    .ToList();
                
                if (neededProperties.Count + monopolyInfo.OwnedByAI != monopolyInfo.TotalInSet)
                {
                    continue; // Some are unowned, better to buy/auction them first.
                }

                // Find a player who owns one of the needed properties
                foreach (var neededProperty in neededProperties)
                {
                    var targetPlayer = otherPlayers.FirstOrDefault(p => p.Id == neededProperty.Owner);
                    if (targetPlayer == null) continue;

                    // Now, construct an offer. Find something the AI has that the target player might want.
                    var offer = ConstructFairOffer(aiPlayer, targetPlayer, neededProperty);
                    if (offer != null)
                    {
                        _context.ResultAction = new PlayerAction(PlayerActionType.ProposeTrade, payload: offer);
                        ThisTask.Succeed();
                        return;
                    }
                }
            }

            ThisTask.Fail();
        }

        private TradeOffer? ConstructFairOffer(PlayerState self, PlayerState target, Property desiredProperty)
        {
            // Simplified valuation logic. A real implementation would be more complex.
            double desiredValue = desiredProperty.Price * _context.Parameters.MonopolyCompletionFactor;
            
            // Offer cash first if AI is rich
            if (self.Cash > _context.Parameters.MinimumCashReserve * 2 && self.Cash > desiredValue)
            {
                 // Hard AI will try to pay less
                int cashOffer = (int)(desiredValue * (1.0 / _context.Parameters.TradeAggressiveness));
                if (self.Cash > cashOffer)
                {
                    return new TradeOffer(self.Id, target.Id, new List<int>(), cashOffer, new List<int>(), new List<int> { desiredProperty.Id }, 0, new List<int>());
                }
            }

            // Look for properties to trade away
            var tradableProperties = _context.GameState.Board.GetPlayerProperties(self.Id)
                .Where(p => p.Houses == 0 && !IsPartOfAlmostCompleteMonopoly(p, self))
                .OrderBy(p => p.Price).ToList();
            
            List<int> propertiesToOffer = new();
            int offeredPropertyValue = 0;

            foreach (var prop in tradableProperties)
            {
                propertiesToOffer.Add(prop.Id);
                offeredPropertyValue += prop.Price;
                if (offeredPropertyValue >= desiredValue * 0.8) // Try to get close to the value
                    break;
            }

            if (offeredPropertyValue > 0)
            {
                int cashSweetener = (int)(desiredValue - offeredPropertyValue);
                if (cashSweetener < 0) cashSweetener = 0;

                if (self.Cash > cashSweetener + _context.Parameters.MinimumCashReserve)
                {
                    return new TradeOffer(self.Id, target.Id, propertiesToOffer, cashSweetener, new List<int>(), new List<int> { desiredProperty.Id }, 0, new List<int>());
                }
            }

            return null; // Can't construct a viable offer
        }

        private bool IsPartOfAlmostCompleteMonopoly(Property property, PlayerState player)
        {
            var group = _context.GameState.Board.GetPropertiesInColorGroup(property.Color);
            int ownedCount = group.Count(p => p.Owner == player.Id);
            return ownedCount == group.Count - 1;
        }

        [Task]
        public void EvaluateIncomingTrade()
        {
            if (_context.GameState.PendingTradeOffer == null || _context.GameState.PendingTradeOffer.TargetPlayerId != _context.CurrentPlayerState.Id)
            {
                ThisTask.Fail();
                return;
            }

            var offer = _context.GameState.PendingTradeOffer;
            var self = _context.CurrentPlayerState;
            var proposer = _context.GameState.Players.First(p => p.Id == offer.InitiatingPlayerId);

            double valueForSelf = CalculateOfferValue(offer.TargetProperties, offer.TargetCash, self);
            double valueForProposer = CalculateOfferValue(offer.InitiatingProperties, offer.InitiatingCash, proposer);

            // Adjust values based on strategic importance
            if (OfferCompletesMonopoly(offer.InitiatingProperties, proposer))
                valueForProposer *= _context.Parameters.MonopolyCompletionFactor;
            
            if (OfferCompletesMonopoly(offer.TargetProperties, self))
                valueForSelf *= _context.Parameters.MonopolyCompletionFactor;

            if (valueForSelf > valueForProposer * _context.Parameters.TradeAggressiveness)
            {
                _context.ResultAction = new PlayerAction(PlayerActionType.AcceptTrade, offer.Id);
            }
            else
            {
                _context.ResultAction = new PlayerAction(PlayerActionType.DeclineTrade, offer.Id);
            }
            
            ThisTask.Succeed();
        }

        private double CalculateOfferValue(List<int> propertyIds, int cash, PlayerState forPlayer)
        {
            double value = cash;
            foreach (var propId in propertyIds)
            {
                var prop = _context.GameState.Board.GetProperty(propId);
                value += prop.Price;
            }
            return value;
        }

        private bool OfferCompletesMonopoly(List<int> offeredPropertyIds, PlayerState receivingPlayer)
        {
            var board = _context.GameState.Board;
            var playerProperties = board.GetPlayerProperties(receivingPlayer.Id).Select(p => p.Id).ToList();
            
            foreach (var propId in offeredPropertyIds)
            {
                var property = board.GetProperty(propId);
                var group = board.GetPropertiesInColorGroup(property.Color);
                
                var combinedProperties = playerProperties.Union(offeredPropertyIds).ToList();
                if (group.All(p => combinedProperties.Contains(p.Id)))
                {
                    return true;
                }
            }
            return false;
        }
    }
}