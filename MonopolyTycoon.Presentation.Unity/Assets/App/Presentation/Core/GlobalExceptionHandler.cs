using MonopolyTycoon.Application.Services;
using MonopolyTycoon.Presentation.Features.CommonUI.ViewModels;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using UnityEngine;

namespace MonopolyTycoon.Presentation.Core
{
    /// <summary>
    /// Implements a global catch-all for unhandled exceptions to prevent the application
    /// from crashing and to display a user-friendly error dialog.
    /// Fulfills requirement REQ-1-023.
    /// </summary>
    public class GlobalExceptionHandler : MonoBehaviour
    {
        private IViewManager _viewManager;
        private ILogger _logger;

        private static readonly ConcurrentQueue<Action> _mainThreadActions = new ConcurrentQueue<Action>();
        private volatile bool _isHandlingException = false;

        /// <summary>
        /// Injected by the CompositionRoot. This method is used instead of constructor injection
        /// for MonoBehaviours.
        /// </summary>
        public void Initialize(IViewManager viewManager, ILogger logger)
        {
            _viewManager = viewManager;
            _logger = logger;
        }

        private void Awake()
        {
            // Using the threaded version is crucial to catch exceptions from any thread.
            Application.logMessageReceivedThreaded += HandleLog;
        }

        private void OnDestroy()
        {
            Application.logMessageReceivedThreaded -= HandleLog;
        }

        private void Update()
        {
            // Process any actions that were queued to run on the main thread.
            if (_mainThreadActions.TryDequeue(out var action))
            {
                action.Invoke();
            }
        }

        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            // We are only interested in unhandled exceptions.
            if (type != LogType.Exception)
            {
                return;
            }

            // Prevent recursive error handling or multiple dialogs for a single crash event.
            if (_isHandlingException)
            {
                return;
            }
            _isHandlingException = true;
            
            // Generate a unique identifier to correlate the user-facing message with the log entry.
            // This is critical for effective bug reporting and diagnosis.
            var correlationId = Guid.NewGuid().ToString();

            // The full exception details are logged for developers.
            // The logString and stackTrace provided by Unity are concatenated.
            var exceptionMessage = $"Unhandled Exception Caught by Global Handler.\nCorrelation ID: {correlationId}\nLog: {logString}\nStackTrace: {stackTrace}";
            _logger.Error(new Exception(logString), exceptionMessage);

            // UI operations must be performed on the main thread.
            // We queue the action to be executed in the Update() loop.
            _mainThreadActions.Enqueue(() => 
            {
                // Ensure the application quits even if showing the dialog fails.
                try
                {
                    ShowErrorDialogOnMainThread(correlationId);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"CRITICAL FAILURE: Could not display the global error dialog. Forcing application quit. Inner Exception: {ex.Message}");
                    Application.Quit();
                }
            });
        }

        private void ShowErrorDialogOnMainThread(string correlationId)
        {
            string logPath;
            try
            {
                // As per REQ-1-020, user data is stored here.
                logPath = System.IO.Path.Combine(Application.persistentDataPath, "logs");
            }
            catch
            {
                // Fallback if persistentDataPath is somehow inaccessible.
                logPath = "Could not determine log path.";
            }

            var viewModel = new ErrorDialogViewModel
            {
                Title = "Unexpected Error",
                Message = "An unexpected error occurred and the application must close. Please provide the following information to support when reporting this issue.",
                CorrelationId = correlationId,
                LogFilePath = logPath
            };

            // This should be a fire-and-forget call. The ErrorDialogPresenter
            // will handle the logic for closing the application.
            _viewManager.ShowView<object>("ErrorDialogView", viewModel);
        }
    }
}