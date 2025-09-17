# 1 Overview

## 1.1 Diagram Id

SEQ-UJ-014

## 1.2 Name

Player Responds to AI Trade Offer

## 1.3 Description

An AI opponent proposes a trade to the human player. A modal dialog appears, presenting the terms of the trade and allowing the player to accept, decline, or propose a counter-offer.

## 1.4 Type

🔹 UserJourney

## 1.5 Purpose

To handle AI-initiated interactions, making the AI opponents feel more dynamic and engaging.

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

- AIService, via AIBehaviorTreeExecutor, generates a trade proposal.
- TradeOrchestrationService triggers an event for the PresentationLayer.
- A modal dialog is displayed to the human player with the trade details.
- Player clicks 'Accept', 'Decline', or 'Counter-Offer'.
- The response is sent to the TradeOrchestrationService.
- If accepted, the RuleEngine executes the asset swap. If a counter-offer is made, it is sent back to the AI for evaluation.

## 1.11 Triggers

- An AI player decides to propose a trade during its turn.

## 1.12 Outcomes

- The player accepts the trade, and assets are swapped.
- The player declines the trade.
- The player enters a counter-offer negotiation loop.

## 1.13 Business Rules

- The UI must present options to Accept, Decline, or Propose Counter-Offer (REQ-1-059).

## 1.14 Error Scenarios

*No items available*

## 1.15 Integration Points

*No items available*

# 2.0 Details

## 2.1 Diagram Id

SEQ-IMPLEMENT-UJ-014

## 2.2 Name

Implementation: Human Player Responds to AI-Initiated Trade Offer

## 2.3 Description

This sequence details the end-to-end technical flow for an AI-initiated trade proposal. The process begins with the AI's decision-making logic, flows through the application services to present a modal dialog to the user, captures the user's response, and executes the state change in the core business logic layer, finally updating the UI to reflect the new game state.

## 2.4 Participants

### 2.4.1 Engine

#### 2.4.1.1 Repository Id

ai-behavior-tree-executor-002

#### 2.4.1.2 Display Name

AIBehaviorTreeExecutor

#### 2.4.1.3 Type

🔹 Engine

#### 2.4.1.4 Technology

.NET 8, Behavior Tree

#### 2.4.1.5 Order

1

#### 2.4.1.6 Style

| Property | Value |
|----------|-------|
| Shape | actor |
| Color | #C44D58 |
| Stereotype | BusinessLogic |

### 2.4.2.0 Service

#### 2.4.2.1 Repository Id

trade-orchestration-service-054

#### 2.4.2.2 Display Name

TradeOrchestrationService

#### 2.4.2.3 Type

🔹 Service

#### 2.4.2.4 Technology

.NET 8, C#

#### 2.4.2.5 Order

2

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #4D82C4 |
| Stereotype | ApplicationService |

### 2.4.3.0 Broker

#### 2.4.3.1 Repository Id

event-bus-001

#### 2.4.3.2 Display Name

InProcessEventBus

#### 2.4.3.3 Type

🔹 Broker

#### 2.4.3.4 Technology

Mediator Pattern (e.g., MediatR)

#### 2.4.3.5 Order

3

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #D9A44E |
| Stereotype | CrossCutting |

### 2.4.4.0 Controller

#### 2.4.4.1 Repository Id

trade-ui-controller-207

#### 2.4.4.2 Display Name

TradeUIController

#### 2.4.4.3 Type

🔹 Controller

#### 2.4.4.4 Technology

Unity Engine, C#

#### 2.4.4.5 Order

4

#### 2.4.4.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #4EC482 |
| Stereotype | Presentation |

### 2.4.5.0 Controller

#### 2.4.5.1 Repository Id

input-handler-206

#### 2.4.5.2 Display Name

InputHandler

#### 2.4.5.3 Type

🔹 Controller

#### 2.4.5.4 Technology

Unity Engine, C#

#### 2.4.5.5 Order

5

#### 2.4.5.6 Style

| Property | Value |
|----------|-------|
| Shape | actor |
| Color | #4EC482 |
| Stereotype | Presentation (User) |

