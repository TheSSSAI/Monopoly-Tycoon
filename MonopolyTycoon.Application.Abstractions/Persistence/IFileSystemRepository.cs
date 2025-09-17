using System.Threading.Tasks;

namespace MonopolyTycoon.Application.Abstractions.Persistence
{
    /// <summary>
    /// Defines a contract for abstracting direct file system operations.
    /// This repository decouples services from the underlying file I/O APIs,
    /// improving testability and maintainability.
    /// </summary>
    public interface IFileSystemRepository
    {
        /// <summary>
        /// Asynchronously writes the given text content to a file at the specified path.
        /// If the file exists, it will be overwritten. If the directory does not exist, it should be created.
        /// </summary>
        /// <param name="filePath">The absolute path of the file to write to.</param>
        /// <param name="content">The string content to be written to the file.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        /// <exception cref="System.IO.IOException">Thrown on file access errors, such as lack of permissions or disk full.</exception>
        /// <exception cref="ArgumentException">Thrown if filePath is null or invalid.</exception>
        Task WriteTextAsync(string filePath, string content);
    }
}