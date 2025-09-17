# 1 Overview

## 1.1 Diagram Id

SEQ-UJ-012

## 1.2 Name

Player Manages Properties via UI

## 1.3 Description

During the pre-roll phase of their turn, the human player opens a dedicated property management screen to view all owned properties, build houses on monopolies, or mortgage/unmortgage assets.

## 1.4 Type

🔹 UserJourney

## 1.5 Purpose

To provide a centralized and efficient UI for players to execute strategic asset management actions, fulfilling REQ-1-074.

## 1.6 Complexity

Medium

## 1.7 Priority

🔴 High

## 1.8 Frequency

OnDemand

## 1.9 Participants

- PresentationLayer
- ApplicationServicesLayer
- BusinessLogicLayer

## 1.10 Key Interactions

- Player clicks 'Manage Properties' button.
- PropertyManagementUIController displays all owned properties, grouped by color.
- Player selects a property and clicks an action button (e.g., 'Build House').
- The UI request is sent to PropertyActionService.
- PropertyActionService validates the action with the RuleEngine (e.g., checks for monopoly, even build rule, sufficient cash).
- If valid, the GameState is updated, and the UI refreshes to show the new state (e.g., house added, cash debited).

## 1.11 Triggers

- User clicks the 'Manage Properties' button in the main HUD.

## 1.12 Outcomes

- The player successfully builds, sells, mortgages, or unmortgages a property.
- The player's cash and property states are updated correctly.
- The UI provides clear feedback on why an action is disabled (e.g., insufficient funds, uneven building).

## 1.13 Business Rules

- Even building rule must be enforced (REQ-1-054).
- Building is only allowed on a complete monopoly.
- Properties with buildings cannot be mortgaged.

## 1.14 Error Scenarios

- User attempts to build unevenly.
- User attempts to perform an action with insufficient funds.

## 1.15 Integration Points

*No items available*

# 2.0 Details

## 2.1 Diagram Id

SEQ-UJ-012-IMPL

## 2.2 Name

Implementation: Player Manages Properties via UI

## 2.3 Description

Technical sequence for a human player using the dedicated property management screen. This diagram details the full lifecycle from UI activation, data fetching, action validation via the rule engine, game state updates, and UI refresh for both successful and failed actions. It strictly follows an MVC/MVP pattern where UI controllers orchestrate calls to application services, which in turn use the domain layer for logic and state manipulation.

## 2.4 Participants

### 2.4.1 Actor

#### 2.4.1.1 Repository Id

USER-ACTOR

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
| Shape | Actor |
| Color | #E6E6E6 |
| Stereotype |  |

### 2.4.2.0 UI Component

#### 2.4.2.1 Repository Id

PL-INPUT-HANDLER

#### 2.4.2.2 Display Name

InputHandler

#### 2.4.2.3 Type

🔹 UI Component

#### 2.4.2.4 Technology

Unity Engine, C#

#### 2.4.2.5 Order

2

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | Component |
| Color | #B4E8C8 |
| Stereotype | Presentation |

### 2.4.3.0 UI Component

#### 2.4.3.1 Repository Id

PL-VIEW-MANAGER

#### 2.4.3.2 Display Name

ViewManager

#### 2.4.3.3 Type

🔹 UI Component

#### 2.4.3.4 Technology

Unity Engine, C#

#### 2.4.3.5 Order

3

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | Component |
| Color | #B4E8C8 |
| Stereotype | Presentation |

### 2.4.4.0 UI Component

#### 2.4.4.1 Repository Id

PL-PROP-UI-CONTROLLER

#### 2.4.4.2 Display Name

PropertyManagementUIController

#### 2.4.4.3 Type

🔹 UI Component

#### 2.4.4.4 Technology

Unity Engine, C#

#### 2.4.4.5 Order

4

#### 2.4.4.6 Style

| Property | Value |
|----------|-------|
| Shape | Component |
| Color | #B4E8C8 |
| Stereotype | Presentation |

### 2.4.5.0 Service Layer

