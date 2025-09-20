using MonopolyTycoon.Presentation.Shared.Services;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cysharp.Threading.Tasks;
using System;

namespace MonopolyTycoon.Presentation.Shared.Services
{
    /// <summary>
    /// Concrete implementation of IAssetProvider using Unity's Addressables system.
    /// This service is responsible for asynchronously loading and managing game assets.
    /// It implements caching to avoid redundant loads and ensures proper memory management by releasing assets.
    /// Fulfills: REQ-1-093 (Theme System via dynamic asset loading).
    /// </summary>
    public class AddressableAssetProvider : IAssetProvider, IAsyncDisposable
    {
        private readonly ConcurrentDictionary<string, AsyncOperationHandle> _cachedHandles = new();
        private readonly ConcurrentDictionary<string, Task> _loadingOperations = new();

        public async UniTask<T> LoadAssetAsync<T>(string key) where T : class
        {
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogError("[AddressableAssetProvider] Asset key cannot be null or empty.");
                return null;
            }

            if (_cachedHandles.TryGetValue(key, out var handle))
            {
                return handle.Result as T;
            }

            // Atomically check and start the loading operation if not already in progress
            var loadingTask = _loadingOperations.GetOrAdd(key, _ => LoadAndCacheAssetInternalAsync<T>(key));

            await loadingTask.AsUniTask();

            // The task is now complete, remove it from the loading operations dictionary
            _loadingOperations.TryRemove(key, out _);
            
            if (_cachedHandles.TryGetValue(key, out var completedHandle) && completedHandle.Status == AsyncOperationStatus.Succeeded)
            {
                return completedHandle.Result as T;
            }

            // If we are here, it means loading failed. The internal method has already logged the error.
            return null;
        }
        
        public void ReleaseAsset(string key)
        {
            if (string.IsNullOrEmpty(key)) return;

            if (_cachedHandles.TryRemove(key, out var handle))
            {
                Addressables.Release(handle);
            }
        }

        private async Task LoadAndCacheAssetInternalAsync<T>(string key) where T : class
        {
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                // We cast the handle to the non-generic version for storage,
                // as the dictionary can't hold generic types.
                _cachedHandles[key] = handle;
            }
            else
            {
                Debug.LogError($"[AddressableAssetProvider] Failed to load asset with key: '{key}'. Reason: {handle.OperationException}");
                // Release the failed handle to clean up memory
                Addressables.Release(handle);
            }
        }

        public async UniTask DisposeAsync()
        {
            // Wait for any ongoing loading operations to complete before cleaning up
            var allLoadingTasks = new List<Task>();
            foreach (var kvp in _loadingOperations)
            {
                allLoadingTasks.Add(kvp.Value);
            }
            await Task.WhenAll(allLoadingTasks);

            foreach (var key in _cachedHandles.Keys)
            {
                if (_cachedHandles.TryRemove(key, out var handle))
                {
                    Addressables.Release(handle);
                }
            }
            _cachedHandles.Clear();
            _loadingOperations.Clear();
        }
    }
}