# 1 Overview

## 1.1 Diagram Id

SEQ-BP-001

## 1.2 Name

Game Ends and Player Statistics are Updated

## 1.3 Description

When a game's win/loss condition is met, the game concludes. This business process orchestrates showing the final summary screen to the user while simultaneously persisting the game's results transactionally to the SQLite database to update the player's historical statistics.

## 1.4 Type

🔹 BusinessProcess

## 1.5 Purpose

To formally conclude a game session, provide the user with a performance summary, and persist their long-term progress to enhance replayability.

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
- REPO-IL-006
- REPO-AA-004

## 1.10 Key Interactions

- Domain Layer's RuleEngine detects a final bankruptcy and declares a winner.
- The Application Services Layer (GameSessionService) is notified of the game's end.
- GameSessionService calls the AddGameResultAsync method on the IStatisticsRepository interface.
- The Infrastructure Layer's StatisticsRepository implementation connects to the SQLite database.
- It begins a transaction, writes a new record for the completed game, and updates the player's aggregate historical stats (wins, duration, etc.). The transaction is then committed.
- In parallel, GameSessionService instructs the Presentation Layer to display the Game Summary screen with final stats from the match.

## 1.11 Triggers

- The last AI opponent goes bankrupt (human wins) or the human player goes bankrupt (human loses).

## 1.12 Outcomes

- The game session is terminated.
- The player's historical statistics in the SQLite database are updated (REQ-1-033).
- If it was a top-10 victory, the high score list is updated (REQ-1-091).
- The user is shown a detailed summary of the completed match (REQ-1-070).

## 1.13 Business Rules

- Win condition is being the last non-bankrupt player (REQ-1-068).
- Lose condition is the human player going bankrupt (REQ-1-069).
- All database updates for statistics must be atomic and transactional.

## 1.14 Error Scenarios

- Failed to write to the SQLite database due to corruption or permissions issues.

## 1.15 Integration Points

- SQLite Database

# 2.0 Details

## 2.1 Diagram Id

SEQ-BP-001

## 2.2 Name

Game End and Statistics Update Business Process

## 2.3 Description

A comprehensive technical sequence detailing the business process initiated when a game's win/loss condition is met. The sequence is orchestrated by the Application Services Layer, which coordinates a transactional database write to the Infrastructure Layer for persisting game results and updating player statistics, followed by a command to the Presentation Layer to display the final game summary screen. This ensures data integrity and decouples business logic from UI concerns.

## 2.4 Participants

### 2.4.1 Business Logic

#### 2.4.1.1 Repository Id

REPO-DM-001

#### 2.4.1.2 Display Name

Domain Layer

#### 2.4.1.3 Type

🔹 Business Logic

#### 2.4.1.4 Technology

.NET 8, C#

#### 2.4.1.5 Order

1

#### 2.4.1.6 Style

| Property | Value |
|----------|-------|
| Shape | actor |
| Color | #F8B400 |
| Stereotype | RuleEngine |

### 2.4.2.0 Application Services

#### 2.4.2.1 Repository Id

REPO-AS-005

#### 2.4.2.2 Display Name

Application Services Layer

#### 2.4.2.3 Type

🔹 Application Services

#### 2.4.2.4 Technology

.NET 8, C#

#### 2.4.2.5 Order

2

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #1E90FF |
| Stereotype | GameSessionService |

### 2.4.3.0 Infrastructure

#### 2.4.3.1 Repository Id

REPO-IL-006

#### 2.4.3.2 Display Name

Infrastructure Layer

#### 2.4.3.3 Type

🔹 Infrastructure

#### 2.4.3.4 Technology

.NET 8, C#, Microsoft.Data.Sqlite

#### 2.4.3.5 Order

3

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | database |
| Color | #90EE90 |
| Stereotype | StatisticsRepository |

### 2.4.4.0 Presentation

#### 2.4.4.1 Repository Id

REPO-PRES-001

#### 2.4.4.2 Display Name

Presentation Layer

#### 2.4.4.3 Type

🔹 Presentation

#### 2.4.4.4 Technology

Unity Engine, C#

#### 2.4.4.5 Order

4

#### 2.4.4.6 Style

| Property | Value |
|----------|-------|
| Shape | boundary |
| Color | #FF6347 |
| Stereotype | ViewManager |

## 2.5.0.0 Interactions

### 2.5.1.0 Method Call

#### 2.5.1.1 Source Id

REPO-AS-005

#### 2.5.1.2 Target Id

REPO-DM-001

#### 2.5.1.3 Message

RuleEngine processes player bankruptcy, which fulfills the game's win/loss condition.

