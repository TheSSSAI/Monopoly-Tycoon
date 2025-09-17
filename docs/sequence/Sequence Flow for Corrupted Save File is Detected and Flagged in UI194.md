# 1 Overview

## 1.1 Diagram Id

SEQ-EH-002

## 1.2 Name

Corrupted Save File is Detected and Flagged in UI

## 1.3 Description

When the user opens the 'Load Game' screen, the system proactively checks the integrity of each save file via checksum validation. If a file is found to be corrupt, it is clearly marked as unusable in the UI, preventing the user from attempting to load it.

## 1.4 Type

🔹 ErrorHandling

## 1.5 Purpose

To protect the application from crashing due to corrupted data and to manage user expectations by clearly communicating which save files are no longer valid.

## 1.6 Complexity

Low

## 1.7 Priority

🔴 High

## 1.8 Frequency

OnDemand

## 1.9 Participants

- REPO-PRES-001
- REPO-AS-005
- REPO-IL-006

## 1.10 Key Interactions

- User navigates to the 'Load Game' screen.
- The Presentation Layer requests the list of save games from the Application Services Layer.
- The Application Services Layer calls the Infrastructure Layer's GameSaveRepository.
- For each save file found, the GameSaveRepository reads its contents, recalculates its checksum, and compares it to the checksum stored within the file.
- If a checksum mismatch is found, the repository flags that save slot's metadata as 'Corrupted'.
- The list of save metadata is returned to the Presentation Layer.
- The UI displays the corrupted save slot differently (e.g., grayed out, with an icon) and disables the 'Load' button for it.

## 1.11 Triggers

- The 'Load Game' screen is opened by the user.

## 1.12 Outcomes

- Corrupted save files are identified and cannot be loaded.
- The user is visually informed about the corrupted data.
- Application stability is maintained by preventing crashes on corrupt data.

## 1.13 Business Rules

- Checksums must be implemented to detect file corruption (REQ-1-088).

## 1.14 Error Scenarios

- Multiple save files are corrupted.

## 1.15 Integration Points

- Local File System

# 2.0 Details

## 2.1 Diagram Id

SEQ-EH-002

## 2.2 Name

Error Handling: Corrupted Save File Detection and UI Flagging

## 2.3 Description

A detailed technical sequence for detecting corrupted save game files upon user request to load a game. The sequence leverages checksum validation to identify data integrity issues, logs the failure for diagnostic purposes, and provides clear, non-blocking feedback to the user by marking the affected save slot as unusable in the user interface. This prevents application crashes and maintains a stable user experience.

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

1

#### 2.4.1.6 Style

| Property | Value |
|----------|-------|
| Shape | actor |
| Color | #E6E6E6 |
| Stereotype | Human |

### 2.4.2.0 UI Component

#### 2.4.2.1 Repository Id

REPO-PRES-001

#### 2.4.2.2 Display Name

LoadGameUIController

#### 2.4.2.3 Type

🔹 UI Component

#### 2.4.2.4 Technology

Unity Engine, C#

#### 2.4.2.5 Order

2

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | boundary |
| Color | #82E0AA |
| Stereotype | Presentation |

### 2.4.3.0 Service

#### 2.4.3.1 Repository Id

REPO-AS-005

#### 2.4.3.2 Display Name

GameSessionService

#### 2.4.3.3 Type

🔹 Service

#### 2.4.3.4 Technology

.NET 8, C#

#### 2.4.3.5 Order

3

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | control |
| Color | #AED6F1 |
| Stereotype | Application Service |

### 2.4.4.0 Repository

#### 2.4.4.1 Repository Id

REPO-IP-SG-008

#### 2.4.4.2 Display Name

GameSaveRepository

#### 2.4.4.3 Type

🔹 Repository

#### 2.4.4.4 Technology

.NET 8, C#, System.Text.Json

#### 2.4.4.5 Order

4

#### 2.4.4.6 Style

| Property | Value |
|----------|-------|
| Shape | entity |
| Color | #F5B7B1 |
| Stereotype | Infrastructure |

### 2.4.5.0 Service

#### 2.4.5.1 Repository Id

REPO-IL-006

#### 2.4.5.2 Display Name

LoggingService

#### 2.4.5.3 Type

🔹 Service

#### 2.4.5.4 Technology

Serilog

#### 2.4.5.5 Order

5

#### 2.4.5.6 Style

| Property | Value |
|----------|-------|
| Shape | entity |
| Color | #F9E79F |
| Stereotype | Infrastructure |

## 2.5.0.0 Interactions

### 2.5.1.0 UI Event

#### 2.5.1.1 Source Id

User

#### 2.5.1.2 Target Id

REPO-PRES-001

#### 2.5.1.3 Message

User clicks the 'Load Game' button from the Main Menu.

#### 2.5.1.4 Sequence Number

