# 1 Overview

## 1.1 Diagram Id

SEQ-DF-002

## 1.2 Name

User Loads a Valid Game from a Local File

## 1.3 Description

From the main menu, the user selects 'Load Game' and chooses a valid save slot. The game state is deserialized from the corresponding JSON file, restored into the Domain Layer, and rendered by the Presentation layer. This sequence includes integrity checks and data migration.

## 1.4 Type

🔹 DataFlow

## 1.5 Purpose

To allow players to resume a previously saved game, restoring their progress and continuing their session.

## 1.6 Complexity

High

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

- Presentation Layer requests a list of available saves from GameSessionService.
- GameSessionService calls the Infrastructure Layer to read save file metadata.
- User selects a slot, triggering a LoadAsync call on the ISaveGameRepository.
- Infrastructure's GameSaveRepository reads the JSON file and validates its checksum (REQ-1-088).
- The repository checks the file's gameVersion. If it is old, it invokes the DataMigrationManager to atomically update the data (REQ-1-090).
- The valid JSON is deserialized into a GameState object.
- The Application Services Layer receives the GameState object and uses it to initialize the Domain Layer.
- The Presentation Layer loads the main game scene and renders the loaded state.

## 1.11 Triggers

- User selects a save file from the 'Load Game' menu.

## 1.12 Outcomes

- The game state is fully restored to the point it was saved.
- The user is placed back into the game, ready to continue their turn.

## 1.13 Business Rules

- A file must pass checksum validation to be considered loadable (REQ-1-088).
- Older save file versions should be automatically migrated where possible (REQ-1-090).

## 1.14 Error Scenarios

- The selected save file is corrupted (checksum mismatch).
- The save file version is incompatible and cannot be migrated.
- File I/O error prevents reading the file.
- Deserialization fails due to malformed data.

## 1.15 Integration Points

- Local File System

# 2.0 Details

## 2.1 Diagram Id

SEQ-DF-002

## 2.2 Name

User Loads a Valid Game from a Local File

## 2.3 Description

Technical sequence for loading a game state from a local JSON file. This process is initiated by the user, orchestrated by the Application Services layer, and executed by the Infrastructure Layer. It includes critical data integrity checks (checksum validation), version compatibility analysis with on-the-fly data migration, and deserialization into a live domain object. The sequence strictly follows the Repository and Layered Architecture patterns.

## 2.4 Participants

### 2.4.1 UI/View Controller

#### 2.4.1.1 Repository Id

REPO-PRES-001

#### 2.4.1.2 Display Name

Presentation Layer

#### 2.4.1.3 Type

🔹 UI/View Controller

#### 2.4.1.4 Technology

Unity Engine, C#

#### 2.4.1.5 Order

1

#### 2.4.1.6 Style

| Property | Value |
|----------|-------|
| Shape | actor |
| Color | #4CAF50 |
| Stereotype | User Interface |

### 2.4.2.0 Service Orchestrator

#### 2.4.2.1 Repository Id

REPO-AS-005

#### 2.4.2.2 Display Name

Application Services

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
| Stereotype | GameSessionService |

### 2.4.3.0 Data Persistence

#### 2.4.3.1 Repository Id

REPO-IL-006

#### 2.4.3.2 Display Name

Infrastructure Layer

#### 2.4.3.3 Type

🔹 Data Persistence

#### 2.4.3.4 Technology

.NET 8, C#, System.Text.Json

#### 2.4.3.5 Order

3

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | database |
| Color | #FF9800 |
| Stereotype | GameSaveRepository |

### 2.4.4.0 Business Object

#### 2.4.4.1 Repository Id

REPO-DM-001

#### 2.4.4.2 Display Name

Domain Model

#### 2.4.4.3 Type

🔹 Business Object

#### 2.4.4.4 Technology

.NET 8, C#

#### 2.4.4.5 Order

4

#### 2.4.4.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #9C27B0 |
| Stereotype | GameState |

## 2.5.0.0 Interactions

### 2.5.1.0 Request

#### 2.5.1.1 Source Id

REPO-PRES-001

#### 2.5.1.2 Target Id

REPO-AS-005

#### 2.5.1.3 Message

User selects save slot 3. Triggers call to load the game.

#### 2.5.1.4 Sequence Number

1

#### 2.5.1.5 Type

🔹 Request

#### 2.5.1.6 Is Synchronous

❌ No

#### 2.5.1.7 Return Message

Returns success/failure result to UI for scene transition.

#### 2.5.1.8 Has Return

✅ Yes

#### 2.5.1.9 Is Activation

✅ Yes

