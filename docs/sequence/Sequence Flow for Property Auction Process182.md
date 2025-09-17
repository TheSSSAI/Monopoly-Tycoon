# 1 Overview

## 1.1 Diagram Id

SEQ-FF-015

## 1.2 Name

Property Auction Process

## 1.3 Description

When a player lands on an unowned property and declines to buy it, the property is immediately auctioned to the highest bidder among all players.

## 1.4 Type

🔹 FeatureFlow

## 1.5 Purpose

To correctly implement the official Monopoly auction rules.

## 1.6 Complexity

High

## 1.7 Priority

🔴 High

## 1.8 Frequency

OnDemand

## 1.9 Participants

- PresentationLayer
- ApplicationServicesLayer
- BusinessLogicLayer

## 1.10 Key Interactions

- RuleEngine determines a property should be auctioned.
- TurnManagementService initiates the auction state.
- For the human player, an auction UI is displayed.
- Bidding proceeds clockwise, starting from the player who declined the purchase.
- AI players use their AIBehaviorTreeExecutor to determine their max bid.
- The TurnManagementService manages the bidding process until no player is willing to increase the bid.
- The highest bidder's cash is debited, and the RuleEngine assigns them the property deed.

## 1.11 Triggers

- A player lands on an unowned property and chooses 'Auction' or declines to buy.

## 1.12 Outcomes

- The property is sold to the highest bidder for the final auction price.

## 1.13 Business Rules

- The auction starts with the player who declined the purchase (REQ-1-052).
- Bidding proceeds clockwise.
- Any player can bid, including the one who originally declined (REQ-1-052).

## 1.14 Error Scenarios

- All players decline to bid.

## 1.15 Integration Points

*No items available*

# 2.0 Details

## 2.1 Diagram Id

SEQ-FF-015

## 2.2 Name

Implementation: Property Auction Process

## 2.3 Description

A detailed sequence for managing the property auction process, as required by REQ-1-052. It begins when a player declines to purchase an unowned property, triggering an auction state managed by the ApplicationServicesLayer. The sequence handles bidding from both AI (via AIBehaviorTreeExecutor) and human players (via a dedicated UI), enforces clockwise bidding rules, and finalizes the transaction by updating the GameState via the BusinessLogicLayer.

## 2.4 Participants

### 2.4.1 UI/View

#### 2.4.1.1 Repository Id

REPO-IP-UI-007

#### 2.4.1.2 Display Name

Presentation Layer

#### 2.4.1.3 Type

🔹 UI/View

#### 2.4.1.4 Technology

Unity Engine, C#

#### 2.4.1.5 Order

1

#### 2.4.1.6 Style

| Property | Value |
|----------|-------|
| Shape | actor |
| Color | #4CAF50 |
| Stereotype | UI |

### 2.4.2.0 Service Orchestration

#### 2.4.2.1 Repository Id

REPO-AS-005

#### 2.4.2.2 Display Name

Application Services Layer

#### 2.4.2.3 Type

🔹 Service Orchestration

#### 2.4.2.4 Technology

.NET 8, C#

#### 2.4.2.5 Order

2

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #2196F3 |
| Stereotype | Service |

### 2.4.3.0 Domain Logic

#### 2.4.3.1 Repository Id

REPO-DM-001

#### 2.4.3.2 Display Name

Business Logic (Domain) Layer

#### 2.4.3.3 Type

🔹 Domain Logic

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

## 2.5.0.0 Interactions

### 2.5.1.0 Synchronous Method Call

#### 2.5.1.1 Source Id

REPO-IP-UI-007

#### 2.5.1.2 Target Id

REPO-AS-005

#### 2.5.1.3 Message

PropertyActionService.DeclinePurchase(playerId, propertyId)

#### 2.5.1.4 Sequence Number

1

#### 2.5.1.5 Type

🔹 Synchronous Method Call

#### 2.5.1.6 Is Synchronous

✅ Yes

#### 2.5.1.7 Return Message

void

#### 2.5.1.8 Has Return

❌ No

#### 2.5.1.9 Is Activation

✅ Yes

#### 2.5.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | DeclinePurchase |
| Parameters | Guid playerId, int propertyId |
| Authentication | N/A |
| Error Handling | Logs argument validation errors. |
| Performance | Sub-millisecond latency expected. |

### 2.5.2.0 Synchronous Method Call

#### 2.5.2.1 Source Id

REPO-AS-005

#### 2.5.2.2 Target Id

REPO-DM-001

#### 2.5.2.3 Message

RuleEngine.InitiateAuction(propertyId, decliningPlayerId)

