# 1 Overview

## 1.1 Diagram Id

SEQ-EP-017

## 1.2 Name

Process AI-to-AI Trade Notification

## 1.3 Description

An AI player successfully completes a trade with another AI. The application logic publishes an event that is consumed by the UI to display a temporary, non-intrusive notification to the human player.

## 1.4 Type

🔹 EventProcessing

## 1.5 Purpose

To keep the human player informed of significant game state changes that occur without their direct involvement, improving game awareness.

## 1.6 Complexity

Medium

## 1.7 Priority

🟡 Medium

## 1.8 Frequency

OnDemand

## 1.9 Participants

- BusinessLogicLayer
- ApplicationServicesLayer
- PresentationLayer

## 1.10 Key Interactions

- RuleEngine finalizes an AI-to-AI trade.
- TradeOrchestrationService publishes a 'TradeCompleted' domain event containing details of the trade.
- A notification handler in the Presentation Layer subscribes to this event.
- Upon receiving the event, the handler formats a message (e.g., 'AI 1 traded Boardwalk to AI 2 for $500').
- The message is displayed as a temporary, non-modal notification on the HUD.

## 1.11 Triggers

- An AI-to-AI trade is accepted and executed.

## 1.12 Outcomes

- The human player is informed of the completed trade without interrupting their experience.

## 1.13 Business Rules

- AI-to-AI trades shall be announced to the human player (REQ-1-059).
- Notifications for informational events should be non-intrusive (REQ-1-073).

## 1.14 Error Scenarios

*No items available*

## 1.15 Integration Points

- In-Process Event Bus / Mediator

# 2.0 Details

## 2.1 Diagram Id

SEQ-EP-017

## 2.2 Name

Implementation: Process AI-to-AI Trade Completion Event for UI Notification

## 2.3 Description

Provides a comprehensive technical specification for the event-driven process that informs the human player of a completed AI-to-AI trade. This sequence is triggered after the Business Logic Layer finalizes a trade, leading to an event publication by the Application Services Layer. A handler in the Presentation Layer consumes this event to display a non-intrusive UI notification, fulfilling REQ-1-059 and REQ-1-073.

## 2.4 Participants

### 2.4.1 Application Service

#### 2.4.1.1 Repository Id

REPO-AS-005

#### 2.4.1.2 Display Name

TradeOrchestrationService

#### 2.4.1.3 Type

🔹 Application Service

#### 2.4.1.4 Technology

.NET 8, C#

#### 2.4.1.5 Order

1

#### 2.4.1.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #FAD7A0 |
| Stereotype | Service |

### 2.4.2.0 Domain Engine

#### 2.4.2.1 Repository Id

REPO-BL-002

#### 2.4.2.2 Display Name

RuleEngine

#### 2.4.2.3 Type

🔹 Domain Engine

#### 2.4.2.4 Technology

.NET 8, C#

#### 2.4.2.5 Order

2

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #A9DFBF |
| Stereotype | Engine |

### 2.4.3.0 Infrastructure Service

#### 2.4.3.1 Repository Id

REPO-IP-EB-010

#### 2.4.3.2 Display Name

InProcessEventBus

#### 2.4.3.3 Type

🔹 Infrastructure Service

#### 2.4.3.4 Technology

MediatR Library

#### 2.4.3.5 Order

3

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #D2B4DE |
| Stereotype | Mediator |

### 2.4.4.0 Presentation Handler

#### 2.4.4.1 Repository Id

REPO-PL-007

#### 2.4.4.2 Display Name

NotificationHandler

#### 2.4.4.3 Type

🔹 Presentation Handler

#### 2.4.4.4 Technology

Unity Engine, C#

#### 2.4.4.5 Order

4

#### 2.4.4.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #A9CCE3 |
| Stereotype | Handler |

## 2.5.0.0 Interactions

### 2.5.1.0 Method Call

#### 2.5.1.1 Source Id

REPO-AS-005

#### 2.5.1.2 Target Id

REPO-BL-002

#### 2.5.1.3 Message

Invoke trade execution for validated AI-to-AI offer.

#### 2.5.1.4 Sequence Number

1

#### 2.5.1.5 Type

🔹 Method Call

#### 2.5.1.6 Is Synchronous

✅ Yes

#### 2.5.1.7 Return Message

Return result of trade execution.

#### 2.5.1.8 Has Return

✅ Yes

#### 2.5.1.9 Is Activation

✅ Yes

