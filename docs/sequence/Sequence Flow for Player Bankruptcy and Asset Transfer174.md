# 1 Overview

## 1.1 Diagram Id

SEQ-BP-007

## 1.2 Name

Player Bankruptcy and Asset Transfer

## 1.3 Description

A player is unable to pay a debt. The system determines if they have assets to liquidate (buildings, properties). If still unable to pay, the system declares them bankrupt and transfers their entire portfolio to the creditor (either another player or the Bank, with different outcomes).

## 1.4 Type

🔹 BusinessProcess

## 1.5 Purpose

To correctly implement the game-ending condition of bankruptcy, a core rule of Monopoly (REQ-1-065, REQ-1-066).

## 1.6 Complexity

High

## 1.7 Priority

🔴 High

## 1.8 Frequency

OnDemand

## 1.9 Participants

- ApplicationServicesLayer
- BusinessLogicLayer

## 1.10 Key Interactions

- A player owes a debt and has insufficient cash.
- The RuleEngine determines the player is bankrupt if they cannot raise sufficient funds after liquidating all assets.
- If the creditor is another player, the RuleEngine transfers all of the debtor's assets (cash, properties, cards) to the creditor.
- If the creditor is the Bank, the RuleEngine returns all properties to the bank, and the TurnManagementService initiates auctions for them.
- The RuleEngine updates the bankrupt player's status in the GameState.
- The TurnManagementService removes the bankrupt player from the turn rotation.

## 1.11 Triggers

- A player incurs a debt they cannot pay after liquidating all possible assets.

## 1.12 Outcomes

- The bankrupt player is removed from the game.
- The creditor receives all of the bankrupt player's assets, or the bank reclaims and auctions them.
- The game continues with the remaining players.

## 1.13 Business Rules

- If debt is to another player, all assets transfer to that player (REQ-1-066).
- If debt is to Bank, properties are auctioned off (REQ-1-066).
- Mortgaged properties transferred to a new owner remain mortgaged.

## 1.14 Error Scenarios

- Incorrect calculation of asset transfer, leading to an invalid game state which should be logged as a critical error.

## 1.15 Integration Points

*No items available*

# 2.0 Details

## 2.1 Diagram Id

SEQ-BP-007

## 2.2 Name

Player Bankruptcy Process and Asset Transfer Workflow

## 2.3 Description

A comprehensive sequence detailing the multi-step business process for handling a player's bankruptcy. The sequence is initiated by the application layer when a debt is owed, and the business logic layer is responsible for enforcing all rules related to asset liquidation, bankruptcy determination, and asset transfer. The process correctly handles the two distinct outcomes based on whether the creditor is another player or the Bank, as specified in REQ-1-065 and REQ-1-066.

## 2.4 Participants

### 2.4.1 Application Service

#### 2.4.1.1 Repository Id

REPO-AS-005

#### 2.4.1.2 Display Name

TurnManagementService

#### 2.4.1.3 Type

🔹 Application Service

#### 2.4.1.4 Technology

.NET 8 C#

#### 2.4.1.5 Order

1

#### 2.4.1.6 Style

| Property | Value |
|----------|-------|
| Shape | actor |
| Color | #1E90FF |
| Stereotype | «Service» |

### 2.4.2.0 Domain Service

#### 2.4.2.1 Repository Id

REPO-DM-001

#### 2.4.2.2 Display Name

RuleEngine

#### 2.4.2.3 Type

🔹 Domain Service

#### 2.4.2.4 Technology

.NET 8 C#

#### 2.4.2.5 Order

2

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | participant |
| Color | #FF4500 |
| Stereotype | «Engine» |

### 2.4.3.0 Domain Aggregate

#### 2.4.3.1 Repository Id

REPO-DM-001

#### 2.4.3.2 Display Name

GameState

#### 2.4.3.3 Type

🔹 Domain Aggregate

#### 2.4.3.4 Technology

In-Memory C# Object

#### 2.4.3.5 Order

3

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | database |
| Color | #32CD32 |
| Stereotype | «State» |

## 2.5.0.0 Interactions

### 2.5.1.0 Method Invocation

#### 2.5.1.1 Source Id

REPO-AS-005

#### 2.5.1.2 Target Id

REPO-DM-001

#### 2.5.1.3 Message

ResolveDebt(debtorPlayerId, creditor, debtAmount)

#### 2.5.1.4 Sequence Number

1

#### 2.5.1.5 Type

🔹 Method Invocation

#### 2.5.1.6 Is Synchronous

✅ Yes

#### 2.5.1.7 Return Message

returns BankruptcyResult

#### 2.5.1.8 Has Return

✅ Yes

#### 2.5.1.9 Is Activation

✅ Yes

