# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-PU-010 |
| Extraction Timestamp | 2024-05-23T10:00:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | High |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

REQ-1-014

#### 1.2.1.2 Requirement Text

Sustain an average of 60 FPS and not drop below 45 FPS at 1080p on recommended specs.

#### 1.2.1.3 Validation Criteria

- Application maintains a smooth frame rate during typical gameplay on target hardware.
- Performance profiling shows frame rate consistency.

#### 1.2.1.4 Implementation Implications

- Rendering pipelines (URP/HDRP) must be optimized.
- Draw calls, texture sizes, and shader complexity must be carefully managed.
- Scripts, especially in Update() loops, must be performant.

#### 1.2.1.5 Extraction Reasoning

This is a core non-functional requirement explicitly assigned to the Presentation Layer, as it is solely responsible for rendering and performance. The repository's technology guidance directly references this requirement.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

REQ-1-071

#### 1.2.2.2 Requirement Text

The game MUST provide a complete User Interface (UI) for all player interactions.

#### 1.2.2.3 Validation Criteria

- All required game actions are accessible through UI elements.
- UI includes HUD, menus, and modal dialogs as needed.

#### 1.2.2.4 Implementation Implications

- Unity's UI system (e.g., UI Toolkit or UGUI) will be used to build all screens.
- Components like HUDController, ViewManager, and others are needed to manage UI state and visibility.

#### 1.2.2.5 Extraction Reasoning

This requirement is a primary responsibility of the Presentation repository, as stated in both the repository's description and the architecture's Presentation Layer definition.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

REQ-1-017

#### 1.2.3.2 Requirement Text

All player actions, such as token movement and dice rolls, MUST be clearly animated.

#### 1.2.3.3 Validation Criteria

- Dice rolls are visually represented.
- Player tokens animate their movement from one space to another.
- Transactions are accompanied by visual feedback.

#### 1.2.3.4 Implementation Implications

- Unity's animation system (Animator, timelines) will be used.
- The GameBoardPresenter component will be responsible for triggering and managing these animations.

#### 1.2.3.5 Extraction Reasoning

The architecture document explicitly assigns responsibility for executing animations (REQ-1-017) to the Presentation Layer.

### 1.2.4.0 Requirement Id

#### 1.2.4.1 Requirement Id

REQ-1-023

#### 1.2.4.2 Requirement Text

The application MUST NOT crash on unhandled exceptions. Instead, it MUST display a modal error dialog to the user.

#### 1.2.4.3 Validation Criteria

- An unhandled exception from any layer results in a user-facing dialog, not a crash.
- The dialog contains a unique error ID for support purposes.

#### 1.2.4.4 Implementation Implications

- A global exception handler must be implemented at the application's entry point.
- A reusable modal dialog prefab must be created in Unity.
- The ViewManager will be responsible for displaying this dialog.

#### 1.2.4.5 Extraction Reasoning

This repository is responsible for the application's composition root and UI, making it the only place to implement a global exception handler that can display a UI dialog as required.

### 1.2.5.0 Requirement Id

#### 1.2.5.1 Requirement Id

REQ-1-093

#### 1.2.5.2 Requirement Text

The game's visual and audio assets MUST be replaceable via a theme system.

#### 1.2.5.3 Validation Criteria

- Assets (models, textures, sounds) can be swapped by changing a configuration.
- The system can load different asset packs at runtime.

#### 1.2.5.4 Implementation Implications

- Unity's Addressables system must be used for asset management to allow for dynamic loading.
- Asset references in code and scenes must be indirect (via Addressables) rather than direct.

#### 1.2.5.5 Extraction Reasoning

This is an extensibility requirement that directly impacts how assets are managed and loaded within the Unity environment, a core responsibility of this repository.

### 1.2.6.0 Requirement Id

#### 1.2.6.1 Requirement Id

REQ-1-080

#### 1.2.6.2 Requirement Text

The settings menu shall contain data management options to 'Reset Statistics' and 'Delete Saved Games'.

#### 1.2.6.3 Validation Criteria

- A button in the settings UI triggers the reset statistics workflow.
- A button in the settings UI triggers the delete all saves workflow.

#### 1.2.6.4 Implementation Implications

- The UI must call a dedicated application service to perform these data management tasks.
- Confirmation dialogs must be presented before executing destructive actions.

#### 1.2.6.5 Extraction Reasoning

This repository implements the user-facing settings menu and must integrate with the application layer to provide the functionality for these data management options.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

CompositionRoot

#### 1.3.1.2 Component Specification

A non-visual component executed at application startup. It is responsible for initializing the Dependency Injection container, registering all services from all layers (Application, Domain, Infrastructure), and starting the main application flow by loading the main menu.

#### 1.3.1.3 Implementation Requirements

- Must execute before any other application logic.
- Must correctly wire concrete implementations (e.g., `SqliteStatisticsRepository`) to their abstractions (e.g., `IStatisticsRepository`).

#### 1.3.1.4 Architectural Context

Belongs to the Presentation Layer, but acts as the central configuration hub for the entire monolithic application.

