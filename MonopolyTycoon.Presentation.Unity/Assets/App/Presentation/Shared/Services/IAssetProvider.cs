using System.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine;

namespace MonopolyTycoon.Presentation.Shared.Services
{
    /// <summary>
    /// Contract for a service that provides access to game assets.
    /// This abstracts the asset loading mechanism (e.g., Resources, AssetBundles, Addressables).
    /// </summary>
    public interface IAssetProvider
    {
        /// <summary>
        /// Asynchronously loads an asset of a specific type using its key.
        /// </summary>
        /// <typeparam name="T">The type of the asset to load.</typeparam>
        /// <param name="key">The addressable key or path to the asset.</param>
        /// <returns>A task that resolves to the loaded asset, or null if loading fails.</returns>
        Task<T> LoadAssetAsync<T>(object key) where T : class;

        /// <summary>
        /// Asynchronously loads a scene by its key.
        /// </summary>
        /// <param name="key">The addressable key for the scene.</param>
        /// <returns>A task that resolves to the loaded scene instance.</returns>
        Task<SceneInstance> LoadSceneAsync(object key);

        /// <summary>
        /// Releases a previously loaded asset to free up memory.
        /// </summary>
        /// <param name="asset">The asset to release.</param>
        void ReleaseAsset(object asset);
    }
}