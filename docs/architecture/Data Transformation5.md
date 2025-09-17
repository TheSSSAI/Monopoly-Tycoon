# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Technology Stack

- .NET 8
- C#
- Unity Engine
- System.Text.Json
- Microsoft.Data.Sqlite
- Serilog

## 1.3 Service Interfaces

- GameSessionService
- GameSaveRepository
- StatisticsRepository
- ConfigurationProvider

## 1.4 Data Models

- GameState (Domain Object)
- PlayerState (Domain Object)
- PlayerProfile (DB Entity)
- PlayerStatistic (DB Entity)
- GameResult (DB Entity)
- GameParticipant (DB Entity)

# 2.0 Data Mapping Strategy

## 2.1 Essential Mappings

### 2.1.1 Mapping Id

#### 2.1.1.1 Mapping Id

MAP-001

#### 2.1.1.2 Source

GameState (Domain Object)

#### 2.1.1.3 Target

JSON Save File

#### 2.1.1.4 Transformation

direct

#### 2.1.1.5 Configuration

##### 2.1.1.5.1 Versioning

Required

#### 2.1.1.6.0 Mapping Technique

Object Serialization

#### 2.1.1.7.0 Justification

REQ-1-087 requires saving the entire game state to a versioned JSON file.

#### 2.1.1.8.0 Complexity

medium

### 2.1.2.0.0 Mapping Id

#### 2.1.2.1.0 Mapping Id

MAP-002

#### 2.1.2.2.0 Source

JSON Save File

#### 2.1.2.3.0 Target

GameState (Domain Object)

#### 2.1.2.4.0 Transformation

direct

#### 2.1.2.5.0 Configuration

##### 2.1.2.5.1 Version Check

Required

##### 2.1.2.5.2 Checksum Validation

Required

#### 2.1.2.6.0 Mapping Technique

Object Deserialization

#### 2.1.2.7.0 Justification

REQ-1-085 and REQ-1-088 require loading a game state from a validated JSON file.

#### 2.1.2.8.0 Complexity

medium

### 2.1.3.0.0 Mapping Id

#### 2.1.3.1.0 Mapping Id

MAP-003

#### 2.1.3.2.0 Source

GameState (at game end)

#### 2.1.3.3.0 Target

SQLite Database (GameResult, GameParticipant entities)

#### 2.1.3.4.0 Transformation

splitting

#### 2.1.3.5.0 Configuration

*No data available*

#### 2.1.3.6.0 Mapping Technique

Object-Relational Mapping (Manual)

#### 2.1.3.7.0 Justification

REQ-1-070 and REQ-1-089 require persisting a detailed summary of the completed game, which involves splitting the final GameState into multiple related database records.

#### 2.1.3.8.0 Complexity

medium

### 2.1.4.0.0 Mapping Id

#### 2.1.4.1.0 Mapping Id

MAP-004

#### 2.1.4.2.0 Source

Game-End Statistics

#### 2.1.4.3.0 Target

PlayerStatistic (DB Entity)

#### 2.1.4.4.0 Transformation

aggregation

#### 2.1.4.5.0 Configuration

*No data available*

#### 2.1.4.6.0 Mapping Technique

Data Aggregation and Update

#### 2.1.4.7.0 Justification

REQ-1-033 and REQ-1-034 require updating the player's historical statistics by aggregating the results of the just-completed game.

#### 2.1.4.8.0 Complexity

simple

## 2.2.0.0.0 Object To Object Mappings

- {'sourceObject': 'GameState', 'targetObject': 'GameResult, GameParticipant[]', 'fieldMappings': [{'sourceField': 'GameState.game_metadata', 'targetField': 'GameResult', 'transformation': 'Direct Mapping', 'dataTypeConversion': 'None'}, {'sourceField': 'GameState.PlayerState[]', 'targetField': 'GameParticipant[]', 'transformation': 'Collection Mapping', 'dataTypeConversion': 'None'}]}

## 2.3.0.0.0 Data Type Conversions

### 2.3.1.0.0 From

#### 2.3.1.1.0 From

