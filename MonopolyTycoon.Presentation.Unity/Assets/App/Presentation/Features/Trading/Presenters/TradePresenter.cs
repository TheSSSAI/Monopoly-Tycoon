using MonopolyTycoon.Application.Abstractions;
using MonopolyTycoon.Application.DataObjects;
using MonopolyTycoon.Presentation.Shared.Events;
using MonopolyTycoon.Presentation.Shared.Services;
using System;
using UniRx;
using VContainer.Unity;
using System.Linq;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace MonopolyTycoon.Presentation.Features.Trading.Presenters
{
    public class TradePresenter : IInitializable, IDisposable
    {
        private readonly ITradeView _view;
        private readonly ITradeOrchestrationService _tradeService;
        private readonly IGameSessionService _gameSessionService;
        private readonly IViewManager _viewManager;
        private readonly CompositeDisposable _disposables = new();

        private TradeViewModel _currentTrade;

        public TradePresenter(ITradeView view, ITradeOrchestrationService tradeService, IGameSessionService gameSessionService, IViewManager viewManager)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _tradeService = tradeService ?? throw new ArgumentNullException(nameof(tradeService));
            _gameSessionService = gameSessionService ?? throw new ArgumentNullException(nameof(gameSessionService));
            _viewManager = viewManager ?? throw new ArgumentNullException(nameof(viewManager));
        }

        public void Initialize()
        {
            // View event subscriptions
            _view.OnCancelClicked.Subscribe(_ => CloseTradeView()).AddTo(_disposables);
            _view.OnProposeClicked.Subscribe(HandleProposeTrade).AddTo(_disposables);
            _view.OnAcceptClicked.Subscribe(HandleAcceptTrade).AddTo(_disposables);
            _view.OnDeclineClicked.Subscribe(HandleDeclineTrade).AddTo(_disposables);
            _view.OnCounterOfferClicked.Subscribe(HandleCounterOffer).AddTo(_disposables);

            // TODO: In a full implementation, we'd subscribe to an event like `TradeInitiatedEvent`
            // For now, we assume another presenter will call a public method to start a trade.
        }
        
        // This would be called by another presenter, e.g., from a player list UI
        public void StartPlayerInitiatedTrade(string opponentId)
        {
            var gameState = _gameSessionService.GetCurrentGameState();
            var human = gameState.Players.First(p => p.IsHuman);
            var opponent = gameState.Players.First(p => p.PlayerId == opponentId);

            _currentTrade = new TradeViewModel
            {
                HumanPlayer = human,
                Opponent = opponent,
                AllProperties = gameState.BoardState.Properties,
                Mode = TradeMode.Proposing
            };

            _view.ShowTradeView(_currentTrade);
        }

        // This would be called by an event handler for AI-initiated trades
        public void DisplayAiOffer(TradeProposalDTO aiOffer)
        {
            var gameState = _gameSessionService.GetCurrentGameState();
            var human = gameState.Players.First(p => p.IsHuman);
            var opponent = gameState.Players.First(p => p.PlayerId == aiOffer.ProposingPlayerId);
            
            _currentTrade = new TradeViewModel
            {
                HumanPlayer = human,
                Opponent = opponent,
                AllProperties = gameState.BoardState.Properties,
                Mode = TradeMode.Responding,
                HumanOfferCash = aiOffer.RequestedCash,
                HumanOfferProperties = aiOffer.RequestedPropertyIds,
                HumanOfferGetOutOfJailCards = aiOffer.RequestedGetOutOfJailCards,
                OpponentOfferCash = aiOffer.OfferedCash,
                OpponentOfferProperties = aiOffer.OfferedPropertyIds,
                OpponentOfferGetOutOfJailCards = aiOffer.OfferedGetOutOfJailCards
            };
            
            _view.ShowTradeView(_currentTrade);
        }

        private async void HandleProposeTrade(TradeViewModel tradeData)
        {
            // US-040: Initiate a trade with an AI opponent
            var proposal = new TradeProposalDTO
            {
                ProposingPlayerId = tradeData.HumanPlayer.PlayerId,
                TargetPlayerId = tradeData.Opponent.PlayerId,
                OfferedPropertyIds = tradeData.HumanOfferProperties,
                OfferedCash = tradeData.HumanOfferCash,
                OfferedGetOutOfJailCards = tradeData.HumanOfferGetOutOfJailCards,
                RequestedPropertyIds = tradeData.OpponentOfferProperties,
                RequestedCash = tradeData.OpponentOfferCash,
                RequestedGetOutOfJailCards = tradeData.OpponentOfferGetOutOfJailCards
            };

            var result = await _tradeService.ProposeTradeAsync(proposal);
            _view.ShowTradeResult(result.IsAccepted, result.Message);
            await UniTask.Delay(2000); // Wait for user to see result
            CloseTradeView();
        }

        private async void HandleAcceptTrade()
        {
            // US-041: Respond to a trade offer from an AI opponent
            var result = await _tradeService.RespondToOfferAsync(true);
            _view.ShowTradeResult(result.IsAccepted, result.Message);
            await UniTask.Delay(2000);
            CloseTradeView();
        }

        private async void HandleDeclineTrade()
        {
            var result = await _tradeService.RespondToOfferAsync(false);
            // No need to show a big message for a simple decline
            CloseTradeView();
        }

        private void HandleCounterOffer()
        {
            // US-042: Propose a counter-offer
            if (_currentTrade == null) return;
            _currentTrade.Mode = TradeMode.Proposing; // Switch UI to proposing mode
            _view.ShowTradeView(_currentTrade); // Re-render view in the new mode
        }

        private void CloseTradeView()
        {
            _view.HideTradeView();
            _viewManager.HideScreenAsync(Screen.Trading).Forget(); // Assuming a dedicated screen
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}