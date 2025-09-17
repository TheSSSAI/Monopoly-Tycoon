using System;
using System.Runtime.Serialization;

namespace MonopolyTycoon.Infrastructure.Persistence.SaveGames.Exceptions;

/// <summary>
/// Represents an error that occurs when attempting to load a save file from a version
/// for which no data migration path is defined. This indicates the save file is too old
/// to be automatically upgraded.
/// This exception fulfills the error handling contract for REQ-1-090.
/// </summary>
[Serializable]
public class UnsupportedSaveVersionException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UnsupportedSaveVersionException"/> class.
    /// </summary>
    public UnsupportedSaveVersionException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnsupportedSaveVersionException"/> class
    /// with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public UnsupportedSaveVersionException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnsupportedSaveVersionException"/> class
    /// with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception, or a null reference
    /// if no inner exception is specified.
    /// </param>
    public UnsupportedSaveVersionException(string message, Exception innerException) : base(message, innerException)
    {
    }
}