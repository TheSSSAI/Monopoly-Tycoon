# 1 Overview

## 1.1 Diagram Id

SEQ-RC-009

## 1.2 Name

Statistics Database Corruption and Recovery

## 1.3 Description

On application startup, the system detects that the SQLite statistics database is corrupt. It automatically attempts to restore from the most recent valid backup. If all backups fail, it prompts the user with an option to reset their data to a clean state.

## 1.4 Type

🔹 RecoveryFlow

## 1.5 Purpose

To protect user data against corruption and provide a path to recovery, enhancing application reliability (REQ-1-089).

## 1.6 Complexity

Medium

## 1.7 Priority

🟡 Medium

## 1.8 Frequency

OnDemand

## 1.9 Participants

- InfrastructureLayer
- PresentationLayer

## 1.10 Key Interactions

- On startup, the StatisticsRepository fails to open the SQLite file due to corruption.
- The repository logic iterates through its three backup files, from newest to oldest.
- It attempts to replace the corrupt file with a backup and re-establish a connection.
- If a backup works, the process continues silently.
- If all backups fail, the repository informs the Presentation Layer of the unrecoverable error.
- The UI presents a dialog to the user, offering to reset their statistics or exit the application.

## 1.11 Triggers

- Application startup, when the statistics database is first accessed.

## 1.12 Outcomes

- The user's statistics are successfully and transparently restored from a backup.
- OR, the user is prompted to reset their statistics, allowing them to continue using the application.

## 1.13 Business Rules

- The system must automatically create backups of the statistics database (REQ-1-089).
- The system shall retain the three most recent backups (REQ-1-089).

## 1.14 Error Scenarios

- Both the primary database file and all available backups are corrupt.

## 1.15 Integration Points

- Local File System

# 2.0 Details

## 2.1 Diagram Id

SEQ-RC-009

## 2.2 Name

Statistics Database Corruption Detection and Automated Recovery

## 2.3 Description

Implementation sequence for detecting a corrupt SQLite statistics database on application startup. The sequence details the automated, tiered recovery process by attempting to restore from the three most recent backups as per REQ-1-089. If all automated recovery attempts fail, it specifies the graceful failure path, which involves notifying the user and providing actionable recovery options (reset or exit).

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
| Shape | component |
| Color | #F2D0A4 |
| Stereotype | Service |

### 2.4.2.0 Infrastructure Repository

#### 2.4.2.1 Repository Id

REPO-IP-ST-009

#### 2.4.2.2 Display Name

StatisticsRepository

#### 2.4.2.3 Type

🔹 Infrastructure Repository

#### 2.4.2.4 Technology

Microsoft.Data.Sqlite

#### 2.4.2.5 Order

2

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | database |
| Color | #B4D6D1 |
| Stereotype | Repository |

### 2.4.3.0 Infrastructure Service

#### 2.4.3.1 Repository Id

REPO-IL-006

#### 2.4.3.2 Display Name

LoggingService

#### 2.4.3.3 Type

🔹 Infrastructure Service

#### 2.4.3.4 Technology

Serilog

#### 2.4.3.5 Order

3

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #D3D3D3 |
| Stereotype | Logger |

### 2.4.4.0 UI Layer

#### 2.4.4.1 Repository Id

presentation_layer

#### 2.4.4.2 Display Name

PresentationLayer

#### 2.4.4.3 Type

🔹 UI Layer

#### 2.4.4.4 Technology

Unity Engine

#### 2.4.4.5 Order

4

#### 2.4.4.6 Style

| Property | Value |
|----------|-------|
| Shape | actor |
| Color | #C8B6E2 |
| Stereotype | UI |

## 2.5.0.0 Interactions

### 2.5.1.0 Method Call

#### 2.5.1.1 Source Id

REPO-AS-005

#### 2.5.1.2 Target Id

REPO-IP-ST-009

#### 2.5.1.3 Message

1. InitializeDatabaseAsync()

#### 2.5.1.4 Sequence Number

1

#### 2.5.1.5 Type

🔹 Method Call

#### 2.5.1.6 Is Synchronous

✅ Yes

#### 2.5.1.7 Return Message

Returns Task on success, or throws UnrecoverableDataException on failure.

#### 2.5.1.8 Has Return

✅ Yes

#### 2.5.1.9 Is Activation

✅ Yes

