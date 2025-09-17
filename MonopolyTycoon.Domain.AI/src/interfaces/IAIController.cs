//-----------------------------------------------------------------------
// <copyright file="IAIController.cs" company="MonopolyTycoon">
//     Copyright (c) MonopolyTycoon. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using MonopolyTycoon.Domain.Core.Events;
using MonopolyTycoon.Domain.Core.ValueObjects;
using MonopolyTycoon.Domain.Entities;

namespace MonopolyTycoon.Domain.AI.Interfaces
{
    /// <summary>
    /// Defines the public contract for the AI decision-making module.
    /// This interface provides a stable entry point for the Application Layer to request
    /// strategic actions from an AI player based on the current game state.
    /// </summary>
    /// <remarks>
    /// Implementations of this interface are expected to be stateless and thread-safe.
    /// They must not modify the input GameState object. The returned actions are proposals
    /// to be validated and executed by the Application Layer.
    /// </remarks>
    public interface IAIController
    {
        /// <summary>
        /// Evaluates the current game state during an AI's turn and determines the next best action.
        /// This is the primary method for proactive AI turn execution.
        /// </summary>
        /// <param name="state">The current, read-only state of the entire game.</param>
        /// <param name="aiPlayerId">The unique identifier of the AI player whose turn it is.</param>
        /// <param name="parameters">The configuration object defining the AI's strategy and difficulty.</param>
        /// <returns>A <see cref="PlayerAction"/> object representing the decided action. This is a proposal, not an executed action.</returns>
        /// <remarks>
        /// This method MUST NOT throw exceptions. In case of any internal error, it must log the
        /// error and return a safe default action (e.g., EndTurnAction) to prevent the game from stalling.
        /// The implementation must not modify the provided <paramref name="state"/>.
        /// </remarks>
        PlayerAction GetNextAction(GameState state, Guid aiPlayerId, AIParameters parameters);

        /// <summary>
        /// Evaluates a trade proposal received from another player and returns a decision.
        /// This method is invoked reactively when another player targets this AI for a trade.
        /// </summary>
        /// <param name="state">The current, read-only state of the entire game.</param>
        /// <param name="aiPlayerId">The unique identifier of the AI player evaluating the offer.</param>
        /// <param name="parameters">The configuration object defining the AI's strategy and difficulty.</param>
        /// <param name="offer">The trade offer being proposed to the AI player.</param>
        /// <returns>A <see cref="TradeDecision"/> enum indicating whether the AI accepts or declines the offer.</returns>
        /// <remarks>
        /// This method MUST NOT throw exceptions. It should handle all internal errors gracefully
        /// and return a safe default decision (e.g., Decline) on failure.
        /// </remarks>
        TradeDecision EvaluateTrade(GameState state, Guid aiPlayerId, AIParameters parameters, TradeOffer offer);

        /// <summary>
        /// Determines the AI's next move in a property auction.
        /// This method is invoked by the auction orchestrator when it is the AI's turn to bid.
        /// </summary>
        /// <param name="state">The current, read-only state of the entire game.</param>
        /// <param name="aiPlayerId">The unique identifier of the AI player who is bidding.</param>
        /// <param name="parameters">The configuration object defining the AI's strategy and difficulty.</param>
        /// <param name="auctionState">The current state of the ongoing auction.</param>
        /// <returns>A <see cref="BidDecision"/> object representing the AI's choice to bid a certain amount or to pass.</returns>
        /// <remarks>
        /// This method MUST NOT throw exceptions. It should handle all internal errors gracefully
        /// and return a safe default decision (e.g., Pass) on failure. The implementation must
        /// ensure the AI does not bid more cash than it currently possesses.
        /// </remarks>
        BidDecision GetAuctionBid(GameState state, Guid aiPlayerId, AIParameters parameters, AuctionState auctionState);
    }
}