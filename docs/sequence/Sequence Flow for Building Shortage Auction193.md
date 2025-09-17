# 1 Overview

## 1.1 Diagram Id

SEQ-FF-020

## 1.2 Name

Building Shortage Auction

## 1.3 Description

Multiple players wish to buy the last remaining house(s) from the bank. The buildings are sold via auction to the highest bidder(s).

## 1.4 Type

🔹 FeatureFlow

## 1.5 Purpose

To correctly implement the rules for resolving competition for a finite supply of buildings.

## 1.6 Complexity

High

## 1.7 Priority

🟡 Medium

## 1.8 Frequency

OnDemand

## 1.9 Participants

- PresentationLayer
- ApplicationServicesLayer
- BusinessLogicLayer

## 1.10 Key Interactions

- A player attempts to build, but the RuleEngine finds an insufficient supply in the Bank.
- The RuleEngine determines other players are also eligible and wish to build.
- TurnManagementService initiates a building auction state.
- The available building(s) are auctioned off one by one.
- The auction process is similar to a property auction, with AI and human bidding.
- The winner pays the bank and the RuleEngine updates their property with the new building.

## 1.11 Triggers

- A player tries to build when the bank's supply of buildings is low and demand is high.

## 1.12 Outcomes

- The last available building(s) are sold to the highest bidder(s).

## 1.13 Business Rules

- If multiple players want the last building(s), they must be auctioned (REQ-1-055).

## 1.14 Error Scenarios

*No items available*

## 1.15 Integration Points

*No items available*

# 2.0 Details

## 2.1 Diagram Id

SEQ-US-052-AC-003

## 2.2 Name

Player Builds a House via Property Management UI

## 2.3 Description

Technical sequence for a human player successfully building a house on a property from the dedicated Property Management screen. This flow covers user input, service layer orchestration, business rule validation, game state mutation, and UI feedback, corresponding to User Story US-052 and Acceptance Criteria AC-003.

## 2.4 Participants

### 2.4.1 UI Controller

#### 2.4.1.1 Repository Id

REPO-PL-UI-001

#### 2.4.1.2 Display Name

PropertyManagementUIController

#### 2.4.1.3 Type

🔹 UI Controller

#### 2.4.1.4 Technology

Unity Engine, C#

#### 2.4.1.5 Order

1

#### 2.4.1.6 Style

| Property | Value |
|----------|-------|
| Shape | actor |
| Color | #4CAF50 |
| Stereotype | Presentation |

### 2.4.2.0 Application Service

#### 2.4.2.1 Repository Id

REPO-AS-005

#### 2.4.2.2 Display Name

PropertyActionService

#### 2.4.2.3 Type

🔹 Application Service

#### 2.4.2.4 Technology

.NET 8, C#

#### 2.4.2.5 Order

2

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #2196F3 |
| Stereotype | Application |

### 2.4.3.0 Domain Service

#### 2.4.3.1 Repository Id

REPO-DM-001

#### 2.4.3.2 Display Name

RuleEngine

#### 2.4.3.3 Type

🔹 Domain Service

#### 2.4.3.4 Technology

.NET 8, C#

#### 2.4.3.5 Order

3

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #FFC107 |
| Stereotype | Domain |

### 2.4.4.0 Domain Aggregate

#### 2.4.4.1 Repository Id

REPO-DM-001

#### 2.4.4.2 Display Name

GameState

#### 2.4.4.3 Type

🔹 Domain Aggregate

#### 2.4.4.4 Technology

.NET 8, C#

#### 2.4.4.5 Order

4

#### 2.4.4.6 Style

| Property | Value |
|----------|-------|
| Shape | database |
| Color | #E91E63 |
| Stereotype | Domain |

### 2.4.5.0 Infrastructure Service

#### 2.4.5.1 Repository Id

REPO-IL-006

#### 2.4.5.2 Display Name

LoggingService

#### 2.4.5.3 Type

🔹 Infrastructure Service

#### 2.4.5.4 Technology

Serilog

#### 2.4.5.5 Order

5

