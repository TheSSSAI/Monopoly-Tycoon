using MonopolyTycoon.Application.Services;
using MonopolyTycoon.Infrastructure.Logging;
using MonopolyTycoon.Infrastructure.Repositories;
using MonopolyTycoon.Presentation.Core;
using MonopolyTycoon.Presentation.Features.CommonUI.Presenters;
using MonopolyTycoon.Presentation.Features.GameBoard.Presenters;
using MonopolyTycoon.Presentation.Features.MainMenu.Presenters;
using Microsoft.Extensions.DependencyInjection;
using System;
using UnityEngine;
using MonopolyTycoon.Application.Abstractions;
using MonopolyTycoon.Infrastructure.Abstractions;

/// <summary>
/// The Composition Root of the application, responsible for initializing and configuring
/// the dependency injection container. This is the first script to run.
/// </summary>
[DefaultExecutionOrder(-100)]
public class CompositionRoot : MonoBehaviour
{
    #region Singleton
    private static CompositionRoot _instance;

    public static IServiceProvider ServiceProvider { get; private set; }

    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        ConfigureServices();
    }
    #endregion

    #region DI Container Setup
    private void ConfigureServices()
    {
        try
        {
            var services = new ServiceCollection();
            
            RegisterInfrastructureServices(services);
            RegisterApplicationServices(services);
            RegisterPresentationServices(services);

            ServiceProvider = services.BuildServiceProvider();

            // Resolve and initialize persistent services
            InitializePersistentServices();

            // Start the application flow
            var viewManager = ServiceProvider.GetRequiredService<IViewManager>();
            viewManager.LoadSceneAsync("MainMenu").Forget();
        }
        catch (Exception e)
        {
            Debug.LogError($"[CompositionRoot] Failed to build Service Provider: {e.Message}\n{e.StackTrace}");
            // Application cannot run without a valid service provider
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }

    private void RegisterInfrastructureServices(IServiceCollection services)
    {
        // Logging
        services.AddSingleton<ILogger>(sp =>
        {
            var logger = new SerilogLoggingService();
            logger.Initialize();
            return logger;
        });

        // Repositories
        services.AddSingleton<ISaveGameRepository, GameSaveRepository>();
        services.AddSingleton<IStatisticsRepository, StatisticsRepository>();
        services.AddSingleton<IPlayerProfileRepository, PlayerProfileRepository>();
        services.AddSingleton<IAIConfigurationRepository, AIConfigurationRepository>();
        services.AddSingleton<IRulebookRepository, RulebookRepository>();
    }

    private void RegisterApplicationServices(IServiceCollection services)
    {
        services.AddSingleton<IGameSessionService, GameSessionService>();
        services.AddSingleton<ITurnManagementService, TurnManagementService>();
        services.AddSingleton<ITradeOrchestrationService, TradeOrchestrationService>();
        services.AddSingleton<IPropertyActionService, PropertyActionService>();
    }

    private void RegisterPresentationServices(IServiceCollection services)
    {
        // Core Presentation Services - These are often MonoBehaviours attached to this GameObject.
        // We find them in the scene and register the instance.
        var viewManager = GetComponentInChildren<IViewManager>();
        if (viewManager != null) services.AddSingleton(viewManager);
        
        var audioService = GetComponentInChildren<IAudioService>();
        if (audioService != null) services.AddSingleton(audioService);
        
        var inputController = GetComponentInChildren<InputController>();
        if (inputController != null) services.AddSingleton(inputController);

        // Core Non-MonoBehaviour Services
        services.AddSingleton<IEventBus, EventBus>();

        // Presenters - These are typically transient as they are tied to a view's lifecycle.
        services.AddTransient<MainMenuPresenter>();
        services.AddTransient<LoadGamePresenter>();
        services.AddTransient<HUDPresenter>();
        services.AddTransient<GameBoardPresenter>();
        services.AddTransient<PropertyManagementPresenter>();
        services.AddTransient<ErrorDialogPresenter>();
        // Add other presenters here as they are created
    }
    
    private void InitializePersistentServices()
    {
        // The GlobalExceptionHandler needs to be created and registered to catch exceptions.
        // It's a MonoBehaviour, so we add it to our persistent GameObject.
        var exceptionHandler = gameObject.AddComponent<GlobalExceptionHandler>();
        var viewManager = ServiceProvider.GetRequiredService<IViewManager>();
        var logger = ServiceProvider.GetRequiredService<ILogger>();
        exceptionHandler.Initialize(viewManager, logger);

        // Input Controller is another persistent service.
        var inputController = ServiceProvider.GetRequiredService<InputController>();
        var eventBus = ServiceProvider.GetRequiredService<IEventBus>();
        inputController.Initialize(eventBus);
    }
    #endregion
}

// Simple extension for async void fire-and-forget pattern with error logging.
public static class TaskExtensions
{
    public static async void Forget(this System.Threading.Tasks.Task task)
    {
        try
        {
            await task;
        }
        catch (Exception e)
        {
            Debug.LogError($"[TaskExtensions.Forget] Unhandled exception in async void task: {e}");
        }
    }
}