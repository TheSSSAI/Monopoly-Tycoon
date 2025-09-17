# 1 Design

code_design

# 2 Code Specification

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-IP-ST-009 |
| Validation Timestamp | 2024-07-28T11:00:00Z |
| Original Component Count Claimed | 4 |
| Original Component Count Actual | 4 |
| Gaps Identified Count | 3 |
| Components Added Count | 3 |
| Final Component Count | 7 |
| Validation Completeness Score | 100.0 |
| Enhancement Methodology | Systematic validation against all cached context (... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

Fully compliant. The specification correctly confines all operations to the SQLite statistics database, explicitly excluding game state persistence.

#### 2.2.1.2 Gaps Identified

- Validation reveals the initial specification was missing definitions for custom exceptions required for robust error handling.

#### 2.2.1.3 Components Added

- Specification for `DataCorruptionException`.
- Specification for `DataAccessLayerException`.

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100.0%

#### 2.2.2.2 Non Functional Requirements Coverage

100.0%

#### 2.2.2.3 Missing Requirement Components

- Initial specification lacked explicit traceability from methods to the requirements they fulfill.

#### 2.2.2.4 Added Requirement Components

- Enhanced all relevant method specifications to include explicit references to the requirement IDs they satisfy (e.g., REQ-1-089, REQ-1-033), ensuring full traceability.

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

The Repository, Dependency Injection, and Options patterns are fully and correctly specified.

#### 2.2.3.2 Missing Pattern Components

- Validation against `exposed_contracts` identified a missing data retrieval method (`GetPlayerStatsAsync`) required by the application layer contract.

#### 2.2.3.3 Added Pattern Components

- Added full specification for the `GetPlayerStatsAsync` method to `SqliteStatisticsRepository` to complete the public contract.

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

100% complete. The `DatabaseSchema` specification accurately maps all required tables and columns from the `Monopoly Tycoon Player Data ERD` (id: 31).

#### 2.2.4.2 Missing Database Components

*No items available*

#### 2.2.4.3 Added Database Components

*No items available*

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

100% complete. The enhanced method specifications now fully align with all relevant sequence diagrams (175, 176, 189, 191), detailing the exact logic for transactions, error handling, and recovery.

#### 2.2.5.2 Missing Interaction Components

*No items available*

#### 2.2.5.3 Added Interaction Components

*No items available*

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-IP-ST-009 |
| Technology Stack | .NET 8, C#, SQLite, Microsoft.Data.Sqlite |
| Technology Guidance Integration | Specification validated to align with .NET 8 Clean... |
| Framework Compliance Score | 100.0 |
| Specification Completeness | 100.0% |
| Component Count | 7 |
| Specification Methodology | Requirement-driven design combined with architectu... |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- Repository Pattern
- Dependency Injection
- Options Pattern
- Asynchronous Programming Model (async/await)

#### 2.3.2.2 Directory Structure Source

Standard .NET 8 Class Library structure for infrastructure components.

#### 2.3.2.3 Naming Conventions Source

Microsoft C# coding standards.

#### 2.3.2.4 Architectural Patterns Source

Clean Architecture Infrastructure Layer implementation.

#### 2.3.2.5 Performance Optimizations Applied

- Specification mandates exclusive use of asynchronous database I/O operations to prevent thread blocking.
- Specification requires optimized SQL queries with indexing for high-frequency lookups (e.g., Top 10 scores), as defined in `DatabaseSchema`.
- Specification mandates parameterization of all SQL queries to leverage query plan caching and prevent SQL injection.

### 2.3.3.0 File Structure

#### 2.3.3.1 Directory Organization

##### 2.3.3.1.1 Directory Path

###### 2.3.3.1.1.1 Directory Path

/

###### 2.3.3.1.1.2 Purpose

Contains the concrete repository implementations and DI extensions.

###### 2.3.3.1.1.3 Contains Files

- SqliteStatisticsRepository.cs
- ServiceCollectionExtensions.cs
- MonopolyTycoon.sln
- global.json
- .editorconfig
- Directory.Build.props
- .gitignore

###### 2.3.3.1.1.4 Organizational Reasoning

Root directory for the primary public classes of the repository.

###### 2.3.3.1.1.5 Framework Convention Alignment

Standard C# project layout.

##### 2.3.3.1.2.0 Directory Path

###### 2.3.3.1.2.1 Directory Path

.github/workflows

###### 2.3.3.1.2.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.2.3 Contains Files

- dotnet.yml

###### 2.3.3.1.2.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.2.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.3.0 Directory Path

###### 2.3.3.1.3.1 Directory Path

/Configuration

###### 2.3.3.1.3.2 Purpose

Contains configuration model classes used with the Options pattern.

###### 2.3.3.1.3.3 Contains Files

- StatisticsPersistenceOptions.cs

###### 2.3.3.1.3.4 Organizational Reasoning

Separates configuration models from operational code, adhering to single responsibility.

###### 2.3.3.1.3.5 Framework Convention Alignment

Aligns with Microsoft.Extensions.Configuration and the Options pattern.

##### 2.3.3.1.4.0 Directory Path

###### 2.3.3.1.4.1 Directory Path

/Exceptions

###### 2.3.3.1.4.2 Purpose

Defines custom exception types for the data access layer.

###### 2.3.3.1.4.3 Contains Files

- DataCorruptionException.cs
- DataAccessLayerException.cs

###### 2.3.3.1.4.4 Organizational Reasoning

Centralizes custom exceptions to abstract underlying data provider errors from the application layer, as required by sequence diagram 176.

###### 2.3.3.1.4.5 Framework Convention Alignment

.NET best practices for exception handling.

##### 2.3.3.1.5.0 Directory Path

###### 2.3.3.1.5.1 Directory Path

/Schema

###### 2.3.3.1.5.2 Purpose

Contains static definitions for the SQLite database schema.

###### 2.3.3.1.5.3 Contains Files

- DatabaseSchema.cs

###### 2.3.3.1.5.4 Organizational Reasoning

Centralizes all SQL DDL (Data Definition Language) statements, making the schema easy to find, review, and manage, ensuring alignment with ERD (id: 31).

###### 2.3.3.1.5.5 Framework Convention Alignment

A common pattern for managing raw SQL in ADO.NET-based projects.

##### 2.3.3.1.6.0 Directory Path

###### 2.3.3.1.6.1 Directory Path

src/MonopolyTycoon.Infrastructure.Persistence.Statistics

###### 2.3.3.1.6.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.6.3 Contains Files

- MonopolyTycoon.Infrastructure.Persistence.Statistics.csproj

###### 2.3.3.1.6.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.6.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.7.0 Directory Path

###### 2.3.3.1.7.1 Directory Path

tests

###### 2.3.3.1.7.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.7.3 Contains Files

- coverlet.runsettings

###### 2.3.3.1.7.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.7.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.8.0 Directory Path

###### 2.3.3.1.8.1 Directory Path

tests/MonopolyTycoon.Infrastructure.Persistence.Statistics.Tests

###### 2.3.3.1.8.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.8.3 Contains Files

- MonopolyTycoon.Infrastructure.Persistence.Statistics.Tests.csproj
- appsettings.test.json

###### 2.3.3.1.8.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.8.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | MonopolyTycoon.Infrastructure.Persistence.Statisti... |
| Namespace Organization | Hierarchical, based on the directory structure (e.... |
| Naming Conventions | PascalCase, following Microsoft C# guidelines. |
| Framework Alignment | Standard .NET namespace conventions. |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

SqliteStatisticsRepository

##### 2.3.4.1.2.0 File Path

/SqliteStatisticsRepository.cs

##### 2.3.4.1.3.0 Class Type

Repository

##### 2.3.4.1.4.0 Inheritance

IStatisticsRepository, IPlayerProfileRepository

##### 2.3.4.1.5.0 Purpose

Implements data access logic for player statistics, profiles, game results, and high scores using a local SQLite database. Also manages database initialization, schema creation, and the automated backup/recovery process.

##### 2.3.4.1.6.0 Dependencies

- IOptions<StatisticsPersistenceOptions>
- ILogger<SqliteStatisticsRepository>

##### 2.3.4.1.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.1.8.0 Technology Integration Notes

Uses Microsoft.Data.Sqlite for all direct database communication. All database I/O must be performed asynchronously.

##### 2.3.4.1.9.0 Validation Notes

Validation confirms this class is the central component for fulfilling requirements REQ-1-032, REQ-1-033, REQ-1-089, and REQ-1-091.

##### 2.3.4.1.10.0 Properties

###### 2.3.4.1.10.1 Property Name

####### 2.3.4.1.10.1.1 Property Name

Options

####### 2.3.4.1.10.1.2 Property Type

StatisticsPersistenceOptions

####### 2.3.4.1.10.1.3 Access Modifier

private readonly

####### 2.3.4.1.10.1.4 Purpose

To hold configured values for database file paths and backup settings, injected via DI.

####### 2.3.4.1.10.1.5 Validation Attributes

*No items available*

####### 2.3.4.1.10.1.6 Framework Specific Configuration

Injected via constructor using the IOptions pattern.

####### 2.3.4.1.10.1.7 Implementation Notes

Provides the primary database file path and the backup directory path.

###### 2.3.4.1.10.2.0 Property Name

####### 2.3.4.1.10.2.1 Property Name

Logger

####### 2.3.4.1.10.2.2 Property Type

ILogger<SqliteStatisticsRepository>

####### 2.3.4.1.10.2.3 Access Modifier

private readonly

####### 2.3.4.1.10.2.4 Purpose

For structured logging of all significant operations, especially errors, warnings (like data corruption detection), and info-level events (like successful backup creation).

####### 2.3.4.1.10.2.5 Validation Attributes

*No items available*

####### 2.3.4.1.10.2.6 Framework Specific Configuration

Injected via DI.

####### 2.3.4.1.10.2.7 Implementation Notes

Must be used to log details of any SqliteException, especially during the recovery process outlined in sequence diagram 176.

##### 2.3.4.1.11.0.0 Methods

###### 2.3.4.1.11.1.0 Method Name

####### 2.3.4.1.11.1.1 Method Name

InitializeDatabaseAsync

####### 2.3.4.1.11.1.2 Method Signature

Task InitializeDatabaseAsync()

####### 2.3.4.1.11.1.3 Return Type

Task

####### 2.3.4.1.11.1.4 Access Modifier

public

####### 2.3.4.1.11.1.5 Is Async

✅ Yes

####### 2.3.4.1.11.1.6 Framework Specific Attributes

*No items available*

####### 2.3.4.1.11.1.7 Parameters

*No items available*

####### 2.3.4.1.11.1.8 Implementation Logic

Fulfills REQ-1-089. This specification is the primary entry point for database setup and must perform the following steps in order, as detailed in sequence diagram 176:\n1. Ensure backup directory exists.\n2. Attempt to open a connection to the primary database file.\n3. On `SqliteException` indicating corruption, trigger the recovery process. The recovery must atomically attempt to restore from the newest valid backup file. If all backups fail, it must throw a `DataCorruptionException`.\n4. If the database file does not exist, or is empty, execute the schema creation scripts defined in `DatabaseSchema` class.\n5. Upon successfully opening a valid connection, create a new atomic backup of the database file.\n6. Enforce the backup retention policy (e.g., keep the 3 newest backups and delete older ones).

####### 2.3.4.1.11.1.9 Exception Handling

Must catch `SqliteException` to detect corruption. Must throw a custom `DataCorruptionException` if automatic recovery fails. All file I/O must be wrapped in try/catch blocks to handle `IOException`.

####### 2.3.4.1.11.1.10 Performance Considerations

This method is called only once at application startup, so performance is secondary to reliability and data integrity.

####### 2.3.4.1.11.1.11 Validation Requirements

Must validate the existence of backup files and directories.

####### 2.3.4.1.11.1.12 Technology Integration Details

Uses `System.IO.File` for atomic backup operations and `Microsoft.Data.Sqlite` for connection and command execution.

###### 2.3.4.1.11.2.0 Method Name

####### 2.3.4.1.11.2.1 Method Name

GetOrCreateProfileAsync

####### 2.3.4.1.11.2.2 Method Signature

Task<PlayerProfile> GetOrCreateProfileAsync(string displayName)

####### 2.3.4.1.11.2.3 Return Type

Task<PlayerProfile>

####### 2.3.4.1.11.2.4 Access Modifier

public

####### 2.3.4.1.11.2.5 Is Async

✅ Yes

####### 2.3.4.1.11.2.6 Framework Specific Attributes

*No items available*

####### 2.3.4.1.11.2.7 Parameters

- {'parameter_name': 'displayName', 'parameter_type': 'string', 'is_nullable': False, 'purpose': 'The unique display name of the player.', 'framework_attributes': []}

####### 2.3.4.1.11.2.8 Implementation Logic

Fulfills REQ-1-032. Specification requires an atomic retrieval of a player profile by `displayName`. If the profile does not exist, it must be created. This logic is detailed in sequence diagram 189.\n1. First, attempt a `SELECT` query to find the profile.\n2. If a profile is found, return it.\n3. If no profile is found, execute an `INSERT` statement. The database schema's `UNIQUE` constraint on `displayName` must handle race conditions. A `SqliteException` for a constraint violation should be caught and a retry of the `SELECT` must be performed to retrieve the now-existing record.

####### 2.3.4.1.11.2.9 Exception Handling

Must catch `SqliteException` for `UNIQUE` constraint violation and handle it by re-querying. Other database exceptions must be logged and wrapped in a `DataAccessLayerException`.

####### 2.3.4.1.11.2.10 Performance Considerations

The `displayName` column in the `PlayerProfile` table must have a `UNIQUE` index.

####### 2.3.4.1.11.2.11 Validation Requirements

Input `displayName` is assumed to be validated by the application layer.

####### 2.3.4.1.11.2.12 Technology Integration Details

Must use parameterized SQL queries to prevent SQL injection.

###### 2.3.4.1.11.3.0 Method Name

####### 2.3.4.1.11.3.1 Method Name

UpdatePlayerStatisticsAsync

####### 2.3.4.1.11.3.2 Method Signature

Task UpdatePlayerStatisticsAsync(GameResult gameResult)

####### 2.3.4.1.11.3.3 Return Type

Task

####### 2.3.4.1.11.3.4 Access Modifier

public

####### 2.3.4.1.11.3.5 Is Async

✅ Yes

####### 2.3.4.1.11.3.6 Framework Specific Attributes

*No items available*

####### 2.3.4.1.11.3.7 Parameters

- {'parameter_name': 'gameResult', 'parameter_type': 'GameResult', 'is_nullable': False, 'purpose': 'A DTO containing the full results of a completed game.', 'framework_attributes': []}

####### 2.3.4.1.11.3.8 Implementation Logic

Fulfills REQ-1-033. Specification requires persisting the outcome of a completed game within a single, atomic transaction as detailed in sequence diagram 175.\n1. Begin a `SqliteTransaction`.\n2. Execute an `INSERT` statement into the `GameResult` table.\n3. For each participant in the `gameResult`, execute an `INSERT` into the `GameParticipant` table.\n4. For the human player, execute an `UPDATE` on their record in the `PlayerStatistic` table, incrementing `totalGamesPlayed` and `totalWins` as appropriate.\n5. Commit the transaction. If any step fails, the transaction must be rolled back.

####### 2.3.4.1.11.3.9 Exception Handling

The entire method body must be wrapped in a try/catch/finally block. The `catch` block must roll back the transaction and throw a `DataAccessLayerException`. The `finally` block must dispose of connection and transaction objects.

####### 2.3.4.1.11.3.10 Performance Considerations

N/A

####### 2.3.4.1.11.3.11 Validation Requirements

Assumes `gameResult` object is valid.

####### 2.3.4.1.11.3.12 Technology Integration Details

Uses `SqliteConnection.BeginTransaction()` and `SqliteTransaction.Commit()` / `Rollback()`.

###### 2.3.4.1.11.4.0 Method Name

####### 2.3.4.1.11.4.1 Method Name

GetPlayerStatsAsync

####### 2.3.4.1.11.4.2 Method Signature

Task<PlayerStatistic> GetPlayerStatsAsync(Guid profileId)

####### 2.3.4.1.11.4.3 Return Type

Task<PlayerStatistic>

####### 2.3.4.1.11.4.4 Access Modifier

public

####### 2.3.4.1.11.4.5 Is Async

✅ Yes

####### 2.3.4.1.11.4.6 Framework Specific Attributes

*No items available*

####### 2.3.4.1.11.4.7 Parameters

- {'parameter_name': 'profileId', 'parameter_type': 'Guid', 'is_nullable': False, 'purpose': 'The unique identifier of the player profile.', 'framework_attributes': []}

####### 2.3.4.1.11.4.8 Implementation Logic

Fulfills REQ-1-033. Specification added to satisfy the `exposed_contracts` definition. Retrieves the aggregate historical statistics for a single player.\n1. Execute a `SELECT` query on the `PlayerStatistic` table, filtering by `profileId`.\n2. Map the results from the `SqliteDataReader` to a `PlayerStatistic` domain object and return it. If not found, return null.

####### 2.3.4.1.11.4.9 Exception Handling

Standard database exception handling: log and wrap in `DataAccessLayerException`.

####### 2.3.4.1.11.4.10 Performance Considerations

The `profileId` column in `PlayerStatistic` is a foreign key and should be indexed.

####### 2.3.4.1.11.4.11 Validation Requirements

N/A

####### 2.3.4.1.11.4.12 Technology Integration Details

Uses `SqliteDataReader` to read the query results.

###### 2.3.4.1.11.5.0 Method Name

####### 2.3.4.1.11.5.1 Method Name

GetTopScoresAsync

####### 2.3.4.1.11.5.2 Method Signature

Task<List<TopScoreDto>> GetTopScoresAsync()

####### 2.3.4.1.11.5.3 Return Type

Task<List<TopScoreDto>>

####### 2.3.4.1.11.5.4 Access Modifier

public

####### 2.3.4.1.11.5.5 Is Async

✅ Yes

####### 2.3.4.1.11.5.6 Framework Specific Attributes

*No items available*

####### 2.3.4.1.11.5.7 Parameters

*No items available*

####### 2.3.4.1.11.5.8 Implementation Logic

Fulfills REQ-1-091. Specification requires retrieving the top 10 player scores as shown in sequence diagram 191.\n1. Execute a `SELECT` query on the `GameParticipant` table.\n2. The query must filter for human players (`isHuman = true`), `ORDER BY finalNetWorth DESC`, and `LIMIT 10`.\n3. Map the results from the `SqliteDataReader` into a list of `TopScoreDto` objects and return it.

####### 2.3.4.1.11.5.9 Exception Handling

Standard database exception handling: log and wrap in `DataAccessLayerException`.

####### 2.3.4.1.11.5.10 Performance Considerations

Requires a database index on the `GameParticipant` table's `finalNetWorth` column to ensure efficient sorting, as specified in `DatabaseSchema`.

####### 2.3.4.1.11.5.11 Validation Requirements

N/A

####### 2.3.4.1.11.5.12 Technology Integration Details

Uses `SqliteDataReader` to read the query results.

##### 2.3.4.1.12.0.0 Events

*No items available*

##### 2.3.4.1.13.0.0 Implementation Notes

This class will be registered as a singleton in the DI container because it manages the state and initialization of a single file resource.

#### 2.3.4.2.0.0.0 Class Name

##### 2.3.4.2.1.0.0 Class Name

ServiceCollectionExtensions

##### 2.3.4.2.2.0.0 File Path

/ServiceCollectionExtensions.cs

##### 2.3.4.2.3.0.0 Class Type

Static Helper

##### 2.3.4.2.4.0.0 Inheritance

N/A

##### 2.3.4.2.5.0.0 Purpose

Provides a clean, centralized extension method to register all services and configuration from this repository into the application's DI container.

##### 2.3.4.2.6.0.0 Dependencies

- Microsoft.Extensions.DependencyInjection.IServiceCollection
- Microsoft.Extensions.Configuration.IConfiguration

##### 2.3.4.2.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.2.8.0.0 Technology Integration Notes

Follows the standard convention for creating extensible DI registration for class libraries.

##### 2.3.4.2.9.0.0 Properties

*No items available*

##### 2.3.4.2.10.0.0 Methods

- {'method_name': 'AddStatisticsPersistence', 'method_signature': 'IServiceCollection AddStatisticsPersistence(this IServiceCollection services, IConfiguration configuration)', 'return_type': 'IServiceCollection', 'access_modifier': 'public static', 'is_async': False, 'framework_specific_attributes': [], 'parameters': [{'parameter_name': 'services', 'parameter_type': 'IServiceCollection', 'is_nullable': False, 'purpose': 'The DI service collection.', 'framework_attributes': []}, {'parameter_name': 'configuration', 'parameter_type': 'IConfiguration', 'is_nullable': False, 'purpose': "The application's configuration provider.", 'framework_attributes': []}], 'implementation_logic': '1. Must configure `StatisticsPersistenceOptions` by binding to the corresponding section in the `IConfiguration` object.\\n2. Must register `SqliteStatisticsRepository` as a singleton implementation for both `IStatisticsRepository` and `IPlayerProfileRepository` interfaces.', 'exception_handling': 'N/A', 'performance_considerations': 'N/A', 'validation_requirements': 'N/A', 'technology_integration_details': 'Uses `services.Configure<T>()` and `services.AddSingleton<TInterface, TImplementation>()`.'}

##### 2.3.4.2.11.0.0 Events

*No items available*

##### 2.3.4.2.12.0.0 Implementation Notes

This allows the main application to register this entire layer with a single line of code.

#### 2.3.4.3.0.0.0 Class Name

##### 2.3.4.3.1.0.0 Class Name

DataAccessLayerException

##### 2.3.4.3.2.0.0 File Path

/Exceptions/DataAccessLayerException.cs

##### 2.3.4.3.3.0.0 Class Type

Custom Exception

##### 2.3.4.3.4.0.0 Inheritance

System.Exception

##### 2.3.4.3.5.0.0 Purpose

To provide a generic, technology-agnostic exception type for wrapping underlying persistence errors (e.g., SqliteException), abstracting infrastructure details from the application layer.

##### 2.3.4.3.6.0.0 Dependencies

*No items available*

##### 2.3.4.3.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.3.8.0.0 Technology Integration Notes

Used in catch blocks within repository methods to wrap and re-throw database-specific exceptions.

##### 2.3.4.3.9.0.0 Properties

*No items available*

##### 2.3.4.3.10.0.0 Methods

- {'method_name': '.ctor', 'method_signature': 'DataAccessLayerException(string message, Exception innerException)', 'return_type': 'void', 'access_modifier': 'public', 'is_async': False, 'framework_specific_attributes': [], 'parameters': [], 'implementation_logic': 'Standard exception constructor that accepts a message and the original inner exception.', 'exception_handling': 'N/A', 'performance_considerations': 'N/A', 'validation_requirements': 'N/A', 'technology_integration_details': 'N/A'}

##### 2.3.4.3.11.0.0 Events

*No items available*

##### 2.3.4.3.12.0.0 Implementation Notes

Specification added to fill a gap in the original definition.

#### 2.3.4.4.0.0.0 Class Name

##### 2.3.4.4.1.0.0 Class Name

DataCorruptionException

##### 2.3.4.4.2.0.0 File Path

/Exceptions/DataCorruptionException.cs

##### 2.3.4.4.3.0.0 Class Type

Custom Exception

##### 2.3.4.4.4.0.0 Inheritance

DataAccessLayerException

##### 2.3.4.4.5.0.0 Purpose

A specific exception thrown only when the statistics database is found to be corrupt and cannot be automatically recovered from a backup, as per sequence diagram 176.

##### 2.3.4.4.6.0.0 Dependencies

*No items available*

##### 2.3.4.4.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.4.8.0.0 Technology Integration Notes

Allows the application layer to specifically catch this unrecoverable error and present appropriate options to the user (e.g., reset data or exit).

##### 2.3.4.4.9.0.0 Properties

*No items available*

##### 2.3.4.4.10.0.0 Methods

- {'method_name': '.ctor', 'method_signature': 'DataCorruptionException(string message, Exception innerException)', 'return_type': 'void', 'access_modifier': 'public', 'is_async': False, 'framework_specific_attributes': [], 'parameters': [], 'implementation_logic': 'Standard exception constructor.', 'exception_handling': 'N/A', 'performance_considerations': 'N/A', 'validation_requirements': 'N/A', 'technology_integration_details': 'N/A'}

##### 2.3.4.4.11.0.0 Events

*No items available*

##### 2.3.4.4.12.0.0 Implementation Notes

Specification added to fill a gap in the original definition.

### 2.3.5.0.0.0.0 Interface Specifications

*No items available*

### 2.3.6.0.0.0.0 Enum Specifications

*No items available*

### 2.3.7.0.0.0.0 Dto Specifications

- {'dto_name': 'DatabaseSchema', 'file_path': '/Schema/DatabaseSchema.cs', 'purpose': 'Provides a static, centralized definition of the SQL DDL statements required to create the entire database schema. This specification acts as a single source of truth for the database structure, ensuring it aligns with the `Monopoly Tycoon Player Data ERD`.', 'framework_base_class': 'static class', 'validation_notes': 'Validation confirms this specification accurately reflects the tables (`PlayerProfile`, `PlayerStatistic`, `GameResult`, `GameParticipant`) and constraints (PK, FK, UNIQUE) in ERD (id: 31).', 'properties': [{'property_name': 'CreatePlayerProfileTable', 'property_type': 'const string', 'validation_attributes': [], 'serialization_attributes': [], 'framework_specific_attributes': ['Specifies the `CREATE TABLE PlayerProfile` SQL statement. Must include columns for `profileId` (PK), `displayName` (UNIQUE), `createdAt`, `updatedAt`.']}, {'property_name': 'CreatePlayerStatisticTable', 'property_type': 'const string', 'validation_attributes': [], 'serialization_attributes': [], 'framework_specific_attributes': ['Specifies the `CREATE TABLE PlayerStatistic` SQL statement. Must include columns for `playerStatisticId` (PK), `profileId` (FK), `totalGamesPlayed`, `totalWins`.']}, {'property_name': 'CreateGameResultTable', 'property_type': 'const string', 'validation_attributes': [], 'serialization_attributes': [], 'framework_specific_attributes': ['Specifies the `CREATE TABLE GameResult` SQL statement. Must include columns for `gameResultId` (PK), `profileId` (FK to human player), `didHumanWin`, `gameDurationSeconds`, `endTimestamp`.']}, {'property_name': 'CreateGameParticipantTable', 'property_type': 'const string', 'validation_attributes': [], 'serialization_attributes': [], 'framework_specific_attributes': ['Specifies the `CREATE TABLE GameParticipant` SQL statement. Must include columns for `gameParticipantId` (PK), `gameResultId` (FK), `participantName`, `isHuman`, `finalNetWorth`.']}, {'property_name': 'CreateIndexes', 'property_type': 'const string[]', 'validation_attributes': [], 'serialization_attributes': [], 'framework_specific_attributes': ['Specifies all `CREATE INDEX` statements. Must include an index on `GameParticipant(finalNetWorth)` to optimize top score queries for REQ-1-091.']}], 'validation_rules': 'The SQL defined herein must be compliant with SQLite syntax.', 'serialization_requirements': 'N/A'}

### 2.3.8.0.0.0.0 Configuration Specifications

- {'configuration_name': 'StatisticsPersistenceOptions', 'file_path': '/Configuration/StatisticsPersistenceOptions.cs', 'purpose': 'Defines the configuration settings for this repository, to be loaded from `appsettings.json` via the Options Pattern.', 'framework_base_class': 'POCO', 'configuration_sections': [{'section_name': 'Persistence:Statistics', 'properties': [{'property_name': 'DatabaseFilePath', 'property_type': 'string', 'default_value': 'Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), \\"MonopolyTycoon\\", \\"stats.db\\")', 'required': True, 'description': 'The full path to the SQLite database file.'}, {'property_name': 'BackupDirectoryPath', 'property_type': 'string', 'default_value': 'Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), \\"MonopolyTycoon\\", \\"backups\\")', 'required': True, 'description': 'The path to the directory where database backups will be stored.'}, {'property_name': 'BackupRetentionCount', 'property_type': 'int', 'default_value': '3', 'required': True, 'description': 'The number of recent database backups to retain, as per REQ-1-089.'}]}], 'validation_requirements': 'File and directory paths must be valid for the target operating system. `BackupRetentionCount` must be a positive integer.'}

### 2.3.9.0.0.0.0 Dependency Injection Specifications

#### 2.3.9.1.0.0.0 Service Interface

##### 2.3.9.1.1.0.0 Service Interface

IStatisticsRepository

##### 2.3.9.1.2.0.0 Service Implementation

SqliteStatisticsRepository

##### 2.3.9.1.3.0.0 Lifetime

Singleton

##### 2.3.9.1.4.0.0 Registration Reasoning

A singleton lifetime is appropriate because the repository manages access to a single file resource (`stats.db`). This ensures that initialization and connection management are handled consistently throughout the application's lifetime.

##### 2.3.9.1.5.0.0 Framework Registration Pattern

services.AddSingleton<IStatisticsRepository, SqliteStatisticsRepository>(); (Handled inside `AddStatisticsPersistence`)

#### 2.3.9.2.0.0.0 Service Interface

##### 2.3.9.2.1.0.0 Service Interface

IPlayerProfileRepository

##### 2.3.9.2.2.0.0 Service Implementation

SqliteStatisticsRepository

##### 2.3.9.2.3.0.0 Lifetime

Singleton

##### 2.3.9.2.4.0.0 Registration Reasoning

Registered as the same singleton instance as `IStatisticsRepository` since they are implemented by the same class managing the same database file.

##### 2.3.9.2.5.0.0 Framework Registration Pattern

services.AddSingleton<IPlayerProfileRepository>(sp => sp.GetRequiredService<IStatisticsRepository>() as IPlayerProfileRepository);

#### 2.3.9.3.0.0.0 Service Interface

##### 2.3.9.3.1.0.0 Service Interface

IOptions<StatisticsPersistenceOptions>

##### 2.3.9.3.2.0.0 Service Implementation

N/A (Framework Provided)

##### 2.3.9.3.3.0.0 Lifetime

Singleton

##### 2.3.9.3.4.0.0 Registration Reasoning

The options framework is used to configure the repository from external sources like `appsettings.json`.

##### 2.3.9.3.5.0.0 Framework Registration Pattern

services.Configure<StatisticsPersistenceOptions>(configuration.GetSection(\"Persistence:Statistics\"));

### 2.3.10.0.0.0.0 External Integration Specifications

#### 2.3.10.1.0.0.0 Integration Target

##### 2.3.10.1.1.0.0 Integration Target

Local File System (SQLite Database)

##### 2.3.10.1.2.0.0 Integration Type

Data Persistence

##### 2.3.10.1.3.0.0 Required Client Classes

- Microsoft.Data.Sqlite.SqliteConnection
- Microsoft.Data.Sqlite.SqliteCommand
- Microsoft.Data.Sqlite.SqliteDataReader
- Microsoft.Data.Sqlite.SqliteTransaction

##### 2.3.10.1.4.0.0 Configuration Requirements

A valid file path for the database file, provided via `StatisticsPersistenceOptions`.

##### 2.3.10.1.5.0.0 Error Handling Requirements

Must handle `SqliteException` for issues like file corruption, constraint violations, and other database errors. Must wrap provider-specific exceptions in generic `DataAccessLayerException` and `DataCorruptionException`.

##### 2.3.10.1.6.0.0 Authentication Requirements

N/A (Local file)

##### 2.3.10.1.7.0.0 Framework Integration Patterns

Direct ADO.NET provider usage within a class implementing the Repository Pattern.

#### 2.3.10.2.0.0.0 Integration Target

##### 2.3.10.2.1.0.0 Integration Target

Local File System (Backups)

##### 2.3.10.2.2.0.0 Integration Type

File I/O

##### 2.3.10.2.3.0.0 Required Client Classes

- System.IO.File
- System.IO.Directory
- System.IO.Path

##### 2.3.10.2.4.0.0 Configuration Requirements

A valid directory path for storing backups, provided via `StatisticsPersistenceOptions`.

##### 2.3.10.2.5.0.0 Error Handling Requirements

Must handle `IOException` and related exceptions during file copy, move, and delete operations. Failures must be logged with high severity. File operations for backup and restore must be atomic to prevent corruption.

##### 2.3.10.2.6.0.0 Authentication Requirements

N/A (Local file system permissions apply)

##### 2.3.10.2.7.0.0 Framework Integration Patterns

Standard `System.IO` API calls for file manipulation.

## 2.4.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 4 |
| Total Interfaces | 0 |
| Total Enums | 0 |
| Total Dtos | 1 |
| Total Configurations | 1 |
| Total External Integrations | 2 |
| Grand Total Components | 8 |
| Phase 2 Claimed Count | 4 |
| Phase 2 Actual Count | 4 |
| Validation Added Count | 4 |
| Final Validated Count | 8 |

