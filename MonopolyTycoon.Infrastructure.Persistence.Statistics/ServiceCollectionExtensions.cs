using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonopolyTycoon.Application.Abstractions.Persistence;
using MonopolyTycoon.Infrastructure.Persistence.Statistics.Configuration;
using MonopolyTycoon.Infrastructure.Persistence.Statistics.Exceptions;

namespace MonopolyTycoon.Infrastructure.Persistence.Statistics;

/// <summary>
/// Contains extension methods for setting up the statistics persistence layer in the dependency injection container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the SQLite-based statistics and player profile repository and its required configuration.
    /// This method encapsulates all the DI setup needed for this infrastructure component.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="configuration">The application's configuration, used to bind persistence options.</param>
    /// <returns>The same <see cref="IServiceCollection"/> so that multiple calls can be chained.</returns>
    /// <remarks>
    /// This method registers <see cref="SqliteStatisticsRepository"/> as a singleton because it manages a single,
    /// long-lived file resource (the SQLite database) and handles its initialization and backup lifecycle.
    /// </remarks>
    public static IServiceCollection AddStatisticsPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure the StatisticsPersistenceOptions from the "Persistence:Statistics" section of appsettings.json
        // This utilizes the Options pattern to provide strongly-typed configuration to the repository.
        // It helps decouple the repository from the configuration source itself.
        services.Configure<StatisticsPersistenceOptions>(configuration.GetSection(StatisticsPersistenceOptions.SectionName));

        // Register the SqliteStatisticsRepository as a singleton.
        // A singleton lifetime is crucial here because the repository manages a persistent file resource,
        // performs one-time initialization (database creation, schema validation, backup/recovery),
        // and should maintain a consistent state for the application's lifetime.
        services.AddSingleton<SqliteStatisticsRepository>();
        
        // Register the singleton instance to serve both IStatisticsRepository and IPlayerProfileRepository interfaces.
        // This ensures that any service requesting either interface will receive the same, single instance
        // of the SqliteStatisticsRepository, preventing multiple initializations or file access conflicts.
        // The factory function `sp => sp.GetRequiredService<SqliteStatisticsRepository>()` resolves the already-registered
        // singleton instance.
        services.AddSingleton<IStatisticsRepository>(sp => sp.GetRequiredService<SqliteStatisticsRepository>());
        services.AddSingleton<IPlayerProfileRepository>(sp => sp.GetRequiredService<SqliteStatisticsRepository>());

        return services;
    }
}