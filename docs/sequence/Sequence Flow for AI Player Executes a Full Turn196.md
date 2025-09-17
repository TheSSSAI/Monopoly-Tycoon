# 1 Overview

## 1.1 Diagram Id

SEQ-FF-001

## 1.2 Name

AI Player Executes a Full Turn

## 1.3 Description

When the game transitions to an AI player's turn, the AI service uses a Behavior Tree to decide on and execute a series of actions. The sequence shows how the AI interacts with the core game logic to manage properties, roll dice, and make decisions, ensuring all its actions are validated by the RuleEngine.

## 1.4 Type

🔹 FeatureFlow

## 1.5 Purpose

To provide a challenging and autonomous opponent for the human player, which is a core objective of the single-player experience.

## 1.6 Complexity

High

## 1.7 Priority

🚨 Critical

## 1.8 Frequency

OnDemand

## 1.9 Participants

- REPO-AS-005
- REPO-DM-001
- REPO-IL-006

## 1.10 Key Interactions

- The Application Services Layer (TurnManagementService) identifies it is an AI's turn.
- It invokes the AIService, providing the current GameState.
- The AIService uses the Domain Layer's AIBehaviorTreeExecutor, which loads behavior parameters from a config file (via Infrastructure Layer).
- The Behavior Tree executes, proposing actions (e.g., 'Build House on X').
- Each proposed action is sent back to Application Services (e.g., PropertyActionService), which validates it with the Domain's RuleEngine.
- If valid, the action is executed and GameState is updated.
- This loop continues for pre-roll management, dice rolling, and post-roll actions.
- The turn ends, and control is passed back to the TurnManagementService.

## 1.11 Triggers

- The game flow transitions to an AI player's turn.

## 1.12 Outcomes

- The AI player completes a full turn according to game rules.
- The GameState is updated based on the AI's actions.
- The Presentation Layer visualizes all AI actions (token movement, transactions, etc.).

## 1.13 Business Rules

- AI decision logic must be implemented using a Behavior Tree (REQ-1-062).
- AI behavior must be tunable via an external configuration file (REQ-1-063).
- The AI must adhere to all official Monopoly rules and have its actions validated by the RuleEngine.

## 1.14 Error Scenarios

- AI configuration file is missing or malformed.
- The AI behavior tree enters an infinite loop or invalid state.

## 1.15 Integration Points

- Local File System (for AI config)

# 2.0 Details

## 2.1 Diagram Id

SEQ-FF-001-IMPL

## 2.2 Name

Implementation: AI Player Turn Execution

## 2.3 Description

Provides a detailed technical blueprint for the sequence of interactions when an AI player executes a full turn. It covers the orchestration by the Application Services layer, decision-making via the Behavior Tree in the Domain layer, rule validation, state updates, and interactions with the Infrastructure layer for configuration and logging.

## 2.4 Participants

### 2.4.1 External Actor

#### 2.4.1.1 Repository Id

GameLoop

#### 2.4.1.2 Display Name

Client / Game Loop

#### 2.4.1.3 Type

🔹 External Actor

#### 2.4.1.4 Technology

Unity Engine Main Thread

#### 2.4.1.5 Order

1

#### 2.4.1.6 Style

| Property | Value |
|----------|-------|
| Shape | actor |
| Color | #E6E6E6 |
| Stereotype | actor |

### 2.4.2.0 Service Layer

#### 2.4.2.1 Repository Id

REPO-AS-005

#### 2.4.2.2 Display Name

Application Services

#### 2.4.2.3 Type

🔹 Service Layer

#### 2.4.2.4 Technology

C# (.NET 8)

#### 2.4.2.5 Order

2

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #D1E8FF |
| Stereotype | TurnManagementService, AIService, PropertyActionSe... |

### 2.4.3.0 Business Logic Layer

#### 2.4.3.1 Repository Id

REPO-DM-001

#### 2.4.3.2 Display Name

Domain Logic

#### 2.4.3.3 Type

🔹 Business Logic Layer

#### 2.4.3.4 Technology

C# (.NET 8)

#### 2.4.3.5 Order

3

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #D5E8D4 |
| Stereotype | AIBehaviorTreeExecutor, RuleEngine |

### 2.4.4.0 Infrastructure Layer

#### 2.4.4.1 Repository Id