#### 2.5.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Method Call |
| Method | Task<bool> GameSessionService.LoadGameAsync(int sl... |
| Parameters | slotId: 3 |
| Authentication | N/A (Local Application) |
| Error Handling | Catches exceptions from lower layers and translate... |
| Performance | The entire operation must complete in under 10 sec... |

### 2.5.2.0 Data Request

#### 2.5.2.1 Source Id

REPO-AS-005

#### 2.5.2.2 Target Id

REPO-IL-006

#### 2.5.2.3 Message

Request loading of game state from persistence.

#### 2.5.2.4 Sequence Number

2

#### 2.5.2.5 Type

🔹 Data Request

#### 2.5.2.6 Is Synchronous

❌ No

#### 2.5.2.7 Return Message

Returns the deserialized GameState object.

#### 2.5.2.8 Has Return

✅ Yes

#### 2.5.2.9 Is Activation

✅ Yes

#### 2.5.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Method Call (via ISaveGameRepository in... |
| Method | Task<GameState> GameSaveRepository.LoadAsync(int s... |
| Parameters | slot: 3 |
| Authentication | N/A |
| Error Handling | Propagates exceptions like SaveFileCorruptedExcept... |
| Performance | File I/O and deserialization are the primary laten... |

#### 2.5.2.11 Nested Interactions

##### 2.5.2.11.1 File I/O

###### 2.5.2.11.1.1 Source Id

REPO-IL-006

###### 2.5.2.11.1.2 Target Id

REPO-IL-006

###### 2.5.2.11.1.3 Message

Read file content from '%APPDATA%/MonopolyTycoon/saves/save_3.json'.

###### 2.5.2.11.1.4 Sequence Number

2.1

###### 2.5.2.11.1.5 Type

🔹 File I/O

###### 2.5.2.11.1.6 Is Synchronous

❌ No

###### 2.5.2.11.1.7 Has Return

❌ No

###### 2.5.2.11.1.8 Technical Details

| Property | Value |
|----------|-------|
| Protocol | File System API |
| Method | File.ReadAllBytesAsync(filePath) |
| Parameters | filePath to save file |
| Error Handling | Throws IOException on file access errors (e.g., fi... |
| Performance | Latency is dependent on storage medium speed (SSD ... |

##### 2.5.2.11.2.0 Validation

###### 2.5.2.11.2.1 Source Id

REPO-IL-006

###### 2.5.2.11.2.2 Target Id

REPO-IL-006

###### 2.5.2.11.2.3 Message

Validate file integrity using stored checksum (REQ-1-088).

###### 2.5.2.11.2.4 Sequence Number

2.2

###### 2.5.2.11.2.5 Type

🔹 Validation

###### 2.5.2.11.2.6 Is Synchronous

✅ Yes

###### 2.5.2.11.2.7 Has Return

❌ No

###### 2.5.2.11.2.8 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Internal Logic |
| Method | bool IsChecksumValid(byte[] fileData, string store... |
| Parameters | File content and checksum from a metadata block wi... |
| Error Handling | Throws SaveFileCorruptedException if calculated ch... |
| Performance | High-performance hash algorithm (e.g., SHA256) sho... |

##### 2.5.2.11.3.0 Validation

###### 2.5.2.11.3.1 Source Id

REPO-IL-006

###### 2.5.2.11.3.2 Target Id

REPO-IL-006

###### 2.5.2.11.3.3 Message

Check gameVersion property for compatibility (REQ-1-090).

###### 2.5.2.11.3.4 Sequence Number

2.3

###### 2.5.2.11.3.5 Type

🔹 Validation

###### 2.5.2.11.3.6 Is Synchronous

✅ Yes

###### 2.5.2.11.3.7 Has Return

❌ No

###### 2.5.2.11.3.8 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Internal Logic |
| Method | MigrationAction GetMigrationAction(string fileVers... |
| Parameters | Version string (e.g., '1.0.0') read from the JSON ... |
| Error Handling | Throws IncompatibleSaveVersionException if the ver... |
| Performance | Negligible. |

##### 2.5.2.11.4.0 Data Transformation

###### 2.5.2.11.4.1 Source Id

REPO-IL-006

###### 2.5.2.11.4.2 Target Id

REPO-IL-006

###### 2.5.2.11.4.3 Message

[Opt] If version is old, invoke DataMigrationManager to upgrade JSON data.

###### 2.5.2.11.4.4 Sequence Number

2.4

###### 2.5.2.11.4.5 Type

🔹 Data Transformation

###### 2.5.2.11.4.6 Is Synchronous

✅ Yes

###### 2.5.2.11.4.7 Return Message

Returns migrated JSON data.

###### 2.5.2.11.4.8 Has Return

✅ Yes

###### 2.5.2.11.4.9 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Internal Logic |
| Method | string DataMigrationManager.Migrate(string oldJson... |
| Parameters | Raw JSON content and its version. |
| Error Handling | Migration process is atomic. Throws DataMigrationE... |
| Performance | Can be complex depending on schema changes between... |

##### 2.5.2.11.5.0 Object Instantiation

###### 2.5.2.11.5.1 Source Id

REPO-IL-006

###### 2.5.2.11.5.2 Target Id

REPO-DM-001

###### 2.5.2.11.5.3 Message

Deserialize valid JSON into GameState object.

###### 2.5.2.11.5.4 Sequence Number

2.5

###### 2.5.2.11.5.5 Type

🔹 Object Instantiation

###### 2.5.2.11.5.6 Is Synchronous

✅ Yes

###### 2.5.2.11.5.7 Return Message

Populated GameState object.

###### 2.5.2.11.5.8 Has Return

✅ Yes

###### 2.5.2.11.5.9 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Serialization Library |
| Method | GameState System.Text.Json.JsonSerializer.Deserial... |
| Parameters | The validated and potentially migrated JSON string... |
| Error Handling | Throws JsonException if the data is malformed and ... |
| Performance | Leverages high-performance .NET 8 JSON library. |

### 2.5.3.0.0.0 State Update

#### 2.5.3.1.0.0 Source Id

REPO-AS-005

#### 2.5.3.2.0.0 Target Id

REPO-DM-001

#### 2.5.3.3.0.0 Message

Set the loaded GameState as the current active session.

#### 2.5.3.4.0.0 Sequence Number

3

#### 2.5.3.5.0.0 Type

🔹 State Update

#### 2.5.3.6.0.0 Is Synchronous

✅ Yes

#### 2.5.3.7.0.0 Has Return

❌ No

#### 2.5.3.8.0.0 Is Activation

❌ No

#### 2.5.3.9.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Method Call |
| Method | void GameSessionManager.SetActiveState(GameState s... |
| Parameters | The fully loaded GameState object. |
| Authentication | N/A |
| Error Handling | N/A. Assumes valid GameState object is provided. |
| Performance | Simple reference assignment, near-instantaneous. |

### 2.5.4.0.0.0 UI Update

#### 2.5.4.1.0.0 Source Id

REPO-PRES-001

#### 2.5.4.2.0.0 Target Id

REPO-PRES-001

#### 2.5.4.3.0.0 Message

Transition to main game scene and render the loaded state.

#### 2.5.4.4.0.0 Sequence Number

4

#### 2.5.4.5.0.0 Type

🔹 UI Update

#### 2.5.4.6.0.0 Is Synchronous

❌ No

#### 2.5.4.7.0.0 Has Return

❌ No

#### 2.5.4.8.0.0 Is Activation

❌ No

#### 2.5.4.9.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Unity SceneManager API |
| Method | SceneManager.LoadSceneAsync('MainGame'); GameBoard... |
| Parameters | N/A |
| Authentication | N/A |
| Error Handling | Unity engine handles scene loading errors. |
| Performance | Subject to asset loading times for the main game s... |

## 2.6.0.0.0.0 Notes

### 2.6.1.0.0.0 Content

#### 2.6.1.1.0.0 Content

The call from Application Services to the Infrastructure Layer is mediated by the ISaveGameRepository interface defined in REPO-AA-004. This adheres to the Dependency Inversion Principle, decoupling application logic from the persistence implementation.

#### 2.6.1.2.0.0 Position

top

#### 2.6.1.3.0.0 Participant Id

REPO-AS-005

#### 2.6.1.4.0.0 Sequence Number

2

### 2.6.2.0.0.0 Content

#### 2.6.2.1.0.0 Content

Error Handling: If GameSaveRepository.LoadAsync throws any exception, GameSessionService catches it, logs the error, and returns 'false'. The Presentation Layer then displays a modal dialog to the user (e.g., 'Failed to load save file. The file may be corrupt.').

#### 2.6.2.2.0.0 Position

bottom

#### 2.6.2.3.0.0 Participant Id

REPO-PRES-001

#### 2.6.2.4.0.0 Sequence Number

1

## 2.7.0.0.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | While the application is offline, checksum validat... |
| Performance Targets | The end-to-end load operation, from user click to ... |
| Error Handling Strategy | The Infrastructure Layer must throw distinct, spec... |
| Testing Considerations | Integration tests are critical. A suite of predefi... |
| Monitoring Requirements | All load attempts should be logged at the INFO lev... |
| Deployment Considerations | The data migration logic must be robust and forwar... |

