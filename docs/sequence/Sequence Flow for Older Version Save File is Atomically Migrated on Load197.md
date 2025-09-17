# 1 Overview

## 1.1 Diagram Id

SEQ-OF-001

## 1.2 Name

Older Version Save File is Atomically Migrated on Load

## 1.3 Description

When a user attempts to load a save file from a previous game version, the system detects the version mismatch and triggers an atomic migration process. This process creates a backup, applies transformations, and only commits the changes upon success, ensuring no data loss.

## 1.4 Type

🔹 OperationalFlow

## 1.5 Purpose

To preserve user progress across game updates, ensuring a smooth user experience and maintaining player engagement after a new version is installed.

## 1.6 Complexity

High

## 1.7 Priority

🟡 Medium

## 1.8 Frequency

OnDemand

## 1.9 Participants

- REPO-IL-006

## 1.10 Key Interactions

- Infrastructure Layer's GameSaveRepository detects an older 'gameVersion' in a save file.
- It invokes the DataMigrationManager, passing the file data.
- The DataMigrationManager creates a temporary backup of the original file.
- It applies a series of version-specific transformation steps in memory to update the data structure.
- If any step fails, the process aborts and the backup is restored, leaving the original file untouched.
- On success, the migrated data is passed back to the GameSaveRepository for standard deserialization.
- The updated data is then written back to the original file path.

## 1.11 Triggers

- An attempt to load a save file with an older 'gameVersion' property.

## 1.12 Outcomes

- The user's old save data is successfully and safely upgraded to the current format.
- The game loads successfully, preserving the user's progress.

## 1.13 Business Rules

- The application must include logic to detect and handle older versions of save files (REQ-1-090).
- The migration process must be atomic; it must fully succeed or leave the original data untouched (REQ-1-100).
- No backward migration (from a newer version to an older one) is supported (REQ-1-100).

## 1.14 Error Scenarios

- The migration logic for a specific version path fails, causing the process to abort and revert.
- File permissions prevent the creation of a backup or writing the migrated file.

## 1.15 Integration Points

- Local File System

# 2.0 Details

## 2.1 Diagram Id

SEQ-OF-001

## 2.2 Name

Atomic Save File Migration on Load

## 2.3 Description

This sequence details the atomic operational flow for detecting and migrating an older version save file upon a user's load request. The process guarantees that the user's original save data is either successfully migrated to the current version or left completely untouched in case of any failure, thereby preserving data integrity as per REQ-1-090 and REQ-1-100.

## 2.4 Participants

### 2.4.1 UI Component (e.g., LoadGameMenu)

#### 2.4.1.1 Repository Id

REPO-PL-007

#### 2.4.1.2 Display Name

Presentation Layer

#### 2.4.1.3 Type

🔹 UI Component (e.g., LoadGameMenu)

#### 2.4.1.4 Technology

Unity Engine, C#

#### 2.4.1.5 Order

1

#### 2.4.1.6 Style

| Property | Value |
|----------|-------|
| Shape | actor |
| Color | #4287f5 |
| Stereotype | UI |

### 2.4.2.0 Application Service

#### 2.4.2.1 Repository Id

REPO-AS-005

#### 2.4.2.2 Display Name

GameSessionService

#### 2.4.2.3 Type

🔹 Application Service

#### 2.4.2.4 Technology

.NET 8, C#

#### 2.4.2.5 Order

2

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #42f5ad |
| Stereotype | Service |

### 2.4.3.0 Infrastructure Repository

#### 2.4.3.1 Repository Id

REPO-IP-SG-008

#### 2.4.3.2 Display Name

GameSaveRepository

#### 2.4.3.3 Type

🔹 Infrastructure Repository

#### 2.4.3.4 Technology

.NET 8, C#, System.Text.Json

#### 2.4.3.5 Order

3

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | database |
| Color | #f5a142 |
| Stereotype | Repository |

