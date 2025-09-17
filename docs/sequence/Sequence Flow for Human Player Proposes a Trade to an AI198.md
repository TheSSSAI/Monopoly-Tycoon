# 1 Overview

## 1.1 Diagram Id

SEQ-UJ-003

## 1.2 Name

Human Player Proposes a Trade to an AI

## 1.3 Description

During their turn, the human player uses the dedicated trading UI to construct and propose a trade offer (properties, cash, cards) to a specific AI opponent. The AI evaluates the offer based on its difficulty settings and responds.

## 1.4 Type

🔹 UserJourney

## 1.5 Purpose

To allow players to engage in strategic negotiations to acquire monopolies, fulfilling requirements REQ-1-059 and REQ-1-060.

## 1.6 Complexity

High

## 1.7 Priority

🔴 High

## 1.8 Frequency

OnDemand

## 1.9 Participants

- REPO-PRES-001
- REPO-AS-005
- REPO-DM-001

## 1.10 Key Interactions

- User opens Trading UI and selects an AI opponent.
- User constructs an offer by selecting assets from both sides.
- Presentation Layer sends the trade proposal to Application Services (TradeOrchestrationService).
- The service forwards the proposal to the AI's logic in the Domain Layer for evaluation.
- AIBehaviorTreeExecutor analyzes the trade's value and strategic impact.
- AI's decision (Accept/Decline) is returned.
- If accepted, the TradeOrchestrationService calls the RuleEngine to atomically swap the assets.
- Presentation Layer is updated with the outcome.

## 1.11 Triggers

- User clicks the 'Propose Trade' button in the Trading UI.

## 1.12 Outcomes

- If the trade is accepted, assets are exchanged between the human and AI player, and the GameState is updated.
- If declined, the game state remains unchanged.
- The user is notified of the outcome.

## 1.13 Business Rules

- Trades can only be initiated during the human player's turn (REQ-1-059).
- AI evaluation of trades must be based on its configured difficulty (REQ-1-062).

## 1.14 Error Scenarios

- Attempting to trade a developed property without first selling buildings.

## 1.15 Integration Points

*No items available*

# 2.0 Details

## 2.1 Diagram Id

SEQ-UJ-003

## 2.2 Name

Implementation: Propose Trade to AI Opponent

## 2.3 Description

Detailed technical sequence for a human player initiating, constructing, and proposing a trade to an AI opponent via the UI. This diagram covers UI state management, application service orchestration for validating the proposal, domain layer AI evaluation, and final atomic execution of the asset exchange in the game state. It directly addresses REQ-1-059 and REQ-1-060.

## 2.4 Participants

### 2.4.1 Actor

#### 2.4.1.1 Repository Id

actor-user

#### 2.4.1.2 Display Name

Human Player

#### 2.4.1.3 Type

🔹 Actor

#### 2.4.1.4 Technology

N/A

#### 2.4.1.5 Order

1

#### 2.4.1.6 Style

| Property | Value |
|----------|-------|
| Shape | actor |
| Color | #E6E6FA |
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
| Shape | rectangle |
| Color | #ADD8E6 |
| Stereotype | Unity Controllers & Views |

### 2.4.3.0 Layer

#### 2.4.3.1 Repository Id

REPO-AS-005

#### 2.4.3.2 Display Name

Application Services Layer

#### 2.4.3.3 Type

🔹 Layer

#### 2.4.3.4 Technology

.NET 8, C#

#### 2.4.3.5 Order

3

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | rectangle |
| Color | #90EE90 |
| Stereotype | Orchestration Services |

### 2.4.4.0 Layer

#### 2.4.4.1 Repository Id

REPO-DM-001

#### 2.4.4.2 Display Name

Business Logic (Domain) Layer

#### 2.4.4.3 Type

🔹 Layer

#### 2.4.4.4 Technology

.NET 8, C#

#### 2.4.4.5 Order

4

#### 2.4.4.6 Style