#### 2.5.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | Task InitializeDatabaseAsync() |
| Parameters | None |
| Authentication | N/A |
| Error Handling | Catches SqliteException to initiate recovery. Thro... |
| Performance | RTO: < 2 seconds for entire recovery check on SSD. |

### 2.5.2.0 Logging

#### 2.5.2.1 Source Id

REPO-IP-ST-009

#### 2.5.2.2 Target Id

REPO-IL-006

#### 2.5.2.3 Message

2. Log(INFO, 'Initializing statistics database...')

#### 2.5.2.4 Sequence Number

2

#### 2.5.2.5 Type

🔹 Logging

#### 2.5.2.6 Is Synchronous

❌ No

#### 2.5.2.7 Has Return

❌ No

#### 2.5.2.8 Is Activation

❌ No

#### 2.5.2.9 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | ILogger.Information(string template, ...) |
| Parameters | Message template and properties. |
| Authentication | N/A |
| Error Handling | Logging failures are handled silently by the Seril... |
| Performance | Negligible |

### 2.5.3.0 Internal Operation

#### 2.5.3.1 Source Id

REPO-IP-ST-009

#### 2.5.3.2 Target Id

REPO-IP-ST-009

#### 2.5.3.3 Message

3. Attempt to open SQLite connection. Catches SqliteException.

#### 2.5.3.4 Sequence Number

3

#### 2.5.3.5 Type

🔹 Internal Operation

#### 2.5.3.6 Is Synchronous

✅ Yes

#### 2.5.3.7 Return Message

Connection object on success, SqliteException on corruption.

#### 2.5.3.8 Has Return

✅ Yes

#### 2.5.3.9 Is Activation

❌ No