#### 2.5.2.4 Sequence Number

2

#### 2.5.2.5 Type

🔹 Synchronous Method Call

#### 2.5.2.6 Is Synchronous

✅ Yes

#### 2.5.2.7 Return Message

AuctionState

#### 2.5.2.8 Has Return

✅ Yes

#### 2.5.2.9 Is Activation

✅ Yes

#### 2.5.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | InitiateAuction |
| Parameters | int propertyId, Guid decliningPlayerId |
| Authentication | N/A |
| Error Handling | Throws InvalidOperationException if property is no... |
| Performance | Sub-millisecond latency expected. |

### 2.5.3.0 Internal State Transition

#### 2.5.3.1 Source Id

REPO-AS-005

#### 2.5.3.2 Target Id

REPO-AS-005

#### 2.5.3.3 Message

TurnManagementService.BeginAuctionLoop(auctionState)

#### 2.5.3.4 Sequence Number

3

#### 2.5.3.5 Type

🔹 Internal State Transition

#### 2.5.3.6 Is Synchronous

✅ Yes

#### 2.5.3.7 Return Message

void

#### 2.5.3.8 Has Return

❌ No

#### 2.5.3.9 Is Activation

❌ No

#### 2.5.3.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | BeginAuctionLoop |
| Parameters | AuctionState auctionState |
| Authentication | N/A |
| Error Handling | Manages the bidding loop, player states, and timeo... |
| Performance | The loop duration is dependent on user/AI response... |

### 2.5.4.0 Asynchronous Event/Notification

#### 2.5.4.1 Source Id

REPO-AS-005

#### 2.5.4.2 Target Id

REPO-IP-UI-007

#### 2.5.4.3 Message

AuctionUIController.DisplayAuction(auctionDetails)

#### 2.5.4.4 Sequence Number

4

#### 2.5.4.5 Type

🔹 Asynchronous Event/Notification

#### 2.5.4.6 Is Synchronous

❌ No

#### 2.5.4.7 Return Message

N/A

#### 2.5.4.8 Has Return

❌ No

#### 2.5.4.9 Is Activation

❌ No

#### 2.5.4.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Event Bus |
| Method | DisplayAuction |
| Parameters | AuctionDetailsDTO auctionDetails |
| Authentication | N/A |
| Error Handling | UI layer handles null or malformed DTOs gracefully... |
| Performance | UI should render in < 250ms. |

### 2.5.5.0 Synchronous Method Call

#### 2.5.5.1 Source Id

REPO-AS-005

#### 2.5.5.2 Target Id

REPO-DM-001

#### 2.5.5.3 Message

AIBehaviorTreeExecutor.DetermineBid(aiPlayerId, propertyId, currentHighestBid)

#### 2.5.5.4 Sequence Number

5

#### 2.5.5.5 Type

🔹 Synchronous Method Call

#### 2.5.5.6 Is Synchronous

✅ Yes

#### 2.5.5.7 Return Message

int bidAmount

#### 2.5.5.8 Has Return

✅ Yes

#### 2.5.5.9 Is Activation

✅ Yes

#### 2.5.5.10 Nested Interactions

*No items available*

#### 2.5.5.11 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | DetermineBid |
| Parameters | Guid aiPlayerId, int propertyId, int currentHighes... |
| Authentication | N/A |
| Error Handling | Returns 0 if AI chooses not to bid. Logs decision ... |
| Performance | Must complete < 500ms to avoid gameplay stutter. |

### 2.5.6.0 Asynchronous Event/Notification

#### 2.5.6.1 Source Id

REPO-AS-005

#### 2.5.6.2 Target Id

REPO-IP-UI-007

#### 2.5.6.3 Message

AuctionUIController.UpdateHighestBid(playerName, newBidAmount)

#### 2.5.6.4 Sequence Number

6

#### 2.5.6.5 Type

🔹 Asynchronous Event/Notification

#### 2.5.6.6 Is Synchronous

❌ No

#### 2.5.6.7 Return Message

N/A

#### 2.5.6.8 Has Return

❌ No

#### 2.5.6.9 Is Activation

❌ No

#### 2.5.6.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Event Bus |
| Method | UpdateHighestBid |
| Parameters | string playerName, int newBidAmount |
| Authentication | N/A |
| Error Handling | UI updates gracefully. |
| Performance | Near-instantaneous update. |

### 2.5.7.0 Synchronous Method Call

#### 2.5.7.1 Source Id

REPO-IP-UI-007

#### 2.5.7.2 Target Id

REPO-AS-005

#### 2.5.7.3 Message

