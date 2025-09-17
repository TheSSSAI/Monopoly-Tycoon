# 1 Overview

## 1.1 Diagram Id

SEQ-DF-001

## 1.2 Name

User Saves the Game State to a Local File

## 1.3 Description

During their turn, the user opens the in-game menu, selects 'Save Game', and chooses a save slot. The current game state is then retrieved from the Domain layer and persisted to a local JSON file via the Infrastructure layer, demonstrating the Repository architectural pattern.

## 1.4 Type

🔹 DataFlow

## 1.5 Purpose

To persist the current state of a game, allowing the player to stop and resume playing at a later time, which is a critical feature for usability and replayability.

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

- User selects a save slot in the Presentation Layer.
- Presentation Layer calls the Application Services Layer (GameSessionService) to trigger a save.
- GameSessionService retrieves the current GameState object from the Domain Layer.
- GameSessionService calls the SaveAsync method on the ISaveGameRepository interface (defined in REPO-AA-004).
- The Infrastructure Layer's GameSaveRepository implementation serializes the GameState object to a versioned JSON format using System.Text.Json.
- GameSaveRepository calculates a checksum and writes the JSON data and checksum to the specified file in the user's APPDATA folder.

## 1.11 Triggers

- User confirms saving the game to a specific slot.

## 1.12 Outcomes

- A JSON file representing the complete game state is created or overwritten on the local file system.
- The user is notified of the successful save via the UI.

## 1.13 Business Rules

- Saving is only allowed during the human player's turn, before the dice roll (REQ-1-085).
- Save files must be versioned and include a checksum for integrity validation (REQ-1-087, REQ-1-088).

## 1.14 Error Scenarios

- File I/O error prevents writing the save file.
- Serialization of the GameState object fails.

## 1.15 Integration Points

- Local File System

# 2.0 Details

## 2.1 Diagram Id

SEQ-DF-001-IMPL

## 2.2 Name

Technical Sequence for Game State Persistence via Repository Pattern

## 2.3 Description

This diagram provides a detailed technical implementation for the user-initiated game save process. It illustrates the data flow from the Presentation Layer, through the Application Services and Domain Layers, to the Infrastructure Layer. The sequence explicitly demonstrates the Repository Pattern, where the Application Service interacts with an abstraction (ISaveGameRepository) and the Infrastructure Layer provides the concrete implementation for file system persistence. The process includes state retrieval, object-to-JSON serialization, checksum calculation for data integrity, and asynchronous file I/O operations.

## 2.4 Participants

### 2.4.1 UI Layer Component

#### 2.4.1.1 Repository Id

REPO-PRES-001

#### 2.4.1.2 Display Name

Presentation Layer (UI Controller)

#### 2.4.1.3 Type

🔹 UI Layer Component

#### 2.4.1.4 Technology

Unity Engine, C#

#### 2.4.1.5 Order

1

#### 2.4.1.6 Style

| Property | Value |
|----------|-------|
| Shape | actor |
| Color | #FFDDC1 |
| Stereotype | Unity MonoBehaviour |

### 2.4.2.0 Application Logic Orchestrator

#### 2.4.2.1 Repository Id

REPO-AS-005

#### 2.4.2.2 Display Name

Application Service (GameSessionService)

#### 2.4.2.3 Type

🔹 Application Logic Orchestrator

#### 2.4.2.4 Technology

.NET 8, C#

#### 2.4.2.5 Order

2

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | rectangle |
| Color | #C2EFEB |
| Stereotype | Service |

### 2.4.3.0 Domain Entity

#### 2.4.3.1 Repository Id

REPO-DM-001

#### 2.4.3.2 Display Name

Domain Model (GameState)

#### 2.4.3.3 Type

🔹 Domain Entity

#### 2.4.3.4 Technology

.NET 8, C#

#### 2.4.3.5 Order

3

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | rectangle |
| Color | #E5D9F2 |
| Stereotype | Entity |

### 2.4.4.0 Interface Contract

