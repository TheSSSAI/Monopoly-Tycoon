using System.Runtime.Serialization;

namespace MonopolyTycoon.Application.Services.Exceptions
{
    /// <summary>
    /// Represents an error related to game session management, such as a failure to start,
    /// save, or load a game. This exception helps to differentiate session-level failures
    /// from in-game rule violations.
    /// </summary>
    [Serializable]
    public class SessionManagementException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SessionManagementException"/> class.
        /// </summary>
        public SessionManagementException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionManagementException"/> class
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public SessionManagementException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionManagementException"/> class
        /// with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public SessionManagementException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionManagementException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        protected SessionManagementException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}