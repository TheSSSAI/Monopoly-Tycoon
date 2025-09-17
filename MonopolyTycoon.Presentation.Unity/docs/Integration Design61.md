# 1 Integration Specifications

## 1.1 Extraction Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-PU-010 |
| Extraction Timestamp | 2024-05-21T11:30:00Z |
| Mapping Validation Score | 100% |
| Context Completeness Score | 100% |
| Implementation Readiness Level | High |

## 1.2 Relevant Requirements

### 1.2.1 Requirement Id

#### 1.2.1.1 Requirement Id

REQ-1-014

#### 1.2.1.2 Requirement Text

The application SHALL maintain an average of 60 frames per second (FPS) and not drop below 45 FPS at 1080p resolution on the recommended hardware specifications.

#### 1.2.1.3 Validation Criteria

- Application performance measured using a standard profiling tool (e.g., Unity Profiler) under typical gameplay scenarios.
- FPS must remain within the specified bounds during a 10-minute automated playtest.

#### 1.2.1.4 Implementation Implications

- All calls from the Presentation Layer to the Application Services Layer must be asynchronous to prevent blocking the main rendering thread.
- UI update logic must be event-driven rather than polling-based to minimize CPU usage in `Update()` loops.

#### 1.2.1.5 Extraction Reasoning

This core performance requirement dictates the asynchronous nature of all integration points with lower layers, making it a primary driver for the integration architecture.

### 1.2.2.0 Requirement Id

#### 1.2.2.1 Requirement Id

REQ-1-073

#### 1.2.2.2 Requirement Text

The system's UI shall use two distinct methods for presenting information: 1) Modal dialogs that halt gameplay must be used for critical human player decisions. 2) Non-intrusive, auto-dismissing notifications must be used for informational events that do not require player input.

#### 1.2.2.3 Validation Criteria

- Landing on an unowned property displays a modal dialog.
- An AI-to-AI trade displays a non-intrusive notification.

#### 1.2.2.4 Implementation Implications

- Direct, awaited method calls to application services are used for user-initiated actions that expect a direct response (modal flows).
- A subscription to a global event bus is required for the UI to react to game events that were not initiated by the user (non-intrusive notifications).

#### 1.2.2.5 Extraction Reasoning

This requirement explicitly defines the need for two primary integration patterns: direct asynchronous calls for commands and a publish-subscribe model for events, which must be reflected in the dependency interfaces.

### 1.2.3.0 Requirement Id

#### 1.2.3.1 Requirement Id

REQ-1-059

#### 1.2.3.2 Requirement Text

The system shall facilitate trading between the human player and AI opponents... When an AI proposes a trade to the human, a modal dialog must be displayed with options to 'Accept', 'Decline', or 'Propose Counter-Offer'.

#### 1.2.3.3 Validation Criteria

- The UI can be triggered to display a trade offer from an AI.
- The player's response is correctly communicated back to the game logic.

#### 1.2.3.4 Implementation Implications

- The Presentation Layer must depend on an `ITradeOrchestrationService` to handle trade logic.
- The UI must subscribe to an event (e.g., `AITradeProposedEvent`) to know when to display the offer dialog.

#### 1.2.3.5 Extraction Reasoning

This requirement mandates a specific, complex interaction flow that necessitates dedicated service interfaces (`ITradeOrchestrationService`) and event-driven integration patterns for the UI to function correctly.

### 1.2.4.0 Requirement Id

#### 1.2.4.1 Requirement Id

REQ-1-023

#### 1.2.4.2 Requirement Text

In the event of an unhandled exception, the system shall display a modal error dialog to the user... The displayed error identifier must directly correlate to a specific entry in the error log.

#### 1.2.4.3 Validation Criteria

- An unhandled exception results in a user-facing dialog.
- The dialog contains a unique ID that is also present in the log files.

#### 1.2.4.4 Implementation Implications

