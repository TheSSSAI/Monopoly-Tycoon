# 1 Overview

## 1.1 Diagram Id

SEQ-FF-003

## 1.2 Name

Player Lands on Owned Property and Pays Rent

## 1.3 Description

A player's turn ends by landing on a property owned by another player. The system automatically calculates the required rent based on various factors (monopoly, development level) and facilitates the payment.

## 1.4 Type

🔹 FeatureFlow

## 1.5 Purpose

To implement the core economic mechanic of rent collection, which is fundamental to the game's objective.

## 1.6 Complexity

Medium

## 1.7 Priority

🚨 Critical

## 1.8 Frequency

OnDemand

## 1.9 Participants

- REPO-AS-005
- REPO-DM-001
- REPO-PRES-001

## 1.10 Key Interactions

- A player's movement phase concludes on an owned, unmortgaged property.
- The Application Services Layer (TurnManagementService) queries the Domain's RuleEngine to calculate the rent due.
- The RuleEngine checks the property's development, monopoly status, and owner to determine the correct rent amount.
- The RuleEngine performs the transaction on the GameState object, debiting the renter and crediting the owner.
- If the renter cannot pay, the bankruptcy flow is triggered.
- The Application Services Layer notifies the Presentation Layer of the transaction.
- The Presentation Layer displays a visual effect and notification (e.g., '-$50') and updates the HUDs for both players.

## 1.11 Triggers

- A player's token lands on another player's property.

## 1.12 Outcomes

- The correct amount of rent is transferred from one player to another.
- Both players' cash balances in the GameState are updated.
- The UI reflects the transaction and updated cash totals.

## 1.13 Business Rules

- Rent is not collected on mortgaged properties (REQ-1-057).
- Rent is doubled on undeveloped properties within a monopoly.
- Rent for Railroads and Utilities depends on the number owned by the player (REQ-1-061).

## 1.14 Error Scenarios

- Player has insufficient cash to pay the full rent, triggering bankruptcy proceedings.

## 1.15 Integration Points

*No items available*

# 2.0 Details

## 2.1 Diagram Id

SEQ-FF-003

## 2.2 Name

Player Lands on Owned Property and Pays Rent

## 2.3 Description

Implementation sequence for the automated calculation and collection of rent when a player lands on an opponent's property. The sequence details the interaction between the Presentation, Application, and Domain layers to enforce rent rules, handle the transaction, and update the UI. It also specifies the trigger condition for the separate bankruptcy flow.

## 2.4 Participants

### 2.4.1 UI/View Layer

#### 2.4.1.1 Repository Id

REPO-PRES-001

#### 2.4.1.2 Display Name

Presentation Layer

#### 2.4.1.3 Type

🔹 UI/View Layer

#### 2.4.1.4 Technology

Unity Engine, C#

#### 2.4.1.5 Order

1

#### 2.4.1.6 Style

| Property | Value |
|----------|-------|
| Shape | actor |
| Color | #4CAF50 |
| Stereotype | Human Player / UI |

### 2.4.2.0 Application Service

#### 2.4.2.1 Repository Id

REPO-AS-005

#### 2.4.2.2 Display Name

TurnManagementService

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
| Stereotype | Orchestrator |

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
| Stereotype | Business Logic |

### 2.4.4.0 Domain Aggregate

#### 2.4.4.1 Repository Id

REPO-DM-001-GS

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
| Stereotype | In-Memory State |

## 2.5.0.0 Interactions

### 2.5.1.0 User Interaction

#### 2.5.1.1 Source Id

REPO-PRES-001

#### 2.5.1.2 Target Id

REPO-AS-005

#### 2.5.1.3 Message

Player movement animation completes. Signals the end of the movement phase.

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