#### 2.5.1.4 Sequence Number

1

#### 2.5.1.5 Type

🔹 Method Call

#### 2.5.1.6 Is Synchronous

✅ Yes

#### 2.5.1.7 Return Message

Returns GameEndResult DTO containing winner, final player states, and game duration.

#### 2.5.1.8 Has Return

✅ Yes

#### 2.5.1.9 Is Activation

✅ Yes

#### 2.5.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-process |
| Method | ruleEngine.ProcessPlayerAction(action) |
| Parameters | PlayerAction object leading to final bankruptcy. |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Must execute within a single frame (<16ms). |

### 2.5.2.0 Self Call

#### 2.5.2.1 Source Id

REPO-AS-005

#### 2.5.2.2 Target Id

REPO-AS-005

#### 2.5.2.3 Message

GameSessionService receives GameEndResult and initiates the game finalization process.

#### 2.5.2.4 Sequence Number

2

#### 2.5.2.5 Type

🔹 Self Call

#### 2.5.2.6 Is Synchronous

✅ Yes

#### 2.5.2.7 Return Message



#### 2.5.2.8 Has Return

❌ No

#### 2.5.2.9 Is Activation

✅ Yes

#### 2.5.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-process |
| Method | self.handleGameEnd(gameEndResult) |
| Parameters | GameEndResult DTO |
| Authentication | N/A |
| Error Handling | Logs error if DTO is null or invalid. |
| Performance | N/A |

### 2.5.3.0 Async Method Call

#### 2.5.3.1 Source Id

REPO-AS-005

#### 2.5.3.2 Target Id

REPO-IL-006

#### 2.5.3.3 Message

Invokes persistence logic via IStatisticsRepository interface (defined in REPO-AA-004).

#### 2.5.3.4 Sequence Number

3

#### 2.5.3.5 Type

🔹 Async Method Call

#### 2.5.3.6 Is Synchronous

❌ No

#### 2.5.3.7 Return Message

Task completes, indicating success or failure of the database transaction.

#### 2.5.3.8 Has Return

✅ Yes

#### 2.5.3.9 Is Activation

✅ Yes

