# 1 Style

Monolithic

# 2 Patterns

## 2.1 Layered Architecture

### 2.1.1 Name

Layered Architecture

### 2.1.2 Description

Organizes the application into horizontal layers, each with a specific responsibility. This creates a clear separation of concerns, where UI, application logic, business rules, and data access are isolated from each other.

### 2.1.3 Benefits

- Separation of Concerns
- Improved Maintainability and Testability
- Reduced Coupling between layers

### 2.1.4 Tradeoffs

- Can add complexity for very simple applications.
- Potential for performance overhead if layers are not designed carefully.

### 2.1.5 Applicability

#### 2.1.5.1 Scenarios

- Desktop applications with distinct UI, logic, and data components.
- Systems where core business logic needs to be independent of the presentation technology.
- Projects requiring unit and integration testing for different parts of the system.

#### 2.1.5.2 Constraints

- The application is self-contained and does not require distributed deployment.

## 2.2.0.0 Model-View-Controller (MVC) / Model-View-Presenter (MVP)

### 2.2.1.0 Name

Model-View-Controller (MVC) / Model-View-Presenter (MVP)

### 2.2.2.0 Description

A pattern used to separate the application's data model and business rules (Model) from the user interface (View) and user input handling (Controller/Presenter). In Unity, C# scripts (MonoBehaviours) often act as Controllers/Presenters, manipulating GameObjects (Views) based on the state of plain C# objects (Models).

### 2.2.3.0 Benefits

- Decouples UI from game logic, allowing each to be modified independently.
- Facilitates easier UI development and testing.
- Improves code organization within the presentation layer.

### 2.2.4.0 Applicability

#### 2.2.4.1 Scenarios

- Applications with a graphical user interface, such as games.
- Systems where the same underlying data needs to be presented in multiple ways.

## 2.3.0.0 Repository Pattern

### 2.3.1.0 Name

Repository Pattern

### 2.3.2.0 Description

Mediates between the domain and data mapping layers using a collection-like interface for accessing domain objects. It abstracts the data source, so the application logic doesn't need to know if data is coming from a file, a database, or another source.

### 2.3.3.0 Benefits

- Decouples business logic from data access implementation.
- Centralizes data access logic, making it easier to manage.
- Enhances testability by allowing mock repositories to be used in unit tests.

### 2.3.4.0 Applicability

#### 2.3.4.1 Scenarios

- Applications that need to persist data to different storage mechanisms (e.g., JSON files for game saves, SQLite for statistics).
- Systems where data access logic is complex and needs to be tested independently.

## 2.4.0.0 Behavior Tree

### 2.4.1.0 Name

Behavior Tree

### 2.4.2.0 Description

A mathematical model of plan execution used in AI applications. It allows for the creation of complex behaviors from simple, reusable tasks, organized in a tree-like structure. This is explicitly required by REQ-1-063 for AI implementation.

### 2.4.3.0 Benefits

- Modular and reusable AI logic.
- Easy to understand and debug AI behavior visually.
- Scalable for creating complex and varied AI opponents.

### 2.4.4.0 Applicability

#### 2.4.4.1 Scenarios

- Implementing AI for game characters with complex decision-making processes.
- Defining AI strategies that can be tuned via external parameters for different difficulty levels.

# 3.0.0.0 Layers

## 3.1.0.0 Presentation

### 3.1.1.0 Id

presentation_layer

### 3.1.2.0 Name

Presentation Layer

### 3.1.3.0 Description

Responsible for all user-facing elements and interactions. This layer is implemented within the Unity game engine and handles rendering, animation, user input, and audio.

### 3.1.4.0 Technologystack

Unity Engine (Latest LTS), C#

### 3.1.5.0 Language

C#

### 3.1.6.0 Type

🔹 Presentation

### 3.1.7.0 Responsibilities