#### 2.5.3.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Internal |
| Method | new SqliteConnection(connectionString).Open() |
| Parameters | Connection string pointing to 'stats.db'. |
| Authentication | N/A |
| Error Handling | A try-catch block specifically for `SqliteExceptio... |
| Performance | < 50ms |

### 2.5.4.0 Logging

#### 2.5.4.1 Source Id

REPO-IP-ST-009

#### 2.5.4.2 Target Id

REPO-IL-006

#### 2.5.4.3 Message

4. [on Exception] Log(WARN, 'Primary database corrupt. Starting recovery...')

#### 2.5.4.4 Sequence Number

4

#### 2.5.4.5 Type

🔹 Logging

#### 2.5.4.6 Is Synchronous

❌ No

#### 2.5.4.7 Has Return

❌ No

#### 2.5.4.8 Is Activation

❌ No

#### 2.5.4.9 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | ILogger.Warning(string template, ...) |
| Parameters | Message template, exception details. |
| Authentication | N/A |
| Error Handling | Silent failure. |
| Performance | Negligible |

### 2.5.5.0 Loop

#### 2.5.5.1 Source Id

REPO-IP-ST-009

#### 2.5.5.2 Target Id

REPO-IP-ST-009

#### 2.5.5.3 Message

5. [loop: for each backup file from newest to oldest (3 attempts)]

#### 2.5.5.4 Sequence Number

5

#### 2.5.5.5 Type

🔹 Loop

#### 2.5.5.6 Is Synchronous

✅ Yes

#### 2.5.5.7 Has Return

❌ No

#### 2.5.5.8 Is Activation

❌ No

#### 2.5.5.9 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Internal |
| Method | for(int i = 3; i > 0; i--) |
| Parameters | Backup file paths (e.g., 'stats.db.bak3', 'stats.d... |
| Authentication | N/A |
| Error Handling | Loop continues to next iteration if a backup is al... |
| Performance | Entire loop should be < 1 second. |

#### 2.5.5.10 Nested Interactions

##### 2.5.5.10.1 Logging

###### 2.5.5.10.1.1 Source Id

REPO-IP-ST-009

###### 2.5.5.10.1.2 Target Id

REPO-IL-006

###### 2.5.5.10.1.3 Message

5.1. Log(INFO, 'Attempting restore from {BackupFile}...')

###### 2.5.5.10.1.4 Sequence Number

6

###### 2.5.5.10.1.5 Type

🔹 Logging

###### 2.5.5.10.1.6 Is Synchronous

❌ No

###### 2.5.5.10.1.7 Has Return

❌ No

###### 2.5.5.10.1.8 Is Activation

❌ No

###### 2.5.5.10.1.9 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | ILogger.Information(string template, ...) |
| Parameters | Template with backup file name. |
| Authentication | N/A |
| Error Handling | Silent failure. |
| Performance | Negligible |

##### 2.5.5.10.2.0 File I/O

###### 2.5.5.10.2.1 Source Id

REPO-IP-ST-009

###### 2.5.5.10.2.2 Target Id

REPO-IP-ST-009

###### 2.5.5.10.2.3 Message

5.2. Atomically replace corrupt DB with backup file.

###### 2.5.5.10.2.4 Sequence Number

7

###### 2.5.5.10.2.5 Type

🔹 File I/O

###### 2.5.5.10.2.6 Is Synchronous

✅ Yes

###### 2.5.5.10.2.7 Has Return

❌ No

###### 2.5.5.10.2.8 Is Activation

❌ No

###### 2.5.5.10.2.9 Technical Details

| Property | Value |
|----------|-------|
| Protocol | File System API |
| Method | File.Replace(source, destination, destinationBacku... |
| Parameters | Backup file path, primary DB path. |
| Authentication | N/A |
| Error Handling | File I/O exceptions are caught and logged; loop co... |
| Performance | Disk I/O dependent, typically < 100ms. |

##### 2.5.5.10.3.0 Internal Operation

###### 2.5.5.10.3.1 Source Id

REPO-IP-ST-009

###### 2.5.5.10.3.2 Target Id

REPO-IP-ST-009

###### 2.5.5.10.3.3 Message

5.3. Re-attempt to open SQLite connection.

###### 2.5.5.10.3.4 Sequence Number

8

###### 2.5.5.10.3.5 Type

🔹 Internal Operation

###### 2.5.5.10.3.6 Is Synchronous

✅ Yes

###### 2.5.5.10.3.7 Has Return

✅ Yes

###### 2.5.5.10.3.8 Is Activation

❌ No

###### 2.5.5.10.3.9 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Internal |
| Method | new SqliteConnection(connectionString).Open() |
| Parameters | Connection string. |
| Authentication | N/A |
| Error Handling | Catches `SqliteException` to determine if this bac... |
| Performance | < 50ms |

##### 2.5.5.10.4.0 Alternate Flow

###### 2.5.5.10.4.1 Source Id

REPO-IP-ST-009

###### 2.5.5.10.4.2 Target Id

REPO-IL-006

###### 2.5.5.10.4.3 Message

5.4. [alt: Restore Succeeded OR Restore Failed]

###### 2.5.5.10.4.4 Sequence Number

9

###### 2.5.5.10.4.5 Type

🔹 Alternate Flow

###### 2.5.5.10.4.6 Is Synchronous

✅ Yes

###### 2.5.5.10.4.7 Has Return

❌ No

###### 2.5.5.10.4.8 Is Activation

❌ No

###### 2.5.5.10.4.9 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Internal |
| Method | if-else block |
| Parameters | Result of the connection attempt. |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | N/A |

###### 2.5.5.10.4.10 Nested Interactions

####### 2.5.5.10.4.10.1 Logging

######## 2.5.5.10.4.10.1.1 Source Id

REPO-IP-ST-009

######## 2.5.5.10.4.10.1.2 Target Id

REPO-IL-006

######## 2.5.5.10.4.10.1.3 Message

5.4.1. [if Succeeded] Log(INFO, 'Successfully restored from {BackupFile}') and break loop.

######## 2.5.5.10.4.10.1.4 Sequence Number

10

######## 2.5.5.10.4.10.1.5 Type

🔹 Logging

######## 2.5.5.10.4.10.1.6 Is Synchronous

❌ No

######## 2.5.5.10.4.10.1.7 Has Return

❌ No

######## 2.5.5.10.4.10.1.8 Is Activation

❌ No

######## 2.5.5.10.4.10.1.9 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | ILogger.Information(string template, ...) |
| Parameters | Success message. |
| Authentication | N/A |
| Error Handling | Silent failure. |
| Performance | Negligible |

####### 2.5.5.10.4.10.2.0 Logging

######## 2.5.5.10.4.10.2.1 Source Id

REPO-IP-ST-009

######## 2.5.5.10.4.10.2.2 Target Id

REPO-IL-006

######## 2.5.5.10.4.10.2.3 Message

5.4.2. [if Failed] Log(WARN, 'Restore from {BackupFile} failed.')

######## 2.5.5.10.4.10.2.4 Sequence Number

11

######## 2.5.5.10.4.10.2.5 Type

🔹 Logging

######## 2.5.5.10.4.10.2.6 Is Synchronous

❌ No

######## 2.5.5.10.4.10.2.7 Has Return

❌ No

######## 2.5.5.10.4.10.2.8 Is Activation

❌ No

######## 2.5.5.10.4.10.2.9 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | ILogger.Warning(string template, ...) |
| Parameters | Warning message with backup file name. |
| Authentication | N/A |
| Error Handling | Silent failure. |
| Performance | Negligible |

### 2.5.6.0.0.0.0.0 Logging

#### 2.5.6.1.0.0.0.0 Source Id

REPO-IP-ST-009

#### 2.5.6.2.0.0.0.0 Target Id

REPO-IL-006

#### 2.5.6.3.0.0.0.0 Message

6. [if loop completed without success] Log(ERROR, 'All recovery attempts failed. Data is unrecoverable.')

#### 2.5.6.4.0.0.0.0 Sequence Number

12

#### 2.5.6.5.0.0.0.0 Type

🔹 Logging

#### 2.5.6.6.0.0.0.0 Is Synchronous

❌ No

#### 2.5.6.7.0.0.0.0 Has Return

❌ No

#### 2.5.6.8.0.0.0.0 Is Activation

❌ No

#### 2.5.6.9.0.0.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | ILogger.Error(Exception ex, string template, ...) |
| Parameters | Error message with details about failed backups. |
| Authentication | N/A |
| Error Handling | Silent failure. |
| Performance | Negligible |

### 2.5.7.0.0.0.0.0 Exception

#### 2.5.7.1.0.0.0.0 Source Id

REPO-IP-ST-009

#### 2.5.7.2.0.0.0.0 Target Id

REPO-AS-005

#### 2.5.7.3.0.0.0.0 Message

7. [if unrecoverable] throw new UnrecoverableDataException('Statistics database is corrupt.')

#### 2.5.7.4.0.0.0.0 Sequence Number

13

#### 2.5.7.5.0.0.0.0 Type

🔹 Exception

#### 2.5.7.6.0.0.0.0 Is Synchronous

✅ Yes

#### 2.5.7.7.0.0.0.0 Return Message



#### 2.5.7.8.0.0.0.0 Has Return

❌ No

#### 2.5.7.9.0.0.0.0 Is Activation

❌ No

#### 2.5.7.10.0.0.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | throw |
| Parameters | Custom exception containing error message and inne... |
| Authentication | N/A |
| Error Handling | This exception is caught by the calling service (`... |
| Performance | N/A |

