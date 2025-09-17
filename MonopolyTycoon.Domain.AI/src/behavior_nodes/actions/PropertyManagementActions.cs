using MonopolyTycoon.Domain.AI.Common;
using MonopolyTycoon.Domain.Entities;
using MonopolyTycoon.Domain.Enums;
using MonopolyTycoon.Domain.ValueObjects;
using Panda;
using System.Linq;

namespace MonopolyTycoon.Domain.AI.BehaviorNodes.Actions
{
    public class PropertyManagementActions
    {
        private readonly AIContext _context;

        public PropertyManagementActions(AIContext context)
        {
            _context = context;
        }

        [Task]
        public void AttemptToBuyUnownedProperty()
        {
            var aiPlayer = _context.CurrentPlayerState;
            var currentSpace = _context.GameState.Board.GetSpace(aiPlayer.CurrentPosition);

            if (currentSpace is not Property property || property.Owner is not null)
            {
                ThisTask.Fail();
                return;
            }

            if (aiPlayer.Cash < property.Price)
            {
                ThisTask.Fail();
                return;
            }

            // Simple decision logic based on parameters. Hard AI is more likely to buy anything.
            bool shouldBuy = aiPlayer.Cash > (_context.Parameters.MinimumCashReserve + property.Price)
                             && _context.Random.NextDouble() < _context.Parameters.PropertyAcquisitionAggressiveness;

            if (shouldBuy)
            {
                _context.ResultAction = new PlayerAction(PlayerActionType.BuyProperty);
                ThisTask.Succeed();
            }
            else
            {
                // Let the property go to auction
                _context.ResultAction = new PlayerAction(PlayerActionType.AuctionProperty);
                ThisTask.Succeed();
            }
        }

        [Task]
        public void ImproveBestMonopoly()
        {
            var aiPlayer = _context.CurrentPlayerState;
            if (aiPlayer.Cash < _context.Parameters.MinimumCashReserve)
            {
                ThisTask.Fail();
                return;
            }

            var ownedMonopolies = _context.GameState.Board.GetPlayerMonopolies(aiPlayer.Id);
            if (!ownedMonopolies.Any())
            {
                ThisTask.Fail();
                return;
            }

            Property? bestPropertyToImprove = null;
            double bestRoi = -1.0;

            foreach (var monopoly in ownedMonopolies)
            {
                // Ensure no property in the set is mortgaged
                if (monopoly.Any(p => p.IsMortgaged))
                {
                    continue;
                }
                
                // Find the property to build on according to the 'even build' rule
                var minHouses = monopoly.Min(p => p.Houses);
                var candidates = monopoly.Where(p => p.Houses == minHouses && p.Houses < 5).ToList();

                if (!candidates.Any())
                {
                    continue; // Fully developed with hotels or cannot build more houses
                }

                // Harder AI prioritizes building more aggressively.
                if (_context.Random.NextDouble() > _context.Parameters.BuildingAggressiveness)
                {
                    continue;
                }

                foreach (var candidate in candidates)
                {
                    if (aiPlayer.Cash < candidate.HouseCost + _context.Parameters.MinimumCashReserve)
                    {
                        continue;
                    }

                    // Calculate ROI: (New Rent - Current Rent) / Cost
                    int currentRent = candidate.GetRent(aiPlayer, _context.GameState.Board);
                    candidate.Houses++; // Temporarily increment to calculate new rent
                    int newRent = candidate.GetRent(aiPlayer, _context.GameState.Board);
                    candidate.Houses--; // Revert change

                    double roi = (double)(newRent - currentRent) / candidate.HouseCost;

                    if (roi > bestRoi)
                    {
                        bestRoi = roi;
                        bestPropertyToImprove = candidate;
                    }
                }
            }

            if (bestPropertyToImprove != null)
            {
                _context.ResultAction = new PlayerAction(PlayerActionType.BuildHouse, bestPropertyToImprove.Id);
                ThisTask.Succeed();
            }
            else
            {
                ThisTask.Fail();
            }
        }

        [Task]
        public void MortgagePropertyToRaiseCash()
        {
            var aiPlayer = _context.CurrentPlayerState;
            var ownedProperties = _context.GameState.Board.GetPlayerProperties(aiPlayer.Id);

            var unmortgagedUndevelopedProperties = ownedProperties
                .Where(p => !p.IsMortgaged && p.Houses == 0)
                .OrderBy(p => p.Price) // Mortgage least valuable properties first
                .ToList();

            if (!unmortgagedUndevelopedProperties.Any())
            {
                ThisTask.Fail();
                return;
            }
            
            var propertyToMortgage = unmortgagedUndevelopedProperties.First();
            _context.ResultAction = new PlayerAction(PlayerActionType.MortgageProperty, propertyToMortgage.Id);
            ThisTask.Succeed();
        }

        [Task]
        public void UnmortgagePropertyIfCashRich()
        {
            var aiPlayer = _context.CurrentPlayerState;

            // Only consider unmortgaging if cash is well above the reserve
            if (aiPlayer.Cash < _context.Parameters.MinimumCashReserve * 2.5)
            {
                ThisTask.Fail();
                return;
            }

            var ownedProperties = _context.GameState.Board.GetPlayerProperties(aiPlayer.Id);
            var mortgagedProperties = ownedProperties
                .Where(p => p.IsMortgaged)
                .OrderByDescending(p => p.Price) // Unmortgage most valuable first
                .ToList();

            if (!mortgagedProperties.Any())
            {
                ThisTask.Fail();
                return;
            }

            var propertyToUnmortgage = mortgagedProperties.First();
            var unmortgageCost = (int)(propertyToUnmortgage.MortgageValue * 1.10);

            if (aiPlayer.Cash > unmortgageCost + _context.Parameters.MinimumCashReserve)
            {
                _context.ResultAction = new PlayerAction(PlayerActionType.UnmortgageProperty, propertyToUnmortgage.Id);
                ThisTask.Succeed();
            }
            else
            {
                ThisTask.Fail();
            }
        }
    }
}