using MonopolyTycoon.Application.Contracts;
using MonopolyTycoon.Presentation.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace MonopolyTycoon.Presentation.Core
{
    public class ViewManager : MonoBehaviour, IViewManager
    {
        [Inject]
        private readonly IObjectResolver _container;
        [Inject]
        private readonly ILoggerAdapter<ViewManager> _logger;

        [Header("Configuration")]
        [SerializeField] private GameObject _loadingScreenPrefab;

        private GameObject _loadingScreenInstance;
        private readonly Dictionary<string, GameObject> _activeViews = new();

        private void Awake()
        {
            if (_loadingScreenPrefab != null)
            {
                _loadingScreenInstance = Instantiate(_loadingScreenPrefab, transform);
                _loadingScreenInstance.SetActive(false);
            }
        }

        public async Task LoadSceneAsync(string sceneName)
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                _logger.LogError("Cannot load scene: sceneName is null or empty.");
                throw new ArgumentNullException(nameof(sceneName));
            }

            try
            {
                ShowLoadingScreen();
                _logger.LogInformation("Loading scene: {SceneName}", sceneName);
                
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
                while (!asyncLoad.isDone)
                {
                    // Optionally update loading progress here
                    await Task.Yield();
                }
                
                _logger.LogInformation("Scene {SceneName} loaded successfully.", sceneName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load scene: {SceneName}", sceneName);
                // In a real scenario, we might want to show an error dialog and return to the main menu
                throw;
            }
            finally
            {
                HideLoadingScreen();
            }
        }

        public async Task<T> ShowView<T>(string viewKey, object viewModel = null) where T : class
        {
            if (string.IsNullOrEmpty(viewKey))
            {
                _logger.LogError("Cannot show view: viewKey is null or empty.");
                throw new ArgumentNullException(nameof(viewKey));
            }

            if (_activeViews.ContainsKey(viewKey))
            {
                _logger.LogWarning("View with key {ViewKey} is already active.", viewKey);
                return _activeViews[viewKey].GetComponent<T>();
            }

            try
            {
                _logger.LogInformation("Showing view with key: {ViewKey}", viewKey);
                AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(viewKey, transform);
                GameObject viewInstance = await handle.Task;

                if (viewInstance == null)
                {
                    throw new InvalidOperationException($"Failed to instantiate Addressable with key '{viewKey}'.");
                }
                
                _activeViews[viewKey] = viewInstance;

                // VContainer specific: Inject dependencies into the newly instantiated view and its children
                _container.InjectGameObject(viewInstance);
                
                // Optional: A common pattern is to have an IView<TViewModel> interface
                // on the view component to pass the view model for initialization.
                if(viewModel != null && viewInstance.TryGetComponent(out IInitializable<object> initializable))
                {
                    initializable.Initialize(viewModel);
                }

                T viewComponent = viewInstance.GetComponent<T>();
                if (viewComponent == null)
                {
                    throw new InvalidOperationException($"View instance for key '{viewKey}' does not have a component of type '{typeof(T).Name}'.");
                }

                return viewComponent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to show view with key: {ViewKey}", viewKey);
                // Optionally destroy the failed instance if it exists
                if (_activeViews.ContainsKey(viewKey))
                {
                    Addressables.ReleaseInstance(_activeViews[viewKey]);
                    _activeViews.Remove(viewKey);
                }
                throw;
            }
        }

        public void HideView(string viewKey)
        {
            if (!_activeViews.TryGetValue(viewKey, out GameObject viewInstance))
            {
                _logger.LogWarning("Attempted to hide a view that is not active: {ViewKey}", viewKey);
                return;
            }
            
            _logger.LogInformation("Hiding view with key: {ViewKey}", viewKey);
            Addressables.ReleaseInstance(viewInstance);
            _activeViews.Remove(viewKey);
        }

        private void ShowLoadingScreen()
        {
            if (_loadingScreenInstance != null)
            {
                _loadingScreenInstance.SetActive(true);
            }
        }

        private void HideLoadingScreen()
        {
            if (_loadingScreenInstance != null)
            {
                _loadingScreenInstance.SetActive(false);
            }
        }
    }

    // Example of a supporting interface for view model initialization
    public interface IInitializable<in T>
    {
        void Initialize(T viewModel);
    }
}