REPO-IL-006

#### 2.4.4.2 Display Name

Infrastructure

#### 2.4.4.3 Type

🔹 Infrastructure Layer

#### 2.4.4.4 Technology

C#, Serilog, System.Text.Json

#### 2.4.4.5 Order

4

#### 2.4.4.6 Style

| Property | Value |
|----------|-------|
| Shape | database |
| Color | #FFE6CC |
| Stereotype | JsonConfigurationProvider, LoggingService |

## 2.5.0.0 Interactions

### 2.5.1.0 Asynchronous Method Call

#### 2.5.1.1 Source Id

GameLoop

#### 2.5.1.2 Target Id

REPO-AS-005

#### 2.5.1.3 Message

Start next turn. `TurnManagementService.ProcessNextTurnAsync()`

#### 2.5.1.4 Sequence Number

1

#### 2.5.1.5 Type

🔹 Asynchronous Method Call

#### 2.5.1.6 Is Synchronous

❌ No

#### 2.5.1.7 Return Message



#### 2.5.1.8 Has Return

❌ No

#### 2.5.1.9 Is Activation

✅ Yes

#### 2.5.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Call |
| Method | ProcessNextTurnAsync() |
| Parameters | None. Reads current game state internally. |
| Authentication | N/A |
| Error Handling | Global exception handler catches failures. |
| Performance | Must complete within one frame tick (<16ms). |

### 2.5.2.0 Internal Method Call

#### 2.5.2.1 Source Id

REPO-AS-005

#### 2.5.2.2 Target Id

REPO-AS-005

#### 2.5.2.3 Message

Identify current player is AI. Invoke `AIService.ExecuteTurnAsync(gameState)`.

#### 2.5.2.4 Sequence Number

2

#### 2.5.2.5 Type

🔹 Internal Method Call

#### 2.5.2.6 Is Synchronous

✅ Yes

#### 2.5.2.7 Return Message

Return: Task (signals turn completion)

#### 2.5.2.8 Has Return

✅ Yes

#### 2.5.2.9 Is Activation

✅ Yes

#### 2.5.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Call |
| Method | ExecuteTurnAsync(GameState gameState) |
| Parameters | GameState: The current, mutable game state object. |
| Authentication | N/A |
| Error Handling | Exceptions are caught, logged, and the AI turn is ... |
| Performance | N/A |

### 2.5.3.0 Logging Call

#### 2.5.3.1 Source Id

REPO-AS-005

#### 2.5.3.2 Target Id

REPO-IL-006

#### 2.5.3.3 Message

Log start of AI turn. `ILogger.Information("Executing turn for {PlayerName}")`

#### 2.5.3.4 Sequence Number

3

#### 2.5.3.5 Type

🔹 Logging Call

#### 2.5.3.6 Is Synchronous

❌ No

#### 2.5.3.7 Return Message



#### 2.5.3.8 Has Return

❌ No

#### 2.5.3.9 Is Activation

❌ No