#### 2.4.5.1 Repository Id

REPO-AS-005

#### 2.4.5.2 Display Name

ApplicationServices

#### 2.4.5.3 Type

🔹 Service Layer

#### 2.4.5.4 Technology

.NET 8, C#

#### 2.4.5.5 Order

5

#### 2.4.5.6 Style

| Property | Value |
|----------|-------|
| Shape | Boundary |
| Color | #A4D4FF |
| Stereotype | Application |

### 2.4.6.0 Domain Layer

#### 2.4.6.1 Repository Id

REPO-DM-001

#### 2.4.6.2 Display Name

BusinessLogic

#### 2.4.6.3 Type

🔹 Domain Layer

#### 2.4.6.4 Technology

.NET 8, C#

#### 2.4.6.5 Order

6

#### 2.4.6.6 Style

| Property | Value |
|----------|-------|
| Shape | Boundary |
| Color | #FFC0CB |
| Stereotype | Domain |

## 2.5.0.0 Interactions

### 2.5.1.0 User Input

#### 2.5.1.1 Source Id

USER-ACTOR

#### 2.5.1.2 Target Id

PL-INPUT-HANDLER

#### 2.5.1.3 Message

Clicks 'Manage Properties' button during pre-roll phase.

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
| Method | OnPointerClick |
| Parameters | EventData eventData |
| Authentication | N/A |
| Error Handling | UI button is only interactable during the correct ... |
| Performance | Response time < 100ms. |

### 2.5.2.0 Method Call

#### 2.5.2.1 Source Id

PL-INPUT-HANDLER

#### 2.5.2.2 Target Id

PL-VIEW-MANAGER

#### 2.5.2.3 Message

Request to display the property management screen.

#### 2.5.2.4 Sequence Number

2

#### 2.5.2.5 Type

🔹 Method Call

#### 2.5.2.6 Is Synchronous

✅ Yes

#### 2.5.2.7 Return Message



#### 2.5.2.8 Has Return

❌ No

#### 2.5.2.9 Is Activation

✅ Yes

#### 2.5.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | void ShowView(ViewId.PropertyManagement) |
| Parameters | enum ViewId |
| Authentication | N/A |
| Error Handling | Log error if ViewId is not found. |
| Performance | View transition should start within one frame. |

### 2.5.3.0 Component Lifecycle

#### 2.5.3.1 Source Id

PL-VIEW-MANAGER

#### 2.5.3.2 Target Id

PL-PROP-UI-CONTROLLER

#### 2.5.3.3 Message

Activates UI and requests data population.

#### 2.5.3.4 Sequence Number

3

#### 2.5.3.5 Type

🔹 Component Lifecycle

#### 2.5.3.6 Is Synchronous

✅ Yes

#### 2.5.3.7 Return Message



#### 2.5.3.8 Has Return

❌ No

#### 2.5.3.9 Is Activation

✅ Yes

#### 2.5.3.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | void OnEnable() |
| Parameters | None |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Initialization logic must not block the UI thread. |

### 2.5.4.0 Method Call

#### 2.5.4.1 Source Id

PL-PROP-UI-CONTROLLER

#### 2.5.4.2 Target Id

REPO-AS-005

#### 2.5.4.3 Message

Fetch current player's asset information.

#### 2.5.4.4 Sequence Number

4

#### 2.5.4.5 Type

🔹 Method Call

#### 2.5.4.6 Is Synchronous

✅ Yes

#### 2.5.4.7 Return Message

Returns a view model for the UI.

#### 2.5.4.8 Has Return

✅ Yes

#### 2.5.4.9 Is Activation

✅ Yes

#### 2.5.4.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | PropertyManagementViewModel GetPropertyManagementV... |
| Parameters | Guid playerId: ID of the human player |
| Authentication | N/A |
| Error Handling | Throws InvalidOperationException if no active game... |
| Performance | Latency < 50ms. Must not perform heavy computation... |

#### 2.5.4.11 Nested Interactions

