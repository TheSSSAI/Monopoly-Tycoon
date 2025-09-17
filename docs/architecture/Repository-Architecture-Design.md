# Monopoly Tycoon - Enterprise Architecture Documentation

## Executive Summary

This document outlines the enterprise architecture for **Monopoly Tycoon**, a standalone, monolithic single-player game for the Windows platform. The architecture has evolved from a coarse-grained layered monolith to a fine-grained, highly-decoupled componentized structure. This strategic decomposition was undertaken to enhance maintainability, improve testability, and enable parallel development workflows.

The core architectural decision was to systematically break down the application, domain, and infrastructure layers into ten distinct, single-responsibility repositories (class libraries). This approach strictly adheres to the **Dependency Inversion Principle** by isolating infrastructure implementations behind a dedicated abstractions layer. The result is a robust, scalable, and maintainable codebase where the core business logic (game rules and AI) is completely independent of the presentation technology (Unity) and data persistence mechanisms (JSON files, SQLite).

The business value delivered by this architecture includes significantly reduced development friction, higher code quality through targeted unit testing, and the flexibility to evolve or replace infrastructure components without impacting the core application. The final solution is a testament to modern monolithic design, balancing structural integrity with development agility.

## Solution Architecture Overview

### Technology Stack

-   **Game Engine**: Unity Engine 2022.3.x (LTS)
-   **Core Framework**: .NET 8 (LTS)
-   **Programming Language**: C# 12
-   **Data Persistence**: SQLite 3.x (for statistics), JSON files (for save games)
-   **Data Access**: Microsoft.Data.Sqlite 8.0.x, System.Text.Json 8.0.x
-   **AI Framework**: Panda BT (Behavior Tree Library)
-   **Logging**: Serilog 3.1.x
-   **Testing**: NUnit 4.x
-   **Installer**: Inno Setup 6.2.x
-   **Version Control**: Git

### Architectural Patterns

-   **Layered Architecture**: The system is organized into four distinct horizontal layers: Presentation, Application Services, Business Logic (Domain), and Infrastructure. This enforces a clear separation of concerns.
-   **Repository Pattern**: Mediates between the application and data persistence layers, abstracting the underlying data sources (SQLite, JSON files).
-   **Model-View-Presenter (MVP)**: Used within the Presentation (Unity) layer to separate UI rendering logic from user input and application state management.
-   **Behavior Tree**: Explicitly used for implementing the modular, configurable, and scalable AI logic for opponents, as per REQ-1-063.
-   **Dependency Injection**: The primary pattern for wiring components together. The Presentation layer acts as the **Composition Root**, initializing the DI container and resolving dependencies for the entire application.

## Repository Architecture Strategy

The architecture was deliberately transformed from a simple 4-repository monolith to a 10-repository componentized structure. This decomposition was driven by the need for clear boundaries, improved testability, and adherence to SOLID principles.

-   **Decomposition Rationale**: The primary driver was the **Single Responsibility Principle (SRP)** and the **Dependency Inversion Principle (DIP)**. The original monolithic layers were broken down based on their distinct responsibilities. For example, the `Infrastructure` layer was split into separate components for logging, configuration, JSON persistence, and SQLite persistence. This isolates technology-specific concerns.
-   **Decoupling Strategy**: The introduction of the `Application.Abstractions` repository is the cornerstone of the decoupling strategy. It breaks the compile-time dependency from the application core to the infrastructure, allowing the core logic to depend only on interfaces. This makes the system more flexible and vastly easier to test.
-   **Development Workflow Optimization**: The fine-grained structure allows developers to work on different components in parallel with minimal friction. A UI developer can work in Unity against mocked application services, a domain expert can write and test game rules in a plain .NET project, and an infrastructure specialist can optimize database queries, all without interfering with one another.

## System Architecture Diagrams

### Repository Dependency Architecture