#### 2.5.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Method Call |
| Method | Task<BankruptcyResult> ResolveDebt(Guid debtorPlay... |
| Parameters | debtorPlayerId: ID of the player in debt. creditor... |
| Authentication | N/A |
| Error Handling | Throws InvalidOperationException if debtor does no... |
| Performance | SLA < 50ms. This operation must be fast to not int... |

#### 2.5.1.11 Nested Interactions

##### 2.5.1.11.1 Internal Method Call

###### 2.5.1.11.1.1 Source Id

REPO-DM-001

###### 2.5.1.11.1.2 Target Id

REPO-DM-001

###### 2.5.1.11.1.3 Message

DetermineIfBankrupt(debtorPlayerId, debtAmount)

###### 2.5.1.11.1.4 Sequence Number

2

###### 2.5.1.11.1.5 Type

🔹 Internal Method Call

###### 2.5.1.11.1.6 Is Synchronous

✅ Yes

###### 2.5.1.11.1.7 Return Message

returns bool

###### 2.5.1.11.1.8 Has Return

✅ Yes

###### 2.5.1.11.1.9 Is Activation

❌ No

###### 2.5.1.11.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Method Call |
| Method | bool DetermineIfBankrupt(Guid debtorPlayerId, deci... |
| Parameters | Internal call to encapsulate the complex logic of ... |
| Authentication | N/A |
| Error Handling | Relies on internal state consistency. |
| Performance | The most computationally intensive part of the seq... |

###### 2.5.1.11.1.11 Nested Interactions

- {'sourceId': 'REPO-DM-001', 'targetId': 'REPO-DM-001', 'message': 'CalculateTotalLiquidatableValue(debtorPlayerId)', 'sequenceNumber': 3, 'type': 'Internal Method Call', 'isSynchronous': True, 'returnMessage': 'returns decimal', 'hasReturn': True, 'isActivation': False, 'technicalDetails': {'protocol': 'In-Process Method Call', 'method': 'decimal CalculateTotalLiquidatableValue(Guid debtorPlayerId)', 'parameters': 'Calculates cash on hand + value from selling all buildings + mortgage value of all properties.', 'authentication': 'N/A', 'errorHandling': 'N/A', 'performance': 'Must iterate through all player assets.'}, 'nestedInteractions': [{'sourceId': 'REPO-DM-001', 'targetId': 'REPO-DM-001', 'message': 'GameState.GetPlayerState(debtorPlayerId)', 'sequenceNumber': 4, 'type': 'Data Access', 'isSynchronous': True, 'returnMessage': 'returns PlayerState object', 'hasReturn': True, 'isActivation': False, 'technicalDetails': {'protocol': 'In-Process Method Call', 'method': 'PlayerState GetPlayerState(Guid playerId)', 'parameters': 'Retrieves the full state of the player from the main GameState aggregate.', 'authentication': 'N/A', 'errorHandling': 'Throws KeyNotFoundException if player does not exist.', 'performance': 'High-speed in-memory lookup.'}}]}

##### 2.5.1.11.2.0 Alternative Fragment

###### 2.5.1.11.2.1 Source Id

REPO-DM-001

###### 2.5.1.11.2.2 Target Id

REPO-DM-001

###### 2.5.1.11.2.3 Message

[ALT: Creditor is Player vs. Bank]

###### 2.5.1.11.2.4 Sequence Number

5

###### 2.5.1.11.2.5 Type

🔹 Alternative Fragment

###### 2.5.1.11.2.6 Is Synchronous

✅ Yes

###### 2.5.1.11.2.7 Return Message



###### 2.5.1.11.2.8 Has Return

❌ No

###### 2.5.1.11.2.9 Is Activation

❌ No

###### 2.5.1.11.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Conditional Logic |
| Method | if (creditor is Player) |
| Parameters | Checks the type of the creditor to determine the a... |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | N/A |

###### 2.5.1.11.2.11 Nested Interactions

####### 2.5.1.11.2.11.1 Fragment Condition

######## 2.5.1.11.2.11.1.1 Source Id

REPO-DM-001

######## 2.5.1.11.2.11.1.2 Target Id

REPO-DM-001

######## 2.5.1.11.2.11.1.3 Message

[Creditor is Player]

######## 2.5.1.11.2.11.1.4 Sequence Number

6

######## 2.5.1.11.2.11.1.5 Type

🔹 Fragment Condition

######## 2.5.1.11.2.11.1.6 Is Synchronous

✅ Yes

######## 2.5.1.11.2.11.1.7 Return Message



######## 2.5.1.11.2.11.1.8 Has Return

❌ No

######## 2.5.1.11.2.11.1.9 Is Activation

❌ No

######## 2.5.1.11.2.11.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | N/A |
| Method | N/A |
| Parameters | REQ-1-066: Transfer all assets to the creditor pla... |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | N/A |

######## 2.5.1.11.2.11.1.11 Nested Interactions

- {'sourceId': 'REPO-DM-001', 'targetId': 'REPO-DM-001', 'message': 'GameState.TransferAllAssets(debtorPlayerId, creditorPlayerId)', 'sequenceNumber': 7, 'type': 'State Mutation', 'isSynchronous': True, 'returnMessage': '', 'hasReturn': False, 'isActivation': True, 'technicalDetails': {'protocol': 'In-Process Method Call', 'method': 'void TransferAllAssets(Guid fromPlayerId, Guid toPlayerId)', 'parameters': 'Atomically transfers cash, all properties (including mortgaged status), and Get Out of Jail Free cards.', 'authentication': 'N/A', 'errorHandling': 'This operation must be atomic. Failure should result in a critical log and halt, preventing a corrupt game state.', 'performance': 'Efficiently reassigns pointers/references in the data model.'}}

####### 2.5.1.11.2.11.2.0 Fragment Condition

######## 2.5.1.11.2.11.2.1 Source Id

REPO-DM-001

######## 2.5.1.11.2.11.2.2 Target Id

REPO-DM-001

######## 2.5.1.11.2.11.2.3 Message

[Creditor is Bank]

######## 2.5.1.11.2.11.2.4 Sequence Number

8

######## 2.5.1.11.2.11.2.5 Type

🔹 Fragment Condition

######## 2.5.1.11.2.11.2.6 Is Synchronous

✅ Yes

######## 2.5.1.11.2.11.2.7 Return Message



######## 2.5.1.11.2.11.2.8 Has Return

❌ No

######## 2.5.1.11.2.11.2.9 Is Activation

❌ No

######## 2.5.1.11.2.11.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | N/A |
| Method | N/A |
| Parameters | REQ-1-066: Return all properties to the bank for a... |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | N/A |

######## 2.5.1.11.2.11.2.11 Nested Interactions

- {'sourceId': 'REPO-DM-001', 'targetId': 'REPO-DM-001', 'message': 'GameState.ReturnAllPropertiesToBank(debtorPlayerId)', 'sequenceNumber': 9, 'type': 'State Mutation', 'isSynchronous': True, 'returnMessage': 'returns List<Property>', 'hasReturn': True, 'isActivation': True, 'technicalDetails': {'protocol': 'In-Process Method Call', 'method': 'List<Property> ReturnAllPropertiesToBank(Guid fromPlayerId)', 'parameters': 'Changes ownership of all properties back to the Bank. Returns the list of reclaimed properties for auction.', 'authentication': 'N/A', 'errorHandling': 'This operation must be atomic.', 'performance': 'N/A'}}

##### 2.5.1.11.3.0.0.0 State Mutation

###### 2.5.1.11.3.1.0.0 Source Id

REPO-DM-001

###### 2.5.1.11.3.2.0.0 Target Id

REPO-DM-001

###### 2.5.1.11.3.3.0.0 Message

GameState.UpdatePlayerStatus(debtorPlayerId, PlayerStatus.Bankrupt)

###### 2.5.1.11.3.4.0.0 Sequence Number

10

###### 2.5.1.11.3.5.0.0 Type

🔹 State Mutation

###### 2.5.1.11.3.6.0.0 Is Synchronous

✅ Yes

###### 2.5.1.11.3.7.0.0 Return Message



###### 2.5.1.11.3.8.0.0 Has Return

❌ No

###### 2.5.1.11.3.9.0.0 Is Activation

✅ Yes

###### 2.5.1.11.3.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Method Call |
| Method | void UpdatePlayerStatus(Guid playerId, PlayerStatu... |
| Parameters | Sets the player's status enum to Bankrupt, effecti... |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | N/A |

### 2.5.2.0.0.0.0.0 Internal Method Call

#### 2.5.2.1.0.0.0.0 Source Id

REPO-AS-005

#### 2.5.2.2.0.0.0.0 Target Id

REPO-AS-005

#### 2.5.2.3.0.0.0.0 Message

ProcessBankruptcyResult(result)

#### 2.5.2.4.0.0.0.0 Sequence Number

11

#### 2.5.2.5.0.0.0.0 Type

🔹 Internal Method Call

#### 2.5.2.6.0.0.0.0 Is Synchronous

✅ Yes

#### 2.5.2.7.0.0.0.0 Return Message



#### 2.5.2.8.0.0.0.0 Has Return

❌ No

#### 2.5.2.9.0.0.0.0 Is Activation

❌ No

#### 2.5.2.10.0.0.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Method Call |
| Method | void ProcessBankruptcyResult(BankruptcyResult resu... |
| Parameters | Handles the aftermath of the bankruptcy based on t... |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | N/A |

#### 2.5.2.11.0.0.0.0 Nested Interactions

##### 2.5.2.11.1.0.0.0 State Mutation

###### 2.5.2.11.1.1.0.0 Source Id

REPO-AS-005

###### 2.5.2.11.1.2.0.0 Target Id

REPO-AS-005

###### 2.5.2.11.1.3.0.0 Message

RemovePlayerFromTurnOrder(result.BankruptPlayerId)

###### 2.5.2.11.1.4.0.0 Sequence Number

12

###### 2.5.2.11.1.5.0.0 Type

🔹 State Mutation

###### 2.5.2.11.1.6.0.0 Is Synchronous

✅ Yes

###### 2.5.2.11.1.7.0.0 Return Message



###### 2.5.2.11.1.8.0.0 Has Return

❌ No

###### 2.5.2.11.1.9.0.0 Is Activation

❌ No

###### 2.5.2.11.1.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Method Call |
| Method | void RemovePlayerFromTurnOrder(Guid playerId) |
| Parameters | Modifies the internal turn queue to exclude the ba... |
| Authentication | N/A |
| Error Handling | Logs a warning if the player was already not in th... |
| Performance | Simple list/queue removal operation. |

##### 2.5.2.11.2.0.0.0 Loop Fragment

###### 2.5.2.11.2.1.0.0 Source Id

REPO-AS-005

###### 2.5.2.11.2.2.0.0 Target Id

REPO-AS-005

###### 2.5.2.11.2.3.0.0 Message

[LOOP: For each property returned to bank]

###### 2.5.2.11.2.4.0.0 Sequence Number

13

###### 2.5.2.11.2.5.0.0 Type

🔹 Loop Fragment

###### 2.5.2.11.2.6.0.0 Is Synchronous

✅ Yes

###### 2.5.2.11.2.7.0.0 Return Message



###### 2.5.2.11.2.8.0.0 Has Return

❌ No

###### 2.5.2.11.2.9.0.0 Is Activation

❌ No

###### 2.5.2.11.2.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Conditional Logic |
| Method | if(result.CreditorType == 'Bank') |
| Parameters | If the bank was the creditor, initiates an auction... |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | N/A |

###### 2.5.2.11.2.11.0.0 Nested Interactions

- {'sourceId': 'REPO-AS-005', 'targetId': 'REPO-AS-005', 'message': 'StartAuction(property)', 'sequenceNumber': 14, 'type': 'Method Invocation', 'isSynchronous': True, 'returnMessage': '', 'hasReturn': False, 'isActivation': False, 'technicalDetails': {'protocol': 'In-Process Method Call', 'method': 'void StartAuction(Property property)', 'parameters': 'Initiates the auction sequence for a single property, another complex business process.', 'authentication': 'N/A', 'errorHandling': 'Delegated to the auction process.', 'performance': 'N/A'}}

## 2.6.0.0.0.0.0.0 Notes

### 2.6.1.0.0.0.0.0 Content

#### 2.6.1.1.0.0.0.0 Content

Business Rule Enforcement: The RuleEngine is the sole authority on bankruptcy determination and asset transfer logic. The Application Service Layer orchestrates the process but does not contain any of the core game rules, adhering to the Layered Architecture.

#### 2.6.1.2.0.0.0.0 Position

top-right

#### 2.6.1.3.0.0.0.0 Participant Id

REPO-DM-001

#### 2.6.1.4.0.0.0.0 Sequence Number

1

### 2.6.2.0.0.0.0.0 Content

#### 2.6.2.1.0.0.0.0 Content

Atomicity Requirement: The asset transfer process within the GameState (steps 7 and 9) must be treated as an atomic transaction. A failure mid-transfer would corrupt the game state. Implementation should wrap this logic in a try-catch block; on failure, a critical error is logged and the game should not be saveable.

#### 2.6.2.2.0.0.0.0 Position

bottom-left

#### 2.6.2.3.0.0.0.0 Participant Id

REPO-DM-001

#### 2.6.2.4.0.0.0.0 Sequence Number

7

### 2.6.3.0.0.0.0.0 Content

#### 2.6.3.1.0.0.0.0 Content

Audit Trail: A successful bankruptcy must be logged at the INFO level with key details: Debtor, Creditor, Final Debt, and a summary of transferred assets. This satisfies the auditability requirement REQ-1-028.

#### 2.6.3.2.0.0.0.0 Position

bottom-right

#### 2.6.3.3.0.0.0.0 Participant Id

REPO-AS-005

#### 2.6.3.4.0.0.0.0 Sequence Number

11

## 2.7.0.0.0.0.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | N/A. The system is entirely offline and single-pla... |
| Performance Targets | The entire bankruptcy resolution process, from deb... |
| Error Handling Strategy | The primary risk is a corrupted GameState due to a... |
| Testing Considerations | Requires extensive integration testing using pre-c... |
| Monitoring Requirements | All key steps of the bankruptcy process should be ... |
| Deployment Considerations | N/A. This logic is part of the core monolithic app... |

