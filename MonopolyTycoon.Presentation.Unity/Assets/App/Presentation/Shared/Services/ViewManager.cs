using MonopolyTycoon.Application.Abstractions;
using MonopolyTycoon.Presentation.Shared.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace MonopolyTycoon.Presentation.Shared.Services
{
    public class ViewManager : IViewManager
    {
        private readonly IObjectResolver _container;
        private readonly IAssetProvider _assetProvider;

        private readonly Dictionary<Screen, GameObject> _activeScreens = new();
        private readonly Dictionary<string, Screen> _sceneToScreenMap = new()
        {
            { "MainMenu", Screen.MainMenu },
            { "GameBoard", Screen.GameBoard }
        };

        private Screen _currentScreen = Screen.None;

        public ViewManager(IObjectResolver container, IAssetProvider assetProvider)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _assetProvider = assetProvider ?? throw new ArgumentNullException(nameof(assetProvider));
        }

        public async Task ShowScreen(Screen screen, object payload = null)
        {
            if (_currentScreen != Screen.None && _currentScreen != Screen.MainMenu && _activeScreens.TryGetValue(_currentScreen, out var currentView))
            {
                // For now, we destroy the old view. A more complex system might use a stack.
                GameObject.Destroy(currentView);
                _activeScreens.Remove(_currentScreen);
            }
            else if (_sceneToScreenMap.ContainsValue(screen))
            {
                // Handle scene transitions
                string sceneName = GetSceneNameForScreen(screen);
                if (SceneManager.GetActiveScene().name != sceneName)
                {
                    await LoadSceneAsync(sceneName);
                    _currentScreen = screen;
                    return;
                }
            }

            string assetKey = GetAssetKeyForScreen(screen);
            var viewPrefab = await _assetProvider.LoadAssetAsync<GameObject>(assetKey);
            
            if (viewPrefab == null)
            {
                Debug.LogError($"[ViewManager] Could not load prefab for screen: {screen} with key: {assetKey}");
                return;
            }

            var viewInstance = _container.Instantiate(viewPrefab);
            _activeScreens[screen] = viewInstance;
            _currentScreen = screen;
        }

        public void HideScreen(Screen screen)
        {
            if (_activeScreens.TryGetValue(screen, out var viewInstance))
            {
                GameObject.Destroy(viewInstance);
                _activeScreens.Remove(screen);
                // In a stack-based system, we would show the previous screen here.
            }
        }

        public async Task<DialogResult> ShowDialog(DialogDefinition definition, Action<DialogResult> callback = null)
        {
            var tcs = new TaskCompletionSource<DialogResult>();

            Action<DialogResult> internalCallback = result =>
            {
                callback?.Invoke(result);
                tcs.SetResult(result);
            };

            var dialogPrefab = await _assetProvider.LoadAssetAsync<GameObject>("ModalDialogView");
            var dialogInstance = _container.Instantiate(dialogPrefab);
            var dialogView = dialogInstance.GetComponent<IModalDialogView>();

            if (dialogView != null)
            {
                dialogView.Configure(definition, 
                    () => internalCallback(DialogResult.Confirmed),
                    () => internalCallback(DialogResult.Cancelled));
            }
            else
            {
                Debug.LogError("[ViewManager] Dialog prefab is missing IModalDialogView component.");
                internalCallback(DialogResult.Cancelled); // Fail gracefully
            }
            
            return await tcs.Task;
        }

        private async Task LoadSceneAsync(string sceneName)
        {
            var asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncLoad.isDone)
            {
                await Task.Yield();
            }
        }
        
        private string GetSceneNameForScreen(Screen screen)
        {
            return screen switch
            {
                Screen.MainMenu => "MainMenu",
                Screen.GameBoard => "GameBoard",
                _ => throw new ArgumentOutOfRangeException(nameof(screen), $"No scene is mapped for screen '{screen}'")
            };
        }

        private string GetAssetKeyForScreen(Screen screen)
        {
            // These keys would match Addressable asset keys
            return screen switch
            {
                Screen.GameSetup => "GameSetupView",
                Screen.LoadGame => "LoadGameView",
                Screen.Settings => "SettingsView",
                Screen.PropertyManagement => "PropertyManagementView",
                _ => throw new ArgumentOutOfRangeException(nameof(screen), $"No asset key is mapped for screen '{screen}'")
            };
        }
    }
}