1

#### 2.5.1.5 Type

🔹 UI Event

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
| Method | OnLoadGameScreenRequested() |
| Parameters | None |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Immediate UI response required. |

### 2.5.2.0 Method Call

#### 2.5.2.1 Source Id

REPO-PRES-001

#### 2.5.2.2 Target Id

REPO-AS-005

#### 2.5.2.3 Message

Request list of all save game slots with their metadata.

#### 2.5.2.4 Sequence Number

2

#### 2.5.2.5 Type

🔹 Method Call

#### 2.5.2.6 Is Synchronous

✅ Yes

#### 2.5.2.7 Return Message

Returns a list of SaveGameMetadata objects.

#### 2.5.2.8 Has Return

✅ Yes

#### 2.5.2.9 Is Activation

✅ Yes

#### 2.5.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | Task<List<SaveGameMetadata>> GetSaveGameSlotsAsync... |
| Parameters | None |
| Authentication | N/A |
| Error Handling | Exceptions are caught, logged, and result in an em... |
| Performance | Should complete within 100ms. |

### 2.5.3.0 Method Call

#### 2.5.3.1 Source Id

REPO-AS-005

#### 2.5.3.2 Target Id

REPO-IP-SG-008

#### 2.5.3.3 Message

Invoke repository to list all available save files and validate their integrity.

#### 2.5.3.4 Sequence Number

3

#### 2.5.3.5 Type

🔹 Method Call

#### 2.5.3.6 Is Synchronous

✅ Yes

#### 2.5.3.7 Return Message

Returns a list of SaveGameMetadata, each with a status (Valid, Corrupted, Empty).

#### 2.5.3.8 Has Return

✅ Yes

#### 2.5.3.9 Is Activation

✅ Yes

