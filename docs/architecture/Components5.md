# 1 Components

## 1.1 Components

### 1.1.1 Engine

#### 1.1.1.1 Id

game-engine-001

#### 1.1.1.2 Name

GameEngine

#### 1.1.1.3 Description

The central component in the Business Logic Layer. It encapsulates the complete game state and strictly enforces all official Monopoly rules as per REQ-1-003. It is technology-agnostic and highly testable.

#### 1.1.1.4 Type

🔹 Engine

#### 1.1.1.5 Dependencies

- ai-behavior-tree-executor-002
- logging-service-101

#### 1.1.1.6 Properties

| Property | Value |
|----------|-------|
| Domain | BusinessLogic |
| Test Coverage Target | 70% |

#### 1.1.1.7 Interfaces

- IGameRuleEnforcer
- IGameStateProvider

#### 1.1.1.8 Technology

.NET 8, C#, NUnit

#### 1.1.1.9 Resources

##### 1.1.1.9.1 Cpu

High

##### 1.1.1.9.2 Memory

High

#### 1.1.1.10.0 Configuration

*No data available*

#### 1.1.1.11.0 Health Check

*No data available*

#### 1.1.1.12.0 Responsible Features

- REQ-1-003
- REQ-1-037
- REQ-1-038
- REQ-1-041
- REQ-1-043
- REQ-1-044
- REQ-1-045
- REQ-1-046
- REQ-1-047
- REQ-1-048
- REQ-1-053
- REQ-1-054
- REQ-1-055
- REQ-1-065
- REQ-1-066
- REQ-1-067

#### 1.1.1.13.0 Security

##### 1.1.1.13.1 Requires Authentication

❌ No

##### 1.1.1.13.2 Requires Authorization

❌ No

### 1.1.2.0.0 Engine

#### 1.1.2.1.0 Id

ai-behavior-tree-executor-002

#### 1.1.2.2.0 Name

AIBehaviorTreeExecutor

#### 1.1.2.3.0 Description

A component within the Business Logic Layer that executes Behavior Trees to make decisions for AI opponents. Its logic is tuned by parameters loaded from an external JSON file, fulfilling REQ-1-063.

#### 1.1.2.4.0 Type

🔹 Engine

#### 1.1.2.5.0 Dependencies

- game-engine-001
- configuration-loader-103
- logging-service-101

#### 1.1.2.6.0 Properties

| Property | Value |
|----------|-------|
| Domain | BusinessLogic |
| Configurable | true |

#### 1.1.2.7.0 Interfaces

- IAIController

#### 1.1.2.8.0 Technology

.NET 8, C#, Behavior Tree Framework

#### 1.1.2.9.0 Resources

##### 1.1.2.9.1 Cpu

Medium

#### 1.1.2.10.0 Configuration

##### 1.1.2.10.1 Param File

ai_config.json

#### 1.1.2.11.0 Health Check

*No data available*

#### 1.1.2.12.0 Responsible Features

- REQ-1-004
- REQ-1-059
- REQ-1-060
- REQ-1-061
- REQ-1-062
- REQ-1-063
- REQ-1-064

#### 1.1.2.13.0 Security

##### 1.1.2.13.1 Requires Authentication

❌ No

##### 1.1.2.13.2 Requires Authorization

❌ No

### 1.1.3.0.0 Service

#### 1.1.3.1.0 Id

game-session-service-051

#### 1.1.3.2.0 Name

GameSessionService

#### 1.1.3.3.0 Description

An Application Services Layer component that orchestrates the overall game lifecycle. It translates high-level user actions (New Game, Load, Save) into coordinated operations across the Business Logic and Infrastructure layers.

#### 1.1.3.4.0 Type

🔹 Service

#### 1.1.3.5.0 Dependencies

- game-engine-001
- save-game-repository-104
- statistics-repository-105
- view-manager-201

#### 1.1.3.6.0 Properties

| Property | Value |
|----------|-------|
| Domain | ApplicationServices |

#### 1.1.3.7.0 Interfaces

- IGameSession

#### 1.1.3.8.0 Technology

.NET 8, C#

#### 1.1.3.9.0 Resources

*No data available*

#### 1.1.3.10.0 Configuration

*No data available*

#### 1.1.3.11.0 Health Check

*No data available*

#### 1.1.3.12.0 Responsible Features

- REQ-1-015
- REQ-1-030
- REQ-1-068
- REQ-1-069
- REQ-1-070
- REQ-1-085

#### 1.1.3.13.0 Security

##### 1.1.3.13.1 Requires Authentication

❌ No

##### 1.1.3.13.2 Requires Authorization

❌ No

### 1.1.4.0.0 Service

#### 1.1.4.1.0 Id

turn-management-service-052

#### 1.1.4.2.0 Name

TurnManagementService

#### 1.1.4.3.0 Description

Part of the Application Services Layer, this component manages the state machine for a player's turn, sequencing through the distinct phases (Pre-Roll, Roll, Action, etc.) and invoking the GameEngine for rule enforcement.

#### 1.1.4.4.0 Type

🔹 Service

#### 1.1.4.5.0 Dependencies

- game-engine-001
- ai-behavior-tree-executor-002
- game-board-presenter-202

#### 1.1.4.6.0 Properties

| Property | Value |
|----------|-------|
| Domain | ApplicationServices |

#### 1.1.4.7.0 Interfaces

- ITurnManager

#### 1.1.4.8.0 Technology

.NET 8, C#

#### 1.1.4.9.0 Resources

*No data available*

#### 1.1.4.10.0 Configuration

*No data available*

#### 1.1.4.11.0 Health Check

*No data available*

#### 1.1.4.12.0 Responsible Features

- REQ-1-038
- REQ-1-039
- REQ-1-040

#### 1.1.4.13.0 Security

##### 1.1.4.13.1 Requires Authentication

❌ No

##### 1.1.4.13.2 Requires Authorization

❌ No

### 1.1.5.0.0 Service

#### 1.1.5.1.0 Id

logging-service-101

#### 1.1.5.2.0 Name

LoggingService

#### 1.1.5.3.0 Description

An Infrastructure Layer wrapper for the Serilog framework. It provides a centralized, configurable logging system for the entire application, handling structured JSON output, file rotation, and log levels as per requirements.

#### 1.1.5.4.0 Type

🔹 Service

#### 1.1.5.5.0 Dependencies

*No items available*

#### 1.1.5.6.0 Properties

| Property | Value |
|----------|-------|
| Domain | Infrastructure |
| Log Path | %APPDATA%/MonopolyTycoon/logs/ |

#### 1.1.5.7.0 Interfaces

- ILogger

#### 1.1.5.8.0 Technology

Serilog, .NET 8, C#

#### 1.1.5.9.0 Resources

*No data available*

#### 1.1.5.10.0 Configuration

