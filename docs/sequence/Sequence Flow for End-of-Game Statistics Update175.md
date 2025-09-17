# 1 Overview

## 1.1 Diagram Id

SEQ-DF-008

## 1.2 Name

End-of-Game Statistics Update

## 1.3 Description

When a game concludes, the system processes the final game state, calculates statistics for the human player (e.g., win/loss, duration), and updates their persistent historical record across multiple tables in the local SQLite database.

## 1.4 Type

🔹 DataFlow

## 1.5 Purpose

To provide long-term progression and replayability by tracking player performance over time (REQ-1-006, REQ-1-033).

## 1.6 Complexity

Medium

## 1.7 Priority

🔴 High

## 1.8 Frequency

OnDemand

## 1.9 Participants

- ApplicationServicesLayer
- InfrastructureLayer

## 1.10 Key Interactions

- The GameSessionService detects the 'GameEnded' event.
- It constructs a GameResult object from the final GameState.
- It calls the IStatisticsRepository with the GameResult within a single database transaction.
- The StatisticsRepository (Infrastructure) updates the human player's PlayerStats record (increments total games played, total wins).
- It inserts a new record for the completed game into the GameHistory table.
- If the win resulted in a high score, it updates the TopScores table.

## 1.11 Triggers

- A game officially ends with a single winner.

## 1.12 Outcomes

- The user's historical statistics (wins, losses, etc.) are permanently updated.
- The Top 10 high scores list is updated if applicable.

## 1.13 Business Rules

- Statistics must be persisted in a local SQLite database (REQ-1-089).
- Tracked stats must include total games, wins, win/loss ratio, etc. (REQ-1-034).
- All database updates for a single game result must be atomic (transactional).

## 1.14 Error Scenarios

- Failure to connect to the SQLite database.
- A SQL transaction fails, and all changes are rolled back to prevent inconsistent data.

## 1.15 Integration Points

- SQLite Database

# 2.0 Details

## 2.1 Diagram Id

SEQ-DF-008

## 2.2 Name

Atomic End-of-Game Statistics Persistence

## 2.3 Description

Details the technical sequence for atomically updating a player's persistent historical statistics in the local SQLite database upon game completion. The sequence is initiated by a domain event, orchestrates data transformation, and uses a repository pattern to execute a series of SQL commands within a single, ACID-compliant transaction to ensure data integrity.

## 2.4 Participants

### 2.4.1 Application Service

#### 2.4.1.1 Repository Id

REPO-AS-005

#### 2.4.1.2 Display Name

ApplicationServicesLayer

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
| Color | #A2D2FF |
| Stereotype | Service |

### 2.4.2.0 Repository

#### 2.4.2.1 Repository Id

REPO-IP-ST-009

#### 2.4.2.2 Display Name

InfrastructureLayer (StatisticsRepository)

#### 2.4.2.3 Type

🔹 Repository

#### 2.4.2.4 Technology

Microsoft.Data.Sqlite

#### 2.4.2.5 Order

2

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #BDE0FE |
| Stereotype | Repository |

### 2.4.3.0 Database

#### 2.4.3.1 Repository Id

sqlite-db

#### 2.4.3.2 Display Name

SQLite Database

#### 2.4.3.3 Type

🔹 Database

#### 2.4.3.4 Technology

SQLite 3.x

#### 2.4.3.5 Order

3

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | database |
| Color | #FFC8DD |
| Stereotype | Database |

## 2.5.0.0 Interactions

### 2.5.1.0 Event Handling

#### 2.5.1.1 Source Id

REPO-AS-005

#### 2.5.1.2 Target Id

REPO-AS-005

#### 2.5.1.3 Message

1. GameEnded event is handled by GameSessionService

#### 2.5.1.4 Sequence Number

1

#### 2.5.1.5 Type

🔹 Event Handling

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
| Protocol | In-Process Event Bus (MediatR) |
| Method | Handle(GameEndedNotification notification) |
| Parameters | finalGameState: The complete game state object at ... |
| Authentication | N/A |
| Error Handling | Logs handler exceptions; no retry. |
| Performance | Should be < 10ms. |

#### 2.5.1.11 Nested Interactions

- {'sourceId': 'REPO-AS-005', 'targetId': 'REPO-AS-005', 'message': '1.1. Transform final GameState into GameResult DTO', 'sequenceNumber': 2, 'type': 'Data Transformation', 'isSynchronous': True, 'returnMessage': 'gameResult: A DTO containing aggregated results (winner, duration, net worths, etc.).', 'hasReturn': True, 'isActivation': False, 'technicalDetails': {'protocol': 'In-Process', 'method': 'MapToGameResult(GameState state)', 'parameters': 'state: GameState', 'authentication': 'N/A', 'errorHandling': 'Standard null checks.', 'performance': 'Should be negligible.'}}

