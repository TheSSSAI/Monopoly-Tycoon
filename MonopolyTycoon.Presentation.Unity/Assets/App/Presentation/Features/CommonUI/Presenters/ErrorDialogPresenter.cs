using MonopolyTycoon.Presentation.Features.CommonUI.Views;
using System;
using UnityEngine;

namespace MonopolyTycoon.Presentation.Features.CommonUI.Presenters
{
    /// <summary>
    /// Presenter for the Error Dialog. This component contains the logic for displaying
    /// error information and handling user actions from the error view. It decouples
    /// the UI logic from the Unity-specific view implementation.
    /// Fulfills REQ-1-023.
    /// </summary>
    public class ErrorDialogPresenter
    {
        private readonly IErrorDialogView _view;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorDialogPresenter"/> class.
        /// </summary>
        /// <param name="view">The view this presenter will control.</param>
        public ErrorDialogPresenter(IErrorDialogView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));

            // Subscribe to events from the view
            _view.OnCloseClicked += HandleCloseClicked;
        }

        /// <summary>
        /// Configures and displays the error dialog with specific error details.
        /// This is the primary entry point for showing an unhandled exception to the user.
        /// </summary>
        /// <param name="errorId">The unique error identifier that correlates to a log entry.</param>
        /// <param name="logPath">The absolute path to the directory containing log files.</param>
        public void ShowError(string errorId, string logPath)
        {
            if (string.IsNullOrWhiteSpace(errorId))
            {
                errorId = "N/A";
            }
            if (string.IsNullOrWhiteSpace(logPath))
            {
                logPath = "Log path could not be determined.";
            }

            _view.SetErrorId(errorId);
            _view.SetLogPath(logPath);
            _view.Show();
        }
        
        /// <summary>
        /// Cleans up subscriptions when the presenter is being disposed.
        /// Although in this specific case the app quits, it's good practice.
        /// </summary>
        public void UnsubscribeEvents()
        {
            _view.OnCloseClicked -= HandleCloseClicked;
        }

        /// <summary>
        /// Handles the user's request to close the error dialog, which results in
        /// terminating the application.
        /// </summary>
        private void HandleCloseClicked()
        {
            // Per US-005, the user can close the application gracefully from the dialog.
            // In Unity, Application.Quit is the standard way to do this.
            // In the Unity Editor, this call will be ignored, but it works in a built player.
            #if UNITY_EDITOR
            Debug.Log("Application.Quit() called. In Editor, this would close the application.");
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
}