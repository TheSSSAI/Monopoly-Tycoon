using Microsoft.Extensions.DependencyInjection;
using MonopolyTycoon.Domain.RuleEngine.Interfaces;
using MonopolyTycoon.Domain.RuleEngine.Services;
using System;
using System.Diagnostics.CodeAnalysis;

namespace MonopolyTycoon.Domain.RuleEngine.DependencyInjection
{
    /// <summary>
    /// Provides extension methods for registering domain rule engine services with the .NET dependency injection container.
    /// This class encapsulates the registration logic, making it easy for the consuming application to configure the necessary services.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the core, stateless services of the MonopolyTycoon.Domain.RuleEngine library with the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        /// <remarks>
        /// This method registers the following services with a Singleton lifetime, which is optimal for their stateless and thread-safe design:
        /// - <see cref="IRuleEngine"/> as <see cref="RuleEngine"/>: The central engine for validating game rules and applying state transitions.
        /// - <see cref="IDiceRoller"/> as <see cref="DiceRoller"/>: The service for generating cryptographically secure dice rolls.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="services"/> argument is null.</exception>
        public static IServiceCollection AddDomainRuleEngine(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            // Register the RuleEngine as a singleton. It is designed to be a stateless service,
            // containing only pure functions that operate on the game state passed into them.
            // A singleton lifetime ensures maximum performance by avoiding repeated allocations.
            services.AddSingleton<IRuleEngine, RuleEngine>();

            // Register the DiceRoller as a singleton. It is also a stateless, thread-safe service.
            // A singleton ensures that the underlying RandomNumberGenerator instance is created
            // only once, which is a performance best practice.
            services.AddSingleton<IDiceRoller, DiceRoller>();

            return services;
        }
    }
}