#### 2.5.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Call |
| Method | TradeResult ExecuteTrade(TradeOffer offer) |
| Parameters | offer: Contains details of the proposing player, r... |
| Authentication | N/A (Internal trusted call) |
| Error Handling | Exceptions for invalid trades (e.g., insufficient ... |
| Performance | Must complete within one frame (<16ms) to avoid ga... |

### 2.5.2.0 Internal Processing

#### 2.5.2.1 Source Id

REPO-BL-002

#### 2.5.2.2 Target Id

REPO-BL-002

#### 2.5.2.3 Message

Atomically update GameState: transfer cash, properties, and cards between AI players.

#### 2.5.2.4 Sequence Number

2

#### 2.5.2.5 Type

🔹 Internal Processing

#### 2.5.2.6 Is Synchronous

✅ Yes

#### 2.5.2.7 Return Message



#### 2.5.2.8 Has Return

❌ No

#### 2.5.2.9 Is Activation

❌ No

#### 2.5.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | N/A |
| Method | Internal State Modification |
| Parameters | N/A |
| Authentication | N/A |
| Error Handling | All state changes are part of a single logical tra... |
| Performance | High-performance memory operations. |

### 2.5.3.0 Event Publication

#### 2.5.3.1 Source Id

REPO-AS-005

#### 2.5.3.2 Target Id

REPO-IP-EB-010

#### 2.5.3.3 Message

Publish domain event upon successful trade execution.

#### 2.5.3.4 Sequence Number

3

#### 2.5.3.5 Type

🔹 Event Publication

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
| Protocol | In-Process Pub/Sub |
| Method | Task Publish(TradeCompletedEvent notification, Can... |
| Parameters | notification: A 'TradeCompletedEvent' object conta... |
| Authentication | N/A (Internal trusted call) |
| Error Handling | The publish operation itself is fire-and-forget. T... |
| Performance | Publishing should be near-instantaneous (<1ms). Th... |

### 2.5.4.0 Event Dispatch

#### 2.5.4.1 Source Id

REPO-IP-EB-010

#### 2.5.4.2 Target Id

REPO-PL-007

#### 2.5.4.3 Message

Asynchronously dispatch event to subscribed UI handler.

#### 2.5.4.4 Sequence Number

4

#### 2.5.4.5 Type

🔹 Event Dispatch

#### 2.5.4.6 Is Synchronous

❌ No

#### 2.5.4.7 Return Message



#### 2.5.4.8 Has Return

❌ No

#### 2.5.4.9 Is Activation

✅ Yes

#### 2.5.4.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Pub/Sub |
| Method | Task Handle(TradeCompletedEvent notification, Canc... |
| Parameters | notification: The `TradeCompletedEvent` object pub... |
| Authentication | N/A |
| Error Handling | The bus will log any exceptions during dispatch bu... |
| Performance | Dispatch is handled on a background thread pool to... |

### 2.5.5.0 Internal Processing

#### 2.5.5.1 Source Id

REPO-PL-007

#### 2.5.5.2 Target Id

REPO-PL-007

#### 2.5.5.3 Message

Format event data into a human-readable notification string.

#### 2.5.5.4 Sequence Number

5

#### 2.5.5.5 Type

🔹 Internal Processing

#### 2.5.5.6 Is Synchronous

✅ Yes

#### 2.5.5.7 Return Message



#### 2.5.5.8 Has Return

❌ No

#### 2.5.5.9 Is Activation

❌ No

#### 2.5.5.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | N/A |
| Method | string FormatNotification(TradeCompletedEvent evt) |
| Parameters | evt: The received event. |
| Authentication | N/A |
| Error Handling | Logic must handle various asset types (cash, prope... |
| Performance | String formatting must be highly efficient. |

### 2.5.6.0 UI Update

#### 2.5.6.1 Source Id

REPO-PL-007

#### 2.5.6.2 Target Id

REPO-PL-007

#### 2.5.6.3 Message

Invoke HUD manager to display the temporary, non-intrusive notification.

#### 2.5.6.4 Sequence Number

6

#### 2.5.6.5 Type

🔹 UI Update

#### 2.5.6.6 Is Synchronous

❌ No

#### 2.5.6.7 Return Message



#### 2.5.6.8 Has Return

❌ No

#### 2.5.6.9 Is Activation

❌ No

#### 2.5.6.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Unity UI Event |
| Method | DisplayNotification(string message, float duration... |
| Parameters | message: The formatted string. duration: Time in s... |
| Authentication | N/A |
| Error Handling | The UI Manager is responsible for queuing and disp... |
| Performance | The call must be non-blocking. The UI Manager will... |

## 2.6.0.0 Notes

### 2.6.1.0 Content

#### 2.6.1.1 Content

The TradeCompletedEvent schema is critical. It must contain player names, a list of properties/cards exchanged, and the cash amount. Version this event schema for future compatibility, though not a concern for v1.0.

#### 2.6.1.2 Position

top

#### 2.6.1.3 Participant Id

REPO-IP-EB-010

#### 2.6.1.4 Sequence Number

3

### 2.6.2.0 Content

#### 2.6.2.1 Content

The entire notification process from event publication to UI display must be asynchronous and decoupled to ensure that a failure in the UI layer (e.g., a missing font) cannot crash or stall the core game logic.

#### 2.6.2.2 Position

bottom

#### 2.6.2.3 Participant Id

REPO-PL-007

#### 2.6.2.4 Sequence Number

4

## 2.7.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | N/A. This is an entirely offline, in-process commu... |
| Performance Targets | The end-to-end latency from trade completion to th... |
| Error Handling Strategy | The NotificationHandler must implement a root-leve... |
| Testing Considerations | 1. Unit Test: The string formatting logic in the N... |
| Monitoring Requirements | All key steps should be logged at DEBUG level with... |
| Deployment Considerations | N/A for this feature. |

