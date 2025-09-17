# 1 Overview

## 1.1 Diagram Id

SEQ-AF-018

## 1.2 Name

Player Profile Creation and Validation

## 1.3 Description

On the game setup screen, a new user enters a profile name. The system validates the input against defined rules and, if valid, creates a persistent profile record for tracking statistics.

## 1.4 Type

🔹 AuthenticationFlow

## 1.5 Purpose

To establish a persistent player identity for tracking historical statistics and high scores.

## 1.6 Complexity

Low

## 1.7 Priority

🔴 High

## 1.8 Frequency

OnDemand

## 1.9 Participants

- PresentationLayer
- ApplicationServicesLayer
- InfrastructureLayer

## 1.10 Key Interactions

- User types a name into the profile name input field.
- The PresentationLayer performs real-time validation (length, allowed characters).
- When the user starts a game, the GameSessionService takes the validated name.
- It calls the PlayerProfileRepository (Infrastructure) to find or create a profile with that name.
- The repository creates a new record in the SQLite database, assigning a persistent unique profile_id.
- This profile_id is then associated with the human player in the GameState.

## 1.11 Triggers

- User enters a profile name on the setup screen and starts a game.

## 1.12 Outcomes

- A valid player profile is created and persisted in the database.
- The user's in-game identity is linked to this persistent profile.

## 1.13 Business Rules

- Display name must be between 3 and 16 characters (REQ-1-032).
- Special characters are disallowed (REQ-1-032).

## 1.14 Error Scenarios

- User enters an invalid name and is shown a validation error message.

## 1.15 Integration Points

- StatisticsRepository (SQLite)

# 2.0 Details

## 2.1 Diagram Id

SEQ-AF-018

## 2.2 Name

Player Profile Creation and Validation

## 2.3 Description

Implementation sequence for creating a persistent player profile. The flow starts with user input on the setup screen, followed by real-time UI validation. Upon starting a game, the application service layer orchestrates the retrieval or creation of a profile in the underlying SQLite database via the infrastructure layer's repository. This ensures a persistent identity for the human player is established before the game session begins.

## 2.4 Participants

### 2.4.1 Actor

#### 2.4.1.1 Repository Id

User

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
| Color | #FFFFFF |
| Stereotype | User |

### 2.4.2.0 Presentation Layer Component

#### 2.4.2.1 Repository Id

REPO-UI-SETUP-001

#### 2.4.2.2 Display Name

SetupScreenUI

#### 2.4.2.3 Type

🔹 Presentation Layer Component

#### 2.4.2.4 Technology

Unity Engine, C#

#### 2.4.2.5 Order

2

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | rectangle |
| Color | #ADD8E6 |
| Stereotype | <<Controller>> |

### 2.4.3.0 Application Service

#### 2.4.3.1 Repository Id

REPO-AS-005

#### 2.4.3.2 Display Name

GameSessionService

#### 2.4.3.3 Type

🔹 Application Service

#### 2.4.3.4 Technology

.NET 8, C#

#### 2.4.3.5 Order

3

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | rectangle |
| Color | #90EE90 |
| Stereotype | <<Service>> |

### 2.4.4.0 Infrastructure Repository

#### 2.4.4.1 Repository Id

REPO-IP-ST-009

#### 2.4.4.2 Display Name

PlayerProfileRepository

#### 2.4.4.3 Type

🔹 Infrastructure Repository

#### 2.4.4.4 Technology

Microsoft.Data.Sqlite, C#

#### 2.4.4.5 Order

4

#### 2.4.4.6 Style

| Property | Value |
|----------|-------|
| Shape | rectangle |
| Color | #FFDDC1 |
| Stereotype | <<Repository>> |

## 2.5.0.0 Interactions

### 2.5.1.0 User Input

#### 2.5.1.1 Source Id

User

#### 2.5.1.2 Target Id

REPO-UI-SETUP-001

#### 2.5.1.3 Message

Enters profile name into input field.

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

OnValueChanged

##### 2.5.1.10.3 Parameters

- {'name': 'profileName', 'type': 'string'}

##### 2.5.1.10.4 Authentication

N/A

##### 2.5.1.10.5 Error Handling

N/A

##### 2.5.1.10.6 Performance

Real-time, <16ms response

#### 2.5.1.11.0 Nested Interactions

*No items available*

### 2.5.2.0.0 Internal Method Call

#### 2.5.2.1.0 Source Id

REPO-UI-SETUP-001

#### 2.5.2.2.0 Target Id

REPO-UI-SETUP-001

#### 2.5.2.3.0 Message

