# 1 Overview

## 1.1 Diagram Id

SEQ-UJ-002

## 1.2 Name

Human Player Builds a House via Property Management UI

## 1.3 Description

During their turn (pre-roll phase), the human player opens the property management screen, selects a property from a completed monopoly, and successfully builds a house on it. The sequence details the validation of this action against the game's core rules.

## 1.4 Type

🔹 UserJourney

## 1.5 Purpose

To enable the strategic development of properties, a core mechanic of Monopoly, via a dedicated and user-friendly interface as described in US-052.

## 1.6 Complexity

Medium

## 1.7 Priority

🔴 High

## 1.8 Frequency

OnDemand

## 1.9 Participants

- REPO-PRES-001
- REPO-AS-005
- REPO-DM-001

## 1.10 Key Interactions

- User clicks 'Manage Properties' button in the HUD (Presentation Layer).
- Presentation Layer displays the property management UI.
- User selects a valid property and clicks 'Build House'.
- Presentation Layer sends a build request to the Application Services Layer (PropertyActionService).
- PropertyActionService validates the action with the Domain Layer's RuleEngine, which checks all relevant business rules.
- RuleEngine confirms legality, and the PropertyActionService instructs the Domain Layer's GameState to be updated (cash debited, house count incremented).
- Application Services Layer confirms success, and the Presentation Layer refreshes the UI to show updated cash and the new house model on the board.

## 1.11 Triggers

- User interaction with the property management screen.

## 1.12 Outcomes

- The player's cash is reduced by the cost of the house.
- The property's development level is increased by one house.
- The number of houses in the bank is decremented.
- The UI is updated to reflect all state changes.

## 1.13 Business Rules

- Player must own a complete monopoly to build (REQ-1-053).
- The 'even building' rule must be enforced (REQ-1-054).
- Player must have sufficient cash for the purchase.
- There must be houses available in the bank supply (REQ-1-055).

## 1.14 Error Scenarios

- Player attempts to build unevenly.
- Player has insufficient funds.
- No houses are left in the bank.

## 1.15 Integration Points

*No items available*

# 2.0 Details

## 2.1 Diagram Id

SEQ-UJ-002

## 2.2 Name

Human Player Builds a House via Property Management UI

## 2.3 Description

Implementation-ready sequence for the user journey where the human player successfully builds a house. The sequence details the UI interactions, application service orchestration, domain logic validation, and state mutation, adhering to the MVVM pattern in the presentation layer.

## 2.4 Participants

### 2.4.1 Actor

#### 2.4.1.1 Repository Id

User

#### 2.4.1.2 Display Name

Player (Human)

#### 2.4.1.3 Type

🔹 Actor

#### 2.4.1.4 Technology

Human Interaction

#### 2.4.1.5 Order

1

#### 2.4.1.6 Style

| Property | Value |
|----------|-------|
| Shape | actor |
| Color | #E6E6E6 |
| Stereotype | User |

### 2.4.2.0 Layer

#### 2.4.2.1 Repository Id

REPO-PRES-001

#### 2.4.2.2 Display Name

Presentation Layer (UI)

#### 2.4.2.3 Type

🔹 Layer

#### 2.4.2.4 Technology

Unity Engine, C#

#### 2.4.2.5 Order

2

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | boundary |
| Color | #B3D9FF |
| Stereotype | Unity/C# |

### 2.4.3.0 Layer

#### 2.4.3.1 Repository Id

REPO-AS-005

#### 2.4.3.2 Display Name

Application Services

#### 2.4.3.3 Type

🔹 Layer

#### 2.4.3.4 Technology

.NET 8, C#

#### 2.4.3.5 Order

3

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | boundary |
| Color | #C2F0C2 |
| Stereotype | .NET 8 |

### 2.4.4.0 Layer

#### 2.4.4.1 Repository Id

REPO-DM-001

#### 2.4.4.2 Display Name

Domain Layer

#### 2.4.4.3 Type

🔹 Layer

#### 2.4.4.4 Technology

.NET 8, C#

#### 2.4.4.5 Order

4

#### 2.4.4.6 Style

| Property | Value |
|----------|-------|
| Shape | boundary |
| Color | #FFE6B3 |
| Stereotype | .NET 8 |

## 2.5.0.0 Interactions

### 2.5.1.0 UI Event

#### 2.5.1.1 Source Id

User

#### 2.5.1.2 Target Id

REPO-PRES-001

#### 2.5.1.3 Message

Clicks 'Manage Properties' button on HUD

#### 2.5.1.4 Sequence Number

1

#### 2.5.1.5 Type

🔹 UI Event