- {'sourceId': 'REPO-AS-005', 'targetId': 'REPO-DM-001', 'message': 'Get required data from GameState object.', 'sequenceNumber': 4.1, 'type': 'Data Access', 'isSynchronous': True, 'returnMessage': 'Returns raw player and property data.', 'hasReturn': True, 'isActivation': True, 'technicalDetails': {'protocol': 'In-Process', 'method': 'PlayerState GetPlayerState(Guid playerId)', 'parameters': 'Guid playerId', 'authentication': 'N/A', 'errorHandling': 'Returns null if player not found.', 'performance': 'Direct memory access, near-instantaneous.'}}

### 2.5.5.0 Internal Logic

#### 2.5.5.1 Source Id

PL-PROP-UI-CONTROLLER

#### 2.5.5.2 Target Id

PL-PROP-UI-CONTROLLER

#### 2.5.5.3 Message

Renders property list, cash, and action buttons based on view model.

#### 2.5.5.4 Sequence Number

5

#### 2.5.5.5 Type

🔹 Internal Logic

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
| Protocol | Internal |
| Method | void Render(PropertyManagementViewModel viewModel) |
| Parameters | PropertyManagementViewModel: DTO containing all da... |
| Authentication | N/A |
| Error Handling | Handles empty property list gracefully per AC-008. |
| Performance | UI rendering and layout must complete within one f... |

### 2.5.6.0 User Input

#### 2.5.6.1 Source Id

USER-ACTOR

#### 2.5.6.2 Target Id

PL-PROP-UI-CONTROLLER

#### 2.5.6.3 Message

Selects a property and clicks 'Build House' button.

#### 2.5.6.4 Sequence Number

6

#### 2.5.6.5 Type

🔹 User Input

#### 2.5.6.6 Is Synchronous

✅ Yes

#### 2.5.6.7 Return Message



#### 2.5.6.8 Has Return

❌ No

#### 2.5.6.9 Is Activation

✅ Yes

#### 2.5.6.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | UI Event |
| Method | OnBuildHouseClicked(Guid propertyId) |
| Parameters | Guid propertyId: The unique ID of the selected pro... |
| Authentication | N/A |
| Error Handling | Button click event triggers the action. |
| Performance | UI should be immediately responsive. |

### 2.5.7.0 Method Call

#### 2.5.7.1 Source Id

PL-PROP-UI-CONTROLLER

#### 2.5.7.2 Target Id

REPO-AS-005

#### 2.5.7.3 Message

Request to execute the 'Build House' action.

#### 2.5.7.4 Sequence Number

7

#### 2.5.7.5 Type

🔹 Method Call

#### 2.5.7.6 Is Synchronous

✅ Yes

#### 2.5.7.7 Return Message

Returns result of the action and updated player state.

#### 2.5.7.8 Has Return

✅ Yes

#### 2.5.7.9 Is Activation

✅ Yes

#### 2.5.7.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | ActionResult AttemptBuildHouse(Guid playerId, Guid... |
| Parameters | Guid playerId, Guid propertyId |
| Authentication | N/A |
| Error Handling | Wraps all exceptions in a standard ActionResult DT... |
| Performance | Should complete in < 100ms. |

### 2.5.8.0 Validation

#### 2.5.8.1 Source Id

REPO-AS-005

#### 2.5.8.2 Target Id

REPO-DM-001

#### 2.5.8.3 Message

Validate build action against game rules.

#### 2.5.8.4 Sequence Number

8

#### 2.5.8.5 Type

🔹 Validation

#### 2.5.8.6 Is Synchronous

✅ Yes

#### 2.5.8.7 Return Message

Returns a validation result object.

#### 2.5.8.8 Has Return

✅ Yes

#### 2.5.8.9 Is Activation

✅ Yes

#### 2.5.8.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | ValidationResult CanBuildHouse(GameState state, Gu... |
| Parameters | GameState, Guid, Guid |
| Authentication | N/A |
| Error Handling | Returns result object with specific failure reason... |
| Performance | Near-instantaneous. |

