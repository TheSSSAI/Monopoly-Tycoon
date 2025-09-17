# 1 Overview

## 1.1 Diagram Id

SEQ-UJ-021

## 1.2 Name

Player Adjusts In-Game Settings

## 1.3 Description

During gameplay, the player pauses the game and opens the settings menu. They adjust options like audio volume or game speed, and the changes take effect immediately.

## 1.4 Type

🔹 UserJourney

## 1.5 Purpose

To allow users to customize their gameplay experience in real-time.

## 1.6 Complexity

Low

## 1.7 Priority

🟡 Medium

## 1.8 Frequency

OnDemand

## 1.9 Participants

- PresentationLayer
- ApplicationServicesLayer

## 1.10 Key Interactions

- Player clicks the 'Settings' button.
- The PresentationLayer displays the settings menu overlay.
- User adjusts a slider (e.g., Music Volume).
- The UI controller for the settings menu directly calls the AudioManager to set the new volume.
- User changes the 'Game Speed' setting.
- The UI controller updates a configuration value in the ApplicationServicesLayer that the TurnManagementService reads to adjust animation delays.

## 1.11 Triggers

- User opens the settings menu during a game.

## 1.12 Outcomes

- Audio volume is adjusted in real-time.
- AI decision delays and animation speeds are modified.
- User successfully resets their statistics or deletes save files after confirmation prompts.

## 1.13 Business Rules

- Game speed setting is temporarily overridden during auctions involving the human player (REQ-1-078).
- Data deletion options require a confirmation prompt (REQ-1-080, REQ-1-081).

## 1.14 Error Scenarios

*No items available*

## 1.15 Integration Points

- AudioManager (Presentation)
- Configuration objects (ApplicationServices)

# 2.0 Details

## 2.1 Diagram Id

SEQ-UJ-021-IMPL

## 2.2 Name

Implementation: Player Adjusts In-Game Settings

## 2.3 Description

Provides a detailed technical sequence for the user journey of adjusting in-game settings. This covers real-time changes to audio and game speed, as well as destructive actions like resetting statistics and deleting save files, including UI state management, service layer orchestration, and infrastructure-level data operations with comprehensive error handling.

## 2.4 Participants

### 2.4.1 Actor

#### 2.4.1.1 Repository Id

user-actor

#### 2.4.1.2 Display Name

Player

#### 2.4.1.3 Type

🔹 Actor

#### 2.4.1.4 Technology

Human-Computer Interaction

#### 2.4.1.5 Order

1

#### 2.4.1.6 Style

| Property | Value |
|----------|-------|
| Shape | Actor |
| Color | #E6E6FA |
| Stereotype | <<Human>> |

### 2.4.2.0 UI Controller

#### 2.4.2.1 Repository Id

REPO-PL-UI-001

#### 2.4.2.2 Display Name

SettingsUIController

#### 2.4.2.3 Type

🔹 UI Controller

#### 2.4.2.4 Technology

