using System;

namespace MonopolyTycoon.Application.Abstractions.Logging
{
    /// <summary>
    /// Defines a generic contract for a logging service, abstracting the underlying
    /// logging framework (e.g., Serilog). This allows for consistent logging
    /// practices throughout the application and decouples the core logic from
    /// a specific logging implementation.
    /// Fulfills requirements: REQ-1-018, REQ-1-028, REQ-1-023.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs a message at the Information level. This level is for general
        /// operational entries of the application, such as key game events or
        /// successful transactions.
        /// </summary>
        /// <param name="messageTemplate">The message template, which may contain placeholders.</param>
        /// <param name="propertyValues">Optional arguments for the message template.</param>
        void Information(string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Logs a message at the Warning level. This level is for non-critical issues
        /// that are recoverable or do not prevent the application from continuing,
        /// but should be noted.
        /// </summary>
        /// <param name="messageTemplate">The message template, which may contain placeholders.</param>
        /// <param name="propertyValues">Optional arguments for the message template.</param>
        void Warning(string messageTemplate, params object[] propertyValues);
        
        /// <summary>
        /// Logs a message at the Debug level. This level is for detailed diagnostic
        /// information, typically used during development or for tracing complex
        /// processes like AI decision-making.
        /// </summary>
        /// <param name="messageTemplate">The message template, which may contain placeholders.</param>
        /// <param name="propertyValues">Optional arguments for the message template.</param>
        void Debug(string messageTemplate, params object[] propertyValues);

        /// <summary>
        /// Logs a message at the Error level. This level is for critical failures,
        /// exceptions, and faults that prevent a specific operation from completing
        /// or may impact application stability.
        /// </summary>
        /// <param name="ex">The exception associated with the error.</param>
        /// <param name="messageTemplate">The message template, which may contain placeholders.</param>
        /// <param name="propertyValues">Optional arguments for the message template.</param>
        void Error(Exception ex, string messageTemplate, params object[] propertyValues);
    }
}