### 2.5.9.0 State Mutation

#### 2.5.9.1 Source Id

REPO-AS-005

#### 2.5.9.2 Target Id

REPO-DM-001

#### 2.5.9.3 Message

Apply changes to the GameState object.

#### 2.5.9.4 Sequence Number

9

#### 2.5.9.5 Type

🔹 State Mutation

#### 2.5.9.6 Is Synchronous

✅ Yes

#### 2.5.9.7 Return Message

Returns updated PlayerState for UI refresh.

#### 2.5.9.8 Has Return

✅ Yes

#### 2.5.9.9 Is Activation

✅ Yes

#### 2.5.9.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | PlayerState ExecuteBuildHouse(GameState state, Gui... |
| Parameters | GameState, Guid, Guid |
| Authentication | N/A |
| Error Handling | Throws if state mutation fails unexpectedly. This ... |
| Performance | Direct memory access, near-instantaneous. |

### 2.5.10.0 Internal Logic

#### 2.5.10.1 Source Id

PL-PROP-UI-CONTROLLER

#### 2.5.10.2 Target Id

PL-PROP-UI-CONTROLLER

#### 2.5.10.3 Message

Refresh UI with updated state (new house, less cash).

#### 2.5.10.4 Sequence Number

10

#### 2.5.10.5 Type

🔹 Internal Logic

#### 2.5.10.6 Is Synchronous

✅ Yes

#### 2.5.10.7 Return Message



#### 2.5.10.8 Has Return

❌ No

#### 2.5.10.9 Is Activation

❌ No

#### 2.5.10.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Internal |
| Method | void UpdateView(PlayerState updatedState) |
| Parameters | PlayerState updatedState |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Partial UI update must be performant, avoiding a f... |

### 2.5.11.0 User Input

#### 2.5.11.1 Source Id

USER-ACTOR

#### 2.5.11.2 Target Id

PL-PROP-UI-CONTROLLER

#### 2.5.11.3 Message

Attempts to mortgage a developed property.

#### 2.5.11.4 Sequence Number

11

#### 2.5.11.5 Type

🔹 User Input

#### 2.5.11.6 Is Synchronous

✅ Yes

#### 2.5.11.7 Return Message



#### 2.5.11.8 Has Return

❌ No

#### 2.5.11.9 Is Activation

✅ Yes

#### 2.5.11.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | UI Event |
| Method | OnMortgageClicked(Guid propertyId) |
| Parameters | Guid propertyId |
| Authentication | N/A |
| Error Handling | Button is disabled in the UI per AC-007. This inte... |
| Performance | N/A |

### 2.5.12.0 Method Call

#### 2.5.12.1 Source Id

PL-PROP-UI-CONTROLLER

#### 2.5.12.2 Target Id

REPO-AS-005

#### 2.5.12.3 Message

Request to execute the 'Mortgage' action.

#### 2.5.12.4 Sequence Number

12

#### 2.5.12.5 Type

🔹 Method Call

#### 2.5.12.6 Is Synchronous

✅ Yes

#### 2.5.12.7 Return Message

Returns failed action result.

#### 2.5.12.8 Has Return

✅ Yes

#### 2.5.12.9 Is Activation

✅ Yes

#### 2.5.12.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | ActionResult AttemptMortgage(Guid playerId, Guid p... |
| Parameters | Guid playerId, Guid propertyId |
| Authentication | N/A |
| Error Handling | Wraps all exceptions in a standard ActionResult DT... |
| Performance | Should complete in < 100ms. |

### 2.5.13.0 Validation

#### 2.5.13.1 Source Id

REPO-AS-005

#### 2.5.13.2 Target Id

REPO-DM-001

#### 2.5.13.3 Message

Validate mortgage action against game rules.

#### 2.5.13.4 Sequence Number

13

#### 2.5.13.5 Type

🔹 Validation

#### 2.5.13.6 Is Synchronous

✅ Yes

#### 2.5.13.7 Return Message