### 2.4.6.0 Engine

#### 2.4.6.1 Repository Id

game-engine-001

#### 2.4.6.2 Display Name

RuleEngine & GameState

#### 2.4.6.3 Type

🔹 Engine

#### 2.4.6.4 Technology

.NET 8, C#

#### 2.4.6.5 Order

6

#### 2.4.6.6 Style

| Property | Value |
|----------|-------|
| Shape | database |
| Color | #C44D58 |
| Stereotype | BusinessLogic |

### 2.4.7.0 Controller

#### 2.4.7.1 Repository Id

hud-controller-203

#### 2.4.7.2 Display Name

HUDController

#### 2.4.7.3 Type

🔹 Controller

#### 2.4.7.4 Technology

Unity Engine, C#

#### 2.4.7.5 Order

7

#### 2.4.7.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #4EC482 |
| Stereotype | Presentation |

## 2.5.0.0 Interactions

### 2.5.1.0 Request

#### 2.5.1.1 Source Id

ai-behavior-tree-executor-002

#### 2.5.1.2 Target Id

trade-orchestration-service-054

#### 2.5.1.3 Message

1. ProposeAITrade(proposal)

#### 2.5.1.4 Sequence Number

1

#### 2.5.1.5 Type

🔹 Request

#### 2.5.1.6 Is Synchronous

✅ Yes

#### 2.5.1.7 Return Message

void

#### 2.5.1.8 Has Return

✅ Yes

#### 2.5.1.9 Is Activation

✅ Yes

#### 2.5.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Call |
| Method | ProposeAITrade(TradeProposal proposal) |
| Parameters | TradeProposal object containing PlayerIDs, cash am... |
| Authentication | N/A (Offline Application) |
| Error Handling | N/A. Initial proposal generation is assumed to be ... |
| Performance | Must execute within the AI's 'thinking time' budge... |

### 2.5.2.0 Event

#### 2.5.2.1 Source Id

trade-orchestration-service-054

#### 2.5.2.2 Target Id

event-bus-001

#### 2.5.2.3 Message

2. Publish(AITradeOfferReceivedEvent)

#### 2.5.2.4 Sequence Number

2

#### 2.5.2.5 Type

🔹 Event

#### 2.5.2.6 Is Synchronous

❌ No

#### 2.5.2.7 Return Message



#### 2.5.2.8 Has Return

❌ No

#### 2.5.2.9 Is Activation

❌ No

#### 2.5.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Event |
| Method | Publish<T>(T notification) |
| Parameters | AITradeOfferReceivedEvent containing a unique trad... |
| Authentication | N/A |
| Error Handling | The event bus should log any failures from subscri... |
| Performance | Near-instantaneous publication. |

### 2.5.3.0 Notification

#### 2.5.3.1 Source Id

event-bus-001

#### 2.5.3.2 Target Id

trade-ui-controller-207

#### 2.5.3.3 Message

3. [Subscribed] HandleAITradeOffer(event)

#### 2.5.3.4 Sequence Number

3

#### 2.5.3.5 Type

🔹 Notification

#### 2.5.3.6 Is Synchronous

❌ No

#### 2.5.3.7 Return Message



#### 2.5.3.8 Has Return

❌ No

#### 2.5.3.9 Is Activation

✅ Yes