### 2.4.4.0 Infrastructure Service

#### 2.4.4.1 Repository Id

REPO-IL-006

#### 2.4.4.2 Display Name

DataMigrationManager

#### 2.4.4.3 Type

🔹 Infrastructure Service

#### 2.4.4.4 Technology

.NET 8, C#

#### 2.4.4.5 Order

4

#### 2.4.4.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #f56e42 |
| Stereotype | Manager |

### 2.4.5.0 Infrastructure Service

#### 2.4.5.1 Repository Id

REPO-IL-006

#### 2.4.5.2 Display Name

LoggingService

#### 2.4.5.3 Type

🔹 Infrastructure Service

#### 2.4.5.4 Technology

Serilog

#### 2.4.5.5 Order

5

#### 2.4.5.6 Style

| Property | Value |
|----------|-------|
| Shape | note |
| Color | #cccccc |
| Stereotype | Logger |

## 2.5.0.0 Interactions

### 2.5.1.0 Request

#### 2.5.1.1 Source Id

REPO-PL-007

#### 2.5.1.2 Target Id

REPO-AS-005

#### 2.5.1.3 Message

User initiates game load for a specific slot.

#### 2.5.1.4 Sequence Number

1

#### 2.5.1.5 Type

🔹 Request

#### 2.5.1.6 Is Synchronous

✅ Yes

#### 2.5.1.7 Return Message

Returns loaded GameState or an error status to UI.

#### 2.5.1.8 Has Return

✅ Yes

#### 2.5.1.9 Is Activation

✅ Yes

#### 2.5.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Call |
| Method | HandleLoadGameRequest(int slotId) |
| Parameters | slotId: The integer identifier for the save slot t... |
| Authentication | N/A (Local Application) |
| Error Handling | Receives and handles exceptions propagated from lo... |
| Performance | N/A |

### 2.5.2.0 Method Call

#### 2.5.2.1 Source Id

REPO-AS-005

#### 2.5.2.2 Target Id

REPO-IP-SG-008

#### 2.5.2.3 Message

Requests the game state from the specified save slot.

#### 2.5.2.4 Sequence Number

2

#### 2.5.2.5 Type

🔹 Method Call

#### 2.5.2.6 Is Synchronous

✅ Yes

#### 2.5.2.7 Return Message

Returns a fully deserialized and validated GameState object.

#### 2.5.2.8 Has Return

✅ Yes

#### 2.5.2.9 Is Activation

✅ Yes

#### 2.5.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Call |
| Method | Task<GameState> LoadAsync(int slotId) |
| Parameters | slotId: The integer identifier for the save slot. |
| Authentication | N/A |
| Error Handling | Catches potential DataMigrationException or IOExce... |
| Performance | This entire call must complete within the 10-secon... |

### 2.5.3.0 File I/O

#### 2.5.3.1 Source Id

REPO-IP-SG-008

#### 2.5.3.2 Target Id

REPO-IP-SG-008

#### 2.5.3.3 Message

Reads raw save file content and peeks at version metadata.

#### 2.5.3.4 Sequence Number

3

#### 2.5.3.5 Type

🔹 File I/O

#### 2.5.3.6 Is Synchronous

✅ Yes

#### 2.5.3.7 Return Message

Returns raw JSON string and detected version.

#### 2.5.3.8 Has Return

✅ Yes

#### 2.5.3.9 Is Activation

❌ No

#### 2.5.3.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | File System API |
| Method | File.ReadAllTextAsync(filePath) |
| Parameters | filePath: Path to the save file, e.g., '%APPDATA%/... |
| Authentication | OS File Permissions |
| Error Handling | Throws IOException if file is unreadable or does n... |
| Performance | Must be highly performant on SSDs. |

### 2.5.4.0 Conditional Logic

#### 2.5.4.1 Source Id

REPO-IP-SG-008

#### 2.5.4.2 Target Id