| Property | Value |
|----------|-------|
| Format | JSON |
| Retention Days | 7 |
| Max Size Mb | 50 |

#### 1.1.5.11.0 Health Check

*No data available*

#### 1.1.5.12.0 Responsible Features

- REQ-1-018
- REQ-1-019
- REQ-1-020
- REQ-1-021
- REQ-1-022
- REQ-1-028

#### 1.1.5.13.0 Security

##### 1.1.5.13.1 Requires Authentication

❌ No

##### 1.1.5.13.2 Requires Authorization

❌ No

### 1.1.6.0.0 Service

#### 1.1.6.1.0 Id

localization-service-102

#### 1.1.6.2.0 Name

LocalizationService

#### 1.1.6.3.0 Description

An Infrastructure Layer component responsible for loading user-facing text from external resource files. It provides a key-based lookup system, ensuring no strings are hardcoded and the application is ready for localization.

#### 1.1.6.4.0 Type

🔹 Service

#### 1.1.6.5.0 Dependencies

*No items available*

#### 1.1.6.6.0 Properties

| Property | Value |
|----------|-------|
| Domain | Infrastructure |

#### 1.1.6.7.0 Interfaces

- ILocalizationProvider

#### 1.1.6.8.0 Technology

.NET 8, C#

#### 1.1.6.9.0 Resources

*No data available*

#### 1.1.6.10.0 Configuration

##### 1.1.6.10.1 Resource Path

Assets/Localization/

##### 1.1.6.10.2 Default Language

en-US

#### 1.1.6.11.0 Health Check

*No data available*

#### 1.1.6.12.0 Responsible Features

- REQ-1-083
- REQ-1-084

#### 1.1.6.13.0 Security

##### 1.1.6.13.1 Requires Authentication

❌ No

##### 1.1.6.13.2 Requires Authorization

❌ No

### 1.1.7.0.0 Service

#### 1.1.7.1.0 Id

configuration-loader-103

#### 1.1.7.2.0 Name

ConfigurationLoader

#### 1.1.7.3.0 Description

An Infrastructure Layer component that reads and parses external JSON configuration files, such as the AI behavior parameters. This allows for runtime tuning without recompiling the application.

#### 1.1.7.4.0 Type

🔹 Service

#### 1.1.7.5.0 Dependencies

*No items available*

#### 1.1.7.6.0 Properties

| Property | Value |
|----------|-------|
| Domain | Infrastructure |

#### 1.1.7.7.0 Interfaces

- IConfigurationProvider

#### 1.1.7.8.0 Technology

.NET 8, C#, System.Text.Json

#### 1.1.7.9.0 Resources

*No data available*

#### 1.1.7.10.0 Configuration

*No data available*

#### 1.1.7.11.0 Health Check

*No data available*

#### 1.1.7.12.0 Responsible Features

- REQ-1-063

#### 1.1.7.13.0 Security

##### 1.1.7.13.1 Requires Authentication

❌ No

##### 1.1.7.13.2 Requires Authorization

❌ No

### 1.1.8.0.0 Repository

#### 1.1.8.1.0 Id

save-game-repository-104

#### 1.1.8.2.0 Name

SaveGameRepository

#### 1.1.8.3.0 Description

Implements the Repository Pattern in the Infrastructure Layer for game save data. It serializes the GameState object to versioned JSON files and performs checksum validation on load.

#### 1.1.8.4.0 Type

🔹 Repository

#### 1.1.8.5.0 Dependencies

- logging-service-101
- data-migration-manager-107

#### 1.1.8.6.0 Properties

| Property | Value |
|----------|-------|
| Domain | Infrastructure |
| Save Path | %APPDATA%/MonopolyTycoon/saves/ |

#### 1.1.8.7.0 Interfaces

- ISaveGameRepository

#### 1.1.8.8.0 Technology

.NET 8, C#, System.Text.Json

#### 1.1.8.9.0 Resources

*No data available*

#### 1.1.8.10.0 Configuration

##### 1.1.8.10.1 Max Slots

5

#### 1.1.8.11.0 Health Check

##### 1.1.8.11.1 Path

/check-save-dir-writable

#### 1.1.8.12.0 Responsible Features

- REQ-1-085
- REQ-1-086
- REQ-1-087
- REQ-1-088

#### 1.1.8.13.0 Security

##### 1.1.8.13.1 Requires Authentication

❌ No

##### 1.1.8.13.2 Requires Authorization

❌ No

### 1.1.9.0.0 Repository

#### 1.1.9.1.0 Id

statistics-repository-105

#### 1.1.9.2.0 Name

StatisticsRepository

#### 1.1.9.3.0 Description

An Infrastructure Layer component implementing the Repository Pattern for player statistics and game results. It manages all interactions with the local SQLite database.

#### 1.1.9.4.0 Type

🔹 Repository

#### 1.1.9.5.0 Dependencies

- logging-service-101
- data-migration-manager-107

#### 1.1.9.6.0 Properties

| Property | Value |
|----------|-------|
| Domain | Infrastructure |
| Db Path | %APPDATA%/MonopolyTycoon/stats.db |

#### 1.1.9.7.0 Interfaces

- IStatisticsRepository
- IPlayerProfileRepository

#### 1.1.9.8.0 Technology

Microsoft.Data.Sqlite, .NET 8, C#

#### 1.1.9.9.0 Resources

*No data available*

#### 1.1.9.10.0 Configuration

##### 1.1.9.10.1 Max Backups

3

#### 1.1.9.11.0 Health Check

##### 1.1.9.11.1 Path

/check-db-connection

#### 1.1.9.12.0 Responsible Features

- REQ-1-032
- REQ-1-033
- REQ-1-034
- REQ-1-089
- REQ-1-091

#### 1.1.9.13.0 Security

##### 1.1.9.13.1 Requires Authentication

❌ No

##### 1.1.9.13.2 Requires Authorization

❌ No

### 1.1.10.0.0 Service

#### 1.1.10.1.0 Id

data-migration-manager-107

#### 1.1.10.2.0 Name

DataMigrationManager

#### 1.1.10.3.0 Description

An Infrastructure Layer component responsible for handling older versions of save files and the statistics database. It attempts to automatically and atomically migrate data to the current version's format.

#### 1.1.10.4.0 Type

🔹 Service

#### 1.1.10.5.0 Dependencies

- logging-service-101

#### 1.1.10.6.0 Properties

| Property | Value |
|----------|-------|
| Domain | Infrastructure |

#### 1.1.10.7.0 Interfaces

- IDataMigrator

#### 1.1.10.8.0 Technology

.NET 8, C#

#### 1.1.10.9.0 Resources

*No data available*

#### 1.1.10.10.0 Configuration

*No data available*

#### 1.1.10.11.0 Health Check

*No data available*

#### 1.1.10.12.0 Responsible Features

