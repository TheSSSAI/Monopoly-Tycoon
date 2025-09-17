using System.Threading.Tasks;

namespace MonopolyTycoon.Application.Abstractions.Persistence
{
    /// <summary>
    /// Provides a contract for abstracting direct file system operations. This decouples
    /// services that need to read or write files from the concrete System.IO APIs,
    /// improving testability.
    /// Fulfills requirements: REQ-1-092.
    /// </summary>
    public interface IFileSystemRepository
    {
        /// <summary>
        /// Asynchronously writes the specified string content to a file at the given path.
        /// If the file exists, it will be overwritten. If the directory does not exist,
        /// it will be created.
        /// </summary>
        /// <param name="filePath">The absolute path of the file to write to.</param>
        /// <param name="content">The string content to be written to the file.</param>
        /// <returns>A task that represents the asynchronous write operation.</returns>
        /// <exception cref="System.IO.IOException">Thrown on file access errors such as permission issues.</exception>
        /// <exception cref="System.ArgumentException">Thrown if filePath is null or empty.</exception>
        Task WriteTextAsync(string filePath, string content);
    }
}