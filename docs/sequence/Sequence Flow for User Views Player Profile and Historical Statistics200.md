# 1 Overview

## 1.1 Diagram Id

SEQ-DF-003

## 1.2 Name

User Views Player Profile and Historical Statistics

## 1.3 Description

From the main menu, the user navigates to a 'Player Stats' screen. The application retrieves the human player's persistent profile and all historical gameplay statistics from the local SQLite database and displays them.

## 1.4 Type

🔹 DataFlow

## 1.5 Purpose

To provide the player with feedback on their long-term performance and progress, enhancing replayability as per REQ-1-006 and REQ-1-033.

## 1.6 Complexity

Medium

## 1.7 Priority

🟡 Medium

## 1.8 Frequency

OnDemand

## 1.9 Participants

- REPO-PRES-001
- REPO-AS-005
- REPO-IL-006
- REPO-AA-004

## 1.10 Key Interactions

- User clicks 'Player Stats' in the main menu.
- Presentation Layer requests stats from the Application Services Layer.
- Application Services Layer calls methods like GetPlayerStatsAsync and GetTopScoresAsync on the IStatisticsRepository interface.
- The Infrastructure Layer's StatisticsRepository implementation queries the SQLite database for all stats associated with the player's profile_id.
- The retrieved data is packaged into DTOs and returned to the Presentation Layer.
- The UI is populated with the historical data (total wins, average duration, etc.).

## 1.11 Triggers

- User navigates to the statistics viewing screen.

## 1.12 Outcomes

- The user is presented with a detailed view of their persistent gameplay statistics and top scores.

## 1.13 Business Rules

- Statistics must be linked to the player's persistent profile_id (REQ-1-033).
- Top scores are sorted by final net worth (REQ-1-091).

## 1.14 Error Scenarios

- The statistics database is corrupted and cannot be read.
- No statistics exist for the current player profile.

## 1.15 Integration Points

- SQLite Database

# 2.0 Details

## 2.1 Diagram Id

SEQ-DF-003

## 2.2 Name

Implementation: Retrieve and Display Player Historical Statistics

## 2.3 Description

Provides a detailed technical sequence for retrieving and displaying a player's historical statistics from a local SQLite database. The sequence is initiated by a user action in the Presentation Layer, orchestrated by the Application Services Layer, and fulfilled by the Infrastructure Layer, adhering to the Repository pattern. Data is mapped to DTOs for UI consumption, and specific database error conditions are handled gracefully.

## 2.4 Participants

### 2.4.1 UI Controller/View

#### 2.4.1.1 Repository Id

REPO-PRES-001

#### 2.4.1.2 Display Name

Presentation Layer (UI)

#### 2.4.1.3 Type

🔹 UI Controller/View

#### 2.4.1.4 Technology

Unity Engine, C#

#### 2.4.1.5 Order

1

#### 2.4.1.6 Style

| Property | Value |
|----------|-------|
| Shape | actor |
| Color | #4CAF50 |
| Stereotype | <<Unity MonoBehaviour>> |

### 2.4.2.0 Service Orchestrator

#### 2.4.2.1 Repository Id

REPO-AS-005

#### 2.4.2.2 Display Name

Application Services Layer

#### 2.4.2.3 Type

🔹 Service Orchestrator

#### 2.4.2.4 Technology

.NET 8, C#

#### 2.4.2.5 Order

2

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #2196F3 |
| Stereotype | <<Service>> |

### 2.4.3.0 Data Repository

#### 2.4.3.1 Repository Id

REPO-IL-006

#### 2.4.3.2 Display Name

Infrastructure Layer

#### 2.4.3.3 Type

🔹 Data Repository

#### 2.4.3.4 Technology

.NET 8, C#, Microsoft.Data.Sqlite

#### 2.4.3.5 Order

3

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | database |
| Color | #9C27B0 |
| Stereotype | <<Repository>> |

## 2.5.0.0 Interactions