- REQ-1-090

#### 1.1.10.13.0 Security

##### 1.1.10.13.1 Requires Authentication

❌ No

##### 1.1.10.13.2 Requires Authorization

❌ No

### 1.1.11.0.0 Controller

#### 1.1.11.1.0 Id

view-manager-201

#### 1.1.11.2.0 Name

ViewManager

#### 1.1.11.3.0 Description

The main controller in the Presentation Layer. It manages the lifecycle of different UI views/scenes (e.g., Main Menu, Game Setup, Game Board, Settings) and handles navigation between them.

#### 1.1.11.4.0 Type

🔹 Controller

#### 1.1.11.5.0 Dependencies

- game-session-service-051
- input-handler-206

#### 1.1.11.6.0 Properties

| Property | Value |
|----------|-------|
| Domain | Presentation |

#### 1.1.11.7.0 Interfaces

- IViewManager

#### 1.1.11.8.0 Technology

Unity, C#

#### 1.1.11.9.0 Resources

*No data available*

#### 1.1.11.10.0 Configuration

*No data available*

#### 1.1.11.11.0 Health Check

*No data available*

#### 1.1.11.12.0 Responsible Features

- REQ-1-016
- REQ-1-073
- REQ-1-075
- REQ-1-077

#### 1.1.11.13.0 Security

##### 1.1.11.13.1 Requires Authentication

❌ No

##### 1.1.11.13.2 Requires Authorization

❌ No

### 1.1.12.0.0 Presenter

#### 1.1.12.1.0 Id

game-board-presenter-202

#### 1.1.12.2.0 Name

GameBoardPresenter

#### 1.1.12.3.0 Description

A Presentation Layer component responsible for rendering the state of the game on the 3D board. It updates token positions, property ownership markers, and house/hotel models based on events from the Application layer.

#### 1.1.12.4.0 Type

🔹 Presenter

#### 1.1.12.5.0 Dependencies

- turn-management-service-052
- vfx-manager-204
- audio-manager-205

#### 1.1.12.6.0 Properties

| Property | Value |
|----------|-------|
| Domain | Presentation |

#### 1.1.12.7.0 Interfaces

*No items available*

#### 1.1.12.8.0 Technology

Unity, C#

#### 1.1.12.9.0 Resources

##### 1.1.12.9.1 Cpu

High (Rendering)

##### 1.1.12.9.2 Memory

High (Assets)

#### 1.1.12.10.0 Configuration

*No data available*

#### 1.1.12.11.0 Health Check

*No data available*

#### 1.1.12.12.0 Responsible Features

- REQ-1-002
- REQ-1-005
- REQ-1-017
- REQ-1-072

#### 1.1.12.13.0 Security

##### 1.1.12.13.1 Requires Authentication

❌ No

##### 1.1.12.13.2 Requires Authorization

❌ No

### 1.1.13.0.0 Controller

#### 1.1.13.1.0 Id

hud-controller-203

#### 1.1.13.2.0 Name

HUDController

#### 1.1.13.3.0 Description

A Presentation Layer component that manages the Heads-Up Display. It displays essential, persistent information for all players, such as cash, name, and current turn indicator.

#### 1.1.13.4.0 Type

🔹 Controller

#### 1.1.13.5.0 Dependencies

- game-session-service-051

#### 1.1.13.6.0 Properties

| Property | Value |
|----------|-------|
| Domain | Presentation |

#### 1.1.13.7.0 Interfaces

*No items available*

#### 1.1.13.8.0 Technology

Unity, C#

#### 1.1.13.9.0 Resources

*No data available*

#### 1.1.13.10.0 Configuration

*No data available*

#### 1.1.13.11.0 Health Check

*No data available*

#### 1.1.13.12.0 Responsible Features

- REQ-1-071
- REQ-1-074
- REQ-1-076

#### 1.1.13.13.0 Security

##### 1.1.13.13.1 Requires Authentication

❌ No

##### 1.1.13.13.2 Requires Authorization

❌ No

### 1.1.14.0.0 Manager

#### 1.1.14.1.0 Id

vfx-manager-204

#### 1.1.14.2.0 Name

VFXManager

#### 1.1.14.3.0 Description

A Presentation Layer component responsible for triggering and managing all visual effects, such as dice roll animations, transaction effects, and property highlighting.

#### 1.1.14.4.0 Type

🔹 Manager

#### 1.1.14.5.0 Dependencies

*No items available*

#### 1.1.14.6.0 Properties

| Property | Value |
|----------|-------|
| Domain | Presentation |

#### 1.1.14.7.0 Interfaces

- IVFXPlayer

#### 1.1.14.8.0 Technology

Unity, C#

#### 1.1.14.9.0 Resources

*No data available*

#### 1.1.14.10.0 Configuration

*No data available*

#### 1.1.14.11.0 Health Check

*No data available*

#### 1.1.14.12.0 Responsible Features

- REQ-1-005
- REQ-1-017

#### 1.1.14.13.0 Security

##### 1.1.14.13.1 Requires Authentication

❌ No

##### 1.1.14.13.2 Requires Authorization

❌ No

### 1.1.15.0.0 Manager

#### 1.1.15.1.0 Id

audio-manager-205

#### 1.1.15.2.0 Name

AudioManager

#### 1.1.15.3.0 Description

Handles all audio playback in the Presentation Layer, including context-aware background music and sound effects for game actions, with volume controls as specified in settings.

#### 1.1.15.4.0 Type

🔹 Manager

#### 1.1.15.5.0 Dependencies

*No items available*

#### 1.1.15.6.0 Properties

| Property | Value |
|----------|-------|
| Domain | Presentation |

#### 1.1.15.7.0 Interfaces

- IAudioPlayer

#### 1.1.15.8.0 Technology

Unity, C#

#### 1.1.15.9.0 Resources

*No data available*

#### 1.1.15.10.0 Configuration

*No data available*

#### 1.1.15.11.0 Health Check

*No data available*

#### 1.1.15.12.0 Responsible Features

- REQ-1-079
- REQ-1-094

#### 1.1.15.13.0 Security

##### 1.1.15.13.1 Requires Authentication

❌ No

##### 1.1.15.13.2 Requires Authorization

❌ No

### 1.1.16.0.0 Controller

#### 1.1.16.1.0 Id

input-handler-206

#### 1.1.16.2.0 Name

InputHandler

#### 1.1.16.3.0 Description

A Presentation Layer component that captures all raw user input (mouse clicks, keyboard presses) and translates them into semantic application events, which are then passed to other UI controllers or services.

#### 1.1.16.4.0 Type

🔹 Controller

#### 1.1.16.5.0 Dependencies

- view-manager-201

#### 1.1.16.6.0 Properties

| Property | Value |
|----------|-------|
| Domain | Presentation |

#### 1.1.16.7.0 Interfaces