ValidateProfileName(profileName)

#### 2.5.2.4.0 Sequence Number

2

#### 2.5.2.5.0 Type

🔹 Internal Method Call

#### 2.5.2.6.0 Is Synchronous

✅ Yes

#### 2.5.2.7.0 Return Message

isValid: bool, errorMessage: string

#### 2.5.2.8.0 Has Return

✅ Yes

#### 2.5.2.9.0 Is Activation

❌ No

#### 2.5.2.10.0 Technical Details

##### 2.5.2.10.1 Protocol

In-memory

##### 2.5.2.10.2 Method

ValidateProfileName

##### 2.5.2.10.3 Parameters

- {'name': 'profileName', 'type': 'string'}

##### 2.5.2.10.4 Authentication

N/A

##### 2.5.2.10.5 Error Handling

Returns validation result and error message for UI display.

##### 2.5.2.10.6 Performance

Near-instantaneous, <1ms

#### 2.5.2.11.0 Nested Interactions

*No items available*

### 2.5.3.0.0 User Action

#### 2.5.3.1.0 Source Id

User

#### 2.5.3.2.0 Target Id

REPO-UI-SETUP-001

#### 2.5.3.3.0 Message

Clicks 'Start Game' button.

#### 2.5.3.4.0 Sequence Number

3

#### 2.5.3.5.0 Type

🔹 User Action

#### 2.5.3.6.0 Is Synchronous

✅ Yes

#### 2.5.3.7.0 Return Message



#### 2.5.3.8.0 Has Return

❌ No

#### 2.5.3.9.0 Is Activation

❌ No

#### 2.5.3.10.0 Technical Details

##### 2.5.3.10.1 Protocol

UI Event

##### 2.5.3.10.2 Method

OnStartGameClicked

##### 2.5.3.10.3 Parameters

*No items available*

##### 2.5.3.10.4 Authentication

N/A

##### 2.5.3.10.5 Error Handling

Button is disabled if profile name is invalid.

##### 2.5.3.10.6 Performance

Real-time, <16ms response

#### 2.5.3.11.0 Nested Interactions

*No items available*

### 2.5.4.0.0 Asynchronous Service Call

#### 2.5.4.1.0 Source Id

REPO-UI-SETUP-001

#### 2.5.4.2.0 Target Id

REPO-AS-005

#### 2.5.4.3.0 Message

StartNewGameAsync(profileName, gameSettings)

#### 2.5.4.4.0 Sequence Number

4

#### 2.5.4.5.0 Type

🔹 Asynchronous Service Call

#### 2.5.4.6.0 Is Synchronous

❌ No

#### 2.5.4.7.0 Return Message

Task<GameSession>

#### 2.5.4.8.0 Has Return

✅ Yes

#### 2.5.4.9.0 Is Activation

✅ Yes

#### 2.5.4.10.0 Technical Details

##### 2.5.4.10.1 Protocol

In-memory

##### 2.5.4.10.2 Method

StartNewGameAsync

##### 2.5.4.10.3 Parameters

###### 2.5.4.10.3.1 string

####### 2.5.4.10.3.1.1 Name

profileName

####### 2.5.4.10.3.1.2 Type

🔹 string

###### 2.5.4.10.3.2.0 GameSetupDto

####### 2.5.4.10.3.2.1 Name

gameSettings

####### 2.5.4.10.3.2.2 Type

🔹 GameSetupDto

##### 2.5.4.10.4.0.0 Authentication

N/A

##### 2.5.4.10.5.0.0 Error Handling

Exceptions are caught and propagated to the UI for display.

##### 2.5.4.10.6.0.0 Performance

Should complete within asset loading time budget (<10s).

#### 2.5.4.11.0.0.0 Nested Interactions

*No items available*

### 2.5.5.0.0.0.0 Asynchronous Repository Call

#### 2.5.5.1.0.0.0 Source Id

REPO-AS-005

#### 2.5.5.2.0.0.0 Target Id

REPO-IP-ST-009

#### 2.5.5.3.0.0.0 Message

GetOrCreateProfileAsync(displayName)

#### 2.5.5.4.0.0.0 Sequence Number

5

#### 2.5.5.5.0.0.0 Type

🔹 Asynchronous Repository Call

#### 2.5.5.6.0.0.0 Is Synchronous

❌ No

#### 2.5.5.7.0.0.0 Return Message

Task<PlayerProfile>

#### 2.5.5.8.0.0.0 Has Return

✅ Yes

#### 2.5.5.9.0.0.0 Is Activation

✅ Yes

#### 2.5.5.10.0.0.0 Technical Details