#### 2.5.1.6 Is Synchronous

✅ Yes

#### 2.5.1.7 Return Message



#### 2.5.1.8 Has Return

❌ No

#### 2.5.1.9 Is Activation

✅ Yes

#### 2.5.1.10 Technical Details

##### 2.5.1.10.1 Protocol

Unity UI Event System

##### 2.5.1.10.2 Method

OnClick()

##### 2.5.1.10.3 Parameters

*No items available*

##### 2.5.1.10.4 Authentication

N/A

##### 2.5.1.10.5 Error Handling

Button is disabled if not player's turn (pre-roll phase).

##### 2.5.1.10.6 Performance

Immediate response (< 16ms).

### 2.5.2.0.0 Internal

#### 2.5.2.1.0 Source Id

REPO-PRES-001

#### 2.5.2.2.0 Target Id

REPO-PRES-001

#### 2.5.2.3.0 Message

ViewManager displays PropertyManagement screen

#### 2.5.2.4.0 Sequence Number

2

#### 2.5.2.5.0 Type

🔹 Internal

#### 2.5.2.6.0 Is Synchronous

✅ Yes

#### 2.5.2.7.0 Return Message



#### 2.5.2.8.0 Has Return

❌ No

#### 2.5.2.9.0 Is Activation

❌ No

#### 2.5.2.10.0 Technical Details

##### 2.5.2.10.1 Protocol

In-Memory

##### 2.5.2.10.2 Method

ViewManager.ShowView('PropertyManagement')

##### 2.5.2.10.3 Parameters

*No items available*

##### 2.5.2.10.4 Authentication

N/A

##### 2.5.2.10.5 Error Handling

Logs error if view prefab is missing.

##### 2.5.2.10.6 Performance

Screen must load and be interactive in < 500ms (NFR).

### 2.5.3.0.0 Data Request

#### 2.5.3.1.0 Source Id

REPO-PRES-001

#### 2.5.3.2.0 Target Id

REPO-AS-005

#### 2.5.3.3.0 Message

PropertyManagementViewModel requests current player asset data

#### 2.5.3.4.0 Sequence Number

3

#### 2.5.3.5.0 Type

🔹 Data Request

#### 2.5.3.6.0 Is Synchronous

✅ Yes

#### 2.5.3.7.0 Return Message

Returns PlayerAssetViewModel DTO

#### 2.5.3.8.0 Has Return

✅ Yes

#### 2.5.3.9.0 Is Activation

✅ Yes

#### 2.5.3.10.0 Technical Details

##### 2.5.3.10.1 Protocol

In-Memory

##### 2.5.3.10.2 Method

GameSessionService.GetPlayerAssets()

##### 2.5.3.10.3 Parameters

- {'name': 'playerId', 'type': 'Guid'}

##### 2.5.3.10.4 Authentication

N/A

##### 2.5.3.10.5 Error Handling

Returns null or throws if game session is not active.

##### 2.5.3.10.6 Performance

Near-instantaneous.

### 2.5.4.0.0 Data Access

#### 2.5.4.1.0 Source Id

REPO-AS-005

#### 2.5.4.2.0 Target Id

REPO-DM-001

#### 2.5.4.3.0 Message

Retrieves current PlayerState from GameState

#### 2.5.4.4.0 Sequence Number

4

#### 2.5.4.5.0 Type

🔹 Data Access

#### 2.5.4.6.0 Is Synchronous

✅ Yes

#### 2.5.4.7.0 Return Message

PlayerState object

#### 2.5.4.8.0 Has Return

✅ Yes

#### 2.5.4.9.0 Is Activation

✅ Yes

#### 2.5.4.10.0 Technical Details

##### 2.5.4.10.1 Protocol

In-Memory

##### 2.5.4.10.2 Method

GameState.GetPlayerState(playerId)

##### 2.5.4.10.3 Parameters

- {'name': 'playerId', 'type': 'Guid'}

##### 2.5.4.10.4 Authentication

N/A

##### 2.5.4.10.5 Error Handling

Throws if player not found.

##### 2.5.4.10.6 Performance

Direct memory access.

### 2.5.5.0.0 UI Update

#### 2.5.5.1.0 Source Id

REPO-PRES-001

#### 2.5.5.2.0 Target Id

REPO-PRES-001

#### 2.5.5.3.0 Message

Populates UI with player's properties and cash from ViewModel

#### 2.5.5.4.0 Sequence Number

5

#### 2.5.5.5.0 Type

🔹 UI Update

#### 2.5.5.6.0 Is Synchronous

✅ Yes

#### 2.5.5.7.0 Return Message



