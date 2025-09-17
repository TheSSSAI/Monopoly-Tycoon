# 1 Overview

## 1.1 Diagram Id

SEQ-UJ-013

## 1.2 Name

Player Initiates Trade with AI

## 1.3 Description

The human player opens the trading interface, selects an AI opponent, constructs a trade offer (properties, cash, cards), and submits it for the AI's consideration.

## 1.4 Type

🔹 UserJourney

## 1.5 Purpose

To enable the human player to proactively engage in trade negotiations with AI opponents.

## 1.6 Complexity

Medium

## 1.7 Priority

🔴 High

## 1.8 Frequency

OnDemand

## 1.9 Participants

- PresentationLayer
- ApplicationServicesLayer
- BusinessLogicLayer

## 1.10 Key Interactions

- Player opens trade UI and selects an AI player.
- TradeUIController allows selection of assets from both parties.
- Player submits the offer.
- TradeOrchestrationService receives the offer and passes it to the AIService.
- AIService invokes the AIBehaviorTreeExecutor to evaluate the trade based on the AI's difficulty and strategic goals.
- The AI's decision (accept/decline) is returned.
- If accepted, the RuleEngine executes the asset transfer; otherwise, the user is notified of the decline.

## 1.11 Triggers

- User clicks 'Propose Trade' during their turn.

## 1.12 Outcomes

- The trade is accepted, and assets are exchanged.
- The trade is declined by the AI.

## 1.13 Business Rules

- The trade interface must clearly show assets from both sides (REQ-1-076).
- AI trade evaluation logic depends on its configured difficulty level (REQ-1-064).

## 1.14 Error Scenarios

- Player attempts to trade an asset they don't own.

## 1.15 Integration Points

*No items available*

# 2.0 Details

## 2.1 Diagram Id

SEQ-UJ-052

## 2.2 Name

Player Manages Properties via Dedicated Interface

## 2.3 Description

This sequence details the end-to-end user journey of the human player accessing the property management screen, selecting a property, and initiating a 'Build House' action. It covers the UI interaction, application service orchestration, business rule validation, game state mutation, and the final UI refresh via an event-driven mechanism, ensuring a clear separation of concerns between layers.

## 2.4 Participants

### 2.4.1 Actor

#### 2.4.1.1 Repository Id

HumanPlayer

#### 2.4.1.2 Display Name

Human Player

#### 2.4.1.3 Type

🔹 Actor

#### 2.4.1.4 Technology

User

#### 2.4.1.5 Order

1

#### 2.4.1.6 Style

| Property | Value |
|----------|-------|
| Shape | actor |
| Color | #E6E6E6 |
| Stereotype | User |

### 2.4.2.0 UI Controller

#### 2.4.2.1 Repository Id

presentation_layer

#### 2.4.2.2 Display Name

PropertyManagementUIController

#### 2.4.2.3 Type

🔹 UI Controller

#### 2.4.2.4 Technology

Unity Engine, C#

#### 2.4.2.5 Order

2

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #B4F8C8 |
| Stereotype | Presentation |

### 2.4.3.0 Application Service

#### 2.4.3.1 Repository Id

app_services_layer

#### 2.4.3.2 Display Name

PropertyActionService

#### 2.4.3.3 Type

🔹 Application Service

#### 2.4.3.4 Technology

.NET 8, C#

#### 2.4.3.5 Order

3

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #A0E7E5 |
| Stereotype | Application |

### 2.4.4.0 Domain Service

#### 2.4.4.1 Repository Id

business_logic_layer

#### 2.4.4.2 Display Name

RuleEngine

#### 2.4.4.3 Type

🔹 Domain Service

#### 2.4.4.4 Technology

.NET 8, C#

#### 2.4.4.5 Order

4

#### 2.4.4.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #FFAEBC |
| Stereotype | Domain Logic |

### 2.4.5.0 Domain Entity

#### 2.4.5.1 Repository Id

business_logic_layer

#### 2.4.5.2 Display Name

GameState

#### 2.4.5.3 Type

🔹 Domain Entity

#### 2.4.5.4 Technology

.NET 8, C#

#### 2.4.5.5 Order

5

#### 2.4.5.6 Style

| Property | Value |
|----------|-------|
| Shape | database |
| Color | #FFAEBC |
| Stereotype | In-Memory State |

