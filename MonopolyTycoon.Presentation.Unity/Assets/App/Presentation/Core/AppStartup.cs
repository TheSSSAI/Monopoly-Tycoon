using MonopolyTycoon.Application;
using MonopolyTycoon.Infrastructure;
using MonopolyTycoon.Presentation.Shared.Services;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MonopolyTycoon.Presentation.Core
{
    /// <summary>
    /// The main entry point for the application, acting as the Composition Root.
    /// This MonoBehaviour initializes the Dependency Injection (DI) container, registers all
    /// services from all architectural layers, sets up global systems like exception handling,
    * and kicks off the initial UI flow.
    /// It is designed as a singleton to ensure it runs only once.
    /// </summary>
    [DefaultExecutionOrder(-100)]
    public sealed class AppStartup : MonoBehaviour
    {
        private static AppStartup _instance;

        /// <summary>
        /// Public static access to the resolved DI container.
        /// Should be used sparingly, primarily for integration with systems
        /// that do not support constructor injection easily.
        /// </summary>
        public static IObjectResolver Container { get; private set; }

        /// <summary>
        /// Unity's Awake method, used here to perform the entire application setup.
        /// This runs before any other script's Start method.
        /// </summary>
        private void Awake()
        {
            // Enforce the singleton pattern. If another instance exists, destroy this one.
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);

            try
            {
                // 1. Create the DI container builder.
                var builder = new ContainerBuilder();

                // 2. Register installers from all layers. The order is important:
                //    - Application defines the core use cases and abstractions.
                //    - Infrastructure provides concrete implementations for those abstractions.
                //    - Presentation registers all UI-related services and presenters.
                builder.RegisterInstaller(new ApplicationInstaller());
                builder.RegisterInstaller(new InfrastructureInstaller());
                builder.RegisterInstaller(new PresentationInstaller());

                // 3. Build the container to create an IObjectResolver.
                Container = builder.Build();

                // 4. Initialize critical global systems that depend on the container.
                InitializeGlobalSystems();

                Debug.Log("Application startup and DI container setup complete.");
            }
            catch (Exception ex)
            {
                // If anything goes wrong during this critical setup phase, the application
                // is in an unrecoverable state. Log a fatal error and quit.
                Debug.LogError($"[AppStartup] A fatal error occurred during application initialization: {ex.Message}\n{ex.StackTrace}");
                // In a real build, we might show a native OS error dialog before quitting.
                Application.Quit();
            }
        }

        /// <summary>
        /// Unity's Start method. Called after all Awake methods have completed.
        /// This is the safest place to initiate the first application action.
        /// </summary>
        private void Start()
        {
            try
            {
                // 5. Resolve the top-level UI manager and show the first screen.
                // This officially hands off control from the Composition Root to the application's UI flow.
                var viewManager = Container.Resolve<IViewManager>();
                viewManager.ShowScreen(Screen.MainMenu);
            }
            catch (Exception ex)
            {
                Debug.LogError($"[AppStartup] Failed to start main menu: {ex.Message}\n{ex.StackTrace}");
                // The GlobalExceptionHandler should catch this in a real scenario if it was successfully initialized.
                // If not, we still quit to prevent a broken state.
                Application.Quit();
            }
        }

        /// <summary>
        /// Instantiates and registers essential, long-lived systems that are required
        /// for the entire application lifecycle.
        /// </summary>
        private void InitializeGlobalSystems()
        {
            // The GlobalExceptionHandler is a critical component for application stability.
            // We resolve it here to ensure it's created and can register its hooks
            // immediately after the container is built.
            var exceptionHandler = Container.Resolve<GlobalExceptionHandler>();
            exceptionHandler.Register();
            Debug.Log("Global Exception Handler registered.");

            // Other global systems could be initialized here if needed.
            // For example, a persistent audio manager or input controller that
            // is not tied to a specific scene's lifetime.
            // In our architecture, InputController is a MonoBehaviour managed by Unity,
            // but we could resolve it here to ensure its instance is ready if needed.
        }
    }
}