##### 2.5.5.10.1.0.0 Protocol

In-memory

##### 2.5.5.10.2.0.0 Method

GetOrCreateProfileAsync

##### 2.5.5.10.3.0.0 Parameters

- {'name': 'displayName', 'type': 'string'}

##### 2.5.5.10.4.0.0 Authentication

N/A

##### 2.5.5.10.5.0.0 Error Handling

Handles SQLite exceptions (e.g., db corruption, disk I/O errors) and logs them. Propagates a service-level exception.

##### 2.5.5.10.6.0.0 Performance

Typically < 50ms on an SSD.

#### 2.5.5.11.0.0.0 Nested Interactions

##### 2.5.5.11.1.0.0 Database Query

###### 2.5.5.11.1.1.0 Source Id

REPO-IP-ST-009

###### 2.5.5.11.1.2.0 Target Id

REPO-IP-ST-009

###### 2.5.5.11.1.3.0 Message

Query: SELECT * FROM PlayerProfiles WHERE DisplayName = @name LIMIT 1

###### 2.5.5.11.1.4.0 Sequence Number

6

###### 2.5.5.11.1.5.0 Type

🔹 Database Query

###### 2.5.5.11.1.6.0 Is Synchronous

✅ Yes

###### 2.5.5.11.1.7.0 Return Message

PlayerProfile row or NULL

###### 2.5.5.11.1.8.0 Has Return

✅ Yes

###### 2.5.5.11.1.9.0 Is Activation

❌ No

###### 2.5.5.11.1.10.0 Technical Details

####### 2.5.5.11.1.10.1 Protocol

SQLite Connection

####### 2.5.5.11.1.10.2 Method

ExecuteReaderAsync

####### 2.5.5.11.1.10.3 Parameters

- {'name': '@name', 'type': 'string'}

####### 2.5.5.11.1.10.4 Authentication

N/A

####### 2.5.5.11.1.10.5 Error Handling

Connection errors or query failures are logged and wrapped in a DataAccessException.

####### 2.5.5.11.1.10.6 Performance

Indexed query, typically < 10ms.

##### 2.5.5.11.2.0.0 Database Command

###### 2.5.5.11.2.1.0 Source Id

REPO-IP-ST-009

###### 2.5.5.11.2.2.0 Target Id

REPO-IP-ST-009

###### 2.5.5.11.2.3.0 Message

Command: INSERT INTO PlayerProfiles (ProfileId, DisplayName, ...) VALUES (@id, @name, ...)

###### 2.5.5.11.2.4.0 Sequence Number

7

###### 2.5.5.11.2.5.0 Type

🔹 Database Command

###### 2.5.5.11.2.6.0 Is Synchronous

✅ Yes

###### 2.5.5.11.2.7.0 Return Message

Rows affected (1)

###### 2.5.5.11.2.8.0 Has Return

✅ Yes

###### 2.5.5.11.2.9.0 Is Activation

❌ No

###### 2.5.5.11.2.10.0 Technical Details

####### 2.5.5.11.2.10.1 Protocol

SQLite Connection

####### 2.5.5.11.2.10.2 Method

ExecuteNonQueryAsync

####### 2.5.5.11.2.10.3 Parameters

######## 2.5.5.11.2.10.3.1 Guid/TEXT

######### 2.5.5.11.2.10.3.1.1 Name

@id

######### 2.5.5.11.2.10.3.1.2 Type

🔹 Guid/TEXT

######## 2.5.5.11.2.10.3.2.0 string

######### 2.5.5.11.2.10.3.2.1 Name

@name

######### 2.5.5.11.2.10.3.2.2 Type

🔹 string

####### 2.5.5.11.2.10.4.0.0 Authentication

N/A

####### 2.5.5.11.2.10.5.0.0 Error Handling

Handles UNIQUE constraint violations (race condition) and other write errors. Logs and throws DataAccessException.

####### 2.5.5.11.2.10.6.0.0 Performance

Typically < 20ms.

### 2.5.6.0.0.0.0.0.0 Internal Method Call

#### 2.5.6.1.0.0.0.0.0 Source Id

REPO-AS-005

#### 2.5.6.2.0.0.0.0.0 Target Id

REPO-AS-005

#### 2.5.6.3.0.0.0.0.0 Message

InitializeGameState(humanProfile, gameSettings)

#### 2.5.6.4.0.0.0.0.0 Sequence Number

8

#### 2.5.6.5.0.0.0.0.0 Type

🔹 Internal Method Call

#### 2.5.6.6.0.0.0.0.0 Is Synchronous

