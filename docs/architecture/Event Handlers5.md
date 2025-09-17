# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Architecture Type

Monolithic with Layered Architecture

## 1.3 Technology Stack

- Unity Engine
- .NET 8
- C#
- SQLite

## 1.4 Bounded Contexts

- Presentation (UI & Rendering)
- Application Services (Use Case Orchestration)
- Business Logic (Domain Rules)
- Infrastructure (Data Persistence, Logging)

# 2.0 Project Specific Events

## 2.1 Event Id

### 2.1.1 Event Id

EVT-001

### 2.1.2 Event Name

GameStateUpdated

### 2.1.3 Event Type

domain

### 2.1.4 Category

🔹 Game State

### 2.1.5 Description

Fired when any significant part of the game state changes, such as a player's cash, property ownership, or position. This is a general-purpose event for UI components to refresh.

### 2.1.6 Trigger Condition

After a game action (e.g., transaction, movement, property purchase) successfully completes.

### 2.1.7 Source Context

Business Logic (Domain) Layer

### 2.1.8 Target Contexts

- Presentation Layer

### 2.1.9 Payload

#### 2.1.9.1 Schema

| Property | Value |
|----------|-------|
| Player Id | Guid |
| Updated Properties | Array<string> |
| New Cash Value | Decimal |

#### 2.1.9.2 Required Fields

- playerId
- updatedProperties

#### 2.1.9.3 Optional Fields

- newCashValue

### 2.1.10.0 Frequency

high

### 2.1.11.0 Business Criticality

normal

### 2.1.12.0 Data Source

| Property | Value |
|----------|-------|
| Database | In-Memory |
| Table | GameState Object |
| Operation | update |

### 2.1.13.0 Routing

| Property | Value |
|----------|-------|
| Routing Key | GameState.Updated |
| Exchange | InProcessBus |
| Queue | N/A (Pub/Sub) |

### 2.1.14.0 Consumers

#### 2.1.14.1 Service

##### 2.1.14.1.1 Service

Presentation Layer

##### 2.1.14.1.2 Handler

HUDController

##### 2.1.14.1.3 Processing Type

async

#### 2.1.14.2.0 Service

##### 2.1.14.2.1 Service

Presentation Layer

##### 2.1.14.2.2 Handler

GameBoardPresenter

##### 2.1.14.2.3 Processing Type

async

### 2.1.15.0.0 Dependencies

*No items available*

### 2.1.16.0.0 Error Handling

| Property | Value |
|----------|-------|
| Retry Strategy | None (Log and ignore) |
| Dead Letter Queue | None |
| Timeout Ms | 100 |

## 2.2.0.0.0 Event Id

### 2.2.1.0.0 Event Id

EVT-002

### 2.2.2.0.0 Event Name

PlayerTurnChanged

### 2.2.3.0.0 Event Type

domain

### 2.2.4.0.0 Category

🔹 Game Flow

### 2.2.5.0.0 Description

Signals the end of one player's turn and the beginning of another's, enabling UI updates and control handoff.

### 2.2.6.0.0 Trigger Condition

A player completes their turn sequence (post-roll phase).

### 2.2.7.0.0 Source Context

Application Services Layer (TurnManagementService)

### 2.2.8.0.0 Target Contexts

- Presentation Layer
- Application Services Layer (AIService)

### 2.2.9.0.0 Payload

#### 2.2.9.1.0 Schema

| Property | Value |
|----------|-------|
| Previous Player Id | Guid |
| Current Player Id | Guid |
| Turn Number | int |

#### 2.2.9.2.0 Required Fields

- currentPlayerId
- turnNumber

#### 2.2.9.3.0 Optional Fields

- previousPlayerId

### 2.2.10.0.0 Frequency

medium

### 2.2.11.0.0 Business Criticality

important

### 2.2.12.0.0 Data Source

| Property | Value |
|----------|-------|
| Database | In-Memory |
| Table | GameState Object |
| Operation | update |

### 2.2.13.0.0 Routing

