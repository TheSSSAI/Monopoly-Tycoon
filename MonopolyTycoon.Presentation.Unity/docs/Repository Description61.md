# 1 Id

REPO-PU-010

# 2 Name

MonopolyTycoon.Presentation.Unity

# 3 Description

This is the main application repository, containing all code and assets for the Unity game engine. It was refactored from the original `MonopolyTycoon.Unity` to clarify its role as the Presentation Layer and the composition root for the entire application. It is responsible for all user-facing concerns: rendering the 3D board (REQ-1-005), displaying the UI (HUD, menus, dialogs per REQ-1-071), handling user input, and playing audio. At startup, this project is responsible for initializing the Dependency Injection container, wiring together the concrete implementations from the Infrastructure layer with the services from the Application layer, and starting the main game loop. While it is a single repository for practical Unity development, it is architected internally to respect the separation of concerns, with clear boundaries between UI controllers (Presenters), views, and the services they consume.

# 4 Type

🔹 Presentation

# 5 Namespace

MonopolyTycoon.Presentation

# 6 Output Path

src/presentation/MonopolyTycoon.Presentation.Unity

# 7 Framework

Unity Engine

# 8 Language

C#

# 9 Technology

Unity, .NET 8

# 10 Thirdparty Libraries

*No items available*

# 11 Layer Ids

- presentation_layer

# 12 Dependencies

- REPO-DM-001
- REPO-AA-004
- REPO-AS-005
- REPO-IL-006

# 13 Requirements

## 13.1 Requirement Id

### 13.1.1 Requirement Id

REQ-1-011

## 13.2.0 Requirement Id

### 13.2.1 Requirement Id

REQ-1-014

## 13.3.0 Requirement Id

### 13.3.1 Requirement Id

REQ-1-017

## 13.4.0 Requirement Id

### 13.4.1 Requirement Id

REQ-1-071

# 14.0.0 Generate Tests

✅ Yes

# 15.0.0 Generate Documentation

✅ Yes

# 16.0.0 Architecture Style

Model-View-Controller (MVP)

# 17.0.0 Architecture Map

- ViewManager
- GameBoardPresenter
- InputController
- HUDController

# 18.0.0 Components Map

- view-manager-201
- game-board-presenter-202
- hud-controller-203

# 19.0.0 Requirements Map

- REQ-1-011
- REQ-1-014

# 20.0.0 Dependency Contracts

## 20.1.0 Repo-As-005

### 20.1.1 Required Interfaces

#### 20.1.1.1 Interface

##### 20.1.1.1.1 Interface

IGameSessionService

##### 20.1.1.1.2 Methods

- Task StartNewGameAsync(GameSetupOptions options)
- Task LoadGameAsync(int slot)
- Task SaveGameAsync(int slot)

##### 20.1.1.1.3 Events

*No items available*

##### 20.1.1.1.4 Properties

*No items available*

#### 20.1.1.2.0 Interface

##### 20.1.1.2.1 Interface

ITurnManagementService

##### 20.1.1.2.2 Methods

- Task ExecutePlayerActionAsync(PlayerAction action)

##### 20.1.1.2.3 Events

*No items available*

##### 20.1.1.2.4 Properties

*No items available*

### 20.1.2.0.0 Integration Pattern

Dependency Injection. Unity scripts (MonoBehaviours) will resolve application services from a central DI container initialized at startup.

### 20.1.3.0.0 Communication Protocol

In-memory asynchronous method calls.

# 21.0.0.0.0 Exposed Contracts

## 21.1.0.0.0 Public Interfaces

*No items available*

# 22.0.0.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | Acts as the Composition Root. A startup script is ... |
| Event Communication | Subscribes to application-level events (e.g., `Gam... |
| Data Flow | Follows an MVP/MVC pattern. It captures raw user i... |
| Error Handling | Implements the user-facing modal error dialog (REQ... |
| Async Patterns | Uses Unity's coroutines or async/await wrappers (l... |

# 23.0.0.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Adhere to Unity best practices. Use prefabs for UI... |
| Performance Considerations | This is the most performance-critical repository. ... |
| Security Considerations | N/A |
| Testing Approach | Use the Unity Test Framework for integration and p... |

# 24.0.0.0.0 Scope Boundaries

## 24.1.0.0.0 Must Implement

- All visual and audio elements of the game.
- Handling of all user input via Unity's Input System.
- Initialization of the DI container and all other application layers (Composition Root).
- Rendering of the 3D board, tokens, and UI elements.

## 24.2.0.0.0 Must Not Implement

- Any core game rules or business logic (e.g., calculating rent).
- Direct data persistence; must always go through an application service.
- AI decision-making strategies.

## 24.3.0.0.0 Extension Points

- The theming system (REQ-1-093) is a major extension point, allowing for swapping of visual and audio assets.
- New UI screens and dialogs can be added as prefabs to support new features.

## 24.4.0.0.0 Validation Rules

- Responsible for basic user input validation, such as character limits and allowed characters for player name entry (REQ-1-032).

