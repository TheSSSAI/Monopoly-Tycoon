using System.Threading.Tasks;

namespace MonopolyTycoon.Application.Abstractions.Services
{
    /// <summary>
    /// Defines a contract for a service that loads and deserializes configuration
    /// files from an external source into strongly-typed objects.
    /// </summary>
    public interface IConfigService
    {
        /// <summary>
        /// Asynchronously loads and deserializes a configuration file into an object of type T.
        /// Implementations should handle caching to avoid redundant file I/O.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the configuration into.</typeparam>
        /// <param name="configName">The logical name or path of the configuration to load (e.g., "AI.Hard", "Rulebook").</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the populated configuration object of type T.
        /// </returns>
        /// <exception cref="System.IO.FileNotFoundException">Thrown if the specified configuration source cannot be found.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the configuration data cannot be deserialized into the specified type T.</exception>
        Task<T> LoadConfigAsync<T>(string configName) where T : class;
    }
}