### 2.5.1.0 User Input

#### 2.5.1.1 Source Id

REPO-PRES-001

#### 2.5.1.2 Target Id

REPO-PRES-001

#### 2.5.1.3 Message

User clicks 'Player Stats' button on the main menu.

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

*No items available*

##### 2.5.1.10.4 Authentication

N/A

##### 2.5.1.10.5 Error Handling

UI button is disabled if no player profile exists.

##### 2.5.1.10.6 Performance

###### 2.5.1.10.6.1 Latency

<50ms response time

### 2.5.2.0.0.0 Method Call

#### 2.5.2.1.0.0 Source Id

REPO-PRES-001

#### 2.5.2.2.0.0 Target Id

REPO-AS-005

#### 2.5.2.3.0.0 Message

Request player statistics for the current profile.

#### 2.5.2.4.0.0 Sequence Number

2

#### 2.5.2.5.0.0 Type

🔹 Method Call

#### 2.5.2.6.0.0 Is Synchronous

✅ Yes

#### 2.5.2.7.0.0 Return Message

Returns PlayerStatisticsDto containing aggregated stats and top scores, or null if not found.

#### 2.5.2.8.0.0 Has Return

✅ Yes

#### 2.5.2.9.0.0 Is Activation

✅ Yes

#### 2.5.2.10.0.0 Technical Details

##### 2.5.2.10.1.0 Protocol

In-Process

##### 2.5.2.10.2.0 Method

Task<PlayerStatisticsDto> PlayerStatsService.GetPlayerStatisticsAsync(Guid profileId)

##### 2.5.2.10.3.0 Parameters

- {'name': 'profileId', 'type': 'Guid', 'description': "The unique ID of the human player's profile."}

##### 2.5.2.10.4.0 Authentication

N/A

##### 2.5.2.10.5.0 Error Handling

Catches InfrastructureException from lower layers and translates it to a UI-friendly error state.

##### 2.5.2.10.6.0 Performance

###### 2.5.2.10.6.1 Latency

Expected completion < 200ms

### 2.5.3.0.0.0 Interface Method Call

#### 2.5.3.1.0.0 Source Id

REPO-AS-005

#### 2.5.3.2.0.0 Target Id

REPO-IL-006

#### 2.5.3.3.0.0 Message

Invoke repository to fetch statistics data from the database.

#### 2.5.3.4.0.0 Sequence Number

3

#### 2.5.3.5.0.0 Type

🔹 Interface Method Call

#### 2.5.3.6.0.0 Is Synchronous

✅ Yes

#### 2.5.3.7.0.0 Return Message

Returns a data transfer object with player statistics.

#### 2.5.3.8.0.0 Has Return

✅ Yes

#### 2.5.3.9.0.0 Is Activation

✅ Yes

#### 2.5.3.10.0.0 Technical Details

##### 2.5.3.10.1.0 Protocol

In-Process (Dependency Injection)

##### 2.5.3.10.2.0 Method

Task<PlayerStatisticsDto> IStatisticsRepository.GetPlayerStatsAsync(Guid profileId)

##### 2.5.3.10.3.0 Parameters

- {'name': 'profileId', 'type': 'Guid', 'description': 'The player profile ID used in the WHERE clause of the SQL query.'}

##### 2.5.3.10.4.0 Authentication

N/A

##### 2.5.3.10.5.0 Error Handling

The concrete implementation will catch SqliteException and wrap it in a custom InfrastructureException.

##### 2.5.3.10.6.0 Performance

###### 2.5.3.10.6.1 Latency

Database query execution target < 100ms

### 2.5.4.0.0.0 Database Query

#### 2.5.4.1.0.0 Source Id

REPO-IL-006

#### 2.5.4.2.0.0 Target Id

REPO-IL-006

#### 2.5.4.3.0.0 Message

Execute SQL queries against local SQLite database.

#### 2.5.4.4.0.0 Sequence Number

