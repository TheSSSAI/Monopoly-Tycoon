using Microsoft.Extensions.DependencyInjection;
using MonopolyTycoon.Application.Abstractions.Configuration;
using MonopolyTycoon.Infrastructure.Configuration.Providers;
using System;
using System.Diagnostics.CodeAnalysis;

namespace MonopolyTycoon.Infrastructure.Configuration.Extensions;

/// <summary>
/// Extension methods for setting up configuration services in an <see cref="IServiceCollection" />.
/// </summary>
[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the <see cref="JsonConfigurationProvider"/> as a singleton implementation of <see cref="IConfigurationProvider"/>.
    /// This allows the application to load strongly-typed configuration objects from JSON files.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="services"/> argument is null.</exception>
    public static IServiceCollection AddConfigurationServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        // The JsonConfigurationProvider is stateless and thread-safe, making it an ideal candidate
        // for a Singleton lifetime. This ensures a single instance is created and reused throughout
        // the application's lifetime, which is the most performant and memory-efficient approach.
        services.AddSingleton<IConfigurationProvider, JsonConfigurationProvider>();

        return services;
    }
}