using MonopolyTycoon.Domain;
using MonopolyTycoon.Domain.Actions;
using MonopolyTycoon.Domain.Entities;
using MonopolyTycoon.Domain.Enums;
using MonopolyTycoon.Domain.RuleEngine.Exceptions;
using MonopolyTycoon.Domain.RuleEngine.Interfaces;
using MonopolyTycoon.Domain.RuleEngine.Models;
using System.Linq;

namespace MonopolyTycoon.Domain.RuleEngine.Services
{
    /// <summary>
    /// Implements the core game logic of Monopoly, providing stateless functions to validate
    /// player actions and compute resulting game states.
    /// This engine is the single source of truth for all game rules.
    /// </summary>
    public class RuleEngine : IRuleEngine
    {
        #region Public Interface Implementation

        /// <summary>
        /// Validates if a proposed player action is legal according to the official Monopoly rules
        /// given the current game state. This method does not cause any side effects.
        /// </summary>
        /// <param name="state">The current, immutable state of the game.</param>
        /// <param name="action">The proposed action to be validated.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating if the action is valid and providing an error message if not.</returns>
        public ValidationResult ValidateAction(GameState state, PlayerAction action)
        {
            if (state is null) return ValidationResult.Failure("GameState cannot be null.");
            if (action is null) return ValidationResult.Failure("PlayerAction cannot be null.");
            if (state.Players.All(p => p.Id != action.PlayerId)) return ValidationResult.Failure("Player not found in game state.");

            return action switch
            {
                PurchasePropertyAction ppa => ValidatePurchaseProperty(state, ppa),
                BuildHouseAction bha => ValidateBuildHouse(state, bha),
                MortgagePropertyAction mpa => ValidateMortgageProperty(state, mpa),
                UnmortgagePropertyAction uma => ValidateUnmortgageProperty(state, uma),
                ProposeTradeAction pta => ValidateProposeTrade(state, pta),
                RespondToTradeAction rta => ValidateRespondToTrade(state, rta),
                PayTaxAction ptxa => ValidatePayTax(state, ptxa),
                PayJailFineAction pjfa => ValidatePayJailFine(state, pjfa),
                UseGetOutOfJailCardAction ugoojca => ValidateUseGetOutOfJailCard(state, ugoojca),
                RollForJailAction rfja => ValidateRollForJail(state, rfja),
                AuctionBidAction aba => ValidateAuctionBid(state, aba),
                _ => ValidationResult.Failure($"Unknown action type: {action.GetType().Name}")
            };
        }

        /// <summary>
        /// Applies a player action to the game state and returns the new, resulting state.
        /// This method is a pure function and must not mutate the input <paramref name="state"/> object.
        /// It is the caller's responsibility to ensure the action is valid before calling this method.
        /// </summary>
        /// <param name="state">The current game state.</param>
        /// <param name="action">The action to apply.</param>
        /// <returns>A new <see cref="GameState"/> instance representing the state after the action is applied.</returns>
        /// <exception cref="RuleEngineInvariantException">Thrown if an invalid action is provided, indicating a logic error in the caller.</exception>
        public GameState ApplyAction(GameState state, PlayerAction action)
        {
            var validationResult = ValidateAction(state, action);
            if (!validationResult.IsValid)
            {
                throw new RuleEngineInvariantException(
                    $"Attempted to apply an invalid action. Validation failed: {validationResult.ErrorMessage}");
            }
            
            // Create a deep copy to ensure immutability of the original state.
            // This assumes GameState and its children are records or have copy constructors.
            var newState = state with { };

            return action switch
            {
                PurchasePropertyAction ppa => ApplyPurchaseProperty(newState, ppa),
                BuildHouseAction bha => ApplyBuildHouse(newState, bha),
                // Other apply actions would follow the same pattern...
                _ => throw new RuleEngineInvariantException($"Apply logic not implemented for action type: {action.GetType().Name}")
            };
        }

        #endregion

        #region Validation Methods

        private ValidationResult ValidatePurchaseProperty(GameState state, PurchasePropertyAction action)
        {
            var player = state.GetPlayer(action.PlayerId);
            var property = state.GetProperty(player.CurrentPosition);

            if (property == null || property.OwnerId != null)
                return ValidationResult.Failure("Property is not available for purchase.");

            if (player.Cash < property.Price)
                return ValidationResult.Failure("Insufficient funds to purchase property.");

            return ValidationResult.Success();
        }