### 2.5.2.0 Repository Method Invocation

#### 2.5.2.1 Source Id

REPO-AS-005

#### 2.5.2.2 Target Id

REPO-IP-ST-009

#### 2.5.2.3 Message

2. UpdatePlayerStatisticsAsync(gameResult)

#### 2.5.2.4 Sequence Number

3

#### 2.5.2.5 Type

🔹 Repository Method Invocation

#### 2.5.2.6 Is Synchronous

✅ Yes

#### 2.5.2.7 Return Message

Task (indicating completion)

#### 2.5.2.8 Has Return

✅ Yes

#### 2.5.2.9 Is Activation

✅ Yes

#### 2.5.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Method Call (DI) |
| Method | IStatisticsRepository.UpdatePlayerStatisticsAsync(... |
| Parameters | result: GameResult DTO |
| Authentication | N/A |
| Error Handling | Propagates DbException on connection or transactio... |
| Performance | Target latency < 500ms for the entire transaction. |

#### 2.5.2.11 Nested Interactions

##### 2.5.2.11.1 Transaction Management

###### 2.5.2.11.1.1 Source Id

REPO-IP-ST-009

###### 2.5.2.11.1.2 Target Id

REPO-IP-ST-009

###### 2.5.2.11.1.3 Message

2.1. Open connection and begin transaction

###### 2.5.2.11.1.4 Sequence Number

4

###### 2.5.2.11.1.5 Type

🔹 Transaction Management

###### 2.5.2.11.1.6 Is Synchronous

✅ Yes

###### 2.5.2.11.1.7 Return Message

dbTransaction

###### 2.5.2.11.1.8 Has Return

✅ Yes

###### 2.5.2.11.1.9 Is Activation

❌ No

###### 2.5.2.11.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Microsoft.Data.Sqlite |
| Method | connection.BeginTransaction(IsolationLevel.Seriali... |
| Parameters | IsolationLevel: Serializable to ensure atomicity a... |
| Authentication | N/A |
| Error Handling | Throws SqliteException if connection fails. |
| Performance | Connection pooling should be enabled by the provid... |

##### 2.5.2.11.2.0 Database Write

###### 2.5.2.11.2.1 Source Id

REPO-IP-ST-009

###### 2.5.2.11.2.2 Target Id

sqlite-db

###### 2.5.2.11.2.3 Message

2.2. UPDATE PlayerStatistics table for human player

###### 2.5.2.11.2.4 Sequence Number

5

###### 2.5.2.11.2.5 Type

🔹 Database Write

###### 2.5.2.11.2.6 Is Synchronous

✅ Yes

###### 2.5.2.11.2.7 Return Message

Rows Affected

###### 2.5.2.11.2.8 Has Return

✅ Yes

###### 2.5.2.11.2.9 Is Activation

❌ No

###### 2.5.2.11.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | ADO.NET Command |
| Method | UPDATE PlayerStatistics SET TotalGamesPlayed = Tot... |
| Parameters | @IsWin, @RentPaid, @RentCollected, @ProfileId deri... |
| Authentication | N/A |
| Error Handling | Handled by the transaction's catch block. |
| Performance | Query must use the primary key index on ProfileId. |

##### 2.5.2.11.3.0 Database Write

###### 2.5.2.11.3.1 Source Id

REPO-IP-ST-009

###### 2.5.2.11.3.2 Target Id

sqlite-db

###### 2.5.2.11.3.3 Message

2.3. INSERT record into GameHistory table

###### 2.5.2.11.3.4 Sequence Number

6

###### 2.5.2.11.3.5 Type

🔹 Database Write

###### 2.5.2.11.3.6 Is Synchronous

✅ Yes

###### 2.5.2.11.3.7 Return Message

New Row ID

###### 2.5.2.11.3.8 Has Return

✅ Yes

###### 2.5.2.11.3.9 Is Activation

❌ No

###### 2.5.2.11.3.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | ADO.NET Command |
| Method | INSERT INTO GameHistory (GameId, ProfileId, FinalN... |
| Parameters | All parameters derived from GameResult DTO. |
| Authentication | N/A |
| Error Handling | Handled by the transaction's catch block. |
| Performance | Simple insert, low impact. |

##### 2.5.2.11.4.0 Business Logic

###### 2.5.2.11.4.1 Source Id

REPO-IP-ST-009

###### 2.5.2.11.4.2 Target Id

REPO-IP-ST-009

###### 2.5.2.11.4.3 Message

2.4. [Conditional] Check if win qualifies for Top 10 Scores

###### 2.5.2.11.4.4 Sequence Number

7

###### 2.5.2.11.4.5 Type

🔹 Business Logic

###### 2.5.2.11.4.6 Is Synchronous

✅ Yes

###### 2.5.2.11.4.7 Return Message

isHighScore: boolean

###### 2.5.2.11.4.8 Has Return

✅ Yes

###### 2.5.2.11.4.9 Is Activation

❌ No

###### 2.5.2.11.4.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | SELECT COUNT(*), MIN(FinalNetWorth) FROM TopScores... |
| Parameters | Compares current score to the 10th highest score i... |
| Authentication | N/A |
| Error Handling | Handled by the transaction's catch block. |
| Performance | Query must use an index on ProfileId and FinalNetW... |

##### 2.5.2.11.5.0 Database Write

###### 2.5.2.11.5.1 Source Id

REPO-IP-ST-009

###### 2.5.2.11.5.2 Target Id

sqlite-db

###### 2.5.2.11.5.3 Message

2.5. [Conditional] Update TopScores table

###### 2.5.2.11.5.4 Sequence Number

8

###### 2.5.2.11.5.5 Type

🔹 Database Write

###### 2.5.2.11.5.6 Is Synchronous

✅ Yes

###### 2.5.2.11.5.7 Return Message

Rows Affected

###### 2.5.2.11.5.8 Has Return

✅ Yes

###### 2.5.2.11.5.9 Is Activation

❌ No

###### 2.5.2.11.5.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | ADO.NET Command |
| Method | DELETE FROM TopScores WHERE ProfileId = @ProfileId... |
| Parameters | Logic to remove the lowest score and insert the ne... |
| Authentication | N/A |
| Error Handling | Handled by the transaction's catch block. |
| Performance | DELETE/INSERT operations are efficient. |

##### 2.5.2.11.6.0 Transaction Management

###### 2.5.2.11.6.1 Source Id

REPO-IP-ST-009

###### 2.5.2.11.6.2 Target Id

sqlite-db

###### 2.5.2.11.6.3 Message

2.6. Commit Transaction

###### 2.5.2.11.6.4 Sequence Number

9

###### 2.5.2.11.6.5 Type

🔹 Transaction Management

###### 2.5.2.11.6.6 Is Synchronous

✅ Yes

###### 2.5.2.11.6.7 Return Message

Success

###### 2.5.2.11.6.8 Has Return

✅ Yes

###### 2.5.2.11.6.9 Is Activation

❌ No

###### 2.5.2.11.6.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Microsoft.Data.Sqlite |
| Method | dbTransaction.Commit() |
| Parameters | None |
| Authentication | N/A |
| Error Handling | Throws InvalidOperationException if transaction is... |
| Performance | Should be very fast. |

### 2.5.3.0.0.0 Method Return

#### 2.5.3.1.0.0 Source Id

REPO-IP-ST-09

#### 2.5.3.2.0.0 Target Id

REPO-AS-005

#### 2.5.3.3.0.0 Message

3. Return Success

#### 2.5.3.4.0.0 Sequence Number

10

#### 2.5.3.5.0.0 Type

🔹 Method Return

#### 2.5.3.6.0.0 Is Synchronous

✅ Yes

#### 2.5.3.7.0.0 Return Message

void

#### 2.5.3.8.0.0 Has Return

❌ No

#### 2.5.3.9.0.0 Is Activation

❌ No

#### 2.5.3.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method |  |
| Parameters |  |
| Authentication | N/A |
| Error Handling |  |
| Performance |  |

## 2.6.0.0.0.0 Notes

### 2.6.1.0.0.0 Content

#### 2.6.1.1.0.0 Content

The entire database operation (steps 2.1-2.6) MUST be wrapped in a try/catch block. The `catch` block must execute `dbTransaction.Rollback()` to ensure atomicity. A `finally` block should ensure the database connection is closed.

#### 2.6.1.2.0.0 Position

bottom-right

#### 2.6.1.3.0.0 Participant Id

REPO-IP-ST-009

#### 2.6.1.4.0.0 Sequence Number

4

### 2.6.2.0.0.0 Content

#### 2.6.2.1.0.0 Content

This flow exemplifies the Unit of Work pattern, where the repository method `UpdatePlayerStatisticsAsync` defines the boundary for a single, atomic business transaction.

#### 2.6.2.2.0.0 Position

top-left

#### 2.6.2.3.0.0 Participant Id

*Not specified*

#### 2.6.2.4.0.0 Sequence Number

3

## 2.7.0.0.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | As per REQ-1-098 and REQ-1-100, the SQLite databas... |
| Performance Targets | The entire transaction, from beginning to commit, ... |
| Error Handling Strategy | If any `SqliteException` (or general `DbException`... |
| Testing Considerations | Integration tests are critical. Use the `Microsoft... |
| Monitoring Requirements | Log transaction start at the INFO level. Log succe... |
| Deployment Considerations | The application's data migration logic (REQ-1-090)... |

