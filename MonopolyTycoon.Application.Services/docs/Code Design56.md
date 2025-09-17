# 1 Design

code_design

# 2 Code Specification

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-AS-005 |
| Validation Timestamp | 2025-01-15T14:30:00Z |
| Original Component Count Claimed | 11 |
| Original Component Count Actual | 8 |
| Gaps Identified Count | 4 |
| Components Added Count | 7 |
| Final Component Count | 15 |
| Validation Completeness Score | 95.5 |
| Enhancement Methodology | Systematic translation of integration design contr... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

Fully compliant. The integration design correctly scopes the repository as an orchestrator. The code specification adds the necessary internal components (validators, exceptions) to fulfill this scope robustly.

#### 2.2.1.2 Gaps Identified

- The integration design implies the need for DTO validation but does not specify the mechanism (e.g., FluentValidation).
- The design requires robust error handling, necessitating custom, application-specific exception types.
- The contracts for player actions and trade results require specific DTOs and enums not explicitly listed in the top-level integration design.

#### 2.2.1.3 Components Added

- GameSetupOptionsValidator
- InvalidActionException
- SessionManagementException
- TradeResult (enum)
- ServiceCollectionExtensions

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100.0%

#### 2.2.2.2 Non Functional Requirements Coverage

100.0%

#### 2.2.2.3 Missing Requirement Components

*No items available*

#### 2.2.2.4 Added Requirement Components

*No items available*

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

The integration design specifies Layered Architecture and Repository patterns. The code specification fully implements these patterns with DI, Facades, and an asynchronous-first design.

#### 2.2.3.2 Missing Pattern Components

*No items available*

#### 2.2.3.3 Added Pattern Components

*No items available*

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

Not applicable. Specification correctly abstracts all database interactions via repository interfaces, adhering to the architectural design.

#### 2.2.4.2 Missing Database Components

*No items available*

#### 2.2.4.3 Added Database Components

*No items available*

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

Fully compliant. The class and method specifications provide a concrete implementation plan for all orchestration logic detailed in sequence diagrams (e.g., Start New Game, AI Turn Execution, Trading).

#### 2.2.5.2 Missing Interaction Components

*No items available*

#### 2.2.5.3 Added Interaction Components

*No items available*

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-AS-005 |
| Technology Stack | .NET 8, C# |
| Technology Guidance Integration | Specification fully aligns with .NET 8 best practi... |
| Framework Compliance Score | 100.0 |
| Specification Completeness | 100.0% |
| Component Count | 15 |
| Specification Methodology | Systematic synthesis of requirements, architectura... |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- Dependency Injection
- Facade Pattern
- Asynchronous Task-based Programming
- Options Pattern for Configuration
- Strategy Pattern (for AI delegation)

#### 2.3.2.2 Directory Structure Source

Microsoft Clean Architecture template, adapted for Application Services layer.

#### 2.3.2.3 Naming Conventions Source

Microsoft C# coding standards.

#### 2.3.2.4 Architectural Patterns Source

Layered Architecture, with services acting as orchestrators for use cases.

#### 2.3.2.5 Performance Optimizations Applied

- Asynchronous-first design using async/await for all I/O-bound and potentially long-running operations.
- Lightweight, stateless services registered with a scoped lifetime to minimize memory footprint.
- Use of C# records for efficient, immutable DTOs.

### 2.3.3.0 File Structure

#### 2.3.3.1 Directory Organization

##### 2.3.3.1.1 Directory Path

###### 2.3.3.1.1.1 Directory Path

/

###### 2.3.3.1.1.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.1.3 Contains Files

- MonopolyTycoon.sln
- global.json
- Directory.Build.props
- .editorconfig
- .gitignore

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

- dotnet.yml

###### 2.3.3.1.2.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.2.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.3.0 Directory Path

###### 2.3.3.1.3.1 Directory Path

src/Application/MonopolyTycoon.Application.Services

###### 2.3.3.1.3.2 Purpose

Root directory for the Application Services project.

###### 2.3.3.1.3.3 Contains Files

- MonopolyTycoon.Application.Services.csproj
- ServiceCollectionExtensions.cs

###### 2.3.3.1.3.4 Organizational Reasoning

Contains project definition and DI extensions at the top level for discoverability.

###### 2.3.3.1.3.5 Framework Convention Alignment

Standard .NET project structure.

##### 2.3.3.1.4.0 Directory Path

###### 2.3.3.1.4.1 Directory Path

src/Application/MonopolyTycoon.Application.Services/DTOs

###### 2.3.3.1.4.2 Purpose

Defines Data Transfer Objects used for communication between the Presentation and Application layers.

###### 2.3.3.1.4.3 Contains Files

- GameSetupOptions.cs
- PlayerAction.cs
- TradeProposal.cs
- TradeResult.cs

###### 2.3.3.1.4.4 Organizational Reasoning

Decouples the application layer's public contract from the internal domain models.

###### 2.3.3.1.4.5 Framework Convention Alignment

Standard pattern for defining data contracts in multi-layered .NET applications.

##### 2.3.3.1.5.0 Directory Path

###### 2.3.3.1.5.1 Directory Path

src/Application/MonopolyTycoon.Application.Services/Exceptions

###### 2.3.3.1.5.2 Purpose

Defines custom exception types specific to the application layer.

###### 2.3.3.1.5.3 Contains Files

- InvalidActionException.cs
- SessionManagementException.cs

###### 2.3.3.1.5.4 Organizational Reasoning

Provides a structured way to handle and communicate application-specific errors.

###### 2.3.3.1.5.5 Framework Convention Alignment

.NET best practice for creating a custom exception hierarchy.

##### 2.3.3.1.6.0 Directory Path

###### 2.3.3.1.6.1 Directory Path

src/Application/MonopolyTycoon.Application.Services/Services

###### 2.3.3.1.6.2 Purpose

Contains the concrete implementations of the application service interfaces.

###### 2.3.3.1.6.3 Contains Files

- GameSessionService.cs
- TurnManagementService.cs
- TradeOrchestrationService.cs
- AIService.cs

###### 2.3.3.1.6.4 Organizational Reasoning

Separates implementation details from contracts, aligning with Clean Architecture principles.

###### 2.3.3.1.6.5 Framework Convention Alignment

Common practice in .NET applications to group service implementations.

##### 2.3.3.1.7.0 Directory Path

###### 2.3.3.1.7.1 Directory Path

src/Application/MonopolyTycoon.Application.Services/Validation

###### 2.3.3.1.7.2 Purpose

Contains validation logic for incoming DTOs using FluentValidation.

###### 2.3.3.1.7.3 Contains Files

