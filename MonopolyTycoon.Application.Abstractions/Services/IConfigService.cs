using System.Threading.Tasks;

namespace MonopolyTycoon.Application.Abstractions.Services
{
    /// <summary>
    /// Defines a contract for a service that loads and deserializes strongly-typed
    /// configuration objects from an external source (e.g., JSON file).
    /// This abstracts the file I/O and deserialization logic from the services that
    /// consume the configuration data.
    /// Fulfills requirements: REQ-1-063, REQ-1-083, REQ-1-084.
    /// </summary>
    public interface IConfigService
    {
        /// <summary>
        /// Asynchronously loads and deserializes a configuration file into a
        /// strongly-typed object of type T.
        /// </summary>
        /// <typeparam name="T">The type of the configuration object to deserialize into.</typeparam>
        /// <param name="configKey">A key or path used to identify the configuration source.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains
        /// the populated configuration object of type T.
        /// </returns>
        /// <exception cref="System.IO.FileNotFoundException">Thrown if the configuration source cannot be found.</exception>
        /// <exception cref="System.Text.Json.JsonException">Thrown if the configuration data is malformed.</exception>
        Task<T> LoadAsync<T>(string configKey) where T : class;
    }
}