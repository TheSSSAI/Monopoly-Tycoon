# 1 Overview

## 1.1 Diagram Id

SEQ-DF-019

## 1.2 Name

Export Top Score History

## 1.3 Description

From the Top Scores screen, the user clicks an 'Export' button. The system retrieves the top 10 scores from the database, formats them into a human-readable text file, and saves it to a user-selected location.

## 1.4 Type

🔹 DataFlow

## 1.5 Purpose

To allow players to share or keep a permanent, external record of their best achievements.

## 1.6 Complexity

Low

## 1.7 Priority

🟢 Low

## 1.8 Frequency

OnDemand

## 1.9 Participants

- PresentationLayer
- ApplicationServicesLayer
- InfrastructureLayer

## 1.10 Key Interactions

- User clicks 'Export Scores' button.
- The UI prompts the user to select a save location and filename for the .txt file.
- The ApplicationServicesLayer requests the top scores from the StatisticsRepository.
- The repository queries the SQLite database.
- The service formats the retrieved data into a string.
- The string is written to the user-specified text file on the local file system.

## 1.11 Triggers

- User clicks the 'Export' button on the Top Scores screen.

## 1.12 Outcomes

- A .txt file containing the formatted high scores is saved to the user's computer.

## 1.13 Business Rules

- Export must be to a human-readable text file (REQ-1-092).

## 1.14 Error Scenarios

- User cancels the file save dialog.
- File I/O error (no write permission).

## 1.15 Integration Points

- Local File System

# 2.0 Details

## 2.1 Diagram Id

SEQ-DF-019

## 2.2 Name

Export Top Score History to File

## 2.3 Description

A detailed sequence for exporting the top 10 player scores. The process is initiated by the user, retrieves data via the application and infrastructure layers, formats it, and writes it to a user-specified text file on the local file system. This sequence emphasizes the Repository Pattern and clear separation of concerns.

## 2.4 Participants

### 2.4.1 UI Controller

#### 2.4.1.1 Repository Id

REPO-PL-007

#### 2.4.1.2 Display Name

TopScoresView

#### 2.4.1.3 Type

🔹 UI Controller

#### 2.4.1.4 Technology

Unity Engine, C#

#### 2.4.1.5 Order

1

#### 2.4.1.6 Style

| Property | Value |
|----------|-------|
| Shape | actor |
| Color | #4CAF50 |
| Stereotype | Presentation |

### 2.4.2.0 Application Service

#### 2.4.2.1 Repository Id

REPO-AS-005

#### 2.4.2.2 Display Name

StatisticsService

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
| Color | #2196F3 |
| Stereotype | Application |

### 2.4.3.0 Data Repository

#### 2.4.3.1 Repository Id

REPO-IP-ST-009

#### 2.4.3.2 Display Name

StatisticsRepository

#### 2.4.3.3 Type

🔹 Data Repository

#### 2.4.3.4 Technology

Microsoft.Data.Sqlite, C#

#### 2.4.3.5 Order

3

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | database |
| Color | #FFC107 |
| Stereotype | Infrastructure |

### 2.4.4.0 File Repository

#### 2.4.4.1 Repository Id

REPO-IP-FS-010

#### 2.4.4.2 Display Name

FileSystemRepository

#### 2.4.4.3 Type

🔹 File Repository

#### 2.4.4.4 Technology

System.IO, C#

#### 2.4.4.5 Order

4

#### 2.4.4.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #9C27B0 |
| Stereotype | Infrastructure |

## 2.5.0.0 Interactions

### 2.5.1.0 UI Event

#### 2.5.1.1 Source Id

REPO-PL-007

#### 2.5.1.2 Target Id

REPO-PL-007

#### 2.5.1.3 Message

User clicks 'Export' button. The view invokes a native OS file save dialog.

#### 2.5.1.4 Sequence Number

1

#### 2.5.1.5 Type

🔹 UI Event

#### 2.5.1.6 Is Synchronous

✅ Yes

#### 2.5.1.7 Return Message

Returns selected file path or null if cancelled.

#### 2.5.1.8 Has Return

