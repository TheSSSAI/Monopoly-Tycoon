# 1 Id

REPO-IP-SG-008

# 2 Name

MonopolyTycoon.Infrastructure.Persistence.SaveGames

# 3 Description

This repository is responsible for the specific task of saving and loading the game state. It was decomposed from the general `MonopolyTycoon.Infrastructure` to isolate the technology and logic related to game persistence. It implements the `ISaveGameRepository` interface from the `Application.Abstractions` layer. Its responsibilities include serializing the `GameState` object to a versioned JSON format (REQ-1-087), writing it to the correct user directory, and performing the reverse process for loading. Crucially, it also handles checksum validation (REQ-1-088) and coordinates with the data migration service (REQ-1-090) for handling older save file versions. This separation provides a clear focus for a critical, user-facing feature of the application, ensuring its logic is robust and independently testable.

# 4 Type

🔹 Data Access

# 5 Namespace

MonopolyTycoon.Infrastructure.Persistence.SaveGames

# 6 Output Path

src/infrastructure/MonopolyTycoon.Infrastructure.Persistence.SaveGames

# 7 Framework

.NET 8

# 8 Language

C#

# 9 Technology

System.Text.Json, File System API

# 10 Thirdparty Libraries

*No items available*

# 11 Layer Ids

- infrastructure_layer

# 12 Dependencies

- REPO-DM-001
- REPO-AA-004
- REPO-IL-006

# 13 Requirements

## 13.1 Requirement Id

### 13.1.1 Requirement Id

REQ-1-085

## 13.2.0 Requirement Id

### 13.2.1 Requirement Id

REQ-1-087

## 13.3.0 Requirement Id

### 13.3.1 Requirement Id

REQ-1-088

## 13.4.0 Requirement Id

### 13.4.1 Requirement Id

REQ-1-090

# 14.0.0 Generate Tests

✅ Yes

# 15.0.0 Generate Documentation

✅ Yes

# 16.0.0 Architecture Style

Repository Pattern

# 17.0.0 Architecture Map

- GameSaveRepository
- DataMigrationManager

# 18.0.0 Components Map

- save-game-repository-104

# 19.0.0 Requirements Map

- REQ-1-085
- REQ-1-087

# 20.0.0 Dependency Contracts

## 20.1.0 Repo-Aa-004

### 20.1.1 Required Interfaces

#### 20.1.1.1 Interface

##### 20.1.1.1.1 Interface

ISaveGameRepository

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
- Warning(string messageTemplate, params object[] propertyValues)

##### 20.1.1.2.3 Events

*No items available*

##### 20.1.1.2.4 Properties

*No items available*

### 20.1.2.0.0 Integration Pattern

Implements the `ISaveGameRepository` interface and consumes the `ILogger` interface for robust error logging.

### 20.1.3.0.0 Communication Protocol

N/A

# 21.0.0.0.0 Exposed Contracts

## 21.1.0.0.0 Public Interfaces

- {'interface': 'ISaveGameRepository (Implementation)', 'methods': ['Task SaveAsync(GameState state, int slot)', 'Task<GameState> LoadAsync(int slot)'], 'events': [], 'properties': [], 'consumers': []}

# 22.0.0.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | The `GameSaveRepository` class is registered with ... |
| Event Communication | N/A |
| Data Flow | Receives a `GameState` object for writing to disk;... |
| Error Handling | Handles file I/O exceptions, deserialization error... |
| Async Patterns | Uses async file I/O operations (`File.WriteAllText... |

# 23.0.0.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Uses `System.Text.Json` for serialization and stan... |
| Performance Considerations | Serialization and file I/O are on the critical pat... |
| Security Considerations | Checksums (e.g., SHA256) must be implemented to pr... |
| Testing Approach | Integration tests are critical. They must write a ... |

# 24.0.0.0.0 Scope Boundaries

## 24.1.0.0.0 Must Implement

- All logic for reading and writing game save files to the local file system (`%APPDATA%`).
- Serialization and deserialization of the `GameState` object to versioned JSON (REQ-1-087).
- Checksum generation on save and validation on load (REQ-1-088).
- Logic to detect old save versions and trigger a data migration process (REQ-1-090).

## 24.2.0.0.0 Must Not Implement

- Logic for persisting player statistics or profiles.
- Any core game logic or rule enforcement.

## 24.3.0.0.0 Extension Points

- The data migration service within this repository is designed to be extensible with new migration steps for future versions.

## 24.4.0.0.0 Validation Rules

- Checksum validation on file read.
- Version compatibility check on file read.