- IInputProvider

#### 1.1.16.8.0 Technology

Unity, C#

#### 1.1.16.9.0 Resources

*No data available*

#### 1.1.16.10.0 Configuration

*No data available*

#### 1.1.16.11.0 Health Check

*No data available*

#### 1.1.16.12.0 Responsible Features

- REQ-1-039
- REQ-1-040

#### 1.1.16.13.0 Security

##### 1.1.16.13.1 Requires Authentication

❌ No

##### 1.1.16.13.2 Requires Authorization

❌ No

## 1.2.0.0.0 Configuration

### 1.2.1.0.0 Environment

Production

### 1.2.2.0.0 Logging Level

INFO

### 1.2.3.0.0 Database Url

local_sqlite_file

### 1.2.4.0.0 Feature Flags

#### 1.2.4.1.0 Enable Tutorial

✅ Yes

#### 1.2.4.2.0 Enable Update Check

✅ Yes

# 2.0.0.0.0 Component Relations

## 2.1.0.0.0 Architecture

### 2.1.1.0.0 Components

#### 2.1.1.1.0 Controller

##### 2.1.1.1.1 Id

ui-manager-001

##### 2.1.1.1.2 Name

UIManager

##### 2.1.1.1.3 Description

A high-level controller in the Presentation Layer that manages the lifecycle of all UI screens and panels, such as the Main Menu, Game Setup, In-Game HUD, and Settings. It orchestrates transitions between different UI states.

##### 2.1.1.1.4 Type

🔹 Controller

##### 2.1.1.1.5 Layer

Presentation Layer

##### 2.1.1.1.6 Dependencies

- game-session-service-101
- audio-manager-009

##### 2.1.1.1.7 Properties

| Property | Value |
|----------|-------|
| Framework | Unity UI (uGUI) |

##### 2.1.1.1.8 Interfaces

- IUIManager

##### 2.1.1.1.9 Technology

Unity Engine, C#

##### 2.1.1.1.10 Resources

###### 2.1.1.1.10.1 Cpu

N/A (Part of main game loop)

###### 2.1.1.1.10.2 Memory

Dependent on UI complexity

##### 2.1.1.1.11.0 Health Check

*Not specified*

##### 2.1.1.1.12.0 Responsible Features

- REQ-1-075
- REQ-1-077

##### 2.1.1.1.13.0 Security

###### 2.1.1.1.13.1 Requires Authentication

❌ No

###### 2.1.1.1.13.2 Requires Authorization

❌ No

#### 2.1.1.2.0.0 View

##### 2.1.1.2.1.0 Id

game-board-view-002

##### 2.1.1.2.2.0 Name

GameBoardView

##### 2.1.1.2.3.0 Description

Responsible for rendering the 3D game board, player tokens, houses, and hotels. It observes the GameState and updates the visual representation of the board accordingly, including animations for token movement and visual effects.

##### 2.1.1.2.4.0 Type

🔹 View

##### 2.1.1.2.5.0 Layer

Presentation Layer

##### 2.1.1.2.6.0 Dependencies

- vfx-manager-008
- audio-manager-009

##### 2.1.1.2.7.0 Properties

| Property | Value |
|----------|-------|
| Rendering Pipeline | URP/HDRP |

##### 2.1.1.2.8.0 Interfaces

- IGameBoardView

##### 2.1.1.2.9.0 Technology

Unity Engine, C#

##### 2.1.1.2.10.0 Resources

###### 2.1.1.2.10.1 Cpu

GPU Intensive

###### 2.1.1.2.10.2 Memory

VRAM Intensive

##### 2.1.1.2.11.0 Health Check

*Not specified*

##### 2.1.1.2.12.0 Responsible Features

- REQ-1-005
- REQ-1-017
- REQ-1-072

##### 2.1.1.2.13.0 Security

###### 2.1.1.2.13.1 Requires Authentication

❌ No

###### 2.1.1.2.13.2 Requires Authorization

❌ No

#### 2.1.1.3.0.0 Controller

##### 2.1.1.3.1.0 Id

hud-controller-003

##### 2.1.1.3.2.0 Name

HUDController

##### 2.1.1.3.3.0 Description

Manages the in-game Heads-Up Display (HUD). It is responsible for displaying real-time information for all players, such as cash, properties, and current turn indicator.

##### 2.1.1.3.4.0 Type

🔹 Controller

##### 2.1.1.3.5.0 Layer

Presentation Layer

##### 2.1.1.3.6.0 Dependencies

- game-session-service-101

##### 2.1.1.3.7.0 Properties

*No data available*

##### 2.1.1.3.8.0 Interfaces

- IHUDController

##### 2.1.1.3.9.0 Technology

Unity Engine, C#

##### 2.1.1.3.10.0 Resources

*No data available*

##### 2.1.1.3.11.0 Health Check

*Not specified*

##### 2.1.1.3.12.0 Responsible Features

- REQ-1-071

##### 2.1.1.3.13.0 Security

###### 2.1.1.3.13.1 Requires Authentication

❌ No

###### 2.1.1.3.13.2 Requires Authorization

❌ No

#### 2.1.1.4.0.0 View

##### 2.1.1.4.1.0 Id

modal-dialog-presenter-004

##### 2.1.1.4.2.0 Name

ModalDialogPresenter

##### 2.1.1.4.3.0 Description

A generic UI component responsible for creating and displaying modal dialogs that halt gameplay to await critical user input. Used for purchase decisions, trade offers, tax choices, and error messages.

##### 2.1.1.4.4.0 Type

🔹 View

##### 2.1.1.4.5.0 Layer

Presentation Layer

##### 2.1.1.4.6.0 Dependencies

- input-handler-006

##### 2.1.1.4.7.0 Properties

| Property | Value |
|----------|-------|
| Is Singleton | ✅ |

##### 2.1.1.4.8.0 Interfaces

- IModalPresenter

##### 2.1.1.4.9.0 Technology

Unity Engine, C#

##### 2.1.1.4.10.0 Resources

*No data available*

##### 2.1.1.4.11.0 Health Check

*Not specified*

##### 2.1.1.4.12.0 Responsible Features

- REQ-1-023
- REQ-1-040
- REQ-1-051
- REQ-1-073

##### 2.1.1.4.13.0 Security

###### 2.1.1.4.13.1 Requires Authentication

❌ No

###### 2.1.1.4.13.2 Requires Authorization

❌ No

#### 2.1.1.5.0.0 Controller

##### 2.1.1.5.1.0 Id

trade-ui-controller-005

##### 2.1.1.5.2.0 Name

TradeUIController

##### 2.1.1.5.3.0 Description

Manages the dedicated two-panel trading interface, allowing the human player to select assets (properties, cash, cards) from their and their opponent's inventory to construct and propose a trade.

##### 2.1.1.5.4.0 Type