- Render the 3D game board, tokens, and visual effects (REQ-1-005).
- Display all User Interface elements, including HUD, modal dialogs, menus, and notifications (REQ-1-071, REQ-1-073).
- Handle all direct user input (e.g., button clicks, selections).
- Manage the isometric camera and ensure proper scaling for different aspect ratios (REQ-1-016).
- Play all background music and sound effects (REQ-1-094).
- Execute animations for dice rolls, token movement, and transactions (REQ-1-017).

### 3.1.8.0 Components

- ViewManager (handles scenes and UI panels)
- GameBoardPresenter (updates visual state of the board)
- InputController (captures and delegates user actions)
- HUDController
- TradeUIController
- PropertyManagementUIController
- AudioEngine
- VFXManager

### 3.1.9.0 Dependencies

- {'layerId': 'app_services_layer', 'type': 'Required'}

## 3.2.0.0 ApplicationServices

### 3.2.1.0 Id

app_services_layer

### 3.2.2.0 Name

Application Services Layer

### 3.2.3.0 Description

Orchestrates the application's use cases and coordinates the domain layer's objects to perform tasks. This layer acts as an intermediary between the Presentation and Business Logic layers, translating UI actions into domain operations.

### 3.2.4.0 Technologystack

.NET 8, C#

### 3.2.5.0 Language

C#

### 3.2.6.0 Type

🔹 ApplicationServices

### 3.2.7.0 Responsibilities

- Manage the overall game lifecycle (e.g., starting a new game, loading a saved game).
- Coordinate the sequence of a player's turn through its distinct phases (REQ-1-038).
- Facilitate interactions between players, such as trades and auctions.
- Process user requests from the Presentation layer and invoke the appropriate business logic.
- Manage game setup, including player configuration and AI difficulty selection (REQ-1-030).
- Handle game state saving and loading operations by coordinating the domain and infrastructure layers.

### 3.2.8.0 Components

- GameSessionService
- TurnManagementService
- TradeOrchestrationService
- PropertyActionService (Buy, Build, Mortgage)
- AIService (delegates turn execution to AI logic)

### 3.2.9.0 Dependencies

#### 3.2.9.1 Required

##### 3.2.9.1.1 Layer Id

business_logic_layer

##### 3.2.9.1.2 Type

🔹 Required

#### 3.2.9.2.0 Required

##### 3.2.9.2.1 Layer Id

infrastructure_layer

##### 3.2.9.2.2 Type

🔹 Required

## 3.3.0.0.0 BusinessLogic

### 3.3.1.0.0 Id

business_logic_layer

### 3.3.2.0.0 Name

Business Logic (Domain) Layer

### 3.3.3.0.0 Description

Contains the core logic and rules of the Monopoly game. This layer is completely independent of any UI or data storage technology, making it highly portable and testable. It encapsulates the game state, entities, and all rule enforcement.

### 3.3.4.0.0 Technologystack

.NET 8, C#

### 3.3.5.0.0 Language

C#

### 3.3.6.0.0 Type

🔹 BusinessLogic

### 3.3.7.0.0 Responsibilities

- Strictly implement and enforce the official rules of Monopoly (REQ-1-003).
- Define and manage the core game state, including player states, property ownership, and card decks (REQ-1-041, REQ-1-031).
- Encapsulate the logic for all game actions (e.g., property purchase, rent payment, jail rules).
- Implement the AI decision-making logic using Behavior Trees (REQ-1-063).
- Perform all calculations, such as rent, taxes, and net worth (REQ-1-048).
- Manage the state of domain entities like Player, Property, Bank, and Dice.

### 3.3.8.0.0 Components

- RuleEngine
- GameState
- PlayerState
- Property
- CardDeck
- Bank
- AIBehaviorTreeExecutor
- DiceRoller (using cryptographically secure RNG as per REQ-1-042)

### 3.3.9.0.0 Dependencies

*No items available*

## 3.4.0.0.0 Infrastructure

### 3.4.1.0.0 Id

infrastructure_layer

### 3.4.2.0.0 Name

Infrastructure Layer

### 3.4.3.0.0 Description

Provides technical capabilities to support the other layers. This includes data persistence, logging, configuration management, and any other interaction with external systems or the operating system.

### 3.4.4.0.0 Technologystack

