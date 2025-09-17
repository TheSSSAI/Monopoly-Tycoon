using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MonopolyTycoon.Application.Abstractions.Services;
using MonopolyTycoon.Application.Services.Services;
using MonopolyTycoon.Application.Services.Validation;
using System.Reflection;

namespace MonopolyTycoon.Application.Services;

/// <summary>
/// Extension methods for setting up application services in an <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds all application-layer services to the specified <see cref="IServiceCollection"/>.
    /// This method registers use case orchestrators and validators, providing a single point of configuration
    /// for the Application Services layer.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register FluentValidation validators from this assembly
        // This automatically discovers and registers all classes inheriting from AbstractValidator
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Register Application Services
        // Services are registered with a Scoped lifetime, meaning a new instance is created for each logical
        // game session (or scope), ensuring that in-memory state like CurrentGameState is maintained for
        // the duration of a session but isolated from others.

        // Session & Turn Management
        services.AddScoped<IGameSessionService, GameSessionService>();
        services.AddScoped<ITurnManagementService, TurnManagementService>();

        // Player Interaction Orchestrators
        services.AddScoped<ITradeOrchestrationService, TradeOrchestrationService>();
        services.AddScoped<IAuctionOrchestrationService, AuctionOrchestrationService>();
        services.AddScoped<IPropertyActionService, PropertyActionService>();

        // AI Coordination
        services.AddScoped<IAIService, AIService>();
        
        // Data and Profile Management
        services.AddScoped<IUserDataManagementService, UserDataManagementService>();
        services.AddScoped<IStatisticsQueryService, StatisticsQueryService>();
        
        return services;
    }
}