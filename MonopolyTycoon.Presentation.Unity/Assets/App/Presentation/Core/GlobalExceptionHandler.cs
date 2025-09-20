using MonopolyTycoon.Presentation.Shared.Services;
using MonopolyTycoon.Presentation.Shared.Views;
using Serilog;
using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace MonopolyTycoon.Presentation.Core
{
    /// <summary>
    /// Implements the global exception handling logic required by REQ-1-023.
    /// It subscribes to system-level and Unity-level unhandled exception events,
    /// logs the error with a unique correlation ID, and commands the ViewManager 
    /// to display a modal error dialog, preventing an abrupt application crash.
    /// </summary>
    public sealed class GlobalExceptionHandler
    {
        private readonly IViewManager _viewManager;
        private readonly ILogger _logger;

        // Volatile to ensure thread safety for this flag
        private volatile bool _isHandlingException = false;

        public GlobalExceptionHandler(IViewManager viewManager, ILogger logger)
        {
            _viewManager = viewManager ?? throw new ArgumentNullException(nameof(viewManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Subscribes to the necessary unhandled exception events.
        /// This should be called once at application startup.
        /// </summary>
        public void Register()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            Application.logMessageReceivedThreaded += OnUnityLogMessageReceived;
        }

        /// <summary>
        /// Unsubscribes from unhandled exception events.
        /// This should be called on application quit to clean up.
        /// </summary>
        public void Unregister()
        {
            AppDomain.CurrentDomain.UnhandledException -= OnUnhandledException;
            Application.logMessageReceivedThreaded -= OnUnityLogMessageReceived;
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            if (args.ExceptionObject is Exception ex)
            {
                HandleException(ex, "An unhandled AppDomain exception occurred.", args.IsTerminating);
            }
            else
            {
                HandleException(new Exception("Non-CLS compliant exception"), $"An unknown error occurred: {args.ExceptionObject}", args.IsTerminating);
            }
        }

        private void OnUnityLogMessageReceived(string condition, string stackTrace, LogType type)
        {
            if (type == LogType.Exception)
            {
                // Unity's LogType.Exception is how it often reports unhandled exceptions from its own threads or coroutines.
                // We treat this as a fatal, unhandled exception.
                var ex = new Exception($"Unity Engine Exception: {condition}\n{stackTrace}");
                HandleException(ex, "An unhandled Unity Engine exception occurred.", true);
            }
        }

        /// <summary>
        /// Central logic for handling any caught exception. This ensures consistent logging and user notification.
        /// </summary>
        /// <param name="ex">The exception that occurred.</param>
        /// <param name="logMessage">A descriptive message for the log.</param>
        /// <param name="isTerminating">Whether the exception is considered fatal to the application.</param>
        private void HandleException(Exception ex, string logMessage, bool isTerminating)
        {
            // Prevent recursive exception handling or handling multiple simultaneous crashes
            if (_isHandlingException) return;
            _isHandlingException = true;
            
            var correlationId = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpperInvariant();

            try
            {
                // Log the exception with fatal severity, including the correlation ID.
                _logger.Fatal(ex, "[{CorrelationId}] {FatalMessage}", correlationId, logMessage);
            }
            catch (Exception logEx)
            {
                // Last resort if the logger itself fails.
                Debug.LogError($"CRITICAL FAILURE: Could not write to log. Original Exception: {ex}. Logging Exception: {logEx}");
            }

            try
            {
                // Construct the user-facing dialog message as per REQ-1-023
                var dialogMessage = BuildUserFacingErrorMessage(correlationId);
                
                var dialogDefinition = new DialogDefinition
                {
                    Title = "Unexpected Error",
                    Message = dialogMessage,
                    ConfirmButtonText = "Close" 
                };

                // The ViewManager is responsible for ensuring this is run on the main thread.
                _viewManager.ShowDialog(dialogDefinition);
            }
            catch (Exception viewEx)
            {
                // If we can't even show the dialog, log it and prepare to terminate.
                Debug.LogError($"CRITICAL FAILURE: Could not display error dialog. Original Exception: {ex}. View Exception: {viewEx}");
            }
            finally
            {
                // Per REQ-1-023, the dialog instructs the user. The application should terminate after an unhandled exception.
                // While the dialog's close button can also call Application.Quit(), this is a fallback.
                if (isTerminating)
                {
                    // Forcing a quit here might be too aggressive if the dialog is showing,
                    // but it ensures termination if the dialog fails.
                    // We let the user close the dialog themselves.
                }
            }
        }

        private string BuildUserFacingErrorMessage(string correlationId)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Monopoly Tycoon has encountered an unexpected error and must close.");
            sb.AppendLine();
            sb.AppendLine("Please report this issue to our support team and provide the following details:");
            sb.AppendLine();
            sb.AppendLine($"<b>Error ID:</b> {correlationId}");
            sb.AppendLine();
            sb.AppendLine("<b>Log File Location:</b>");
            
            try
            {
                // REQ-1-020 specifies the log path.
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string logDirectory = Path.Combine(appDataPath, "MonopolyTycoon", "logs");
                sb.AppendLine(logDirectory);
            }
            catch(Exception)
            {
                sb.AppendLine("Could not determine log file path.");
            }

            return sb.ToString();
        }
    }
}