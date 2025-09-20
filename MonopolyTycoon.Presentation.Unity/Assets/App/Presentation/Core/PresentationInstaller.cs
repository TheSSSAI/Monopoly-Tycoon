using MonopolyTycoon.Presentation.Core;
using MonopolyTycoon.Presentation.Features.GameBoard.Presenters;
using MonopolyTycoon.Presentation.Features.HUD.Presenters;
using MonopolyTycoon.Presentation.Features.MainMenu.Presenters;
using MonopolyTycoon.Presentation.Features.PropertyManagement.Presenters;
using MonopolyTycoon.Presentation.Features.Trading.Presenters;
using MonopolyTycoon.Presentation.Shared.Services;
using Zenject;

namespace MonopolyTycoon.Presentation.Core
{
    /// <summary>
    /// Zenject installer for the Presentation layer.
    /// This class is responsible for binding all the presentation-layer services,
    /// presenters, and controllers into the Dependency Injection container.
    /// It is executed by a Zenject SceneContext or ProjectContext at application startup.
    /// </summary>
    public class PresentationInstaller : Installer<PresentationInstaller>
    {
        public override void InstallBindings()
        {
            // The bindings in this installer set up the contracts and implementations for the
            // entire Presentation layer, following the Model-View-Presenter (MVP) pattern and
            // Clean Architecture principles. It ensures that components are decoupled and
            // can be resolved at runtime.

            BindSharedServices();
            BindGlobalHandlersAndControllers();
            BindFeaturePresenters();
        }

        /// <summary>
        /// Binds shared services that are used across multiple presentation features.
        /// These are typically long-lived, global services.
        /// </summary>
        private void BindSharedServices()
        {
            // IViewManager is responsible for orchestrating the display of different UI screens.
            // It's a singleton because there should only be one manager controlling the UI state
            // for the entire application lifecycle.
            // REQ-1-071: Manages all UI elements.
            Container.Bind<IViewManager>().To<ViewManager>().AsSingle();

            // IAssetProvider abstracts the asset loading mechanism. Using Addressables allows
            // for dynamic, theme-based asset loading without hardcoding references.
            // It's a singleton to manage asset caching and prevent redundant loading.
            // REQ-1-093: Supports the theme system by providing addressable assets.
            Container.Bind<IAssetProvider>().To<AddressableAssetProvider>().AsSingle();
        }

        /// <summary>
        /// Binds global handlers and controllers that manage application-wide concerns
        /// like input and exception handling.
        /// </summary>
        private void BindGlobalHandlersAndControllers()
        {
            // GlobalExceptionHandler catches any unhandled exceptions to prevent the application
            // from crashing, instead displaying a user-friendly error dialog.
            // It's a singleton to ensure it's registered once at startup and persists.
            // REQ-1-023: Implements the modal error dialog display on unhandled exceptions.
            Container.Bind<GlobalExceptionHandler>().AsSingle();

            // InputController centralizes user input from Unity's Input System.
            // It's bound FromComponentInHierarchy because it's a MonoBehaviour that must
            // exist in the scene to receive Unity's input events. It's a singleton
            // ensuring a single source of input translation for the application.
            // Note: This requires an InputController component to be present on a GameObject
            // within the Zenject scene context.
            Container.Bind<InputController>().FromComponentInHierarchy().AsSingle();
        }

        /// <summary>
        /// Binds all feature-specific presenters.
        /// Presenters contain the presentation logic that drives the views. They are bound
        /// as Transient because their lifecycle is typically tied to the view they manage.
        /// A new presenter is created each time a view is shown or needs a presenter.
        /// </summary>
        private void BindFeaturePresenters()
        {
            // GameBoardPresenter manages the 3D game board visuals, including token movement and property markers.
            // Fulfills REQ-1-005, REQ-1-017, REQ-1-050.
            Container.BindInterfacesAndSelfTo<GameBoardPresenter>().AsTransient();

            // HUDPresenter manages the Heads-Up Display, showing player cash, turn indicators, etc.
            // Fulfills REQ-1-071.
            Container.BindInterfacesAndSelfTo<HUDPresenter>().AsTransient();

            // PropertyManagementPresenter handles the logic for the property management screen (building, mortgaging).
            // Fulfills requirements related to US-052, US-033, US-038.
            Container.BindInterfacesAndSelfTo<PropertyManagementPresenter>().AsTransient();

            // TradePresenter orchestrates the trade UI and interactions between the player and AI.
            // Fulfills requirements related to US-040, US-041, US-042, US-053.
            Container.BindInterfacesAndSelfTo<TradePresenter>().AsTransient();

            // GameSetupPresenter manages the configuration of a new game.
            // Fulfills requirements related to US-009, US-010, US-011, US-014.
            Container.BindInterfacesAndSelfTo<GameSetupPresenter>().AsTransient();

            // LoadGamePresenter handles the logic for the load game screen, including displaying save metadata.
            // Fulfills requirements related to US-062, US-063.
            Container.BindInterfacesAndSelfTo<LoadGamePresenter>().AsTransient();

            // MainMenuPresenter handles the navigation logic for the main menu screen.
            // Fulfills requirements related to US-008.
            Container.BindInterfacesAndSelfTo<MainMenuPresenter>().AsTransient();
        }
    }
}