#### 2.4.5.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #9E9E9E |
| Stereotype | Infrastructure |

## 2.5.0.0 Interactions

### 2.5.1.0 User Interaction

#### 2.5.1.1 Source Id

REPO-PL-UI-001

#### 2.5.1.2 Target Id

REPO-PL-UI-001

#### 2.5.1.3 Message

User clicks 'Build House' button for a specific property.

#### 2.5.1.4 Sequence Number

1

#### 2.5.1.5 Type

🔹 User Interaction

#### 2.5.1.6 Is Synchronous

✅ Yes

#### 2.5.1.7 Return Message



#### 2.5.1.8 Has Return

❌ No

#### 2.5.1.9 Is Activation

✅ Yes

#### 2.5.1.10 Technical Details

##### 2.5.1.10.1 Protocol

UI Event

##### 2.5.1.10.2 Method

OnClick()

##### 2.5.1.10.3 Parameters

- {'name': 'eventData', 'type': 'PointerEventData'}

##### 2.5.1.10.4 Authentication

N/A

##### 2.5.1.10.5 Error Handling

UI button is disabled if action is not possible, preventing invalid calls.

##### 2.5.1.10.6 Performance

Immediate response required (<16ms).

### 2.5.2.0.0 Service Call

#### 2.5.2.1.0 Source Id

REPO-PL-UI-001

#### 2.5.2.2.0 Target Id

REPO-AS-005

#### 2.5.2.3.0 Message

Request to build a house on the selected property.

#### 2.5.2.4.0 Sequence Number

2

#### 2.5.2.5.0 Type

🔹 Service Call

#### 2.5.2.6.0 Is Synchronous

✅ Yes

#### 2.5.2.7.0 Return Message

Returns result indicating success and updated state.

#### 2.5.2.8.0 Has Return

✅ Yes

#### 2.5.2.9.0 Is Activation

✅ Yes

#### 2.5.2.10.0 Technical Details

##### 2.5.2.10.1 Protocol

In-Process Method Call

##### 2.5.2.10.2 Method

Task<ActionResult> BuildHouseAsync(Guid playerId, Guid propertyId)

##### 2.5.2.10.3 Parameters

###### 2.5.2.10.3.1 Guid

####### 2.5.2.10.3.1.1 Name

playerId

####### 2.5.2.10.3.1.2 Type

🔹 Guid

####### 2.5.2.10.3.1.3 Description

Unique ID of the human player.

###### 2.5.2.10.3.2.0 Guid

####### 2.5.2.10.3.2.1 Name

propertyId

####### 2.5.2.10.3.2.2 Type

🔹 Guid

####### 2.5.2.10.3.2.3 Description

Unique ID of the property to build on.

##### 2.5.2.10.4.0.0 Authentication

N/A

##### 2.5.2.10.5.0.0 Error Handling

Catches RuleViolationException and translates to ActionResult.Failure. Other exceptions propagate.

##### 2.5.2.10.6.0.0 Performance

Expected latency < 50ms.

### 2.5.3.0.0.0.0 State Retrieval

#### 2.5.3.1.0.0.0 Source Id

REPO-AS-005

#### 2.5.3.2.0.0.0 Target Id

REPO-DM-001

#### 2.5.3.3.0.0.0 Message

Get current game state to perform validation.

#### 2.5.3.4.0.0.0 Sequence Number

3

#### 2.5.3.5.0.0.0 Type

🔹 State Retrieval

#### 2.5.3.6.0.0.0 Is Synchronous

✅ Yes

#### 2.5.3.7.0.0.0 Return Message

Returns a deep copy or immutable view of the current GameState.

#### 2.5.3.8.0.0.0 Has Return

✅ Yes

#### 2.5.3.9.0.0.0 Is Activation

❌ No

#### 2.5.3.10.0.0.0 Technical Details

##### 2.5.3.10.1.0.0 Protocol

In-Process Method Call

##### 2.5.3.10.2.0.0 Method

GameState GetCurrentState()

##### 2.5.3.10.3.0.0 Parameters

