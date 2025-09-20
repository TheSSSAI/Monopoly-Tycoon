using MonopolyTycoon.Presentation.Shared.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MonopolyTycoon.Presentation.Shared
{
    /// <summary>
    /// Concrete implementation of IAssetProvider using Unity's Addressables system.
    /// Handles asynchronous loading and caching of assets.
    /// This is the foundation for the theme system (REQ-1-093).
    /// </summary>
    public class AddressableAssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _cachedHandles = new();

        public async Task<T> LoadAssetAsync<T>(string key) where T : Object
        {
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogError("[AddressableAssetProvider] Asset key is null or empty.");
                return null;
            }

            if (_cachedHandles.TryGetValue(key, out var cachedHandle))
            {
                return cachedHandle.Result as T;
            }

            var handle = Addressables.LoadAssetAsync<T>(key);
            _cachedHandles[key] = handle;

            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return handle.Result;
            }
            
            Debug.LogError($"[AddressableAssetProvider] Failed to load asset with key: {key}. Reason: {handle.OperationException}");
            _cachedHandles.Remove(key);
            Addressables.Release(handle);
            return null;
        }

        public async Task<GameObject> InstantiateAsync(string key, Transform parent = null)
        {
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogError("[AddressableAssetProvider] Prefab key is null or empty.");
                return null;
            }

            var handle = Addressables.InstantiateAsync(key, parent);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return handle.Result;
            }

            Debug.LogError($"[AddressableAssetProvider] Failed to instantiate prefab with key: {key}. Reason: {handle.OperationException}");
            return null;
        }

        public void ReleaseAsset(string key)
        {
            if (string.IsNullOrEmpty(key)) return;

            if (_cachedHandles.TryGetValue(key, out var handle))
            {
                _cachedHandles.Remove(key);
                Addressables.Release(handle);
            }
        }

        public void CleanUp()
        {
            foreach (var handle in _cachedHandles.Values)
            {
                Addressables.Release(handle);
            }
            _cachedHandles.Clear();
        }
    }
}