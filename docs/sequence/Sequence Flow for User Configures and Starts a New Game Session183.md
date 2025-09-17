# 1 Overview

## 1.1 Diagram Id

SEQ-UJ-001

## 1.2 Name

User Configures and Starts a New Game Session

## 1.3 Description

A user launches the application, looks up or creates a player profile, configures a new game by selecting the number of AI opponents and their difficulty, chooses a token, and starts the game session. This sequence translates the user's setup choices into the initial state of the core Domain objects.

## 1.4 Type

🔹 UserJourney

## 1.5 Purpose

To allow the user to configure and begin a new game of Monopoly Tycoon, fulfilling the core use case of the application.

## 1.6 Complexity

Medium

## 1.7 Priority

🚨 Critical

## 1.8 Frequency

OnDemand

## 1.9 Participants

- REPO-PRES-001
- REPO-AS-005
- REPO-DM-001
- REPO-IL-006
- REPO-AA-004

## 1.10 Key Interactions

- User provides player name and selects token/AI configuration in Presentation Layer.
- Presentation Layer calls Application Services Layer (GameSessionService) to initiate a new game.
- GameSessionService requests player profile from Infrastructure (StatisticsRepository) to see if a profile exists.
- GameSessionService creates a new GameState object in the Domain Layer, populating it based on user configuration.
- Domain Layer initializes player states, board state, and card decks according to official rules.
- GameSessionService signals the Presentation Layer to transition to the main game board scene.

## 1.11 Triggers

- User clicks the 'Start Game' button on the game setup screen.

## 1.12 Outcomes

- A new game session is created and initialized.
- The user is presented with the main game board, ready for the first player's turn.

## 1.13 Business Rules

- A game must consist of 1 human and 1 to 3 AI opponents (REQ-1-030).
- Each AI opponent must have a difficulty level assigned (REQ-1-030).
- Player profile name must be validated (3-16 chars, no special characters) (REQ-1-032).

## 1.14 Error Scenarios

- Invalid player name is entered.
- Failure to load initial game assets.

## 1.15 Integration Points

*No items available*

# 2.0 Details

## 2.1 Diagram Id

SEQ-UJ-001-IMPL

## 2.2 Name

User Configures and Starts a New Game Session

## 2.3 Description

A comprehensive technical sequence detailing the user journey from the game setup screen to the start of a new game. This diagram specifies the interactions between the Presentation (Unity), Application Services, Domain, and Infrastructure layers, including user input validation, state object creation, data persistence, and scene transition management.

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

0

#### 2.4.1.6 Style

| Property | Value |
|----------|-------|
| Shape | actor |
| Color | #E6E6E6 |
| Stereotype | Human Actor |

### 2.4.2.0 UI Layer

#### 2.4.2.1 Repository Id

REPO-PRES-001

#### 2.4.2.2 Display Name

Presentation Layer

#### 2.4.2.3 Type

🔹 UI Layer

#### 2.4.2.4 Technology

Unity Engine, C#

#### 2.4.2.5 Order

1

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | boundary |
| Color | #B3E5FC |
| Stereotype | Unity Scene Controller (GameSetupController) |

### 2.4.3.0 Service Layer

#### 2.4.3.1 Repository Id

REPO-AS-005

#### 2.4.3.2 Display Name

Application Services Layer

#### 2.4.3.3 Type

🔹 Service Layer

#### 2.4.3.4 Technology

.NET 8, C#

#### 2.4.3.5 Order

2

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | control |
| Color | #C8E6C9 |
| Stereotype | GameSessionService |

### 2.4.4.0 Infrastructure Layer

#### 2.4.4.1 Repository Id

REPO-IL-006

#### 2.4.4.2 Display Name

Infrastructure Layer

#### 2.4.4.3 Type

🔹 Infrastructure Layer

#### 2.4.4.4 Technology

Microsoft.Data.Sqlite

#### 2.4.4.5 Order

3

#### 2.4.4.6 Style

| Property | Value |
|----------|-------|
| Shape | database |
| Color | #FFECB3 |
| Stereotype | StatisticsRepository (SQLite) |

### 2.4.5.0 Domain Layer

#### 2.4.5.1 Repository Id

REPO-DM-001

#### 2.4.5.2 Display Name

