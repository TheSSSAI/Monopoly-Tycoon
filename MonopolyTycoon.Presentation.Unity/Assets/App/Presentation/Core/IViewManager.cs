using System.Threading.Tasks;

namespace MonopolyTycoon.Presentation.Core
{
    /// <summary>
    /// Defines the contract for a service that manages UI views and scene transitions.
    /// This service abstracts the underlying scene and UI panel management implementation,
    /// allowing presenters to request UI changes without being coupled to Unity's SceneManager or specific prefabs.
    /// </summary>
    public interface IViewManager
    {
        /// <summary>
        /// Asynchronously loads a Unity scene by name.
        /// </summary>
        /// <remarks>
        /// Fulfills requirement REQ-1-015 by ensuring scene loading is non-blocking.
        /// An implementation should display a global loading screen during the transition.
        /// </remarks>
        /// <param name="sceneName">The name of the scene to load.</param>
        /// <returns>A task that completes when the scene has finished loading and is active.</returns>
        Task LoadSceneAsync(string sceneName);

        /// <summary>
        /// Asynchronously loads and displays a UI view (e.g., a modal dialog, a panel) from its prefab.
        /// </summary>
        /// <typeparam name="T">The type of the view's interface (e.g., IErrorDialogView).</typeparam>
        /// <param name="viewKey">An identifier for the view, typically an Addressable asset key.</param>
        /// <param name="viewModel">Optional data to pass to the view for initialization.</param>
        /// <returns>A task that completes with an instance of the view's interface once it's displayed.</returns>
        Task<T> ShowViewAsync<T>(string viewKey, object viewModel = null) where T : class, IView;
        
        /// <summary>
        /// Hides or closes a currently displayed UI view.
        /// </summary>
        /// <param name="view">The view instance to hide.</param>
        void HideView(IView view);
    }
    
    /// <summary>
    /// A marker interface for all UI View interfaces.
    /// This allows the ViewManager to work with views in a generic way.
    /// </summary>
    public interface IView { }
}