| Property | Value |
|----------|-------|
| Routing Key | PlayerTurn.Changed |
| Exchange | InProcessBus |
| Queue | N/A (Pub/Sub) |

### 2.2.14.0.0 Consumers

#### 2.2.14.1.0 Service

##### 2.2.14.1.1 Service

Presentation Layer

##### 2.2.14.1.2 Handler

HUDController

##### 2.2.14.1.3 Processing Type

async

#### 2.2.14.2.0 Service

##### 2.2.14.2.1 Service

Application Services Layer

##### 2.2.14.2.2 Handler

AIService

##### 2.2.14.2.3 Processing Type

async

### 2.2.15.0.0 Dependencies

*No items available*

### 2.2.16.0.0 Error Handling

| Property | Value |
|----------|-------|
| Retry Strategy | None (Log and ignore) |
| Dead Letter Queue | None |
| Timeout Ms | 100 |

## 2.3.0.0.0 Event Id

### 2.3.1.0.0 Event Id

EVT-003

### 2.3.2.0.0 Event Name

GameEnded

### 2.3.3.0.0 Event Type

domain

### 2.3.4.0.0 Category

🔹 Game Lifecycle

### 2.3.5.0.0 Description

Announces the conclusion of the game, either by human win or loss, triggering the display of the summary screen. Fulfills REQ-1-068 and REQ-1-069.

### 2.3.6.0.0 Trigger Condition

A player is declared the sole winner after all others are bankrupt.

### 2.3.7.0.0 Source Context

Business Logic (Domain) Layer (RuleEngine)

### 2.3.8.0.0 Target Contexts

- Presentation Layer
- Infrastructure Layer

### 2.3.9.0.0 Payload

#### 2.3.9.1.0 Schema

| Property | Value |
|----------|-------|
| Winner Player Id | Guid |
| Human Result | Win\|Loss |
| Game Result Id | Guid |

#### 2.3.9.2.0 Required Fields

- winnerPlayerId
- humanResult
- gameResultId

#### 2.3.9.3.0 Optional Fields

*No items available*

### 2.3.10.0.0 Frequency

low

### 2.3.11.0.0 Business Criticality

critical

### 2.3.12.0.0 Data Source

| Property | Value |
|----------|-------|
| Database | SQLite |
| Table | GameResult |
| Operation | create |

### 2.3.13.0.0 Routing

| Property | Value |
|----------|-------|
| Routing Key | Game.Ended |
| Exchange | InProcessBus |
| Queue | N/A (Pub/Sub) |

### 2.3.14.0.0 Consumers

#### 2.3.14.1.0 Service

##### 2.3.14.1.1 Service

Presentation Layer

##### 2.3.14.1.2 Handler

ViewManager

##### 2.3.14.1.3 Processing Type

async

#### 2.3.14.2.0 Service

##### 2.3.14.2.1 Service

Infrastructure Layer

##### 2.3.14.2.2 Handler

StatisticsRepository

##### 2.3.14.2.3 Processing Type

async

### 2.3.15.0.0 Dependencies

*No items available*

### 2.3.16.0.0 Error Handling

| Property | Value |
|----------|-------|
| Retry Strategy | None (Failure is critical, log extensively) |
| Dead Letter Queue | None |
| Timeout Ms | 500 |

# 3.0.0.0.0 Event Types And Schema Design

## 3.1.0.0.0 Essential Event Types

### 3.1.1.0.0 Event Name

#### 3.1.1.1.0 Event Name

GameStateUpdated

#### 3.1.1.2.0 Category

🔹 domain

#### 3.1.1.3.0 Description

For decoupling the game logic from UI components that need to react to state changes (e.g., HUD, board visuals).

#### 3.1.1.4.0 Priority

🔴 high

### 3.1.2.0.0 Event Name

#### 3.1.2.1.0 Event Name

PlayerTurnChanged

#### 3.1.2.2.0 Category

🔹 domain

#### 3.1.2.3.0 Description

To manage the game flow and trigger AI actions without tightly coupling the turn manager to the AI service.