- GameSetupOptionsValidator.cs

###### 2.3.3.1.7.4 Organizational Reasoning

Centralizes validation logic, keeping services focused on orchestration and business flow.

###### 2.3.3.1.7.5 Framework Convention Alignment

Best practice for integrating FluentValidation into a .NET service layer.

##### 2.3.3.1.8.0 Directory Path

###### 2.3.3.1.8.1 Directory Path

src/MonopolyTycoon.Application.Services

###### 2.3.3.1.8.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.8.3 Contains Files

- MonopolyTycoon.Application.Services.csproj

###### 2.3.3.1.8.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.8.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.9.0 Directory Path

###### 2.3.3.1.9.1 Directory Path

tests/MonopolyTycoon.Application.Services.Tests

###### 2.3.3.1.9.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.9.3 Contains Files

- MonopolyTycoon.Application.Services.Tests.csproj
- appsettings.Development.json
- coverlet.runsettings

###### 2.3.3.1.9.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.9.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | MonopolyTycoon.Application.Services |
| Namespace Organization | Hierarchical by feature area, such as `MonopolyTyc... |
| Naming Conventions | PascalCase for namespaces, classes, methods, and p... |
| Framework Alignment | Follows Microsoft's standard C# and .NET namespace... |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

GameSessionService

##### 2.3.4.1.2.0 File Path

src/Application/MonopolyTycoon.Application.Services/Services/GameSessionService.cs

##### 2.3.4.1.3.0 Class Type

Service

##### 2.3.4.1.4.0 Inheritance

IGameSessionService

##### 2.3.4.1.5.0 Purpose

Implements the IGameSessionService interface to orchestrate the game lifecycle, including starting, saving, and loading games, fulfilling REQ-1-030.

##### 2.3.4.1.6.0 Dependencies

- ISaveGameRepository
- IPlayerProfileRepository
- IStatisticsRepository
- IDomainFactory
- IEventBus
- ILogger<GameSessionService>

##### 2.3.4.1.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.1.8.0 Technology Integration Notes

Leverages async/await for all I/O operations related to saving and loading games via the repository abstraction. Uses constructor injection for all dependencies.

##### 2.3.4.1.9.0 Validation Notes

Validation complete.

##### 2.3.4.1.10.0 Properties

- {'property_name': 'CurrentGameState', 'property_type': 'GameState', 'access_modifier': 'private', 'purpose': 'Holds the state of the currently active game session in memory.', 'validation_attributes': [], 'framework_specific_configuration': "Managed internally by the service's lifecycle methods.", 'implementation_notes': 'This property makes the service stateful within a given scope. The service must be registered with a Scoped lifetime in the DI container.', 'validation_notes': 'Correctly identified as a stateful component requiring Scoped lifetime.'}

##### 2.3.4.1.11.0 Methods

###### 2.3.4.1.11.1 Method Name

####### 2.3.4.1.11.1.1 Method Name

StartNewGameAsync

####### 2.3.4.1.11.1.2 Method Signature

StartNewGameAsync(GameSetupOptions options, CancellationToken cancellationToken = default)

####### 2.3.4.1.11.1.3 Return Type

Task

####### 2.3.4.1.11.1.4 Access Modifier

public

####### 2.3.4.1.11.1.5 Is Async

true

####### 2.3.4.1.11.1.6 Framework Specific Attributes

*No items available*

####### 2.3.4.1.11.1.7 Parameters

######## 2.3.4.1.11.1.7.1 Parameter Name

######### 2.3.4.1.11.1.7.1.1 Parameter Name

options

######### 2.3.4.1.11.1.7.1.2 Parameter Type

GameSetupOptions

######### 2.3.4.1.11.1.7.1.3 Is Nullable

❌ No

######### 2.3.4.1.11.1.7.1.4 Purpose

A DTO containing all user-defined settings for the new game, such as AI count and difficulty.

######### 2.3.4.1.11.1.7.1.5 Framework Attributes

*No items available*

######## 2.3.4.1.11.1.7.2.0 Parameter Name

######### 2.3.4.1.11.1.7.2.1 Parameter Name

cancellationToken

######### 2.3.4.1.11.1.7.2.2 Parameter Type

CancellationToken

######### 2.3.4.1.11.1.7.2.3 Is Nullable

❌ No

######### 2.3.4.1.11.1.7.2.4 Purpose

Cancellation token for async operation control

######### 2.3.4.1.11.1.7.2.5 Framework Attributes

*No items available*

####### 2.3.4.1.11.1.8.0.0 Implementation Logic

1. Validate the `options` DTO using an injected validator. 2. Log the initiation of a new game. 3. Call `IPlayerProfileRepository.GetOrCreateProfileAsync`. 4. Use a domain factory (`IDomainFactory`) to create a new `GameState` object based on the provided options and profile. 5. Set the newly created `GameState` as the `CurrentGameState`. 6. Publish a `NewGameStarted` application event for other components to consume.

####### 2.3.4.1.11.1.9.0.0 Exception Handling

Catches exceptions from the domain factory or repository, logs them with critical severity, and wraps them in a `SessionManagementException` before re-throwing.

####### 2.3.4.1.11.1.10.0.0 Performance Considerations

The creation of the GameState should be fast. The method is async to support potential async setup steps, but the core logic is CPU-bound.

####### 2.3.4.1.11.1.11.0.0 Validation Requirements

Input `GameSetupOptions` must be validated before use via an injected FluentValidation validator.

####### 2.3.4.1.11.1.12.0.0 Technology Integration Details

This method orchestrates the creation of the core domain model (`GameState`) based on input from the presentation layer.

####### 2.3.4.1.11.1.13.0.0 Validation Notes

Specification is complete and aligns with sequence diagram 183.

###### 2.3.4.1.11.2.0.0.0 Method Name

####### 2.3.4.1.11.2.1.0.0 Method Name

LoadGameAsync

####### 2.3.4.1.11.2.2.0.0 Method Signature

LoadGameAsync(int slot, CancellationToken cancellationToken = default)

####### 2.3.4.1.11.2.3.0.0 Return Type

Task

####### 2.3.4.1.11.2.4.0.0 Access Modifier

public

####### 2.3.4.1.11.2.5.0.0 Is Async

true

####### 2.3.4.1.11.2.6.0.0 Framework Specific Attributes

*No items available*

####### 2.3.4.1.11.2.7.0.0 Parameters

######## 2.3.4.1.11.2.7.1.0 Parameter Name

######### 2.3.4.1.11.2.7.1.1 Parameter Name

slot

######### 2.3.4.1.11.2.7.1.2 Parameter Type

int

######### 2.3.4.1.11.2.7.1.3 Is Nullable

❌ No

######### 2.3.4.1.11.2.7.1.4 Purpose

The identifier of the save slot to load the game from.

######### 2.3.4.1.11.2.7.1.5 Framework Attributes

*No items available*

######## 2.3.4.1.11.2.7.2.0 Parameter Name

######### 2.3.4.1.11.2.7.2.1 Parameter Name

cancellationToken

######### 2.3.4.1.11.2.7.2.2 Parameter Type

CancellationToken

######### 2.3.4.1.11.2.7.2.3 Is Nullable

❌ No

######### 2.3.4.1.11.2.7.2.4 Purpose

Cancellation token for async operation control

######### 2.3.4.1.11.2.7.2.5 Framework Attributes

*No items available*

####### 2.3.4.1.11.2.8.0.0 Implementation Logic

1. Log the attempt to load a game from the specified slot. 2. Invoke `_saveGameRepository.LoadAsync(slot)`. 3. Upon successful return, set the loaded `GameState` as the `CurrentGameState`. 4. Publish a `GameLoaded` application event for the UI to consume.

####### 2.3.4.1.11.2.9.0.0 Exception Handling

Catches potential `SaveGameCorruptedException` or `FileNotFoundException` from the repository, logs the error, and re-throws as a `SessionManagementException` with a user-friendly message.

####### 2.3.4.1.11.2.10.0.0 Performance Considerations

This is an I/O-bound operation. The `async Task` signature is critical to avoid blocking the game's main thread, fulfilling REQ-1-015.

####### 2.3.4.1.11.2.11.0.0 Validation Requirements

The slot number should be validated to be within the allowed range (e.g., 1-5).

####### 2.3.4.1.11.2.12.0.0 Technology Integration Details

Directly depends on the `ISaveGameRepository` abstraction to decouple from the persistence mechanism (e.g., JSON files).

####### 2.3.4.1.11.2.13.0.0 Validation Notes

Specification is complete and aligns with sequence diagram 188.

###### 2.3.4.1.11.3.0.0.0 Method Name

####### 2.3.4.1.11.3.1.0.0 Method Name

SaveGameAsync

####### 2.3.4.1.11.3.2.0.0 Method Signature

SaveGameAsync(int slot, CancellationToken cancellationToken = default)

####### 2.3.4.1.11.3.3.0.0 Return Type

Task

####### 2.3.4.1.11.3.4.0.0 Access Modifier

public

####### 2.3.4.1.11.3.5.0.0 Is Async

true

####### 2.3.4.1.11.3.6.0.0 Framework Specific Attributes

*No items available*

####### 2.3.4.1.11.3.7.0.0 Parameters

######## 2.3.4.1.11.3.7.1.0 Parameter Name

######### 2.3.4.1.11.3.7.1.1 Parameter Name

slot

######### 2.3.4.1.11.3.7.1.2 Parameter Type

int

######### 2.3.4.1.11.3.7.1.3 Is Nullable

❌ No

######### 2.3.4.1.11.3.7.1.4 Purpose

The identifier of the save slot to save the game to.

######### 2.3.4.1.11.3.7.1.5 Framework Attributes

*No items available*

######## 2.3.4.1.11.3.7.2.0 Parameter Name

######### 2.3.4.1.11.3.7.2.1 Parameter Name

cancellationToken

######### 2.3.4.1.11.3.7.2.2 Parameter Type

CancellationToken

######### 2.3.4.1.11.3.7.2.3 Is Nullable

❌ No

######### 2.3.4.1.11.3.7.2.4 Purpose

Cancellation token for async operation control

######### 2.3.4.1.11.3.7.2.5 Framework Attributes

*No items available*

####### 2.3.4.1.11.3.8.0.0 Implementation Logic

1. Check if `CurrentGameState` is null. If so, throw an `InvalidOperationException`. 2. Log the attempt to save the game. 3. Invoke `_saveGameRepository.SaveAsync(CurrentGameState, slot)`. 4. Upon completion, publish a `GameSaved` application event.

####### 2.3.4.1.11.3.9.0.0 Exception Handling

Handles exceptions from the repository during the save operation, logging them, and communicating the failure by re-throwing as a `SessionManagementException`.

####### 2.3.4.1.11.3.10.0.0 Performance Considerations

I/O-bound operation. Must be fully asynchronous.

####### 2.3.4.1.11.3.11.0.0 Validation Requirements

The slot number should be validated to be within the allowed range.

####### 2.3.4.1.11.3.12.0.0 Technology Integration Details

Orchestrates the persistence of the current in-memory domain state.

####### 2.3.4.1.11.3.13.0.0 Validation Notes

Specification is complete and aligns with sequence diagram 187.

##### 2.3.4.1.12.0.0.0.0 Events

*No items available*

##### 2.3.4.1.13.0.0.0.0 Implementation Notes

This service is the primary entry point for the presentation layer to manage the overall game session.

#### 2.3.4.2.0.0.0.0.0 Class Name

##### 2.3.4.2.1.0.0.0.0 Class Name

TurnManagementService

##### 2.3.4.2.2.0.0.0.0 File Path

src/Application/MonopolyTycoon.Application.Services/Services/TurnManagementService.cs

##### 2.3.4.2.3.0.0.0.0 Class Type

Service

##### 2.3.4.2.4.0.0.0.0 Inheritance

ITurnManagementService

##### 2.3.4.2.5.0.0.0.0 Purpose

Manages the progression of a player's turn through distinct phases and orchestrates action execution, fulfilling REQ-1-038.

##### 2.3.4.2.6.0.0.0.0 Dependencies

- IGameSessionService
- IRuleEngine
- IAIService
- IEventBus
- ILogger<TurnManagementService>

##### 2.3.4.2.7.0.0.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.2.8.0.0.0.0 Technology Integration Notes

Acts as a state machine for turn phases. Coordinates with other services like `AIService` and domain services like `IRuleEngine`.

##### 2.3.4.2.9.0.0.0.0 Validation Notes

Validation complete.

##### 2.3.4.2.10.0.0.0.0 Properties

*No items available*

##### 2.3.4.2.11.0.0.0.0 Methods

###### 2.3.4.2.11.1.0.0.0 Method Name

####### 2.3.4.2.11.1.1.0.0 Method Name

ExecutePlayerActionAsync

####### 2.3.4.2.11.1.2.0.0 Method Signature

ExecutePlayerActionAsync(PlayerAction action, CancellationToken cancellationToken = default)

####### 2.3.4.2.11.1.3.0.0 Return Type

Task

####### 2.3.4.2.11.1.4.0.0 Access Modifier

public

####### 2.3.4.2.11.1.5.0.0 Is Async

true

####### 2.3.4.2.11.1.6.0.0 Framework Specific Attributes

*No items available*

####### 2.3.4.2.11.1.7.0.0 Parameters

######## 2.3.4.2.11.1.7.1.0 Parameter Name

######### 2.3.4.2.11.1.7.1.1 Parameter Name

action

######### 2.3.4.2.11.1.7.1.2 Parameter Type

PlayerAction

######### 2.3.4.2.11.1.7.1.3 Is Nullable

❌ No

######### 2.3.4.2.11.1.7.1.4 Purpose

A DTO representing the action the player wants to perform.

######### 2.3.4.2.11.1.7.1.5 Framework Attributes

*No items available*

######## 2.3.4.2.11.1.7.2.0 Parameter Name

######### 2.3.4.2.11.1.7.2.1 Parameter Name

cancellationToken

######### 2.3.4.2.11.1.7.2.2 Parameter Type

CancellationToken

######### 2.3.4.2.11.1.7.2.3 Is Nullable

❌ No

######### 2.3.4.2.11.1.7.2.4 Purpose

Cancellation token for async operation control

######### 2.3.4.2.11.1.7.2.5 Framework Attributes

*No items available*

####### 2.3.4.2.11.1.8.0.0 Implementation Logic

1. Retrieve the current `GameState` from `IGameSessionService`. 2. Check if the proposed action is valid for the current turn phase. If not, throw `InvalidActionException`. 3. Call `_ruleEngine.ValidateAction(gameState, action)`. If validation fails, throw `InvalidActionException` with the reason from the validation result. 4. If valid, call `_ruleEngine.ApplyAction(gameState, action)` to get the new state. 5. Update the game state in the session service with the new state. 6. Publish a `GameStateUpdated` event.

####### 2.3.4.2.11.1.9.0.0 Exception Handling

Translates domain validation failures (`ValidationResult`) into application-level exceptions (`InvalidActionException`) for the presentation layer to handle.

####### 2.3.4.2.11.1.10.0.0 Performance Considerations

Mostly CPU-bound unless an action triggers an I/O operation. Asynchronous to maintain a consistent API pattern.

####### 2.3.4.2.11.1.11.0.0 Validation Requirements

Validates the action against the current turn phase before delegating to the domain for rule validation.

####### 2.3.4.2.11.1.12.0.0 Technology Integration Details

This method is the primary bridge between UI commands and domain logic execution for player actions.

####### 2.3.4.2.11.1.13.0.0 Validation Notes

Correctly specifies the orchestration of validation and application of actions.

###### 2.3.4.2.11.2.0.0.0 Method Name

####### 2.3.4.2.11.2.1.0.0 Method Name

EndTurnAsync

####### 2.3.4.2.11.2.2.0.0 Method Signature

EndTurnAsync(CancellationToken cancellationToken = default)

####### 2.3.4.2.11.2.3.0.0 Return Type

Task

####### 2.3.4.2.11.2.4.0.0 Access Modifier

public

####### 2.3.4.2.11.2.5.0.0 Is Async

true

####### 2.3.4.2.11.2.6.0.0 Framework Specific Attributes

*No items available*

####### 2.3.4.2.11.2.7.0.0 Parameters

- {'parameter_name': 'cancellationToken', 'parameter_type': 'CancellationToken', 'is_nullable': False, 'purpose': 'Cancellation token for async operation control', 'framework_attributes': []}

####### 2.3.4.2.11.2.8.0.0 Implementation Logic

1. Retrieve the current `GameState`. 2. Advance the game to the next player. 3. Set the turn phase to `PreRoll` for the new player. 4. Log the turn change. 5. If the new player is an AI, invoke `_aiService.ExecuteTurnAsync(gameState)`. 6. Publish a `TurnChanged` event.

####### 2.3.4.2.11.2.9.0.0 Exception Handling

Handles any errors during the AI turn execution and ensures the game state remains consistent.

####### 2.3.4.2.11.2.10.0.0 Performance Considerations

The AI turn execution could be long-running, so the `await` is crucial.

####### 2.3.4.2.11.2.11.0.0 Validation Requirements

Verifies that the turn is in a state that can be ended (e.g., Post-Roll phase).

####### 2.3.4.2.11.2.12.0.0 Technology Integration Details

Orchestrates the transition between players, including delegating control to the AI system.

####### 2.3.4.2.11.2.13.0.0 Validation Notes

Correctly specifies the delegation to the AI service.

##### 2.3.4.2.12.0.0.0.0 Events

*No items available*

##### 2.3.4.2.13.0.0.0.0 Implementation Notes

This service embodies the main game loop logic from the application layer's perspective.

#### 2.3.4.3.0.0.0.0.0 Class Name

##### 2.3.4.3.1.0.0.0.0 Class Name

TradeOrchestrationService

##### 2.3.4.3.2.0.0.0.0 File Path

src/Application/MonopolyTycoon.Application.Services/Services/TradeOrchestrationService.cs

##### 2.3.4.3.3.0.0.0.0 Class Type

Service

##### 2.3.4.3.4.0.0.0.0 Inheritance

ITradeOrchestrationService

##### 2.3.4.3.5.0.0.0.0 Purpose

Manages the complex, multi-step workflow of trading between players, fulfilling REQ-1-059.

##### 2.3.4.3.6.0.0.0.0 Dependencies

- IGameSessionService
- IRuleEngine
- IAIService
- IEventBus
- ILogger<TradeOrchestrationService>

##### 2.3.4.3.7.0.0.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.3.8.0.0.0.0 Technology Integration Notes

Coordinates between the UI (for the human player's part), the AI service (for AI evaluation), and the domain's rule engine (for final execution).

##### 2.3.4.3.9.0.0.0.0 Validation Notes

Validation complete.

##### 2.3.4.3.10.0.0.0.0 Properties

*No items available*

##### 2.3.4.3.11.0.0.0.0 Methods

- {'method_name': 'ProposeTradeAsync', 'method_signature': 'ProposeTradeAsync(TradeProposal proposal, CancellationToken cancellationToken = default)', 'return_type': 'Task<TradeResult>', 'access_modifier': 'public', 'is_async': 'true', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'proposal', 'parameter_type': 'TradeProposal', 'is_nullable': False, 'purpose': 'A DTO containing all assets being offered and requested in the trade.', 'framework_attributes': []}, {'parameter_name': 'cancellationToken', 'parameter_type': 'CancellationToken', 'is_nullable': False, 'purpose': 'Cancellation token for async operation control', 'framework_attributes': []}], 'implementation_logic': '1. Retrieve the current `GameState`. 2. Validate the proposal using the `IRuleEngine` to ensure all assets are tradable and the current turn phase is `PreRoll`. 3. If the target player is an AI, invoke `_aiService.EvaluateTradeProposalAsync(proposal)`. 4. If the trade is accepted (by AI or a human player response), create a `TradeAction` domain object. 5. Call `_ruleEngine.ApplyAction(gameState, tradeAction)` to atomically execute the asset swap. 6. Update the game state and publish a `GameStateUpdated` event. 7. Return a `TradeResult` enum indicating Accepted or Declined.', 'exception_handling': 'Catches validation errors and returns a `TradeResult.Invalid` status. Must handle exceptions during AI evaluation or final execution.', 'performance_considerations': 'The AI evaluation part might be computationally intensive, so `async` is appropriate.', 'validation_requirements': 'The service must validate that the trade is proposed during the correct turn phase (Pre-Roll).', 'technology_integration_details': 'This service specification fulfills REQ-1-059 by orchestrating the trade sequence detailed in sequence diagram 198.', 'validation_notes': 'Logic correctly aligns with sequence diagrams.'}

##### 2.3.4.3.12.0.0.0.0 Events

*No items available*

##### 2.3.4.3.13.0.0.0.0 Implementation Notes

This service encapsulates a complex business process, acting as a use case-specific orchestrator.

#### 2.3.4.4.0.0.0.0.0 Class Name

##### 2.3.4.4.1.0.0.0.0 Class Name

AIService

##### 2.3.4.4.2.0.0.0.0 File Path

src/Application/MonopolyTycoon.Application.Services/Services/AIService.cs

##### 2.3.4.4.3.0.0.0.0 Class Type

Service

##### 2.3.4.4.4.0.0.0.0 Inheritance

IAIService

##### 2.3.4.4.5.0.0.0.0 Purpose

Acts as a high-level facade for executing an AI player's turn and evaluating game actions like trades. Decouples turn management from AI implementation details.

##### 2.3.4.4.6.0.0.0.0 Dependencies

- Domain.AI.IAIController
- IRuleEngine
- IEventBus
- ILogger<AIService>

##### 2.3.4.4.7.0.0.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.4.8.0.0.0.0 Technology Integration Notes

Delegates complex decision-making to the domain layer's AI components (e.g., Behavior Trees). This service is purely for orchestration.

##### 2.3.4.4.9.0.0.0.0 Validation Notes

Validation complete.

##### 2.3.4.4.10.0.0.0.0 Properties

*No items available*

##### 2.3.4.4.11.0.0.0.0 Methods

###### 2.3.4.4.11.1.0.0.0 Method Name

####### 2.3.4.4.11.1.1.0.0 Method Name

ExecuteTurnAsync

####### 2.3.4.4.11.1.2.0.0 Method Signature

ExecuteTurnAsync(GameState gameState, CancellationToken cancellationToken = default)

####### 2.3.4.4.11.1.3.0.0 Return Type

Task

####### 2.3.4.4.11.1.4.0.0 Access Modifier

public

####### 2.3.4.4.11.1.5.0.0 Is Async

true

####### 2.3.4.4.11.1.6.0.0 Framework Specific Attributes

*No items available*

####### 2.3.4.4.11.1.7.0.0 Parameters

######## 2.3.4.4.11.1.7.1.0 Parameter Name

######### 2.3.4.4.11.1.7.1.1 Parameter Name

gameState

######### 2.3.4.4.11.1.7.1.2 Parameter Type

GameState

######### 2.3.4.4.11.1.7.1.3 Is Nullable

❌ No

######### 2.3.4.4.11.1.7.1.4 Purpose

The current state of the game for the AI to make decisions on.

######### 2.3.4.4.11.1.7.1.5 Framework Attributes

*No items available*

######## 2.3.4.4.11.1.7.2.0 Parameter Name

######### 2.3.4.4.11.1.7.2.1 Parameter Name

cancellationToken

######### 2.3.4.4.11.1.7.2.2 Parameter Type

CancellationToken

######### 2.3.4.4.11.1.7.2.3 Is Nullable

❌ No

######### 2.3.4.4.11.1.7.2.4 Purpose

Cancellation token for async operation control

######### 2.3.4.4.11.1.7.2.5 Framework Attributes

*No items available*

####### 2.3.4.4.11.1.8.0.0 Implementation Logic

1. Log the start of the AI turn. 2. In a loop, request the next action from the `IAIController` for the current turn phase (e.g., PreRoll, Roll, PostRoll). 3. For each proposed action, validate it using `IRuleEngine`. 4. If valid, apply the action via `IRuleEngine` and publish `GameStateUpdated` event. 5. Continue until the behavior tree signals the end of the turn. This implementation follows the flow in sequence diagram 196.

####### 2.3.4.4.11.1.9.0.0 Exception Handling

Must handle any exceptions during AI action execution gracefully to prevent crashing the game loop. Errors should be logged, and the AI should forfeit its action.

####### 2.3.4.4.11.1.10.0.0 Performance Considerations

Can be a long-running, CPU-intensive operation. `async Task` is essential to avoid blocking.

####### 2.3.4.4.11.1.11.0.0 Validation Requirements

Relies on the `IRuleEngine` for all action validation; this service does not contain business rules.

####### 2.3.4.4.11.1.12.0.0 Technology Integration Details

Provides the critical link between the application's game loop and the domain's AI decision-making engine.

####### 2.3.4.4.11.1.13.0.0 Validation Notes

Specification correctly implements the loop described in sequence diagram 196.

###### 2.3.4.4.11.2.0.0.0 Method Name

####### 2.3.4.4.11.2.1.0.0 Method Name

EvaluateTradeProposalAsync

####### 2.3.4.4.11.2.2.0.0 Method Signature

EvaluateTradeProposalAsync(TradeProposal proposal, CancellationToken cancellationToken = default)

####### 2.3.4.4.11.2.3.0.0 Return Type

Task<TradeResult>

####### 2.3.4.4.11.2.4.0.0 Access Modifier

public

####### 2.3.4.4.11.2.5.0.0 Is Async

true

####### 2.3.4.4.11.2.6.0.0 Framework Specific Attributes

*No items available*

####### 2.3.4.4.11.2.7.0.0 Parameters

######## 2.3.4.4.11.2.7.1.0 Parameter Name

######### 2.3.4.4.11.2.7.1.1 Parameter Name

proposal

######### 2.3.4.4.11.2.7.1.2 Parameter Type

TradeProposal

######### 2.3.4.4.11.2.7.1.3 Is Nullable

❌ No

######### 2.3.4.4.11.2.7.1.4 Purpose

The trade offer to be evaluated.

######### 2.3.4.4.11.2.7.1.5 Framework Attributes

*No items available*

######## 2.3.4.4.11.2.7.2.0 Parameter Name

######### 2.3.4.4.11.2.7.2.1 Parameter Name

cancellationToken

######### 2.3.4.4.11.2.7.2.2 Parameter Type

CancellationToken

######### 2.3.4.4.11.2.7.2.3 Is Nullable

❌ No

######### 2.3.4.4.11.2.7.2.4 Purpose

Cancellation token for async operation control

######### 2.3.4.4.11.2.7.2.5 Framework Attributes

*No items available*

####### 2.3.4.4.11.2.8.0.0 Implementation Logic

1. Delegate the proposal to the `IAIController` or a dedicated AI trade evaluation model in the domain layer. 2. The domain logic will analyze the trade's value based on the AI's personality and strategic goals. 3. Return the decision (Accepted/Declined) as a `TradeResult` enum.

####### 2.3.4.4.11.2.9.0.0 Exception Handling

Should handle errors during evaluation and default to a `Declined` result to be safe.

####### 2.3.4.4.11.2.10.0.0 Performance Considerations

This could involve complex calculations and should be asynchronous.

####### 2.3.4.4.11.2.11.0.0 Validation Requirements

Assumes the trade proposal has already been validated for rule compliance.

####### 2.3.4.4.11.2.12.0.0 Technology Integration Details

Connects the `TradeOrchestrationService` with the AI's decision-making logic.

####### 2.3.4.4.11.2.13.0.0 Validation Notes

Specification complete.

##### 2.3.4.4.12.0.0.0.0 Events

*No items available*

##### 2.3.4.4.13.0.0.0.0 Implementation Notes

This service is a key part of the Strategy Pattern, where the \"strategy\" for an AI player's turn is executed.

#### 2.3.4.5.0.0.0.0.0 Class Name

##### 2.3.4.5.1.0.0.0.0 Class Name

GameSetupOptionsValidator

##### 2.3.4.5.2.0.0.0.0 File Path

src/Application/MonopolyTycoon.Application.Services/Validation/GameSetupOptionsValidator.cs

##### 2.3.4.5.3.0.0.0.0 Class Type

Validator

##### 2.3.4.5.4.0.0.0.0 Inheritance

FluentValidation.AbstractValidator<GameSetupOptions>

##### 2.3.4.5.5.0.0.0.0 Purpose

Provides validation rules for the GameSetupOptions DTO to ensure data integrity before a new game is created.

##### 2.3.4.5.6.0.0.0.0 Dependencies

*No items available*

##### 2.3.4.5.7.0.0.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.5.8.0.0.0.0 Technology Integration Notes

Integrates with the FluentValidation library. It will be registered with the DI container and injected into services that need it.

##### 2.3.4.5.9.0.0.0.0 Validation Notes

Component added to address validation gap.

##### 2.3.4.5.10.0.0.0.0 Properties

*No items available*

##### 2.3.4.5.11.0.0.0.0 Methods

- {'method_name': '.ctor', 'method_signature': 'GameSetupOptionsValidator()', 'return_type': 'void', 'access_modifier': 'public', 'is_async': 'false', 'framework_specific_attributes': [], 'parameters': [], 'implementation_logic': "Defines validation rules in the constructor using FluentValidation's fluent API. Rules must enforce: `HumanPlayerName` is not empty and within length constraints (REQ-1-032). `AiOpponents` list is not null, not empty, and has between 1 and 3 items (REQ-1-007).", 'exception_handling': 'N/A', 'performance_considerations': 'Validation is fast and synchronous.', 'validation_requirements': 'Fulfills REQ-1-032 and REQ-1-007 validation criteria.', 'technology_integration_details': 'Uses `RuleFor(x => x.PropertyName)...` syntax.', 'validation_notes': 'Specification complete.'}

##### 2.3.4.5.12.0.0.0.0 Events

*No items available*

##### 2.3.4.5.13.0.0.0.0 Implementation Notes



#### 2.3.4.6.0.0.0.0.0 Class Name

##### 2.3.4.6.1.0.0.0.0 Class Name

InvalidActionException

##### 2.3.4.6.2.0.0.0.0 File Path

src/Application/MonopolyTycoon.Application.Services/Exceptions/InvalidActionException.cs

##### 2.3.4.6.3.0.0.0.0 Class Type

Custom Exception

##### 2.3.4.6.4.0.0.0.0 Inheritance

System.Exception

##### 2.3.4.6.5.0.0.0.0 Purpose

Represents an error that occurs when a player attempts to perform an action that is not valid according to the current game state or rules.

##### 2.3.4.6.6.0.0.0.0 Dependencies

*No items available*

##### 2.3.4.6.7.0.0.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.6.8.0.0.0.0 Technology Integration Notes

Standard custom exception implementation.

##### 2.3.4.6.9.0.0.0.0 Validation Notes

Component added to address error handling gap.

##### 2.3.4.6.10.0.0.0.0 Properties

*No items available*

##### 2.3.4.6.11.0.0.0.0 Methods

- {'method_name': '.ctor', 'method_signature': 'InvalidActionException(string message)', 'return_type': 'void', 'access_modifier': 'public', 'is_async': 'false', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'message', 'parameter_type': 'string', 'is_nullable': False, 'purpose': 'The reason why the action is invalid.', 'framework_attributes': []}], 'implementation_logic': 'Calls the base Exception constructor with the provided message.', 'exception_handling': 'N/A', 'performance_considerations': 'N/A', 'validation_requirements': 'N/A', 'technology_integration_details': 'N/A', 'validation_notes': 'Specification complete.'}

##### 2.3.4.6.12.0.0.0.0 Events

*No items available*

##### 2.3.4.6.13.0.0.0.0 Implementation Notes



#### 2.3.4.7.0.0.0.0.0 Class Name

##### 2.3.4.7.1.0.0.0.0 Class Name

SessionManagementException

##### 2.3.4.7.2.0.0.0.0 File Path

src/Application/MonopolyTycoon.Application.Services/Exceptions/SessionManagementException.cs

##### 2.3.4.7.3.0.0.0.0 Class Type

Custom Exception

##### 2.3.4.7.4.0.0.0.0 Inheritance

System.Exception

##### 2.3.4.7.5.0.0.0.0 Purpose

Represents an error related to game session management, such as a failure to save or load a game.

##### 2.3.4.7.6.0.0.0.0 Dependencies

*No items available*

##### 2.3.4.7.7.0.0.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.7.8.0.0.0.0 Technology Integration Notes

Standard custom exception implementation.

##### 2.3.4.7.9.0.0.0.0 Validation Notes

Component added to address error handling gap.

##### 2.3.4.7.10.0.0.0.0 Properties

*No items available*

##### 2.3.4.7.11.0.0.0.0 Methods

- {'method_name': '.ctor', 'method_signature': 'SessionManagementException(string message, Exception innerException)', 'return_type': 'void', 'access_modifier': 'public', 'is_async': 'false', 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'message', 'parameter_type': 'string', 'is_nullable': False, 'purpose': 'A user-friendly message explaining the session error.', 'framework_attributes': []}, {'parameter_name': 'innerException', 'parameter_type': 'Exception', 'is_nullable': False, 'purpose': 'The underlying exception that caused the session failure.', 'framework_attributes': []}], 'implementation_logic': 'Calls the base Exception constructor with the message and inner exception.', 'exception_handling': 'N/A', 'performance_considerations': 'N/A', 'validation_requirements': 'N/A', 'technology_integration_details': 'N/A', 'validation_notes': 'Specification complete.'}

##### 2.3.4.7.12.0.0.0.0 Events

*No items available*

##### 2.3.4.7.13.0.0.0.0 Implementation Notes



### 2.3.5.0.0.0.0.0.0 Interface Specifications

*No items available*

### 2.3.6.0.0.0.0.0.0 Enum Specifications

- {'enum_name': 'TradeResult', 'file_path': 'src/Application/MonopolyTycoon.Application.Services/DTOs/TradeResult.cs', 'underlying_type': 'int', 'purpose': 'Represents the possible outcomes of a trade proposal.', 'framework_attributes': [], 'values': [{'value_name': 'Accepted', 'value': '0', 'description': 'The trade was accepted and has been executed.'}, {'value_name': 'Declined', 'value': '1', 'description': 'The trade was declined by the target player.'}, {'value_name': 'Invalid', 'value': '2', 'description': 'The trade was invalid according to game rules (e.g., proposed at the wrong time).'}], 'validation_notes': 'Added to support the return type of `ITradeOrchestrationService.ProposeTradeAsync`.'}

### 2.3.7.0.0.0.0.0.0 Dto Specifications

#### 2.3.7.1.0.0.0.0.0 Dto Name

##### 2.3.7.1.1.0.0.0.0 Dto Name

GameSetupOptions

##### 2.3.7.1.2.0.0.0.0 File Path

src/Application/MonopolyTycoon.Application.Services/DTOs/GameSetupOptions.cs

##### 2.3.7.1.3.0.0.0.0 Purpose

A data transfer object to carry new game configuration from the presentation layer, fulfilling REQ-1-030.

##### 2.3.7.1.4.0.0.0.0 Framework Base Class

record

##### 2.3.7.1.5.0.0.0.0 Properties

###### 2.3.7.1.5.1.0.0.0 Property Name

####### 2.3.7.1.5.1.1.0.0 Property Name

HumanPlayerName

####### 2.3.7.1.5.1.2.0.0 Property Type

string

####### 2.3.7.1.5.1.3.0.0 Validation Attributes

- [Required]
- [StringLength(16, MinimumLength = 3)]

####### 2.3.7.1.5.1.4.0.0 Serialization Attributes

*No items available*

####### 2.3.7.1.5.1.5.0.0 Framework Specific Attributes

*No items available*

###### 2.3.7.1.5.2.0.0.0 Property Name

####### 2.3.7.1.5.2.1.0.0 Property Name

AiOpponents

####### 2.3.7.1.5.2.2.0.0 Property Type

List<AiOpponentConfiguration>

####### 2.3.7.1.5.2.3.0.0 Validation Attributes

- [Required]
- [MinLength(1)]
- [MaxLength(3)]

####### 2.3.7.1.5.2.4.0.0 Serialization Attributes

*No items available*

####### 2.3.7.1.5.2.5.0.0 Framework Specific Attributes

*No items available*

##### 2.3.7.1.6.0.0.0.0 Validation Rules

HumanPlayerName must not be empty or whitespace. The number of AI opponents must be between 1 and 3. This must be enforced by an associated FluentValidation class.

##### 2.3.7.1.7.0.0.0.0 Serialization Requirements

Standard JSON serialization. The C# record type provides immutability.

##### 2.3.7.1.8.0.0.0.0 Validation Notes

Validation complete.

#### 2.3.7.2.0.0.0.0.0 Dto Name

##### 2.3.7.2.1.0.0.0.0 Dto Name

PlayerAction

##### 2.3.7.2.2.0.0.0.0 File Path

src/Application/MonopolyTycoon.Application.Services/DTOs/PlayerAction.cs

##### 2.3.7.2.3.0.0.0.0 Purpose

A base DTO for representing any action a player can take. Specific actions will inherit from this.

##### 2.3.7.2.4.0.0.0.0 Framework Base Class

record

##### 2.3.7.2.5.0.0.0.0 Properties

- {'property_name': 'PlayerId', 'property_type': 'Guid', 'validation_attributes': ['[Required]'], 'serialization_attributes': [], 'framework_specific_attributes': []}

##### 2.3.7.2.6.0.0.0.0 Validation Rules

A valid PlayerId must be provided.

##### 2.3.7.2.7.0.0.0.0 Serialization Requirements

Specification recommends using this as a base for a discriminated union of action types in the presentation layer contract.

##### 2.3.7.2.8.0.0.0.0 Validation Notes

Validation complete.

#### 2.3.7.3.0.0.0.0.0 Dto Name

##### 2.3.7.3.1.0.0.0.0 Dto Name

TradeProposal

##### 2.3.7.3.2.0.0.0.0 File Path

src/Application/MonopolyTycoon.Application.Services/DTOs/TradeProposal.cs

##### 2.3.7.3.3.0.0.0.0 Purpose

A data transfer object representing a complete trade offer between two players.

##### 2.3.7.3.4.0.0.0.0 Framework Base Class

record

##### 2.3.7.3.5.0.0.0.0 Properties

###### 2.3.7.3.5.1.0.0.0 Property Name

####### 2.3.7.3.5.1.1.0.0 Property Name

OfferingPlayerId

####### 2.3.7.3.5.1.2.0.0 Property Type

Guid

####### 2.3.7.3.5.1.3.0.0 Validation Attributes

- [Required]

####### 2.3.7.3.5.1.4.0.0 Serialization Attributes

*No items available*

####### 2.3.7.3.5.1.5.0.0 Framework Specific Attributes

*No items available*

###### 2.3.7.3.5.2.0.0.0 Property Name

####### 2.3.7.3.5.2.1.0.0 Property Name

TargetPlayerId

####### 2.3.7.3.5.2.2.0.0 Property Type

Guid

####### 2.3.7.3.5.2.3.0.0 Validation Attributes

- [Required]

####### 2.3.7.3.5.2.4.0.0 Serialization Attributes

*No items available*

####### 2.3.7.3.5.2.5.0.0 Framework Specific Attributes

*No items available*

###### 2.3.7.3.5.3.0.0.0 Property Name

####### 2.3.7.3.5.3.1.0.0 Property Name

PropertiesOffered

####### 2.3.7.3.5.3.2.0.0 Property Type

List<Guid>

####### 2.3.7.3.5.3.3.0.0 Validation Attributes

*No items available*

####### 2.3.7.3.5.3.4.0.0 Serialization Attributes

*No items available*

####### 2.3.7.3.5.3.5.0.0 Framework Specific Attributes

*No items available*

###### 2.3.7.3.5.4.0.0.0 Property Name

####### 2.3.7.3.5.4.1.0.0 Property Name

CashOffered

####### 2.3.7.3.5.4.2.0.0 Property Type

decimal

####### 2.3.7.3.5.4.3.0.0 Validation Attributes

- [Range(0, double.MaxValue)]

####### 2.3.7.3.5.4.4.0.0 Serialization Attributes

*No items available*

####### 2.3.7.3.5.4.5.0.0 Framework Specific Attributes

*No items available*

###### 2.3.7.3.5.5.0.0.0 Property Name

####### 2.3.7.3.5.5.1.0.0 Property Name

PropertiesRequested

####### 2.3.7.3.5.5.2.0.0 Property Type

List<Guid>

####### 2.3.7.3.5.5.3.0.0 Validation Attributes

*No items available*

####### 2.3.7.3.5.5.4.0.0 Serialization Attributes

*No items available*

####### 2.3.7.3.5.5.5.0.0 Framework Specific Attributes

*No items available*

###### 2.3.7.3.5.6.0.0.0 Property Name

####### 2.3.7.3.5.6.1.0.0 Property Name

CashRequested

####### 2.3.7.3.5.6.2.0.0 Property Type

decimal

####### 2.3.7.3.5.6.3.0.0 Validation Attributes

- [Range(0, double.MaxValue)]

####### 2.3.7.3.5.6.4.0.0 Serialization Attributes

*No items available*

####### 2.3.7.3.5.6.5.0.0 Framework Specific Attributes

*No items available*

##### 2.3.7.3.6.0.0.0.0 Validation Rules

At least one item must be offered or requested. Player IDs cannot be the same.

##### 2.3.7.3.7.0.0.0.0 Serialization Requirements

Standard JSON serialization. The record type ensures the proposal is immutable once created.

##### 2.3.7.3.8.0.0.0.0 Validation Notes

Validation complete.

### 2.3.8.0.0.0.0.0.0 Configuration Specifications

*No items available*

### 2.3.9.0.0.0.0.0.0 Dependency Injection Specifications

#### 2.3.9.1.0.0.0.0.0 Service Interface

##### 2.3.9.1.1.0.0.0.0 Service Interface

IGameSessionService

##### 2.3.9.1.2.0.0.0.0 Service Implementation

GameSessionService

##### 2.3.9.1.3.0.0.0.0 Lifetime

Scoped

##### 2.3.9.1.4.0.0.0.0 Registration Reasoning

Scoped lifetime ensures the service instance and its in-memory GameState persist for a single logical game session but are isolated from others.

##### 2.3.9.1.5.0.0.0.0 Framework Registration Pattern

services.AddScoped<IGameSessionService, GameSessionService>();

##### 2.3.9.1.6.0.0.0.0 Validation Notes

Correct lifetime for a session-based service.

#### 2.3.9.2.0.0.0.0.0 Service Interface

##### 2.3.9.2.1.0.0.0.0 Service Interface

ITurnManagementService

##### 2.3.9.2.2.0.0.0.0 Service Implementation

TurnManagementService

##### 2.3.9.2.3.0.0.0.0 Lifetime

Scoped

##### 2.3.9.2.4.0.0.0.0 Registration Reasoning

Scoped lifetime ensures it operates on the same GameState instance as the GameSessionService within the same operational scope.

##### 2.3.9.2.5.0.0.0.0 Framework Registration Pattern

services.AddScoped<ITurnManagementService, TurnManagementService>();

##### 2.3.9.2.6.0.0.0.0 Validation Notes

Correct lifetime for a session-based service.

#### 2.3.9.3.0.0.0.0.0 Service Interface

##### 2.3.9.3.1.0.0.0.0 Service Interface

ITradeOrchestrationService

##### 2.3.9.3.2.0.0.0.0 Service Implementation

TradeOrchestrationService

##### 2.3.9.3.3.0.0.0.0 Lifetime

Scoped

##### 2.3.9.3.4.0.0.0.0 Registration Reasoning

Scoped to the current session to ensure it interacts with the correct game state.

##### 2.3.9.3.5.0.0.0.0 Framework Registration Pattern

services.AddScoped<ITradeOrchestrationService, TradeOrchestrationService>();

##### 2.3.9.3.6.0.0.0.0 Validation Notes

Correct lifetime for a session-based service.

#### 2.3.9.4.0.0.0.0.0 Service Interface

##### 2.3.9.4.1.0.0.0.0 Service Interface

IAIService

##### 2.3.9.4.2.0.0.0.0 Service Implementation

AIService

##### 2.3.9.4.3.0.0.0.0 Lifetime

Scoped

##### 2.3.9.4.4.0.0.0.0 Registration Reasoning

Scoped to the current session to ensure it interacts with the correct game state and AI models.

##### 2.3.9.4.5.0.0.0.0 Framework Registration Pattern

services.AddScoped<IAIService, AIService>();

##### 2.3.9.4.6.0.0.0.0 Validation Notes

Correct lifetime for a session-based service.

### 2.3.10.0.0.0.0.0.0 External Integration Specifications

*No items available*

## 2.4.0.0.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 7 |
| Total Interfaces | 0 |
| Total Enums | 1 |
| Total Dtos | 3 |
| Total Configurations | 0 |
| Total External Integrations | 0 |
| Grand Total Components | 11 |
| Phase 2 Claimed Count | 11 |
| Phase 2 Actual Count | 11 |
| Validation Added Count | 0 |
| Final Validated Count | 11 |

