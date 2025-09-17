# 1 Overview

## 1.1 Diagram Id

SEQ-FF-002

## 1.2 Name

Property Auction is Conducted

## 1.3 Description

When a player lands on an unowned property but declines to purchase it, the property is immediately put up for auction to all players. Bidding proceeds until a winner is determined.

## 1.4 Type

🔹 FeatureFlow

## 1.5 Purpose

To implement the official auction rule (REQ-1-052), ensuring properties enter the game and preventing game stalls.

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

- A player declines to buy a property.
- Application Services (PropertyActionService) initiates an auction.
- The Presentation Layer displays the auction UI to the human player, showing the current property, bid, and bidding player.
- The Application Services Layer manages the bidding turn order (clockwise).
- For AI players, the AIBehaviorTreeExecutor determines their maximum bid and places bids accordingly.
- For the human player, the UI waits for a 'Bid' or 'Pass' action.
- The auction continues until all but one player has passed.
- The winning player's cash is debited, and the property is transferred to them in the Domain Layer's GameState.

## 1.11 Triggers

- A player lands on an unowned property and selects the 'Auction' option.

## 1.12 Outcomes

- The auctioned property is sold to the highest bidder.
- The winner's cash is debited by the final bid amount.

## 1.13 Business Rules

- The auction must include all players.
- Bidding proceeds clockwise from the player who declined the initial purchase.
- The minimum starting bid is $1 (REQ-1-052).

## 1.14 Error Scenarios

- All players pass on the initial bid.

## 1.15 Integration Points

*No items available*

# 2.0 Details

## 2.1 Diagram Id

SEQ-FF-002

## 2.2 Name

Player Declines Purchase and Initiates Property Auction

## 2.3 Description

Technical sequence for conducting a property auction as per REQ-1-052. The flow is initiated by the Presentation Layer, orchestrated by an AuctionService in the Application Services Layer, which manages the bidding loop. It queries the Domain Layer's AIBehaviorTreeExecutor for AI bids and invokes the RuleEngine to atomically finalize the property transfer to the winner's GameState.

## 2.4 Participants

### 2.4.1 Actor

#### 2.4.1.1 Repository Id

User

#### 2.4.1.2 Display Name

User

#### 2.4.1.3 Type

🔹 Actor

#### 2.4.1.4 Technology

Human

#### 2.4.1.5 Order

1

#### 2.4.1.6 Style

| Property | Value |
|----------|-------|
| Shape | actor |
| Color | #E6E6E6 |
| Stereotype | <<Human Player>> |

### 2.4.2.0 Layer

#### 2.4.2.1 Repository Id

REPO-PRES-001

#### 2.4.2.2 Display Name

Presentation Layer

#### 2.4.2.3 Type

🔹 Layer

#### 2.4.2.4 Technology

Unity Engine, C#

#### 2.4.2.5 Order

2

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #ADD8E6 |
| Stereotype | <<View/Controller>> |

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
| Shape | component |
| Color | #90EE90 |
| Stereotype | <<Service>> |

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
| Shape | component |
| Color | #FFB6C1 |
| Stereotype | <<Aggregate/Engine>> |

## 2.5.0.0 Interactions

### 2.5.1.0 User Input

#### 2.5.1.1 Source Id

User

#### 2.5.1.2 Target Id

REPO-PRES-001

#### 2.5.1.3 Message

Selects 'Auction' option from Property Purchase dialog.

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

##### 2.5.1.10.1 Protocol

UI Event

##### 2.5.1.10.2 Method

OnClick()

##### 2.5.1.10.3 Parameters

- { action: 'auction' }

##### 2.5.1.10.4 Authentication

N/A

##### 2.5.1.10.5 Error Handling

Button is only enabled when action is valid.

##### 2.5.1.10.6 Performance

###### 2.5.1.10.6.1 Latency

<50ms

### 2.5.2.0.0.0 Command

#### 2.5.2.1.0.0 Source Id

REPO-PRES-001

#### 2.5.2.2.0.0 Target Id

REPO-AS-005

#### 2.5.2.3.0.0 Message

PropertyActionService.InitiateAuction(propertyId, startingPlayerId)

#### 2.5.2.4.0.0 Sequence Number

2

#### 2.5.2.5.0.0 Type

🔹 Command

#### 2.5.2.6.0.0 Is Synchronous

❌ No

#### 2.5.2.7.0.0 Return Message



#### 2.5.2.8.0.0 Has Return

❌ No

#### 2.5.2.9.0.0 Is Activation

✅ Yes

#### 2.5.2.10.0.0 Technical Details

##### 2.5.2.10.1.0 Protocol

In-Process Method Call

##### 2.5.2.10.2.0 Method

Task InitiateAuction(int propertyId, Guid startingPlayerId)

##### 2.5.2.10.3.0 Parameters

- propertyId: ID of the property to be auctioned.
- startingPlayerId: ID of the player who declined the purchase.

##### 2.5.2.10.4.0 Authentication

N/A

##### 2.5.2.10.5.0 Error Handling

Throws InvalidOperationException if property is not unowned or game is not in correct state.

##### 2.5.2.10.6.0 Performance