TurnManagementService.SubmitHumanBid(humanPlayerId, bidAmount)

#### 2.5.7.4 Sequence Number

7

#### 2.5.7.5 Type

🔹 Synchronous Method Call

#### 2.5.7.6 Is Synchronous

✅ Yes

#### 2.5.7.7 Return Message

void

#### 2.5.7.8 Has Return

❌ No

#### 2.5.7.9 Is Activation

❌ No

#### 2.5.7.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | SubmitHumanBid |
| Parameters | Guid humanPlayerId, int bidAmount (0 to pass) |
| Authentication | N/A |
| Error Handling | Validates bid is higher than current bid and playe... |
| Performance | Sub-millisecond latency expected. |

### 2.5.8.0 Internal State Transition

#### 2.5.8.1 Source Id

REPO-AS-005

#### 2.5.8.2 Target Id

REPO-AS-005

#### 2.5.8.3 Message

TurnManagementService.EndAuctionLoop()

#### 2.5.8.4 Sequence Number

8

#### 2.5.8.5 Type

🔹 Internal State Transition

#### 2.5.8.6 Is Synchronous

✅ Yes

#### 2.5.8.7 Return Message

AuctionResult

#### 2.5.8.8 Has Return

✅ Yes

#### 2.5.8.9 Is Activation

❌ No

#### 2.5.8.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | EndAuctionLoop |
| Parameters | None |
| Authentication | N/A |
| Error Handling | Handles the case where no one bids; AuctionResult ... |
| Performance | Determines winner based on final state of bidding ... |

### 2.5.9.0 Synchronous Method Call

#### 2.5.9.1 Source Id

REPO-AS-005

#### 2.5.9.2 Target Id

REPO-DM-001

#### 2.5.9.3 Message

RuleEngine.FinalizeAuctionSale(winnerId, propertyId, finalAmount)

#### 2.5.9.4 Sequence Number

9

#### 2.5.9.5 Type

🔹 Synchronous Method Call

#### 2.5.9.6 Is Synchronous

✅ Yes

#### 2.5.9.7 Return Message

TransactionResult

#### 2.5.9.8 Has Return

✅ Yes

#### 2.5.9.9 Is Activation

✅ Yes

#### 2.5.9.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | FinalizeAuctionSale |
| Parameters | Guid winnerId, int propertyId, int finalAmount |
| Authentication | N/A |
| Error Handling | Returns failure if winner lacks funds. Throws exce... |
| Performance | Sub-millisecond latency expected. |

### 2.5.10.0 Asynchronous Event/Notification

#### 2.5.10.1 Source Id

REPO-AS-005

#### 2.5.10.2 Target Id

REPO-IP-UI-007

#### 2.5.10.3 Message

AuctionUIController.ShowResultAndClose(auctionResult)

#### 2.5.10.4 Sequence Number

10

#### 2.5.10.5 Type

🔹 Asynchronous Event/Notification

#### 2.5.10.6 Is Synchronous

❌ No

#### 2.5.10.7 Return Message

N/A

#### 2.5.10.8 Has Return

❌ No

#### 2.5.10.9 Is Activation

❌ No

#### 2.5.10.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Event Bus |
| Method | ShowResultAndClose |
| Parameters | AuctionResultDTO auctionResult |
| Authentication | N/A |
| Error Handling | Displays winner or 'No bids' message, then closes ... |
| Performance | Displays result for a fixed duration (e.g., 3 seco... |

## 2.6.0.0 Notes

### 2.6.1.0 Content

#### 2.6.1.1 Content

Interactions 5, 6, and 7 occur within a loop managed by the TurnManagementService. The loop proceeds clockwise among active bidders until only one remains.

#### 2.6.1.2 Position

bottom

#### 2.6.1.3 Participant Id

REPO-AS-005

#### 2.6.1.4 Sequence Number

7

### 2.6.2.0 Content

#### 2.6.2.1 Content

If AuctionResult has no winner (all players passed), the call to FinalizeAuctionSale (Step 9) is skipped.

#### 2.6.2.2 Position

bottom

#### 2.6.2.3 Participant Id

REPO-AS-005

#### 2.6.2.4 Sequence Number

8

## 2.7.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | N/A. This is an offline, single-player feature wit... |
| Performance Targets | The auction UI must remain responsive (60 FPS). AI... |
| Error Handling Strategy | If all players pass on the initial bid, the TurnMa... |
| Testing Considerations | Requires integration tests with mock AI and human ... |
| Monitoring Requirements | Log key auction events at INFO level: AuctionStart... |
| Deployment Considerations | This is a core game mechanic as per official rules... |

