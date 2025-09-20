using MonopolyTycoon.Presentation.Shared.Services;
using System;
using UniRx;
using VContainer.Unity;
using UnityEngine;
using MonopolyTycoon.Presentation.Features.MainMenu.Views;

namespace MonopolyTycoon.Presentation.Features.MainMenu.Presenters
{
    public class MainMenuPresenter : IInitializable, IDisposable
    {
        private readonly IMainMenuView _view;
        private readonly IViewManager _viewManager;
        private readonly CompositeDisposable _disposables = new();

        public MainMenuPresenter(IMainMenuView view, IViewManager viewManager)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _viewManager = viewManager ?? throw new ArgumentNullException(nameof(viewManager));
        }

        public void Initialize()
        {
            _view.OnNewGameClicked.Subscribe(_ => OnNewGame()).AddTo(_disposables);
            _view.OnLoadGameClicked.Subscribe(_ => OnLoadGame()).AddTo(_disposables);
            _view.OnSettingsClicked.Subscribe(_ => OnSettings()).AddTo(_disposables);
            _view.OnQuitClicked.Subscribe(_ => OnQuit()).AddTo(_disposables);
        }

        private void OnNewGame()
        {
            // US-008: Start a new game from the main menu
            _viewManager.ShowScreenAsync(Screen.GameSetup).Forget();
        }

        private void OnLoadGame()
        {
            // US-062: Load a game from a previously saved slot
            _viewManager.ShowScreenAsync(Screen.LoadGame).Forget();
        }

        private void OnSettings()
        {
            _viewManager.ShowScreenAsync(Screen.Settings).Forget();
        }

        private void OnQuit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}