###### 2.5.2.10.6.1 Latency

<10ms

### 2.5.3.0.0.0 Event Notification

#### 2.5.3.1.0.0 Source Id

REPO-AS-005

#### 2.5.3.2.0.0 Target Id

REPO-PRES-001

#### 2.5.3.3.0.0 Message

AuctionUIManager.DisplayAuction(auctionState)

#### 2.5.3.4.0.0 Sequence Number

3

#### 2.5.3.5.0.0 Type

🔹 Event Notification

#### 2.5.3.6.0.0 Is Synchronous

❌ No

#### 2.5.3.7.0.0 Return Message



#### 2.5.3.8.0.0 Has Return

❌ No

#### 2.5.3.9.0.0 Is Activation

❌ No

#### 2.5.3.10.0.0 Technical Details

##### 2.5.3.10.1.0 Protocol

C# Event / Observer Pattern

##### 2.5.3.10.2.0 Method

OnAuctionStarted(AuctionState auctionState)

##### 2.5.3.10.3.0 Parameters

- auctionState: DTO containing property details, current bid, bidding player, etc.

##### 2.5.3.10.4.0 Authentication

N/A

##### 2.5.3.10.5.0 Error Handling

UI layer gracefully handles null state.

##### 2.5.3.10.6.0 Performance

###### 2.5.3.10.6.1 Latency

N/A (Async)

### 2.5.4.0.0.0 Query

#### 2.5.4.1.0.0 Source Id

REPO-AS-005

#### 2.5.4.2.0.0 Target Id

REPO-DM-001

#### 2.5.4.3.0.0 Message

AIBehaviorTreeExecutor.GetAuctionBid(auctionState, playerState)

#### 2.5.4.4.0.0 Sequence Number

4

#### 2.5.4.5.0.0 Type

🔹 Query

#### 2.5.4.6.0.0 Is Synchronous

✅ Yes

#### 2.5.4.7.0.0 Return Message

BidDecision { amount: decimal, pass: bool }

#### 2.5.4.8.0.0 Has Return

✅ Yes

#### 2.5.4.9.0.0 Is Activation

✅ Yes

#### 2.5.4.10.0.0 Technical Details

##### 2.5.4.10.1.0 Protocol

In-Process Method Call

##### 2.5.4.10.2.0 Method

BidDecision GetAuctionBid(AuctionState auctionState, PlayerState playerState)

##### 2.5.4.10.3.0 Parameters

- auctionState: Current state of the auction.
- playerState: The AI player's current financial and property state.

##### 2.5.4.10.4.0 Authentication

N/A

##### 2.5.4.10.5.0 Error Handling

Returns 'pass' if AI logic fails.

##### 2.5.4.10.6.0 Performance

###### 2.5.4.10.6.1 Latency

<100ms on recommended hardware.

### 2.5.5.0.0.0 User Input

#### 2.5.5.1.0.0 Source Id

User

#### 2.5.5.2.0.0 Target Id

REPO-PRES-001

#### 2.5.5.3.0.0 Message

Submits bid or passes via Auction UI.

#### 2.5.5.4.0.0 Sequence Number

5

#### 2.5.5.5.0.0 Type

🔹 User Input

#### 2.5.5.6.0.0 Is Synchronous

✅ Yes

#### 2.5.5.7.0.0 Return Message



#### 2.5.5.8.0.0 Has Return

❌ No

#### 2.5.5.9.0.0 Is Activation

❌ No

#### 2.5.5.10.0.0 Technical Details

##### 2.5.5.10.1.0 Protocol

UI Event

##### 2.5.5.10.2.0 Method

OnClick()

##### 2.5.5.10.3.0 Parameters

- { action: 'bid', amount: decimal } or { action: 'pass' }

##### 2.5.5.10.4.0 Authentication

N/A

##### 2.5.5.10.5.0 Error Handling

UI validates bid amount is greater than current bid.

##### 2.5.5.10.6.0 Performance

###### 2.5.5.10.6.1 Latency

<50ms

### 2.5.6.0.0.0 Command

#### 2.5.6.1.0.0 Source Id

REPO-PRES-001

#### 2.5.6.2.0.0 Target Id

REPO-AS-005

#### 2.5.6.3.0.0 Message

AuctionService.ProcessPlayerBid(playerId, bidDecision)

#### 2.5.6.4.0.0 Sequence Number

6

#### 2.5.6.5.0.0 Type

🔹 Command

#### 2.5.6.6.0.0 Is Synchronous

❌ No

#### 2.5.6.7.0.0 Return Message



#### 2.5.6.8.0.0 Has Return

❌ No

#### 2.5.6.9.0.0 Is Activation

❌ No

#### 2.5.6.10.0.0 Technical Details

##### 2.5.6.10.1.0 Protocol

In-Process Method Call

##### 2.5.6.10.2.0 Method

void ProcessPlayerBid(Guid playerId, BidDecision bidDecision)

##### 2.5.6.10.3.0 Parameters

- playerId: ID of the human or AI player.
- bidDecision: The action taken by the player.

##### 2.5.6.10.4.0 Authentication

N/A

##### 2.5.6.10.5.0 Error Handling

