using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MonopolyTycoon.Presentation.Shared.Events;
using MonopolyTycoon.Application.Abstractions;
using UnityEngine;
using UnityEngine.SceneManagement;
using MonopolyTycoon.Presentation.Shared.Views;
using Zenject;

namespace MonopolyTycoon.Presentation.Shared.Services
{
    public class ViewManager : IViewManager
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IInstantiator _instantiator;
        private readonly Dictionary<Screen, GameObject> _loadedScreens = new();
        private readonly Stack<Screen> _screenStack = new();

        private GameObject _activeScreen;
        private LoadingSpinnerView _loadingSpinner;

        public ViewManager(IAssetProvider assetProvider, IInstantiator instantiator)
        {
            _assetProvider = assetProvider ?? throw new ArgumentNullException(nameof(assetProvider));
            _instantiator = instantiator ?? throw new ArgumentNullException(nameof(instantiator));
        }

        public async Task ShowScreen(Screen screen, object payload = null)
        {
            if (_activeScreen != null)
            {
                _activeScreen.SetActive(false);
            }

            if (!_loadedScreens.TryGetValue(screen, out var screenInstance))
            {
                var screenPrefab = await _assetProvider.LoadAssetAsync<GameObject>(screen.ToString());
                if (screenPrefab == null)
                {
                    Debug.LogError($"[ViewManager] Failed to load prefab for screen: {screen}");
                    // Potentially show an error dialog here
                    return;
                }
                screenInstance = _instantiator.InstantiatePrefab(screenPrefab);
                _loadedScreens[screen] = screenInstance;
            }

            _activeScreen = screenInstance;
            _activeScreen.SetActive(true);

            // This is a simplified navigation stack. A more robust implementation might be needed.
            if (_screenStack.Count == 0 || _screenStack.Peek() != screen)
            {
                _screenStack.Push(screen);
            }
        }

        public async Task ShowDialog(DialogDefinition definition)
        {
            var dialogPrefab = await _assetProvider.LoadAssetAsync<GameObject>("ModalDialogView");
            if (dialogPrefab == null)
            {
                Debug.LogError("[ViewManager] ModalDialogView prefab not found!");
                return;
            }

            var dialogInstance = _instantiator.InstantiatePrefabForComponent<ModalDialogView>(dialogPrefab);
            
            var tcs = new TaskCompletionSource<bool>();

            Action onConfirm = () =>
            {
                if (!tcs.Task.IsCompleted) tcs.SetResult(true);
                GameObject.Destroy(dialogInstance.gameObject);
            };
            
            Action onCancel = () =>
            {
                if (!tcs.Task.IsCompleted) tcs.SetResult(false);
                GameObject.Destroy(dialogInstance.gameObject);
            };

            dialogInstance.Configure(definition, onConfirm, onCancel);
            
            await tcs.Task;
        }

        public async Task ShowLoadingSpinner()
        {
            if (_loadingSpinner != null)
            {
                _loadingSpinner.Show();
                return;
            }

            var spinnerPrefab = await _assetProvider.LoadAssetAsync<GameObject>("LoadingSpinnerView");
            if (spinnerPrefab == null)
            {
                Debug.LogError("[ViewManager] LoadingSpinnerView prefab not found!");
                return;
            }

            _loadingSpinner = _instantiator.InstantiatePrefabForComponent<LoadingSpinnerView>(spinnerPrefab);
            _loadingSpinner.Show();
        }

        public void HideLoadingSpinner()
        {
            if (_loadingSpinner != null)
            {
                _loadingSpinner.Hide();
            }
        }

        public async Task GoBack()
        {
            if (_screenStack.Count > 1)
            {
                // Pop current screen
                _screenStack.Pop();
                // Get previous screen
                var previousScreen = _screenStack.Peek();
                // Show it without pushing to the stack again
                await ShowScreenInternal(previousScreen);
            }
            else
            {
                Debug.LogWarning("[ViewManager] No screen to go back to.");
            }
        }

        private async Task ShowScreenInternal(Screen screen)
        {
             if (_activeScreen != null)
            {
                _activeScreen.SetActive(false);
            }

            if (!_loadedScreens.TryGetValue(screen, out var screenInstance))
            {
                var screenPrefab = await _assetProvider.LoadAssetAsync<GameObject>(screen.ToString());
                if (screenPrefab == null)
                {
                    Debug.LogError($"[ViewManager] Failed to load prefab for screen: {screen}");
                    return;
                }
                screenInstance = _instantiator.InstantiatePrefab(screenPrefab);
                _loadedScreens[screen] = screenInstance;
            }

            _activeScreen = screenInstance;
            _activeScreen.SetActive(true);
        }
    }
}