- A `GlobalExceptionHandler` component in this repository must have a dependency on a logging service (`ILogger`) to write the exception details before showing the UI.
- This component also needs a dependency on a `ViewManager` or similar UI service to display the error dialog.

#### 1.2.4.5 Extraction Reasoning

This reliability requirement necessitates an integration between the top-level Presentation Layer's exception handler and the Infrastructure Layer's logging service, demonstrating a key cross-layer integration point.

## 1.3.0.0 Relevant Components

### 1.3.1.0 Component Name

#### 1.3.1.1 Component Name

CompositionRoot

#### 1.3.1.2 Component Specification

The application's entry point. A MonoBehaviour script in a startup scene responsible for initializing the Dependency Injection (DI) container and wiring together all application layers. It resolves and registers concrete implementations from Infrastructure repositories against the abstract interfaces consumed by the Application and Presentation layers.

#### 1.3.1.3 Implementation Requirements

- Must run before any other application logic.
- Must configure and build the DI container, registering all services.

#### 1.3.1.4 Architectural Context

Presentation Layer. This component embodies the repository's role as the Composition Root.

#### 1.3.1.5 Extraction Reasoning

This is the most critical integration component, as it establishes all connections between the Presentation Layer and the rest of the application.

### 1.3.2.0 Component Name

#### 1.3.2.1 Component Name

MainMenuPresenter

#### 1.3.2.2 Component Specification

Handles the logic for the main menu, including starting a new game. It translates user input from the `IMainMenuView` into calls to the application services.

#### 1.3.2.3 Implementation Requirements

- Must call `IPlayerProfileRepository.GetOrCreateProfileAsync` and `IGameSessionService.StartNewGameAsync`.

#### 1.3.2.4 Architectural Context

Presentation Layer. Presenter in the MVP pattern.

#### 1.3.2.5 Extraction Reasoning

This component demonstrates the consumption of multiple application and repository interfaces to fulfill the 'New Game' use case (REQ-1-030, REQ-1-032).

### 1.3.3.0 Component Name

#### 1.3.3.1 Component Name

HUDPresenter

#### 1.3.3.2 Component Specification

Handles the logic for the main game's Heads-Up Display. It subscribes to game state events to reactively update UI elements with player information.

#### 1.3.3.3 Implementation Requirements

- Must subscribe to `GameStateUpdatedEvent` via an `IEventBus`.

#### 1.3.3.4 Architectural Context

Presentation Layer. Presenter in the MVP pattern.

#### 1.3.3.5 Extraction Reasoning

This component is the primary consumer of the event-driven integration pattern, required to keep the UI synchronized with the game state without direct coupling.

### 1.3.4.0 Component Name

#### 1.3.4.1 Component Name

GlobalExceptionHandler

#### 1.3.4.2 Component Specification

A persistent MonoBehaviour that registers itself to handle all unhandled exceptions from the application. It logs the exception details and displays a user-friendly error dialog.

#### 1.3.4.3 Implementation Requirements

- Must be instantiated by the CompositionRoot.
- Must have dependencies on `ILogger` and `IViewManager`.

#### 1.3.4.4 Architectural Context

Presentation Layer. A cross-cutting concern handler.

#### 1.3.4.5 Extraction Reasoning

This component implements the critical reliability requirement REQ-1-023 and is a key integration point for the logging service.

## 1.4.0.0 Architectural Layers

- {'layer_name': 'Presentation Layer', 'layer_responsibilities': "Responsible for all user-facing elements and interactions, including rendering the 3D game, displaying all UI, handling user input, managing animations, and playing audio. It also serves as the application's Composition Root, initializing the DI container.", 'layer_constraints': ['Must not contain any core game rule logic (e.g., rent calculation).', 'Must not perform direct data persistence; all such operations must be delegated to the Application Services Layer through abstract interfaces.'], 'implementation_patterns': ['Model-View-Presenter (MVP)', 'Dependency Injection (as Composition Root)', 'Observer (subscribing to events from lower layers)'], 'extraction_reasoning': "This repository is the sole and complete implementation of the Presentation Layer as defined in the solution's architecture."}

