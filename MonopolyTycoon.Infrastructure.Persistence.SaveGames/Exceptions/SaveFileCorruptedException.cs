using System;
using System.Runtime.Serialization;

namespace MonopolyTycoon.Infrastructure.Persistence.SaveGames.Exceptions;

/// <summary>
/// Represents an error that occurs when a save file fails an integrity check
/// (e.g., checksum mismatch) or is malformed and cannot be parsed.
/// This exception is thrown to signal that the save file is unreliable and should not be used,
/// fulfilling the error handling contract for REQ-1-088.
/// </summary>
[Serializable]
public class SaveFileCorruptedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SaveFileCorruptedException"/> class.
    /// </summary>
    public SaveFileCorruptedException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SaveFileCorruptedException"/> class
    /// with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public SaveFileCorruptedException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SaveFileCorruptedException"/> class
    /// with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or a null reference
    /// if no inner exception is specified.
    /// </param>
    public SaveFileCorruptedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}