🔹 Controller

##### 2.1.1.5.5.0 Layer

Presentation Layer

##### 2.1.1.5.6.0 Dependencies

- trade-service-103

##### 2.1.1.5.7.0 Properties

*No data available*

##### 2.1.1.5.8.0 Interfaces

- ITradeUI

##### 2.1.1.5.9.0 Technology

Unity Engine, C#

##### 2.1.1.5.10.0 Resources

*No data available*

##### 2.1.1.5.11.0 Health Check

*Not specified*

##### 2.1.1.5.12.0 Responsible Features

- REQ-1-059
- REQ-1-076

##### 2.1.1.5.13.0 Security

###### 2.1.1.5.13.1 Requires Authentication

❌ No

###### 2.1.1.5.13.2 Requires Authorization

❌ No

#### 2.1.1.6.0.0 Controller

##### 2.1.1.6.1.0 Id

input-handler-006

##### 2.1.1.6.2.0 Name

InputHandler

##### 2.1.1.6.3.0 Description

A centralized component that captures raw user input (mouse clicks, keyboard presses) and translates them into semantic game actions, which are then dispatched to the appropriate services or UI controllers.

##### 2.1.1.6.4.0 Type

🔹 Controller

##### 2.1.1.6.5.0 Layer

Presentation Layer

##### 2.1.1.6.6.0 Dependencies

- turn-manager-service-102
- ui-manager-001

##### 2.1.1.6.7.0 Properties

| Property | Value |
|----------|-------|
| Input System | Unity Input System |

##### 2.1.1.6.8.0 Interfaces

*No items available*

##### 2.1.1.6.9.0 Technology

Unity Engine, C#

##### 2.1.1.6.10.0 Resources

*No data available*

##### 2.1.1.6.11.0 Health Check

*Not specified*

##### 2.1.1.6.12.0 Responsible Features

- REQ-1-039
- REQ-1-040

##### 2.1.1.6.13.0 Security

###### 2.1.1.6.13.1 Requires Authentication

❌ No

###### 2.1.1.6.13.2 Requires Authorization

❌ No

#### 2.1.1.7.0.0 Service

##### 2.1.1.7.1.0 Id

audio-manager-009

##### 2.1.1.7.2.0 Name

AudioManager

##### 2.1.1.7.3.0 Description

Manages all audio playback, including background music tracks and sound effects. Provides volume control functionality and handles context-aware music changes.

##### 2.1.1.7.4.0 Type

🔹 Service

##### 2.1.1.7.5.0 Layer

Presentation Layer

##### 2.1.1.7.6.0 Dependencies

*No items available*

##### 2.1.1.7.7.0 Properties

| Property | Value |
|----------|-------|
| Mixer Groups | ['Master', 'Music', 'SFX'] |

##### 2.1.1.7.8.0 Interfaces

- IAudioManager

##### 2.1.1.7.9.0 Technology

Unity Engine, C#

##### 2.1.1.7.10.0 Resources

*No data available*

##### 2.1.1.7.11.0 Health Check

*Not specified*

##### 2.1.1.7.12.0 Responsible Features

- REQ-1-079
- REQ-1-094

##### 2.1.1.7.13.0 Security

###### 2.1.1.7.13.1 Requires Authentication

❌ No

###### 2.1.1.7.13.2 Requires Authorization

❌ No

#### 2.1.1.8.0.0 Service

##### 2.1.1.8.1.0 Id

vfx-manager-008

##### 2.1.1.8.2.0 Name

VFXManager

##### 2.1.1.8.3.0 Description

Controls the instantiation and playback of all visual effects in the game, such as particle effects for transactions, property highlights, and dice roll animations.

##### 2.1.1.8.4.0 Type

🔹 Service

##### 2.1.1.8.5.0 Layer

Presentation Layer

##### 2.1.1.8.6.0 Dependencies

*No items available*

##### 2.1.1.8.7.0 Properties

| Property | Value |
|----------|-------|
| Particle System | Unity Particle System / VFX Graph |

##### 2.1.1.8.8.0 Interfaces

- IVFXManager

##### 2.1.1.8.9.0 Technology

Unity Engine, C#

##### 2.1.1.8.10.0 Resources

*No data available*

##### 2.1.1.8.11.0 Health Check

*Not specified*

##### 2.1.1.8.12.0 Responsible Features

- REQ-1-017
- REQ-1-005

##### 2.1.1.8.13.0 Security

###### 2.1.1.8.13.1 Requires Authentication

❌ No

###### 2.1.1.8.13.2 Requires Authorization

❌ No

#### 2.1.1.9.0.0 Service

##### 2.1.1.9.1.0 Id

game-session-service-101

##### 2.1.1.9.2.0 Name

GameSessionService

##### 2.1.1.9.3.0 Description

Orchestrates the overall game lifecycle. It is responsible for initializing new games, coordinating the loading of saved games, and managing the end-of-game sequence.

##### 2.1.1.9.4.0 Type

🔹 Service

##### 2.1.1.9.5.0 Layer

Application Services Layer

##### 2.1.1.9.6.0 Dependencies

- turn-manager-service-102
- game-state-manager-201
- game-save-repository-301
- statistics-repository-303

##### 2.1.1.9.7.0 Properties

*No data available*

##### 2.1.1.9.8.0 Interfaces

- IGameSessionService

##### 2.1.1.9.9.0 Technology

.NET 8, C#

##### 2.1.1.9.10.0 Resources

*No data available*

##### 2.1.1.9.11.0 Health Check

*Not specified*

##### 2.1.1.9.12.0 Responsible Features

- REQ-1-015
- REQ-1-068
- REQ-1-069
- REQ-1-085

##### 2.1.1.9.13.0 Security

###### 2.1.1.9.13.1 Requires Authentication

❌ No

###### 2.1.1.9.13.2 Requires Authorization

❌ No

#### 2.1.1.10.0.0 Service

##### 2.1.1.10.1.0 Id

turn-manager-service-102

##### 2.1.1.10.2.0 Name

TurnManagerService

##### 2.1.1.10.3.0 Description

Controls the flow of the game by managing the sequence of player turns and the distinct phases within each turn (Pre-Roll, Roll, Action, Post-Roll). It coordinates between player input, AI actions, and the Rule Engine.

##### 2.1.1.10.4.0 Type

🔹 Service

##### 2.1.1.10.5.0 Layer

Application Services Layer

##### 2.1.1.10.6.0 Dependencies

- game-state-manager-201
- rule-engine-202
- ai-orchestrator-service-105

##### 2.1.1.10.7.0 Properties

*No data available*

##### 2.1.1.10.8.0 Interfaces

- ITurnManager

##### 2.1.1.10.9.0 Technology

.NET 8, C#

##### 2.1.1.10.10.0 Resources

*No data available*

##### 2.1.1.10.11.0 Health Check