Domain Layer

#### 2.4.5.3 Type

🔹 Domain Layer

#### 2.4.5.4 Technology

.NET 8 POCOs

#### 2.4.5.5 Order

4

#### 2.4.5.6 Style

| Property | Value |
|----------|-------|
| Shape | entity |
| Color | #F8BBD0 |
| Stereotype | GameState |

## 2.5.0.0 Interactions

### 2.5.1.0 User Input

#### 2.5.1.1 Source Id

User

#### 2.5.1.2 Target Id

REPO-PRES-001

#### 2.5.1.3 Message

Enters player name and selects game configuration (AI count, difficulties, token).

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
| Method | GameSetupController.OnConfigurationUpdated(GameSet... |
| Parameters | ViewModel reflects UI state, including player name... |
| Authentication | N/A |
| Error Handling | Real-time validation feedback is provided for the ... |
| Performance | UI must respond to input changes in <50ms. |

### 2.5.2.0 User Action

#### 2.5.2.1 Source Id

User

#### 2.5.2.2 Target Id

REPO-PRES-001

#### 2.5.2.3 Message

Clicks 'Start Game' button.

#### 2.5.2.4 Sequence Number

2

#### 2.5.2.5 Type

🔹 User Action

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
| Protocol | UI Event |
| Method | GameSetupController.OnStartGameClicked() |
| Parameters | None. |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | N/A |

### 2.5.3.0 Validation

#### 2.5.3.1 Source Id

REPO-PRES-001

#### 2.5.3.2 Target Id

REPO-PRES-001

#### 2.5.3.3 Message

ValidateGameConfiguration(viewModel)

#### 2.5.3.4 Sequence Number

3

#### 2.5.3.5 Type

🔹 Validation

#### 2.5.3.6 Is Synchronous

✅ Yes

#### 2.5.3.7 Return Message

bool isValid

#### 2.5.3.8 Has Return

✅ Yes

#### 2.5.3.9 Is Activation

❌ No

#### 2.5.3.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Internal Method Call |
| Method | private bool ValidateGameConfiguration(GameSetupVi... |
| Parameters | Current UI ViewModel state. |
| Authentication | N/A |
| Error Handling | If validation fails, an error message is displayed... |
| Performance | Validation logic must execute in <10ms. |

### 2.5.4.0 UI State Change

#### 2.5.4.1 Source Id

REPO-PRES-001

#### 2.5.4.2 Target Id

REPO-PRES-001

#### 2.5.4.3 Message

DisplayLoadingOverlay('Creating game...')

#### 2.5.4.4 Sequence Number

4

#### 2.5.4.5 Type

🔹 UI State Change

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
| Protocol | Internal Method Call |
| Method | UIManager.ShowLoadingScreen(string message) |
| Parameters | Message to display to the user. |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Loading screen must appear within one frame. |

### 2.5.5.0 Service Request

#### 2.5.5.1 Source Id

REPO-PRES-001

#### 2.5.5.2 Target Id

REPO-AS-005

#### 2.5.5.3 Message

StartNewGameAsync(gameSetupDto)

#### 2.5.5.4 Sequence Number

5

#### 2.5.5.5 Type

🔹 Service Request

#### 2.5.5.6 Is Synchronous

✅ Yes

#### 2.5.5.7 Return Message

Task<StartGameResult>

#### 2.5.5.8 Has Return

✅ Yes

#### 2.5.5.9 Is Activation

✅ Yes

#### 2.5.5.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Memory Method Call |
| Method | Task<StartGameResult> IGameSessionService.StartNew... |
| Parameters | GameSetupDto { string PlayerName, string TokenId, ... |
| Authentication | N/A |
| Error Handling | Handles ApplicationException from the service laye... |
| Performance | This call is asynchronous to prevent blocking the ... |

### 2.5.6.0 Data Request

#### 2.5.6.1 Source Id

REPO-AS-005

#### 2.5.6.2 Target Id

REPO-IL-006

#### 2.5.6.3 Message

IStatisticsRepository.GetOrCreateProfileAsync(playerName)

#### 2.5.6.4 Sequence Number

6

#### 2.5.6.5 Type

🔹 Data Request

#### 2.5.6.6 Is Synchronous

✅ Yes

#### 2.5.6.7 Return Message

Task<PlayerProfile>

#### 2.5.6.8 Has Return

✅ Yes

#### 2.5.6.9 Is Activation

✅ Yes

#### 2.5.6.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Memory Method Call (via DI) |
| Method | Task<PlayerProfile> IStatisticsRepository.GetOrCre... |
| Parameters | Player's profile name from the DTO. |
| Authentication | N/A |
| Error Handling | Catches InfrastructureException from the repositor... |
| Performance | Expected latency < 50ms on an SSD. |

#### 2.5.6.11 Nested Interactions

##### 2.5.6.11.1 Database Query

###### 2.5.6.11.1.1 Source Id

REPO-IL-006

###### 2.5.6.11.1.2 Target Id

REPO-IL-006

###### 2.5.6.11.1.3 Message

```sql
SELECT * FROM PlayerProfiles WHERE Name = @name
```

###### 2.5.6.11.1.4 Sequence Number

6.1

###### 2.5.6.11.1.5 Type

🔹 Database Query

###### 2.5.6.11.1.6 Is Synchronous

✅ Yes

###### 2.5.6.11.1.7 Return Message

DbDataReader (0 or 1 row)

###### 2.5.6.11.1.8 Has Return

✅ Yes

###### 2.5.6.11.1.9 Is Activation

❌ No

###### 2.5.6.11.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | SQLite Connection |
| Method | DbCommand.ExecuteReaderAsync() |
| Parameters | SQL query with parameterized name. |
| Authentication | N/A |
| Error Handling | Handles SQLiteException (e.g., DB file locked, cor... |
| Performance | Query should be indexed on the 'Name' column for f... |

##### 2.5.6.11.2.0 Database Command

###### 2.5.6.11.2.1 Source Id

REPO-IL-006

###### 2.5.6.11.2.2 Target Id

REPO-IL-006

###### 2.5.6.11.2.3 Message

[IF NOT EXISTS] INSERT INTO PlayerProfiles ...

###### 2.5.6.11.2.4 Sequence Number

6.2

###### 2.5.6.11.2.5 Type

🔹 Database Command

###### 2.5.6.11.2.6 Is Synchronous

✅ Yes

###### 2.5.6.11.2.7 Return Message

int rowsAffected

###### 2.5.6.11.2.8 Has Return

✅ Yes

###### 2.5.6.11.2.9 Is Activation

❌ No

###### 2.5.6.11.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | SQLite Connection |
| Method | DbCommand.ExecuteNonQueryAsync() |
| Parameters | SQL INSERT statement with a new Profile ID (GUID) ... |
| Authentication | N/A |
| Error Handling | Handles SQLiteException and throws InfrastructureE... |
| Performance | N/A |

### 2.5.7.0.0.0 Object Creation

#### 2.5.7.1.0.0 Source Id

REPO-AS-005

#### 2.5.7.2.0.0 Target Id

REPO-DM-001

#### 2.5.7.3.0.0 Message

new GameState(gameSetupDto, playerProfile)

#### 2.5.7.4.0.0 Sequence Number

7

#### 2.5.7.5.0.0 Type

🔹 Object Creation

#### 2.5.7.6.0.0 Is Synchronous

✅ Yes

#### 2.5.7.7.0.0 Return Message

GameState instance

#### 2.5.7.8.0.0 Has Return

✅ Yes

#### 2.5.7.9.0.0 Is Activation

✅ Yes

#### 2.5.7.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Object Instantiation |
| Method | GameState(GameSetupDto setup, PlayerProfile profil... |
| Parameters | The user's configuration and the retrieved or newl... |
| Authentication | N/A |
| Error Handling | The constructor enforces domain invariants (e.g., ... |
| Performance | Initialization should be near-instantaneous. |

### 2.5.8.0.0.0 Internal Logic

#### 2.5.8.1.0.0 Source Id

REPO-DM-001

#### 2.5.8.2.0.0 Target Id

REPO-DM-001

#### 2.5.8.3.0.0 Message

InitializeGame()

#### 2.5.8.4.0.0 Sequence Number

8

#### 2.5.8.5.0.0 Type

🔹 Internal Logic

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
| Protocol | Internal Method Call |
| Method | private void InitializeGame() |
| Parameters | Internal state from the constructor. |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | This is where the bulk of the setup logic occurs: ... |

### 2.5.9.0.0.0 State Management

#### 2.5.9.1.0.0 Source Id

REPO-AS-005

#### 2.5.9.2.0.0 Target Id

REPO-AS-005

#### 2.5.9.3.0.0 Message

SetCurrentSession(gameState)

#### 2.5.9.4.0.0 Sequence Number

9

#### 2.5.9.5.0.0 Type

🔹 State Management

#### 2.5.9.6.0.0 Is Synchronous

✅ Yes

#### 2.5.9.7.0.0 Return Message



#### 2.5.9.8.0.0 Has Return

❌ No

#### 2.5.9.9.0.0 Is Activation

❌ No

#### 2.5.9.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Internal Method Call |
| Method | private void SetCurrentSession(GameState state) |
| Parameters | The newly created and initialized GameState object... |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Sets a static or singleton reference to the active... |

### 2.5.10.0.0.0 UI State Change

#### 2.5.10.1.0.0 Source Id

REPO-PRES-001

#### 2.5.10.2.0.0 Target Id

REPO-PRES-001

#### 2.5.10.3.0.0 Message

HideLoadingOverlay()

#### 2.5.10.4.0.0 Sequence Number

10

#### 2.5.10.5.0.0 Type

🔹 UI State Change

#### 2.5.10.6.0.0 Is Synchronous

✅ Yes

#### 2.5.10.7.0.0 Return Message



#### 2.5.10.8.0.0 Has Return

❌ No

#### 2.5.10.9.0.0 Is Activation

❌ No

#### 2.5.10.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Internal Method Call |
| Method | UIManager.HideLoadingScreen() |
| Parameters | None. |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | N/A |

### 2.5.11.0.0.0 Navigation

#### 2.5.11.1.0.0 Source Id

REPO-PRES-001

#### 2.5.11.2.0.0 Target Id

REPO-PRES-001

#### 2.5.11.3.0.0 Message

LoadSceneAsync('GameBoardScene')

#### 2.5.11.4.0.0 Sequence Number

11

#### 2.5.11.5.0.0 Type

🔹 Navigation

#### 2.5.11.6.0.0 Is Synchronous

❌ No

#### 2.5.11.7.0.0 Return Message



#### 2.5.11.8.0.0 Has Return

❌ No

#### 2.5.11.9.0.0 Is Activation

❌ No

#### 2.5.11.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Unity Scene Management |
| Method | SceneManager.LoadSceneAsync(string sceneName) |
| Parameters | The name of the main game board scene. |
| Authentication | N/A |
| Error Handling | Handles exceptions if the scene asset cannot be fo... |
| Performance | Asset loading for the next scene should comply wit... |

## 2.6.0.0.0.0 Notes

### 2.6.1.0.0.0 Content

#### 2.6.1.1.0.0 Content

The call from GameSessionService (REPO-AS-005) to StatisticsRepository (REPO-IL-006) is decoupled via the IStatisticsRepository interface defined in the Abstractions project (REPO-AA-004). This diagram shows the concrete runtime interaction.

#### 2.6.1.2.0.0 Position

top

#### 2.6.1.3.0.0 Participant Id

REPO-AS-005

#### 2.6.1.4.0.0 Sequence Number

6

### 2.6.2.0.0.0 Content

#### 2.6.2.1.0.0 Content

All game rule logic for initial setup (starting cash, property states, card deck order) is encapsulated within the GameState (REPO-DM-001) constructor and its internal methods, ensuring the Domain Layer is the single source of truth for business rules.

#### 2.6.2.2.0.0 Position

bottom

#### 2.6.2.3.0.0 Participant Id

REPO-DM-001

#### 2.6.2.4.0.0 Sequence Number

8

## 2.7.0.0.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | Input validation on the player profile name (REQ-1... |
| Performance Targets | The entire sequence, from clicking 'Start Game' to... |
| Error Handling Strategy | A layered error handling approach is required. The... |
| Testing Considerations | Integration tests should cover the entire flow, mo... |
| Monitoring Requirements | Log key events at INFO level: 'New game session cr... |
| Deployment Considerations | The initial SQLite database schema must be created... |