This diagram illustrates the complete dependency graph of the 10 repositories, showing how they are interconnected. The flow of dependencies is strictly downward and towards the central abstraction layers, enforcing a clean, acyclic graph.

mermaid
graph TD
    subgraph Presentation Layer
        REPO_PU_010["MonopolyTycoon.Presentation.Unity<br>(Composition Root)"]
    end

    subgraph Application Services Layer
        REPO_AS_005["MonopolyTycoon.Application.Services"]
        REPO_AA_004["MonopolyTycoon.Application.Abstractions"]
    end

    subgraph Business Logic (Domain) Layer
        REPO_DA_003["MonopolyTycoon.Domain.AI"]
        REPO_DR_002["MonopolyTycoon.Domain.RuleEngine"]
        REPO_DM_001["MonopolyTycoon.Domain.Models"]
    end

    subgraph Infrastructure Layer
        REPO_IP_SG_008["MonopolyTycoon.Infrastructure.Persistence.SaveGames"]
        REPO_IP_ST_009["MonopolyTycoon.Infrastructure.Persistence.Statistics"]
        REPO_IC_007["MonopolyTycoon.Infrastructure.Configuration"]
        REPO_IL_006["MonopolyTycoon.Infrastructure.Logging"]
    end

    %% Dependencies
    REPO_PU_010 --> REPO_AS_005
    REPO_PU_010 --> REPO_AA_004
    REPO_PU_010 --> REPO_DM_001
    REPO_PU_010 --> REPO_IL_006

    REPO_AS_005 --> REPO_AA_004
    REPO_AS_005 --> REPO_DA_003
    REPO_AS_005 --> REPO_DR_002
    REPO_AS_005 --> REPO_DM_001
    REPO_AS_005 --> REPO_IL_006

    REPO_AA_004 --> REPO_DM_001

    REPO_DA_003 --> REPO_DM_001
    REPO_DR_002 --> REPO_DM_001

    REPO_IP_SG_008 --> REPO_AA_004
    REPO_IP_SG_008 --> REPO_DM_001
    REPO_IP_SG_008 --> REPO_IL_006

    REPO_IP_ST_009 --> REPO_AA_004
    REPO_IP_ST_009 --> REPO_DM_001
    REPO_IP_ST_009 --> REPO_IL_006

    REPO_IC_007 --> REPO_AA_004
    REPO_IC_007 --> REPO_IL_006

    REPO_IL_006 --> REPO_AA_004

    %% Styling
    classDef presentation fill:#87CEEB,stroke:#333,stroke-width:2px;
    classDef app fill:#90EE90,stroke:#333,stroke-width:2px;
    classDef domain fill:#FFD700,stroke:#333,stroke-width:2px;
    classDef infra fill:#D3D3D3,stroke:#333,stroke-width:2px;

    class REPO_PU_010 presentation;
    class REPO_AS_005,REPO_AA_004 app;
    class REPO_DA_003,REPO_DR_002,REPO_DM_001 domain;
    class REPO_IP_SG_008,REPO_IP_ST_009,REPO_IC_007,REPO_IL_006 infra;


## Repository Catalog

This catalog provides a detailed specification for each of the final 10 repositories.

### Domain Layer

-   **REPO-DM-001: MonopolyTycoon.Domain.Models**
    -   **Description**: The foundational library containing the canonical POCO data models (`GameState`, `PlayerState`). It has zero dependencies and is referenced by every other project.
    -   **Responsibilities**: Define the core data structures and contracts for the entire application.
    -   **Technology**: .NET 8 Class Library.

-   **REPO-DR-002: MonopolyTycoon.Domain.RuleEngine**
    -   **Description**: A pure, stateless library that implements and enforces all official Monopoly game rules. It is completely isolated from the UI and infrastructure, making it highly testable.
    -   **Responsibilities**: Rule validation, game state transitions, and secure dice rolling.
    -   **Technology**: .NET 8 Class Library.