Returns failed validation result.

#### 2.5.13.8 Has Return

✅ Yes

#### 2.5.13.9 Is Activation

✅ Yes

#### 2.5.13.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | ValidationResult CanMortgage(GameState state, Guid... |
| Parameters | GameState, Guid, Guid |
| Authentication | N/A |
| Error Handling | Returns result object with failure reason: PROPERT... |
| Performance | Near-instantaneous. |

### 2.5.14.0 UI Feedback

#### 2.5.14.1 Source Id

PL-PROP-UI-CONTROLLER

#### 2.5.14.2 Target Id

PL-PROP-UI-CONTROLLER

#### 2.5.14.3 Message

Display non-intrusive error notification.

#### 2.5.14.4 Sequence Number

14

#### 2.5.14.5 Type

🔹 UI Feedback

#### 2.5.14.6 Is Synchronous

✅ Yes

#### 2.5.14.7 Return Message



#### 2.5.14.8 Has Return

❌ No

#### 2.5.14.9 Is Activation

❌ No

#### 2.5.14.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | Internal |
| Method | void ShowNotification(string message, Notification... |
| Parameters | string message: 'Property must be undeveloped to m... |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Notification should appear and fade without impact... |

### 2.5.15.0 User Input

#### 2.5.15.1 Source Id

USER-ACTOR

#### 2.5.15.2 Target Id

PL-VIEW-MANAGER

#### 2.5.15.3 Message

Clicks 'Close' button to exit screen.

#### 2.5.15.4 Sequence Number

15

#### 2.5.15.5 Type

🔹 User Input

#### 2.5.15.6 Is Synchronous

✅ Yes

#### 2.5.15.7 Return Message



#### 2.5.15.8 Has Return

❌ No

#### 2.5.15.9 Is Activation

✅ Yes

#### 2.5.15.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | UI Event |
| Method | OnPointerClick |
| Parameters | EventData eventData |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Immediate response. |

### 2.5.16.0 Component Lifecycle

#### 2.5.16.1 Source Id

PL-VIEW-MANAGER

#### 2.5.16.2 Target Id

PL-PROP-UI-CONTROLLER

#### 2.5.16.3 Message

Deactivates the UI component.

#### 2.5.16.4 Sequence Number

16

#### 2.5.16.5 Type

🔹 Component Lifecycle

#### 2.5.16.6 Is Synchronous

✅ Yes

#### 2.5.16.7 Return Message



#### 2.5.16.8 Has Return

❌ No

#### 2.5.16.9 Is Activation

✅ Yes

#### 2.5.16.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | void OnDisable() |
| Parameters | None |
| Authentication | N/A |
| Error Handling | N/A |
| Performance | Cleanup should be immediate. |

## 2.6.0.0 Notes

### 2.6.1.0 Content

#### 2.6.1.1 Content

The PropertyManagementUIController serves as the Controller/Presenter in an MVC/MVP pattern. It is responsible for translating user inputs into service calls and updating the View (Unity GameObjects) based on the results.

#### 2.6.1.2 Position

Top

#### 2.6.1.3 Participant Id

PL-PROP-UI-CONTROLLER

#### 2.6.1.4 Sequence Number

0

### 2.6.2.0 Content

#### 2.6.2.1 Content

All action validation logic is centralized in the RuleEngine. The Application Services layer acts as an orchestrator, and the Presentation Layer should not contain any business rule logic beyond simple display formatting.

#### 2.6.2.2 Position

Bottom

#### 2.6.2.3 Participant Id

REPO-DM-001

#### 2.6.2.4 Sequence Number

8

## 2.7.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | Client-side validation (disabling buttons) is for ... |
| Performance Targets | The property management UI must load in under 500m... |
| Error Handling Strategy | Service layer methods must not throw exceptions fo... |
| Testing Considerations | Unit test the RuleEngine for all validation edge c... |
| Monitoring Requirements | Log INFO messages for every successful property ma... |
| Deployment Considerations | UI assets for the property management screen shoul... |

