using System;
using System.Collections.Generic;

namespace MonopolyTycoon.Presentation.Features.Trading.Views
{
    /// <summary>
    /// Defines the contract for the trading interface View.
    /// </summary>
    public interface ITradeView
    {
        #region Events

        /// <summary>
        /// Fired when the user submits a trade proposal they have constructed.
        /// </summary>
        event Action<TradeOfferViewModel> OnTradeProposed;

        /// <summary>
        /// Fired when the user accepts a trade offer presented by an AI.
        /// </summary>
        event Action OnTradeAccepted;

        /// <summary>
        /// Fired when the user declines a trade offer presented by an AI.
        /// </summary>
        event Action OnTradeDeclined;

        /// <summary>
        /// Fired when the user initiates a counter-offer to an AI's proposal.
        /// </summary>
        event Action OnCounterOfferInitiated;
        
        /// <summary>
        /// Fired when the user cancels the trading process and closes the view.
        /// </summary>
        event Action OnTradeCancelled;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes and displays the trading UI for a player-initiated trade.
        /// </summary>
        /// <param name="viewModel">Data model containing the initial state for the trade UI.</param>
        void ShowForPlayerInitiation(TradeInitiationViewModel viewModel);
        
        /// <summary>
        /// Displays and populates the trading UI in response to an AI's trade proposal.
        /// </summary>
        /// <param name="viewModel">Data model representing the AI's offer.</param>
        void ShowForAIProposal(AIProposalViewModel viewModel);

        /// <summary>
        /// Transitions the UI from viewing an AI proposal to an editable state for a counter-offer.
        /// </summary>
        void EnterCounterOfferMode();

        #endregion
    }

    #region ViewModels

    public class TradeAssetViewModel
    {
        public List<int> PropertyIds { get; set; } = new();
        public int CashAmount { get; set; }
        public int GetOutOfJailFreeCards { get; set; }
    }

    public class TradeOfferViewModel
    {
        public string TargetPlayerId { get; set; }
        public TradeAssetViewModel PlayerOffer { get; set; }
        public TradeAssetViewModel OpponentOffer { get; set; }
    }

    public class TradeInitiationViewModel
    {
        public string HumanPlayerName { get; set; }
        public List<string> AvailableOpponentNames { get; set; }
        // Further properties to list available assets for trade would be included here.
    }

    public class AIProposalViewModel
    {
        public string ProposingAIName { get; set; }
        public TradeAssetViewModel AIOffer { get; set; }
        public TradeAssetViewModel PlayerRequest { get; set; }
    }

    #endregion
}