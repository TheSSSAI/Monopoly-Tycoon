namespace MonopolyTycoon.Application.Abstractions.Logging
{
    /// <summary>
    /// Defines a generic contract for a logging service, abstracting the underlying logging framework.
    /// This allows for consistent logging practices across the application and simplifies dependency injection and testing.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs an informational message. These are typically used for tracking key application events,
        /// such as starting a new game, completing a transaction, or an AI making a significant decision.
        /// </summary>
        /// <param name="messageTemplate">The message template, which can include placeholders for structured logging (e.g., "Player {PlayerId} passed Go").</param>
        /// <param name="propertyValues">The values corresponding to the placeholders in the message template.</param>
        void Information(string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Logs a warning message. Warnings indicate potential issues that are not critical errors but should be noted,
        /// such as failing to restore a backup or encountering a recoverable error.
        /// </summary>
        /// <param name="messageTemplate">The message template for the warning.</param>
        /// <param name="propertyValues">The values for structured logging placeholders.</param>
        void Warning(string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Logs an error message, typically accompanied by an exception. This is used for critical failures,
        /// unhandled exceptions, or operations that result in a failure state (e.g., file I/O errors, database corruption).
        /// </summary>
        /// <param name="ex">The exception that occurred.</param>
        /// <param name="messageTemplate">The message template describing the context of the error.</param>
        /// <param name="propertyValues">The values for structured logging placeholders.</param>
        void Error(Exception ex, string messageTemplate, params object[] propertyValues);
    }
}