## 2.5.0.0 Interactions

### 2.5.1.0 User Input

#### 2.5.1.1 Source Id

HumanPlayer

#### 2.5.1.2 Target Id

presentation_layer

#### 2.5.1.3 Message

1. Clicks 'Manage Properties' button on HUD during Pre-Roll Phase.

#### 2.5.1.4 Sequence Number

1

#### 2.5.1.5 Type

🔹 User Input

#### 2.5.1.6 Is Synchronous

✅ Yes

#### 2.5.1.7 Return Message



#### 2.5.1.8 Has Return

❌ No

#### 2.5.1.9 Is Activation

✅ Yes

#### 2.5.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | UI Event |
| Method | OnClick() |
| Parameters | UIEventArgs |
| Authentication | N/A |
| Error Handling | Button is only enabled during the correct game pha... |
| Performance | UI response must be immediate (<100ms). |

### 2.5.2.0 Data Request

#### 2.5.2.1 Source Id

presentation_layer

#### 2.5.2.2 Target Id

business_logic_layer

#### 2.5.2.3 Message

2. Fetches current player and board state data for rendering.

#### 2.5.2.4 Sequence Number

2

#### 2.5.2.5 Type

🔹 Data Request

#### 2.5.2.6 Is Synchronous

✅ Yes

#### 2.5.2.7 Return Message

3. Returns PlayerAssetDataDTO.

#### 2.5.2.8 Has Return

✅ Yes

#### 2.5.2.9 Is Activation

❌ No

#### 2.5.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Method Call |
| Method | GameSessionService.GetPlayerAssetData() |
| Parameters | PlayerId |
| Authentication | N/A |
| Error Handling | Handles cases where GameState is not yet initializ... |
| Performance | Data fetch must be < 50ms to ensure fast UI load. |

### 2.5.3.0 UI Render

#### 2.5.3.1 Source Id

presentation_layer

#### 2.5.3.2 Target Id

presentation_layer

#### 2.5.3.3 Message

4. Renders Property Management view with owned properties, cash, and dynamically enabled/disabled action buttons based on game rules.

#### 2.5.3.4 Sequence Number

4

#### 2.5.3.5 Type

🔹 UI Render

#### 2.5.3.6 Is Synchronous

✅ Yes

#### 2.5.3.7 Return Message



#### 2.5.3.8 Has Return

❌ No

#### 2.5.3.9 Is Activation

❌ No

#### 2.5.3.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Internal Logic |
| Method | RenderView(PlayerAssetDataDTO) |
| Parameters | DTO containing lists of properties, cash balance, ... |
| Authentication | N/A |
| Error Handling | Gracefully handles having no properties to display... |
| Performance | UI must load in <500ms and maintain 60 FPS scrolli... |

### 2.5.4.0 User Input

#### 2.5.4.1 Source Id

HumanPlayer

#### 2.5.4.2 Target Id

presentation_layer

#### 2.5.4.3 Message

5. Selects a property and clicks 'Build House' button.

#### 2.5.4.4 Sequence Number

5

#### 2.5.4.5 Type

🔹 User Input

#### 2.5.4.6 Is Synchronous

✅ Yes

#### 2.5.4.7 Return Message



#### 2.5.4.8 Has Return

❌ No

#### 2.5.4.9 Is Activation

❌ No

#### 2.5.4.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | UI Event |
| Method | OnClickBuildHouse() |
| Parameters | PropertyId |
| Authentication | N/A |
| Error Handling | Button is disabled if action is invalid based on i... |
| Performance | Immediate feedback. |

### 2.5.5.0 Command

#### 2.5.5.1 Source Id

presentation_layer

#### 2.5.5.2 Target Id

app_services_layer

#### 2.5.5.3 Message

6. Submits build request.

#### 2.5.5.4 Sequence Number

6

#### 2.5.5.5 Type

🔹 Command

#### 2.5.5.6 Is Synchronous

✅ Yes

#### 2.5.5.7 Return Message

13. Returns ActionResult { IsSuccess: true }.

#### 2.5.5.8 Has Return

✅ Yes

#### 2.5.5.9 Is Activation

✅ Yes