| Property | Value |
|----------|-------|
| Protocol | In-process Method Call |
| Method | ProcessActionPhase() |
| Parameters | None (Service already holds current player context... |
| Authentication | N/A |
| Error Handling | Exceptions are propagated to a global handler. |
| Performance | Must be invoked within the same frame as animation... |

### 2.5.2.0 Command

#### 2.5.2.1 Source Id

REPO-AS-005

#### 2.5.2.2 Target Id

REPO-DM-001

#### 2.5.2.3 Message

Requests the Domain Layer to process the rent transaction for the current player.

#### 2.5.2.4 Sequence Number

2

#### 2.5.2.5 Type

🔹 Command

#### 2.5.2.6 Is Synchronous

✅ Yes

#### 2.5.2.7 Return Message

Returns a result object indicating the outcome (RentPaid, NoRentDue, or BankruptcyRequired).

#### 2.5.2.8 Has Return

✅ Yes

#### 2.5.2.9 Is Activation

✅ Yes

#### 2.5.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-process Method Call |
| Method | Task<TransactionResult> ExecuteRentTransaction(Gam... |
| Parameters | GameState: The authoritative state object. renterP... |
| Authentication | N/A |
| Error Handling | The method is designed not to throw exceptions for... |
| Performance | Execution must be completed in < 5ms to avoid impa... |

### 2.5.3.0 Data Read

#### 2.5.3.1 Source Id

REPO-DM-001

#### 2.5.3.2 Target Id

REPO-DM-001-GS

#### 2.5.3.3 Message

Reads property data, owner status, and development level from the GameState.

#### 2.5.3.4 Sequence Number

3

#### 2.5.3.5 Type

🔹 Data Read

#### 2.5.3.6 Is Synchronous

✅ Yes

#### 2.5.3.7 Return Message

Property, Owner, and Renter state objects.

#### 2.5.3.8 Has Return

✅ Yes

#### 2.5.3.9 Is Activation

✅ Yes

#### 2.5.3.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-memory Access |
| Method | gameState.GetProperty(spaceIndex); gameState.GetPl... |
| Parameters | spaceIndex: Integer board position. playerId: Guid... |
| Authentication | N/A |
| Error Handling | Relies on the GameState object's internal consiste... |
| Performance | Near-instantaneous in-memory object graph traversa... |

### 2.5.4.0 Business Rule Execution

#### 2.5.4.1 Source Id

REPO-DM-001

#### 2.5.4.2 Target Id

REPO-DM-001

#### 2.5.4.3 Message

Calculates rent based on rules: checks for mortgage, monopoly, development level, and property type (e.g., Railroads).

#### 2.5.4.4 Sequence Number

4

#### 2.5.4.5 Type

🔹 Business Rule Execution

#### 2.5.4.6 Is Synchronous

✅ Yes

#### 2.5.4.7 Return Message

Calculated rent amount.

#### 2.5.4.8 Has Return

✅ Yes

#### 2.5.4.9 Is Activation

❌ No

#### 2.5.4.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Internal Method Call |
| Method | private int CalculateRent(Property property, Playe... |
| Parameters | The property and its owner. |
| Authentication | N/A |
| Error Handling | Pure function, no exceptions expected. |
| Performance | Must be highly optimized; core economic calculatio... |

### 2.5.5.0 Data Write

#### 2.5.5.1 Source Id

REPO-DM-001

#### 2.5.5.2 Target Id

REPO-DM-001-GS

#### 2.5.5.3 Message

Applies the transaction: debits renter's cash and credits owner's cash directly within the GameState.

#### 2.5.5.4 Sequence Number

5

#### 2.5.5.5 Type

🔹 Data Write

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
| Protocol | In-memory Mutation |
| Method | renter.DebitCash(amount); owner.CreditCash(amount)... |
| Parameters | amount: The calculated rent value. |
| Authentication | N/A |
| Error Handling | The RuleEngine must pre-validate if the renter has... |
| Performance | Near-instantaneous. |

### 2.5.6.0 Event Notification

#### 2.5.6.1 Source Id

REPO-AS-005

#### 2.5.6.2 Target Id

REPO-PRES-001

#### 2.5.6.3 Message

Notifies the UI about the successful rent payment event.

#### 2.5.6.4 Sequence Number

6

#### 2.5.6.5 Type

🔹 Event Notification

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
| Protocol | In-process Pub/Sub or Callback |
| Method | OnRentPaid(RentPaidEvent eventData) |
| Parameters | RentPaidEvent { Guid RenterId, Guid OwnerId, int A... |
| Authentication | N/A |
| Error Handling | UI event handlers should contain their own try-cat... |
| Performance | Asynchronous to prevent blocking the game logic lo... |

### 2.5.7.0 UI Update

#### 2.5.7.1 Source Id

REPO-PRES-001

#### 2.5.7.2 Target Id

REPO-PRES-001

#### 2.5.7.3 Message

Updates the HUD with new cash values for both players and plays a visual/audio effect for the transaction.

#### 2.5.7.4 Sequence Number

7

#### 2.5.7.5 Type

🔹 UI Update

#### 2.5.7.6 Is Synchronous

✅ Yes

#### 2.5.7.7 Return Message



#### 2.5.7.8 Has Return

❌ No

#### 2.5.7.9 Is Activation

❌ No

#### 2.5.7.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Internal Method Call |
| Method | HUDController.UpdatePlayerCash(playerId, newAmount... |
| Parameters | Player IDs, new cash values, board position, and t... |
| Authentication | N/A |
| Error Handling | UI components must be robust against missing refer... |
| Performance | All UI updates and effects must complete within a ... |

## 2.6.0.0 Notes

### 2.6.1.0 Content

#### 2.6.1.1 Content

Audit Log: The TurnManagementService is responsible for logging the successful transaction at the INFO level, including turn number, player IDs, property ID, and amount, to fulfill auditability requirement REQ-1-028.

#### 2.6.1.2 Position

bottom-left

#### 2.6.1.3 Participant Id

REPO-AS-005

#### 2.6.1.4 Sequence Number

6

### 2.6.2.0 Content

#### 2.6.2.1 Content

Bankruptcy Path: If the RuleEngine determines the renter has insufficient funds (Step 4), it returns a 'BankruptcyRequired' result instead of performing Step 5. The TurnManagementService would then initiate a separate, more complex bankruptcy sequence, which is outside the scope of this diagram.

#### 2.6.2.2 Position

bottom-right

#### 2.6.2.3 Participant Id

REPO-DM-001

#### 2.6.2.4 Sequence Number

5

### 2.6.3.0 Content

#### 2.6.3.1 Content

No Rent Path: If the property is mortgaged, the RuleEngine determines this in Step 3 and immediately returns a 'NoRentDue' result. The flow would then terminate without any transaction or UI notification.

#### 2.6.3.2 Position

top-right

#### 2.6.3.3 Participant Id

REPO-DM-001

#### 2.6.3.4 Sequence Number

3

## 2.7.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | N/A for an offline, single-player feature. No exte... |
| Performance Targets | The entire back-end sequence (Steps 2-5) must comp... |
| Error Handling Strategy | Business rule violations (e.g., insufficient funds... |
| Testing Considerations | Integration tests are critical. Test data must inc... |
| Monitoring Requirements | The successful rent transaction must be logged at ... |
| Deployment Considerations | This is a core feature and must be included and en... |