4

#### 2.5.4.5.0.0 Type

🔹 Database Query

#### 2.5.4.6.0.0 Is Synchronous

✅ Yes

#### 2.5.4.7.0.0 Return Message

Returns database rows for stats and top scores.

#### 2.5.4.8.0.0 Has Return

✅ Yes

#### 2.5.4.9.0.0 Is Activation

✅ Yes

#### 2.5.4.10.0.0 Technical Details

##### 2.5.4.10.1.0 Protocol

Microsoft.Data.Sqlite

##### 2.5.4.10.2.0 Method

ExecuteReaderAsync()

##### 2.5.4.10.3.0 Parameters

###### 2.5.4.10.3.1 SQL

####### 2.5.4.10.3.1.1 Name

Query 1 (Player Stats)

####### 2.5.4.10.3.1.2 Type

🔹 SQL

####### 2.5.4.10.3.1.3 Description

```sql
SELECT TotalGamesPlayed, TotalWins, ... FROM PlayerStatistics WHERE ProfileId = @profileId
```

###### 2.5.4.10.3.2.0 SQL

####### 2.5.4.10.3.2.1 Name

Query 2 (Top Scores)

####### 2.5.4.10.3.2.2 Type

🔹 SQL

####### 2.5.4.10.3.2.3 Description

```sql
SELECT ProfileName, FinalNetWorth, GameDuration, TotalTurns FROM GameResults WHERE ProfileId = @profileId AND IsWin = 1 ORDER BY FinalNetWorth DESC LIMIT 10
```

##### 2.5.4.10.4.0.0 Authentication

N/A

##### 2.5.4.10.5.0.0 Error Handling

Handles connection failures or query errors by throwing a SqliteException.

##### 2.5.4.10.6.0.0 Performance

###### 2.5.4.10.6.1.0 Notes

An index on PlayerStatistics(ProfileId) and GameResults(ProfileId, IsWin, FinalNetWorth) is critical for performance.

### 2.5.5.0.0.0.0 Data Transformation

#### 2.5.5.1.0.0.0 Source Id

REPO-IL-006

#### 2.5.5.2.0.0.0 Target Id

REPO-IL-006

#### 2.5.5.3.0.0.0 Message

Map database results to PlayerStatisticsDto.

#### 2.5.5.4.0.0.0 Sequence Number

5

#### 2.5.5.5.0.0.0 Type

🔹 Data Transformation

#### 2.5.5.6.0.0.0 Is Synchronous

✅ Yes

#### 2.5.5.7.0.0.0 Return Message



#### 2.5.5.8.0.0.0 Has Return

❌ No

#### 2.5.5.9.0.0.0 Is Activation

❌ No

#### 2.5.5.10.0.0.0 Technical Details

##### 2.5.5.10.1.0.0 Protocol

In-Process

##### 2.5.5.10.2.0.0 Method

Manual mapping from SqliteDataReader to DTOs

##### 2.5.5.10.3.0.0 Parameters

*No items available*

##### 2.5.5.10.4.0.0 Authentication

N/A

##### 2.5.5.10.5.0.0 Error Handling

If no rows are returned for the profile, a null or empty DTO is constructed to signify 'no stats found'.

##### 2.5.5.10.6.0.0 Performance

###### 2.5.5.10.6.1.0 Notes

Mapping should be highly efficient, avoiding reflection where possible.

### 2.5.6.0.0.0.0 UI Update

#### 2.5.6.1.0.0.0 Source Id

REPO-PRES-001

#### 2.5.6.2.0.0.0 Target Id

REPO-PRES-001

#### 2.5.6.3.0.0.0 Message

[Success Path] Populate UI fields with data from the returned PlayerStatisticsDto.

#### 2.5.6.4.0.0.0 Sequence Number

6

#### 2.5.6.5.0.0.0 Type

🔹 UI Update

#### 2.5.6.6.0.0.0 Is Synchronous

