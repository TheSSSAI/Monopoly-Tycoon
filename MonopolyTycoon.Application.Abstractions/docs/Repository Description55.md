# 1 Id

REPO-AA-004

# 2 Name

MonopolyTycoon.Application.Abstractions

# 3 Description

This repository defines the interfaces (contracts) for services that are implemented in the Infrastructure layer. It was extracted from the original `MonopolyTycoon.Application` repository to facilitate the Dependency Inversion Principle. It contains interfaces like `ISaveGameRepository`, `IStatisticsRepository`, and `ILogger`. By placing these abstractions in a separate, lightweight library, the `Application.Services` and `Domain` layers can depend on these contracts without needing any reference to the concrete infrastructure implementations (like SQLite or Serilog). This is a critical architectural pattern that completely decouples the core application logic from the details of data storage and logging, enhancing testability, maintainability, and interchangeability of infrastructure components.

# 4 Type

🔹 Cross-Cutting Library

# 5 Namespace

MonopolyTycoon.Application.Abstractions

# 6 Output Path

src/application/MonopolyTycoon.Application.Abstractions

# 7 Framework

.NET 8

# 8 Language

C#

# 9 Technology

.NET Class Library

# 10 Thirdparty Libraries

*No items available*

# 11 Layer Ids

- app_services_layer

# 12 Dependencies

- REPO-DM-001

# 13 Requirements

*No items available*

# 14 Generate Tests

❌ No

# 15 Generate Documentation

✅ Yes

# 16 Architecture Style

Layered Architecture

# 17 Architecture Map

- GameSessionService

# 18 Components Map

*No items available*

# 19 Requirements Map

*No items available*

# 20 Dependency Contracts

## 20.1 Repo-Dm-001

### 20.1.1 Required Interfaces

- {'interface': 'GameState', 'methods': [], 'events': [], 'properties': []}

### 20.1.2 Integration Pattern

Direct project reference to use domain models in interface signatures.

### 20.1.3 Communication Protocol

In-memory.

# 21.0.0 Exposed Contracts

## 21.1.0 Public Interfaces

### 21.1.1 Interface

#### 21.1.1.1 Interface

ISaveGameRepository

#### 21.1.1.2 Methods

- Task SaveAsync(GameState state, int slot)
- Task<GameState> LoadAsync(int slot)
- Task<List<SaveGameMetadata>> ListSavesAsync()

#### 21.1.1.3 Events

*No items available*

#### 21.1.1.4 Properties

*No items available*

#### 21.1.1.5 Consumers

- REPO-AS-005
- REPO-IP-SG-008

### 21.1.2.0 Interface

#### 21.1.2.1 Interface

IStatisticsRepository

#### 21.1.2.2 Methods

- Task AddGameResultAsync(GameResult result)
- Task<List<TopScore>> GetTopScoresAsync()
- Task UpdatePlayerStatsAsync(PlayerStats stats)

#### 21.1.2.3 Events

*No items available*

#### 21.1.2.4 Properties

*No items available*

#### 21.1.2.5 Consumers

- REPO-AS-005
- REPO-IP-ST-009

### 21.1.3.0 Interface

#### 21.1.3.1 Interface

ILogger

#### 21.1.3.2 Methods

- void Information(string messageTemplate, params object[] propertyValues)
- void Warning(string messageTemplate, params object[] propertyValues)
- void Error(Exception ex, string messageTemplate)

#### 21.1.3.3 Events

*No items available*

#### 21.1.3.4 Properties

*No items available*

#### 21.1.3.5 Consumers

- REPO-AS-005
- REPO-IL-006

# 22.0.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | These interfaces are the fundamental contracts use... |
| Event Communication | N/A |
| Data Flow | Defines the data flow contracts for I/O operations... |
| Error Handling | N/A |
| Async Patterns | Interfaces must define async method signatures (e.... |

# 23.0.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | This project must contain only C# interfaces, enum... |
| Performance Considerations | N/A |
| Security Considerations | N/A |
| Testing Approach | N/A - This repository contains no concrete impleme... |

# 24.0.0.0 Scope Boundaries

## 24.1.0.0 Must Implement

- All abstract contracts for external-facing services like persistence, logging, configuration, etc.

## 24.2.0.0 Must Not Implement

- Any concrete classes with logic.
- Any third-party library dependencies.

## 24.3.0.0 Extension Points

- New interfaces can be added to support new types of infrastructure services.

## 24.4.0.0 Validation Rules

*No items available*

