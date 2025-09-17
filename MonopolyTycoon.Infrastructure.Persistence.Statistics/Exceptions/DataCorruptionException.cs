// Copyright (c) MonopolyTycoon. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.Serialization;

namespace MonopolyTycoon.Infrastructure.Persistence.Statistics.Exceptions
{
    /// <summary>
    /// Represents an error that occurs when the statistics database is found to be corrupt
    /// and cannot be automatically recovered from a backup.
    /// This is a specific, unrecoverable error that the application layer must handle
    /// to provide the user with explicit recovery options (e.g., reset data or exit).
    /// </summary>
    [Serializable]
    public class DataCorruptionException : DataAccessLayerException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataCorruptionException"/> class.
        /// </summary>
        public DataCorruptionException()
            : base("The statistics database is corrupt and could not be recovered from backups.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCorruptionException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DataCorruptionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataCorruptionException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public DataCorruptionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}