*Not specified*

##### 2.1.1.10.12.0 Responsible Features

- REQ-1-037
- REQ-1-038

##### 2.1.1.10.13.0 Security

###### 2.1.1.10.13.1 Requires Authentication

❌ No

###### 2.1.1.10.13.2 Requires Authorization

❌ No

#### 2.1.1.11.0.0 Service

##### 2.1.1.11.1.0 Id

trade-service-103

##### 2.1.1.11.2.0 Name

TradeService

##### 2.1.1.11.3.0 Description

Mediates all trading activities between players. It processes trade proposals, forwards them for evaluation (to the human player or AI), and executes the asset exchange upon acceptance by calling the Rule Engine.

##### 2.1.1.11.4.0 Type

🔹 Service

##### 2.1.1.11.5.0 Layer

Application Services Layer

##### 2.1.1.11.6.0 Dependencies

- rule-engine-202
- game-state-manager-201
- logging-service-304

##### 2.1.1.11.7.0 Properties

*No data available*

##### 2.1.1.11.8.0 Interfaces

- ITradeService

##### 2.1.1.11.9.0 Technology

.NET 8, C#

##### 2.1.1.11.10.0 Resources

*No data available*

##### 2.1.1.11.11.0 Health Check

*Not specified*

##### 2.1.1.11.12.0 Responsible Features

- REQ-1-059
- REQ-1-060

##### 2.1.1.11.13.0 Security

###### 2.1.1.11.13.1 Requires Authentication

❌ No

###### 2.1.1.11.13.2 Requires Authorization

❌ No

#### 2.1.1.12.0.0 Service

##### 2.1.1.12.1.0 Id

player-action-service-104

##### 2.1.1.12.2.0 Name

PlayerActionService

##### 2.1.1.12.3.0 Description

Acts as a facade for all discrete player actions, such as buying properties, building houses, or mortgaging. It receives requests from the UI or AI, validates them with the Rule Engine, and applies changes to the Game State.

##### 2.1.1.12.4.0 Type

🔹 Service

##### 2.1.1.12.5.0 Layer

Application Services Layer

##### 2.1.1.12.6.0 Dependencies

- rule-engine-202
- game-state-manager-201

##### 2.1.1.12.7.0 Properties

*No data available*

##### 2.1.1.12.8.0 Interfaces

- IPlayerActionService

##### 2.1.1.12.9.0 Technology

.NET 8, C#

##### 2.1.1.12.10.0 Resources

*No data available*

##### 2.1.1.12.11.0 Health Check

*Not specified*

##### 2.1.1.12.12.0 Responsible Features

- REQ-1-051
- REQ-1-053
- REQ-1-057

##### 2.1.1.12.13.0 Security

###### 2.1.1.12.13.1 Requires Authentication

❌ No

###### 2.1.1.12.13.2 Requires Authorization

❌ No

#### 2.1.1.13.0.0 Service

##### 2.1.1.13.1.0 Id

ai-orchestrator-service-105

##### 2.1.1.13.2.0 Name

AIOrchestratorService

##### 2.1.1.13.3.0 Description

Responsible for triggering and managing an AI player's turn. It invokes the AIBehaviorTreeExecutor to get a decision and then uses other application services (like PlayerActionService and TradeService) to execute the chosen actions.

##### 2.1.1.13.4.0 Type

🔹 Service

##### 2.1.1.13.5.0 Layer

Application Services Layer

##### 2.1.1.13.6.0 Dependencies

- ai-behavior-tree-executor-203
- player-action-service-104
- trade-service-103
- logging-service-304

##### 2.1.1.13.7.0 Properties

*No data available*

##### 2.1.1.13.8.0 Interfaces

- IAIOrchestrator

##### 2.1.1.13.9.0 Technology

.NET 8, C#

##### 2.1.1.13.10.0 Resources

*No data available*

##### 2.1.1.13.11.0 Health Check

*Not specified*

##### 2.1.1.13.12.0 Responsible Features

- REQ-1-004
- REQ-1-064

##### 2.1.1.13.13.0 Security

###### 2.1.1.13.13.1 Requires Authentication

❌ No

###### 2.1.1.13.13.2 Requires Authorization

❌ No

#### 2.1.1.14.0.0 Manager

##### 2.1.1.14.1.0 Id

game-state-manager-201

##### 2.1.1.14.2.0 Name

GameStateManager

##### 2.1.1.14.3.0 Description

The heart of the Business Logic Layer. It encapsulates the single, authoritative `GameState` object, which contains the complete state of the game, including all players, properties, card decks, and game metadata. It provides controlled access and modification methods.

##### 2.1.1.14.4.0 Type

🔹 Manager

##### 2.1.1.14.5.0 Layer

Business Logic (Domain) Layer

##### 2.1.1.14.6.0 Dependencies

*No items available*

##### 2.1.1.14.7.0 Properties

| Property | Value |
|----------|-------|
| State Object Class | GameState |

##### 2.1.1.14.8.0 Interfaces

- IGameStateProvider

##### 2.1.1.14.9.0 Technology

.NET 8, C#

##### 2.1.1.14.10.0 Resources

*No data available*

##### 2.1.1.14.11.0 Health Check

*Not specified*

##### 2.1.1.14.12.0 Responsible Features

- REQ-1-041
- REQ-1-031

##### 2.1.1.14.13.0 Security

###### 2.1.1.14.13.1 Requires Authentication

❌ No

###### 2.1.1.14.13.2 Requires Authorization

❌ No

#### 2.1.1.15.0.0 Engine

##### 2.1.1.15.1.0 Id

rule-engine-202

##### 2.1.1.15.2.0 Name

RuleEngine

##### 2.1.1.15.3.0 Description

A stateless component that contains the pure implementation of all official Monopoly rules. It takes the current game state and a proposed action, then validates the action's legality and returns the resulting state changes.

##### 2.1.1.15.4.0 Type

🔹 Engine

##### 2.1.1.15.5.0 Layer

Business Logic (Domain) Layer

##### 2.1.1.15.6.0 Dependencies

*No items available*

##### 2.1.1.15.7.0 Properties

| Property | Value |
|----------|-------|
| Rule Baseline | Hasbro 2024 |

##### 2.1.1.15.8.0 Interfaces

- IRuleEngine

##### 2.1.1.15.9.0 Technology

.NET 8, C#

##### 2.1.1.15.10.0 Resources

*No data available*

##### 2.1.1.15.11.0 Health Check

*Not specified*

##### 2.1.1.15.12.0 Responsible Features

- REQ-1-003
- REQ-1-043
- REQ-1-045
- REQ-1-048
- REQ-1-054
- REQ-1-065

##### 2.1.1.15.13.0 Security

###### 2.1.1.15.13.1 Requires Authentication

❌ No

###### 2.1.1.15.13.2 Requires Authorization

