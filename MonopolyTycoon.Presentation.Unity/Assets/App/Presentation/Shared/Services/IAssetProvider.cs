using System.Threading.Tasks;
using UnityEngine;

namespace MonopolyTycoon.Presentation.Shared.Services
{
    /// <summary>
    /// Defines a contract for a service that provides asynchronous loading of game assets.
    /// This abstracts the underlying asset management system (e.g., Addressables, Resources)
    /// to support features like theming and reduce hard-coded asset references.
    /// </summary>
    public interface IAssetProvider
    {
        /// <summary>
        /// Asynchronously loads a Unity GameObject prefab from a given key or address.
        /// </summary>
        /// <param name="key">The unique identifier for the asset (e.g., an Addressable key).</param>
        /// <returns>A task that completes with the loaded GameObject. Returns null if loading fails.</returns>
        Task<GameObject> LoadGameObjectAsync(string key);

        /// <summary>
        /// Asynchronously loads an audio clip.
        /// </summary>
        /// <param name="key">The unique identifier for the audio clip.</param>
        /// <returns>A task that completes with the loaded AudioClip. Returns null if loading fails.</returns>
        Task<AudioClip> LoadAudioClipAsync(string key);

        /// <summary>
        /// Asynchronously loads a sprite.
        /// </summary>
        /// <param name="key">The unique identifier for the sprite.</param>
        /// <returns>A task that completes with the loaded Sprite. Returns null if loading fails.</returns>
        Task<Sprite> LoadSpriteAsync(string key);

        /// <summary>
        /// Asynchronously loads a ScriptableObject, such as a ThemeDefinition.
        /// </summary>
        /// <typeparam name="T">The type of ScriptableObject to load.</typeparam>
        /// <param name="key">The unique identifier for the asset.</param>
        /// <returns>A task that completes with the loaded ScriptableObject. Returns null if loading fails.</returns>
        Task<T> LoadScriptableObjectAsync<T>(string key) where T : ScriptableObject;

        /// <summary>
        /// Releases a previously loaded asset from memory to free up resources.
        /// </summary>
        /// <param name="asset">The asset instance to release.</param>
        void ReleaseAsset(Object asset);
    }
}