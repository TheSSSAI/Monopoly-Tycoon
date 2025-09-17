using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MonopolyTycoon.Infrastructure.Logging.Configuration;
using MonopolyTycoon.Infrastructure.Logging.Factories;
using Serilog;

namespace MonopolyTycoon.Infrastructure.Logging.Extensions
{
    /// <summary>
    /// Provides extension methods for registering logging services with the dependency injection container.
    /// This is the sole public entry point for configuring and enabling logging for the application.
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Configures and registers the Serilog logging pipeline with the application's service collection.
        /// This method reads configuration from the provided <see cref="IConfiguration"/>, binds it to
        /// <see cref="LoggingOptions"/>, and uses a factory to construct the logger.
        /// It wires Serilog into the standard Microsoft.Extensions.Logging framework, allowing consumers
        /// throughout the application to inject and use the generic <see cref="Microsoft.Extensions.Logging.ILogger{TCategoryName}"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configuration">The application's configuration provider.</param>
        /// <returns>The original <see cref="IServiceCollection"/> for fluent chaining.</returns>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if the required logging configuration section is missing or invalid, ensuring a "fail-fast"
        /// behavior at application startup.
        /// </exception>
        public static IServiceCollection AddLoggingServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Bind the "Logging" section from appsettings.json to the LoggingOptions class.
            // This enables strongly-typed configuration and validation via DataAnnotations on the options class.
            // REQ-1-019, REQ-1-021: Makes log paths and rolling policies externally configurable.
            services.AddOptions<LoggingOptions>()
                .Bind(configuration.GetSection(LoggingOptions.SectionName))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            // Register Serilog as the logging provider.
            // The lambda provides a deferred execution context, allowing access to the IServiceProvider
            // which is necessary to resolve the IOptions<LoggingOptions> we just configured.
            services.AddSerilog((serviceProvider, loggerConfiguration) =>
            {
                // Resolve the strongly-typed logging options from the DI container.
                // Using GetRequiredService ensures the application will fail to start if configuration is missing.
                var loggingOptions = serviceProvider.GetRequiredService<IOptions<LoggingOptions>>().Value;

                // Delegate the complex task of building the Serilog logger configuration
                // to the dedicated LoggerFactory. This keeps the DI setup clean and adheres to SRP.
                // The factory encapsulates all Serilog-specific API calls.
                // This call configures file paths, JSON formatting, PII redaction, rolling file policies, etc.
                // Fulfills REQ-1-018, REQ-1-019, REQ-1-021, REQ-1-022.
                LoggerFactory.Configure(loggerConfiguration, loggingOptions);
            });

            return services;
        }
    }
}