#### 1.3.1.5 Extraction Reasoning

The repository description explicitly states it serves as the application's composition root, which is a critical architectural responsibility for wiring the layered architecture together.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

ViewManager

#### 1.3.2.2 Component Specification

Handles the lifecycle of scenes and UI panels/screens. Responsible for activating, deactivating, and transitioning between different views like the Main Menu, Game Board, and Settings screen.

#### 1.3.2.3 Implementation Requirements

- Manage a collection of UI prefabs.
- Provide methods to show/hide specific UI panels, often with animations.
- Handle scene loading and unloading.

#### 1.3.2.4 Architectural Context

Belongs to the Presentation Layer. Acts as a high-level controller for the application's overall UI state.

#### 1.3.2.5 Extraction Reasoning

This component is explicitly listed in the architecture's Presentation Layer and is essential for managing the UI as required by REQ-1-071.

### 1.3.3.0 Component Name

#### 1.3.3.1 Component Name

GameBoardPresenter

#### 1.3.3.2 Component Specification

Updates the visual state of the 3D game board, including player tokens, properties (houses/hotels), and other visual effects. It subscribes to game state updates and translates them into visual changes.

#### 1.3.3.3 Implementation Requirements

- Hold references to 3D models for tokens, houses, and hotels.
- Implement logic to animate token movement between board spaces.
- Listen for GameStateUpdated events to refresh the visual representation of the board.

#### 1.3.3.4 Architectural Context

Belongs to the Presentation Layer. Acts as the Presenter in an MVP pattern for the main game board view.

#### 1.3.3.5 Extraction Reasoning

This component is listed in the architecture and is the primary component responsible for fulfilling rendering and animation requirements (REQ-1-005, REQ-1-017).

### 1.3.4.0 Component Name

#### 1.3.4.1 Component Name

GlobalExceptionHandler

#### 1.3.4.2 Component Specification

A non-visual component that registers itself to handle all unhandled exceptions application-wide. It logs the exception details and uses the ViewManager to display a final error dialog to the user.

#### 1.3.4.3 Implementation Requirements

- Must be instantiated once at startup by the CompositionRoot.
- Must depend on ILogger and IViewManager.

#### 1.3.4.4 Architectural Context

Belongs to the Presentation Layer. This is the only layer that can both handle a global event and display a UI element in response.

#### 1.3.4.5 Extraction Reasoning

This component is the direct implementation of the critical reliability requirement REQ-1-023 and is a key integration point between the application's core logic and UI feedback during failure.

## 1.4.0.0 Architectural Layers

- {'layer_name': 'Presentation Layer', 'layer_responsibilities': 'Responsible for all user-facing elements and interactions, including rendering the 3D world and 2D UI, handling user input, managing animations, and playing audio/visual effects. Also acts as the Composition Root for the application.', 'layer_constraints': ['Must not contain any core game rule logic (e.g., rent calculation, trade validation).', 'Must not perform direct data access (e.g., reading save files); must delegate all such operations to the Application Services layer.', 'Must be optimized to meet strict performance targets (REQ-1-014).'], 'implementation_patterns': ['Model-View-Presenter (MVP)', 'Dependency Injection (as the Composition Root)', 'Event-Driven (subscribing to game state events)'], 'extraction_reasoning': 'This repository IS the implementation of the Presentation Layer. Its entire scope, responsibilities, and constraints are defined by this architectural layer.'}

## 1.5.0.0 Dependency Interfaces

### 1.5.1.0 Interface Name

#### 1.5.1.1 Interface Name

Application Service Interfaces

#### 1.5.1.2 Source Repository

REPO-AS-005

#### 1.5.1.3 Method Contracts

##### 1.5.1.3.1 Method Name

###### 1.5.1.3.1.1 Method Name

IGameSessionService.StartNewGameAsync

###### 1.5.1.3.1.2 Method Signature

Task StartNewGameAsync(GameSetupOptions options)

###### 1.5.1.3.1.3 Method Purpose

Initiates a new game session based on the player's configuration choices from the UI.

###### 1.5.1.3.1.4 Integration Context

Called from the 'Start Game' button action in the game setup UI.

##### 1.5.1.3.2.0 Method Name

###### 1.5.1.3.2.1 Method Name

ITurnManagementService.ExecutePlayerActionAsync

###### 1.5.1.3.2.2 Method Signature

Task ExecutePlayerActionAsync(PlayerAction action)

###### 1.5.1.3.2.3 Method Purpose

Processes a specific game action submitted by the player (e.g., build house, mortgage property).

###### 1.5.1.3.2.4 Integration Context

Called from various UI elements like the Property Management screen when the player confirms an action.

##### 1.5.1.3.3.0 Method Name

###### 1.5.1.3.3.1 Method Name

ITradeOrchestrationService.ProposeTradeAsync

###### 1.5.1.3.3.2 Method Signature

Task<TradeResult> ProposeTradeAsync(TradeProposal proposal)

###### 1.5.1.3.3.3 Method Purpose

Submits a trade proposal constructed in the UI to the application layer for AI evaluation.