#### 2.4.4.1 Repository Id

REPO-AA-004

#### 2.4.4.2 Display Name

Repository Abstraction (ISaveGameRepository)

#### 2.4.4.3 Type

🔹 Interface Contract

#### 2.4.4.4 Technology

.NET 8, C#

#### 2.4.4.5 Order

4

#### 2.4.4.6 Style

| Property | Value |
|----------|-------|
| Shape | rectangle |
| Color | #FFFACD |
| Stereotype | Interface |

### 2.4.5.0 Repository Implementation

#### 2.4.5.1 Repository Id

REPO-IL-006

#### 2.4.5.2 Display Name

Infrastructure (GameSaveRepository)

#### 2.4.5.3 Type

🔹 Repository Implementation

#### 2.4.5.4 Technology

.NET 8, C#, System.Text.Json

#### 2.4.5.5 Order

5

#### 2.4.5.6 Style

| Property | Value |
|----------|-------|
| Shape | database |
| Color | #D3D3D3 |
| Stereotype | Repository |

## 2.5.0.0 Interactions

### 2.5.1.0 Asynchronous Method Call

#### 2.5.1.1 Source Id

REPO-PRES-001

#### 2.5.1.2 Target Id

REPO-AS-005

#### 2.5.1.3 Message

1. RequestSaveGameAsync(saveSlot: int)

#### 2.5.1.4 Sequence Number

1

#### 2.5.1.5 Type

🔹 Asynchronous Method Call

#### 2.5.1.6 Is Synchronous

❌ No

#### 2.5.1.7 Return Message

8. return Task<bool> (saveSuccess)

#### 2.5.1.8 Has Return

✅ Yes

#### 2.5.1.9 Is Activation

✅ Yes

#### 2.5.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | RequestSaveGameAsync |
| Parameters | [in] saveSlot: int - The 1-based index of the save... |
| Authentication | N/A (Local application) |
| Error Handling | The UI Controller must await the task and handle a... |
| Performance | Method invocation should be sub-millisecond. |

### 2.5.2.0 Synchronous Data Retrieval

#### 2.5.2.1 Source Id

REPO-AS-005

#### 2.5.2.2 Target Id

REPO-DM-001

#### 2.5.2.3 Message

2. GetCurrentGameState()

#### 2.5.2.4 Sequence Number

2

#### 2.5.2.5 Type

🔹 Synchronous Data Retrieval

#### 2.5.2.6 Is Synchronous

✅ Yes

#### 2.5.2.7 Return Message

currentGameState: GameState

#### 2.5.2.8 Has Return

✅ Yes

#### 2.5.2.9 Is Activation

❌ No

#### 2.5.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | GetCurrentGameState (via injected IGameStateProvid... |
| Parameters | None |
| Authentication | N/A |
| Error Handling | If GameState is null or invalid, the save operatio... |
| Performance | Should be a direct memory reference, near-instanta... |

### 2.5.3.0 Asynchronous Interface Call

#### 2.5.3.1 Source Id

REPO-AS-005

#### 2.5.3.2 Target Id

REPO-AA-004

#### 2.5.3.3 Message

3. SaveAsync(gameState: GameState, slot: int)

#### 2.5.3.4 Sequence Number

3

#### 2.5.3.5 Type

🔹 Asynchronous Interface Call

#### 2.5.3.6 Is Synchronous

❌ No

#### 2.5.3.7 Return Message

7. return Task<bool> (writeSuccess)

#### 2.5.3.8 Has Return

✅ Yes

#### 2.5.3.9 Is Activation

✅ Yes

#### 2.5.3.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process (via Dependency Injection) |
| Method | SaveAsync |
| Parameters | [in] gameState: The complete GameState object to b... |
| Authentication | N/A |
| Error Handling | The service must wrap this call in a try-catch blo... |
| Performance | Latency is dependent on file I/O performance of th... |

### 2.5.4.0 Implementation Invocation

#### 2.5.4.1 Source Id