❌ No

#### 2.1.1.16.0.0 Engine

##### 2.1.1.16.1.0 Id

ai-behavior-tree-executor-203

##### 2.1.1.16.2.0 Name

AIBehaviorTreeExecutor

##### 2.1.1.16.3.0 Description

Executes a Behavior Tree to determine an AI player's next action. It loads configurable parameters based on the AI's difficulty level to influence its decision-making process for trades, purchases, and building.

##### 2.1.1.16.4.0 Type

🔹 Engine

##### 2.1.1.16.5.0 Layer

Business Logic (Domain) Layer

##### 2.1.1.16.6.0 Dependencies

- configuration-provider-305

##### 2.1.1.16.7.0 Properties

| Property | Value |
|----------|-------|
| Architecture | Behavior Tree |

##### 2.1.1.16.8.0 Interfaces

- IAIDecisionMaker

##### 2.1.1.16.9.0 Technology

.NET 8, C#

##### 2.1.1.16.10.0 Resources

*No data available*

##### 2.1.1.16.11.0 Health Check

*Not specified*

##### 2.1.1.16.12.0 Responsible Features

- REQ-1-059
- REQ-1-062
- REQ-1-063
- REQ-1-064

##### 2.1.1.16.13.0 Security

###### 2.1.1.16.13.1 Requires Authentication

❌ No

###### 2.1.1.16.13.2 Requires Authorization

❌ No

#### 2.1.1.17.0.0 Utility

##### 2.1.1.17.1.0 Id

dice-roller-204

##### 2.1.1.17.2.0 Name

DiceRoller

##### 2.1.1.17.3.0 Description

Provides a secure and unpredictable dice rolling mechanism. It uses a cryptographically secure random number generator as required to ensure fairness in all dice rolls.

##### 2.1.1.17.4.0 Type

🔹 Utility

##### 2.1.1.17.5.0 Layer

Business Logic (Domain) Layer

##### 2.1.1.17.6.0 Dependencies

*No items available*

##### 2.1.1.17.7.0 Properties

| Property | Value |
|----------|-------|
| Algorithm | System.Security.Cryptography.RandomNumberGenerator |

##### 2.1.1.17.8.0 Interfaces

- IDice

##### 2.1.1.17.9.0 Technology

.NET 8, C#

##### 2.1.1.17.10.0 Resources

*No data available*

##### 2.1.1.17.11.0 Health Check

*Not specified*

##### 2.1.1.17.12.0 Responsible Features

- REQ-1-042

##### 2.1.1.17.13.0 Security

###### 2.1.1.17.13.1 Requires Authentication

❌ No

###### 2.1.1.17.13.2 Requires Authorization

❌ No

#### 2.1.1.18.0.0 Repository

##### 2.1.1.18.1.0 Id

game-save-repository-301

##### 2.1.1.18.2.0 Name

GameSaveRepository

##### 2.1.1.18.3.0 Description

Implements the Repository Pattern for game save data. It is responsible for serializing the `GameState` object to a versioned JSON file and deserializing it back, handling all file system interactions.

##### 2.1.1.18.4.0 Type

🔹 Repository

##### 2.1.1.18.5.0 Layer

Infrastructure Layer

##### 2.1.1.18.6.0 Dependencies

- file-system-service-307
- logging-service-304

##### 2.1.1.18.7.0 Properties

| Property | Value |
|----------|-------|
| Format | JSON |
| Serialization Library | System.Text.Json |

##### 2.1.1.18.8.0 Interfaces

- IGameSaveRepository

##### 2.1.1.18.9.0 Technology

.NET 8, C#

##### 2.1.1.18.10.0 Resources

###### 2.1.1.18.10.1 Storage

2 GB (per REQ-1-013)

##### 2.1.1.18.11.0 Health Check

*Not specified*

##### 2.1.1.18.12.0 Responsible Features

- REQ-1-085
- REQ-1-086
- REQ-1-087

##### 2.1.1.18.13.0 Security

###### 2.1.1.18.13.1 Requires Authentication

❌ No

###### 2.1.1.18.13.2 Requires Authorization

❌ No

#### 2.1.1.19.0.0 Repository

##### 2.1.1.19.1.0 Id

profile-repository-302

##### 2.1.1.19.2.0 Name

ProfileRepository

##### 2.1.1.19.3.0 Description

Manages persistence for the `PlayerProfile` entity using an SQLite database. Handles creation, retrieval, and validation of player profiles.

##### 2.1.1.19.4.0 Type

🔹 Repository

##### 2.1.1.19.5.0 Layer

Infrastructure Layer

##### 2.1.1.19.6.0 Dependencies

- logging-service-304

##### 2.1.1.19.7.0 Properties

| Property | Value |
|----------|-------|
| Database | SQLite |
| Library | Microsoft.Data.Sqlite |

##### 2.1.1.19.8.0 Interfaces

- IProfileRepository

##### 2.1.1.19.9.0 Technology

.NET 8, C#

##### 2.1.1.19.10.0 Resources

###### 2.1.1.19.10.1 Storage

Part of shared app data

##### 2.1.1.19.11.0 Health Check

*Not specified*

##### 2.1.1.19.12.0 Responsible Features

- REQ-1-032
- REQ-1-089

##### 2.1.1.19.13.0 Security

###### 2.1.1.19.13.1 Requires Authentication

❌ No

###### 2.1.1.19.13.2 Requires Authorization

❌ No

#### 2.1.1.20.0.0 Repository

##### 2.1.1.20.1.0 Id

statistics-repository-303

##### 2.1.1.20.2.0 Name

StatisticsRepository

##### 2.1.1.20.3.0 Description

Manages persistence for player statistics and game results (`PlayerStatistic`, `GameResult` entities) using an SQLite database. Responsible for tracking historical data and top scores.

##### 2.1.1.20.4.0 Type

🔹 Repository

##### 2.1.1.20.5.0 Layer

Infrastructure Layer

##### 2.1.1.20.6.0 Dependencies

- logging-service-304

##### 2.1.1.20.7.0 Properties

| Property | Value |
|----------|-------|
| Database | SQLite |
| Library | Microsoft.Data.Sqlite |

##### 2.1.1.20.8.0 Interfaces

- IStatisticsRepository

##### 2.1.1.20.9.0 Technology

.NET 8, C#

##### 2.1.1.20.10.0 Resources

###### 2.1.1.20.10.1 Storage

Part of shared app data

##### 2.1.1.20.11.0 Health Check

*Not specified*

##### 2.1.1.20.12.0 Responsible Features

- REQ-1-033
- REQ-1-034
- REQ-1-089
- REQ-1-091

##### 2.1.1.20.13.0 Security

###### 2.1.1.20.13.1 Requires Authentication

❌ No

###### 2.1.1.20.13.2 Requires Authorization

