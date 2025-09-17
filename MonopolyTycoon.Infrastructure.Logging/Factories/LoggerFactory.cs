using MonopolyTycoon.Infrastructure.Logging.Configuration;
using MonopolyTycoon.Infrastructure.Logging.Policies;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System;
using System.Diagnostics;
using System.IO;

namespace MonopolyTycoon.Infrastructure.Logging.Factories
{
    /// <summary>
    /// Centralizes the creation and configuration of the Serilog logger instance.
    /// This class encapsulates all Serilog-specific configuration logic, ensuring that
    /// the rest of the application is decoupled from the concrete logging implementation.
    /// </summary>
    internal static class LoggerFactory
    {
        /// <summary>
        /// Configures a Serilog <see cref="LoggerConfiguration"/> instance based on the provided options.
        /// This method applies all application-specific logging rules, including PII redaction,
        /// structured JSON formatting, and asynchronous rolling file sinks.
        /// </summary>
        /// <param name="loggerConfiguration">The Serilog configuration object to be configured.</param>
        /// <param name="options">The strongly-typed options containing all logging settings.</param>
        internal static void Configure(LoggerConfiguration loggerConfiguration, LoggingOptions options)
        {
            // For diagnosing configuration issues, especially in production environments.
            SelfLog.Enable(message => Debug.WriteLine($"Serilog SelfLog: {message}"));

            if (options is null)
            {
                throw new ArgumentNullException(nameof(options), "LoggingOptions cannot be null.");
            }

            // Construct the log file path according to REQ-1-020
            var logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), options.LogFileDirectory);
            var logFilePath = Path.Combine(logDirectory, "log-.json");

            // Set the minimum logging level from configuration.
            var minimumLevel = Enum.TryParse<LogEventLevel>(options.MinimumLevel, true, out var level) 
                ? level 
                : LogEventLevel.Information;

            loggerConfiguration
                .MinimumLevel.Is(minimumLevel)
                .Enrich.FromLogContext() // Enables contextual properties (e.g., TurnNumber) to be added to logs.
                .Destructure.With(new PiiRedactionPolicy()) // Apply PII redaction as per REQ-1-022.
                .WriteTo.Async(sink => sink.File(
                    new JsonFormatter(), // Structured JSON logging as per REQ-1-019.
                    logFilePath,
                    rollingInterval: RollingInterval.Day, // Create a new log file daily.
                    rollOnFileSizeLimit: true, // Create a new file if the size limit is reached.
                    fileSizeLimitBytes: options.FileSizeLimitBytes, // Size limit from config (REQ-1-021).
                    retainedFileCountLimit: options.RetainedFileCountLimit // Retention policy from config (REQ-1-021).
                ), bufferSize: 500); // Buffer for the async sink to improve performance (REQ-1-014).

            // In debug builds, also write to the debug console for easier development-time diagnostics.
#if DEBUG
            loggerConfiguration.WriteTo.Debug(
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
            );
#endif
        }
    }
}