| Property | Value |
|----------|-------|
| Shape | rectangle |
| Color | #FFB6C1 |
| Stereotype | Rule Engine & AI Logic |

## 2.5.0.0 Interactions

### 2.5.1.0 User Input

#### 2.5.1.1 Source Id

actor-user

#### 2.5.1.2 Target Id

REPO-PRES-001

#### 2.5.1.3 Message

Clicks 'Trade' button on main HUD during pre-roll phase.

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
| Authentication | None |
| Error Handling | None |
| Performance | UI response time < 50ms |

#### 2.5.1.11 Nested Interactions

*No items available*

### 2.5.2.0 Data Request

#### 2.5.2.1 Source Id

REPO-PRES-001

#### 2.5.2.2 Target Id

REPO-AS-005

#### 2.5.2.3 Message

Requests data needed to populate the trading interface.

#### 2.5.2.4 Sequence Number

2

#### 2.5.2.5 Type

🔹 Data Request

#### 2.5.2.6 Is Synchronous

✅ Yes

#### 2.5.2.7 Return Message

Returns TradeUIDataDto with lists of tradable assets for all players.

#### 2.5.2.8 Has Return

✅ Yes

#### 2.5.2.9 Is Activation

✅ Yes

#### 2.5.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-memory method call |
| Method | TradeOrchestrationService.GetTradeUIDataAsync() |
| Parameters | None |
| Authentication | None |
| Error Handling | Handles GameStateNotFoundException. |
| Performance | Latency < 20ms |

#### 2.5.2.11 Nested Interactions

*No items available*

### 2.5.3.0 User Interaction

#### 2.5.3.1 Source Id

actor-user

#### 2.5.3.2 Target Id

REPO-PRES-001

#### 2.5.3.3 Message

Selects AI opponent and constructs the trade offer by selecting properties, cash, and cards from both panels.

#### 2.5.3.4 Sequence Number

3

#### 2.5.3.5 Type