###### 1.5.1.3.3.4 Integration Context

Called from the trading UI when the player confirms their trade offer.

##### 1.5.1.3.4.0 Method Name

###### 1.5.1.3.4.1 Method Name

IUserDataManagementService.ResetStatisticsAsync

###### 1.5.1.3.4.2 Method Signature

Task ResetStatisticsAsync()

###### 1.5.1.3.4.3 Method Purpose

Triggers the deletion of all historical statistics for the current player.

###### 1.5.1.3.4.4 Integration Context

Called from the settings menu in response to the user action defined in REQ-1-080.

##### 1.5.1.3.5.0 Method Name

###### 1.5.1.3.5.1 Method Name

IStatisticsQueryService.GetTopScoresAsync

###### 1.5.1.3.5.2 Method Signature

Task<List<TopScoreDto>> GetTopScoresAsync()

###### 1.5.1.3.5.3 Method Purpose

Retrieves the high scores list for display in the UI.

###### 1.5.1.3.5.4 Integration Context

Called when the user navigates to the 'Top Scores' screen (REQ-1-091).

#### 1.5.1.4.0.0 Integration Pattern

Dependency Injection

#### 1.5.1.5.0.0 Communication Protocol

In-memory asynchronous method calls

#### 1.5.1.6.0.0 Extraction Reasoning

This is the primary dependency for the Presentation Layer. It consumes the facades provided by the Application Services Layer to execute all game logic and user-initiated commands, ensuring a clean separation of concerns.

### 1.5.2.0.0.0 Interface Name

#### 1.5.2.1.0.0 Interface Name

Cross-Cutting Service Abstractions

#### 1.5.2.2.0.0 Source Repository

REPO-AA-004

#### 1.5.2.3.0.0 Method Contracts

##### 1.5.2.3.1.0 Method Name

###### 1.5.2.3.1.1 Method Name

IApplicationEventBus.Subscribe

###### 1.5.2.3.1.2 Method Signature

void Subscribe<TEvent>(Action<TEvent> handler)

###### 1.5.2.3.1.3 Method Purpose

Allows UI components (Presenters) to subscribe to application-wide events, such as 'GameStateUpdated'.

###### 1.5.2.3.1.4 Integration Context

Used by components like the HUDPresenter and GameBoardPresenter to reactively update the view based on state changes originating from the core logic.

##### 1.5.2.3.2.0 Method Name

###### 1.5.2.3.2.1 Method Name

ILogger.Error

###### 1.5.2.3.2.2 Method Signature

void Error(Exception ex, string messageTemplate, params object[] propertyValues)

###### 1.5.2.3.2.3 Method Purpose

Allows the global exception handler to log detailed error information before displaying a UI dialog.

###### 1.5.2.3.2.4 Integration Context

Called by the GlobalExceptionHandler component to fulfill REQ-1-023.

##### 1.5.2.3.3.0 Method Name

###### 1.5.2.3.3.1 Method Name

IConfigurationProvider.LoadAsync

###### 1.5.2.3.3.2 Method Signature

Task<T?> LoadAsync<T>(string configPath) where T : class

###### 1.5.2.3.3.3 Method Purpose

Allows the presentation layer to load its own configuration or content files, such as the rulebook or localization strings.

###### 1.5.2.3.3.4 Integration Context

Called by UI components responsible for displaying content defined in external files (e.g., the Rulebook screen loading 'rulebook.json' as per REQ-1-083).

#### 1.5.2.4.0.0 Integration Pattern

Dependency Injection

#### 1.5.2.5.0.0 Communication Protocol

In-process method calls

#### 1.5.2.6.0.0 Extraction Reasoning

The Presentation Layer depends on these cross-cutting abstractions to integrate with services like logging, eventing, and configuration without being tightly coupled to their specific infrastructure implementations.

## 1.6.0.0.0.0 Exposed Interfaces

*No items available*

## 1.7.0.0.0.0 Technology Context

### 1.7.1.0.0.0 Framework Requirements

Must be built using the Unity Engine (Latest LTS) and C# with .NET 8 compatibility. All code must be organized within the MonopolyTycoon.Presentation namespace.

### 1.7.2.0.0.0 Integration Technologies

- Unity Test Framework (for playmode and integration tests)
- Unity Addressables System (for theme support per REQ-1-093)
- Unity Input System (for handling user input)
- A Unity-compatible Dependency Injection framework (e.g., Zenject, VContainer)

### 1.7.3.0.0.0 Performance Constraints

Must maintain an average of 60 FPS on recommended hardware (REQ-1-014). This requires careful management of rendering, physics, and script execution. All service calls to lower layers involving I/O must be asynchronous.

### 1.7.4.0.0.0 Security Requirements

Not applicable for this repository, as it is a client-side application with no direct handling of sensitive user data or server communication.

## 1.8.0.0.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | All repository connections have been fully specifi... |
| Cross Reference Validation | All extracted elements show strong consistency. Re... |
| Implementation Readiness Assessment | The context is highly implementation-ready. It pro... |
| Quality Assurance Confirmation | Systematic analysis confirms that the repository's... |