#### 2.5.3.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Event Handler |
| Method | OnAITradeOfferReceived(AITradeOfferReceivedEvent e... |
| Parameters | The event payload. |
| Authentication | N/A |
| Error Handling | Handler must ensure it's on the main UI thread bef... |
| Performance | Handler logic to display UI must be highly optimiz... |

### 2.5.4.0 Internal

#### 2.5.4.1 Source Id

trade-ui-controller-207

#### 2.5.4.2 Target Id

trade-ui-controller-207

#### 2.5.4.3 Message

4. RenderTradeModalDialog(event.Proposal)

#### 2.5.4.4 Sequence Number

4

#### 2.5.4.5 Type

🔹 Internal

#### 2.5.4.6 Is Synchronous

✅ Yes

#### 2.5.4.7 Return Message

void

#### 2.5.4.8 Has Return

✅ Yes

#### 2.5.4.9 Is Activation

❌ No

#### 2.5.4.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Internal Call |
| Method | RenderTradeModalDialog(TradeProposal proposal) |
| Parameters | The proposal data is used to populate UI text and ... |
| Authentication | N/A |
| Error Handling | Gracefully handle missing UI elements; log errors. |
| Performance | Modal dialog must be fully rendered and interactiv... |

### 2.5.5.0 User Interaction

#### 2.5.5.1 Source Id

input-handler-206

#### 2.5.5.2 Target Id

trade-ui-controller-207

#### 2.5.5.3 Message

5. User clicks 'Accept' button

#### 2.5.5.4 Sequence Number

5

#### 2.5.5.5 Type

🔹 User Interaction

#### 2.5.5.6 Is Synchronous

❌ No

#### 2.5.5.7 Return Message



#### 2.5.5.8 Has Return

❌ No

#### 2.5.5.9 Is Activation

❌ No

#### 2.5.5.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Unity UI Event System |
| Method | Button.onClick event triggers OnAcceptButtonClicke... |
| Parameters | None. |
| Authentication | N/A |
| Error Handling | UI should provide visual feedback on click and dis... |
| Performance | UI response to click must be <100ms. |

### 2.5.6.0 Request

#### 2.5.6.1 Source Id

trade-ui-controller-207

#### 2.5.6.2 Target Id

trade-orchestration-service-054

#### 2.5.6.3 Message

6. RespondToAITrade(tradeId, ACCEPTED)

#### 2.5.6.4 Sequence Number

6

#### 2.5.6.5 Type

🔹 Request

#### 2.5.6.6 Is Synchronous

✅ Yes

#### 2.5.6.7 Return Message

bool success

#### 2.5.6.8 Has Return

✅ Yes

#### 2.5.6.9 Is Activation

❌ No

#### 2.5.6.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Call |
| Method | RespondToAITrade(Guid tradeId, TradeResponse respo... |
| Parameters | The unique ID of the trade and an enum value for t... |
| Authentication | N/A |
| Error Handling | The UI should handle a 'false' return by showing a... |
| Performance | Call should return quickly; heavy logic is in subs... |

### 2.5.7.0 Command

#### 2.5.7.1 Source Id

trade-orchestration-service-054

#### 2.5.7.2 Target Id

game-engine-001

#### 2.5.7.3 Message

7. ExecuteTrade(proposal)

#### 2.5.7.4 Sequence Number

7

#### 2.5.7.5 Type

🔹 Command

#### 2.5.7.6 Is Synchronous

✅ Yes

#### 2.5.7.7 Return Message

void

#### 2.5.7.8 Has Return

✅ Yes

#### 2.5.7.9 Is Activation

✅ Yes

#### 2.5.7.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Call |
| Method | ExecuteTrade(TradeProposal proposal) |
| Parameters | The original proposal object. |
| Authentication | N/A |
| Error Handling | Throws `InvalidTradeException` if a player no long... |
| Performance | Validation and state change should complete in < 1... |

### 2.5.8.0 State Change

#### 2.5.8.1 Source Id

game-engine-001

#### 2.5.8.2 Target Id

game-engine-001

#### 2.5.8.3 Message

8. Update PlayerState objects (cash, properties)

#### 2.5.8.4 Sequence Number

8

#### 2.5.8.5 Type

🔹 State Change

#### 2.5.8.6 Is Synchronous

✅ Yes

#### 2.5.8.7 Return Message

void

#### 2.5.8.8 Has Return

✅ Yes

#### 2.5.8.9 Is Activation

❌ No

#### 2.5.8.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Memory Update |
| Method | Internal state modification of PlayerState objects... |
| Parameters | N/A. |
| Authentication | N/A |
| Error Handling | This operation must be atomic. If any part fails, ... |
| Performance | Extremely fast. |

### 2.5.9.0 Event

#### 2.5.9.1 Source Id

trade-orchestration-service-054

#### 2.5.9.2 Target Id

event-bus-001

#### 2.5.9.3 Message

9. Publish(GameStateUpdatedEvent)

#### 2.5.9.4 Sequence Number

9

#### 2.5.9.5 Type

🔹 Event

#### 2.5.9.6 Is Synchronous

❌ No

#### 2.5.9.7 Return Message



#### 2.5.9.8 Has Return

❌ No

#### 2.5.9.9 Is Activation

❌ No

#### 2.5.9.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Event |
| Method | Publish<T>(T notification) |
| Parameters | GameStateUpdatedEvent payload indicating which pla... |
| Authentication | N/A |
| Error Handling | N/A. |
| Performance | Near-instantaneous. |

### 2.5.10.0 Notification

#### 2.5.10.1 Source Id

event-bus-001

#### 2.5.10.2 Target Id

hud-controller-203

#### 2.5.10.3 Message

10. [Subscribed] HandleGameStateUpdate(event)

#### 2.5.10.4 Sequence Number

10

#### 2.5.10.5 Type

🔹 Notification

#### 2.5.10.6 Is Synchronous

❌ No

#### 2.5.10.7 Return Message



#### 2.5.10.8 Has Return

❌ No

#### 2.5.10.9 Is Activation

✅ Yes

#### 2.5.10.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Event Handler |
| Method | OnGameStateUpdated(GameStateUpdatedEvent event) |
| Parameters | The event payload. |
| Authentication | N/A |
| Error Handling | Handler must be resilient to partial data and ensu... |
| Performance | UI refresh logic should not cause frame drops. |

### 2.5.11.0 Internal

#### 2.5.11.1 Source Id

hud-controller-203

#### 2.5.11.2 Target Id

hud-controller-203

#### 2.5.11.3 Message

11. Refresh Player HUDs (cash, property indicators)

#### 2.5.11.4 Sequence Number

11

#### 2.5.11.5 Type

🔹 Internal

#### 2.5.11.6 Is Synchronous

✅ Yes

#### 2.5.11.7 Return Message

void

#### 2.5.11.8 Has Return

✅ Yes

#### 2.5.11.9 Is Activation

❌ No

#### 2.5.11.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Internal Call |
| Method | RefreshUI() |
| Parameters | Uses data from the updated GameState to change tex... |
| Authentication | N/A |
| Error Handling | Log errors if UI elements are not found. |
| Performance | Must complete within a single frame (< 16ms). |

## 2.6.0.0 Notes

### 2.6.1.0 Content

#### 2.6.1.1 Content

The `InProcessEventBus` is critical for decoupling the Application Services Layer from the Presentation Layer. Services publish events without knowing who the subscribers are.

#### 2.6.1.2 Position

top-right

#### 2.6.1.3 Participant Id

event-bus-001

#### 2.6.1.4 Sequence Number

2

### 2.6.2.0 Content

#### 2.6.2.1 Content

The `RuleEngine` acts as the sole authority for mutating the `GameState`. Services request changes, but only the engine can apply them, ensuring all game rules are enforced centrally.

#### 2.6.2.2 Position

bottom-right

#### 2.6.2.3 Participant Id

game-engine-001

#### 2.6.2.4 Sequence Number

7

### 2.6.3.0 Content

#### 2.6.3.1 Content

The flow for 'Decline' is a shorter version of this, ending after step 6. The 'Counter-Offer' flow would involve a new proposal being sent back to the AIBehaviorTreeExecutor for evaluation.

#### 2.6.3.2 Position

bottom-left

#### 2.6.3.3 Participant Id

*Not specified*

#### 2.6.3.4 Sequence Number

11

## 2.7.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | N/A. All interactions are local and offline within... |
| Performance Targets | The trade modal dialog must appear in < 250ms from... |
| Error Handling Strategy | The `RuleEngine` is the primary source of validati... |
| Testing Considerations | Integration tests are critical and must cover all ... |
| Monitoring Requirements | Log the full `TradeProposal` details when generate... |
| Deployment Considerations | N/A for a monolithic client application. |