REPO-IP-SG-008

#### 2.5.4.3 Message

[Conditional] If fileVersion < currentAppVersion, migration is required.

#### 2.5.4.4 Sequence Number

4

#### 2.5.4.5 Type

🔹 Conditional Logic

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
| Protocol | In-Process Logic |
| Method | Internal version comparison logic. |
| Parameters | fileVersion (string), currentAppVersion (string) |
| Authentication | N/A |
| Error Handling | If fileVersion > currentAppVersion, throws an Inco... |
| Performance | Negligible. |

### 2.5.5.0 Logging

#### 2.5.5.1 Source Id

REPO-IP-SG-008

#### 2.5.5.2 Target Id

REPO-IL-006

#### 2.5.5.3 Message

Logs warning about version mismatch and initiates migration.

#### 2.5.5.4 Sequence Number

5

#### 2.5.5.5 Type

🔹 Logging

#### 2.5.5.6 Is Synchronous

❌ No

#### 2.5.5.7 Return Message



#### 2.5.5.8 Has Return

❌ No

#### 2.5.5.9 Is Activation

❌ No

#### 2.5.5.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Call |
| Method | Log.Warning("Old save version {oldVersion} detecte... |
| Parameters | Log context properties including file path, old ve... |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Asynchronous to not block the main thread. |

### 2.5.6.0 Method Call

#### 2.5.6.1 Source Id

REPO-IP-SG-008

#### 2.5.6.2 Target Id

REPO-IL-006

#### 2.5.6.3 Message

Delegates the migration process for the raw data.

#### 2.5.6.4 Sequence Number

6

#### 2.5.6.5 Type

🔹 Method Call

#### 2.5.6.6 Is Synchronous

✅ Yes

#### 2.5.6.7 Return Message

Returns the migrated raw JSON string upon success.

#### 2.5.6.8 Has Return

✅ Yes

#### 2.5.6.9 Is Activation

✅ Yes

#### 2.5.6.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Call |
| Method | Task<string> MigrateSaveDataAsync(string rawJsonDa... |
| Parameters | rawJsonData: The full content of the old save file... |
| Authentication | N/A |
| Error Handling | Catches DataMigrationException and propagates it. |
| Performance | The core of the performance budget. Must be highly... |

### 2.5.7.0 File I/O

#### 2.5.7.1 Source Id

REPO-IL-006

#### 2.5.7.2 Target Id

REPO-IL-006

#### 2.5.7.3 Message

Creates a temporary backup of the original save file.

#### 2.5.7.4 Sequence Number

7

#### 2.5.7.5 Type

🔹 File I/O

#### 2.5.7.6 Is Synchronous

✅ Yes

#### 2.5.7.7 Return Message



#### 2.5.7.8 Has Return

❌ No

#### 2.5.7.9 Is Activation

❌ No

#### 2.5.7.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | File System API |
| Method | File.Copy(sourceFilePath, backupFilePath) |
| Parameters | backupFilePath: e.g., 'save_1.json.bak' |
| Authentication | OS File Permissions |
| Error Handling | Throws IOException if permissions are insufficient... |
| Performance | Fast on local storage. |

### 2.5.8.0 Data Transformation

#### 2.5.8.1 Source Id

REPO-IL-006

#### 2.5.8.2 Target Id

REPO-IL-006

#### 2.5.8.3 Message

[Loop] Applies version-specific data transformation steps in memory.

#### 2.5.8.4 Sequence Number

8

#### 2.5.8.5 Type

🔹 Data Transformation

#### 2.5.8.6 Is Synchronous

✅ Yes

#### 2.5.8.7 Return Message

Returns transformed data for the next step.

#### 2.5.8.8 Has Return

✅ Yes

#### 2.5.8.9 Is Activation

❌ No

#### 2.5.8.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Logic |
| Method | ApplyMigration_V1_to_V2(JObject data) |
| Parameters | data: A mutable representation of the JSON data (e... |
| Authentication | N/A |
| Error Handling | If a transformation rule fails (e.g., missing fiel... |
| Performance | Must be efficient to stay within the load time bud... |

#### 2.5.8.11 Nested Interactions

##### 2.5.8.11.1 Exception Handling

###### 2.5.8.11.1.1 Source Id

REPO-IL-006

###### 2.5.8.11.1.2 Target Id

REPO-IL-006

###### 2.5.8.11.1.3 Message

[Failure] If any step throws an exception, catch it.

###### 2.5.8.11.1.4 Sequence Number

8.1

###### 2.5.8.11.1.5 Type

🔹 Exception Handling

###### 2.5.8.11.1.6 Is Synchronous

✅ Yes

###### 2.5.8.11.1.7 Return Message



###### 2.5.8.11.1.8 Has Return

❌ No

###### 2.5.8.11.1.9 Is Activation

❌ No

###### 2.5.8.11.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | try-catch block |
| Method |  |
| Parameters |  |
| Authentication |  |
| Error Handling |  |
| Performance |  |

##### 2.5.8.11.2.0 File I/O

###### 2.5.8.11.2.1 Source Id

REPO-IL-006

###### 2.5.8.11.2.2 Target Id

REPO-IL-006

###### 2.5.8.11.2.3 Message

Restore the original file from the temporary backup.

###### 2.5.8.11.2.4 Sequence Number

8.2

###### 2.5.8.11.2.5 Type

🔹 File I/O

###### 2.5.8.11.2.6 Is Synchronous

✅ Yes

###### 2.5.8.11.2.7 Return Message



###### 2.5.8.11.2.8 Has Return

❌ No

###### 2.5.8.11.2.9 Is Activation

❌ No

###### 2.5.8.11.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | File System API |
| Method | File.Move(backupFilePath, sourceFilePath, overwrit... |
| Parameters |  |
| Authentication | OS File Permissions |
| Error Handling | If restore fails, the system is in a critical stat... |
| Performance |  |

##### 2.5.8.11.3.0 File I/O

###### 2.5.8.11.3.1 Source Id

REPO-IL-006

###### 2.5.8.11.3.2 Target Id

REPO-IL-006

###### 2.5.8.11.3.3 Message

Clean up the backup file.

###### 2.5.8.11.3.4 Sequence Number

8.3

###### 2.5.8.11.3.5 Type

🔹 File I/O

###### 2.5.8.11.3.6 Is Synchronous

✅ Yes

###### 2.5.8.11.3.7 Return Message



###### 2.5.8.11.3.8 Has Return

❌ No

###### 2.5.8.11.3.9 Is Activation

❌ No

###### 2.5.8.11.3.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | File System API |
| Method | File.Delete(backupFilePath) |
| Parameters |  |
| Authentication |  |
| Error Handling | Log a warning if cleanup fails, but do not fail th... |
| Performance |  |

##### 2.5.8.11.4.0 Exception Throw

###### 2.5.8.11.4.1 Source Id

REPO-IL-006

###### 2.5.8.11.4.2 Target Id

REPO-IP-SG-008

###### 2.5.8.11.4.3 Message

Throw DataMigrationException to signal failure.

###### 2.5.8.11.4.4 Sequence Number

8.4

###### 2.5.8.11.4.5 Type

🔹 Exception Throw

###### 2.5.8.11.4.6 Is Synchronous

✅ Yes

###### 2.5.8.11.4.7 Return Message



###### 2.5.8.11.4.8 Has Return

❌ No

###### 2.5.8.11.4.9 Is Activation

❌ No

###### 2.5.8.11.4.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | C# Exception |
| Method | throw new DataMigrationException(...) |
| Parameters |  |
| Authentication |  |
| Error Handling |  |
| Performance |  |

### 2.5.9.0.0.0 File I/O

#### 2.5.9.1.0.0 Source Id

REPO-IL-006

#### 2.5.9.2.0.0 Target Id

REPO-IL-006

#### 2.5.9.3.0.0 Message

Deletes the temporary backup file upon successful migration.

#### 2.5.9.4.0.0 Sequence Number

9

#### 2.5.9.5.0.0 Type

🔹 File I/O

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
| Protocol | File System API |
| Method | File.Delete(backupFilePath) |
| Parameters |  |
| Authentication | OS File Permissions |
| Error Handling | Log a warning if cleanup fails, but do not fail th... |
| Performance | Fast on local storage. |

### 2.5.10.0.0.0 File I/O

#### 2.5.10.1.0.0 Source Id

REPO-IP-SG-008

#### 2.5.10.2.0.0 Target Id

REPO-IP-SG-008

#### 2.5.10.3.0.0 Message

Atomically overwrites the original file with the migrated data.

#### 2.5.10.4.0.0 Sequence Number

10

#### 2.5.10.5.0.0 Type

🔹 File I/O

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
| Protocol | File System API |
| Method | File.WriteAllTextAsync(filePath, migratedJsonData) |
| Parameters | filePath: The original save file path. |
| Authentication | OS File Permissions |
| Error Handling | Throws IOException if write fails. This is a criti... |
| Performance | Fast on local storage. |

### 2.5.11.0.0.0 Data Deserialization

#### 2.5.11.1.0.0 Source Id

REPO-IP-SG-008

#### 2.5.11.2.0.0 Target Id

REPO-IP-SG-008

#### 2.5.11.3.0.0 Message

Deserializes the newly migrated JSON data into the GameState object.

#### 2.5.11.4.0.0 Sequence Number

11

#### 2.5.11.5.0.0 Type

🔹 Data Deserialization

#### 2.5.11.6.0.0 Is Synchronous

✅ Yes

#### 2.5.11.7.0.0 Return Message

The fully hydrated GameState object.

#### 2.5.11.8.0.0 Has Return

✅ Yes

#### 2.5.11.9.0.0 Is Activation

❌ No

#### 2.5.11.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Call |
| Method | System.Text.Json.JsonSerializer.Deserialize<GameSt... |
| Parameters | migratedJsonData: The now up-to-date JSON string. |
| Authentication | N/A |
| Error Handling | Throws JsonException if the migrated data is malfo... |
| Performance | Should be highly performant. |

## 2.6.0.0.0.0 Notes

### 2.6.1.0.0.0 Content

#### 2.6.1.1.0.0 Content



```
Atomic Operation Guarantee (REQ-1-100):
The entire migration process is wrapped in a try/catch/finally block. A temporary backup is created before any modifications. If any step fails, the backup is restored, leaving the original file exactly as it was. The backup is deleted only after a fully successful migration.
```

#### 2.6.1.2.0.0 Position

RightOf

#### 2.6.1.3.0.0 Participant Id

REPO-IL-006

#### 2.6.1.4.0.0 Sequence Number

7

### 2.6.2.0.0.0 Content

#### 2.6.2.1.0.0 Content



```
Test Data Requirement (REQ-1-027):
QA must maintain a library of save files from every previously released version to test all possible migration paths during regression testing for a new release. This is critical to prevent data loss for long-term players.
```

#### 2.6.2.2.0.0 Position

Bottom

#### 2.6.2.3.0.0 Participant Id

*Not specified*

#### 2.6.2.4.0.0 Sequence Number

11

## 2.7.0.0.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | The application must have read/write permissions t... |
| Performance Targets | The entire load process, including detection, migr... |
| Error Handling Strategy | The process is designed for atomicity. A `DataMigr... |
| Testing Considerations | 1. Create integration tests for each version-to-ve... |
| Monitoring Requirements | Leverage structured logging (Serilog) extensively.... |
| Deployment Considerations | With each new release that includes a save file sc... |

