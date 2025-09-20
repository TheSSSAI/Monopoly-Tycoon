using System;
using System.Threading.Tasks;
using MonopolyTycoon.Presentation.Shared.Views;

namespace MonopolyTycoon.Presentation.Shared.Services
{
    /// <summary>
    /// Defines a contract for a service that manages the showing, hiding, and layering of UI screens and dialogs.
    /// This service acts as the central orchestrator for UI navigation within the presentation layer.
    /// </summary>
    public interface IViewManager
    {
        /// <summary>
        /// Asynchronously loads and displays a specific UI screen prefab, hiding any other active screens as necessary.
        /// </summary>
        /// <param name="screen">The strongly-typed identifier for the screen to show.</param>
        /// <param name="payload">Optional data to pass to the screen's presenter upon initialization.</param>
        /// <returns>A task that completes when the screen has finished loading and its entry animation is complete.</returns>
        Task ShowScreenAsync(Screen screen, object payload = null);

        /// <summary>
        /// Hides the specified screen.
        /// </summary>
        /// <param name="screen">The screen to hide.</param>
        /// <returns>A task that completes when the screen's exit animation is complete.</returns>
        Task HideScreenAsync(Screen screen);

        /// <summary>
        /// Displays a modal dialog box with the specified definition.
        /// </summary>
        /// <param name="definition">An object containing the title, message, and button configurations for the dialog.</param>
        /// <returns>A task that completes with the user's choice (e.g., Confirm, Cancel) when the dialog is dismissed.</returns>
        Task<DialogResult> ShowDialogAsync(DialogDefinition definition);

        /// <summary>
        /// Displays a non-intrusive, auto-dismissing notification (toast message).
        /// </summary>
        /// <param name="message">The message to display in the notification.</param>
        /// <param name="durationSeconds">How long the notification should be visible before automatically fading out.</param>
        void ShowNotification(string message, float durationSeconds = 3.0f);

        /// <summary>
        /// Displays a persistent loading spinner overlay.
        /// </summary>
        /// <param name="message">An optional message to display alongside the spinner (e.g., "Loading...").</param>
        void ShowLoadingSpinner(string message = null);

        /// <summary>
        /// Hides the persistent loading spinner overlay.
        /// </summary>
        void HideLoadingSpinner();
    }
}