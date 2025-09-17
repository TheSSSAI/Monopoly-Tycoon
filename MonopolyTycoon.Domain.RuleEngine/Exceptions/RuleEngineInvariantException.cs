using System;
using System.Runtime.Serialization;

namespace MonopolyTycoon.Domain.RuleEngine.Exceptions
{
    /// <summary>
    /// Represents an error that occurs when a rule engine invariant is violated.
    /// This exception is thrown for unrecoverable logic errors, such as attempting to
    /// apply an action that has not been validated first, indicating a flaw in the
    /// calling application's logic rather than a predictable game rule violation.
    /// </summary>
    [Serializable]
    public class RuleEngineInvariantException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuleEngineInvariantException"/> class.
        /// </summary>
        public RuleEngineInvariantException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleEngineInvariantException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public RuleEngineInvariantException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RuleEngineInvariantException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public RuleEngineInvariantException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}