.NET 8, C#, Serilog, Microsoft.Data.Sqlite, System.Text.Json

### 3.4.5.0.0 Language

C#

### 3.4.6.0.0 Type

🔹 Infrastructure

### 3.4.7.0.0 Responsibilities

- Implement repositories for data persistence.
- Serialize the GameState object to JSON for saving and deserialize for loading (REQ-1-087).
- Manage the SQLite database for storing and retrieving player profiles and statistics (REQ-1-089).
- Handle all application logging using the Serilog framework, as per logging requirements (REQ-1-018, REQ-1-019, REQ-1-021).
- Load external configuration files, such as AI behavior parameters (REQ-1-063) and localization strings (REQ-1-084).
- Implement data migration logic for save files and the statistics database (REQ-1-090).
- Manage file system interactions, such as creating directories and validating save files (REQ-1-075, REQ-1-088).

### 3.4.8.0.0 Components

- GameSaveRepository (JSON implementation)
- StatisticsRepository (SQLite implementation)
- PlayerProfileRepository (SQLite implementation)
- LoggingService (Serilog Wrapper)
- JsonConfigurationProvider
- LocalizationService
- DataMigrationManager

### 3.4.9.0.0 Dependencies

*No items available*

# 4.0.0.0.0 Quality Attributes

## 4.1.0.0.0 Performance

### 4.1.1.0.0 Name

Performance

### 4.1.2.0.0 Description

The system must provide a smooth and responsive user experience on recommended hardware.

### 4.1.3.0.0 Requirements

- Sustain an average of 60 FPS and not drop below 45 FPS at 1080p on recommended specs (REQ-1-014).
- Load a game from the main menu in under 10 seconds on recommended specs with an SSD (REQ-1-015).

### 4.1.4.0.0 Tactics

- Efficient 3D asset and rendering pipeline management within Unity.
- Asynchronous loading of game assets and save files.
- Optimized serialization/deserialization logic using System.Text.Json.
- Performant database queries for statistics.

## 4.2.0.0.0 Maintainability

### 4.2.1.0.0 Name

Maintainability

### 4.2.2.0.0 Description

The codebase should be easy to understand, modify, and test.

### 4.2.3.0.0 Requirements

- Adherence to Microsoft C# Coding Conventions (REQ-1-024).
- Unit test coverage of at least 70% for core logic (REQ-1-025).
- Presence of integration tests for key workflows (REQ-1-026).

### 4.2.4.0.0 Tactics

- Strict adherence to the Layered Architecture to enforce separation of concerns.
- Use of Dependency Injection to decouple components.
- Implementation of the Repository pattern to isolate data access logic.
- Comprehensive documentation of system architecture and data schemas (REQ-1-096).

## 4.3.0.0.0 Reliability

### 4.3.1.0.0 Name

Reliability

### 4.3.2.0.0 Description

The application must handle errors gracefully and protect user data.

### 4.3.3.0.0 Requirements

- Display a modal error dialog for unhandled exceptions (REQ-1-023).
- Implement checksum/hash validation for save files to detect corruption (REQ-1-088).
- Automatically create backups of the statistics database (REQ-1-089).

### 4.3.4.0.0 Tactics

- Global exception handling mechanism to catch unhandled exceptions.
- Atomic data migration process to prevent data loss during updates (REQ-1-090).
- Robust logging of errors with unique identifiers to aid in debugging.

## 4.4.0.0.0 Extensibility

### 4.4.1.0.0 Name

Extensibility

### 4.4.2.0.0 Description

The system should be designed to accommodate future changes, such as new languages or game themes.

### 4.4.3.0.0 Requirements

- All user-facing text must be stored in external resource files (REQ-1-084).
- A theme system for dynamic replacement of visual and audio assets (REQ-1-093).
- AI behavior parameters stored in an external JSON file (REQ-1-063).

### 4.4.4.0.0 Tactics

- Implementation of a key-based localization service.
- Development of an asset management system capable of loading theme packages at runtime.
- Configuration-driven design for AI, allowing behavior changes without recompiling the application.