### 2.5.8.0.0.0.0.0 UI Notification

#### 2.5.8.1.0.0.0.0 Source Id

REPO-AS-005

#### 2.5.8.2.0.0.0.0 Target Id

presentation_layer

#### 2.5.8.3.0.0.0.0 Message

8. [on Exception] DisplayDataCorruptionDialog(type: 'Statistics')

#### 2.5.8.4.0.0.0.0 Sequence Number

14

#### 2.5.8.5.0.0.0.0 Type

🔹 UI Notification

#### 2.5.8.6.0.0.0.0 Is Synchronous

❌ No

#### 2.5.8.7.0.0.0.0 Has Return

❌ No

#### 2.5.8.8.0.0.0.0 Is Activation

✅ Yes

#### 2.5.8.9.0.0.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Event/Callback |
| Method | IViewManager.ShowDataCorruptionDialog(DataCorrupti... |
| Parameters | ViewModel with title, message, and button options ... |
| Authentication | N/A |
| Error Handling | If the UI fails to show the dialog, a critical err... |
| Performance | < 100ms to display UI. |

### 2.5.9.0.0.0.0.0 User Input

#### 2.5.9.1.0.0.0.0 Source Id

presentation_layer

#### 2.5.9.2.0.0.0.0 Target Id

REPO-AS-005

#### 2.5.9.3.0.0.0.0 Message

9. [alt: User clicks 'Reset Data' OR 'Exit']

#### 2.5.9.4.0.0.0.0 Sequence Number

15

#### 2.5.9.5.0.0.0.0 Type

🔹 User Input

#### 2.5.9.6.0.0.0.0 Is Synchronous

✅ Yes

#### 2.5.9.7.0.0.0.0 Has Return

✅ Yes

#### 2.5.9.8.0.0.0.0 Is Activation

❌ No

#### 2.5.9.9.0.0.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | UI Event |
| Method | Button.onClick.AddListener() |
| Parameters | Callback methods in `GameSessionService` or a UI c... |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | N/A |

#### 2.5.9.10.0.0.0.0 Nested Interactions

##### 2.5.9.10.1.0.0.0 Method Call

###### 2.5.9.10.1.1.0.0 Source Id

presentation_layer

###### 2.5.9.10.1.2.0.0 Target Id

REPO-AS-005

###### 2.5.9.10.1.3.0.0 Message

9.1. [if Reset] RequestStatisticsReset()

###### 2.5.9.10.1.4.0.0 Sequence Number

16

###### 2.5.9.10.1.5.0.0 Type

🔹 Method Call

###### 2.5.9.10.1.6.0.0 Is Synchronous

✅ Yes

###### 2.5.9.10.1.7.0.0 Has Return

✅ Yes

###### 2.5.9.10.1.8.0.0 Is Activation

❌ No

###### 2.5.9.10.1.9.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | Task RequestStatisticsReset() |
| Parameters | None |
| Authentication | N/A |
| Error Handling | Errors during reset are logged; UI shows a failure... |
| Performance | N/A |

##### 2.5.9.10.2.0.0.0 Method Call

###### 2.5.9.10.2.1.0.0 Source Id

REPO-AS-005

###### 2.5.9.10.2.2.0.0 Target Id

REPO-IP-ST-009

###### 2.5.9.10.2.3.0.0 Message

9.1.1. ResetStatisticsDataAsync()

###### 2.5.9.10.2.4.0.0 Sequence Number

17

###### 2.5.9.10.2.5.0.0 Type

🔹 Method Call

###### 2.5.9.10.2.6.0.0 Is Synchronous

✅ Yes

###### 2.5.9.10.2.7.0.0 Has Return

✅ Yes

###### 2.5.9.10.2.8.0.0 Is Activation

✅ Yes

###### 2.5.9.10.2.9.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | Task ResetStatisticsDataAsync() |
| Parameters | None |
| Authentication | N/A |
| Error Handling | Logs errors if file deletion or recreation fails. |
| Performance | < 200ms |

##### 2.5.9.10.3.0.0.0 System Call

###### 2.5.9.10.3.1.0.0 Source Id

presentation_layer

###### 2.5.9.10.3.2.0.0 Target Id

presentation_layer

###### 2.5.9.10.3.3.0.0 Message

9.2. [if Exit] QuitApplication()

###### 2.5.9.10.3.4.0.0 Sequence Number

18

###### 2.5.9.10.3.5.0.0 Type

🔹 System Call

###### 2.5.9.10.3.6.0.0 Is Synchronous

❌ No

###### 2.5.9.10.3.7.0.0 Has Return

❌ No

###### 2.5.9.10.3.8.0.0 Is Activation

❌ No

###### 2.5.9.10.3.9.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | OS API |
| Method | Application.Quit() |
| Parameters | None |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | N/A |

## 2.6.0.0.0.0.0.0 Notes

### 2.6.1.0.0.0.0.0 Content

#### 2.6.1.1.0.0.0.0 Content

The file replacement operation (Step 5.2) MUST be atomic to prevent a failed copy from leaving the primary DB in an even worse state. `File.Replace` in .NET is the preferred method as it provides this guarantee.

#### 2.6.1.2.0.0.0.0 Position

bottom-left

#### 2.6.1.3.0.0.0.0 Participant Id

REPO-IP-ST-009

#### 2.6.1.4.0.0.0.0 Sequence Number

7

### 2.6.2.0.0.0.0.0 Content

#### 2.6.2.1.0.0.0.0 Content

The user-facing dialog is the final step in the recovery process. Its purpose is to clearly communicate the data loss and provide the user with control over the outcome, fulfilling the 'Communication Recovery' aspect of this flow.

#### 2.6.2.2.0.0.0.0 Position

bottom-right

#### 2.6.2.3.0.0.0.0 Participant Id

presentation_layer

#### 2.6.2.4.0.0.0.0 Sequence Number

14

## 2.7.0.0.0.0.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | No specific security requirements for this local, ... |
| Performance Targets | The entire automated recovery process (detection a... |
| Error Handling Strategy | The primary error (`SqliteException`) triggers the... |
| Testing Considerations | A dedicated suite of integration tests is required... |
| Monitoring Requirements | Monitoring is achieved through structured logging.... |
| Deployment Considerations | The installer must ensure the application has corr... |