-   **REPO-DA-003: MonopolyTycoon.Domain.AI**
    -   **Description**: Encapsulates all AI opponent logic. It uses a Behavior Tree architecture to make decisions, which are tuned by external configuration files.
    -   **Responsibilities**: AI decision-making for all in-game actions (purchasing, trading, building).
    -   **Technology**: .NET 8 Class Library, Panda BT.

### Application Layer

-   **REPO-AA-004: MonopolyTycoon.Application.Abstractions**
    -   **Description**: A critical cross-cutting library that defines the interfaces for all infrastructure services (`ILogger`, `ISaveGameRepository`, etc.). This enables the Dependency Inversion Principle.
    -   **Responsibilities**: Define the contracts between the application core and its infrastructure.
    -   **Technology**: .NET 8 Class Library.

-   **REPO-AS-005: MonopolyTycoon.Application.Services**
    -   **Description**: The orchestration layer. It contains services that manage application use cases, connecting UI actions to the domain and infrastructure layers.
    -   **Responsibilities**: Game lifecycle management, turn orchestration, and coordinating player actions.
    -   **Technology**: .NET 8 Class Library.

### Infrastructure Layer

-   **REPO-IL-006: MonopolyTycoon.Infrastructure.Logging**
    -   **Description**: A centralized library providing a concrete implementation of the `ILogger` interface using Serilog. It handles all logging configuration and policies.
    -   **Responsibilities**: Structured JSON logging, file rotation, and PII filtering.
    -   **Technology**: .NET 8 Class Library, Serilog.

-   **REPO-IC-007: MonopolyTycoon.Infrastructure.Configuration**
    -   **Description**: A utility library for loading and parsing external JSON configuration files.
    -   **Responsibilities**: Deserializing configuration for AI, localization, and the rulebook.
    -   **Technology**: .NET 8 Class Library, System.Text.Json.

-   **REPO-IP-SG-008: MonopolyTycoon.Infrastructure.Persistence.SaveGames**
    -   **Description**: Implements the `ISaveGameRepository` interface. It handles serializing the `GameState` to versioned JSON files for the save/load feature.
    -   **Responsibilities**: Game state serialization/deserialization, checksum validation, and data migration for save files.
    -   **Technology**: .NET 8 Class Library, System.Text.Json.

-   **REPO-IP-ST-009: MonopolyTycoon.Infrastructure.Persistence.Statistics**
    -   **Description**: Implements the repository interfaces for player statistics. It manages all interaction with the local SQLite database.
    -   **Responsibilities**: Persisting player profiles, historical stats, and high scores. Manages the database schema and backups.
    -   **Technology**: .NET 8 Class Library, Microsoft.Data.Sqlite.

### Presentation Layer

-   **REPO-PU-010: MonopolyTycoon.Presentation.Unity**
    -   **Description**: The main Unity project. It is responsible for all rendering, UI, user input, and audio. It also acts as the **Composition Root**, initializing the DI container and wiring all the application's components together.
    -   **Responsibilities**: All user-facing presentation and interaction. Application startup and dependency injection setup.
    -   **Technology**: Unity Engine, .NET 8, C#.

## Integration Architecture

-   **Primary Pattern**: Dependency Injection is the core integration pattern. At startup, the `Presentation.Unity` project constructs a DI container, registers all concrete implementations from the Infrastructure layer against the interfaces from the `Application.Abstractions` layer, and registers all services. Components (like Unity MonoBehaviours or Application Services) then receive their dependencies via constructor injection.
-   **Data Flow**: A typical user action follows this flow:
    1.  **Input**: `Presentation.Unity` captures a user click.
    2.  **Command**: The UI controller calls a method on an injected `Application.Service` (e.g., `ITurnManagementService.ExecutePlayerActionAsync`).
    3.  **Orchestration**: The service coordinates the action, first validating it with the `Domain.RuleEngine`.
    4.  **Execution**: If valid, the service applies the action via the `RuleEngine` to get a new `GameState`.
    5.  **Persistence (if needed)**: The service might use an `ISaveGameRepository` to save the new state.
    6.  **Notification**: The service can publish an event (e.g., `GameStateUpdated`) that the UI subscribes to, triggering a visual refresh.