#### 2.5.5.8.0 Has Return

❌ No

#### 2.5.5.9.0 Is Activation

❌ No

#### 2.5.5.10.0 Technical Details

##### 2.5.5.10.1 Protocol

In-Memory

##### 2.5.5.10.2 Method

PropertyManagementView.DataBind(viewModel)

##### 2.5.5.10.3 Parameters

*No items available*

##### 2.5.5.10.4 Authentication

N/A

##### 2.5.5.10.5 Error Handling

UI gracefully handles empty property list (AC-008).

##### 2.5.5.10.6 Performance

Smooth scrolling (60 FPS) must be maintained even with all 28 properties (NFR).

### 2.5.6.0.0 UI Event

#### 2.5.6.1.0 Source Id

User

#### 2.5.6.2.0 Target Id

REPO-PRES-001

#### 2.5.6.3.0 Message

Selects a valid property and clicks 'Build House'

#### 2.5.6.4.0 Sequence Number

6

#### 2.5.6.5.0 Type

🔹 UI Event

#### 2.5.6.6.0 Is Synchronous

✅ Yes

#### 2.5.6.7.0 Return Message



#### 2.5.6.8.0 Has Return

❌ No

#### 2.5.6.9.0 Is Activation

❌ No

#### 2.5.6.10.0 Technical Details

##### 2.5.6.10.1 Protocol

Unity UI Event System

##### 2.5.6.10.2 Method

PropertyManagementViewModel.OnBuildHouseClicked()

##### 2.5.6.10.3 Parameters

- {'name': 'propertyId', 'type': 'int'}

##### 2.5.6.10.4 Authentication

N/A

##### 2.5.6.10.5 Error Handling

Button is pre-emptively disabled based on ViewModel state if action is invalid.

##### 2.5.6.10.6 Performance

Immediate response (< 16ms).

### 2.5.7.0.0 Command

#### 2.5.7.1.0 Source Id

REPO-PRES-001

#### 2.5.7.2.0 Target Id

REPO-AS-005

#### 2.5.7.3.0 Message

Sends BuildHouseRequest to PropertyActionService

#### 2.5.7.4.0 Sequence Number

7

#### 2.5.7.5.0 Type

🔹 Command

#### 2.5.7.6.0 Is Synchronous

✅ Yes

#### 2.5.7.7.0 Return Message

Returns BuildHouseResult DTO

#### 2.5.7.8.0 Has Return

✅ Yes

#### 2.5.7.9.0 Is Activation

✅ Yes

#### 2.5.7.10.0 Technical Details

##### 2.5.7.10.1 Protocol

In-Memory

##### 2.5.7.10.2 Method

PropertyActionService.RequestBuildHouseAsync(request)

##### 2.5.7.10.3 Parameters

- {'name': 'request', 'type': 'BuildHouseRequest', 'schema': '{ playerId: Guid, propertyId: int }'}

##### 2.5.7.10.4 Authentication

N/A

##### 2.5.7.10.5 Error Handling

Service handles invalid requests gracefully.

##### 2.5.7.10.6 Performance

Should complete within 100ms.

### 2.5.8.0.0 Validation

#### 2.5.8.1.0 Source Id

REPO-AS-005

#### 2.5.8.2.0 Target Id

REPO-DM-001

#### 2.5.8.3.0 Message

Validates action with RuleEngine

#### 2.5.8.4.0 Sequence Number

8

#### 2.5.8.5.0 Type

🔹 Validation

#### 2.5.8.6.0 Is Synchronous

✅ Yes

#### 2.5.8.7.0 Return Message

Returns successful ValidationResult

#### 2.5.8.8.0 Has Return

✅ Yes

#### 2.5.8.9.0 Is Activation

✅ Yes

#### 2.5.8.10.0 Technical Details

##### 2.5.8.10.1 Protocol

In-Memory

##### 2.5.8.10.2 Method

RuleEngine.ValidateBuildHouseAction(gameState, playerId, propertyId)

##### 2.5.8.10.3 Parameters

###### 2.5.8.10.3.1 GameState

####### 2.5.8.10.3.1.1 Name

gameState

####### 2.5.8.10.3.1.2 Type

🔹 GameState

###### 2.5.8.10.3.2.0 Guid

####### 2.5.8.10.3.2.1 Name

playerId

####### 2.5.8.10.3.2.2 Type

🔹 Guid

###### 2.5.8.10.3.3.0 int

####### 2.5.8.10.3.3.1 Name

propertyId

####### 2.5.8.10.3.3.2 Type

🔹 int

##### 2.5.8.10.4.0.0 Authentication

N/A