#### 2.5.3.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Call |
| Method | Information(string template, params object[] value... |
| Parameters | Structured log message with AI player's name. |
| Authentication | N/A |
| Error Handling | Logging failures are handled internally by Serilog... |
| Performance | Low overhead. |

### 2.5.4.0 Method Call

#### 2.5.4.1 Source Id

REPO-AS-005

#### 2.5.4.2 Target Id

REPO-DM-001

#### 2.5.4.3 Message

Request pre-roll action. `AIBehaviorTreeExecutor.GetNextActionAsync(gameState, 'PreRoll')`

#### 2.5.4.4 Sequence Number

4

#### 2.5.4.5 Type

🔹 Method Call

#### 2.5.4.6 Is Synchronous

✅ Yes

#### 2.5.4.7 Return Message

Return: `AIAction` (e.g., `BuildHouseAction`) or `null`

#### 2.5.4.8 Has Return

✅ Yes

#### 2.5.4.9 Is Activation

✅ Yes

#### 2.5.4.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Call |
| Method | GetNextActionAsync(GameState gameState, TurnPhase ... |
| Parameters | gameState: The current state. phase: Enum indicati... |
| Authentication | N/A |
| Error Handling | Throws `ConfigurationException` if config is inval... |
| Performance | Decision should be made within 500ms to avoid play... |

#### 2.5.4.11 Nested Interactions

##### 2.5.4.11.1 Data Request

###### 2.5.4.11.1.1 Source Id

REPO-DM-001

###### 2.5.4.11.1.2 Target Id

REPO-IL-006

###### 2.5.4.11.1.3 Message

Load AI config. `JsonConfig.Get<AIParams>(aiDifficulty)`

###### 2.5.4.11.1.4 Sequence Number

4.1

###### 2.5.4.11.1.5 Type

🔹 Data Request

###### 2.5.4.11.1.6 Is Synchronous

✅ Yes

###### 2.5.4.11.1.7 Return Message

Return: `AIParams` object (cached on first load)

###### 2.5.4.11.1.8 Has Return

✅ Yes

###### 2.5.4.11.1.9 Is Activation

❌ No

###### 2.5.4.11.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | File System API |
| Method | Get<T>(string key) |
| Parameters | key: String identifier for AI difficulty. |
| Authentication | N/A |
| Error Handling | Throws `FileNotFoundException` or `JsonException` ... |
| Performance | File I/O, initial load can be slow, subsequent cal... |

##### 2.5.4.11.2.0 Return Data

###### 2.5.4.11.2.1 Source Id

REPO-IL-006

###### 2.5.4.11.2.2 Target Id

REPO-DM-001

###### 2.5.4.11.2.3 Message

Return AI Behavior Parameters

###### 2.5.4.11.2.4 Sequence Number

4.2

###### 2.5.4.11.2.5 Type

🔹 Return Data

###### 2.5.4.11.2.6 Is Synchronous

✅ Yes

###### 2.5.4.11.2.7 Return Message



###### 2.5.4.11.2.8 Has Return

❌ No

###### 2.5.4.11.2.9 Is Activation

❌ No

###### 2.5.4.11.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Memory |
| Method |  |
| Parameters | AIParams: Deserialized object with thresholds, pri... |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Immediate. |

##### 2.5.4.11.3.0 Logging Call

###### 2.5.4.11.3.1 Source Id

REPO-DM-001

###### 2.5.4.11.3.2 Target Id

REPO-IL-006

###### 2.5.4.11.3.3 Message

Log decision path. `ILogger.Debug("Node {NodeName} evaluated to {Result}")`

###### 2.5.4.11.3.4 Sequence Number

4.3

###### 2.5.4.11.3.5 Type

🔹 Logging Call

###### 2.5.4.11.3.6 Is Synchronous

❌ No

###### 2.5.4.11.3.7 Return Message



###### 2.5.4.11.3.8 Has Return

❌ No

###### 2.5.4.11.3.9 Is Activation

❌ No

###### 2.5.4.11.3.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Call |
| Method | Debug(string template, params object[] values) |
| Parameters | Structured log message detailing the Behavior Tree... |
| Authentication | N/A |
| Error Handling | Logging failures are handled internally by Serilog... |
| Performance | Minimal, but can be verbose. |

### 2.5.5.0.0.0 Return Data

#### 2.5.5.1.0.0 Source Id

REPO-DM-001

#### 2.5.5.2.0.0 Target Id

REPO-AS-005

#### 2.5.5.3.0.0 Message

Return proposed action (`BuildHouseAction`)

#### 2.5.5.4.0.0 Sequence Number

5

#### 2.5.5.5.0.0 Type

🔹 Return Data

#### 2.5.5.6.0.0 Is Synchronous

✅ Yes

#### 2.5.5.7.0.0 Return Message



#### 2.5.5.8.0.0 Has Return

❌ No

#### 2.5.5.9.0.0 Is Activation

❌ No

#### 2.5.5.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Memory |
| Method |  |
| Parameters | `AIAction` object containing action type and targe... |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Immediate. |

### 2.5.6.0.0.0 Internal Method Call

#### 2.5.6.1.0.0 Source Id

REPO-AS-005

#### 2.5.6.2.0.0 Target Id

REPO-AS-005

#### 2.5.6.3.0.0 Message

Dispatch action. `PropertyActionService.ExecuteBuildActionAsync(gameState, action)`

#### 2.5.6.4.0.0 Sequence Number

6

#### 2.5.6.5.0.0 Type

🔹 Internal Method Call

#### 2.5.6.6.0.0 Is Synchronous

✅ Yes

#### 2.5.6.7.0.0 Return Message

Return: `ActionResult` (Success or Failure with reason)

#### 2.5.6.8.0.0 Has Return

✅ Yes

#### 2.5.6.9.0.0 Is Activation

✅ Yes

#### 2.5.6.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Call |
| Method | ExecuteBuildActionAsync(GameState gameState, Build... |
| Parameters | The current game state and the specific action DTO... |
| Authentication | N/A |
| Error Handling | Catches validation failures from RuleEngine and re... |
| Performance | Should be very fast (<5ms). |

### 2.5.7.0.0.0 Validation Request

#### 2.5.7.1.0.0 Source Id

REPO-AS-005

#### 2.5.7.2.0.0 Target Id

REPO-DM-001

#### 2.5.7.3.0.0 Message

Validate action. `RuleEngine.ValidateBuildAction(gameState, action)`

#### 2.5.7.4.0.0 Sequence Number

7

#### 2.5.7.5.0.0 Type

🔹 Validation Request

#### 2.5.7.6.0.0 Is Synchronous

✅ Yes

#### 2.5.7.7.0.0 Return Message

Return: `ValidationResult` (contains IsValid and ErrorMessage)

#### 2.5.7.8.0.0 Has Return

✅ Yes

#### 2.5.7.9.0.0 Is Activation

✅ Yes

#### 2.5.7.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Call |
| Method | ValidateBuildAction(GameState gameState, BuildHous... |
| Parameters | Current state and action details. |
| Authentication | N/A |
| Error Handling | This method is pure and should not throw exception... |
| Performance | Critical path; must be highly optimized. |

### 2.5.8.0.0.0 Return Data

#### 2.5.8.1.0.0 Source Id

REPO-DM-001

#### 2.5.8.2.0.0 Target Id

REPO-AS-005

#### 2.5.8.3.0.0 Message

Return `ValidationResult.Success`

#### 2.5.8.4.0.0 Sequence Number

8

#### 2.5.8.5.0.0 Type

🔹 Return Data

#### 2.5.8.6.0.0 Is Synchronous

✅ Yes

#### 2.5.8.7.0.0 Return Message



#### 2.5.8.8.0.0 Has Return

❌ No

#### 2.5.8.9.0.0 Is Activation

❌ No

#### 2.5.8.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Memory |
| Method |  |
| Parameters | `ValidationResult` object. |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Immediate. |

### 2.5.9.0.0.0 State Update

#### 2.5.9.1.0.0 Source Id

REPO-AS-005

#### 2.5.9.2.0.0 Target Id

REPO-DM-001

#### 2.5.9.3.0.0 Message

Apply changes to `GameState` object (e.g., `gameState.Bank.Houses--`, `player.Cash -= cost`)

#### 2.5.9.4.0.0 Sequence Number

9

#### 2.5.9.5.0.0 Type

🔹 State Update

#### 2.5.9.6.0.0 Is Synchronous

✅ Yes

#### 2.5.9.7.0.0 Return Message



#### 2.5.9.8.0.0 Has Return

❌ No

#### 2.5.9.9.0.0 Is Activation

❌ No

#### 2.5.9.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Memory Mutation |
| Method | Direct object property modification. |
| Parameters | N/A |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Immediate. |

### 2.5.10.0.0.0 Logging Call

#### 2.5.10.1.0.0 Source Id

REPO-AS-005

#### 2.5.10.2.0.0 Target Id

REPO-IL-006

#### 2.5.10.3.0.0 Message

Log transaction audit. `ILogger.Information("Transaction: {Type}...")`

#### 2.5.10.4.0.0 Sequence Number

10

#### 2.5.10.5.0.0 Type

🔹 Logging Call

#### 2.5.10.6.0.0 Is Synchronous

❌ No

#### 2.5.10.7.0.0 Return Message



#### 2.5.10.8.0.0 Has Return

❌ No

#### 2.5.10.9.0.0 Is Activation

❌ No

#### 2.5.10.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Call |
| Method | Information(string template, params object[] value... |
| Parameters | Structured log per REQ-1-028, including turn numbe... |
| Authentication | N/A |
| Error Handling | Logging failures are handled internally by Serilog... |
| Performance | Low overhead. |

### 2.5.11.0.0.0 Loop

#### 2.5.11.1.0.0 Source Id

REPO-AS-005

#### 2.5.11.2.0.0 Target Id

REPO-AS-005

#### 2.5.11.3.0.0 Message

Loop back to step 4 until no more pre-roll actions are proposed.

#### 2.5.11.4.0.0 Sequence Number

11

#### 2.5.11.5.0.0 Type

🔹 Loop

#### 2.5.11.6.0.0 Is Synchronous

❌ No

#### 2.5.11.7.0.0 Return Message



#### 2.5.11.8.0.0 Has Return

❌ No

#### 2.5.11.9.0.0 Is Activation

❌ No

#### 2.5.11.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Control Flow |
| Method |  |
| Parameters | Condition: `action != null` |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | N/A |

### 2.5.12.0.0.0 Process Block

#### 2.5.12.1.0.0 Source Id

REPO-AS-005

#### 2.5.12.2.0.0 Target Id

REPO-AS-005

#### 2.5.12.3.0.0 Message

Complete turn (roll dice, move, land on space action) using same pattern (request action -> validate -> execute).

#### 2.5.12.4.0.0 Sequence Number

12

#### 2.5.12.5.0.0 Type

🔹 Process Block

#### 2.5.12.6.0.0 Is Synchronous

✅ Yes

#### 2.5.12.7.0.0 Return Message



#### 2.5.12.8.0.0 Has Return

❌ No

#### 2.5.12.9.0.0 Is Activation

❌ No

#### 2.5.12.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Call |
| Method | Internal private methods within AIService |
| Parameters | gameState |
| Authentication | N/A |
| Error Handling | Follows the established pattern. |
| Performance | N/A |

### 2.5.13.0.0.0 Event Publication

#### 2.5.13.1.0.0 Source Id

REPO-AS-005

#### 2.5.13.2.0.0 Target Id

GameLoop

#### 2.5.13.3.0.0 Message

Notify Turn Complete (event publication or callback)

#### 2.5.13.4.0.0 Sequence Number

13

#### 2.5.13.5.0.0 Type

🔹 Event Publication

#### 2.5.13.6.0.0 Is Synchronous

❌ No

#### 2.5.13.7.0.0 Return Message



#### 2.5.13.8.0.0 Has Return

❌ No

#### 2.5.13.9.0.0 Is Activation

❌ No

#### 2.5.13.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Event Bus |
| Method | Publish(new TurnEndedEvent(playerId)) |
| Parameters | Event object with relevant turn data. |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Immediate. |

## 2.6.0.0.0.0 Notes

### 2.6.1.0.0.0 Content

#### 2.6.1.1.0.0 Content

The GameState object is the single source of truth and is passed by reference to all services to be mutated. This avoids state synchronization issues in a monolithic architecture.

#### 2.6.1.2.0.0 Position

top-right

#### 2.6.1.3.0.0 Participant Id

*Not specified*

#### 2.6.1.4.0.0 Sequence Number

*Not specified*

### 2.6.2.0.0.0 Content

#### 2.6.2.1.0.0 Content

The pre-roll action loop (steps 4-11) continues until the AIBehaviorTreeExecutor returns a 'null' or 'NoAction' object, signaling it has no more strategic moves to make before rolling.

#### 2.6.2.2.0.0 Position

bottom-left

#### 2.6.2.3.0.0 Participant Id

*Not specified*

#### 2.6.2.4.0.0 Sequence Number

11

### 2.6.3.0.0.0 Content

#### 2.6.3.1.0.0 Content

Validation failures from the RuleEngine are not exceptions. They are expected outcomes that the AIService must handle (e.g., by logging the failed attempt and moving on).

#### 2.6.3.2.0.0 Position

middle-right

#### 2.6.3.3.0.0 Participant Id

REPO-DM-001

#### 2.6.3.4.0.0 Sequence Number

7

## 2.7.0.0.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | As an offline application, security focuses on dat... |
| Performance Targets | The entire AI turn execution, especially the AIBeh... |
| Error Handling Strategy | 1. **Configuration Error:** If `ai_params.json` is... |
| Testing Considerations | 1. **Unit Tests (NUnit):** Individual nodes of the... |
| Monitoring Requirements | Adhere strictly to logging requirements: `DEBUG` f... |
| Deployment Considerations | The `ai_params.json` configuration file must be in... |