## 1.5.0.0 Dependency Interfaces

### 1.5.1.0 Interface Name

#### 1.5.1.1 Interface Name

IGameSessionService

#### 1.5.1.2 Source Repository

REPO-AA-004

#### 1.5.1.3 Method Contracts

##### 1.5.1.3.1 Method Name

###### 1.5.1.3.1.1 Method Name

StartNewGameAsync

###### 1.5.1.3.1.2 Method Signature

Task StartNewGameAsync(GameSetupOptions options)

###### 1.5.1.3.1.3 Method Purpose

Initiates the creation of a new game session based on user-selected options from the setup screen.

###### 1.5.1.3.1.4 Integration Context

Called from a UI presenter (e.g., MainMenuPresenter) when the user clicks the 'Start Game' button.

##### 1.5.1.3.2.0 Method Name

###### 1.5.1.3.2.1 Method Name

LoadGameAsync

###### 1.5.1.3.2.2 Method Signature

Task LoadGameAsync(int slot)

###### 1.5.1.3.2.3 Method Purpose

Loads a previously saved game state from a specified slot.

###### 1.5.1.3.2.4 Integration Context

Called from the 'Load Game' screen presenter when the user selects a save slot.

##### 1.5.1.3.3.0 Method Name

###### 1.5.1.3.3.1 Method Name

SaveGameAsync

###### 1.5.1.3.3.2 Method Signature

Task SaveGameAsync(int slot)

###### 1.5.1.3.3.3 Method Purpose

Saves the current game state to a specified slot.

###### 1.5.1.3.3.4 Integration Context

Called from the in-game pause menu presenter when the user clicks 'Save Game'.

#### 1.5.1.4.0.0 Integration Pattern

Dependency Injection

#### 1.5.1.5.0.0 Communication Protocol

In-memory asynchronous method calls.

#### 1.5.1.6.0.0 Extraction Reasoning

This is the primary dependency for managing the game's lifecycle. The Presentation Layer orchestrates these high-level actions based on user input, delegating all logic to the Application Layer via this interface.

### 1.5.2.0.0.0 Interface Name

#### 1.5.2.1.0.0 Interface Name

ITurnManagementService

#### 1.5.2.2.0.0 Source Repository

REPO-AA-004

#### 1.5.2.3.0.0 Method Contracts

- {'method_name': 'ExecutePlayerActionAsync', 'method_signature': 'Task ExecutePlayerActionAsync(PlayerAction action)', 'method_purpose': 'Processes a specific game action initiated by the player, such as buying property, building a house, or rolling the dice.', 'integration_context': 'Called by various UI presenters (e.g., PropertyManagementPresenter, DiceRollPresenter) in response to user button clicks.'}

#### 1.5.2.4.0.0 Integration Pattern

Dependency Injection

#### 1.5.2.5.0.0 Communication Protocol

In-memory asynchronous method calls.

#### 1.5.2.6.0.0 Extraction Reasoning

This dependency allows the UI to submit all player game actions for validation and execution without needing to know any of the underlying game rules, ensuring a clean separation of concerns.

### 1.5.3.0.0.0 Interface Name

#### 1.5.3.1.0.0 Interface Name

ITradeOrchestrationService

#### 1.5.3.2.0.0 Source Repository

REPO-AA-004

#### 1.5.3.3.0.0 Method Contracts

- {'method_name': 'ProposeTradeAsync', 'method_signature': 'Task<TradeResult> ProposeTradeAsync(TradeProposal proposal)', 'method_purpose': 'Submits a trade proposal from the human player to an AI for evaluation.', 'integration_context': 'Called by the `TradePresenter` when the user finalizes and submits a trade offer.'}