✅ Yes

#### 2.5.1.9 Is Activation

✅ Yes

#### 2.5.1.10 Technical Details

##### 2.5.1.10.1 Protocol

In-Process (Unity Event)

##### 2.5.1.10.2 Method

OnExportButtonClicked()

##### 2.5.1.10.3 Parameters

*No items available*

##### 2.5.1.10.4 Authentication

None

##### 2.5.1.10.5 Error Handling

If the user cancels the dialog, the process is aborted gracefully. No error is thrown.

##### 2.5.1.10.6 Performance

Latency is dependent on user interaction with the OS file dialog.

### 2.5.2.0.0 Asynchronous Method Call

#### 2.5.2.1.0 Source Id

REPO-PL-007

#### 2.5.2.2.0 Target Id

REPO-AS-005

#### 2.5.2.3.0 Message

Request to export top scores to the selected file path.

#### 2.5.2.4.0 Sequence Number

2

#### 2.5.2.5.0 Type

🔹 Asynchronous Method Call

#### 2.5.2.6.0 Is Synchronous

❌ No

#### 2.5.2.7.0 Return Message

Task<ExportResult> indicating success or failure and a user-facing message.

#### 2.5.2.8.0 Has Return

✅ Yes

#### 2.5.2.9.0 Is Activation

✅ Yes

#### 2.5.2.10.0 Technical Details

##### 2.5.2.10.1 Protocol

In-Process (DI)

##### 2.5.2.10.2 Method

Task<ExportResult> ExportTopScoresAsync(string filePath)

##### 2.5.2.10.3 Parameters

- {'name': 'filePath', 'type': 'string', 'description': 'The full path, including filename, selected by the user.'}

##### 2.5.2.10.4 Authentication

None

##### 2.5.2.10.5 Error Handling

The calling view must handle the returned ExportResult to display appropriate notifications.

##### 2.5.2.10.6 Performance

Should complete in < 500ms.

### 2.5.3.0.0 Asynchronous Method Call

#### 2.5.3.1.0 Source Id

REPO-AS-005

#### 2.5.3.2.0 Target Id

REPO-IP-ST-009

#### 2.5.3.3.0 Message

Request the list of top 10 scores.

#### 2.5.3.4.0 Sequence Number

3

#### 2.5.3.5.0 Type

🔹 Asynchronous Method Call

#### 2.5.3.6.0 Is Synchronous

❌ No

#### 2.5.3.7.0 Return Message

Task<List<TopScore>> containing the top 10 score records.

#### 2.5.3.8.0 Has Return

✅ Yes

#### 2.5.3.9.0 Is Activation

✅ Yes

#### 2.5.3.10.0 Technical Details

##### 2.5.3.10.1 Protocol

In-Process (DI)

##### 2.5.3.10.2 Method

Task<List<TopScore>> GetTopScoresAsync()

##### 2.5.3.10.3 Parameters

*No items available*

##### 2.5.3.10.4 Authentication

None

##### 2.5.3.10.5 Error Handling

Handles `SqliteException`. If an error occurs, it is caught, logged, and propagated as a failure in the ExportResult.

##### 2.5.3.10.6 Performance

Expected latency < 50ms with proper indexing.

### 2.5.4.0.0 Database Query

#### 2.5.4.1.0 Source Id

REPO-IP-ST-009

#### 2.5.4.2.0 Target Id

REPO-IP-ST-009

#### 2.5.4.3.0 Message

Execute optimized query against SQLite DB and map results.

#### 2.5.4.4.0 Sequence Number

4

#### 2.5.4.5.0 Type

🔹 Database Query

#### 2.5.4.6.0 Is Synchronous

✅ Yes

#### 2.5.4.7.0 Return Message

Returns a `DbDataReader` with the query results.

#### 2.5.4.8.0 Has Return

✅ Yes

#### 2.5.4.9.0 Is Activation

❌ No

#### 2.5.4.10.0 Technical Details

##### 2.5.4.10.1 Protocol

ADO.NET

##### 2.5.4.10.2 Method