✅ Yes

#### 2.5.6.7.0.0.0.0.0 Return Message

GameState

#### 2.5.6.8.0.0.0.0.0 Has Return

✅ Yes

#### 2.5.6.9.0.0.0.0.0 Is Activation

❌ No

#### 2.5.6.10.0.0.0.0.0 Technical Details

##### 2.5.6.10.1.0.0.0.0 Protocol

In-memory

##### 2.5.6.10.2.0.0.0.0 Method

InitializeGameState

##### 2.5.6.10.3.0.0.0.0 Parameters

###### 2.5.6.10.3.1.0.0.0 PlayerProfile

####### 2.5.6.10.3.1.1.0.0 Name

humanProfile

####### 2.5.6.10.3.1.2.0.0 Type

🔹 PlayerProfile

###### 2.5.6.10.3.2.0.0.0 GameSetupDto

####### 2.5.6.10.3.2.1.0.0 Name

gameSettings

####### 2.5.6.10.3.2.2.0.0 Type

🔹 GameSetupDto

##### 2.5.6.10.4.0.0.0.0 Authentication

N/A

##### 2.5.6.10.5.0.0.0.0 Error Handling

N/A

##### 2.5.6.10.6.0.0.0.0 Performance

Near-instantaneous.

#### 2.5.6.11.0.0.0.0.0 Nested Interactions

*No items available*

### 2.5.7.0.0.0.0.0.0 Scene Management

#### 2.5.7.1.0.0.0.0.0 Source Id

REPO-UI-SETUP-001

#### 2.5.7.2.0.0.0.0.0 Target Id

REPO-UI-SETUP-001

#### 2.5.7.3.0.0.0.0.0 Message

TransitionToGameBoardScene()

#### 2.5.7.4.0.0.0.0.0 Sequence Number

9

#### 2.5.7.5.0.0.0.0.0 Type

🔹 Scene Management

#### 2.5.7.6.0.0.0.0.0 Is Synchronous

✅ Yes

#### 2.5.7.7.0.0.0.0.0 Return Message



#### 2.5.7.8.0.0.0.0.0 Has Return

❌ No

#### 2.5.7.9.0.0.0.0.0 Is Activation

❌ No

#### 2.5.7.10.0.0.0.0.0 Technical Details

##### 2.5.7.10.1.0.0.0.0 Protocol

Unity SceneManager

##### 2.5.7.10.2.0.0.0.0 Method

LoadSceneAsync

##### 2.5.7.10.3.0.0.0.0 Parameters

- {'name': 'sceneName', 'type': 'string', 'value': 'GameBoard'}

##### 2.5.7.10.4.0.0.0.0 Authentication

N/A

##### 2.5.7.10.5.0.0.0.0 Error Handling

Unity handles scene loading errors.

##### 2.5.7.10.6.0.0.0.0 Performance

Subject to REQ-1-015, under 10 seconds.

#### 2.5.7.11.0.0.0.0.0 Nested Interactions

*No items available*

## 2.6.0.0.0.0.0.0.0 Notes

### 2.6.1.0.0.0.0.0.0 Content

#### 2.6.1.1.0.0.0.0.0 Content

Real-time validation (step 2) occurs on every keystroke in the UI to provide immediate feedback, enforcing REQ-1-032 without waiting for a server roundtrip. The 'Start Game' button remains disabled until the validation passes.

#### 2.6.1.2.0.0.0.0.0 Position

top-right

#### 2.6.1.3.0.0.0.0.0 Participant Id

REPO-UI-SETUP-001

#### 2.6.1.4.0.0.0.0.0 Sequence Number

2

### 2.6.2.0.0.0.0.0.0 Content

#### 2.6.2.1.0.0.0.0.0 Content

The database interaction in step 7 is a conditional 'Create'. The profile is only created if it does not already exist with the same name. This allows players to reuse their profiles across multiple games.

#### 2.6.2.2.0.0.0.0.0 Position

bottom-right

#### 2.6.2.3.0.0.0.0.0 Participant Id

REPO-IP-ST-009

#### 2.6.2.4.0.0.0.0.0 Sequence Number

7

## 2.7.0.0.0.0.0.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | Input validation is the primary security control. ... |
| Performance Targets | The entire profile creation/retrieval process (ste... |
| Error Handling Strategy | Validation Failures: The UI must display a clear, ... |
| Testing Considerations | Unit tests must cover the `ValidateProfileName` lo... |
| Monitoring Requirements | Log successful creation of a new player profile at... |
| Deployment Considerations | The application installer must ensure that the use... |