✅ Yes

#### 2.5.6.7.0.0.0 Return Message



#### 2.5.6.8.0.0.0 Has Return

❌ No

#### 2.5.6.9.0.0.0 Is Activation

❌ No

#### 2.5.6.10.0.0.0 Technical Details

##### 2.5.6.10.1.0.0 Protocol

UI Event

##### 2.5.6.10.2.0.0 Method

UpdateUI(PlayerStatisticsDto stats)

##### 2.5.6.10.3.0.0 Parameters

*No items available*

##### 2.5.6.10.4.0.0 Authentication

N/A

##### 2.5.6.10.5.0.0 Error Handling

If the DTO is empty/null (no stats found), display a message like 'No games played yet.'

##### 2.5.6.10.6.0.0 Performance

###### 2.5.6.10.6.1.0 Notes

UI update must occur on the main Unity thread.

### 2.5.7.0.0.0.0 UI Update

#### 2.5.7.1.0.0.0 Source Id

REPO-PRES-001

#### 2.5.7.2.0.0.0 Target Id

REPO-PRES-001

#### 2.5.7.3.0.0.0 Message

[Failure Path] Display a user-friendly error message.

#### 2.5.7.4.0.0.0 Sequence Number

7

#### 2.5.7.5.0.0.0 Type

🔹 UI Update

#### 2.5.7.6.0.0.0 Is Synchronous

✅ Yes

#### 2.5.7.7.0.0.0 Return Message



#### 2.5.7.8.0.0.0 Has Return

❌ No

#### 2.5.7.9.0.0.0 Is Activation

❌ No

#### 2.5.7.10.0.0.0 Technical Details

##### 2.5.7.10.1.0.0 Protocol

UI Event

##### 2.5.7.10.2.0.0 Method

ShowErrorState(string message)

##### 2.5.7.10.3.0.0 Parameters

- {'name': 'message', 'type': 'string', 'description': "e.g., 'Error: Could not load player statistics. The data file may be corrupted.'"}

##### 2.5.7.10.4.0.0 Authentication

N/A

##### 2.5.7.10.5.0.0 Error Handling

This is the final step in the error handling chain for this sequence.

##### 2.5.7.10.6.0.0 Performance

###### 2.5.7.10.6.1.0 Notes

Error display should not block the user from navigating back to the main menu.

## 2.6.0.0.0.0.0 Notes

### 2.6.1.0.0.0.0 Content

#### 2.6.1.1.0.0.0 Content

Dependency Injection: The Application Services Layer depends on the IStatisticsRepository interface (from REPO-AA-004), not the concrete implementation. At runtime, the DI container provides the StatisticsRepository instance from the Infrastructure Layer.

#### 2.6.1.2.0.0.0 Position

top-right

#### 2.6.1.3.0.0.0 Participant Id

REPO-AS-005

#### 2.6.1.4.0.0.0 Sequence Number

3

### 2.6.2.0.0.0.0 Content

#### 2.6.2.1.0.0.0 Content

Data Transfer Objects (DTOs): The repository is responsible for mapping raw database entities into a clean DTO (`PlayerStatisticsDto`) tailored for the UI's needs. This decouples the Presentation Layer from the database schema.

#### 2.6.2.2.0.0.0 Position

bottom-left

#### 2.6.2.3.0.0.0 Participant Id

REPO-IL-006

#### 2.6.2.4.0.0.0 Sequence Number

5

## 2.7.0.0.0.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | As the application is offline, security concerns a... |
| Performance Targets | The entire end-to-end sequence from user click to ... |
| Error Handling Strategy | If `StatisticsRepository` catches a `SqliteExcepti... |
| Testing Considerations | 1. **Unit Tests (REPO-AS-005):** The `PlayerStatsS... |
| Monitoring Requirements | The `PlayerStatsService` should log the initiation... |
| Deployment Considerations | The application installer must ensure the `%APPDAT... |