```sql
SELECT ProfileName, FinalNetWorth, GameDuration, TotalTurns FROM TopScores ORDER BY FinalNetWorth DESC LIMIT 10
```

##### 2.5.4.10.3 Parameters

*No items available*

##### 2.5.4.10.4 Authentication

None (local file access)

##### 2.5.4.10.5 Error Handling

Connection errors or query syntax errors will throw `SqliteException`.

##### 2.5.4.10.6 Performance

Requires a database index on the `FinalNetWorth` column for optimal performance.

#### 2.5.4.11.0 Nested Interactions

- {'sourceId': 'REPO-IP-ST-009', 'targetId': 'REPO-IP-ST-009', 'message': 'Map `DbDataReader` rows to a `List<TopScore>` DTOs.', 'sequenceNumber': 5, 'type': 'Data Mapping', 'isSynchronous': True, 'returnMessage': 'Populated `List<TopScore>`.', 'hasReturn': True, 'isActivation': False, 'technicalDetails': {'protocol': 'In-Memory', 'method': 'MapReaderToTopScores(reader)', 'parameters': [], 'authentication': 'None', 'errorHandling': 'Handles potential data type conversion errors.', 'performance': 'Negligible overhead.'}}

### 2.5.5.0.0 Data Transformation

#### 2.5.5.1.0 Source Id

REPO-AS-005

#### 2.5.5.2.0 Target Id

REPO-AS-005

#### 2.5.5.3.0 Message

Format the list of TopScore objects into a human-readable string.

#### 2.5.5.4.0 Sequence Number

6

#### 2.5.5.5.0 Type

🔹 Data Transformation

#### 2.5.5.6.0 Is Synchronous

✅ Yes

#### 2.5.5.7.0 Return Message

Formatted string content for the .txt file.

#### 2.5.5.8.0 Has Return

✅ Yes

#### 2.5.5.9.0 Is Activation

❌ No

#### 2.5.5.10.0 Technical Details

##### 2.5.5.10.1 Protocol

In-Memory

##### 2.5.5.10.2 Method

FormatScoresForExport(List<TopScore> scores)

##### 2.5.5.10.3 Parameters

- {'name': 'scores', 'type': 'List<TopScore>', 'description': 'The data retrieved from the database.'}

##### 2.5.5.10.4 Authentication

None

##### 2.5.5.10.5 Error Handling

N/A

##### 2.5.5.10.6 Performance

Negligible overhead.

### 2.5.6.0.0 Asynchronous Method Call

#### 2.5.6.1.0 Source Id

REPO-AS-005

#### 2.5.6.2.0 Target Id

REPO-IP-FS-010

#### 2.5.6.3.0 Message

Request to write the formatted string to the specified file.

#### 2.5.6.4.0 Sequence Number

7

#### 2.5.6.5.0 Type

🔹 Asynchronous Method Call

#### 2.5.6.6.0 Is Synchronous

❌ No

#### 2.5.6.7.0 Return Message

Task representing the completion of the file write operation.

#### 2.5.6.8.0 Has Return

✅ Yes

#### 2.5.6.9.0 Is Activation

✅ Yes

#### 2.5.6.10.0 Technical Details

##### 2.5.6.10.1 Protocol

In-Process (DI)

##### 2.5.6.10.2 Method

Task WriteTextAsync(string filePath, string content)

##### 2.5.6.10.3 Parameters

###### 2.5.6.10.3.1 string

####### 2.5.6.10.3.1.1 Name

filePath

####### 2.5.6.10.3.1.2 Type

🔹 string

####### 2.5.6.10.3.1.3 Description

The target file path.

###### 2.5.6.10.3.2.0 string

####### 2.5.6.10.3.2.1 Name

content

####### 2.5.6.10.3.2.2 Type

🔹 string

####### 2.5.6.10.3.2.3 Description

The formatted score data.

##### 2.5.6.10.4.0.0 Authentication

None

##### 2.5.6.10.5.0.0 Error Handling

Catches and wraps `IOException` and `UnauthorizedAccessException` to be handled by the service layer.

