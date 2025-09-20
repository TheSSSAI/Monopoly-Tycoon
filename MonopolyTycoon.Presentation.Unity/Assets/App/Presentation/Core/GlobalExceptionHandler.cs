using MonopolyTycoon.Application.Abstractions.Logging;
using MonopolyTycoon.Presentation.Shared.Services;
using MonopolyTycoon.Presentation.Shared.Views;
using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace MonopolyTycoon.Presentation.Core
{
    /// <summary>
    /// Implements the global exception handling logic required by REQ-1-023.
    /// It subscribes to the system's unhandled exception event at startup,
    /// logs the error with a unique correlation ID, and commands the ViewManager
    /// to display a modal error dialog, preventing an ungraceful application crash.
    /// </summary>
    public sealed class GlobalExceptionHandler : IDisposable
    {
        private readonly ILogger _logger;
        private readonly IViewManager _viewManager;
        private bool _disposed = false;

        public GlobalExceptionHandler(ILogger logger, IViewManager viewManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _viewManager = viewManager ?? throw new ArgumentNullException(nameof(viewManager));
        }

        /// <summary>
        /// Registers the global exception handler to catch all unhandled exceptions
        /// within the current application domain. This should be called once at startup.
        /// </summary>
        public void Register()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            Application.logMessageReceived += OnUnityLogMessageReceived;
            _logger.Information("Global Exception Handler registered.");
        }

        /// <summary>
        /// Unregisters the global exception handler.
        /// </summary>
        public void Dispose()
        {
            if (_disposed) return;
            AppDomain.CurrentDomain.UnhandledException -= OnUnhandledException;
            Application.logMessageReceived -= OnUnityLogMessageReceived;
            _disposed = true;
        }

        /// <summary>
        /// Handles unhandled exceptions from Unity's logging system, specifically for critical errors.
        /// </summary>
        private void OnUnityLogMessageReceived(string condition, string stackTrace, LogType type)
        {
            if (type == LogType.Exception)
            {
                // This is a fallback for exceptions that might be caught by Unity's logger before AppDomain
                // We create a synthetic exception object to pass to our main handler.
                var syntheticException = new Exception($"{condition}\n{stackTrace}");
                HandleException(syntheticException);
            }
        }

        /// <summary>
        /// The primary handler for any unhandled exceptions in the AppDomain.
        /// </summary>
        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            if (args.ExceptionObject is Exception ex)
            {
                HandleException(ex);
            }
        }

        /// <summary>
        /// Central logic for processing a caught exception.
        /// This method generates a correlation ID, logs the exception, and displays a user-friendly dialog.
        /// </summary>
        /// <param name="ex">The exception that was thrown.</param>
        private void HandleException(Exception ex)
        {
            // This is a safety measure to prevent recursive error handling if the error handler itself fails.
            // We unregister immediately to prevent any further exceptions from being caught by this handler.
            Dispose();

            string correlationId = Guid.NewGuid().ToString("N");

            try
            {
                // REQ-1-022: No PII is explicitly added here. The logger is configured to handle sanitization.
                // The only permitted PII is the profile name for debugging context, which is handled by the logger itself.
                _logger.Fatal(ex, "FATAL UNHANDLED EXCEPTION. Correlation ID: {CorrelationId}", correlationId);

                // REQ-1-023: The dialog must provide a unique error identifier and instruct the user on how to find logs.
                string logPath = GetLogDirectoryPath();

                var dialogMessage = new StringBuilder();
                dialogMessage.AppendLine("An unexpected error has occurred and the application must close.");
                dialogMessage.AppendLine("The error has been logged for diagnosis.");
                dialogMessage.AppendLine();
                dialogMessage.AppendLine($"<b>Error ID:</b> {correlationId}");
                dialogMessage.AppendLine();
                dialogMessage.AppendLine("Please include this ID and the log files when reporting the issue.");
                dialogMessage.AppendLine($"<b>Log Location:</b> {logPath}");

                var dialogDefinition = new ModalDialogView.DialogDefinition
                {
                    Title = "Application Error",
                    Message = dialogMessage.ToString(),
                    ConfirmButtonText = "Close Application"
                };

                // We don't await this because the application is in a terminal state.
                // We just want to show the dialog and then quit.
                _viewManager.ShowDialog(dialogDefinition).ContinueWith(_ => QuitApplication());
            }
            catch (Exception innerEx)
            {
                // Last resort if the logger or view manager fails.
                Debug.LogError($"CRITICAL FAILURE IN EXCEPTION HANDLER. Correlation ID: {correlationId}. Original Exception: {ex}. Handler Exception: {innerEx}");
                QuitApplication();
            }
        }

        /// <summary>
        /// Retrieves the path to the log directory, fulfilling REQ-1-020 and REQ-1-023.
        /// </summary>
        /// <returns>The absolute path to the log directory.</returns>
        private string GetLogDirectoryPath()
        {
            // REQ-1-020: Log files are stored in a 'logs' subdirectory within the application's data folder at `%APPDATA%/MonopolyTycoon/`.
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appDirectory = Path.Combine(appDataPath, "MonopolyTycoon");
            return Path.Combine(appDirectory, "logs");
        }

        private void QuitApplication()
        {
            // In a standalone build, this will close the application.
            // In the editor, it will stop play mode.
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}