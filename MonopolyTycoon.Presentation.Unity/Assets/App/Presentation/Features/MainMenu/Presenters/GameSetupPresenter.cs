using MonopolyTycoon.Application.Abstractions;
using MonopolyTycoon.Application.DataObjects;
using MonopolyTycoon.Presentation.Shared.Services;
using System;
using System.Text.RegularExpressions;
using UniRx;
using VContainer.Unity;

namespace MonopolyTycoon.Presentation.Features.MainMenu.Presenters
{
    public class GameSetupPresenter : IInitializable, IDisposable
    {
        private readonly IGameSetupView _view;
        private readonly IViewManager _viewManager;
        private readonly IGameSessionService _gameSessionService;
        private readonly CompositeDisposable _disposables = new();

        private GameSetupOptionsDTO _setupOptions = new();

        public GameSetupPresenter(IGameSetupView view, IViewManager viewManager, IGameSessionService gameSessionService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _viewManager = viewManager ?? throw new ArgumentNullException(nameof(viewManager));
            _gameSessionService = gameSessionService ?? throw new ArgumentNullException(nameof(gameSessionService));
        }

        public void Initialize()
        {
            _view.OnPlayerNameChanged.Subscribe(HandlePlayerNameChange).AddTo(_disposables);
            _view.OnAiCountChanged.Subscribe(HandleAiCountChange).AddTo(_disposables);
            _view.OnTokenSelected.Subscribe(HandleTokenSelection).AddTo(_disposables);
            _view.OnAiDifficultyChanged.Subscribe(HandleAiDifficultyChange).AddTo(_disposables);
            _view.OnStartGameClicked.Subscribe(_ => HandleStartGame()).AddTo(_disposables);
            _view.OnBackClicked.Subscribe(_ => _viewManager.ShowScreenAsync(Screen.MainMenu).Forget()).AddTo(_disposables);

            // Initialize view with default options
            _view.UpdateAIConfigDisplay(_setupOptions.AIPlayers.Count);
            ValidateAndRefresh();
        }

        private void HandlePlayerNameChange(string name)
        {
            _setupOptions.PlayerName = name.Trim();
            ValidateAndRefresh();
        }

        private void HandleAiCountChange(int count)
        {
            while (_setupOptions.AIPlayers.Count < count)
            {
                _setupOptions.AIPlayers.Add(new AIOpponentOptionsDTO());
            }
            while (_setupOptions.AIPlayers.Count > count)
            {
                _setupOptions.AIPlayers.RemoveAt(_setupOptions.AIPlayers.Count - 1);
            }
            _view.UpdateAIConfigDisplay(count);
            ValidateAndRefresh();
        }

        private void HandleTokenSelection(string tokenId)
        {
            _setupOptions.PlayerTokenId = tokenId;
            ValidateAndRefresh();
        }

        private void HandleAiDifficultyChange((int index, AIDifficulty difficulty) data)
        {
            if (data.index >= 0 && data.index < _setupOptions.AIPlayers.Count)
            {
                _setupOptions.AIPlayers[data.index].Difficulty = data.difficulty;
            }
            ValidateAndRefresh();
        }

        private void ValidateAndRefresh()
        {
            // REQ-1-032: The display name input must be validated to be between 3 and 16 characters long and must not contain special characters.
            bool isNameValid = _setupOptions.PlayerName.Length >= 3 &&
                               _setupOptions.PlayerName.Length <= 16 &&
                               Regex.IsMatch(_setupOptions.PlayerName, "^[a-zA-Z0-9]*$");

            bool isTokenValid = !string.IsNullOrEmpty(_setupOptions.PlayerTokenId);

            bool canStart = isNameValid && isTokenValid;

            _view.SetStartButtonEnabled(canStart);
            if (!isNameValid && !string.IsNullOrWhiteSpace(_setupOptions.PlayerName))
            {
                _view.ShowValidationError("Name must be 3-16 alphanumeric characters.");
            }
            else
            {
                _view.HideValidationError();
            }
        }

        private async void HandleStartGame()
        {
            ValidateAndRefresh();
            if (!_view.IsStartButtonEnabled) return;

            _view.SetStartButtonEnabled(false);
            var result = await _gameSessionService.StartNewGameAsync(_setupOptions);

            if (result.IsSuccess)
            {
                await _viewManager.ShowScreenAsync(Screen.GameBoard);
            }
            else
            {
                // In a real app, show an error dialog
                UnityEngine.Debug.LogError($"Failed to start game: {result.ErrorMessage}");
                _view.SetStartButtonEnabled(true);
            }
        }
        
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}