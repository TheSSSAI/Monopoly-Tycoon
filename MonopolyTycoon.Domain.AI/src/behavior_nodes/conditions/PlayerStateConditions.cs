using MonopolyTycoon.Domain.AI.Common;
using MonopolyTycoon.Domain.Enums;
using Panda;
using System.Linq;

namespace MonopolyTycoon.Domain.AI.BehaviorNodes.Conditions
{
    public class PlayerStateConditions
    {
        private readonly AIContext _context;

        public PlayerStateConditions(AIContext context)
        {
            _context = context;
        }

        [Task]
        public void IsCashBelowReserveThreshold()
        {
            if (_context.CurrentPlayerState.Cash < _context.Parameters.MinimumCashReserve)
            {
                ThisTask.Succeed();
            }
            else
            {
                ThisTask.Fail();
            }
        }

        [Task]
        public void HasAnyMonopoly()
        {
            if (_context.GameState.Board.GetPlayerMonopolies(_context.CurrentPlayerState.Id).Any())
            {
                ThisTask.Succeed();
            }
            else
            {
                ThisTask.Fail();
            }
        }

        [Task]
        public void CanAffordToBuildOnAnyMonopoly()
        {
            var aiPlayer = _context.CurrentPlayerState;
            var monopolies = _context.GameState.Board.GetPlayerMonopolies(aiPlayer.Id);

            if (!monopolies.Any())
            {
                ThisTask.Fail();
                return;
            }

            foreach (var monopoly in monopolies)
            {
                // Cannot build on a mortgaged set
                if (monopoly.Any(p => p.IsMortgaged))
                {
                    continue;
                }
                
                // Find the cheapest house in the set that can be built
                var minHouses = monopoly.Min(p => p.Houses);
                var cheapestPropertyToBuildOn = monopoly
                    .Where(p => p.Houses == minHouses && p.Houses < 5)
                    .OrderBy(p => p.HouseCost)
                    .FirstOrDefault();

                if (cheapestPropertyToBuildOn != null && aiPlayer.Cash >= cheapestPropertyToBuildOn.HouseCost + _context.Parameters.MinimumCashReserve)
                {
                    ThisTask.Succeed();
                    return;
                }
            }

            ThisTask.Fail();
        }

        [Task]
        public void IsInJail()
        {
            if (_context.CurrentPlayerState.Status == PlayerStatus.InJail)
            {
                ThisTask.Succeed();
            }
            else
            {
                ThisTask.Fail();
            }
        }

        [Task]
        public void HasGetOutOfJailCard()
        {
            if (_context.CurrentPlayerState.GetOutOfJailCards > 0)
            {
                ThisTask.Succeed();
            }
            else
            {
                ThisTask.Fail();
            }
        }
        
        [Task]
        public void IsInPreRollPhase()
        {
            if (_context.CurrentPhase == TurnPhase.PreRoll)
            {
                ThisTask.Succeed();
            }
            else
            {
                ThisTask.Fail();
            }
        }

        [Task]
        public void IsInPostRollPhase()
        {
            if (_context.CurrentPhase == TurnPhase.PostRoll)
            {
                ThisTask.Succeed();
            }
            else
            {
                ThisTask.Fail();
            }
        }

        [Task]
        public void IsOnUnownedProperty()
        {
            var currentSpace = _context.GameState.Board.GetSpace(_context.CurrentPlayerState.CurrentPosition);
            if (currentSpace is Domain.Entities.Property property && property.Owner == null)
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