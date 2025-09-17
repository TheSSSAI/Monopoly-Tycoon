# 1 Id

REPO-AS-005

# 2 Name

MonopolyTycoon.Application.Services

# 3 Description

This repository is the orchestrator of the application, containing the services that coordinate use cases. It was the primary component of the original `MonopolyTycoon.Application` repository. It connects the user actions from the Presentation Layer with the business logic in the Domain Layer and the data persistence in the Infrastructure Layer. For example, the `GameSessionService` handles the logic for starting a new game or loading a save, coordinating between the `RuleEngine`, `SaveGameRepository`, and UI. It depends on the abstractions in `Application.Abstractions`, not on concrete infrastructure, making it highly testable. This is the 'glue' layer of the application architecture, responsible for managing the flow of control and implementing application-specific logic that doesn't belong in the core domain.

# 4 Type

🔹 Application Services

# 5 Namespace

MonopolyTycoon.Application.Services

# 6 Output Path

src/application/MonopolyTycoon.Application.Services

# 7 Framework

.NET 8

# 8 Language

C#

# 9 Technology

.NET Class Library

# 10 Thirdparty Libraries

*No items available*

# 11 Layer Ids

- app_services_layer

# 12 Dependencies

- REPO-DM-001
- REPO-DR-002
- REPO-DA-003
- REPO-AA-004
- REPO-IL-006

# 13 Requirements

## 13.1 Requirement Id

### 13.1.1 Requirement Id

REQ-1-030

## 13.2.0 Requirement Id

### 13.2.1 Requirement Id

REQ-1-038

## 13.3.0 Requirement Id

### 13.3.1 Requirement Id

REQ-1-059

# 14.0.0 Generate Tests

✅ Yes

# 15.0.0 Generate Documentation

✅ Yes

# 16.0.0 Architecture Style

Layered Architecture

# 17.0.0 Architecture Map

- GameSessionService
- TurnManagementService
- TradeOrchestrationService
- AIService

# 18.0.0 Components Map

- game-session-service-051
- turn-management-service-052

# 19.0.0 Requirements Map

- REQ-1-030
- REQ-1-038

# 20.0.0 Dependency Contracts

## 20.1.0 Repo-Aa-004

### 20.1.1 Required Interfaces

#### 20.1.1.1 Interface

##### 20.1.1.1.1 Interface

ISaveGameRepository

##### 20.1.1.1.2 Methods

- Task SaveAsync(GameState state, int slot)
- Task<GameState> LoadAsync(int slot)

##### 20.1.1.1.3 Events

*No items available*

##### 20.1.1.1.4 Properties

*No items available*

#### 20.1.1.2.0 Interface

##### 20.1.1.2.1 Interface

ILogger

##### 20.1.1.2.2 Methods

- Information(string messageTemplate, params object[] propertyValues)

##### 20.1.1.2.3 Events

*No items available*

##### 20.1.1.2.4 Properties

*No items available*

### 20.1.2.0.0 Integration Pattern

Dependency Injection. Services will be constructed with implementations of these interfaces.

### 20.1.3.0.0 Communication Protocol

In-memory asynchronous method calls.

## 20.2.0.0.0 Repo-Dr-002

### 20.2.1.0.0 Required Interfaces

- {'interface': 'IRuleEngine', 'methods': ['ValidationResult ValidateAction(GameState state, PlayerAction action)', 'GameState ApplyAction(GameState state, PlayerAction action)'], 'events': [], 'properties': []}

### 20.2.2.0.0 Integration Pattern

Dependency Injection. The RuleEngine is injected into services that need to validate or apply game state changes.

### 20.2.3.0.0 Communication Protocol

In-memory synchronous method calls.

# 21.0.0.0.0 Exposed Contracts

## 21.1.0.0.0 Public Interfaces

### 21.1.1.0.0 Interface

#### 21.1.1.1.0 Interface

IGameSessionService

#### 21.1.1.2.0 Methods

- Task StartNewGameAsync(GameSetupOptions options)
- Task LoadGameAsync(int slot)
- Task SaveGameAsync(int slot)

#### 21.1.1.3.0 Events

*No items available*

#### 21.1.1.4.0 Properties

*No items available*

#### 21.1.1.5.0 Consumers

- REPO-PU-010

### 21.1.2.0.0 Interface

#### 21.1.2.1.0 Interface

ITurnManagementService

#### 21.1.2.2.0 Methods

- Task ExecutePlayerActionAsync(PlayerAction action)
- Task EndTurnAsync()

#### 21.1.2.3.0 Events

*No items available*

#### 21.1.2.4.0 Properties

*No items available*

#### 21.1.2.5.0 Consumers

- REPO-PU-010

# 22.0.0.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | All services in this layer are designed to be reso... |
| Event Communication | Can publish application-level events (e.g., `GameL... |
| Data Flow | Acts as a mediator. It translates simple requests ... |
| Error Handling | Catches specific exceptions from the domain (e.g.,... |
| Async Patterns | Heavily uses async/await to orchestrate I/O-bound ... |

# 23.0.0.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Should not contain any UI-framework-specific code ... |
| Performance Considerations | Application services should be lightweight and pri... |
| Security Considerations | N/A |
| Testing Approach | Unit test services extensively by mocking their de... |

# 24.0.0.0.0 Scope Boundaries

## 24.1.0.0.0 Must Implement

- Orchestration of all business use cases (e.g., starting a game, handling a player's turn).
- Management of application state and flow control.
- Coordinating between the domain and infrastructure layers.

## 24.2.0.0.0 Must Not Implement

- Core business rules (this is the Domain layer's responsibility).
- Direct data access logic (this is the Infrastructure layer's responsibility).
- UI rendering or input handling (this is the Presentation layer's responsibility).

## 24.3.0.0.0 Extension Points

- New application services can be added to support new high-level features or use cases.

## 24.4.0.0.0 Validation Rules

- Validates input DTOs from the presentation layer before passing data to the domain (e.g., ensuring player name meets length requirements).

