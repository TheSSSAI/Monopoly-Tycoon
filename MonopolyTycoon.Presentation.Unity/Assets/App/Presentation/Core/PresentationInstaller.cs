using MonopolyTycoon.Presentation.Events;
using MonopolyTycoon.Presentation.Features.CommonUI.Presenters;
using MonopolyTycoon.Presentation.Features.GameBoard.Presenters;
using MonopolyTycoon.Presentation.Features.HUD.Presenters;
using MonopolyTycoon.Presentation.Features.MainMenu.Presenters;
using MonopolyTycoon.Presentation.Features.PropertyManagement.Presenters;
using MonopolyTycoon.Presentation.Shared.Services;
using Zenject;

namespace MonopolyTycoon.Presentation.Core
{
    /// <summary>
    /// The PresentationInstaller is responsible for binding all the presentation-layer services,
    /// presenters, and other components into the dependency injection container. This is a core
    /// part of the Composition Root, ensuring that all dependencies within the Presentation layer
    /// are correctly resolved at runtime.
    /// This installer is executed by the AppStartup process.
    /// </summary>
    public class PresentationInstaller : Installer<PresentationInstaller>
    {
        public override void InstallBindings()
        {
            InstallCoreServices();
            InstallGlobalComponents();
            InstallFeaturePresenters();
        }

        /// <summary>
        /// Binds core, application-wide services that are essential for the Presentation layer to function.
        /// These services are typically singletons, maintaining their state across the entire application lifecycle.
        /// </summary>
        private void InstallCoreServices()
        {
            // The EventBus is the central messaging system for decoupled communication between components.
            // It's a singleton to ensure a single, consistent communication channel.
            // Fulfills integration pattern seen in Sequence Diagrams 181, 184, 186.
            Container.Bind<IEventBus>().To<EventBus>().AsSingle();

            // The ViewManager handles the loading, showing, and hiding of UI screens and modals.
            // It's a singleton to manage the global UI state and navigation stack.
            // Fulfills REQ-1-071 and is a key component in REQ-1-023 (error dialog).
            Container.Bind<IViewManager>().To<ViewManager>().AsSingle();

            // The AddressableAssetProvider handles dynamic loading of assets (prefabs, themes, etc.).
            // It's a singleton to manage asset caching and prevent redundant loading.
            // Fulfills REQ-1-093 (Theme System).
            Container.Bind<IAssetProvider>().To<AddressableAssetProvider>().AsSingle();

            // The AudioService manages playback of music and sound effects throughout the application.
            // It's a singleton to provide consistent audio control and state (e.g., volume levels).
            // Fulfills REQ-1-094 and settings requirement REQ-1-079.
            Container.Bind<IAudioService>().To<AudioService>().AsSingle();
        }

        /// <summary>
        /// Binds global components that are always active, such as the exception handler and input controller.
        /// </summary>
        private void InstallGlobalComponents()
        {
            // The GlobalExceptionHandler catches all unhandled exceptions to prevent crashes.
            // It's a singleton to ensure it's registered once at startup and remains active.
            // Fulfills REQ-1-023 as detailed in Sequence Diagram 192.
            Container.Bind<GlobalExceptionHandler>().AsSingle();

            // The InputController translates raw hardware input into application-level events.
            // It is bound from a component in the scene to integrate with Unity's Input System.
            // This is a singleton-like pattern for a MonoBehaviour.
            Container.Bind<InputController>().FromComponentInHierarchy().AsSingle();
        }

        /// <summary>
        /// Binds the presenters for each specific UI feature/screen.
        /// Presenters are typically bound as transient, meaning a new instance is created
        /// each time it's needed. This ensures they don't hold stale state between
        /// different instances of their associated views.
        /// </summary>
        private void InstallFeaturePresenters()
        {
            // Presenter for the main menu, handling navigation to other screens.
            // Directly supports US-008.
            Container.Bind<MainMenuPresenter>().AsTransient();

            // Presenter for the load game screen, handling display of save slots.
            // Supports US-062 and graceful error handling in US-063, as shown in Sequence Diagram 185.
            Container.Bind<LoadGamePresenter>().AsTransient();

            // Presenter for the main game board, orchestrating token animations and property visuals.
            // Fulfills REQ-1-005 and REQ-1-017.
            Container.Bind<GameBoardPresenter>().AsTransient();
            
            // Presenter for the Heads-Up Display, showing real-time player stats.
            // Fulfills REQ-1-071 and US-049.
            Container.Bind<HUDPresenter>().AsTransient();

            // Presenter for the property management screen, handling build/mortgage actions.
            // Supports user stories US-033, US-038, US-052 and Sequence Diagrams 179, 180.
            Container.Bind<PropertyManagementPresenter>().AsTransient();

            // Presenter for the reusable error dialog.
            // Though simple, binding it allows for easier testing and management.
            // Supports REQ-1-023.
            Container.Bind<ErrorDialogPresenter>().AsTransient();
        }
    }
}