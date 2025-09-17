using System;
using System.Threading.Tasks;
using MonopolyTycoon.Application.Abstractions;
using MonopolyTycoon.Presentation.Core;
using MonopolyTycoon.Presentation.Features.MainMenu.Views;
using VContainer;
using MonopolyTycoon.Domain.Objects;

namespace MonopolyTycoon.Presentation.Features.MainMenu.Presenters
{
    public class MainMenuPresenter : IDisposable
    {
        private readonly IMainMenuView view;
        private readonly IViewManager viewManager;
        private readonly IGameSessionService gameSessionService;
        private readonly IPlayerProfileRepository playerProfileRepository;

        [Inject]
        public MainMenuPresenter(IMainMenuView view, IViewManager viewManager, IGameSessionService gameSessionService, IPlayerProfileRepository playerProfileRepository)
        {
            this.view = view;
            this.viewManager = viewManager;
            this.gameSessionService = gameSessionService;
            this.playerProfileRepository = playerProfileRepository;
        }

        public void Initialize()
        {
            view.OnNewGameClicked += HandleNewGameClicked;
            view.OnLoadGameClicked += HandleLoadGameClicked;
            view.OnSettingsClicked += HandleSettingsClicked;
            view.OnQuitClicked += HandleQuitClicked;
            view.OnPlayerNameChanged += HandlePlayerNameChanged;

            // Set initial state
            view.SetPlayerName("");
            view.SetNewGameButtonInteractable(false);
        }

        private void HandlePlayerNameChanged(string newName)
        {
            // REQ-1-032: The display name input must be validated to be between 3 and 16 characters long and must not contain special characters.
            var validationResult = PlayerProfile.ValidateDisplayName(newName);
            view.SetNewGameButtonInteractable(validationResult.IsValid);
            if (validationResult.IsValid)
            {
                view.HideValidationError();
            }
            else
            {
                view.ShowValidationError(validationResult.ErrorMessage);
            }
        }

        private async void HandleNewGameClicked()
        {
            var playerName = view.GetPlayerName().Trim();
            var validationResult = PlayerProfile.ValidateDisplayName(playerName);

            if (!validationResult.IsValid)
            {
                view.ShowValidationError(validationResult.ErrorMessage);
                return;
            }

            await StartNewGameAsync(playerName);
        }

        private async Task StartNewGameAsync(string playerName)
        {
            // This would normally be a more complex flow involving a dedicated game setup screen.
            // For now, we create a default game setup.
            try
            {
                await viewManager.ShowLoadingScreenAsync("Creating new game...");

                // Get or create player profile
                var profile = await playerProfileRepository.GetOrCreateProfileAsync(playerName);
                
                // Create default game setup options. In a full implementation, these would be gathered from the setup screen UI.
                var gameSetup = new GameSetupOptions
                {
                    PlayerProfileId = profile.Id,
                    HumanPlayerName = profile.DisplayName,
                    HumanPlayerTokenId = "token.classic.car", // Default token
                    AIPlayers = new System.Collections.Generic.List<AISetupOptions>
                    {
                        new AISetupOptions { Name = "AI-Bot 1", Difficulty = AIDifficulty.Medium }
                    }
                };
                
                await gameSessionService.StartNewGameAsync(gameSetup);

                await viewManager.LoadSceneAsync("GameBoardScene");
            }
            catch (Exception ex)
            {
                // In a real scenario, we would show a specific error dialog
                Debug.LogError($"Failed to start new game: {ex.Message}");
                await viewManager.ShowErrorDialogAsync("Failed to Start Game", "An unexpected error occurred while creating the new game. Please check the logs for more details.");
            }
            finally
            {
                await viewManager.HideLoadingScreenAsync();
            }
        }

        private async void HandleLoadGameClicked()
        {
            await viewManager.ShowViewAsync<LoadGamePresenter>("LoadGameView");
        }

        private async void HandleSettingsClicked()
        {
            await viewManager.ShowViewAsync<object>("SettingsView"); // Assuming a generic view for settings
        }

        private void HandleQuitClicked()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }

        public void Dispose()
        {
            view.OnNewGameClicked -= HandleNewGameClicked;
            view.OnLoadGameClicked -= HandleLoadGameClicked;
            view.OnSettingsClicked -= HandleSettingsClicked;
            view.OnQuitClicked -= HandleQuitClicked;
            view.OnPlayerNameChanged -= HandlePlayerNameChanged;
        }
    }
}