System.Decimal (C#)

#### 2.3.1.2.0 To

REAL (SQLite)

#### 2.3.1.3.0 Conversion Method

Direct type mapping via ORM/library

#### 2.3.1.4.0 Validation Required

❌ No

### 2.3.2.0.0 From

#### 2.3.2.1.0 From

System.Guid (C#)

#### 2.3.2.2.0 To

TEXT (SQLite)

#### 2.3.2.3.0 Conversion Method

ToString() / Guid.Parse()

#### 2.3.2.4.0 Validation Required

✅ Yes

## 2.4.0.0.0 Bidirectional Mappings

- {'entity': 'GameState', 'forwardMapping': 'Serialization to JSON (MAP-001)', 'reverseMapping': 'Deserialization from JSON (MAP-002)', 'consistencyStrategy': 'Checksum validation (REQ-1-088) ensures integrity on read.'}

# 3.0.0.0.0 Schema Validation Requirements

## 3.1.0.0.0 Field Level Validations

### 3.1.1.0.0 Field

#### 3.1.1.1.0 Field

PlayerProfile.displayName

#### 3.1.1.2.0 Rules

- LENGTH_BETWEEN_3_AND_16
- REGEX_NO_SPECIAL_CHARS

#### 3.1.1.3.0 Priority

🚨 critical

#### 3.1.1.4.0 Error Message

Display name must be 3-16 characters long and contain no special characters.

### 3.1.2.0.0 Field

#### 3.1.2.1.0 Field

PlayerStatistic.totalWins

#### 3.1.2.2.0 Rules

- NON_NEGATIVE

#### 3.1.2.3.0 Priority

🔴 high

#### 3.1.2.4.0 Error Message

Total wins cannot be negative.

## 3.2.0.0.0 Cross Field Validations

*No items available*

## 3.3.0.0.0 Business Rule Validations

### 3.3.1.0.0 Rule Id

#### 3.3.1.1.0 Rule Id

VALIDATE-SAVE-INTEGRITY

#### 3.3.1.2.0 Description

Validates that a save file has not been corrupted or tampered with.

#### 3.3.1.3.0 Fields

- Entire JSON save file content

#### 3.3.1.4.0 Logic

Compare a stored checksum within the file against a newly computed checksum of the file's content.

#### 3.3.1.5.0 Priority

🚨 critical

### 3.3.2.0.0 Rule Id

#### 3.3.2.1.0 Rule Id

VALIDATE-SAVE-VERSION

#### 3.3.2.2.0 Description

Ensures the save file version is compatible with the current application version.

#### 3.3.2.3.0 Fields

- gameVersion field in JSON save file

#### 3.3.2.4.0 Logic

Compare the file's version number against the application's supported version range.

#### 3.3.2.5.0 Priority

🚨 critical

## 3.4.0.0.0 Conditional Validations

*No items available*

## 3.5.0.0.0 Validation Groups

*No items available*

# 4.0.0.0.0 Transformation Pattern Evaluation

## 4.1.0.0.0 Selected Patterns

### 4.1.1.0.0 Pattern

#### 4.1.1.1.0 Pattern

adapter

#### 4.1.1.2.0 Use Case

The Repository pattern implementation for Statistics acts as an adapter between the application's domain objects (e.g., GameState) and the data entities required by the SQLite database.

#### 4.1.1.3.0 Implementation

StatisticsRepository class

#### 4.1.1.4.0 Justification

Decouples the Application Services layer from the specific data persistence technology (SQLite) as per the defined architecture.

### 4.1.2.0.0 Pattern

#### 4.1.2.1.0 Pattern

converter

#### 4.1.2.2.0 Use Case

Converting the GameState domain object to and from its JSON representation for save files.

#### 4.1.2.3.0 Implementation

A dedicated GameStateSerializer class using System.Text.Json.

#### 4.1.2.4.0 Justification

Centralizes the logic for serialization and deserialization, including versioning and checksum calculation, as required by REQ-1-087 and REQ-1-088.

## 4.2.0.0.0 Pipeline Processing

### 4.2.1.0.0 Required

❌ No

### 4.2.2.0.0 Stages

*No items available*

### 4.2.3.0.0 Parallelization

❌ No

## 4.3.0.0.0 Processing Mode

### 4.3.1.0.0 Real Time

#### 4.3.1.1.0 Required

✅ Yes

#### 4.3.1.2.0 Scenarios

- Saving game state
- Loading game state
- Updating statistics after a game

#### 4.3.1.3.0 Latency Requirements

Loading must not exceed 10 seconds (REQ-1-015).

### 4.3.2.0.0 Batch

| Property | Value |
|----------|-------|
| Required | ❌ |
| Batch Size | 0 |
| Frequency |  |

### 4.3.3.0.0 Streaming

| Property | Value |
|----------|-------|
| Required | ❌ |
| Streaming Framework |  |
| Windowing Strategy |  |

## 4.4.0.0.0 Canonical Data Model

### 4.4.1.0.0 Applicable

✅ Yes

### 4.4.2.0.0 Scope

- In-Memory Game State

### 4.4.3.0.0 Benefits

- The `GameState` domain object serves as the canonical model for an active game, ensuring a single, consistent source of truth that is then transformed for different persistence needs (JSON for saves, SQLite for historical stats).

# 5.0.0.0.0 Version Handling Strategy

## 5.1.0.0.0 Schema Evolution

### 5.1.1.0.0 Strategy

On-Read Data Migration

### 5.1.2.0.0 Versioning Scheme

Semantic versioning string (e.g., '1.1.0') stored within the JSON save file and a schema version number in the SQLite DB.

### 5.1.3.0.0 Compatibility

| Property | Value |
|----------|-------|
| Backward | ✅ |
| Forward | ❌ |
| Reasoning | REQ-1-090 requires the application to handle older... |

## 5.2.0.0.0 Transformation Versioning

| Property | Value |
|----------|-------|
| Mechanism | Application code contains version-specific migrati... |
| Version Identification | Version property in the data file. |
| Migration Strategy | An automated, atomic process that attempts to upgr... |

## 5.3.0.0.0 Data Model Changes

| Property | Value |
|----------|-------|
| Migration Path | A chain of migration steps, e.g., v1 -> v2, v2 -> ... |
| Rollback Strategy | If migration fails, the original file is left unto... |
| Validation Strategy | Post-migration, the data is validated against the ... |

## 5.4.0.0.0 Schema Registry

| Property | Value |
|----------|-------|
| Required | ❌ |
| Technology |  |
| Governance |  |

# 6.0.0.0.0 Performance Optimization

## 6.1.0.0.0 Critical Requirements

- {'operation': 'Load Game (JSON Deserialization)', 'maxLatency': '10 seconds', 'throughputTarget': 'N/A (single-user)', 'justification': 'REQ-1-015 specifies a maximum load time to ensure a good user experience.'}

## 6.2.0.0.0 Parallelization Opportunities

*No items available*

## 6.3.0.0.0 Caching Strategies

- {'cacheType': 'In-Memory', 'cacheScope': 'Application Session', 'evictionPolicy': 'None (cleared on exit)', 'applicableTransformations': ['AI Configuration JSON Deserialization (REQ-1-063)', 'Localization String JSON Deserialization (REQ-1-084)']}

## 6.4.0.0.0 Memory Optimization

### 6.4.1.0.0 Techniques

- Use of streaming APIs in System.Text.Json if save files become excessively large.

### 6.4.2.0.0 Thresholds



### 6.4.3.0.0 Monitoring Required

❌ No

## 6.5.0.0.0 Lazy Evaluation

### 6.5.1.0.0 Applicable

❌ No

### 6.5.2.0.0 Scenarios

*No items available*

### 6.5.3.0.0 Implementation



## 6.6.0.0.0 Bulk Processing

### 6.6.1.0.0 Required

❌ No

### 6.6.2.0.0 Batch Sizes

#### 6.6.2.1.0 Optimal

0

#### 6.6.2.2.0 Maximum

0

### 6.6.3.0.0 Parallelism

0

# 7.0.0.0.0 Error Handling And Recovery

## 7.1.0.0.0 Error Handling Strategies

### 7.1.1.0.0 Error Type

#### 7.1.1.1.0 Error Type

Save File Corruption

#### 7.1.1.2.0 Strategy

Detect via checksum, mark file as unusable in the UI, and log the error.

#### 7.1.1.3.0 Fallback Action

Prevent loading the corrupted file.

#### 7.1.1.4.0 Escalation Path

- Log File

### 7.1.2.0.0 Error Type

#### 7.1.2.1.0 Error Type

Incompatible Save Version

#### 7.1.2.2.0 Strategy

Attempt automatic migration. If it fails, mark file as unusable and log the error.

#### 7.1.2.3.0 Fallback Action

Prevent loading the incompatible file.

#### 7.1.2.4.0 Escalation Path

- Log File

### 7.1.3.0.0 Error Type

#### 7.1.3.1.0 Error Type

Unhandled Transformation Exception

#### 7.1.3.2.0 Strategy

Catch globally, log the exception with a unique ID, and display a user-friendly error dialog.

#### 7.1.3.3.0 Fallback Action

Halt the current operation gracefully.

#### 7.1.3.4.0 Escalation Path

- Log File
- User-facing Error Dialog (REQ-1-023)

## 7.2.0.0.0 Logging Requirements

### 7.2.1.0.0 Log Level

error

### 7.2.2.0.0 Included Data

- Timestamp
- Unique Error ID
- Exception Details (stack trace, message)
- File Path (if applicable)
- Data Version (if applicable)

### 7.2.3.0.0 Retention Period

7 days or 50MB (REQ-1-021)

### 7.2.4.0.0 Alerting

❌ No

## 7.3.0.0.0 Partial Success Handling

### 7.3.1.0.0 Strategy

Atomic operations. For data migration, the entire process must succeed or fail as a single unit.

### 7.3.2.0.0 Reporting Mechanism

Log file entry indicating success or failure of the migration attempt.

### 7.3.3.0.0 Recovery Actions

- If migration fails, restore the original file from a temporary backup made before the attempt.

## 7.4.0.0.0 Circuit Breaking

*No items available*

## 7.5.0.0.0 Retry Strategies

*No items available*

## 7.6.0.0.0 Error Notifications

- {'condition': 'Any unhandled transformation error occurs.', 'recipients': ['User'], 'severity': 'high', 'channel': 'Modal Error Dialog'}

# 8.0.0.0.0 Project Specific Transformations

## 8.1.0.0.0 Game State Serialization

### 8.1.1.0.0 Transformation Id

PST-001

### 8.1.2.0.0 Name

Game State Serialization

### 8.1.3.0.0 Description

Transforms the in-memory GameState C# object into a versioned JSON string for persistence in a save file.

### 8.1.4.0.0 Source

#### 8.1.4.1.0 Service

GameSessionService

#### 8.1.4.2.0 Model

GameState

#### 8.1.4.3.0 Fields

- All

### 8.1.5.0.0 Target

#### 8.1.5.1.0 Service

GameSaveRepository

#### 8.1.5.2.0 Model

JSON File

#### 8.1.5.3.0 Fields

- All

### 8.1.6.0.0 Transformation

#### 8.1.6.1.0 Type

🔹 direct

#### 8.1.6.2.0 Logic

Use System.Text.Json.JsonSerializer to serialize the GameState object. A checksum and version number are added to the resulting structure.

#### 8.1.6.3.0 Configuration

*No data available*

### 8.1.7.0.0 Frequency

on-demand

### 8.1.8.0.0 Criticality

critical

### 8.1.9.0.0 Dependencies

- REQ-1-087

### 8.1.10.0.0 Validation

#### 8.1.10.1.0 Pre Transformation

*No items available*

#### 8.1.10.2.0 Post Transformation

- Checksum generation

### 8.1.11.0.0 Performance

| Property | Value |
|----------|-------|
| Expected Volume | Low (single object per operation) |
| Latency Requirement | Should be near-instantaneous. |
| Optimization Strategy | Standard library usage. |

## 8.2.0.0.0 Game State Deserialization and Validation

### 8.2.1.0.0 Transformation Id

PST-002

### 8.2.2.0.0 Name

Game State Deserialization and Validation

### 8.2.3.0.0 Description

Transforms a JSON save file into an in-memory GameState C# object after validating its integrity and version.

### 8.2.4.0.0 Source

#### 8.2.4.1.0 Service

GameSaveRepository

#### 8.2.4.2.0 Model

JSON File

#### 8.2.4.3.0 Fields

- All

### 8.2.5.0.0 Target

#### 8.2.5.1.0 Service

GameSessionService

#### 8.2.5.2.0 Model

GameState

#### 8.2.5.3.0 Fields

- All

### 8.2.6.0.0 Transformation

#### 8.2.6.1.0 Type

🔹 direct

#### 8.2.6.2.0 Logic

First, validate checksum and version (REQ-1-088). If version is old, trigger migration (REQ-1-090). If valid, use System.Text.Json.JsonSerializer to deserialize into a GameState object.

#### 8.2.6.3.0 Configuration

*No data available*

### 8.2.7.0.0 Frequency

on-demand

### 8.2.8.0.0 Criticality

critical

### 8.2.9.0.0 Dependencies

- REQ-1-088
- REQ-1-090

### 8.2.10.0.0 Validation

#### 8.2.10.1.0 Pre Transformation

- VALIDATE-SAVE-INTEGRITY
- VALIDATE-SAVE-VERSION

#### 8.2.10.2.0 Post Transformation

*No items available*

### 8.2.11.0.0 Performance

| Property | Value |
|----------|-------|
| Expected Volume | Low (single file per operation) |
| Latency Requirement | < 10 seconds (REQ-1-015) |
| Optimization Strategy | Efficient deserialization using source generation ... |

## 8.3.0.0.0 Game Result Persistence

### 8.3.1.0.0 Transformation Id

PST-003

### 8.3.2.0.0 Name

Game Result Persistence

### 8.3.3.0.0 Description

Transforms the final state of a completed game into normalized records for storage in the SQLite statistics database.

### 8.3.4.0.0 Source

#### 8.3.4.1.0 Service

ApplicationServicesLayer

#### 8.3.4.2.0 Model

GameState

#### 8.3.4.3.0 Fields

- game_metadata
- PlayerState[]

### 8.3.5.0.0 Target

#### 8.3.5.1.0 Service

StatisticsRepository

#### 8.3.5.2.0 Model

GameResult, GameParticipant

#### 8.3.5.3.0 Fields

- All fields for both entities

### 8.3.6.0.0 Transformation

#### 8.3.6.1.0 Type

🔹 splitting

#### 8.3.6.2.0 Logic

Create a single GameResult record from game metadata. Iterate through the final list of players in GameState, creating a GameParticipant record for each, linked to the new GameResult.

#### 8.3.6.3.0 Configuration

*No data available*

### 8.3.7.0.0 Frequency

on-demand

### 8.3.8.0.0 Criticality

high

### 8.3.9.0.0 Dependencies

- REQ-1-070
- REQ-1-091

### 8.3.10.0.0 Validation

#### 8.3.10.1.0 Pre Transformation

*No items available*

#### 8.3.10.2.0 Post Transformation

*No items available*

### 8.3.11.0.0 Performance

| Property | Value |
|----------|-------|
| Expected Volume | Low (1 GameResult, 2-4 GameParticipants per operat... |
| Latency Requirement | Should be near-instantaneous. |
| Optimization Strategy | Wrap database writes in a single transaction. |

# 9.0.0.0.0 Implementation Priority

## 9.1.0.0.0 Component

### 9.1.1.0.0 Component

GameState <-> JSON Transformation (PST-001, PST-002)

### 9.1.2.0.0 Priority

🔴 high

### 9.1.3.0.0 Dependencies

*No items available*

### 9.1.4.0.0 Estimated Effort

Medium

### 9.1.5.0.0 Risk Level

high

## 9.2.0.0.0 Component

### 9.2.1.0.0 Component

Game Result Persistence Transformation (PST-003)

### 9.2.2.0.0 Priority

🔴 high

### 9.2.3.0.0 Dependencies

*No items available*

### 9.2.4.0.0 Estimated Effort

Medium

### 9.2.5.0.0 Risk Level

medium

## 9.3.0.0.0 Component

### 9.3.1.0.0 Component

Data Migration Framework (for Saves and DB)

### 9.3.2.0.0 Priority

🟡 medium

### 9.3.3.0.0 Dependencies

- GameState <-> JSON Transformation

### 9.3.4.0.0 Estimated Effort

High

### 9.3.5.0.0 Risk Level

high

## 9.4.0.0.0 Component

### 9.4.1.0.0 Component

Configuration File Transformations (AI, Localization)

### 9.4.2.0.0 Priority

🟡 medium

### 9.4.3.0.0 Dependencies

*No items available*

### 9.4.4.0.0 Estimated Effort

Low

### 9.4.5.0.0 Risk Level

low

# 10.0.0.0.0 Risk Assessment

## 10.1.0.0.0 Risk

### 10.1.1.0.0 Risk

A flaw in the serialization/deserialization logic could lead to corrupted save files, causing permanent loss of user progress.

### 10.1.2.0.0 Impact

high

### 10.1.3.0.0 Probability

medium

### 10.1.4.0.0 Mitigation

Implement robust checksum validation (REQ-1-088). Create extensive integration tests for the save/load cycle (REQ-1-026). Use a temporary file for writing saves and only replace the original on successful completion.

### 10.1.5.0.0 Contingency Plan

The statistics database backup mechanism (REQ-1-089) provides some data protection, but save files would be lost. Provide clear instructions to users on how to manually back up their save data folder.

## 10.2.0.0.0 Risk

### 10.2.1.0.0 Risk

The data migration logic for a new version fails, preventing users from accessing their old save files or statistics after an update.

### 10.2.2.0.0 Impact

high

### 10.2.3.0.0 Probability

medium

### 10.2.4.0.0 Mitigation

Design migration to be atomic (all-or-nothing). Thoroughly test migration paths using predefined test files from older versions (REQ-1-027). The logic must leave the original file untouched on failure.

### 10.2.5.0.0 Contingency Plan

Provide a standalone data migration tool or detailed instructions for users to manually export/import key data if the automatic process fails.

# 11.0.0.0.0 Recommendations

## 11.1.0.0.0 Category

### 11.1.1.0.0 Category

🔹 Implementation

### 11.1.2.0.0 Recommendation

Implement the core serialization and persistence transformations within the Infrastructure Layer using the Repository pattern, as defined in the architecture.

### 11.1.3.0.0 Justification

This enforces the layered architecture, ensuring that domain logic remains decoupled from the specifics of JSON files or SQLite databases, which improves maintainability and testability.

### 11.1.4.0.0 Priority

🔴 high

### 11.1.5.0.0 Implementation Notes

The GameSaveRepository will handle JSON transformation. The StatisticsRepository will handle the transformation to SQLite entities.

## 11.2.0.0.0 Category

### 11.2.1.0.0 Category

🔹 Validation

### 11.2.2.0.0 Recommendation

Perform data validation (e.g., checksum, version check) immediately upon reading data from a source, before any further processing or deserialization.

### 11.2.3.0.0 Justification

This fail-fast approach prevents corrupted or incompatible data from propagating through the system, reducing the risk of unexpected exceptions in the business logic layer.

### 11.2.4.0.0 Priority

🔴 high

### 11.2.5.0.0 Implementation Notes

The repository's 'GetById' or 'Load' method should be responsible for this initial validation.

## 11.3.0.0.0 Category

### 11.3.1.0.0 Category

🔹 Versioning

### 11.3.2.0.0 Recommendation

Develop a simple, forward-only data migration framework early in the development cycle.

### 11.3.3.0.0 Justification

Addressing schema evolution from the start (as required by REQ-1-090) is less costly than retrofitting it later. It ensures that user data can be preserved across application updates, which is critical for player retention.

### 11.3.4.0.0 Priority

🟡 medium

### 11.3.5.0.0 Implementation Notes

The framework should allow registering version-specific transformation steps that are executed sequentially to bring data up to the current version.