*No items available*

##### 2.5.3.10.4.0.0 Authentication

N/A

##### 2.5.3.10.5.0.0 Error Handling

N/A

##### 2.5.3.10.6.0.0 Performance

Expected latency < 1ms.

### 2.5.4.0.0.0.0 Domain Logic Execution

#### 2.5.4.1.0.0.0 Source Id

REPO-AS-005

#### 2.5.4.2.0.0.0 Target Id

REPO-DM-001

#### 2.5.4.3.0.0.0 Message

Validate build action and compute resulting state.

#### 2.5.4.4.0.0.0 Sequence Number

4

#### 2.5.4.5.0.0.0 Type

🔹 Domain Logic Execution

#### 2.5.4.6.0.0.0 Is Synchronous

✅ Yes

#### 2.5.4.7.0.0.0 Return Message

Returns a result object with the new GameState on success.

#### 2.5.4.8.0.0.0 Has Return

✅ Yes

#### 2.5.4.9.0.0.0 Is Activation

✅ Yes

#### 2.5.4.10.0.0.0 Technical Details

##### 2.5.4.10.1.0.0 Protocol

In-Process Method Call

##### 2.5.4.10.2.0.0 Method

RuleResult<GameState> ValidateAndApplyBuildAction(GameState currentState, Guid playerId, Guid propertyId)

##### 2.5.4.10.3.0.0 Parameters

###### 2.5.4.10.3.1.0 GameState

####### 2.5.4.10.3.1.1 Name

currentState

####### 2.5.4.10.3.1.2 Type

🔹 GameState

####### 2.5.4.10.3.1.3 Description

The current state of the game.

###### 2.5.4.10.3.2.0 Guid

####### 2.5.4.10.3.2.1 Name

playerId

####### 2.5.4.10.3.2.2 Type

🔹 Guid

###### 2.5.4.10.3.3.0 Guid

####### 2.5.4.10.3.3.1 Name

propertyId

####### 2.5.4.10.3.3.2 Type

🔹 Guid

##### 2.5.4.10.4.0.0 Authentication

N/A

##### 2.5.4.10.5.0.0 Error Handling

Returns RuleResult.Failure with a specific reason (e.g., INSUFFICIENT_FUNDS, UNEVEN_BUILD) if any business rule is violated. Does not throw exceptions for predictable rule failures.

##### 2.5.4.10.6.0.0 Performance

Expected latency < 10ms.

#### 2.5.4.11.0.0.0 Nested Interactions

##### 2.5.4.11.1.0.0 Internal Check

###### 2.5.4.11.1.1.0 Source Id

REPO-DM-001

###### 2.5.4.11.1.2.0 Target Id

REPO-DM-001

###### 2.5.4.11.1.3.0 Message

Verify player owns a full monopoly for the property.

###### 2.5.4.11.1.4.0 Sequence Number

4.1

###### 2.5.4.11.1.5.0 Type

🔹 Internal Check

###### 2.5.4.11.1.6.0 Is Synchronous

✅ Yes

###### 2.5.4.11.1.7.0 Return Message

boolean

###### 2.5.4.11.1.8.0 Has Return

✅ Yes

###### 2.5.4.11.1.9.0 Is Activation

❌ No

###### 2.5.4.11.1.10.0 Technical Details

####### 2.5.4.11.1.10.1 Protocol

Internal Method Call

####### 2.5.4.11.1.10.2 Method

HasMonopoly(player, property)

####### 2.5.4.11.1.10.3 Parameters

*No items available*

####### 2.5.4.11.1.10.4 Authentication

N/A

####### 2.5.4.11.1.10.5 Error Handling

N/A

####### 2.5.4.11.1.10.6 Performance

Sub-millisecond.

##### 2.5.4.11.2.0.0 Internal Check

###### 2.5.4.11.2.1.0 Source Id

REPO-DM-001

###### 2.5.4.11.2.2.0 Target Id

REPO-DM-001

###### 2.5.4.11.2.3.0 Message

Enforce 'Even Building' rule (BR-001).