🔹 User Interaction

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
| Protocol | UI Event |
| Method | OnAssetSelected(assetId, panel) |
| Parameters | Asset selection events |
| Authentication | None |
| Error Handling | UI performs real-time client-side validation (e.g.... |
| Performance | Instantaneous visual feedback |

#### 2.5.3.11 Nested Interactions

*No items available*

### 2.5.4.0 User Input

#### 2.5.4.1 Source Id

actor-user

#### 2.5.4.2 Target Id

REPO-PRES-001

#### 2.5.4.3 Message

Clicks 'Propose Trade' button.

#### 2.5.4.4 Sequence Number

4

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
| Method | OnClick() -> TradeUIController.OnProposeTrade() |
| Parameters | UIEventArgs |
| Authentication | None |
| Error Handling | Button is disabled until a valid offer is construc... |
| Performance | UI response time < 50ms |

#### 2.5.4.11 Nested Interactions

*No items available*

### 2.5.5.0 Service Call

#### 2.5.5.1 Source Id

REPO-PRES-001

#### 2.5.5.2 Target Id

REPO-AS-005

#### 2.5.5.3 Message

Submits the trade proposal for processing.

#### 2.5.5.4 Sequence Number

5

#### 2.5.5.5 Type

🔹 Service Call

#### 2.5.5.6 Is Synchronous

✅ Yes

#### 2.5.5.7 Return Message

Returns TradeProposalResultDto indicating outcome (Accepted, Declined, Invalid).

#### 2.5.5.8 Has Return

✅ Yes

#### 2.5.5.9 Is Activation

❌ No

#### 2.5.5.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-memory method call |
| Method | TradeOrchestrationService.ProposeTradeAsync(TradeO... |
| Parameters | TradeOfferDto { OffererId, OffereeId, OfferedAsset... |
| Authentication | None |
| Error Handling | Handles exceptions from lower layers and translate... |
| Performance | Total processing time should be < 2 seconds to avo... |

#### 2.5.5.11 Nested Interactions

##### 2.5.5.11.1 Validation

###### 2.5.5.11.1.1 Source Id

REPO-AS-005

###### 2.5.5.11.1.2 Target Id

REPO-DM-001

###### 2.5.5.11.1.3 Message

Validates the trade against core business rules.

###### 2.5.5.11.1.4 Sequence Number

6

###### 2.5.5.11.1.5 Type

🔹 Validation

###### 2.5.5.11.1.6 Is Synchronous

✅ Yes

###### 2.5.5.11.1.7 Return Message

Returns true if valid.

###### 2.5.5.11.1.8 Has Return

✅ Yes

###### 2.5.5.11.1.9 Is Activation

✅ Yes

###### 2.5.5.11.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-memory method call |
| Method | RuleEngine.IsTradeValid(TradeOffer offer) |
| Parameters | Domain model `TradeOffer` |
| Authentication | None |
| Error Handling | Throws BusinessRuleValidationException if, e.g., a... |
| Performance | Latency < 5ms |

###### 2.5.5.11.1.11 Nested Interactions

*No items available*

##### 2.5.5.11.2.0 Logic Execution

###### 2.5.5.11.2.1 Source Id

REPO-AS-005

###### 2.5.5.11.2.2 Target Id

REPO-DM-001

###### 2.5.5.11.2.3 Message

Forwards the validated offer to the AI for evaluation.

###### 2.5.5.11.2.4 Sequence Number

7

###### 2.5.5.11.2.5 Type

🔹 Logic Execution

###### 2.5.5.11.2.6 Is Synchronous

✅ Yes

###### 2.5.5.11.2.7 Return Message

Returns TradeDecision enum (Accepted/Declined).

###### 2.5.5.11.2.8 Has Return

✅ Yes

###### 2.5.5.11.2.9 Is Activation

❌ No

###### 2.5.5.11.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-memory method call |
| Method | AIBehaviorTreeExecutor.EvaluateTradeOffer(TradeOff... |
| Parameters | Domain model `TradeOffer` |
| Authentication | None |
| Error Handling | Logs evaluation details at DEBUG level. Has an int... |
| Performance | Latency depends on AI difficulty, target < 1500ms. |

###### 2.5.5.11.2.11 Nested Interactions

*No items available*

##### 2.5.5.11.3.0 State Update

###### 2.5.5.11.3.1 Source Id

REPO-AS-005

###### 2.5.5.11.3.2 Target Id

REPO-DM-001

###### 2.5.5.11.3.3 Message

[Conditional: IF Accepted] Executes the asset exchange.

###### 2.5.5.11.3.4 Sequence Number

8

###### 2.5.5.11.3.5 Type

🔹 State Update

###### 2.5.5.11.3.6 Is Synchronous

✅ Yes

###### 2.5.5.11.3.7 Return Message

Returns void on success.

###### 2.5.5.11.3.8 Has Return

✅ Yes

###### 2.5.5.11.3.9 Is Activation

❌ No

###### 2.5.5.11.3.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-memory method call |
| Method | RuleEngine.ExecuteTrade(TradeOffer offer) |
| Parameters | Domain model `TradeOffer` |
| Authentication | None |
| Error Handling | Method must be atomic. It modifies the `GameState`... |
| Performance | Latency < 10ms |

###### 2.5.5.11.3.11 Nested Interactions

*No items available*

### 2.5.6.0.0.0 UI Update

#### 2.5.6.1.0.0 Source Id

REPO-PRES-001

#### 2.5.6.2.0.0 Target Id

REPO-PRES-001

#### 2.5.6.3.0.0 Message

Displays outcome notification ('Trade Accepted' or 'Trade Declined/Invalid').

#### 2.5.6.4.0.0 Sequence Number

9

#### 2.5.6.5.0.0 Type

🔹 UI Update

#### 2.5.6.6.0.0 Is Synchronous

❌ No

#### 2.5.6.7.0.0 Return Message



#### 2.5.6.8.0.0 Has Return

❌ No

#### 2.5.6.9.0.0 Is Activation

❌ No

#### 2.5.6.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Internal UI call |
| Method | NotificationPresenter.Show(message, duration) |
| Parameters | string message, float duration, NotificationType t... |
| Authentication | None |
| Error Handling | None |
| Performance | Instantaneous |

#### 2.5.6.11.0.0 Nested Interactions

*No items available*

### 2.5.7.0.0.0 UI Update

#### 2.5.7.1.0.0 Source Id

REPO-PRES-001

#### 2.5.7.2.0.0 Target Id

REPO-PRES-001

#### 2.5.7.3.0.0 Message

[Conditional: IF Accepted] Refreshes UI elements (HUD, Property Management Screen) to reflect new asset ownership and cash balances.

#### 2.5.7.4.0.0 Sequence Number

10

#### 2.5.7.5.0.0 Type

🔹 UI Update

#### 2.5.7.6.0.0 Is Synchronous

❌ No

#### 2.5.7.7.0.0 Return Message



#### 2.5.7.8.0.0 Has Return

❌ No

#### 2.5.7.9.0.0 Is Activation

❌ No

#### 2.5.7.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Internal Event Bus (Pub/Sub) |
| Method | EventBus.Publish(new GameStateUpdatedEvent()) |
| Parameters | Event object carrying details of what changed. |
| Authentication | None |
| Error Handling | Subscribers (e.g., HUDController) handle their own... |
| Performance | Decoupled update, handled on next UI frame. |

#### 2.5.7.11.0.0 Nested Interactions

*No items available*

### 2.5.8.0.0.0 UI Feedback

#### 2.5.8.1.0.0 Source Id

REPO-PRES-001

#### 2.5.8.2.0.0 Target Id

actor-user

#### 2.5.8.3.0.0 Message

Closes trade UI and returns control to the player for their turn.

#### 2.5.8.4.0.0 Sequence Number

11

#### 2.5.8.5.0.0 Type

🔹 UI Feedback

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
| Protocol | UI Rendering |
| Method | ViewManager.CloseView('TradeUI') |
| Parameters | None |
| Authentication | None |
| Error Handling | None |
| Performance | Instantaneous |

#### 2.5.8.11.0.0 Nested Interactions

*No items available*

## 2.6.0.0.0.0 Notes

### 2.6.1.0.0.0 Content

#### 2.6.1.1.0.0 Content

The AI evaluation logic within the AIBehaviorTreeExecutor is the most complex part of this sequence. It must consider the immediate cash value, strategic value of completing monopolies (for self or opponent), and blocking potential opponent monopolies. Its parameters are tuned per difficulty level as per REQ-1-063.

#### 2.6.1.2.0.0 Position

top-right

#### 2.6.1.3.0.0 Participant Id

REPO-DM-001

#### 2.6.1.4.0.0 Sequence Number

7

### 2.6.2.0.0.0 Content

#### 2.6.2.1.0.0 Content

The RuleEngine.ExecuteTrade method must be atomic. If any part of the asset transfer fails, the entire transaction must be rolled back to ensure the GameState remains consistent. This is managed by modifying a single in-memory GameState object.

#### 2.6.2.2.0.0 Position

bottom-right

#### 2.6.2.3.0.0 Participant Id

REPO-DM-001

#### 2.6.2.4.0.0 Sequence Number

8

## 2.7.0.0.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | Primary security concern is data validation. The A... |
| Performance Targets | The end-to-end user experience from clicking 'Prop... |
| Error Handling Strategy | The `TradeOrchestrationService` acts as a central ... |
| Testing Considerations | Integration testing is critical. Create a suite of... |
| Monitoring Requirements | Log all trade proposals at the INFO level, includi... |
| Deployment Considerations | The AI behavior parameters that influence trade de... |

