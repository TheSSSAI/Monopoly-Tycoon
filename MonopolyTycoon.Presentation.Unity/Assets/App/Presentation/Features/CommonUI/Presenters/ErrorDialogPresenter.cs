using MonopolyTycoon.Application.Abstractions;
using MonopolyTycoon.Presentation.Shared.Views;
using System;
using UnityEngine;
using VContainer.Unity;

namespace MonopolyTycoon.Presentation.Features.CommonUI.Presenters
{
    public class ErrorDialogPresenter : IStartable, IDisposable
    {
        private readonly IGlobalExceptionHandler _globalExceptionHandler;
        private readonly IViewManager _viewManager;

        public ErrorDialogPresenter(IGlobalExceptionHandler globalExceptionHandler, IViewManager viewManager)
        {
            _globalExceptionHandler = globalExceptionHandler ?? throw new ArgumentNullException(nameof(globalExceptionHandler));
            _viewManager = viewManager ?? throw new ArgumentNullException(nameof(viewManager));
        }

        public void Start()
        {
            _globalExceptionHandler.OnUnhandledException += HandleUnhandledException;
        }

        public void Dispose()
        {
            _globalExceptionHandler.OnUnhandledException -= HandleUnhandledException;
        }

        private void HandleUnhandledException(string errorId, string message, string logPath)
        {
            // REQ-1-023: In the event of an unhandled exception, the system shall display a modal error dialog to the user.
            var dialogDefinition = new DialogDefinition
            {
                Title = "Unexpected Error",
                // REQ-1-023: 1) Inform the user that an error occurred, 2) Provide a unique error identifier, and 3) Instruct the user on how to find the log files.
                Message = $"An unexpected error occurred.\n\nPlease report this issue with the following details:\n\nError ID: {errorId}\n\nLogs can be found at:\n{logPath}",
                ConfirmButtonText = "Close Application",
                HasCancelButton = false
            };
            
            _viewManager.ShowDialog(dialogDefinition, OnDialogClose);
        }

        private void OnDialogClose(DialogResult result)
        {
            // The only option is to close the application gracefully.
            // This ensures the potentially corrupted state is not persisted.
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}