#### 2.5.5.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Method Call |
| Method | BuildHouseAsync(BuildRequestDTO) |
| Parameters | BuildRequestDTO { PlayerId, PropertyId } |
| Authentication | N/A |
| Error Handling | Handles potential exceptions from lower layers. |
| Performance | Should complete within a single frame (<16ms). |

### 2.5.6.0 Validation Request

#### 2.5.6.1 Source Id

app_services_layer

#### 2.5.6.2 Target Id

business_logic_layer

#### 2.5.6.3 Message

7. Requests validation of the build action from the Rule Engine.

#### 2.5.6.4 Sequence Number

7

#### 2.5.6.5 Type

🔹 Validation Request

#### 2.5.6.6 Is Synchronous

✅ Yes

#### 2.5.6.7 Return Message

8. Returns RuleValidationResult { IsValid: true }.

#### 2.5.6.8 Has Return

✅ Yes

#### 2.5.6.9 Is Activation

✅ Yes

#### 2.5.6.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Method Call |
| Method | CanBuildHouse(GameState, PlayerId, PropertyId) |
| Parameters | Current GameState object and action parameters. |
| Authentication | N/A |
| Error Handling | Returns a detailed reason for failure (e.g., Insuf... |
| Performance | Validation must be highly performant, executing in... |

#### 2.5.6.11 Nested Interactions

##### 2.5.6.11.1 Validation Response

###### 2.5.6.11.1.1 Source Id

app_services_layer

###### 2.5.6.11.1.2 Target Id

business_logic_layer

###### 2.5.6.11.1.3 Message

7a. [ALT: Validation Fails] RuleEngine returns RuleValidationResult { IsValid: false, Reason: UnevenBuilding }.

###### 2.5.6.11.1.4 Sequence Number

7

###### 2.5.6.11.1.5 Type

🔹 Validation Response

###### 2.5.6.11.1.6 Is Synchronous

✅ Yes

###### 2.5.6.11.1.7 Return Message



###### 2.5.6.11.1.8 Has Return

❌ No

###### 2.5.6.11.1.9 Is Activation

❌ No

###### 2.5.6.11.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Method Call |
| Method |  |
| Parameters | RuleValidationResult object. |
| Authentication | N/A |
| Error Handling | Service layer receives the specific failure reason... |
| Performance |  |

##### 2.5.6.11.2.0 Command Response

###### 2.5.6.11.2.1 Source Id

app_services_layer

###### 2.5.6.11.2.2 Target Id

presentation_layer

###### 2.5.6.11.2.3 Message

7b. [ALT] Returns ActionResult { IsSuccess: false, Error: 'Building must be even' }.

###### 2.5.6.11.2.4 Sequence Number

7

###### 2.5.6.11.2.5 Type

🔹 Command Response

###### 2.5.6.11.2.6 Is Synchronous

✅ Yes

###### 2.5.6.11.2.7 Return Message



###### 2.5.6.11.2.8 Has Return

❌ No

###### 2.5.6.11.2.9 Is Activation

❌ No

###### 2.5.6.11.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Method Call |
| Method |  |
| Parameters | ActionResult object |
| Authentication | N/A |
| Error Handling | UI layer receives the error and displays a non-int... |
| Performance |  |

### 2.5.7.0.0.0 State Mutation

#### 2.5.7.1.0.0 Source Id

app_services_layer

#### 2.5.7.2.0.0 Target Id

business_logic_layer

#### 2.5.7.3.0.0 Message

9. Mutates GameState: debits player cash, increments property house count, decrements bank house supply.

#### 2.5.7.4.0.0 Sequence Number

9

#### 2.5.7.5.0.0 Type

🔹 State Mutation

#### 2.5.7.6.0.0 Is Synchronous

✅ Yes

#### 2.5.7.7.0.0 Return Message



#### 2.5.7.8.0.0 Has Return

❌ No

#### 2.5.7.9.0.0 Is Activation

✅ Yes

#### 2.5.7.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Object Modification |
| Method | GameState.Update(stateChanges) |
| Parameters | A transactional object detailing the state changes... |
| Authentication | N/A |
| Error Handling | Operations must be atomic. If any part fails (e.g.... |
| Performance | State update must be near-instantaneous. |

### 2.5.8.0.0.0 Event Publication

#### 2.5.8.1.0.0 Source Id

app_services_layer

#### 2.5.8.2.0.0 Target Id

app_services_layer

#### 2.5.8.3.0.0 Message

10. Publishes `GameStateUpdated` event to in-process event bus.

#### 2.5.8.4.0.0 Sequence Number

10

#### 2.5.8.5.0.0 Type

🔹 Event Publication

#### 2.5.8.6.0.0 Is Synchronous

❌ No

#### 2.5.8.7.0.0 Return Message



#### 2.5.8.8.0.0 Has Return

❌ No

#### 2.5.8.9.0.0 Is Activation

❌ No

#### 2.5.8.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Pub/Sub (Mediator) |
| Method | EventBus.Publish(new GameStateUpdatedEvent()) |
| Parameters | GameStateUpdatedEvent payload (can include specifi... |
| Authentication | N/A |
| Error Handling | Event bus handles subscriber exceptions to prevent... |
| Performance | Fire-and-forget, minimal overhead. |

### 2.5.9.0.0.0 Event Subscription

#### 2.5.9.1.0.0 Source Id

presentation_layer

#### 2.5.9.2.0.0 Target Id

presentation_layer

#### 2.5.9.3.0.0 Message

11. [Subscriber] Receives `GameStateUpdated` event.

#### 2.5.9.4.0.0 Sequence Number

11

#### 2.5.9.5.0.0 Type

🔹 Event Subscription

#### 2.5.9.6.0.0 Is Synchronous

❌ No

#### 2.5.9.7.0.0 Return Message



#### 2.5.9.8.0.0 Has Return

❌ No

#### 2.5.9.9.0.0 Is Activation

❌ No

#### 2.5.9.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Pub/Sub (Mediator) |
| Method | OnGameStateUpdated(event) |
| Parameters | GameStateUpdatedEvent |
| Authentication | N/A |
| Error Handling | Gracefully handles events when the UI is not visib... |
| Performance | Handler logic must be lightweight. |

### 2.5.10.0.0.0 UI Refresh

#### 2.5.10.1.0.0 Source Id

presentation_layer

#### 2.5.10.2.0.0 Target Id

presentation_layer

#### 2.5.10.3.0.0 Message

12. Re-fetches state and re-renders view to reflect changes (updated cash, new house icon, updated button states).

#### 2.5.10.4.0.0 Sequence Number

12

#### 2.5.10.5.0.0 Type

🔹 UI Refresh

#### 2.5.10.6.0.0 Is Synchronous

✅ Yes

#### 2.5.10.7.0.0 Return Message



#### 2.5.10.8.0.0 Has Return

❌ No

#### 2.5.10.9.0.0 Is Activation

❌ No

#### 2.5.10.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Internal Logic |
| Method | RefreshView() |
| Parameters | N/A |
| Authentication | N/A |
| Error Handling | Ensures UI is always consistent with the authorita... |
| Performance | Refresh must be efficient to avoid frame drops. |

## 2.6.0.0.0.0 Notes

### 2.6.1.0.0.0 Content

#### 2.6.1.1.0.0 Content

The initial enabling/disabling of buttons in step 4 is a client-side optimization to provide immediate feedback, but the authoritative validation always occurs in the RuleEngine (step 7) to ensure game integrity.

#### 2.6.1.2.0.0 Position

top-right

#### 2.6.1.3.0.0 Participant Id

*Not specified*

#### 2.6.1.4.0.0 Sequence Number

4

### 2.6.2.0.0.0 Content

#### 2.6.2.1.0.0 Content

Using an event-driven pattern (steps 10-12) decouples the application service from the presentation layer. The service doesn't need to know which UI components care about the state change, enhancing maintainability.

#### 2.6.2.2.0.0 Position

bottom-left

#### 2.6.2.3.0.0 Participant Id

*Not specified*

#### 2.6.2.4.0.0 Sequence Number

10

## 2.7.0.0.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | N/A for this feature. As an offline, single-player... |
| Performance Targets | The Property Management screen must load in under ... |
| Error Handling Strategy | Client-side validation (disabling buttons) provide... |
| Testing Considerations | Requires a suite of pre-configured game state file... |
| Monitoring Requirements | Log all validation failures at the INFO level to h... |
| Deployment Considerations | The UI layout and components must be implemented u... |