REPO-AA-004

#### 2.5.4.2 Target Id

REPO-IL-006

#### 2.5.4.3 Message

4. [DI Resolution] SaveAsync is invoked on the concrete implementation

#### 2.5.4.4 Sequence Number

4

#### 2.5.4.5 Type

🔹 Implementation Invocation

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
| Protocol | Dependency Injection |
| Method | N/A |
| Parameters | N/A |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | N/A |

#### 2.5.4.11 Nested Interactions

##### 2.5.4.11.1 Internal Data Transformation

###### 2.5.4.11.1.1 Source Id

REPO-IL-006

###### 2.5.4.11.1.2 Target Id

REPO-IL-006

###### 2.5.4.11.1.3 Message

4.1. Serialize GameState to JSON

###### 2.5.4.11.1.4 Sequence Number

4.1

###### 2.5.4.11.1.5 Type

🔹 Internal Data Transformation

###### 2.5.4.11.1.6 Is Synchronous

✅ Yes

###### 2.5.4.11.1.7 Return Message

jsonString: string

###### 2.5.4.11.1.8 Has Return

✅ Yes

###### 2.5.4.11.1.9 Is Activation

❌ No

###### 2.5.4.11.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | System.Text.Json.JsonSerializer |
| Method | Serialize |
| Parameters | The GameState object with a version property (REQ-... |
| Authentication | N/A |
| Error Handling | Catch JsonException. Log detailed error and return... |
| Performance | Serialization performance is critical. Use source ... |

##### 2.5.4.11.2.0 Internal Data Validation

###### 2.5.4.11.2.1 Source Id

REPO-IL-006

###### 2.5.4.11.2.2 Target Id

REPO-IL-006

###### 2.5.4.11.2.3 Message

4.2. Calculate SHA256 checksum of JSON string

###### 2.5.4.11.2.4 Sequence Number

4.2

###### 2.5.4.11.2.5 Type

🔹 Internal Data Validation

###### 2.5.4.11.2.6 Is Synchronous

✅ Yes

###### 2.5.4.11.2.7 Return Message

checksum: string

###### 2.5.4.11.2.8 Has Return

✅ Yes

###### 2.5.4.11.2.9 Is Activation

❌ No

###### 2.5.4.11.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | System.Security.Cryptography |
| Method | SHA256.HashData |
| Parameters | The UTF8 byte array of the JSON string. |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Minimal overhead for typical save file sizes. |

##### 2.5.4.11.3.0 Asynchronous File I/O

###### 2.5.4.11.3.1 Source Id

REPO-IL-006

###### 2.5.4.11.3.2 Target Id

REPO-IL-006

###### 2.5.4.11.3.3 Message

4.3. Write versioned JSON and checksum to file

###### 2.5.4.11.3.4 Sequence Number

4.3

###### 2.5.4.11.3.5 Type

🔹 Asynchronous File I/O

###### 2.5.4.11.3.6 Is Synchronous

❌ No

###### 2.5.4.11.3.7 Return Message

Task<bool>

###### 2.5.4.11.3.8 Has Return

✅ Yes

###### 2.5.4.11.3.9 Is Activation

❌ No

###### 2.5.4.11.3.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | File System API |
| Method | File.WriteAllTextAsync |
| Parameters | Target file path: `%APPDATA%/MonopolyTycoon/saves/... |
| Authentication | N/A, relies on OS file permissions. |
| Error Handling | Catch IOException, UnauthorizedAccessException. Lo... |
| Performance | This is the main source of latency. Must be async ... |

### 2.5.5.0.0.0 Asynchronous Return

#### 2.5.5.1.0.0 Source Id

REPO-IL-006

#### 2.5.5.2.0.0 Target Id

REPO-AA-004

#### 2.5.5.3.0.0 Message

5. return Task.FromResult(true)

#### 2.5.5.4.0.0 Sequence Number

5

#### 2.5.5.5.0.0 Type

🔹 Asynchronous Return

#### 2.5.5.6.0.0 Is Synchronous

❌ No

#### 2.5.5.7.0.0 Return Message



#### 2.5.5.8.0.0 Has Return

❌ No

#### 2.5.5.9.0.0 Is Activation

❌ No

#### 2.5.5.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | N/A |
| Parameters | Boolean indicating success of the file write opera... |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | N/A |

### 2.5.6.0.0.0 Task Completion

#### 2.5.6.1.0.0 Source Id

REPO-AA-004

#### 2.5.6.2.0.0 Target Id

REPO-AS-005

#### 2.5.6.3.0.0 Message

6. Awaited task completes

#### 2.5.6.4.0.0 Sequence Number

6

#### 2.5.6.5.0.0 Type

🔹 Task Completion

#### 2.5.6.6.0.0 Is Synchronous

❌ No

#### 2.5.6.7.0.0 Return Message



#### 2.5.6.8.0.0 Has Return

❌ No

#### 2.5.6.9.0.0 Is Activation

❌ No

#### 2.5.6.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | async/await |
| Method | N/A |
| Parameters | N/A |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | N/A |

### 2.5.7.0.0.0 Task Completion

#### 2.5.7.1.0.0 Source Id

REPO-AS-005

#### 2.5.7.2.0.0 Target Id

REPO-PRES-001

#### 2.5.7.3.0.0 Message

7. Awaited task completes, returning success status

#### 2.5.7.4.0.0 Sequence Number

7

#### 2.5.7.5.0.0 Type

🔹 Task Completion

#### 2.5.7.6.0.0 Is Synchronous

❌ No

#### 2.5.7.7.0.0 Return Message



#### 2.5.7.8.0.0 Has Return

❌ No

#### 2.5.7.9.0.0 Is Activation

❌ No

#### 2.5.7.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | async/await |
| Method | N/A |
| Parameters | N/A |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | N/A |

### 2.5.8.0.0.0 UI Update

#### 2.5.8.1.0.0 Source Id

REPO-PRES-001

#### 2.5.8.2.0.0 Target Id

REPO-PRES-001

#### 2.5.8.3.0.0 Message

8. DisplaySaveResult(success: bool)

#### 2.5.8.4.0.0 Sequence Number

8

#### 2.5.8.5.0.0 Type

🔹 UI Update

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
| Protocol | Unity UI API |
| Method | DisplaySaveResult |
| Parameters | [in] success: Determines whether to show a 'Save S... |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | UI updates must occur on the main thread. |

## 2.6.0.0.0.0 Notes

### 2.6.1.0.0.0 Content

#### 2.6.1.1.0.0 Content

Business Rule Check (REQ-1-085): The 'Save Game' button in the UI (REPO-PRES-001) must be disabled unless it is the human player's turn and they are in the 'Pre-Roll Management Phase'. This check is performed in the Presentation Layer before initiating the save sequence.

#### 2.6.1.2.0.0 Position

top-left

#### 2.6.1.3.0.0 Participant Id

REPO-PRES-001

#### 2.6.1.4.0.0 Sequence Number

1

### 2.6.2.0.0.0 Content

#### 2.6.2.1.0.0 Content

The Infrastructure Layer (REPO-IL-006) must be completely unaware of any game rules. Its sole responsibility is the technical implementation of persistence for the given data contract (GameState).

#### 2.6.2.2.0.0 Position

bottom-right

#### 2.6.2.3.0.0 Participant Id

REPO-IL-006

#### 2.6.2.4.0.0 Sequence Number

4

## 2.7.0.0.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | File system access is limited by the operating sys... |
| Performance Targets | The entire save operation, from user click to conf... |
| Error Handling Strategy | A layered error handling approach is required. The... |
| Testing Considerations | Unit tests (NUnit) should cover the `GameSaveRepos... |
| Monitoring Requirements | All save attempts must be logged. A successful sav... |
| Deployment Considerations | The installer (Inno Setup) must ensure that the ap... |