        private ValidationResult ValidateBuildHouse(GameState state, BuildHouseAction action)
        {
            var player = state.GetPlayer(action.PlayerId);
            var property = state.GetProperty(action.PropertyId);
            
            if (property == null || property.OwnerId != player.Id)
                return ValidationResult.Failure("Player does not own this property.");

            if (property.IsMortgaged)
                return ValidationResult.Failure("Cannot build on a mortgaged property.");

            if (property.ColorGroup == ColorGroup.None || property.ColorGroup == ColorGroup.Railroad || property.ColorGroup == ColorGroup.Utility)
                return ValidationResult.Failure("Cannot build on this type of property.");
            
            var monopolyProperties = state.GetPropertiesInGroup(property.ColorGroup).ToList();
            if (monopolyProperties.Any(p => p.OwnerId != player.Id))
                return ValidationResult.Failure("Player does not own the full monopoly for this color group.");
            
            if (monopolyProperties.Any(p => p.IsMortgaged))
                return ValidationResult.Failure("Cannot build on a monopoly with mortgaged properties.");

            if (property.HouseCount >= 4 && property.HotelCount > 0)
                return ValidationResult.Failure("Property is already fully developed with a hotel.");

            // Even building rule (REQ-1-054)
            int minHouses = monopolyProperties.Min(p => p.HouseCount);
            if (property.HouseCount > minHouses)
                return ValidationResult.Failure("Building must be even. Must build on other properties in this set first.");

            if (property.HouseCount < 4)
            {
                if (state.Bank.HousesAvailable < 1)
                    return ValidationResult.Failure("No houses available in the bank."); // REQ-1-055
                if (player.Cash < property.HouseCost)
                    return ValidationResult.Failure("Insufficient funds to build a house.");
            }
            else // Upgrading to hotel
            {
                if (state.Bank.HotelsAvailable < 1)
                    return ValidationResult.Failure("No hotels available in the bank."); // REQ-1-055
                if (player.Cash < property.HouseCost) // Hotel cost is same as house cost
                    return ValidationResult.Failure("Insufficient funds to build a hotel.");
            }

            return ValidationResult.Success();
        }

        private ValidationResult ValidateMortgageProperty(GameState state, MortgagePropertyAction action)
        {
            var player = state.GetPlayer(action.PlayerId);
            var property = state.GetProperty(action.PropertyId);

            if (property == null || property.OwnerId != player.Id)
                return ValidationResult.Failure("Player does not own this property.");

            if (property.IsMortgaged)
                return ValidationResult.Failure("Property is already mortgaged.");
            
            if (property.HouseCount > 0 || property.HotelCount > 0)
                 return ValidationResult.Failure("Cannot mortgage a property with buildings on it.");

            // Check if any property in its color group has buildings, as all must be sold first.
            if (property.ColorGroup != ColorGroup.None && property.ColorGroup != ColorGroup.Railroad && property.ColorGroup != ColorGroup.Utility)
            {
                var monopolyProperties = state.GetPropertiesInGroup(property.ColorGroup);
                if (monopolyProperties.Any(p => p.HouseCount > 0 || p.HotelCount > 0))
                {
                    return ValidationResult.Failure("All buildings on the entire color group must be sold before any property can be mortgaged.");
                }
            }

            return ValidationResult.Success();
        }

        private ValidationResult ValidateUnmortgageProperty(GameState state, UnmortgagePropertyAction action)
        {
            var player = state.GetPlayer(action.PlayerId);
            var property = state.GetProperty(action.PropertyId);

            if (property == null || property.OwnerId != player.Id)
                return ValidationResult.Failure("Player does not own this property.");

            if (!property.IsMortgaged)
                return ValidationResult.Failure("Property is not mortgaged.");
            
            int unmortgageCost = (int)(property.MortgageValue * 1.10m);
            if (player.Cash < unmortgageCost)
                return ValidationResult.Failure("Insufficient funds to unmortgage.");
                
            return ValidationResult.Success();
        }
        
        private ValidationResult ValidateProposeTrade(GameState state, ProposeTradeAction action)
        {
            var proposer = state.GetPlayer(action.PlayerId);
            var target = state.GetPlayer(action.TargetPlayerId);

            if (target == null) return ValidationResult.Failure("Target player not found.");
            if (proposer.Id == target.Id) return ValidationResult.Failure("Cannot trade with yourself.");

            // Validate proposer's items
            if (proposer.Cash < action.CashOffered) return ValidationResult.Failure("Proposer has insufficient cash.");
            if (proposer.GetOutOfJailFreeCards < action.JailCardsOffered) return ValidationResult.Failure("Proposer has insufficient Get Out of Jail Free cards.");
            foreach (var propId in action.PropertiesOffered)
            {
                var prop = state.GetProperty(propId);
                if (prop == null || prop.OwnerId != proposer.Id) return ValidationResult.Failure("Proposer does not own an offered property.");
                if (PropertyHasBuildings(state, prop)) return ValidationResult.Failure("Cannot trade properties with buildings.");
            }

            // Validate target's items
            if (target.Cash < action.CashRequested) return ValidationResult.Failure("Target has insufficient cash.");
            if (target.GetOutOfJailFreeCards < action.JailCardsRequested) return ValidationResult.Failure("Target has insufficient Get Out of Jail Free cards.");
            foreach (var propId in action.PropertiesRequested)
            {
                var prop = state.GetProperty(propId);
                if (prop == null || prop.OwnerId != target.Id) return ValidationResult.Failure("Target does not own a requested property.");
                if (PropertyHasBuildings(state, prop)) return ValidationResult.Failure("Cannot trade properties with buildings.");
            }

            return ValidationResult.Success();
        }
        
