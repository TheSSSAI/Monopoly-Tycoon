# 1 Overview

## 1.1 Diagram Id

SEQ-OP-010

## 1.2 Name

Data Migration for Old Save File

## 1.3 Description

After an update, a user loads a save file from a previous version. The system detects the old version, makes a safe backup of the original file, and atomically applies a series of transformations to upgrade the file to the current format before loading.

## 1.4 Type

🔹 OperationalFlow

## 1.5 Purpose

To preserve user progress across game updates, ensuring a smooth user experience and maintaining player engagement (REQ-1-090).

## 1.6 Complexity

High

## 1.7 Priority

🟡 Medium

## 1.8 Frequency

OnDemand

## 1.9 Participants

- InfrastructureLayer
- ApplicationServicesLayer

## 1.10 Key Interactions

- User selects an old save file to load.
- The GameSaveRepository reads the file and checks the gameVersion property.
- It determines the version is old and invokes the DataMigrationManager.
- The DataMigrationManager makes a temporary backup of the original file.
- It applies a series of version-specific transformation steps (e.g., v1 -> v2, v2 -> v3) to upgrade the data structure in-memory.
- If migration succeeds, the original file is overwritten with the new version, and the temporary backup is deleted.
- If migration fails, the original file is restored from the temporary backup, and an error is reported.

## 1.11 Triggers

- A load operation is requested on a data file with an old version number.

## 1.12 Outcomes

- The old save file is successfully and transparently upgraded to the new format.
- The game loads successfully from the migrated data.

## 1.13 Business Rules

- The migration process must be atomic; if it fails, the original file must be left untouched (REQ-1-090).
- Backward migration (new to old) is not supported (REQ-1-090).

## 1.14 Error Scenarios

- The migration process fails due to an unexpected data format, leaving the original file intact.
- An I/O error occurs, triggering a rollback from backup.

## 1.15 Integration Points

- Local File System

# 2.0 Details

## 2.1 Diagram Id

SEQ-OP-010

## 2.2 Name

Atomic Data Migration for Legacy Save File

## 2.3 Description

Details the operational flow for detecting a legacy save file, performing a safe, atomic, in-place upgrade, and handling potential failures to ensure user data is preserved across application updates as per REQ-1-090. This flow is critical for maintaining a positive user experience and protecting player progress.

## 2.4 Participants

### 2.4.1 Application Service

#### 2.4.1.1 Repository Id

REPO-AS-005

#### 2.4.1.2 Display Name

GameSessionService

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
| Stereotype | Service |

### 2.4.2.0 Infrastructure Repository

#### 2.4.2.1 Repository Id

REPO-IP-SG-008

#### 2.4.2.2 Display Name

GameSaveRepository

#### 2.4.2.3 Type

🔹 Infrastructure Repository

#### 2.4.2.4 Technology

.NET 8 C#, System.Text.Json

#### 2.4.2.5 Order

2

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #4682B4 |
| Stereotype | Repository |

### 2.4.3.0 Infrastructure Service

#### 2.4.3.1 Repository Id

REPO-IP-DM-010

#### 2.4.3.2 Display Name

DataMigrationManager

#### 2.4.3.3 Type

🔹 Infrastructure Service

#### 2.4.3.4 Technology

.NET 8 C#

#### 2.4.3.5 Order

3

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #8A2BE2 |
| Stereotype | Service |

### 2.4.4.0 Infrastructure Service

#### 2.4.4.1 Repository Id

REPO-IL-006

#### 2.4.4.2 Display Name

LoggingService

#### 2.4.4.3 Type

🔹 Infrastructure Service

#### 2.4.4.4 Technology

Serilog

#### 2.4.4.5 Order

4

#### 2.4.4.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #5F9EA0 |
| Stereotype | Logger |

## 2.5.0.0 Interactions

### 2.5.1.0 Asynchronous Method Call

#### 2.5.1.1 Source Id

REPO-AS-005

#### 2.5.1.2 Target Id

REPO-IP-SG-008

#### 2.5.1.3 Message

LoadGameAsync(slotNumber)

#### 2.5.1.4 Sequence Number

1

#### 2.5.1.5 Type

🔹 Asynchronous Method Call

#### 2.5.1.6 Is Synchronous

❌ No

#### 2.5.1.7 Return Message

Task<GameState>

#### 2.5.1.8 Has Return

✅ Yes

#### 2.5.1.9 Is Activation

✅ Yes

