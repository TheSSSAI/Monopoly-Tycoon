using MonopolyTycoon.Application.Abstractions;
using MonopolyTycoon.Application.DataObjects;
using MonopolyTycoon.Presentation.Events;
using MonopolyTycoon.Presentation.Features.GameBoard.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VContainer.Unity;
using UnityEngine;

namespace MonopolyTycoon.Presentation.Features.GameBoard.Presenters
{
    public class GameBoardPresenter : IInitializable, IDisposable
    {
        private readonly IGameBoardView _view;
        private readonly IEventBus _eventBus;
        private readonly IGameSessionService _gameSessionService;

        private GameStateDTO _lastGameState;

        public GameBoardPresenter(IGameBoardView view, IEventBus eventBus, IGameSessionService gameSessionService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _gameSessionService = gameSessionService ?? throw new ArgumentNullException(nameof(gameSessionService));
        }

        public void Initialize()
        {
            _eventBus.Subscribe<GameStateUpdatedEvent>(OnGameStateUpdated);
            var initialGameState = _gameSessionService.GetCurrentGameState();
            if (initialGameState != null)
            {
                InitializeBoard(initialGameState);
            }
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<GameStateUpdatedEvent>(OnGameStateUpdated);
        }

        private void InitializeBoard(GameStateDTO gameState)
        {
            _view.InitializeBoard(gameState.Players.Count);
            
            foreach (var player in gameState.Players)
            {
                _view.CreatePlayerToken(player.Id, player.TokenId, player.CurrentPosition);
            }

            foreach (var property in gameState.Board.Properties)
            {
                if (property.OwnerId != null)
                {
                    _view.UpdatePropertyOwnership(property.Position, property.OwnerId);
                }
                _view.UpdatePropertyDevelopment(property.Position, property.DevelopmentLevel);
            }

            _lastGameState = gameState;
        }

        private async void OnGameStateUpdated(GameStateUpdatedEvent e)
        {
            var newState = e.NewState;

            if (_lastGameState == null)
            {
                InitializeBoard(newState);
                return;
            }

            // REQ-1-017: The system shall implement interactive graphical elements to provide visual feedback...
            // This includes animated token movement, visual highlighting of selected properties, and visual effects...
            await ProcessPlayerMovements(newState);
            ProcessPropertyChanges(newState);
            
            _lastGameState = newState;
        }

        private async Task ProcessPlayerMovements(GameStateDTO newState)
        {
            for (int i = 0; i < newState.Players.Count; i++)
            {
                var newPlayerState = newState.Players[i];
                var oldPlayerState = _lastGameState.Players.Find(p => p.Id == newPlayerState.Id);

                if (oldPlayerState != null && oldPlayerState.CurrentPosition != newPlayerState.CurrentPosition)
                {
                    // REQ-1-017: ...animated token movement...
                    await _view.AnimateTokenMovementAsync(newPlayerState.Id, newPlayerState.CurrentPosition);
                }
            }
        }
        
        private void ProcessPropertyChanges(GameStateDTO newState)
        {
            foreach (var newProperty in newState.Board.Properties)
            {
                var oldProperty = _lastGameState.Board.Properties.Find(p => p.Position == newProperty.Position);
                if (oldProperty == null) continue;

                // Check for ownership change
                if (oldProperty.OwnerId != newProperty.OwnerId)
                {
                    // REQ-1-072: The system shall visually represent property ownership directly on the game board.
                    _view.UpdatePropertyOwnership(newProperty.Position, newProperty.OwnerId);
                }

                // Check for development level change (houses/hotels)
                if (oldProperty.DevelopmentLevel != newProperty.DevelopmentLevel)
                {
                    _view.UpdatePropertyDevelopment(newProperty.Position, newProperty.DevelopmentLevel);
                }

                // REQ-1-057: ...prevent rent collection on any mortgaged property.
                // The visual representation for mortgage status is part of the ownership update.
                if (oldProperty.IsMortgaged != newProperty.IsMortgaged)
                {
                     // Ownership view should handle showing mortgage status
                    _view.UpdatePropertyOwnership(newProperty.Position, newProperty.OwnerId, newProperty.IsMortgaged);
                }
            }
        }
    }
}