#### 3.1.2.4.0 Priority

🔴 high

### 3.1.3.0.0 Event Name

#### 3.1.3.1.0 Event Name

UINotificationRequested

#### 3.1.3.2.0 Category

🔹 integration

#### 3.1.3.3.0 Description

For domain logic to request non-intrusive UI notifications (REQ-1-073) without a direct dependency on the Presentation Layer.

#### 3.1.3.4.0 Priority

🟡 medium

### 3.1.4.0.0 Event Name

#### 3.1.4.1.0 Event Name

GameEnded

#### 3.1.4.2.0 Category

🔹 domain

#### 3.1.4.3.0 Description

To signal the end of the game and trigger finalization logic like showing the summary screen and updating persistent stats.

#### 3.1.4.4.0 Priority

🔴 high

## 3.2.0.0.0 Schema Design

| Property | Value |
|----------|-------|
| Format | JSON |
| Reasoning | The system architecture already specifies JSON for... |
| Consistency Approach | A shared library of event POCOs will be defined an... |

## 3.3.0.0.0 Schema Evolution

| Property | Value |
|----------|-------|
| Backward Compatibility | ✅ |
| Forward Compatibility | ✅ |
| Strategy | Not a primary concern in a monolithic application ... |

## 3.4.0.0.0 Event Structure

### 3.4.1.0.0 Standard Fields

- EventId (Guid)
- Timestamp (DateTimeOffset)
- Version (int)

### 3.4.2.0.0 Metadata Requirements

- CorrelationId to trace the originating user action or game turn.

# 4.0.0.0.0 Event Routing And Processing

## 4.1.0.0.0 Routing Mechanisms

- {'type': 'In-Process Pub/Sub (Mediator Pattern)', 'description': 'An in-memory event bus that allows different components within the same process to communicate without direct dependencies. This is ideal for a monolithic architecture.', 'useCase': 'Decoupling the Business Logic Layer from the Presentation Layer. For example, the RuleEngine publishes a `GameStateUpdated` event, and the HUDController and AudioEngine subscribe to it.'}

## 4.2.0.0.0 Processing Patterns

- {'pattern': 'sequential', 'applicableScenarios': ['All game logic events where the order of operations matters.'], 'implementation': 'Event handlers are invoked synchronously within the event bus. For UI updates, the handler can dispatch the work to the main UI thread to prevent race conditions.'}

## 4.3.0.0.0 Filtering And Subscription

### 4.3.1.0.0 Filtering Mechanism

Type-based filtering. Subscribers register to handle a specific C# event class.

### 4.3.2.0.0 Subscription Model

In-memory registration of handlers against event types during application startup.

### 4.3.3.0.0 Routing Keys

- Not required for in-process pub/sub. The C# class type serves as the routing key.

## 4.4.0.0.0 Handler Isolation

| Property | Value |
|----------|-------|
| Required | ❌ |
| Approach | N/A |
| Reasoning | In a single-process monolithic application, handle... |

## 4.5.0.0.0 Delivery Guarantees

| Property | Value |
|----------|-------|
| Level | at-most-once |
| Justification | Events are in-memory and ephemeral. If the applica... |
| Implementation | Standard in-process event dispatch. No persistence... |

# 5.0.0.0.0 Event Storage And Replay

## 5.1.0.0.0 Persistence Requirements