##### 2.5.6.10.6.0.0 Performance

Latency is dependent on local disk speed.

### 2.5.7.0.0.0.0 Asynchronous Return

#### 2.5.7.1.0.0.0 Source Id

REPO-AS-005

#### 2.5.7.2.0.0.0 Target Id

REPO-PL-007

#### 2.5.7.3.0.0.0 Message

Return the final result of the export operation.

#### 2.5.7.4.0.0.0 Sequence Number

8

#### 2.5.7.5.0.0.0 Type

🔹 Asynchronous Return

#### 2.5.7.6.0.0.0 Is Synchronous

❌ No

#### 2.5.7.7.0.0.0 Return Message



#### 2.5.7.8.0.0.0 Has Return

❌ No

#### 2.5.7.9.0.0.0 Is Activation

❌ No

#### 2.5.7.10.0.0.0 Technical Details

##### 2.5.7.10.1.0.0 Protocol

In-Process (Task)

##### 2.5.7.10.2.0.0 Method

return exportResult

##### 2.5.7.10.3.0.0 Parameters

- {'name': 'exportResult', 'type': 'ExportResult', 'description': 'Contains `Success` (bool) and `Message` (string).'}

##### 2.5.7.10.4.0.0 Authentication

None

##### 2.5.7.10.5.0.0 Error Handling

N/A

##### 2.5.7.10.6.0.0 Performance

N/A

### 2.5.8.0.0.0.0 UI Update

#### 2.5.8.1.0.0.0 Source Id

REPO-PL-007

#### 2.5.8.2.0.0.0 Target Id

REPO-PL-007

#### 2.5.8.3.0.0.0 Message

Display a success or failure notification to the user.

#### 2.5.8.4.0.0.0 Sequence Number

9

#### 2.5.8.5.0.0.0 Type

🔹 UI Update

#### 2.5.8.6.0.0.0 Is Synchronous

✅ Yes

#### 2.5.8.7.0.0.0 Return Message



#### 2.5.8.8.0.0.0 Has Return

❌ No

#### 2.5.8.9.0.0.0 Is Activation

❌ No

#### 2.5.8.10.0.0.0 Technical Details

##### 2.5.8.10.1.0.0 Protocol

In-Process (Unity UI)

##### 2.5.8.10.2.0.0 Method

ShowNotification(string message)

##### 2.5.8.10.3.0.0 Parameters

*No items available*

##### 2.5.8.10.4.0.0 Authentication

None

##### 2.5.8.10.5.0.0 Error Handling

N/A

##### 2.5.8.10.6.0.0 Performance

Must not block the UI thread.

## 2.6.0.0.0.0.0 Notes

### 2.6.1.0.0.0.0 Content

#### 2.6.1.1.0.0.0 Content

Business Rule REQ-1-092: The output format must be human-readable text. The `FormatScoresForExport` method implements this by creating a structured string, e.g., '1. [Name] - Net Worth: $[Amount] ...'.

#### 2.6.1.2.0.0.0 Position

top-right

#### 2.6.1.3.0.0.0 Participant Id

*Not specified*

#### 2.6.1.4.0.0.0 Sequence Number

6

### 2.6.2.0.0.0.0 Content

#### 2.6.2.1.0.0.0 Content

The initial file path selection (Seq 1) is a critical branching point. If the user cancels the file dialog, the `filePath` will be null or empty, and the `ExportTopScoresAsync` call (Seq 2) will not be made.

#### 2.6.2.2.0.0.0 Position

bottom-left

#### 2.6.2.3.0.0.0 Participant Id

REPO-PL-007

#### 2.6.2.4.0.0.0 Sequence Number

1

## 2.7.0.0.0.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | As an offline application, security risks are mini... |
| Performance Targets | The entire operation, excluding user interaction t... |
| Error Handling Strategy | The `StatisticsService` is responsible for catchin... |
| Testing Considerations | 1. **Integration Test:** An end-to-end test should... |
| Monitoring Requirements | The `StatisticsService` should log the initiation ... |
| Deployment Considerations | The application installer must ensure that the app... |

