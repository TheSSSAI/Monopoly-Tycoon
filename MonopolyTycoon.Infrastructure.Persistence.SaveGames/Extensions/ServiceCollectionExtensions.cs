using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonopolyTycoon.Application.Abstractions;
using MonopolyTycoon.Infrastructure.Persistence.SaveGames.Abstractions;
using MonopolyTycoon.Infrastructure.Persistence.SaveGames.Configuration;
using MonopolyTycoon.Infrastructure.Persistence.SaveGames.Services;

namespace MonopolyTycoon.Infrastructure.Persistence.SaveGames.Extensions;

/// <summary>
/// Provides extension methods for registering the save game persistence infrastructure services
/// with the <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the necessary services for save game persistence, including the repository,
    /// data migration manager, and path provider.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="configuration">The application's configuration, used to bind settings.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    /// <remarks>
    /// This method encapsulates the dependency injection setup for the entire
    /// MonopolyTycoon.Infrastructure.Persistence.SaveGames project. It follows the standard
    /// .NET pattern for creating modular, reusable configuration.
    /// </remarks>
    public static IServiceCollection AddSaveGamePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure the SaveGameSettings object using the Options Pattern, binding it
        // to the "Persistence:SaveGames" section of the configuration file (e.g., appsettings.json).
        // This makes the path provider configurable and environment-agnostic.
        services.Configure<SaveGameSettings>(configuration.GetSection("Persistence:SaveGames"));

        // Register the ISaveFilePathProvider as a Singleton. It is stateless and its
        // configuration is immutable for the application's lifetime, making Singleton the
        // most efficient lifetime.
        services.AddSingleton<ISaveFilePathProvider, SaveFilePathProvider>();

        // Register the IDataMigrationManager as Scoped. While it's currently stateless,
        // a Scoped lifetime provides flexibility for future enhancements and aligns
        // with the lifetime of its primary consumer, the GameSaveRepository.
        services.AddScoped<IDataMigrationManager, DataMigrationManager>();
        
        // Register the ISaveGameRepository as Scoped. This is the primary service exposed
        // by this project. A Scoped lifetime ensures a new instance per logical operation
        // (e.g., per user action in a desktop app), which is a safe default for repositories.
        services.AddScoped<ISaveGameRepository, GameSaveRepository>();

        return services;
    }
}