Logs invalid bids; ignores if it's not the specified player's turn.

##### 2.5.6.10.6.0 Performance

###### 2.5.6.10.6.1 Latency

<10ms

### 2.5.7.0.0.0 Internal Logic

#### 2.5.7.1.0.0 Source Id

REPO-AS-005

#### 2.5.7.2.0.0 Target Id

REPO-AS-005

#### 2.5.7.3.0.0 Message

Check for auction end condition (1 active bidder remaining).

#### 2.5.7.4.0.0 Sequence Number

7

#### 2.5.7.5.0.0 Type

🔹 Internal Logic

#### 2.5.7.6.0.0 Is Synchronous

✅ Yes

#### 2.5.7.7.0.0 Return Message

isAuctionOver: bool

#### 2.5.7.8.0.0 Has Return

✅ Yes

#### 2.5.7.9.0.0 Is Activation

❌ No

#### 2.5.7.10.0.0 Technical Details

##### 2.5.7.10.1.0 Protocol

Internal Method Call

##### 2.5.7.10.2.0 Method

bool CheckEndCondition()

##### 2.5.7.10.3.0 Parameters

*No items available*

##### 2.5.7.10.4.0 Authentication

N/A

##### 2.5.7.10.5.0 Error Handling

Internal state validation.

##### 2.5.7.10.6.0 Performance

###### 2.5.7.10.6.1 Latency

<1ms

### 2.5.8.0.0.0 Command

#### 2.5.8.1.0.0 Source Id

REPO-AS-005

#### 2.5.8.2.0.0 Target Id

REPO-DM-001

#### 2.5.8.3.0.0 Message

RuleEngine.ExecutePropertyPurchase(winnerId, propertyId, finalBid)

#### 2.5.8.4.0.0 Sequence Number

8

#### 2.5.8.5.0.0 Type

🔹 Command

#### 2.5.8.6.0.0 Is Synchronous

✅ Yes

#### 2.5.8.7.0.0 Return Message

TransactionResult { success: bool, error: string }

#### 2.5.8.8.0.0 Has Return

✅ Yes

#### 2.5.8.9.0.0 Is Activation

✅ Yes

#### 2.5.8.10.0.0 Technical Details

##### 2.5.8.10.1.0 Protocol

In-Process Method Call

##### 2.5.8.10.2.0 Method

TransactionResult ExecutePropertyPurchase(Guid winnerId, int propertyId, decimal finalBid)

##### 2.5.8.10.3.0 Parameters

- winnerId: The player who won the auction.
- propertyId: The property being purchased.
- finalBid: The final price.

##### 2.5.8.10.4.0 Authentication

N/A

##### 2.5.8.10.5.0 Error Handling

Returns failure result if player lacks funds. This operation must be atomic, modifying GameState transactionally.

##### 2.5.8.10.6.0 Performance

###### 2.5.8.10.6.1 Latency

<5ms

### 2.5.9.0.0.0 Event Notification

#### 2.5.9.1.0.0 Source Id

REPO-AS-005

#### 2.5.9.2.0.0 Target Id

REPO-PRES-001

#### 2.5.9.3.0.0 Message

AuctionUIManager.CloseAuction(auctionResult)

#### 2.5.9.4.0.0 Sequence Number

9

#### 2.5.9.5.0.0 Type

🔹 Event Notification

#### 2.5.9.6.0.0 Is Synchronous

❌ No

#### 2.5.9.7.0.0 Return Message



#### 2.5.9.8.0.0 Has Return

❌ No

#### 2.5.9.9.0.0 Is Activation

❌ No

#### 2.5.9.10.0.0 Technical Details

##### 2.5.9.10.1.0 Protocol

C# Event / Observer Pattern

##### 2.5.9.10.2.0 Method

OnAuctionEnded(AuctionResult auctionResult)

##### 2.5.9.10.3.0 Parameters

- auctionResult: DTO with winner, final price, and status.

##### 2.5.9.10.4.0 Authentication

N/A

##### 2.5.9.10.5.0 Error Handling

UI closes gracefully even if result is null.

##### 2.5.9.10.6.0 Performance

###### 2.5.9.10.6.1 Latency

N/A (Async)

## 2.6.0.0.0.0 Notes

- {'content': 'Bidding Loop (Steps 4-7): The AuctionService in the Application Services Layer (REPO-AS-005) manages a state machine that iterates clockwise through all active players. For each player, it either waits for human input (via step 5 & 6) or queries the AI for a decision (step 4). After each bid, it notifies the Presentation Layer to update the UI and checks the end condition (step 7).', 'position': 'top', 'participantId': 'REPO-AS-005', 'sequenceNumber': 4}

## 2.7.0.0.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | Input validation is required on the bid amount sub... |
| Performance Targets | The auction process must feel fast-paced. AI bid d... |
| Error Handling Strategy | If all players pass on the initial $1 bid, the Auc... |
| Testing Considerations | Integration tests are critical. Test scenarios mus... |
| Monitoring Requirements | To comply with REQ-1-028, every significant auctio... |
| Deployment Considerations | This is a core gameplay feature and must be includ... |

