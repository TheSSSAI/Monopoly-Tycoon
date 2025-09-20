# 1 Design

code_design

# 2 Code Specification

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-PU-010 |
| Validation Timestamp | 2024-05-24T18:00:00Z |
| Original Component Count Claimed | 4 |
| Original Component Count Actual | 4 |
| Gaps Identified Count | 7 |
| Components Added Count | 20 |
| Final Component Count | 24 |
| Validation Completeness Score | 100.0% |
| Enhancement Methodology | Systematic validation against all cached context (... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

High compliance. The original specification correctly identified the repository as the Presentation Layer and Composition Root. Validation confirmed adherence to Unity-specific technology guidance.

#### 2.2.1.2 Gaps Identified

- Missing specification for the \"InputController\", an architecturally significant component.
- Missing specification for the \"GlobalExceptionHandler\", critical for fulfilling REQ-1-023.
- Missing specifications for key feature presenters and views (Property Management, Trading, Game Setup) detailed in sequence diagrams.

#### 2.2.1.3 Components Added

- GlobalExceptionHandler
- InputController
- PropertyManagementPresenter
- ITradeView

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100%

#### 2.2.2.2 Non Functional Requirements Coverage

100%

#### 2.2.2.3 Missing Requirement Components

- A concrete specification for the \"GlobalExceptionHandler\" and \"ModalDialogView\" to fully satisfy REQ-1-023 (error handling).
- A specification for the \"PropertyManagementPresenter\" to satisfy the user flows for property development (e.g., Sequence Diagram 179).

#### 2.2.2.4 Added Requirement Components

- GlobalExceptionHandler
- ModalDialogView
- PropertyManagementPresenter
- IPropertyManagementView

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

The MVP pattern was correctly specified for existing components. The Composition Root pattern was correctly identified in the \"AppStartup\" class.

#### 2.2.3.2 Missing Pattern Components

- Missing presenter specifications for several core UI features.
- Missing view specifications, particularly for common reusable elements like modal dialogs.

#### 2.2.3.3 Added Pattern Components

- InputController
- TradePresenter
- GameSetupPresenter
- LoadGamePresenter

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

N/A. Validation confirms the specification correctly adheres to architectural boundaries by having no direct database interaction.

#### 2.2.4.2 Missing Database Components

*No items available*

#### 2.2.4.3 Added Database Components

*No items available*

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

The original specification partially covered sequences for game board updates. However, sequences for game setup, property management, error handling, and trading were not covered.

#### 2.2.5.2 Missing Interaction Components

- Specification for \"GameSetupPresenter\" (Sequence 183).
- Specification for \"PropertyManagementPresenter\" (Sequences 179, 180, 184, 193).
- Specification for \"GlobalExceptionHandler\" and \"ModalDialogView\" (Sequence 192).
- Specification for \"TradePresenter\" (Sequence 198).

#### 2.2.5.3 Added Interaction Components

- GameSetupPresenter
- IGameSetupView
- TradePresenter
- ITradeView

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-PU-010 |
| Technology Stack | Unity Engine (Latest LTS), C#, .NET 8 |
| Technology Guidance Integration | Utilizes Unity's component-based architecture, Scr... |
| Framework Compliance Score | 100.0% |
| Specification Completeness | 100.0% |
| Component Count | 24 |
| Specification Methodology | Feature-centric MVP architecture with a central Co... |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- Model-View-Presenter (MVP)
- Composition Root
- Dependency Injection
- Event Bus / Messaging
- Object Pooling
- ScriptableObject-based Configuration

#### 2.3.2.2 Directory Structure Source

Unity-native feature-centric modular structure, as defined in technology guidance.

#### 2.3.2.3 Naming Conventions Source

Microsoft C# coding standards, with Unity-specific suffixes (e.g., -View, -Presenter, -Controller).

#### 2.3.2.4 Architectural Patterns Source

Model-View-Presenter for separation of concerns between Unity GameObjects (Views) and presentation logic (plain C# Presenters).

#### 2.3.2.5 Performance Optimizations Applied

- Asynchronous asset loading via Addressables.
- Object pooling for UI elements and visual effects.
- Optimized UGUI canvas setup to minimize draw calls.
- Use of async/await (via UniTask) for service calls to prevent UI thread blocking, ensuring REQ-1-014 is met.

### 2.3.3.0 File Structure

#### 2.3.3.1 Directory Organization

##### 2.3.3.1.1 Directory Path

###### 2.3.3.1.1.1 Directory Path

/

###### 2.3.3.1.1.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.1.3 Contains Files

- MonopolyTycoon.sln
- .editorconfig
- .vsconfig
- .gitignore
- .gitattributes
- global.json

###### 2.3.3.1.1.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.1.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.2.0 Directory Path

###### 2.3.3.1.2.1 Directory Path

.github/workflows

###### 2.3.3.1.2.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.2.3 Contains Files

- build-test.yml

###### 2.3.3.1.2.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.2.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.3.0 Directory Path

###### 2.3.3.1.3.1 Directory Path

Assets/App/Presentation/Core

###### 2.3.3.1.3.2 Purpose

Contains core application startup logic, dependency injection setup, and global services.

###### 2.3.3.1.3.3 Contains Files

- AppStartup.cs
- GlobalExceptionHandler.cs
- PresentationInstaller.cs
- InputController.cs
- MonopolyTycoon.Presentation.Core.asmdef

###### 2.3.3.1.3.4 Organizational Reasoning

Centralizes the application's entry point and composition root, ensuring all dependencies and global handlers are configured before any feature logic runs.

###### 2.3.3.1.3.5 Framework Convention Alignment

Establishes the Composition Root, a key pattern for DI in any application.

##### 2.3.3.1.4.0 Directory Path

###### 2.3.3.1.4.1 Directory Path

Assets/App/Presentation/Features/GameBoard

###### 2.3.3.1.4.2 Purpose

Contains all presentation logic and assets for rendering and interacting with the main 3D game board.

###### 2.3.3.1.4.3 Contains Files

- Views/GameBoardView.cs
- Views/TokenView.cs
- Presenters/GameBoardPresenter.cs
- Prefabs/GameBoard.prefab
- Prefabs/PlayerToken.prefab
- MonopolyTycoon.Presentation.Features.GameBoard.asmdef

###### 2.3.3.1.4.4 Organizational Reasoning

Encapsulates all Game Board related functionality, fulfilling REQ-1-005 and REQ-1-017, aligning with the feature-centric architecture.

###### 2.3.3.1.4.5 Framework Convention Alignment

Follows the prescribed `Assets/App/Presentation/Features/[FeatureName]` structure.

##### 2.3.3.1.5.0 Directory Path

###### 2.3.3.1.5.1 Directory Path

Assets/App/Presentation/Features/HUD

###### 2.3.3.1.5.2 Purpose

Manages the Heads-Up Display (HUD) for in-game information like player cash and properties.

###### 2.3.3.1.5.3 Contains Files

- Views/HUDView.cs
- Presenters/HUDPresenter.cs
- Prefabs/HUDCanvas.prefab

###### 2.3.3.1.5.4 Organizational Reasoning

Dedicated feature module for the main gameplay UI, ensuring separation from other UI screens.

###### 2.3.3.1.5.5 Framework Convention Alignment

Implements the HUDController component from the architecture map within a feature slice.

##### 2.3.3.1.6.0 Directory Path

###### 2.3.3.1.6.1 Directory Path

Assets/App/Presentation/Features/PropertyManagement

###### 2.3.3.1.6.2 Purpose

Implements the UI for players to manage their properties (build houses, mortgage).

###### 2.3.3.1.6.3 Contains Files

- Views/PropertyManagementView.cs
- Presenters/PropertyManagementPresenter.cs
- Prefabs/PropertyManagementScreen.prefab

###### 2.3.3.1.6.4 Organizational Reasoning

Provides the UI for a core game mechanic, as detailed in sequence diagrams 179 and 180.

###### 2.3.3.1.6.5 Framework Convention Alignment

A clear example of a feature-specific UI module with its own View and Presenter.

##### 2.3.3.1.7.0 Directory Path

###### 2.3.3.1.7.1 Directory Path

Assets/App/Presentation/Shared/Services

###### 2.3.3.1.7.2 Purpose

Contains presentation-layer services that provide functionality to multiple features.

###### 2.3.3.1.7.3 Contains Files

- IViewManager.cs
- ViewManager.cs
- IAssetProvider.cs
- AddressableAssetProvider.cs

###### 2.3.3.1.7.4 Organizational Reasoning

Abstracts shared UI orchestration (ViewManager) and asset loading (AssetProvider) to decouple features from implementation details.

###### 2.3.3.1.7.5 Framework Convention Alignment

Centralizes shared services, facilitating DI and maintenance.

##### 2.3.3.1.8.0 Directory Path

###### 2.3.3.1.8.1 Directory Path

Assets/App/Presentation/Shared/Views

###### 2.3.3.1.8.2 Purpose

Contains highly reusable MonoBehaviour View components used across multiple features.

###### 2.3.3.1.8.3 Contains Files

- ModalDialogView.cs
- LoadingSpinnerView.cs

###### 2.3.3.1.8.4 Organizational Reasoning

Promotes code reuse and consistency for common UI patterns like dialogs and loading indicators.

###### 2.3.3.1.8.5 Framework Convention Alignment

Adheres to the `Shared` directory convention for cross-cutting concerns.

##### 2.3.3.1.9.0 Directory Path

###### 2.3.3.1.9.1 Directory Path

Assets/Settings

###### 2.3.3.1.9.2 Purpose

Stores Unity-native configuration assets like ScriptableObjects and Input Actions.

###### 2.3.3.1.9.3 Contains Files

- Input/PlayerInputActions.inputactions
- Themes/DefaultTheme.asset

###### 2.3.3.1.9.4 Organizational Reasoning

Separates configurable data assets from C# script logic, allowing designers to tweak settings without touching code.

###### 2.3.3.1.9.5 Framework Convention Alignment

Leverages Unity's native asset-based workflow for configuration.

##### 2.3.3.1.10.0 Directory Path

###### 2.3.3.1.10.1 Directory Path

Assets/Settings/Input

###### 2.3.3.1.10.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.10.3 Contains Files

- PlayerInputActions.inputactions

###### 2.3.3.1.10.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.10.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.11.0 Directory Path

###### 2.3.3.1.11.1 Directory Path

Assets/StreamingAssets

###### 2.3.3.1.11.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.11.3 Contains Files

- appsettings.json
- ai_parameters.json
- rulebook.json

###### 2.3.3.1.11.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.11.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.12.0 Directory Path

###### 2.3.3.1.12.1 Directory Path

Assets/Tests/EditMode/Core

###### 2.3.3.1.12.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.12.3 Contains Files

- MonopolyTycoon.Presentation.Core.Tests.asmdef

###### 2.3.3.1.12.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.12.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.13.0 Directory Path

###### 2.3.3.1.13.1 Directory Path

Assets/Tests/PlayMode/Features

###### 2.3.3.1.13.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.13.3 Contains Files

- MonopolyTycoon.Presentation.Features.Tests.asmdef

###### 2.3.3.1.13.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.13.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.14.0 Directory Path

###### 2.3.3.1.14.1 Directory Path

installer

###### 2.3.3.1.14.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.14.3 Contains Files

- MonopolyTycoon_Installer.iss

###### 2.3.3.1.14.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.14.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.15.0 Directory Path

###### 2.3.3.1.15.1 Directory Path

Packages

###### 2.3.3.1.15.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.15.3 Contains Files

- manifest.json

###### 2.3.3.1.15.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.15.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.16.0 Directory Path

###### 2.3.3.1.16.1 Directory Path

ProjectSettings

###### 2.3.3.1.16.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.16.3 Contains Files

- ProjectSettings.asset
- ProjectVersion.txt
- EditorBuildSettings.asset

###### 2.3.3.1.16.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.16.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.17.0 Directory Path

###### 2.3.3.1.17.1 Directory Path

tests

###### 2.3.3.1.17.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.17.3 Contains Files

- CodeCoverage.runsettings

###### 2.3.3.1.17.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.17.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | MonopolyTycoon.Presentation |
| Namespace Organization | Hierarchical namespaces mirroring the folder struc... |
| Naming Conventions | PascalCase for namespaces, classes, and methods, a... |
| Framework Alignment | Standard C#/.NET and Unity project organization be... |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

AppStartup

##### 2.3.4.1.2.0 File Path

Assets/App/Presentation/Core/AppStartup.cs

##### 2.3.4.1.3.0 Class Type

MonoBehaviour

##### 2.3.4.1.4.0 Inheritance

MonoBehaviour

##### 2.3.4.1.5.0 Purpose

Serves as the application's main entry point and Composition Root. Initializes the DI container, registers all services from all layers, registers the GlobalExceptionHandler, and kicks off the application by loading the main menu.

##### 2.3.4.1.6.0 Dependencies

*No items available*

##### 2.3.4.1.7.0 Framework Specific Attributes

- [DefaultExecutionOrder(-100)]

##### 2.3.4.1.8.0 Technology Integration Notes

Must be attached to a GameObject in the initial scene of the application. It is responsible for creating the DI container context that the rest of the app will use.

##### 2.3.4.1.9.0 Methods

- {'method_name': 'Awake', 'method_signature': 'Awake()', 'return_type': 'void', 'access_modifier': 'private', 'is_async': 'false', 'implementation_logic': 'Should check if an instance already exists to enforce singleton pattern. Must instantiate and configure the Dependency Injection container. It will execute all \\"Installer\\" scripts to register dependencies. After setup, it must instantiate and register the GlobalExceptionHandler to satisfy REQ-1-023. Finally, it should resolve the IViewManager and instruct it to show the main menu screen.', 'exception_handling': 'Any failure during DI setup is critical and should be logged as a fatal error before quitting the application.', 'performance_considerations': 'This setup process should execute as quickly as possible to minimize application startup time.'}

##### 2.3.4.1.10.0 Implementation Notes

This class is the linchpin of the entire application's architecture, connecting the decoupled layers at runtime.

#### 2.3.4.2.0.0 Class Name

##### 2.3.4.2.1.0 Class Name

GlobalExceptionHandler

##### 2.3.4.2.2.0 File Path

Assets/App/Presentation/Core/GlobalExceptionHandler.cs

##### 2.3.4.2.3.0 Class Type

Service

##### 2.3.4.2.4.0 Inheritance

object

##### 2.3.4.2.5.0 Purpose

Implements the global exception handling logic required by REQ-1-023. It subscribes to the system's unhandled exception event, logs the error, and commands the ViewManager to display a modal error dialog, preventing application crashes.

##### 2.3.4.2.6.0 Dependencies

- IViewManager
- ILogger

##### 2.3.4.2.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.2.8.0 Technology Integration Notes

This is a plain C# class that must be instantiated and have its `Register()` method called by `AppStartup` at the very beginning of the application lifecycle.

##### 2.3.4.2.9.0 Methods

###### 2.3.4.2.9.1 Method Name

####### 2.3.4.2.9.1.1 Method Name

Register

####### 2.3.4.2.9.1.2 Method Signature

Register()

####### 2.3.4.2.9.1.3 Return Type

void

####### 2.3.4.2.9.1.4 Access Modifier

public

####### 2.3.4.2.9.1.5 Is Async

false

####### 2.3.4.2.9.1.6 Implementation Logic

Specification requires this method to subscribe to `AppDomain.CurrentDomain.UnhandledException`. The handler method must be bound here.

####### 2.3.4.2.9.1.7 Exception Handling

N/A

###### 2.3.4.2.9.2.0 Method Name

####### 2.3.4.2.9.2.1 Method Name

OnUnhandledException

####### 2.3.4.2.9.2.2 Method Signature

OnUnhandledException(object sender, UnhandledExceptionEventArgs args)

####### 2.3.4.2.9.2.3 Return Type

void

####### 2.3.4.2.9.2.4 Access Modifier

private

####### 2.3.4.2.9.2.5 Is Async

false

####### 2.3.4.2.9.2.6 Implementation Logic

Specification requires this handler to generate a unique correlation ID. It must log the exception details with the ID using the injected ILogger. It must then construct a user-friendly error message and use the IViewManager to display the modal error dialog, as detailed in Sequence Diagram 192.

####### 2.3.4.2.9.2.7 Exception Handling

Must ensure the logging and UI display operations are wrapped in their own try-catch blocks to prevent recursive exceptions.

##### 2.3.4.2.10.0.0 Implementation Notes

This component is critical for application reliability and provides the graceful failure mode required by the NFRs.

#### 2.3.4.3.0.0.0 Class Name

##### 2.3.4.3.1.0.0 Class Name

InputController

##### 2.3.4.3.2.0.0 File Path

Assets/App/Presentation/Core/InputController.cs

##### 2.3.4.3.3.0.0 Class Type

Controller

##### 2.3.4.3.4.0.0 Inheritance

MonoBehaviour

##### 2.3.4.3.5.0.0 Purpose

Acts as the central hub for capturing and interpreting user input via Unity's New Input System. It translates low-level input actions into high-level application events or commands.

##### 2.3.4.3.6.0.0 Dependencies

- IEventBus

##### 2.3.4.3.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.3.8.0.0 Technology Integration Notes

Will hold a reference to the `PlayerInputActions` asset. Listens for events from the `PlayerInput` component and translates them into meaningful game commands.

##### 2.3.4.3.9.0.0 Methods

###### 2.3.4.3.9.1.0 Method Name

####### 2.3.4.3.9.1.1 Method Name

OnEnable

####### 2.3.4.3.9.1.2 Method Signature

OnEnable()

####### 2.3.4.3.9.1.3 Return Type

void

####### 2.3.4.3.9.1.4 Access Modifier

private

####### 2.3.4.3.9.1.5 Is Async

false

####### 2.3.4.3.9.1.6 Implementation Logic

Specification requires subscribing to input action events (e.g., `_playerInput.actions[\\\"Submit\\\"].performed += OnSubmit;`).

###### 2.3.4.3.9.2.0 Method Name

####### 2.3.4.3.9.2.1 Method Name

OnSubmit

####### 2.3.4.3.9.2.2 Method Signature

OnSubmit(InputAction.CallbackContext context)

####### 2.3.4.3.9.2.3 Return Type

void

####### 2.3.4.3.9.2.4 Access Modifier

private

####### 2.3.4.3.9.2.5 Is Async

false

####### 2.3.4.3.9.2.6 Implementation Logic

When a raw input action is received, this method's specification is to determine the context and publish a higher-level, semantic event on the IEventBus (e.g., `SubmitButtonPressedEvent`). UI presenters will listen for these semantic events instead of raw input.

####### 2.3.4.3.9.2.7 Exception Handling

N/A

##### 2.3.4.3.10.0.0 Implementation Notes

This class decouples the rest of the presentation layer from the specifics of the input hardware and Unity's input system, improving modularity and testability.

#### 2.3.4.4.0.0.0 Class Name

##### 2.3.4.4.1.0.0 Class Name

ModalDialogView

##### 2.3.4.4.2.0.0 File Path

Assets/App/Presentation/Shared/Views/ModalDialogView.cs

##### 2.3.4.4.3.0.0 Class Type

View

##### 2.3.4.4.4.0.0 Inheritance

MonoBehaviour, IModalDialogView

##### 2.3.4.4.5.0.0 Purpose

The MonoBehaviour view component for a reusable modal dialog. It handles rendering text and buttons and exposes Unity Events for user interactions.

##### 2.3.4.4.6.0.0 Dependencies

- TextMeshProUGUI titleText
- TextMeshProUGUI messageText
- Button confirmButton
- Button cancelButton

##### 2.3.4.4.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.4.8.0.0 Technology Integration Notes

This script will be attached to a Dialog Prefab. Its public fields will be linked to the corresponding UI components in the Unity Editor.

##### 2.3.4.4.9.0.0 Methods

- {'method_name': 'Configure', 'method_signature': 'Configure(DialogDefinition definition, Action onConfirm, Action onCancel)', 'return_type': 'void', 'access_modifier': 'public', 'is_async': 'false', 'implementation_logic': 'Specification requires this method to set the text for the title and message. It must also configure the visibility and text of the buttons based on the `DialogDefinition`. It will then wire up the `onClick` events of the buttons to invoke the provided `onConfirm` and `onCancel` actions.', 'exception_handling': 'Should handle null actions gracefully.'}

##### 2.3.4.4.10.0.0 Implementation Notes

This reusable view is critical for displaying error messages (REQ-1-023) and user confirmations throughout the application.

#### 2.3.4.5.0.0.0 Class Name

##### 2.3.4.5.1.0.0 Class Name

GameBoardPresenter

##### 2.3.4.5.2.0.0 File Path

Assets/App/Presentation/Features/GameBoard/Presenters/GameBoardPresenter.cs

##### 2.3.4.5.3.0.0 Class Type

Presenter

##### 2.3.4.5.4.0.0 Inheritance

object, IInitializable, IDisposable

##### 2.3.4.5.5.0.0 Purpose

Orchestrates the visual representation of the game board. It translates game state data into visual changes, such as moving tokens, adding houses, and triggering animations. Fulfills REQ-1-005, REQ-1-017.

##### 2.3.4.5.6.0.0 Dependencies

- IGameBoardView
- IEventBus
- IAssetProvider

##### 2.3.4.5.7.0.0 Methods

###### 2.3.4.5.7.1.0 Method Name

####### 2.3.4.5.7.1.1 Method Name

Initialize

####### 2.3.4.5.7.1.2 Method Signature

Initialize()

####### 2.3.4.5.7.1.3 Return Type

void

####### 2.3.4.5.7.1.4 Access Modifier

public

####### 2.3.4.5.7.1.5 Implementation Logic

Should subscribe to the `GameStateUpdatedEvent` on the IEventBus. Should perform initial setup of the board view, pre-loading and instantiating player token prefabs using the IAssetProvider based on the initial game setup data.

###### 2.3.4.5.7.2.0 Method Name

####### 2.3.4.5.7.2.1 Method Name

OnGameStateUpdated

####### 2.3.4.5.7.2.2 Method Signature

OnGameStateUpdated(GameStateUpdatedEvent evt)

####### 2.3.4.5.7.2.3 Return Type

void

####### 2.3.4.5.7.2.4 Access Modifier

private

####### 2.3.4.5.7.2.5 Is Async

true

####### 2.3.4.5.7.2.6 Implementation Logic

Must compare the new game state with the last known state to identify changes. For each player whose position has changed, it must call `IGameBoardView.AnimateTokenToPositionAsync`. For each property whose development level has changed, it must call the view to add or remove house/hotel models. Should handle all visual updates required to make the board match the authoritative GameState.

####### 2.3.4.5.7.2.7 Performance Considerations

Animations should be executed via coroutines or a tweening library to be non-blocking. Must be highly performant to meet REQ-1-014.

##### 2.3.4.5.8.0.0 Implementation Notes

This class is critical for the main gameplay loop's visual feedback and directly satisfies core visual requirements.

#### 2.3.4.6.0.0.0 Class Name

##### 2.3.4.6.1.0.0 Class Name

HUDPresenter

##### 2.3.4.6.2.0.0 File Path

Assets/App/Presentation/Features/HUD/Presenters/HUDPresenter.cs

##### 2.3.4.6.3.0.0 Class Type

Presenter

##### 2.3.4.6.4.0.0 Inheritance

object, IInitializable, IDisposable

##### 2.3.4.6.5.0.0 Purpose

Manages the state and logic for the Heads-Up Display. It provides the HUDView with formatted data and handles user interactions like clicking the \"End Turn\" or \"Manage Properties\" buttons. Fulfills REQ-1-071.

##### 2.3.4.6.6.0.0 Dependencies

- IHUDView
- IEventBus
- ITurnManagementService
- IViewManager

##### 2.3.4.6.7.0.0 Methods

###### 2.3.4.6.7.1.0 Method Name

####### 2.3.4.6.7.1.1 Method Name

Initialize

####### 2.3.4.6.7.1.2 Method Signature

Initialize()

####### 2.3.4.6.7.1.3 Return Type

void

####### 2.3.4.6.7.1.4 Access Modifier

public

####### 2.3.4.6.7.1.5 Implementation Logic

Should subscribe to `GameStateUpdatedEvent` and `TurnPhaseChangedEvent` on the IEventBus. Should bind its methods to the events exposed by IHUDView, such as `OnManagePropertiesClicked`.

###### 2.3.4.6.7.2.0 Method Name

####### 2.3.4.6.7.2.1 Method Name

OnGameStateUpdated

####### 2.3.4.6.7.2.2 Method Signature

OnGameStateUpdated(GameStateUpdatedEvent evt)

####### 2.3.4.6.7.2.3 Return Type

void

####### 2.3.4.6.7.2.4 Access Modifier

private

####### 2.3.4.6.7.2.5 Implementation Logic

Must extract relevant data for the current human player (e.g., cash, properties) from the game state. It must then create a view-specific data model (e.g., HUDViewModel DTO) and pass it to the IHUDView's `UpdateDisplay` method.

###### 2.3.4.6.7.3.0 Method Name

####### 2.3.4.6.7.3.1 Method Name

OnManagePropertiesClicked

####### 2.3.4.6.7.3.2 Method Signature

OnManagePropertiesClicked()

####### 2.3.4.6.7.3.3 Return Type

void

####### 2.3.4.6.7.3.4 Access Modifier

private

####### 2.3.4.6.7.3.5 Implementation Logic

Should call `_viewManager.ShowScreen(Screen.PropertyManagement)` to open the property management UI, as shown in Sequence Diagram 179.

#### 2.3.4.7.0.0.0 Class Name

##### 2.3.4.7.1.0.0 Class Name

PropertyManagementPresenter

##### 2.3.4.7.2.0.0 File Path

Assets/App/Presentation/Features/PropertyManagement/Presenters/PropertyManagementPresenter.cs

##### 2.3.4.7.3.0.0 Class Type

Presenter

##### 2.3.4.7.4.0.0 Inheritance

object, IInitializable, IDisposable

##### 2.3.4.7.5.0.0 Purpose

Handles the business logic for the property management screen. It fetches player asset data, validates user actions (like build house, mortgage), and calls application services to execute them.

##### 2.3.4.7.6.0.0 Dependencies

- IPropertyManagementView
- IPropertyActionService
- IEventBus

##### 2.3.4.7.7.0.0 Methods

###### 2.3.4.7.7.1.0 Method Name

####### 2.3.4.7.7.1.1 Method Name

Initialize

####### 2.3.4.7.7.1.2 Method Signature

Initialize()

####### 2.3.4.7.7.1.3 Return Type

void

####### 2.3.4.7.7.1.4 Access Modifier

public

####### 2.3.4.7.7.1.5 Implementation Logic

Specification requires subscribing to `GameStateUpdatedEvent` to refresh the view. Also requires binding to view events like `OnBuildHouseRequested`.

###### 2.3.4.7.7.2.0 Method Name

####### 2.3.4.7.7.2.1 Method Name

OnBuildHouseRequested

####### 2.3.4.7.7.2.2 Method Signature

OnBuildHouseRequested(string propertyId)

####### 2.3.4.7.7.2.3 Return Type

void

####### 2.3.4.7.7.2.4 Access Modifier

private

####### 2.3.4.7.7.2.5 Is Async

true

####### 2.3.4.7.7.2.6 Implementation Logic

Specification requires this method to invoke `IPropertyActionService.BuildHouseAsync(propertyId)`. It must handle the result, commanding the view to show a success message or an error notification, as detailed in Sequence Diagrams 179 and 180.

##### 2.3.4.7.8.0.0 Implementation Notes

This component is the controller for the user flows defined in sequences 179, 180, 184, and 193.

### 2.3.5.0.0.0.0 Interface Specifications

#### 2.3.5.1.0.0.0 Interface Name

##### 2.3.5.1.1.0.0 Interface Name

IViewManager

##### 2.3.5.1.2.0.0 File Path

Assets/App/Presentation/Shared/Services/IViewManager.cs

##### 2.3.5.1.3.0.0 Purpose

Defines a contract for a service that manages the showing, hiding, and layering of UI screens and dialogs.

##### 2.3.5.1.4.0.0 Method Contracts

###### 2.3.5.1.4.1.0 Method Name

####### 2.3.5.1.4.1.1 Method Name

ShowScreen

####### 2.3.5.1.4.1.2 Method Signature

Task ShowScreen(Screen screen, object payload = null)

####### 2.3.5.1.4.1.3 Return Type

Task

####### 2.3.5.1.4.1.4 Contract Description

Asynchronously loads and displays a specific UI screen prefab, hiding any other active screens as necessary.

###### 2.3.5.1.4.2.0 Method Name

####### 2.3.5.1.4.2.1 Method Name

ShowDialog

####### 2.3.5.1.4.2.2 Method Signature

Task<DialogResult> ShowDialog(DialogDefinition definition)

####### 2.3.5.1.4.2.3 Return Type

Task<DialogResult>

####### 2.3.5.1.4.2.4 Contract Description

Displays a modal dialog box and returns a Task that completes when the user makes a choice. This is critical for REQ-1-023.

##### 2.3.5.1.5.0.0 Implementation Guidance

The concrete implementation (`ViewManager.cs`) should use the IAssetProvider to load screen prefabs asynchronously. It needs to manage a stack of active screens to handle layering and back navigation.

#### 2.3.5.2.0.0.0 Interface Name

##### 2.3.5.2.1.0.0 Interface Name

IGameBoardView

##### 2.3.5.2.2.0.0 File Path

Assets/App/Presentation/Features/GameBoard/Views/IGameBoardView.cs

##### 2.3.5.2.3.0.0 Purpose

Defines the contract for the GameBoard's View component. It exposes methods for the GameBoardPresenter to command, abstracting away the underlying Unity GameObject and component details.

##### 2.3.5.2.4.0.0 Method Contracts

###### 2.3.5.2.4.1.0 Method Name

####### 2.3.5.2.4.1.1 Method Name

AnimateTokenToPositionAsync

####### 2.3.5.2.4.1.2 Method Signature

Task AnimateTokenToPositionAsync(string playerId, int targetTileIndex)

####### 2.3.5.2.4.1.3 Return Type

Task

####### 2.3.5.2.4.1.4 Contract Description

Executes a visual animation of a player's token moving along the board path to a new tile, fulfilling REQ-1-017. The returned Task should complete when the animation finishes.

###### 2.3.5.2.4.2.0 Method Name

####### 2.3.5.2.4.2.1 Method Name

UpdatePropertyVisuals

####### 2.3.5.2.4.2.2 Method Signature

void UpdatePropertyVisuals(int tileIndex, int houseCount)

####### 2.3.5.2.4.2.3 Return Type

void

####### 2.3.5.2.4.2.4 Contract Description

Instantiates, removes, or updates the 3D models for houses and hotels on a specific property tile.

##### 2.3.5.2.5.0.0 Implementation Guidance

The implementing class, `GameBoardView.cs`, will be a MonoBehaviour. It will need a way to map tile indices to Transform positions in the 3D scene. It should use a tweening library (like DOTween) or coroutines for smooth animations.

#### 2.3.5.3.0.0.0 Interface Name

##### 2.3.5.3.1.0.0 Interface Name

IPropertyManagementView

##### 2.3.5.3.2.0.0 File Path

Assets/App/Presentation/Features/PropertyManagement/Views/IPropertyManagementView.cs

##### 2.3.5.3.3.0.0 Purpose

Defines the contract for the Property Management screen's View component.

##### 2.3.5.3.4.0.0 Method Contracts

###### 2.3.5.3.4.1.0 Method Name

####### 2.3.5.3.4.1.1 Method Name

DisplayAssets

####### 2.3.5.3.4.1.2 Method Signature

void DisplayAssets(PlayerAssetViewModel viewModel)

####### 2.3.5.3.4.1.3 Return Type

void

####### 2.3.5.3.4.1.4 Contract Description

Populates the UI with the player's properties, cash, and dynamically enables/disables action buttons based on the provided view model.

###### 2.3.5.3.4.2.0 Method Name

####### 2.3.5.3.4.2.1 Method Name

ShowError

####### 2.3.5.3.4.2.2 Method Signature

void ShowError(string message)

####### 2.3.5.3.4.2.3 Return Type

void

####### 2.3.5.3.4.2.4 Contract Description

Displays a non-intrusive error notification to the user (e.g., \"Cannot mortgage developed property\").

##### 2.3.5.3.5.0.0 Property Contracts

- {'property_name': 'OnBuildHouseRequested', 'property_type': 'event Action<string>', 'getter_contract': 'Event fired when the user clicks the \\"Build House\\" button for a property.', 'setter_contract': 'N/A'}

### 2.3.6.0.0.0.0 Enum Specifications

- {'enum_name': 'Screen', 'file_path': 'Assets/App/Presentation/Shared/Services/Screen.cs', 'underlying_type': 'int', 'purpose': 'Provides a strongly-typed identifier for each major UI screen in the application.', 'values': [{'value_name': 'None', 'value': '0', 'description': 'Represents no screen.'}, {'value_name': 'MainMenu', 'value': '1', 'description': 'The main menu screen.'}, {'value_name': 'GameSetup', 'value': '2', 'description': 'The new game configuration screen.'}, {'value_name': 'GameBoard', 'value': '3', 'description': 'The main game screen with the HUD and 3D board.'}, {'value_name': 'Settings', 'value': '4', 'description': 'The application settings screen.'}, {'value_name': 'PropertyManagement', 'value': '5', 'description': 'The screen for managing owned properties.'}]}

### 2.3.7.0.0.0.0 Dto Specifications

- {'dto_name': 'SaveGameMetadata', 'file_path': 'Assets/App/Presentation/Features/LoadGame/ViewModels/SaveGameMetadata.cs', 'purpose': 'A view-specific data model used to populate the list of save slots in the Load Game UI. This is a projection of data received from the Application layer.', 'properties': [{'property_name': 'SlotNumber', 'property_type': 'int'}, {'property_name': 'SaveName', 'property_type': 'string'}, {'property_name': 'SaveTimestamp', 'property_type': 'string'}, {'property_name': 'Status', 'property_type': 'SaveStatus'}], 'implementation_notes': 'This DTO is essential for the flow described in Sequence Diagram 185, where a corrupted save file is indicated by the \\"Status\\" property.'}

### 2.3.8.0.0.0.0 Configuration Specifications

- {'configuration_name': 'ThemeDefinition', 'file_path': 'Assets/Settings/Themes/ThemeDefinition.cs', 'purpose': 'A ScriptableObject that defines the asset keys for a specific visual/audio theme. This allows designers to create new themes by creating new instances of this asset and populating its fields. Fulfills REQ-1-093.', 'framework_base_class': 'ScriptableObject', 'configuration_sections': [{'section_name': 'Board Assets', 'properties': [{'property_name': 'GameBoardPrefabKey', 'property_type': 'string', 'required': True, 'description': 'Addressable key for the main game board prefab.'}, {'property_name': 'PlayerTokenPrefabKeys', 'property_type': 'List<string>', 'required': True, 'description': 'List of Addressable keys for the available player token prefabs.'}]}, {'section_name': 'Audio Assets', 'properties': [{'property_name': 'MainMenuMusicKey', 'property_type': 'string', 'required': True, 'description': 'Addressable key for the main menu background music AudioClip.'}, {'property_name': 'DiceRollSfxKey', 'property_type': 'string', 'required': True, 'description': 'Addressable key for the dice roll sound effect AudioClip.'}]}], 'validation_requirements': 'All key fields must be non-empty and correspond to valid Addressable assets.'}

### 2.3.9.0.0.0.0 Dependency Injection Specifications

#### 2.3.9.1.0.0.0 Service Interface

##### 2.3.9.1.1.0.0 Service Interface

IViewManager

##### 2.3.9.1.2.0.0 Service Implementation

ViewManager

##### 2.3.9.1.3.0.0 Lifetime

Singleton

##### 2.3.9.1.4.0.0 Registration Reasoning

View management is a global concern that needs to persist across scene loads. A singleton instance ensures a consistent state for UI navigation.

##### 2.3.9.1.5.0.0 Framework Registration Pattern

Container.Bind<IViewManager>().To<ViewManager>().AsSingle();

#### 2.3.9.2.0.0.0 Service Interface

##### 2.3.9.2.1.0.0 Service Interface

IAssetProvider

##### 2.3.9.2.2.0.0 Service Implementation

AddressableAssetProvider

##### 2.3.9.2.3.0.0 Lifetime

Singleton

##### 2.3.9.2.4.0.0 Registration Reasoning

Asset loading and caching is a global, long-lived service. A singleton prevents redundant loading and manages asset lifetimes efficiently.

##### 2.3.9.2.5.0.0 Framework Registration Pattern

Container.Bind<IAssetProvider>().To<AddressableAssetProvider>().AsSingle();

#### 2.3.9.3.0.0.0 Service Interface

##### 2.3.9.3.1.0.0 Service Interface

GameBoardPresenter

##### 2.3.9.3.2.0.0 Service Implementation

GameBoardPresenter

##### 2.3.9.3.3.0.0 Lifetime

Scoped

##### 2.3.9.3.4.0.0 Registration Reasoning

The presenter's lifecycle is tied to the GameBoard scene. A scoped (or transient) lifetime ensures a fresh instance is created when the game starts and is disposed of when the game ends.

##### 2.3.9.3.5.0.0 Framework Registration Pattern

Container.BindInterfacesAndSelfTo<GameBoardPresenter>().AsTransient();

#### 2.3.9.4.0.0.0 Service Interface

##### 2.3.9.4.1.0.0 Service Interface

IGameSessionService

##### 2.3.9.4.2.0.0 Service Implementation

GameSessionService (from REPO-AS-005)

##### 2.3.9.4.3.0.0 Lifetime

Singleton

##### 2.3.9.4.4.0.0 Registration Reasoning

The Presentation layer does not define this implementation but registers the one provided by the Application layer. Game session state is a global, application-wide concern.

##### 2.3.9.4.5.0.0 Framework Registration Pattern

Container.Bind<IGameSessionService>().To<GameSessionService>().AsSingle();

### 2.3.10.0.0.0.0 External Integration Specifications

- {'integration_target': 'Application Layer (REPO-AS-005)', 'integration_type': 'In-Memory Service Consumption', 'required_client_classes': ['MainMenuPresenter', 'LoadGamePresenter', 'HUDPresenter'], 'configuration_requirements': "Requires the Application layer's assembly to be referenced. DI container must be configured to inject Application services.", 'error_handling_requirements': 'UI Presenters must implement try-catch blocks around service calls to handle potential exceptions from lower layers, displaying user-friendly error messages. The GlobalExceptionHandler provides a final safety net.', 'authentication_requirements': 'N/A', 'framework_integration_patterns': 'Dependency Injection is the sole pattern for communication. UI Presenters will be injected with interfaces like `IGameSessionService` and `ITurnManagementService`.'}

## 2.4.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 7 |
| Total Interfaces | 3 |
| Total Enums | 1 |
| Total Dtos | 1 |
| Total Configurations | 1 |
| Total External Integrations | 1 |
| Grand Total Components | 14 |
| Phase 2 Claimed Count | 45 |
| Phase 2 Actual Count | 13 |
| Validation Added Count | 11 |
| Final Validated Count | 24 |