❌ No

#### 2.1.1.21.0.0 Service

##### 2.1.1.21.1.0 Id

logging-service-304

##### 2.1.1.21.2.0 Name

LoggingService

##### 2.1.1.21.3.0 Description

A wrapper around the Serilog framework. It configures and provides a standardized logging interface for the entire application, handling structured JSON output, log levels, and rolling file policies.

##### 2.1.1.21.4.0 Type

🔹 Service

##### 2.1.1.21.5.0 Layer

Infrastructure Layer

##### 2.1.1.21.6.0 Dependencies

*No items available*

##### 2.1.1.21.7.0 Properties

| Property | Value |
|----------|-------|
| Framework | Serilog |
| Format | JSON |
| Retention Days | 7 |
| Max Size Mb | 50 |

##### 2.1.1.21.8.0 Interfaces

- ILogger

##### 2.1.1.21.9.0 Technology

.NET 8, C#, Serilog

##### 2.1.1.21.10.0 Resources

###### 2.1.1.21.10.1 Storage

50 MB (per REQ-1-021)

##### 2.1.1.21.11.0 Health Check

*Not specified*

##### 2.1.1.21.12.0 Responsible Features

- REQ-1-018
- REQ-1-019
- REQ-1-020
- REQ-1-021
- REQ-1-022
- REQ-1-028

##### 2.1.1.21.13.0 Security

###### 2.1.1.21.13.1 Requires Authentication

❌ No

###### 2.1.1.21.13.2 Requires Authorization

❌ No

#### 2.1.1.22.0.0 Provider

##### 2.1.1.22.1.0 Id

configuration-provider-305

##### 2.1.1.22.2.0 Name

ConfigurationProvider

##### 2.1.1.22.3.0 Description

Loads and provides access to external configuration files. Its primary responsibility is to load the AI behavior parameters from a JSON file, allowing AI tuning without recompiling.

##### 2.1.1.22.4.0 Type

🔹 Provider

##### 2.1.1.22.5.0 Layer

Infrastructure Layer

##### 2.1.1.22.6.0 Dependencies

- file-system-service-307

##### 2.1.1.22.7.0 Properties

*No data available*

##### 2.1.1.22.8.0 Interfaces

- IConfigurationProvider

##### 2.1.1.22.9.0 Technology

.NET 8, C#

##### 2.1.1.22.10.0 Resources

*No data available*

##### 2.1.1.22.11.0 Health Check

*Not specified*

##### 2.1.1.22.12.0 Responsible Features

- REQ-1-063

##### 2.1.1.22.13.0 Security

###### 2.1.1.22.13.1 Requires Authentication

❌ No

###### 2.1.1.22.13.2 Requires Authorization

❌ No

#### 2.1.1.23.0.0 Provider

##### 2.1.1.23.1.0 Id

localization-provider-306

##### 2.1.1.23.2.0 Name

LocalizationProvider

##### 2.1.1.23.3.0 Description

Loads user-facing strings from external resource files (e.g., JSON). The application uses this service to retrieve all text via a key-based lookup, ensuring no hardcoded strings.

##### 2.1.1.23.4.0 Type

🔹 Provider

##### 2.1.1.23.5.0 Layer

Infrastructure Layer

##### 2.1.1.23.6.0 Dependencies

- file-system-service-307

##### 2.1.1.23.7.0 Properties

| Property | Value |
|----------|-------|
| Default Language | en-US |

##### 2.1.1.23.8.0 Interfaces

- ILocalizationProvider

##### 2.1.1.23.9.0 Technology

.NET 8, C#

##### 2.1.1.23.10.0 Resources

*No data available*

##### 2.1.1.23.11.0 Health Check

*Not specified*

##### 2.1.1.23.12.0 Responsible Features

- REQ-1-084

##### 2.1.1.23.13.0 Security

###### 2.1.1.23.13.1 Requires Authentication

❌ No

###### 2.1.1.23.13.2 Requires Authorization

❌ No

#### 2.1.1.24.0.0 Service

##### 2.1.1.24.1.0 Id

file-system-service-307

##### 2.1.1.24.2.0 Name

FileSystemService

##### 2.1.1.24.3.0 Description

An abstraction layer over direct file system operations. Provides testable methods for reading/writing files, checking existence, creating directories, and calculating file hashes/checksums.

##### 2.1.1.24.4.0 Type

🔹 Service

##### 2.1.1.24.5.0 Layer

Infrastructure Layer

##### 2.1.1.24.6.0 Dependencies

- logging-service-304

##### 2.1.1.24.7.0 Properties

*No data available*

##### 2.1.1.24.8.0 Interfaces

- IFileSystem

##### 2.1.1.24.9.0 Technology

.NET 8, C#

##### 2.1.1.24.10.0 Resources

*No data available*

##### 2.1.1.24.11.0 Health Check

*Not specified*

##### 2.1.1.24.12.0 Responsible Features

- REQ-1-020
- REQ-1-087
- REQ-1-088

##### 2.1.1.24.13.0 Security

###### 2.1.1.24.13.1 Requires Authentication

❌ No

###### 2.1.1.24.13.2 Requires Authorization

❌ No

#### 2.1.1.25.0.0 Service

##### 2.1.1.25.1.0 Id

data-migration-service-308

##### 2.1.1.25.2.0 Name

DataMigrationService

##### 2.1.1.25.3.0 Description

Manages the versioning and automatic migration of data files. It checks save files and the statistics database upon loading and attempts to upgrade them to the current format if they are from an older version.

##### 2.1.1.25.4.0 Type

🔹 Service

##### 2.1.1.25.5.0 Layer

Infrastructure Layer

##### 2.1.1.25.6.0 Dependencies

- file-system-service-307
- logging-service-304

##### 2.1.1.25.7.0 Properties

| Property | Value |
|----------|-------|
| Migration Strategy | Atomic |

##### 2.1.1.25.8.0 Interfaces

- IDataMigrationService

##### 2.1.1.25.9.0 Technology

.NET 8, C#

##### 2.1.1.25.10.0 Resources

*No data available*

##### 2.1.1.25.11.0 Health Check

*Not specified*

##### 2.1.1.25.12.0 Responsible Features

- REQ-1-090

##### 2.1.1.25.13.0 Security

###### 2.1.1.25.13.1 Requires Authentication

❌ No

###### 2.1.1.25.13.2 Requires Authorization

❌ No

### 2.1.2.0.0.0 Configuration

| Property | Value |
|----------|-------|
| Environment | Production |
| Logging Level | INFO |
| Database Path | %APPDATA%/MonopolyTycoon/stats.db |
| Saves Path | %APPDATA%/MonopolyTycoon/saves/ |
| Logs Path | %APPDATA%/MonopolyTycoon/logs/ |
| Ai Config Path | Assets/Configuration/ai_params.json |