Unity MonoBehaviour (C#)

#### 2.4.2.5 Order

2

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | Component |
| Color | #ADD8E6 |
| Stereotype | <<Presentation>> |

### 2.4.3.0 UI Service

#### 2.4.3.1 Repository Id

REPO-PL-AM-002

#### 2.4.3.2 Display Name

AudioManager

#### 2.4.3.3 Type

🔹 UI Service

#### 2.4.3.4 Technology

Unity MonoBehaviour (C#)

#### 2.4.3.5 Order

3

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | Component |
| Color | #ADD8E6 |
| Stereotype | <<Presentation>> |

### 2.4.4.0 Application Service

#### 2.4.4.1 Repository Id

REPO-AS-005

#### 2.4.4.2 Display Name

GameSessionService

#### 2.4.4.3 Type

🔹 Application Service

#### 2.4.4.4 Technology

.NET 8 (C#)

#### 2.4.4.5 Order

4

#### 2.4.4.6 Style

| Property | Value |
|----------|-------|
| Shape | Component |
| Color | #90EE90 |
| Stereotype | <<Application>> |

### 2.4.5.0 Repository

#### 2.4.5.1 Repository Id

REPO-IP-ST-009

#### 2.4.5.2 Display Name

StatisticsRepository

#### 2.4.5.3 Type

🔹 Repository

#### 2.4.5.4 Technology

Microsoft.Data.Sqlite

#### 2.4.5.5 Order

5

#### 2.4.5.6 Style

| Property | Value |
|----------|-------|
| Shape | Database |
| Color | #FFDAB9 |
| Stereotype | <<Infrastructure>> |

### 2.4.6.0 Repository

#### 2.4.6.1 Repository Id

REPO-IP-SG-008

#### 2.4.6.2 Display Name

SaveGameRepository

#### 2.4.6.3 Type

🔹 Repository

#### 2.4.6.4 Technology

System.Text.Json / File System

#### 2.4.6.5 Order

6

#### 2.4.6.6 Style

| Property | Value |
|----------|-------|
| Shape | Database |
| Color | #FFDAB9 |
| Stereotype | <<Infrastructure>> |

## 2.5.0.0 Interactions

### 2.5.1.0 User Input

#### 2.5.1.1 Source Id

user-actor

#### 2.5.1.2 Target Id

REPO-PL-UI-001

#### 2.5.1.3 Message

1. Clicks 'Settings' button during gameplay.

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
| Protocol | Unity Input System Event |
| Method | OnClick() |
| Parameters | UI Button event payload |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Immediate response (<16ms) required. |

### 2.5.2.0 Internal State Change

#### 2.5.2.1 Source Id

REPO-PL-UI-001

#### 2.5.2.2 Target Id

REPO-PL-UI-001

#### 2.5.2.3 Message

2. Pauses game simulation and displays Settings UI overlay.

#### 2.5.2.4 Sequence Number

2

#### 2.5.2.5 Type

🔹 Internal State Change

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
| Protocol | In-Process Method Call |
| Method | DisplaySettingsView() |
| Parameters | Loads current settings from persistent storage or ... |
| Authentication | N/A |
| Error Handling | If settings file is corrupt, load hardcoded defaul... |
| Performance | UI overlay must appear smoothly without causing a ... |

### 2.5.3.0 User Input

#### 2.5.3.1 Source Id

user-actor

#### 2.5.3.2 Target Id

REPO-PL-UI-001

#### 2.5.3.3 Message

3. Adjusts 'Music Volume' slider.

#### 2.5.3.4 Sequence Number

3

#### 2.5.3.5 Type

🔹 User Input

#### 2.5.3.6 Is Synchronous

✅ Yes

#### 2.5.3.7 Return Message



#### 2.5.3.8 Has Return

❌ No

#### 2.5.3.9 Is Activation

❌ No

#### 2.5.3.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Unity UI Event |
| Method | OnValueChanged(float newValue) |
| Parameters | The new volume level (e.g., 0.0 to 1.0). |
| Authentication | N/A |
| Error Handling | Input is constrained by the slider's min/max value... |
| Performance | Real-time feedback is essential. |

### 2.5.4.0 Method Call

#### 2.5.4.1 Source Id

REPO-PL-UI-001

#### 2.5.4.2 Target Id

REPO-PL-AM-002

#### 2.5.4.3 Message

4. Sets new music volume.

#### 2.5.4.4 Sequence Number

4

#### 2.5.4.5 Type

🔹 Method Call

#### 2.5.4.6 Is Synchronous

✅ Yes

#### 2.5.4.7 Return Message

5. void

#### 2.5.4.8 Has Return

✅ Yes

#### 2.5.4.9 Is Activation

✅ Yes

#### 2.5.4.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Method Call |
| Method | SetVolume(AudioChannel.Music, float volume) |
| Parameters | `AudioChannel` enum, `float` volume value from sli... |
| Authentication | N/A |
| Error Handling | AudioManager should handle invalid values graceful... |
| Performance | Immediate application of volume change. |

### 2.5.5.0 User Input

#### 2.5.5.1 Source Id

user-actor

#### 2.5.5.2 Target Id

REPO-PL-UI-001

#### 2.5.5.3 Message

6. Clicks 'Reset All Statistics' button.

#### 2.5.5.4 Sequence Number

6

#### 2.5.5.5 Type

🔹 User Input

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
| Protocol | Unity Input System Event |
| Method | OnClick() |
| Parameters | UI Button event payload. |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Immediate response. |

### 2.5.6.0 UI Action

#### 2.5.6.1 Source Id

REPO-PL-UI-001

#### 2.5.6.2 Target Id

REPO-PL-UI-001

#### 2.5.6.3 Message

7. Displays a confirmation modal dialog.

#### 2.5.6.4 Sequence Number

7

#### 2.5.6.5 Type

🔹 UI Action

#### 2.5.6.6 Is Synchronous

✅ Yes

#### 2.5.6.7 Return Message



#### 2.5.6.8 Has Return

❌ No

#### 2.5.6.9 Is Activation

❌ No

#### 2.5.6.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Method Call |
| Method | ModalDialogPresenter.ShowConfirm(title, message, o... |
| Parameters | Dialog text and callback delegates for confirmatio... |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Modal must appear instantly. |

#### 2.5.6.11 Nested Interactions

##### 2.5.6.11.1 User Input

###### 2.5.6.11.1.1 Source Id

user-actor

###### 2.5.6.11.1.2 Target Id

REPO-PL-UI-001

###### 2.5.6.11.1.3 Message

7.1. Clicks 'Confirm' on modal dialog.

###### 2.5.6.11.1.4 Sequence Number

8

###### 2.5.6.11.1.5 Type

🔹 User Input

###### 2.5.6.11.1.6 Is Synchronous

✅ Yes

###### 2.5.6.11.1.7 Return Message



###### 2.5.6.11.1.8 Has Return

❌ No

###### 2.5.6.11.1.9 Is Activation

❌ No

###### 2.5.6.11.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Unity Event Callback |
| Method | onConfirm.Invoke() |
| Parameters | None |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | N/A |

##### 2.5.6.11.2.0 Asynchronous Method Call

###### 2.5.6.11.2.1 Source Id

REPO-PL-UI-001

###### 2.5.6.11.2.2 Target Id

REPO-AS-005

###### 2.5.6.11.2.3 Message

8. Requests to reset player statistics.

###### 2.5.6.11.2.4 Sequence Number

9

###### 2.5.6.11.2.5 Type

🔹 Asynchronous Method Call

###### 2.5.6.11.2.6 Is Synchronous

❌ No

###### 2.5.6.11.2.7 Return Message

13. Returns operation result.

###### 2.5.6.11.2.8 Has Return

✅ Yes

###### 2.5.6.11.2.9 Is Activation

✅ Yes

###### 2.5.6.11.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Method Call |
| Method | Task<Result> ResetAllPlayerStatisticsAsync() |
| Parameters | None. Assumes a single player profile. |
| Authentication | N/A |
| Error Handling | The Task will fault if an unhandled exception occu... |
| Performance | This operation must not block the UI thread. |

##### 2.5.6.11.3.0 Asynchronous Method Call

###### 2.5.6.11.3.1 Source Id

REPO-AS-005

###### 2.5.6.11.3.2 Target Id

REPO-IP-ST-009

###### 2.5.6.11.3.3 Message

9. Deletes all player statistics records.

###### 2.5.6.11.3.4 Sequence Number

10

###### 2.5.6.11.3.5 Type

🔹 Asynchronous Method Call

###### 2.5.6.11.3.6 Is Synchronous

❌ No

###### 2.5.6.11.3.7 Return Message

12. Returns success or failure.

###### 2.5.6.11.3.8 Has Return

✅ Yes

###### 2.5.6.11.3.9 Is Activation

✅ Yes

###### 2.5.6.11.3.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Method Call (via abstraction) |
| Method | Task ResetStatisticsAsync() |
| Parameters | None |
| Authentication | N/A |
| Error Handling | Throws `SqliteException` on DB access error. Throw... |
| Performance | Should be fast, but async to handle potential file... |

##### 2.5.6.11.4.0 Database Operation

###### 2.5.6.11.4.1 Source Id

REPO-IP-ST-009

###### 2.5.6.11.4.2 Target Id

REPO-IP-ST-009

###### 2.5.6.11.4.3 Message

10. Executes SQL `DELETE FROM PlayerStatistics;`.

###### 2.5.6.11.4.4 Sequence Number

11

###### 2.5.6.11.4.5 Type

🔹 Database Operation

###### 2.5.6.11.4.6 Is Synchronous

✅ Yes

###### 2.5.6.11.4.7 Return Message

11. DB operation completes.

###### 2.5.6.11.4.8 Has Return

✅ Yes

###### 2.5.6.11.4.9 Is Activation

❌ No

###### 2.5.6.11.4.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | SQLite Connection |
| Method | ExecuteNonQueryAsync() |
| Parameters | SQL command text for deleting records. |
| Authentication | N/A |
| Error Handling | Handled by the repository's try-catch block. |
| Performance | Depends on DB size, expected to be very fast. |

##### 2.5.6.11.5.0 UI Update

###### 2.5.6.11.5.1 Source Id

REPO-AS-005

###### 2.5.6.11.5.2 Target Id

REPO-PL-UI-001

###### 2.5.6.11.5.3 Message

14. Displays success or failure notification to user.

###### 2.5.6.11.5.4 Sequence Number

14

###### 2.5.6.11.5.5 Type

🔹 UI Update

###### 2.5.6.11.5.6 Is Synchronous

✅ Yes

###### 2.5.6.11.5.7 Return Message



###### 2.5.6.11.5.8 Has Return

❌ No

###### 2.5.6.11.5.9 Is Activation

❌ No

###### 2.5.6.11.5.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Async Await Callback |
| Method | ShowToastNotification(string message, Notification... |
| Parameters | e.g., ('Statistics successfully reset.', Notificat... |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Notification should appear without delay. |

### 2.5.7.0.0.0 User Input

#### 2.5.7.1.0.0 Source Id

user-actor

#### 2.5.7.2.0.0 Target Id

REPO-PL-UI-001

#### 2.5.7.3.0.0 Message

15. Clicks 'Delete All Save Files' button and confirms.

#### 2.5.7.4.0.0 Sequence Number

15

#### 2.5.7.5.0.0 Type

🔹 User Input

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
| Protocol | Unity Input System Event |
| Method | OnClick() |
| Parameters | Follows the same confirmation modal flow as steps ... |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Immediate response. |

### 2.5.8.0.0.0 Asynchronous Method Call

#### 2.5.8.1.0.0 Source Id

REPO-PL-UI-001

#### 2.5.8.2.0.0 Target Id

REPO-AS-005

#### 2.5.8.3.0.0 Message

16. Requests to delete all save files.

#### 2.5.8.4.0.0 Sequence Number

16

#### 2.5.8.5.0.0 Type

🔹 Asynchronous Method Call

#### 2.5.8.6.0.0 Is Synchronous

❌ No

#### 2.5.8.7.0.0 Return Message

21. Returns operation result.

#### 2.5.8.8.0.0 Has Return

✅ Yes

#### 2.5.8.9.0.0 Is Activation

✅ Yes

#### 2.5.8.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Method Call |
| Method | Task<Result> DeleteAllSaveFilesAsync() |
| Parameters | None. |
| Authentication | N/A |
| Error Handling | The UI must handle a faulted Task, indicating a fa... |
| Performance | Must not block the UI thread. |

### 2.5.9.0.0.0 Asynchronous Method Call

#### 2.5.9.1.0.0 Source Id

REPO-AS-005

#### 2.5.9.2.0.0 Target Id

REPO-IP-SG-008

#### 2.5.9.3.0.0 Message

17. Deletes all save game files.

#### 2.5.9.4.0.0 Sequence Number

17

#### 2.5.9.5.0.0 Type

🔹 Asynchronous Method Call

#### 2.5.9.6.0.0 Is Synchronous

❌ No

#### 2.5.9.7.0.0 Return Message

20. Returns success or failure.

#### 2.5.9.8.0.0 Has Return

✅ Yes

#### 2.5.9.9.0.0 Is Activation

✅ Yes

#### 2.5.9.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Method Call (via abstraction) |
| Method | Task DeleteAllSavesAsync() |
| Parameters | None. |
| Authentication | N/A |
| Error Handling | Throws `IOException` on file system errors (e.g., ... |
| Performance | Performance depends on file system I/O. |

### 2.5.10.0.0.0 File System Operation

#### 2.5.10.1.0.0 Source Id

REPO-IP-SG-008

#### 2.5.10.2.0.0 Target Id

REPO-IP-SG-008

#### 2.5.10.3.0.0 Message

18. Iterates through save directory and deletes each file.

#### 2.5.10.4.0.0 Sequence Number

18

#### 2.5.10.5.0.0 Type

🔹 File System Operation

#### 2.5.10.6.0.0 Is Synchronous

✅ Yes

#### 2.5.10.7.0.0 Return Message

19. All files deleted.

#### 2.5.10.8.0.0 Has Return

✅ Yes

#### 2.5.10.9.0.0 Is Activation

❌ No

#### 2.5.10.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | System.IO API |
| Method | File.Delete(string path) |
| Parameters | Path to each save file (e.g., 'save_slot_1.json'). |
| Authentication | N/A |
| Error Handling | The method is wrapped in a try-catch block within ... |
| Performance | I/O bound. |

### 2.5.11.0.0.0 UI Update

#### 2.5.11.1.0.0 Source Id

REPO-PL-UI-001

#### 2.5.11.2.0.0 Target Id

REPO-PL-UI-001

#### 2.5.11.3.0.0 Message

22. Displays success/failure notification and closes settings menu.

#### 2.5.11.4.0.0 Sequence Number

22

#### 2.5.11.5.0.0 Type

🔹 UI Update

#### 2.5.11.6.0.0 Is Synchronous

✅ Yes

#### 2.5.11.7.0.0 Return Message



#### 2.5.11.8.0.0 Has Return

❌ No

#### 2.5.11.9.0.0 Is Activation

❌ No

#### 2.5.11.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process Method Call |
| Method | CloseSettingsView() |
| Parameters | Resumes game simulation and hides the settings UI. |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Immediate UI response. |

## 2.6.0.0.0.0 Notes

### 2.6.1.0.0.0 Content

#### 2.6.1.1.0.0 Content

REQ-1-080 and REQ-1-081 (Confirmation Prompts) are implemented by the modal dialog flow in steps 7-8.

#### 2.6.1.2.0.0 Position

Top

#### 2.6.1.3.0.0 Participant Id

*Not specified*

#### 2.6.1.4.0.0 Sequence Number

7

### 2.6.2.0.0.0 Content

#### 2.6.2.1.0.0 Content

The `Result` type returned from services is a custom discriminated union or similar pattern to clearly communicate success or a specific error type (e.g., `FileSystemError`, `DatabaseError`) back to the UI.

#### 2.6.2.2.0.0 Position

Right

#### 2.6.2.3.0.0 Participant Id

REPO-AS-005

#### 2.6.2.4.0.0 Sequence Number

9

## 2.7.0.0.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | As this is an offline, client-side feature, there ... |
| Performance Targets | All UI interactions must be instantaneous (<100ms ... |
| Error Handling Strategy | Repositories (`StatisticsRepository`, `SaveGameRep... |
| Testing Considerations | Unit tests for `SettingsUIController` should mock ... |
| Monitoring Requirements | Log successful data deletion events (stats reset, ... |
| Deployment Considerations | N/A for this feature. |