#### 2.5.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | Task<GameState> LoadGameAsync(int slotNumber) |
| Parameters | slotNumber: The integer identifier for the save sl... |
| Authentication | N/A (Internal call) |
| Error Handling | Catches exceptions from repository, such as `FileN... |
| Performance | This call is subject to the overall 10s load time ... |

### 2.5.2.0 Logging Call

#### 2.5.2.1 Source Id

REPO-IP-SG-008

#### 2.5.2.2 Target Id

REPO-IL-006

#### 2.5.2.3 Message

Information("Attempting to load and validate save slot {slot}", slotNumber)

#### 2.5.2.4 Sequence Number

2

#### 2.5.2.5 Type

🔹 Logging Call

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
| Protocol | In-Process |
| Method | void Information(string template, params object[] ... |
| Parameters | Message template and structured data for logging. |
| Authentication | N/A |
| Error Handling | Logging failures are non-critical and should not i... |
| Performance | Negligible impact. |

### 2.5.3.0 Internal Operation

#### 2.5.3.1 Source Id

REPO-IP-SG-008

#### 2.5.3.2 Target Id

REPO-IP-SG-008

#### 2.5.3.3 Message

Read file and check version property

#### 2.5.3.4 Sequence Number

3

#### 2.5.3.5 Type

🔹 Internal Operation

#### 2.5.3.6 Is Synchronous

✅ Yes

#### 2.5.3.7 Return Message

isLegacy: bool, rawContent: byte[]

#### 2.5.3.8 Has Return

✅ Yes

#### 2.5.3.9 Is Activation

❌ No

#### 2.5.3.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | File System I/O |
| Method | Reads the specified save file. Performs a partial ... |
| Parameters | filePath: Path to the .json save file. |
| Authentication | N/A |
| Error Handling | Handles `FileNotFoundException`, `IOException`. Fa... |
| Performance | Must be very fast (<100ms) to quickly determine if... |

### 2.5.4.0 Asynchronous Method Call

#### 2.5.4.1 Source Id

REPO-IP-SG-008

#### 2.5.4.2 Target Id

REPO-IP-DM-010

#### 2.5.4.3 Message

MigrateSaveDataAsync(rawContent, sourceVersion)

#### 2.5.4.4 Sequence Number

4

#### 2.5.4.5 Type

🔹 Asynchronous Method Call

#### 2.5.4.6 Is Synchronous

❌ No

#### 2.5.4.7 Return Message

Task<byte[]>

#### 2.5.4.8 Has Return

✅ Yes

#### 2.5.4.9 Is Activation

✅ Yes

#### 2.5.4.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | Task<byte[]> MigrateSaveDataAsync(byte[] rawData, ... |
| Parameters | rawData: The full content of the legacy save file.... |
| Authentication | N/A |
| Error Handling | Propagates `MigrationFailedException` if the atomi... |
| Performance | This is the most performance-intensive part of the... |

### 2.5.5.0 Logging Call

#### 2.5.5.1 Source Id

REPO-IP-DM-010

#### 2.5.5.2 Target Id

REPO-IL-006

#### 2.5.5.3 Message

Information("Starting data migration from v{source} to v{target}")

#### 2.5.5.4 Sequence Number

5

#### 2.5.5.5 Type

🔹 Logging Call

#### 2.5.5.6 Is Synchronous

✅ Yes

#### 2.5.5.7 Return Message



#### 2.5.5.8 Has Return

❌ No

#### 2.5.5.9 Is Activation

❌ No

#### 2.5.5.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | void Information(string template, params object[] ... |
| Parameters | Logs the start of the operation with a shared Corr... |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Negligible. |

### 2.5.6.0 Internal Operation

#### 2.5.6.1 Source Id

REPO-IP-DM-010

#### 2.5.6.2 Target Id

REPO-IP-DM-010

#### 2.5.6.3 Message

Execute atomic file migration operation

#### 2.5.6.4 Sequence Number

6

#### 2.5.6.5 Type

🔹 Internal Operation

#### 2.5.6.6 Is Synchronous

✅ Yes

#### 2.5.6.7 Return Message

migratedData: byte[]

#### 2.5.6.8 Has Return

✅ Yes

#### 2.5.6.9 Is Activation

❌ No

#### 2.5.6.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | File System I/O |
| Method | A private method wrapping the entire atomic proces... |
| Parameters | N/A |
| Authentication | N/A |
| Error Handling | A top-level try-catch block is mandatory. On any e... |
| Performance | File I/O and deserialization/serialization are the... |

#### 2.5.6.11 Nested Interactions

- {'sourceId': 'REPO-IP-DM-010', 'targetId': 'REPO-IL-006', 'message': 'Error(ex, "Migration failed. Initiating rollback.") OR Information("Migration Succeeded.")', 'sequenceNumber': 6.1, 'type': 'Conditional Logging', 'isSynchronous': True, 'returnMessage': '', 'hasReturn': False, 'isActivation': False, 'technicalDetails': {'protocol': 'In-Process', 'method': 'Varies based on outcome.', 'parameters': 'Includes Exception details and CorrelationId on failure.', 'authentication': 'N/A', 'errorHandling': 'N/A', 'performance': 'N/A'}}

### 2.5.7.0 Asynchronous Return

#### 2.5.7.1 Source Id

REPO-IP-DM-010

#### 2.5.7.2 Target Id

REPO-IP-SG-008

#### 2.5.7.3 Message

Return migratedData

#### 2.5.7.4 Sequence Number

7

#### 2.5.7.5 Type

🔹 Asynchronous Return

#### 2.5.7.6 Is Synchronous

❌ No

#### 2.5.7.7 Return Message



#### 2.5.7.8 Has Return

❌ No

#### 2.5.7.9 Is Activation

❌ No

#### 2.5.7.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | N/A |
| Parameters | The byte array of the now-current save file. |
| Authentication | N/A |
| Error Handling | If an exception was thrown, it is propagated here. |
| Performance | N/A |

### 2.5.8.0 Internal Operation

#### 2.5.8.1 Source Id

REPO-IP-SG-008

#### 2.5.8.2 Target Id

REPO-IP-SG-008

#### 2.5.8.3 Message

Deserialize<GameState>(migratedData)

#### 2.5.8.4 Sequence Number

8

#### 2.5.8.5 Type

🔹 Internal Operation

#### 2.5.8.6 Is Synchronous

✅ Yes

#### 2.5.8.7 Return Message

gameState: GameState

#### 2.5.8.8 Has Return

✅ Yes

#### 2.5.8.9 Is Activation

❌ No

#### 2.5.8.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | System.Text.Json.JsonSerializer.Deserialize<GameSt... |
| Parameters | The byte array of the current-version save file. |
| Authentication | N/A |
| Error Handling | Handles `JsonException`. A failure at this point a... |
| Performance | Deserialization of the full game state can be cost... |

### 2.5.9.0 Asynchronous Return

#### 2.5.9.1 Source Id

REPO-IP-SG-008

#### 2.5.9.2 Target Id

REPO-AS-005

#### 2.5.9.3 Message

Return gameState

#### 2.5.9.4 Sequence Number

9

#### 2.5.9.5 Type

🔹 Asynchronous Return

#### 2.5.9.6 Is Synchronous

❌ No

#### 2.5.9.7 Return Message



#### 2.5.9.8 Has Return

❌ No

#### 2.5.9.9 Is Activation

❌ No

#### 2.5.9.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | N/A |
| Parameters | The fully deserialized `GameState` object, ready f... |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | N/A |

## 2.6.0.0 Notes

### 2.6.1.0 Content

#### 2.6.1.1 Content

The atomicity of the migration is the most critical aspect. The backup-migrate-overwrite-delete sequence must be wrapped in a robust error handling block to prevent data loss. The original file is the source of truth until the entire process completes successfully.

#### 2.6.1.2 Position

top

#### 2.6.1.3 Participant Id

REPO-IP-DM-010

#### 2.6.1.4 Sequence Number

6

### 2.6.2.0 Content

#### 2.6.2.1 Content

A 'MigrationFailedException' should be a custom exception type containing context like source/target versions and the inner exception to aid debugging from log files.

#### 2.6.2.2 Position

bottom

#### 2.6.2.3 Participant Id

*Not specified*

#### 2.6.2.4 Sequence Number

*Not specified*

## 2.7.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | File system access must be strictly limited to the... |
| Performance Targets | The entire migration and load process must complet... |
| Error Handling Strategy | The process MUST be atomic per REQ-1-090. A `try-c... |
| Testing Considerations | Requires a comprehensive suite of legacy save file... |
| Monitoring Requirements | Log key operational events at `INFO` level: 'Migra... |
| Deployment Considerations | Each new application version that introduces a bre... |