#### 2.5.3.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-process (DI) |
| Method | IStatisticsRepository.AddGameResultAsync(gameResul... |
| Parameters | GameResult and PlayerStats DTOs derived from GameE... |
| Authentication | N/A |
| Error Handling | Caller must await the Task and handle potential ex... |
| Performance | Target < 500ms on recommended hardware with SSD. |

#### 2.5.3.11 Nested Interactions

##### 2.5.3.11.1 Database Operation

###### 2.5.3.11.1.1 Source Id

REPO-IL-006

###### 2.5.3.11.1.2 Target Id

REPO-IL-006

###### 2.5.3.11.1.3 Message

Begins a new SQLite transaction to ensure atomicity of all subsequent writes.

###### 2.5.3.11.1.4 Sequence Number

3.1

###### 2.5.3.11.1.5 Type

🔹 Database Operation

###### 2.5.3.11.1.6 Is Synchronous

✅ Yes

###### 2.5.3.11.1.7 Return Message



###### 2.5.3.11.1.8 Has Return

❌ No

###### 2.5.3.11.1.9 Is Activation

❌ No

###### 2.5.3.11.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Microsoft.Data.Sqlite |
| Method | connection.BeginTransaction() |
| Parameters |  |
| Authentication | N/A |
| Error Handling | A try-finally block is initiated to ensure transac... |
| Performance | Low overhead. |

##### 2.5.3.11.2.0 Database Operation

###### 2.5.3.11.2.1 Source Id

REPO-IL-006

###### 2.5.3.11.2.2 Target Id

REPO-IL-006

###### 2.5.3.11.2.3 Message

Inserts a new record into the GameResults table with summary data.

###### 2.5.3.11.2.4 Sequence Number

3.2

###### 2.5.3.11.2.5 Type

🔹 Database Operation

###### 2.5.3.11.2.6 Is Synchronous

✅ Yes

###### 2.5.3.11.2.7 Return Message



###### 2.5.3.11.2.8 Has Return

❌ No

###### 2.5.3.11.2.9 Is Activation

❌ No

###### 2.5.3.11.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | SQL |
| Method | ExecuteNonQueryAsync("INSERT INTO GameResults...") |
| Parameters | SQL parameters for winnerId, duration, turnCount, ... |
| Authentication | N/A |
| Error Handling | Any SqliteException will be caught by the outer ha... |
| Performance | N/A |

##### 2.5.3.11.3.0 Database Operation

###### 2.5.3.11.3.1 Source Id

REPO-IL-006

###### 2.5.3.11.3.2 Target Id

REPO-IL-006

###### 2.5.3.11.3.3 Message

Updates the aggregate PlayerStatistics table for the human player.

###### 2.5.3.11.3.4 Sequence Number

3.3

###### 2.5.3.11.3.5 Type

🔹 Database Operation

###### 2.5.3.11.3.6 Is Synchronous

✅ Yes

###### 2.5.3.11.3.7 Return Message



###### 2.5.3.11.3.8 Has Return

❌ No

###### 2.5.3.11.3.9 Is Activation

❌ No

###### 2.5.3.11.3.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | SQL |
| Method | ExecuteNonQueryAsync("UPDATE PlayerStatistics SET ... |
| Parameters | SQL parameters for win/loss increment, total rent,... |
| Authentication | N/A |
| Error Handling | Any SqliteException will be caught by the outer ha... |
| Performance | N/A |

##### 2.5.3.11.4.0 Database Operation

###### 2.5.3.11.4.1 Source Id

REPO-IL-006

###### 2.5.3.11.4.2 Target Id

REPO-IL-006

###### 2.5.3.11.4.3 Message

Commits the transaction, making all data changes permanent.

###### 2.5.3.11.4.4 Sequence Number

3.4

###### 2.5.3.11.4.5 Type

🔹 Database Operation

###### 2.5.3.11.4.6 Is Synchronous

✅ Yes

###### 2.5.3.11.4.7 Return Message



###### 2.5.3.11.4.8 Has Return

❌ No

###### 2.5.3.11.4.9 Is Activation

❌ No

###### 2.5.3.11.4.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Microsoft.Data.Sqlite |
| Method | transaction.Commit() |
| Parameters |  |
| Authentication | N/A |
| Error Handling | Occurs within the try block. If this fails, the ca... |
| Performance | N/A |

### 2.5.4.0.0.0 Command

#### 2.5.4.1.0.0 Source Id

REPO-AS-005

#### 2.5.4.2.0.0 Target Id

REPO-PRES-001

#### 2.5.4.3.0.0 Message

Commands the ViewManager to display the Game Summary screen with final stats.

#### 2.5.4.4.0.0 Sequence Number

4

#### 2.5.4.5.0.0 Type

🔹 Command

#### 2.5.4.6.0.0 Is Synchronous

✅ Yes

#### 2.5.4.7.0.0 Return Message



#### 2.5.4.8.0.0 Has Return

❌ No

#### 2.5.4.9.0.0 Is Activation

✅ Yes

#### 2.5.4.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-process |
| Method | viewManager.ShowGameSummary(gameSummaryData) |
| Parameters | GameSummaryData DTO, containing stats from the com... |
| Authentication | N/A |
| Error Handling | The Presentation Layer is responsible for handling... |
| Performance | UI transition must start immediately. |

## 2.6.0.0.0.0 Notes

### 2.6.1.0.0.0 Content

#### 2.6.1.1.0.0 Content

Business Rule Enforcement: The atomicity of the statistics update is guaranteed by wrapping all database write operations within a single SQLite transaction (Interactions 3.1-3.4).

#### 2.6.1.2.0.0 Position

bottom

#### 2.6.1.3.0.0 Participant Id

REPO-IL-006

#### 2.6.1.4.0.0 Sequence Number

3

### 2.6.2.0.0.0 Content

#### 2.6.2.1.0.0 Content

Architectural Pattern: The Application Service Layer (REPO-AS-005) acts as an orchestrator, decoupling the Domain Layer's rule execution from both data persistence (REPO-IL-006) and user interface updates (REPO-PRES-001).

#### 2.6.2.2.0.0 Position

top

#### 2.6.2.3.0.0 Participant Id

REPO-AS-005

#### 2.6.2.4.0.0 Sequence Number

2

### 2.6.3.0.0.0 Content

#### 2.6.3.1.0.0 Content

Audit Trail: The successful completion of interaction 3.2 creates an auditable record of the completed game in the database, fulfilling a key requirement of the business process.

#### 2.6.3.2.0.0 Position

right

#### 2.6.3.3.0.0 Participant Id

REPO-IL-006

#### 2.6.3.4.0.0 Sequence Number

3.2

## 2.7.0.0.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | N/A. The application is entirely offline and local... |
| Performance Targets | The entire business process from game end detectio... |
| Error Handling Strategy | If any database operation within the transaction (... |
| Testing Considerations | 1. Integration tests are required to validate the ... |
| Monitoring Requirements | 1. A structured log entry at the INFO level should... |
| Deployment Considerations | The application must ensure that the SQLite databa... |