#### 1.5.3.4.0.0 Integration Pattern

Dependency Injection

#### 1.5.3.5.0.0 Communication Protocol

In-memory asynchronous method calls.

#### 1.5.3.6.0.0 Extraction Reasoning

This interface is required to handle the complex, multi-step trading use case initiated by the player, as specified in requirements like REQ-1-059 and detailed in user stories.

### 1.5.4.0.0.0 Interface Name

#### 1.5.4.1.0.0 Interface Name

IEventBus

#### 1.5.4.2.0.0 Source Repository

REPO-AA-004

#### 1.5.4.3.0.0 Method Contracts

- {'method_name': 'Subscribe<T>', 'method_signature': 'void Subscribe<T>(Action<T> handler) where T : IEvent', 'method_purpose': 'Allows a UI component to register a callback method that will be invoked when a specific game event occurs.', 'integration_context': 'Called by presenters (e.g., HUDPresenter, GameBoardPresenter) during their initialization to listen for game state changes.'}

#### 1.5.4.4.0.0 Integration Pattern

Publish-Subscribe

#### 1.5.4.5.0.0 Communication Protocol

In-memory event aggregation.

#### 1.5.4.6.0.0 Extraction Reasoning

This dependency is critical for decoupling the UI from the game logic. It allows the UI to react to state changes (e.g., an AI-to-AI trade, cash changing) without the game logic needing any knowledge of the UI, fulfilling the pattern shown in sequence diagrams and REQ-1-073.

### 1.5.5.0.0.0 Interface Name

#### 1.5.5.1.0.0 Interface Name

ILogger

#### 1.5.5.2.0.0 Source Repository

REPO-AA-004

#### 1.5.5.3.0.0 Method Contracts

- {'method_name': 'Error', 'method_signature': 'void Error(Exception ex, string messageTemplate, params object[] propertyValues)', 'method_purpose': 'Logs critical, unhandled exceptions caught by the global exception handler.', 'integration_context': 'Called by the `GlobalExceptionHandler` component before it displays the user-facing error dialog, ensuring the error details are persisted for debugging.'}

#### 1.5.5.4.0.0 Integration Pattern

Dependency Injection

#### 1.5.5.5.0.0 Communication Protocol

In-memory synchronous method calls.

#### 1.5.5.6.0.0 Extraction Reasoning

Required to fulfill the reliability requirement REQ-1-023, which mandates that unhandled exceptions are both logged and displayed to the user. This connects the top-level Presentation Layer to the Infrastructure Layer's logging service.

## 1.6.0.0.0.0 Exposed Interfaces

*No items available*

## 1.7.0.0.0.0 Technology Context

### 1.7.1.0.0.0 Framework Requirements

Unity Engine (Latest LTS) with .NET 8. Must adhere to Unity best practices, including the use of prefabs for UI, ScriptableObjects for configuration, and the Addressables system for asset management to support theming (REQ-1-093).

### 1.7.2.0.0.0 Integration Technologies

- Microsoft.Extensions.DependencyInjection (as the DI Container)
- An in-process, custom Event Bus for publish-subscribe communication
- Unity Test Framework for integration testing of presenters and views

### 1.7.3.0.0.0 Performance Constraints

Must maintain an average of 60 FPS (REQ-1-014) and load scenes/saves in under 10 seconds (REQ-1-015). All integration points with lower layers must be asynchronous to prevent blocking the render thread.

### 1.7.4.0.0.0 Security Requirements

Not applicable for this client-side repository. No sensitive data is handled or transmitted.

## 1.8.0.0.0.0 Extraction Validation

| Property | Value |
|----------|-------|
| Mapping Completeness Check | All repository connections have been identified by... |
| Cross Reference Validation | The dependencies on interfaces from `REPO-AA-004` ... |
| Implementation Readiness Assessment | The integration specification is highly actionable... |
| Quality Assurance Confirmation | Systematic review confirms the integration design ... |