##### 2.5.8.10.5.0.0 Error Handling

Returns ValidationResult object with IsValid=false and specific error reason (e.g., EvenBuildViolation, InsufficientFunds).

##### 2.5.8.10.6.0.0 Performance

Must be highly performant as it's a core logic check.

### 2.5.9.0.0.0.0 State Mutation

#### 2.5.9.1.0.0.0 Source Id

REPO-AS-005

#### 2.5.9.2.0.0.0 Target Id

REPO-DM-001

#### 2.5.9.3.0.0.0 Message

Applies state change to GameState

#### 2.5.9.4.0.0.0 Sequence Number

9

#### 2.5.9.5.0.0.0 Type

🔹 State Mutation

#### 2.5.9.6.0.0.0 Is Synchronous

✅ Yes

#### 2.5.9.7.0.0.0 Return Message

Returns updated PlayerState

#### 2.5.9.8.0.0.0 Has Return

✅ Yes

#### 2.5.9.9.0.0.0 Is Activation

❌ No

#### 2.5.9.10.0.0.0 Technical Details

##### 2.5.9.10.1.0.0 Protocol

In-Memory

##### 2.5.9.10.2.0.0 Method

GameState.ExecuteBuildHouse(playerId, propertyId)

##### 2.5.9.10.3.0.0 Parameters

###### 2.5.9.10.3.1.0 Guid

####### 2.5.9.10.3.1.1 Name

playerId

####### 2.5.9.10.3.1.2 Type

🔹 Guid

###### 2.5.9.10.3.2.0 int

####### 2.5.9.10.3.2.1 Name

propertyId

####### 2.5.9.10.3.2.2 Type

🔹 int

##### 2.5.9.10.4.0.0 Authentication

N/A

##### 2.5.9.10.5.0.0 Error Handling

Throws if state is inconsistent; assumes prior validation.

##### 2.5.9.10.6.0.0 Performance

Direct memory access.

### 2.5.10.0.0.0.0 Event Publication

#### 2.5.10.1.0.0.0 Source Id

REPO-DM-001

#### 2.5.10.2.0.0.0 Target Id

REPO-DM-001

#### 2.5.10.3.0.0.0 Message

Publishes GameStateUpdated event

#### 2.5.10.4.0.0.0 Sequence Number

10

#### 2.5.10.5.0.0.0 Type

🔹 Event Publication

#### 2.5.10.6.0.0.0 Is Synchronous

❌ No

#### 2.5.10.7.0.0.0 Return Message



#### 2.5.10.8.0.0.0 Has Return

❌ No

#### 2.5.10.9.0.0.0 Is Activation

❌ No

#### 2.5.10.10.0.0.0 Technical Details

##### 2.5.10.10.1.0.0 Protocol

In-Process Pub/Sub

##### 2.5.10.10.2.0.0 Method

EventBus.Publish(new GameStateUpdatedEvent(...))

##### 2.5.10.10.3.0.0 Parameters

- {'name': 'event', 'type': 'GameStateUpdatedEvent'}

##### 2.5.10.10.4.0.0 Authentication

N/A

##### 2.5.10.10.5.0.0 Error Handling

N/A (Fire-and-forget).

##### 2.5.10.10.6.0.0 Performance

Asynchronous.

### 2.5.11.0.0.0.0 Response

#### 2.5.11.1.0.0.0 Source Id

REPO-AS-005

#### 2.5.11.2.0.0.0 Target Id

REPO-PRES-001

#### 2.5.11.3.0.0.0 Message

Confirms success to UI controller

#### 2.5.11.4.0.0.0 Sequence Number

11

#### 2.5.11.5.0.0.0 Type

🔹 Response

#### 2.5.11.6.0.0.0 Is Synchronous

✅ Yes

#### 2.5.11.7.0.0.0 Return Message

BuildHouseResult { IsSuccess: true, UpdatedPlayerState: DTO }

#### 2.5.11.8.0.0.0 Has Return

❌ No

#### 2.5.11.9.0.0.0 Is Activation

❌ No

#### 2.5.11.10.0.0.0 Technical Details

##### 2.5.11.10.1.0.0 Protocol

In-Memory

##### 2.5.11.10.2.0.0 Method

return result;

##### 2.5.11.10.3.0.0 Parameters

*No items available*

##### 2.5.11.10.4.0.0 Authentication

N/A

##### 2.5.11.10.5.0.0 Error Handling

Returns IsSuccess: false with error message on validation failure.

##### 2.5.11.10.6.0.0 Performance

N/A

### 2.5.12.0.0.0.0 UI Update

