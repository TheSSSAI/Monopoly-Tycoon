# 1 Id

REPO-IP-ST-009

# 2 Name

MonopolyTycoon.Infrastructure.Persistence.Statistics

# 3 Description

This repository handles all interactions with the local SQLite database for persisting long-term user data. Decomposed from `MonopolyTycoon.Infrastructure`, its sole focus is on storing and retrieving player profiles (REQ-1-032), historical gameplay statistics (REQ-1-033), and the Top 10 high scores list (REQ-1-091). It implements the `IStatisticsRepository` and `IPlayerProfileRepository` interfaces using `Microsoft.Data.Sqlite`. This separation isolates all database-specific code, including schema definitions, SQL queries, connection management, and the automated backup mechanism (REQ-1-089), from other persistence types like file-based saves. This allows the database technology to be managed, updated, or even replaced independently of the rest of the application's infrastructure.

# 4 Type

🔹 Data Access

# 5 Namespace

MonopolyTycoon.Infrastructure.Persistence.Statistics

# 6 Output Path

src/infrastructure/MonopolyTycoon.Infrastructure.Persistence.Statistics

# 7 Framework

.NET 8

# 8 Language

C#

# 9 Technology

SQLite, Microsoft.Data.Sqlite

# 10 Thirdparty Libraries

- Microsoft.Data.Sqlite

# 11 Layer Ids

- infrastructure_layer

# 12 Dependencies

- REPO-DM-001
- REPO-AA-004
- REPO-IL-006

# 13 Requirements

## 13.1 Requirement Id

### 13.1.1 Requirement Id

REQ-1-033

## 13.2.0 Requirement Id

### 13.2.1 Requirement Id

REQ-1-089

## 13.3.0 Requirement Id

### 13.3.1 Requirement Id

REQ-1-091

# 14.0.0 Generate Tests

✅ Yes

# 15.0.0 Generate Documentation

✅ Yes

# 16.0.0 Architecture Style

Repository Pattern

# 17.0.0 Architecture Map

- StatisticsRepository
- PlayerProfileRepository

# 18.0.0 Components Map

- statistics-repository-105

# 19.0.0 Requirements Map

- REQ-1-033
- REQ-1-089

# 20.0.0 Dependency Contracts

## 20.1.0 Repo-Aa-004

### 20.1.1 Required Interfaces

#### 20.1.1.1 Interface

##### 20.1.1.1.1 Interface

IStatisticsRepository

##### 20.1.1.1.2 Methods

*No items available*

##### 20.1.1.1.3 Events

*No items available*

##### 20.1.1.1.4 Properties

*No items available*

#### 20.1.1.2.0 Interface

##### 20.1.1.2.1 Interface

ILogger

##### 20.1.1.2.2 Methods

- Error(Exception ex, string messageTemplate)

##### 20.1.1.2.3 Events

*No items available*

##### 20.1.1.2.4 Properties

*No items available*

### 20.1.2.0.0 Integration Pattern

Implements repository interfaces from the abstractions layer and consumes the `ILogger` interface for error handling.

### 20.1.3.0.0 Communication Protocol

N/A

# 21.0.0.0.0 Exposed Contracts

## 21.1.0.0.0 Public Interfaces

- {'interface': 'IStatisticsRepository (Implementation)', 'methods': ['Task UpdatePlayerStatsAsync(PlayerStats stats)', 'Task<PlayerStats> GetPlayerStatsAsync(Guid profileId)'], 'events': [], 'properties': [], 'consumers': []}

# 22.0.0.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | The `StatisticsRepository` class is registered wit... |
| Event Communication | N/A |
| Data Flow | Receives DTOs or domain objects from the Applicati... |
| Error Handling | Handles `SqliteException` (e.g., connection errors... |
| Async Patterns | Uses async database APIs (`ExecuteNonQueryAsync`, ... |

# 23.0.0.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | A lightweight ORM like Dapper could be considered ... |
| Performance Considerations | Queries for the Top 10 high score list must be opt... |
| Security Considerations | Must exclusively use parameterized queries to prev... |
| Testing Approach | Integration tests should be written against an in-... |

# 24.0.0.0.0 Scope Boundaries

## 24.1.0.0.0 Must Implement

- All interactions with the `stats.db` SQLite file.
- Schema creation and management.
- CRUD operations for player profiles, statistics, and game results.
- The automated database backup and retention logic as per REQ-1-089.

## 24.2.0.0.0 Must Not Implement

- Saving or loading the main game state (that is the `SaveGames` repository's job).
- Any business logic; it only persists and retrieves data.

## 24.3.0.0.0 Extension Points

- A schema migration system (e.g., using simple version tracking in a metadata table) could be added to handle database changes in future application versions.

## 24.4.0.0.0 Validation Rules

*No items available*