| Property | Value |
|----------|-------|
| Required | ❌ |
| Duration | N/A |
| Reasoning | The system uses state persistence (JSON save files... |

## 5.2.0.0.0 Event Sourcing

### 5.2.1.0.0 Necessary

❌ No

### 5.2.2.0.0 Justification

The architecture specifies persisting the current `GameState` object, not a log of events (REQ-1-041). Event sourcing is a significant architectural shift that is not required by any functional or non-functional requirement.

### 5.2.3.0.0 Scope

*No items available*

## 5.3.0.0.0 Technology Options

- {'technology': 'N/A', 'suitability': 'low', 'reasoning': 'No event store is required.'}

## 5.4.0.0.0 Replay Capabilities

### 5.4.1.0.0 Required

❌ No

### 5.4.2.0.0 Scenarios

*No items available*

### 5.4.3.0.0 Implementation

N/A

## 5.5.0.0.0 Retention Policy

| Property | Value |
|----------|-------|
| Strategy | No retention |
| Duration | In-memory only, transient |
| Archiving Approach | N/A |

# 6.0.0.0.0 Dead Letter Queue And Error Handling

## 6.1.0.0.0 Dead Letter Strategy

| Property | Value |
|----------|-------|
| Approach | None required. |
| Queue Configuration | N/A |
| Processing Logic | Exceptions from event handlers should be caught, l... |

## 6.2.0.0.0 Retry Policies

- {'errorType': 'Any Exception', 'maxRetries': 0, 'backoffStrategy': 'None', 'delayConfiguration': 'N/A'}

## 6.3.0.0.0 Poison Message Handling

| Property | Value |
|----------|-------|
| Detection Mechanism | N/A |
| Handling Strategy | Not applicable in an in-process system. A faulty e... |
| Alerting Required | ❌ |

## 6.4.0.0.0 Error Notification

### 6.4.1.0.0 Channels

- Local Log File (Serilog)
- User-facing Error Dialog (as per REQ-1-023)

### 6.4.2.0.0 Severity

critical

### 6.4.3.0.0 Recipients

- User (via dialog)
- Support (via log files)

## 6.5.0.0.0 Recovery Procedures

- {'scenario': 'Event handler throws an unhandled exception.', 'procedure': 'The exception is caught by the global handler. A unique error ID is generated and logged. The user is shown an error dialog. The application state remains as it was before the event was published.', 'automationLevel': 'automated'}

# 7.0.0.0.0 Event Versioning Strategy

## 7.1.0.0.0 Schema Evolution Approach

| Property | Value |
|----------|-------|
| Strategy | Synchronous Update |
| Versioning Scheme | Simple integer property on the event POCO (e.g., `... |
| Migration Strategy | All event classes and their handlers are updated a... |

## 7.2.0.0.0 Compatibility Requirements

| Property | Value |
|----------|-------|
| Backward Compatible | ✅ |
| Forward Compatible | ✅ |
| Reasoning | Since all code is deployed together, there will ne... |

## 7.3.0.0.0 Version Identification

| Property | Value |
|----------|-------|
| Mechanism | C# Type System |
| Location | payload |
| Format | The class definition itself serves as the version ... |

## 7.4.0.0.0 Consumer Upgrade Strategy

| Property | Value |
|----------|-------|
| Approach | N/A (monolithic deployment) |
| Rollout Strategy | N/A |
| Rollback Procedure | N/A |

## 7.5.0.0.0 Schema Registry

| Property | Value |
|----------|-------|
| Required | ❌ |
| Technology | N/A |
| Governance | Managed via source control (Git) as part of the mo... |

# 8.0.0.0.0 Event Monitoring And Observability

## 8.1.0.0.0 Monitoring Capabilities

- {'capability': 'Event Logging', 'justification': 'To trace the flow of logic for debugging and analysis, aligning with REQ-1-019.', 'implementation': 'The in-process event bus will be instrumented to log every published event and the result of each handler invocation (success/failure) to Serilog at the DEBUG level.'}

## 8.2.0.0.0 Tracing And Correlation

| Property | Value |
|----------|-------|
| Tracing Required | ✅ |
| Correlation Strategy | A `CorrelationId` (Guid) will be generated at the ... |
| Trace Id Propagation | Passed as a parameter during event publication and... |

## 8.3.0.0.0 Performance Metrics

- {'metric': 'Event Handler Execution Time', 'threshold': '>16ms (to avoid frame drops)', 'alerting': False}

## 8.4.0.0.0 Event Flow Visualization

| Property | Value |
|----------|-------|
| Required | ❌ |
| Tooling | N/A |
| Scope | The flow can be manually reconstructed by analyzin... |

## 8.5.0.0.0 Alerting Requirements

- {'condition': 'Event handler throws an exception.', 'severity': 'critical', 'responseTime': 'N/A (local application)', 'escalationPath': ['Log file', 'User error dialog']}

# 9.0.0.0.0 Implementation Priority

## 9.1.0.0.0 Component

### 9.1.1.0.0 Component

In-Process Event Bus (Mediator)

### 9.1.2.0.0 Priority

🔴 high

### 9.1.3.0.0 Dependencies

*No items available*

### 9.1.4.0.0 Estimated Effort

Low (if using a library like MediatR)

## 9.2.0.0.0 Component

### 9.2.1.0.0 Component

Define Core Domain Event Schemas

### 9.2.2.0.0 Priority

🔴 high

### 9.2.3.0.0 Dependencies

- In-Process Event Bus (Mediator)

### 9.2.4.0.0 Estimated Effort

Low

## 9.3.0.0.0 Component

### 9.3.1.0.0 Component

Instrument Domain Logic to Publish Events

### 9.3.2.0.0 Priority

🟡 medium

### 9.3.3.0.0 Dependencies

- Define Core Domain Event Schemas

### 9.3.4.0.0 Estimated Effort

Medium

## 9.4.0.0.0 Component

### 9.4.1.0.0 Component

Implement UI Handlers to Subscribe to Events

### 9.4.2.0.0 Priority

🟡 medium

### 9.4.3.0.0 Dependencies

- Define Core Domain Event Schemas

### 9.4.4.0.0 Estimated Effort

Medium

# 10.0.0.0.0 Risk Assessment

## 10.1.0.0.0 Risk

### 10.1.1.0.0 Risk

Over-reliance on events for request-response interactions where a direct call is simpler.

### 10.1.2.0.0 Impact

medium

### 10.1.3.0.0 Probability

medium

### 10.1.4.0.0 Mitigation

Establish clear coding guidelines: use events for notifications and state synchronization (fire-and-forget), not for queries or commands that require a direct response.

## 10.2.0.0.0 Risk

### 10.2.1.0.0 Risk

Developers bypass the event bus and create direct dependencies between layers, defeating the purpose of decoupling.

### 10.2.2.0.0 Impact

high

### 10.2.3.0.0 Probability

low

### 10.2.4.0.0 Mitigation

Conduct regular code reviews to enforce the layered architecture and the use of the event bus for cross-cutting concerns.

# 11.0.0.0.0 Recommendations

## 11.1.0.0.0 Category

### 11.1.1.0.0 Category

🔹 Technology

### 11.1.2.0.0 Recommendation

Use a lightweight, well-supported C# library like MediatR to implement the in-process Mediator/Pub-Sub pattern.

### 11.1.3.0.0 Justification

It avoids reinventing the wheel, is trivial to integrate into a .NET project, and provides a clean, dependency-injection-friendly API for publishing events and registering handlers, perfectly fitting the monolithic architecture.

### 11.1.4.0.0 Priority

🔴 high

## 11.2.0.0.0 Category

### 11.2.1.0.0 Category

🔹 Design

### 11.2.2.0.0 Recommendation

Strictly limit event usage to Domain Events for decoupling. Avoid using events for command-style (RPC) calls between services.

### 11.2.3.0.0 Justification

This maintains a clear separation of concerns and prevents the event system from becoming a complex, hard-to-debug web of remote procedure calls. Events should signify that 'something has happened', not 'do this thing'.

### 11.2.4.0.0 Priority

🔴 high

## 11.3.0.0.0 Category

### 11.3.1.0.0 Category

🔹 Observability

### 11.3.2.0.0 Recommendation

Integrate the event bus with the existing Serilog logging infrastructure to log the publication and handling of every event.

### 11.3.3.0.0 Justification

This provides a detailed, traceable audit log of the game's internal state changes with minimal effort, leveraging an already-required system component (REQ-1-018).

### 11.3.4.0.0 Priority

🟡 medium