-   **Contracts**: The `Application.Abstractions` library is the definitive source of truth for all contracts between the application core and infrastructure. This ensures that the core is completely shielded from implementation details.

## Technology Implementation Framework

-   **Domain Layer**: Must remain pure .NET 8 with no external dependencies other than its constituent projects. All logic should be stateless where possible (e.g., the `RuleEngine`).
-   **Infrastructure Layer**: All I/O-bound operations (file access, database queries) must be implemented using `async/await` patterns to prevent blocking the main game thread. Implementations must be robust, handling exceptions (e.g., file not found, DB connection error) and logging them appropriately.
-   **Presentation Layer**: Adhere to Unity best practices. Decouple Unity `MonoBehaviour` scripts from application logic by having them delegate to injected services. Use Unity's Addressables system to manage assets, which is crucial for implementing the swappable themes requirement.

## Performance & Scalability Architecture

-   **Performance**: The architecture directly supports performance requirements.
    -   **FPS (REQ-1-014)**: The separation of concerns ensures that only the `Presentation.Unity` layer is responsible for rendering. The use of async I/O in the infrastructure layer prevents database or file operations from causing frame drops.
    -   **Load Time (REQ-1-015)**: The choice of `System.Text.Json` and async file APIs in the `Persistence.SaveGames` repository is geared towards high-performance deserialization to meet the <10 second load time requirement.
-   **Scalability**: As a single-user, offline desktop application, traditional scalability (handling more users or load) is not a requirement. The architecture is instead scaled for **development and maintenance**, allowing a team to work efficiently on a complex monolithic codebase.

## Development & Deployment Strategy

-   **Development Workflow**: The multi-repository structure allows for a 'solution-level' build for integration and 'project-level' focus for development. A developer working on AI can build and test only the `Domain.AI` project, enjoying very fast compile times, while relying on the stable contracts from `Domain.Models`.
-   **Testing Strategy**: Each repository has a corresponding test project.
    -   `Domain.*`: Heavily unit-tested with NUnit.
    -   `Application.*`: Unit-tested with NUnit and mocking frameworks (e.g., Moq).
    -   `Infrastructure.*`: Integration-tested against real file systems or in-memory databases.
    -   `Presentation.Unity`: Tested with the Unity Test Framework (Playmode and Editmode tests).
-   **Deployment**: A CI/CD pipeline will build all class libraries, place the resulting DLLs into the Unity project's `Assets` folder, and then build the final Windows executable. This executable and its data are then packaged into a single installer using an Inno Setup script.

## Architecture Decision Records (ADRs)

-   **ADR-001: Adopt a Fine-Grained Decomposed Monolith**
    -   **Decision**: Decompose the initial 4-layer monolith into 10 single-responsibility repositories.
    -   **Rationale**: To maximize testability, enforce separation of concerns, enable parallel development, and improve long-term maintainability.

-   **ADR-002: Isolate Infrastructure via an Abstractions Layer**
    -   **Decision**: Create a dedicated `Application.Abstractions` repository to hold all infrastructure interfaces.
    -   **Rationale**: Adheres to the Dependency Inversion Principle. Decouples the application core from concrete implementations, allowing infrastructure to be swapped and enabling efficient unit testing with mocks.

-   **ADR-003: Separate AI Logic from Core Rule Engine**
    -   **Decision**: Create a distinct `Domain.AI` repository separate from `Domain.RuleEngine`.
    -   **Rationale**: Isolates the complex, specialized logic of AI. It also contains the third-party Behavior Tree dependency, preventing it from 'leaking' into the core domain logic.