###### 2.5.4.11.2.4.0 Sequence Number

4.2

###### 2.5.4.11.2.5.0 Type

🔹 Internal Check

###### 2.5.4.11.2.6.0 Is Synchronous

✅ Yes

###### 2.5.4.11.2.7.0 Return Message

boolean

###### 2.5.4.11.2.8.0 Has Return

✅ Yes

###### 2.5.4.11.2.9.0 Is Activation

❌ No

###### 2.5.4.11.2.10.0 Technical Details

####### 2.5.4.11.2.10.1 Protocol

Internal Method Call

####### 2.5.4.11.2.10.2 Method

IsBuildEven(player, property)

####### 2.5.4.11.2.10.3 Parameters

*No items available*

####### 2.5.4.11.2.10.4 Authentication

N/A

####### 2.5.4.11.2.10.5 Error Handling

N/A

####### 2.5.4.11.2.10.6 Performance

Sub-millisecond.

##### 2.5.4.11.3.0.0 Internal Check

###### 2.5.4.11.3.1.0 Source Id

REPO-DM-001

###### 2.5.4.11.3.2.0 Target Id

REPO-DM-001

###### 2.5.4.11.3.3.0 Message

Check for sufficient player cash and bank supply.

###### 2.5.4.11.3.4.0 Sequence Number

4.3

###### 2.5.4.11.3.5.0 Type

🔹 Internal Check

###### 2.5.4.11.3.6.0 Is Synchronous

✅ Yes

###### 2.5.4.11.3.7.0 Return Message

boolean

###### 2.5.4.11.3.8.0 Has Return

✅ Yes

###### 2.5.4.11.3.9.0 Is Activation

❌ No

###### 2.5.4.11.3.10.0 Technical Details

####### 2.5.4.11.3.10.1 Protocol

Internal Method Call

####### 2.5.4.11.3.10.2 Method

HasSufficientResources(player, bank)

####### 2.5.4.11.3.10.3 Parameters

*No items available*

####### 2.5.4.11.3.10.4 Authentication

N/A

####### 2.5.4.11.3.10.5 Error Handling

N/A

####### 2.5.4.11.3.10.6 Performance

Sub-millisecond.

### 2.5.5.0.0.0.0 State Mutation

#### 2.5.5.1.0.0.0 Source Id

REPO-AS-005

#### 2.5.5.2.0.0.0 Target Id

REPO-DM-001

#### 2.5.5.3.0.0.0 Message

Commit the new game state.

#### 2.5.5.4.0.0.0 Sequence Number

5

#### 2.5.5.5.0.0.0 Type

🔹 State Mutation

#### 2.5.5.6.0.0.0 Is Synchronous

✅ Yes

#### 2.5.5.7.0.0.0 Return Message



#### 2.5.5.8.0.0.0 Has Return

❌ No

#### 2.5.5.9.0.0.0 Is Activation

✅ Yes

#### 2.5.5.10.0.0.0 Technical Details

##### 2.5.5.10.1.0.0 Protocol

In-Process Method Call

##### 2.5.5.10.2.0.0 Method

void UpdateState(GameState newState)

##### 2.5.5.10.3.0.0 Parameters

- {'name': 'newState', 'type': 'GameState', 'description': 'The valid, updated game state returned by the RuleEngine.'}

##### 2.5.5.10.4.0.0 Authentication

N/A

##### 2.5.5.10.5.0.0 Error Handling

A lock or other concurrency control mechanism may be used to ensure atomic updates.

##### 2.5.5.10.6.0.0 Performance

Expected latency < 1ms.

### 2.5.6.0.0.0.0 Logging

#### 2.5.6.1.0.0.0 Source Id

REPO-AS-005

#### 2.5.6.2.0.0.0 Target Id

REPO-IL-006

#### 2.5.6.3.0.0.0 Message

Log successful build transaction for audit.

#### 2.5.6.4.0.0.0 Sequence Number

6

#### 2.5.6.5.0.0.0 Type

🔹 Logging

#### 2.5.6.6.0.0.0 Is Synchronous