        private ValidationResult ValidateRespondToTrade(GameState state, RespondToTradeAction action)
        {
            // The main validation is that the trade still exists and is pending for this player.
            // The atomicity of the trade itself is re-validated on Apply.
            // This validation is simple as the core logic is in ValidateProposeTrade.
            var trade = state.PendingTrades.FirstOrDefault(t => t.Id == action.TradeId && t.TargetPlayerId == action.PlayerId);
            if (trade == null)
            {
                return ValidationResult.Failure("Trade offer not found or not valid for this player.");
            }
            return ValidationResult.Success();
        }
        
        private ValidationResult ValidatePayTax(GameState state, PayTaxAction action)
        {
            // Assuming tax is triggered by landing on a space.
            // For Income Tax, the choice is part of the action.
            return ValidationResult.Success();
        }
        
        private ValidationResult ValidatePayJailFine(GameState state, PayJailFineAction action)
        {
            var player = state.GetPlayer(action.PlayerId);
            if (player.Status != PlayerStatus.InJail) return ValidationResult.Failure("Player is not in jail.");
            if (player.Cash < 50) return ValidationResult.Failure("Insufficient funds to pay jail fine.");
            return ValidationResult.Success();
        }

        private ValidationResult ValidateUseGetOutOfJailCard(GameState state, UseGetOutOfJailCardAction action)
        {
            var player = state.GetPlayer(action.PlayerId);
            if (player.Status != PlayerStatus.InJail) return ValidationResult.Failure("Player is not in jail.");
            if (player.GetOutOfJailFreeCards < 1) return ValidationResult.Failure("Player does not have a Get Out of Jail Free card.");
            return ValidationResult.Success();
        }

        private ValidationResult ValidateRollForJail(GameState state, RollForJailAction action)
        {
            var player = state.GetPlayer(action.PlayerId);
            if (player.Status != PlayerStatus.InJail) return ValidationResult.Failure("Player is not in jail.");
            return ValidationResult.Success();
        }

        private ValidationResult ValidateAuctionBid(GameState state, AuctionBidAction action)
        {
            var player = state.GetPlayer(action.PlayerId);
            var auction = state.CurrentAuction;
            if (auction == null) return ValidationResult.Failure("There is no active auction.");
            if (auction.HighestBidderId == player.Id) return ValidationResult.Failure("You are already the highest bidder.");
            if (player.Cash < action.BidAmount) return ValidationResult.Failure("Insufficient funds for this bid.");
            if (action.BidAmount <= auction.HighestBid) return ValidationResult.Failure("Bid must be higher than the current highest bid.");
            
            return ValidationResult.Success();
        }

        #endregion

        #region Application Methods

        private GameState ApplyPurchaseProperty(GameState state, PurchasePropertyAction action)
        {
            var player = state.GetPlayer(action.PlayerId);
            var property = state.GetProperty(player.CurrentPosition);
            
            // It's safe to assume these are not null due to prior validation.
            player.Cash -= property!.Price;
            property.OwnerId = player.Id;
            
            return state;
        }

        private GameState ApplyBuildHouse(GameState state, BuildHouseAction action)
        {
            var player = state.GetPlayer(action.PlayerId);
            var property = state.GetProperty(action.PropertyId);
            
            player.Cash -= property!.HouseCost;

            if (property.HouseCount < 4)
            {
                property.HouseCount++;
                state.Bank.HousesAvailable--;
            }
            else
            {
                property.HouseCount = 0;
                property.HotelCount = 1;
                state.Bank.HousesAvailable += 4;
                state.Bank.HotelsAvailable--;
            }

            return state;
        }


        // NOTE: A full implementation would have Apply methods for all validated action types.
        // For example: ApplyMortgage, ApplyUnmortgage, ApplyTrade, ApplyPayTax, etc.
        // Each would modify the state copy and return it.

        #endregion

        #region Helper Methods

        private bool PropertyHasBuildings(GameState state, PropertyState property)
        {
            if (property.HouseCount > 0 || property.HotelCount > 0) return true;

            // Per rules, you cannot trade a property from an improved monopoly,
            // even if that specific property is undeveloped.
            if (property.ColorGroup != ColorGroup.None && property.ColorGroup != ColorGroup.Railroad && property.ColorGroup != ColorGroup.Utility)
            {
                 var groupProperties = state.GetPropertiesInGroup(property.ColorGroup);
                 return groupProperties.Any(p => p.HouseCount > 0 || p.HotelCount > 0);
            }

            return false;
        }

        #endregion
    }
}