using Microsoft.Extensions.Logging;
using MonopolyTycoon.Application.Abstractions.Configuration;
using MonopolyTycoon.Infrastructure.Configuration.Exceptions;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace MonopolyTycoon.Infrastructure.Configuration.Providers
{
    /// <summary>
    /// Provides a concrete implementation for loading and deserializing configuration from a JSON file.
    /// This service encapsulates the details of file system access and JSON parsing, providing a reusable
    /// and robust mechanism for various parts of the application to load their configuration.
    /// It fulfills REQ-1-063, REQ-1-083, and REQ-1-084.
    /// </summary>
    public class JsonConfigurationProvider : IConfigurationProvider
    {
        private readonly ILogger<JsonConfigurationProvider> _logger;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonConfigurationProvider"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for diagnostics and error reporting.</param>
        public JsonConfigurationProvider(ILogger<JsonConfigurationProvider> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };
        }

        /// <summary>
        /// Asynchronously loads and deserializes a configuration file from the specified path into a strongly-typed object.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize the configuration into.</typeparam>
        /// <param name="configPath">The absolute or relative path to the JSON configuration file.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the deserialized object, or null if the file is empty or contains only null.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="configPath"/> is null, empty, or whitespace.</exception>
        /// <exception cref="ConfigurationException">Thrown when the file cannot be found, read, or parsed, wrapping the original exception.</exception>
        public async Task<T?> LoadAsync<T>(string configPath) where T : class
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(configPath);

            _logger.LogDebug("Attempting to load configuration file of type {ConfigType} from path: {ConfigPath}", typeof(T).Name, configPath);

            try
            {
                if (!File.Exists(configPath))
                {
                    _logger.LogError("Configuration file not found at path: {ConfigPath}", configPath);
                    throw new FileNotFoundException($"The configuration file was not found.", configPath);
                }

                string jsonContent = await File.ReadAllTextAsync(configPath);

                if (string.IsNullOrWhiteSpace(jsonContent))
                {
                    _logger.LogWarning("Configuration file at path {ConfigPath} is empty. Returning default for {ConfigType}.", configPath, typeof(T).Name);
                    return default;
                }
                
                var configuration = JsonSerializer.Deserialize<T>(jsonContent, _jsonSerializerOptions);

                _logger.LogInformation("Successfully loaded and deserialized configuration for {ConfigType} from {ConfigPath}", typeof(T).Name, configPath);

                return configuration;
            }
            catch (FileNotFoundException ex)
            {
                // This catch is for clarity, but the File.Exists check above makes it less likely.
                var errorMessage = $"Configuration file could not be found at the specified path: {configPath}";
                _logger.LogError(ex, errorMessage);
                throw new ConfigurationException(errorMessage, ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                var errorMessage = $"The directory for the configuration file could not be found. Path: {configPath}";
                _logger.LogError(ex, errorMessage);
                throw new ConfigurationException(errorMessage, ex);
            }
            catch (JsonException ex)
            {
                var errorMessage = $"Failed to parse the configuration file due to invalid JSON format. Path: {configPath}";
                _logger.LogError(ex, errorMessage);
                throw new ConfigurationException(errorMessage, ex);
            }
            catch (IOException ex)
            {
                var errorMessage = $"An I/O error occurred while reading the configuration file. Path: {configPath}. Check file permissions and ensure it's not in use.";
                _logger.LogError(ex, errorMessage);
                throw new ConfigurationException(errorMessage, ex);
            }
            catch (Exception ex)
            {
                var errorMessage = $"An unexpected error occurred while loading the configuration file. Path: {configPath}";
                _logger.LogError(ex, errorMessage);
                throw new ConfigurationException(errorMessage, ex);
            }
        }
    }
}