❌ No

#### 2.5.6.7.0.0.0 Return Message



#### 2.5.6.8.0.0.0 Has Return

❌ No

#### 2.5.6.9.0.0.0 Is Activation

✅ Yes

#### 2.5.6.10.0.0.0 Technical Details

##### 2.5.6.10.1.0.0 Protocol

In-Process Method Call

##### 2.5.6.10.2.0.0 Method

Information(messageTemplate, propertyValues)

##### 2.5.6.10.3.0.0 Parameters

###### 2.5.6.10.3.1.0 string

####### 2.5.6.10.3.1.1 Name

messageTemplate

####### 2.5.6.10.3.1.2 Type

🔹 string

####### 2.5.6.10.3.1.3 Value

Player {PlayerId} built 1 house on {PropertyId} for {Cost}. Turn: {TurnNumber}

###### 2.5.6.10.3.2.0 object[]

####### 2.5.6.10.3.2.1 Name

propertyValues

####### 2.5.6.10.3.2.2 Type

🔹 object[]

##### 2.5.6.10.4.0.0 Authentication

N/A

##### 2.5.6.10.5.0.0 Error Handling

Logging failures should be caught and ignored to not impact gameplay.

##### 2.5.6.10.6.0.0 Performance

Asynchronous call; should not block the main thread.

### 2.5.7.0.0.0.0 UI Update

#### 2.5.7.1.0.0.0 Source Id

REPO-PL-UI-001

#### 2.5.7.2.0.0.0 Target Id

REPO-PL-UI-001

#### 2.5.7.3.0.0.0 Message

Update UI to reflect new state.

#### 2.5.7.4.0.0.0 Sequence Number

7

#### 2.5.7.5.0.0.0 Type

🔹 UI Update

#### 2.5.7.6.0.0.0 Is Synchronous

✅ Yes

#### 2.5.7.7.0.0.0 Return Message



#### 2.5.7.8.0.0.0 Has Return

❌ No

#### 2.5.7.9.0.0.0 Is Activation

❌ No

#### 2.5.7.10.0.0.0 Technical Details

##### 2.5.7.10.1.0.0 Protocol

UI Rendering

##### 2.5.7.10.2.0.0 Method

RefreshView(updatedPlayerState)

##### 2.5.7.10.3.0.0 Parameters

*No items available*

##### 2.5.7.10.4.0.0 Authentication

N/A

##### 2.5.7.10.5.0.0 Error Handling

N/A

##### 2.5.7.10.6.0.0 Performance

Must complete within a single frame (<16ms).

## 2.6.0.0.0.0.0 Notes

### 2.6.1.0.0.0.0 Content

#### 2.6.1.1.0.0.0 Content

Alternative UI Update: The PropertyActionService could publish a `GameStateUpdated` event. The PropertyManagementUIController would subscribe to this event to decouple it from the service layer, aligning with an Event-Driven pattern.

#### 2.6.1.2.0.0.0 Position

bottom-right

#### 2.6.1.3.0.0.0 Participant Id

REPO-AS-005

#### 2.6.1.4.0.0.0 Sequence Number

7

### 2.6.2.0.0.0.0 Content

#### 2.6.2.1.0.0.0 Content

The RuleEngine is designed to be stateless. It receives the current state, validates an action, and returns a new state without modifying the original. This makes it highly predictable and testable.

#### 2.6.2.2.0.0.0 Position

top-right

#### 2.6.2.3.0.0.0 Participant Id

REPO-DM-001

#### 2.6.2.4.0.0.0 Sequence Number

4

## 2.7.0.0.0.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | N/A. Feature is part of an offline, single-player ... |
| Performance Targets | The end-to-end user interaction (from button click... |
| Error Handling Strategy | The `PropertyActionService` is the primary error h... |
| Testing Considerations | 1. Unit test the `RuleEngine`'s `ValidateAndApplyB... |
| Monitoring Requirements | A structured log entry must be created at the INFO... |
| Deployment Considerations | This feature is a core part of the application and... |

