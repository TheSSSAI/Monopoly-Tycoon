using MonopolyTycoon.Domain.AI.Common;
using MonopolyTycoon.Domain.Entities;
using MonopolyTycoon.Domain.Enums;
using Panda;
using System.Linq;

namespace MonopolyTycoon.Domain.AI.BehaviorNodes.Conditions
{
    public class TradingConditions
    {
        private readonly AIContext _context;

        public TradingConditions(AIContext context)
        {
            _context = context;
        }

        [Task]
        public void IsTradePossible()
        {
            // Check if there is at least one other active player to trade with.
            bool otherActivePlayerExists = _context.GameState.Players
                .Any(p => p.Id != _context.CurrentPlayerState.Id && p.Status == PlayerStatus.Active);
            
            if (otherActivePlayerExists)
            {
                ThisTask.Succeed();
            }
            else
            {
                ThisTask.Fail();
            }
        }

        [Task]
        public void IsIncomingTradeOfferPending()
        {
            if (_context.GameState.PendingTradeOffer != null && _context.GameState.PendingTradeOffer.TargetPlayerId == _context.CurrentPlayerState.Id)
            {
                ThisTask.Succeed();
            }
            else
            {
                ThisTask.Fail();
            }
        }
        
        [Task]
        public void DoesAnyPlayerHavePropertyAINeedsForMonopoly()
        {
            var aiPlayerId = _context.CurrentPlayerState.Id;
            var board = _context.GameState.Board;

            var potentialMonopolies = board.GetAllColorGroups()
                .Where(group => {
                    var propertiesInGroup = group.Value;
                    int ownedByAI = propertiesInGroup.Count(p => p.Owner == aiPlayerId);
                    int unowned = propertiesInGroup.Count(p => p.Owner == null);
                    // A trade is needed if the AI owns some, none are unowned, and the rest are owned by others.
                    return ownedByAI > 0 && ownedByAI + unowned < propertiesInGroup.Count;
                });
            
            if (potentialMonopolies.Any())
            {
                ThisTask.Succeed();
            }
            else
            {
                ThisTask.Fail();
            }
        }

        [Task]
        public void IsWillingToTrade()
        {
            // The AI's willingness to initiate trades is a parameter
            if (_context.Random.NextDouble() < _context.Parameters.TradeWillingness)
            {
                ThisTask.Succeed();
            }
            else
            {
                ThisTask.Fail();
            }
        }
    }
}