#### 2.5.12.1.0.0.0 Source Id

REPO-PRES-001

#### 2.5.12.2.0.0.0 Target Id

REPO-PRES-001

#### 2.5.12.3.0.0.0 Message

Updates PropertyManagementViewModel and triggers UI refresh

#### 2.5.12.4.0.0.0 Sequence Number

12

#### 2.5.12.5.0.0.0 Type

🔹 UI Update

#### 2.5.12.6.0.0.0 Is Synchronous

✅ Yes

#### 2.5.12.7.0.0.0 Return Message



#### 2.5.12.8.0.0.0 Has Return

❌ No

#### 2.5.12.9.0.0.0 Is Activation

❌ No

#### 2.5.12.10.0.0.0 Technical Details

##### 2.5.12.10.1.0.0 Protocol

In-Memory (MVVM Data Binding)

##### 2.5.12.10.2.0.0 Method

ViewModel.UpdateState(result.UpdatedPlayerState)

##### 2.5.12.10.3.0.0 Parameters

*No items available*

##### 2.5.12.10.4.0.0 Authentication

N/A

##### 2.5.12.10.5.0.0 Error Handling

N/A

##### 2.5.12.10.6.0.0 Performance

UI updates should complete within a single frame.

### 2.5.13.0.0.0.0 Event Subscription

#### 2.5.13.1.0.0.0 Source Id

REPO-PRES-001

#### 2.5.13.2.0.0.0 Target Id

REPO-PRES-001

#### 2.5.13.3.0.0.0 Message

GameBoardPresenter, listening for GameStateUpdated event, adds house model to board

#### 2.5.13.4.0.0.0 Sequence Number

13

#### 2.5.13.5.0.0.0 Type

🔹 Event Subscription

#### 2.5.13.6.0.0.0 Is Synchronous

❌ No

#### 2.5.13.7.0.0.0 Return Message



#### 2.5.13.8.0.0.0 Has Return

❌ No

#### 2.5.13.9.0.0.0 Is Activation

❌ No

#### 2.5.13.10.0.0.0 Technical Details

##### 2.5.13.10.1.0.0 Protocol

In-Process Pub/Sub

##### 2.5.13.10.2.0.0 Method

GameBoardPresenter.OnGameStateUpdated(event)

##### 2.5.13.10.3.0.0 Parameters

*No items available*

##### 2.5.13.10.4.0.0 Authentication

N/A

##### 2.5.13.10.5.0.0 Error Handling

Logs error if 3D model is missing.

##### 2.5.13.10.6.0.0 Performance

Visual update should be non-blocking.

## 2.6.0.0.0.0.0 Notes

### 2.6.1.0.0.0.0 Content

#### 2.6.1.1.0.0.0 Content

The Presentation Layer uses the Model-View-ViewModel (MVVM) pattern. The ViewModel requests data, exposes it to the View, and handles user commands by calling Application Services. This decouples the UI rendering logic from the application's business logic.

#### 2.6.1.2.0.0.0 Position

top_right

#### 2.6.1.3.0.0.0 Participant Id

REPO-PRES-001

#### 2.6.1.4.0.0.0 Sequence Number

3

### 2.6.2.0.0.0.0 Content

#### 2.6.2.1.0.0.0 Content

The RuleEngine is a stateless service. It takes the current game state and a proposed action, and returns a validation result. It does not modify the state itself, adhering to the Command-Query Responsibility Segregation (CQRS) principle at a conceptual level.

#### 2.6.2.2.0.0.0 Position

bottom_right

#### 2.6.2.3.0.0.0 Participant Id

REPO-DM-001

#### 2.6.2.4.0.0.0 Sequence Number

8

### 2.6.3.0.0.0.0 Content

#### 2.6.3.1.0.0.0 Content

Error Scenario: If the RuleEngine returns a validation failure (e.g., InsufficientFunds), the PropertyActionService (step 8) would immediately return a failure result to the UI (step 11), skipping the state mutation steps (9, 10). The UI would then display a non-intrusive notification.

#### 2.6.3.2.0.0.0 Position

bottom_left

#### 2.6.3.3.0.0.0 Participant Id

REPO-AS-005

#### 2.6.3.4.0.0.0 Sequence Number

8

## 2.7.0.0.0.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | Input validation is critical at the Application Se... |
| Performance Targets | The entire user interaction, from click to UI upda... |
| Error Handling Strategy | Validation failures are considered normal operatio... |
| Testing Considerations | Unit tests are required for the `RuleEngine` cover... |
| Monitoring Requirements | Log all build actions at the INFO level, including... |
| Deployment Considerations | N/A for this sequence. |