#### 2.5.3.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | Task<List<SaveGameMetadata>> ListSavesAsync() |
| Parameters | None |
| Authentication | N/A |
| Error Handling | Handles file system exceptions (`IOException`, `Fi... |
| Performance | Latency is dependent on disk I/O. Target < 400ms f... |

### 2.5.4.0 Internal Process

#### 2.5.4.1 Source Id

REPO-IP-SG-008

#### 2.5.4.2 Target Id

REPO-IP-SG-008

#### 2.5.4.3 Message

Loop through each potential save slot (e.g., 1-5).

#### 2.5.4.4 Sequence Number

4

#### 2.5.4.5 Type

🔹 Internal Process

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
| Protocol | N/A |
| Method | For (int slot = 1; slot <= MAX_SLOTS; slot++) |
| Parameters | None |
| Authentication | N/A |
| Error Handling | Loop continues even if one slot fails validation. |
| Performance | N/A |

### 2.5.5.0 Alternate Flow

#### 2.5.5.1 Source Id

REPO-IP-SG-008

#### 2.5.5.2 Target Id

REPO-IP-SG-008

#### 2.5.5.3 Message

ALT: Checksum Validation Fails for a save file.

#### 2.5.5.4 Sequence Number

5

#### 2.5.5.5 Type

🔹 Alternate Flow

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
| Protocol | N/A |
| Method | ValidateChecksum(fileBytes) |
| Parameters | File contents as byte array. |
| Authentication | N/A |
| Error Handling | This is the error detection step. |
| Performance | Checksum calculation (e.g., SHA256) should be fast... |

#### 2.5.5.11 Nested Interactions

##### 2.5.5.11.1 Method Call

###### 2.5.5.11.1.1 Source Id

REPO-IP-SG-008

###### 2.5.5.11.1.2 Target Id

REPO-IL-006

###### 2.5.5.11.1.3 Message

Log the data corruption event at ERROR level for diagnostics.

###### 2.5.5.11.1.4 Sequence Number

5.1

###### 2.5.5.11.1.5 Type

🔹 Method Call

###### 2.5.5.11.1.6 Is Synchronous

❌ No

###### 2.5.5.11.1.7 Return Message



###### 2.5.5.11.1.8 Has Return

❌ No

###### 2.5.5.11.1.9 Is Activation

✅ Yes

###### 2.5.5.11.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | LogError("Save file corruption detected in slot {S... |
| Parameters | Structured log message with slot ID. |
| Authentication | N/A |
| Error Handling | Logging failure should not interrupt the main flow... |
| Performance | Asynchronous logging to avoid blocking. |

##### 2.5.5.11.2.0 Internal Process

###### 2.5.5.11.2.1 Source Id

REPO-IP-SG-008

###### 2.5.5.11.2.2 Target Id

REPO-IP-SG-008

###### 2.5.5.11.2.3 Message

Create SaveGameMetadata object with status 'Corrupted'.

###### 2.5.5.11.2.4 Sequence Number

5.2

###### 2.5.5.11.2.5 Type

🔹 Internal Process

###### 2.5.5.11.2.6 Is Synchronous

✅ Yes

###### 2.5.5.11.2.7 Return Message



###### 2.5.5.11.2.8 Has Return

❌ No

###### 2.5.5.11.2.9 Is Activation

❌ No

###### 2.5.5.11.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | N/A |
| Method | new SaveGameMetadata { SlotId = slotId, Status = S... |
| Parameters | None |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | N/A |

### 2.5.6.0.0.0 Return

#### 2.5.6.1.0.0 Source Id

REPO-IP-SG-008

#### 2.5.6.2.0.0 Target Id

REPO-AS-005

#### 2.5.6.3.0.0 Message

Return `List<SaveGameMetadata>` to service layer.

#### 2.5.6.4.0.0 Sequence Number

6

#### 2.5.6.5.0.0 Type

🔹 Return

#### 2.5.6.6.0.0 Is Synchronous

✅ Yes

#### 2.5.6.7.0.0 Return Message



#### 2.5.6.8.0.0 Has Return

❌ No

#### 2.5.6.9.0.0 Is Activation

❌ No

#### 2.5.6.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | return metadataList; |
| Parameters | `metadataList`: Contains metadata for all slots, s... |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | N/A |

### 2.5.7.0.0.0 Return

#### 2.5.7.1.0.0 Source Id

REPO-AS-005

#### 2.5.7.2.0.0 Target Id

REPO-PRES-001

#### 2.5.7.3.0.0 Message

Forward `List<SaveGameMetadata>` to UI controller.

#### 2.5.7.4.0.0 Sequence Number

7

#### 2.5.7.5.0.0 Type

🔹 Return

#### 2.5.7.6.0.0 Is Synchronous

✅ Yes

#### 2.5.7.7.0.0 Return Message



#### 2.5.7.8.0.0 Has Return

❌ No

#### 2.5.7.9.0.0 Is Activation

❌ No

#### 2.5.7.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | return metadataList; |
| Parameters | The validated list of save game metadata. |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | N/A |

### 2.5.8.0.0.0 Internal Process

#### 2.5.8.1.0.0 Source Id

REPO-PRES-001

#### 2.5.8.2.0.0 Target Id

REPO-PRES-001

#### 2.5.8.3.0.0 Message

Iterate through the returned metadata and render the UI for each slot.

#### 2.5.8.4.0.0 Sequence Number

8

#### 2.5.8.5.0.0 Type

🔹 Internal Process

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
| Protocol | N/A |
| Method | foreach (var metadata in metadataList) |
| Parameters | None |
| Authentication | N/A |
| Error Handling | UI rendering should be robust to handle different ... |
| Performance | UI updates should not cause frame drops. |

### 2.5.9.0.0.0 UI Update

#### 2.5.9.1.0.0 Source Id

REPO-PRES-001

#### 2.5.9.2.0.0 Target Id

REPO-PRES-001

#### 2.5.9.3.0.0 Message

If metadata.Status is 'Corrupted', disable the 'Load' button, apply a visual indicator (e.g., gray out, warning icon), and set a tooltip explaining the file is unusable.

#### 2.5.9.4.0.0 Sequence Number

9

#### 2.5.9.5.0.0 Type

🔹 UI Update

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
| Protocol | UI Framework |
| Method | UpdateSlotView(metadata) |
| Parameters | `metadata`: The SaveGameMetadata object for the sl... |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | N/A |

## 2.6.0.0.0.0 Notes

### 2.6.1.0.0.0 Content

#### 2.6.1.1.0.0 Content

The checksum validation logic is critical. It involves reading the file, separating the stored checksum from the data payload, computing a new checksum (e.g., SHA256) on the payload, and comparing the two. This fulfills REQ-1-088.

#### 2.6.1.2.0.0 Position

RightOf

#### 2.6.1.3.0.0 Participant Id

REPO-IP-SG-008

#### 2.6.1.4.0.0 Sequence Number

5

### 2.6.2.0.0.0 Content

#### 2.6.2.1.0.0 Content

The user-facing communication is key for this error handling sequence. The UI must clearly and passively inform the user of the problem without causing alarm or interrupting their ability to load other, valid saves. This is a form of graceful degradation.

#### 2.6.2.2.0.0 Position

LeftOf

#### 2.6.2.3.0.0 Participant Id

REPO-PRES-001

#### 2.6.2.4.0.0 Sequence Number

9

## 2.7.0.0.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | No personally identifiable information (PII), othe... |
| Performance Targets | The entire validation process for all save slots m... |
| Error Handling Strategy | The `GameSaveRepository` must implement try-catch ... |
| Testing Considerations | Requires a dedicated set of test data including: 1... |
| Monitoring Requirements | The primary monitoring mechanism is the local log ... |
| Deployment Considerations | N/A for this sequence. |

