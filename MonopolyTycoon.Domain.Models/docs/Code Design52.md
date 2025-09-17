# 1 Design

code_design

# 2 Code Specification

## 2.1 Validation Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-DM-001 |
| Validation Timestamp | 2024-05-20T11:00:00Z |
| Original Component Count Claimed | 10 |
| Original Component Count Actual | 10 |
| Gaps Identified Count | 0 |
| Components Added Count | 0 |
| Final Component Count | 10 |
| Validation Completeness Score | 100.0 |
| Enhancement Methodology | Systematic, cross-contextual validation of specifi... |

## 2.2 Validation Summary

### 2.2.1 Repository Scope Validation

#### 2.2.1.1 Scope Compliance

Fully compliant. The specification strictly adheres to the repository's role as a dependency-free data model library, correctly excluding all business logic and external dependencies as mandated by the scope boundaries.

#### 2.2.1.2 Gaps Identified

*No items available*

#### 2.2.1.3 Components Added

*No items available*

### 2.2.2.0 Requirements Coverage Validation

#### 2.2.2.1 Functional Requirements Coverage

100%

#### 2.2.2.2 Non Functional Requirements Coverage

100%

#### 2.2.2.3 Missing Requirement Components

*No items available*

#### 2.2.2.4 Added Requirement Components

*No items available*

#### 2.2.2.5 Validation Notes

Specification for \"GameState\" and \"PlayerState\" models provide complete coverage for REQ-1-041 and REQ-1-031, respectively. The data structures are sufficient to meet all validation criteria.

### 2.2.3.0 Architectural Pattern Validation

#### 2.2.3.1 Pattern Implementation Completeness

The specification correctly implements all designated architectural patterns. The use of C# \"record\" types is an exemplary implementation of the Immutable Object and DTO patterns. Its role as a dependency-free library perfectly realizes the Shared Kernel concept.

#### 2.2.3.2 Missing Pattern Components

*No items available*

#### 2.2.3.3 Added Pattern Components

*No items available*

### 2.2.4.0 Database Mapping Validation

#### 2.2.4.1 Entity Mapping Completeness

100% complete. The \"GameState\" model and its constituent parts provide a perfect 1:1 mapping to the \"Game State Save File Structure\" JSON document schema. All fields, types, and structures align.

#### 2.2.4.2 Missing Database Components

*No items available*

#### 2.2.4.3 Added Database Components

*No items available*

### 2.2.5.0 Sequence Interaction Validation

#### 2.2.5.1 Interaction Implementation Completeness

Fully compliant. The specified data models serve as the precise data payloads required for all relevant sequence diagrams, such as game saving (ID 187) and loading (ID 188). The specification enables the contracts for those interactions.

#### 2.2.5.2 Missing Interaction Components

*No items available*

#### 2.2.5.3 Added Interaction Components

*No items available*

## 2.3.0.0 Enhanced Specification

### 2.3.1.0 Specification Metadata

| Property | Value |
|----------|-------|
| Repository Id | REPO-DM-001 |
| Technology Stack | .NET 8, C# 12 |
| Technology Guidance Integration | Utilizes C# \"record\" types for immutability, \"i... |
| Framework Compliance Score | 100.0 |
| Specification Completeness | 100.0% |
| Component Count | 10 |
| Specification Methodology | Domain-Driven Design (Data Model aspect), creating... |

### 2.3.2.0 Technology Framework Integration

#### 2.3.2.1 Framework Patterns Applied

- POCO (Plain Old C# Object)
- Immutable Objects (via C# records)
- Data Transfer Object (DTO)
- Shared Kernel

#### 2.3.2.2 Directory Structure Source

.NET Class Library standard conventions, with namespace-driven folder organization.

#### 2.3.2.3 Naming Conventions Source

Microsoft C# coding standards (PascalCase for types and properties).

#### 2.3.2.4 Architectural Patterns Source

Layered Architecture, with this repository providing foundational models for the Business Logic Layer.

#### 2.3.2.5 Performance Optimizations Applied

- Designed for efficient serialization/deserialization with System.Text.Json.
- Use of \"record\" types which provide efficient, value-based equality comparisons and can reduce allocation overhead.

### 2.3.3.0 File Structure

#### 2.3.3.1 Directory Organization

##### 2.3.3.1.1 Directory Path

###### 2.3.3.1.1.1 Directory Path

/

###### 2.3.3.1.1.2 Purpose

Root directory for the project, containing the project file and solution-wide configuration.

###### 2.3.3.1.1.3 Contains Files

- MonopolyTycoon.Domain.Models.csproj
- Directory.Build.props
- .editorconfig
- MonopolyTycoon.sln
- global.json
- README.md
- .gitignore
- .gitattributes

###### 2.3.3.1.1.4 Organizational Reasoning

Standard .NET project structure. Centralizes project definition and build/style configuration for consistency.

###### 2.3.3.1.1.5 Framework Convention Alignment

Follows standard .NET solution structure and MSBuild conventions.

##### 2.3.3.1.2.0 Directory Path

###### 2.3.3.1.2.1 Directory Path

.github/workflows

###### 2.3.3.1.2.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.2.3 Contains Files

- dotnet-build.yml

###### 2.3.3.1.2.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.2.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

##### 2.3.3.1.3.0 Directory Path

###### 2.3.3.1.3.1 Directory Path

GameStateModels/

###### 2.3.3.1.3.2 Purpose

Contains the \"GameState\" aggregate root and all its constituent data models.

###### 2.3.3.1.3.3 Contains Files

- GameState.cs
- PlayerState.cs
- BoardState.cs
- BankState.cs
- DeckStates.cs
- GameMetadata.cs
- PropertyState.cs

###### 2.3.3.1.3.4 Organizational Reasoning

Groups all related components of the game state aggregate, mapping directly to the \"MonopolyTycoon.Domain.Models.GameStateModels\" namespace.

###### 2.3.3.1.3.5 Framework Convention Alignment

Adheres to namespace-per-folder convention in .NET.

##### 2.3.3.1.4.0 Directory Path

###### 2.3.3.1.4.1 Directory Path

src/MonopolyTycoon.Domain.Models

###### 2.3.3.1.4.2 Purpose

Infrastructure and project configuration files

###### 2.3.3.1.4.3 Contains Files

- MonopolyTycoon.Domain.Models.csproj

###### 2.3.3.1.4.4 Organizational Reasoning

Contains project setup, configuration, and infrastructure files for development and deployment

###### 2.3.3.1.4.5 Framework Convention Alignment

Standard project structure for infrastructure as code and development tooling

#### 2.3.3.2.0.0 Namespace Strategy

| Property | Value |
|----------|-------|
| Root Namespace | MonopolyTycoon.Domain.Models |
| Namespace Organization | Hierarchical by domain aggregate. All game state m... |
| Naming Conventions | PascalCase for all namespaces, types, and public m... |
| Framework Alignment | Follows Microsoft's recommended namespace design g... |

### 2.3.4.0.0.0 Class Specifications

#### 2.3.4.1.0.0 Class Name

##### 2.3.4.1.1.0 Class Name

GameState

##### 2.3.4.1.2.0 File Path

GameStateModels/GameState.cs

##### 2.3.4.1.3.0 Class Type

Record

##### 2.3.4.1.4.0 Inheritance

None

##### 2.3.4.1.5.0 Purpose

Represents the comprehensive, serializable state of a single game session. Acts as the aggregate root for all game data, directly fulfilling REQ-1-041 and mapping 1:1 with the \"GameState Save File Structure\" design.

##### 2.3.4.1.6.0 Dependencies

*No items available*

##### 2.3.4.1.7.0 Framework Specific Attributes

*No items available*

##### 2.3.4.1.8.0 Technology Integration Notes

Specified as an immutable \"record\" to ensure state consistency and thread safety. Designed for efficient serialization to JSON for game save files.

##### 2.3.4.1.9.0 Validation Notes

Specification validated as complete and compliant with architectural principles. It correctly aggregates all sub-state models.

##### 2.3.4.1.10.0 Properties

###### 2.3.4.1.10.1 Property Name

####### 2.3.4.1.10.1.1 Property Name

GameStateId

####### 2.3.4.1.10.1.2 Property Type

Guid

####### 2.3.4.1.10.1.3 Access Modifier

public

####### 2.3.4.1.10.1.4 Purpose

Unique identifier for the game state instance. Corresponds to \"SavedGame.savedGameId\" in the relational schema.

####### 2.3.4.1.10.1.5 Validation Attributes

*No items available*

####### 2.3.4.1.10.1.6 Framework Specific Configuration

[JsonPropertyName(\"gameStateId\")]

####### 2.3.4.1.10.1.7 Implementation Notes

Property must be defined in the primary constructor with an \"init\" accessor to ensure immutability.

###### 2.3.4.1.10.2.0 Property Name

####### 2.3.4.1.10.2.1 Property Name

GameVersion

####### 2.3.4.1.10.2.2 Property Type

string

####### 2.3.4.1.10.2.3 Access Modifier

public

####### 2.3.4.1.10.2.4 Purpose

The application version that created this state, crucial for data migration logic as per REQ-1-090.

####### 2.3.4.1.10.2.5 Validation Attributes

*No items available*

####### 2.3.4.1.10.2.6 Framework Specific Configuration

[JsonPropertyName(\"gameVersion\")]

####### 2.3.4.1.10.2.7 Implementation Notes

Property must be defined in the primary constructor with an \"init\" accessor.

###### 2.3.4.1.10.3.0 Property Name

####### 2.3.4.1.10.3.1 Property Name

PlayerStates

####### 2.3.4.1.10.3.2 Property Type

IReadOnlyList<PlayerState>

####### 2.3.4.1.10.3.3 Access Modifier

public

####### 2.3.4.1.10.3.4 Purpose

An immutable list containing the state of every player in the game, fulfilling a key part of REQ-1-041.

####### 2.3.4.1.10.3.5 Validation Attributes

*No items available*

####### 2.3.4.1.10.3.6 Framework Specific Configuration

[JsonPropertyName(\"playerStates\")]

####### 2.3.4.1.10.3.7 Implementation Notes

Specification mandates IReadOnlyList<T> to prevent modification of the collection after initialization, enforcing the immutability contract.

###### 2.3.4.1.10.4.0 Property Name

####### 2.3.4.1.10.4.1 Property Name

BoardState

####### 2.3.4.1.10.4.2 Property Type

BoardState

####### 2.3.4.1.10.4.3 Access Modifier

public

####### 2.3.4.1.10.4.4 Purpose

Encapsulates the state of all properties on the game board.

####### 2.3.4.1.10.4.5 Validation Attributes

*No items available*

####### 2.3.4.1.10.4.6 Framework Specific Configuration

[JsonPropertyName(\"boardState\")]

####### 2.3.4.1.10.4.7 Implementation Notes

Property must be defined in the primary constructor with an \"init\" accessor.

###### 2.3.4.1.10.5.0 Property Name

####### 2.3.4.1.10.5.1 Property Name

BankState

####### 2.3.4.1.10.5.2 Property Type

BankState

####### 2.3.4.1.10.5.3 Access Modifier

public

####### 2.3.4.1.10.5.4 Purpose

Represents the state of the game's bank, such as available houses and hotels.

####### 2.3.4.1.10.5.5 Validation Attributes

*No items available*

####### 2.3.4.1.10.5.6 Framework Specific Configuration

[JsonPropertyName(\"bankState\")]

####### 2.3.4.1.10.5.7 Implementation Notes

Property must be defined in the primary constructor with an \"init\" accessor.

###### 2.3.4.1.10.6.0 Property Name

####### 2.3.4.1.10.6.1 Property Name

DeckStates

####### 2.3.4.1.10.6.2 Property Type

DeckStates

####### 2.3.4.1.10.6.3 Access Modifier

public

####### 2.3.4.1.10.6.4 Purpose

Represents the current order of cards in the Chance and Community Chest decks.

####### 2.3.4.1.10.6.5 Validation Attributes

*No items available*

####### 2.3.4.1.10.6.6 Framework Specific Configuration

[JsonPropertyName(\"deckStates\")]

####### 2.3.4.1.10.6.7 Implementation Notes

Property must be defined in the primary constructor with an \"init\" accessor.

###### 2.3.4.1.10.7.0 Property Name

####### 2.3.4.1.10.7.1 Property Name

GameMetadata

####### 2.3.4.1.10.7.2 Property Type

GameMetadata

####### 2.3.4.1.10.7.3 Access Modifier

public

####### 2.3.4.1.10.7.4 Purpose

Contains metadata about the game session, such as the current turn number and active player.

####### 2.3.4.1.10.7.5 Validation Attributes

*No items available*

####### 2.3.4.1.10.7.6 Framework Specific Configuration

[JsonPropertyName(\"gameMetadata\")]

####### 2.3.4.1.10.7.7 Implementation Notes

Property must be defined in the primary constructor with an \"init\" accessor.

##### 2.3.4.1.11.0.0 Methods

*No items available*

##### 2.3.4.1.12.0.0 Events

*No items available*

##### 2.3.4.1.13.0.0 Implementation Notes

This record must be a pure data container with no business logic, methods, or dependencies, adhering strictly to the repository's \"must_not_implement\" scope boundary.

#### 2.3.4.2.0.0.0 Class Name

##### 2.3.4.2.1.0.0 Class Name

PlayerState

##### 2.3.4.2.2.0.0 File Path

GameStateModels/PlayerState.cs

##### 2.3.4.2.3.0.0 Class Type

Record

##### 2.3.4.2.4.0.0 Inheritance

None

##### 2.3.4.2.5.0.0 Purpose

Represents the detailed state of a single player, including identity, cash, and status, directly fulfilling REQ-1-031.

##### 2.3.4.2.6.0.0 Dependencies

*No items available*

##### 2.3.4.2.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.2.8.0.0 Technology Integration Notes

Specified as an immutable \"record\" for predictable state management. It is a fundamental component of the GameState aggregate.

##### 2.3.4.2.9.0.0 Validation Notes

Specification validated as containing all necessary fields for player state tracking as per REQ-1-031.

##### 2.3.4.2.10.0.0 Properties

###### 2.3.4.2.10.1.0 Property Name

####### 2.3.4.2.10.1.1 Property Name

PlayerId

####### 2.3.4.2.10.1.2 Property Type

Guid

####### 2.3.4.2.10.1.3 Access Modifier

public

####### 2.3.4.2.10.1.4 Purpose

Unique identifier for the player.

####### 2.3.4.2.10.1.5 Validation Attributes

*No items available*

####### 2.3.4.2.10.1.6 Framework Specific Configuration

[JsonPropertyName(\"playerId\")]

####### 2.3.4.2.10.1.7 Implementation Notes

Must be defined in the primary constructor.

###### 2.3.4.2.10.2.0 Property Name

####### 2.3.4.2.10.2.1 Property Name

PlayerName

####### 2.3.4.2.10.2.2 Property Type

string

####### 2.3.4.2.10.2.3 Access Modifier

public

####### 2.3.4.2.10.2.4 Purpose

The display name of the player.

####### 2.3.4.2.10.2.5 Validation Attributes

*No items available*

####### 2.3.4.2.10.2.6 Framework Specific Configuration

[JsonPropertyName(\"playerName\")]

####### 2.3.4.2.10.2.7 Implementation Notes

Must be defined in the primary constructor.

###### 2.3.4.2.10.3.0 Property Name

####### 2.3.4.2.10.3.1 Property Name

IsHuman

####### 2.3.4.2.10.3.2 Property Type

bool

####### 2.3.4.2.10.3.3 Access Modifier

public

####### 2.3.4.2.10.3.4 Purpose

Flag indicating if the player is controlled by a human or AI.

####### 2.3.4.2.10.3.5 Validation Attributes

*No items available*

####### 2.3.4.2.10.3.6 Framework Specific Configuration

[JsonPropertyName(\"isHuman\")]

####### 2.3.4.2.10.3.7 Implementation Notes

Must be defined in the primary constructor.

###### 2.3.4.2.10.4.0 Property Name

####### 2.3.4.2.10.4.1 Property Name

Cash

####### 2.3.4.2.10.4.2 Property Type

int

####### 2.3.4.2.10.4.3 Access Modifier

public

####### 2.3.4.2.10.4.4 Purpose

The player's current cash balance.

####### 2.3.4.2.10.4.5 Validation Attributes

*No items available*

####### 2.3.4.2.10.4.6 Framework Specific Configuration

[JsonPropertyName(\"cash\")]

####### 2.3.4.2.10.4.7 Implementation Notes

Must be defined in the primary constructor.

###### 2.3.4.2.10.5.0 Property Name

####### 2.3.4.2.10.5.1 Property Name

Position

####### 2.3.4.2.10.5.2 Property Type

int

####### 2.3.4.2.10.5.3 Access Modifier

public

####### 2.3.4.2.10.5.4 Purpose

The player's current position on the board, represented by a tile index (0-39).

####### 2.3.4.2.10.5.5 Validation Attributes

*No items available*

####### 2.3.4.2.10.5.6 Framework Specific Configuration

[JsonPropertyName(\"position\")]

####### 2.3.4.2.10.5.7 Implementation Notes

Must be defined in the primary constructor.

###### 2.3.4.2.10.6.0 Property Name

####### 2.3.4.2.10.6.1 Property Name

InJail

####### 2.3.4.2.10.6.2 Property Type

bool

####### 2.3.4.2.10.6.3 Access Modifier

public

####### 2.3.4.2.10.6.4 Purpose

Flag indicating if the player is currently in jail.

####### 2.3.4.2.10.6.5 Validation Attributes

*No items available*

####### 2.3.4.2.10.6.6 Framework Specific Configuration

[JsonPropertyName(\"inJail\")]

####### 2.3.4.2.10.6.7 Implementation Notes

Must be defined in the primary constructor.

###### 2.3.4.2.10.7.0 Property Name

####### 2.3.4.2.10.7.1 Property Name

TurnsInJail

####### 2.3.4.2.10.7.2 Property Type

int

####### 2.3.4.2.10.7.3 Access Modifier

public

####### 2.3.4.2.10.7.4 Purpose

The number of turns the player has spent in jail during the current stint.

####### 2.3.4.2.10.7.5 Validation Attributes

*No items available*

####### 2.3.4.2.10.7.6 Framework Specific Configuration

[JsonPropertyName(\"turnsInJail\")]

####### 2.3.4.2.10.7.7 Implementation Notes

Must be defined in the primary constructor.

##### 2.3.4.2.11.0.0 Methods

*No items available*

##### 2.3.4.2.12.0.0 Events

*No items available*

##### 2.3.4.2.13.0.0 Implementation Notes

This record must remain a pure data container. All logic related to player actions will be handled by the domain's rule engine.

#### 2.3.4.3.0.0.0 Class Name

##### 2.3.4.3.1.0.0 Class Name

BoardState

##### 2.3.4.3.2.0.0 File Path

GameStateModels/BoardState.cs

##### 2.3.4.3.3.0.0 Class Type

Record

##### 2.3.4.3.4.0.0 Inheritance

None

##### 2.3.4.3.5.0.0 Purpose

Represents the state of all ownable properties on the game board.

##### 2.3.4.3.6.0.0 Dependencies

*No items available*

##### 2.3.4.3.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.3.8.0.0 Technology Integration Notes

Uses a dictionary for efficient lookup of property states by a unique identifier.

##### 2.3.4.3.9.0.0 Validation Notes

Specification validated. The use of a dictionary is an efficient data structure choice for this purpose.

##### 2.3.4.3.10.0.0 Properties

- {'property_name': 'Properties', 'property_type': 'IReadOnlyDictionary<int, PropertyState>', 'access_modifier': 'public', 'purpose': "An immutable dictionary mapping a property's tile index to its current state.", 'validation_attributes': [], 'framework_specific_configuration': '[JsonPropertyName(\\"properties\\")]', 'implementation_notes': 'The key (int) represents the board tile index (0-39). The value contains the dynamic state of that property. IReadOnlyDictionary is specified for immutability.'}

##### 2.3.4.3.11.0.0 Methods

*No items available*

##### 2.3.4.3.12.0.0 Events

*No items available*

##### 2.3.4.3.13.0.0 Implementation Notes

This model is purely for data representation.

#### 2.3.4.4.0.0.0 Class Name

##### 2.3.4.4.1.0.0 Class Name

PropertyState

##### 2.3.4.4.2.0.0 File Path

GameStateModels/PropertyState.cs

##### 2.3.4.4.3.0.0 Class Type

Record

##### 2.3.4.4.4.0.0 Inheritance

None

##### 2.3.4.4.5.0.0 Purpose

Represents the dynamic state of a single ownable property, such as ownership and development level.

##### 2.3.4.4.6.0.0 Dependencies

*No items available*

##### 2.3.4.4.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.4.8.0.0 Technology Integration Notes

A simple, immutable data record.

##### 2.3.4.4.9.0.0 Validation Notes

Specification validated. Correctly models individual property states, including the important nullable OwnerId.

##### 2.3.4.4.10.0.0 Properties

###### 2.3.4.4.10.1.0 Property Name

####### 2.3.4.4.10.1.1 Property Name

OwnerId

####### 2.3.4.4.10.1.2 Property Type

Guid?

####### 2.3.4.4.10.1.3 Access Modifier

public

####### 2.3.4.4.10.1.4 Purpose

The PlayerId of the owner. A null value indicates the property is unowned.

####### 2.3.4.4.10.1.5 Validation Attributes

*No items available*

####### 2.3.4.4.10.1.6 Framework Specific Configuration

[JsonPropertyName(\"ownerId\")]

####### 2.3.4.4.10.1.7 Implementation Notes

Specification mandates a nullable Guid to correctly represent the absence of an owner.

###### 2.3.4.4.10.2.0 Property Name

####### 2.3.4.4.10.2.1 Property Name

HouseCount

####### 2.3.4.4.10.2.2 Property Type

int

####### 2.3.4.4.10.2.3 Access Modifier

public

####### 2.3.4.4.10.2.4 Purpose

The number of houses on the property. A value of 5 represents a hotel.

####### 2.3.4.4.10.2.5 Validation Attributes

*No items available*

####### 2.3.4.4.10.2.6 Framework Specific Configuration

[JsonPropertyName(\"houseCount\")]

####### 2.3.4.4.10.2.7 Implementation Notes

Domain logic in other layers will enforce that this value is between 0 and 5, inclusive.

###### 2.3.4.4.10.3.0 Property Name

####### 2.3.4.4.10.3.1 Property Name

IsMortgaged

####### 2.3.4.4.10.3.2 Property Type

bool

####### 2.3.4.4.10.3.3 Access Modifier

public

####### 2.3.4.4.10.3.4 Purpose

Flag indicating if the property is currently mortgaged.

####### 2.3.4.4.10.3.5 Validation Attributes

*No items available*

####### 2.3.4.4.10.3.6 Framework Specific Configuration

[JsonPropertyName(\"isMortgaged\")]

####### 2.3.4.4.10.3.7 Implementation Notes

Must be defined in the primary constructor.

##### 2.3.4.4.11.0.0 Methods

*No items available*

##### 2.3.4.4.12.0.0 Events

*No items available*

##### 2.3.4.4.13.0.0 Implementation Notes

Pure data model with no associated logic.

#### 2.3.4.5.0.0.0 Class Name

##### 2.3.4.5.1.0.0 Class Name

BankState

##### 2.3.4.5.2.0.0 File Path

GameStateModels/BankState.cs

##### 2.3.4.5.3.0.0 Class Type

Record

##### 2.3.4.5.4.0.0 Inheritance

None

##### 2.3.4.5.5.0.0 Purpose

Represents the number of houses and hotels available for purchase from the bank.

##### 2.3.4.5.6.0.0 Dependencies

*No items available*

##### 2.3.4.5.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.5.8.0.0 Technology Integration Notes

A simple, immutable data record.

##### 2.3.4.5.9.0.0 Validation Notes

Specification validated as complete for its purpose.

##### 2.3.4.5.10.0.0 Properties

###### 2.3.4.5.10.1.0 Property Name

####### 2.3.4.5.10.1.1 Property Name

HousesRemaining

####### 2.3.4.5.10.1.2 Property Type

int

####### 2.3.4.5.10.1.3 Access Modifier

public

####### 2.3.4.5.10.1.4 Purpose

The number of house tokens remaining in the bank.

####### 2.3.4.5.10.1.5 Validation Attributes

*No items available*

####### 2.3.4.5.10.1.6 Framework Specific Configuration

[JsonPropertyName(\"housesRemaining\")]

####### 2.3.4.5.10.1.7 Implementation Notes

Must be defined in the primary constructor.

###### 2.3.4.5.10.2.0 Property Name

####### 2.3.4.5.10.2.1 Property Name

HotelsRemaining

####### 2.3.4.5.10.2.2 Property Type

int

####### 2.3.4.5.10.2.3 Access Modifier

public

####### 2.3.4.5.10.2.4 Purpose

The number of hotel tokens remaining in the bank.

####### 2.3.4.5.10.2.5 Validation Attributes

*No items available*

####### 2.3.4.5.10.2.6 Framework Specific Configuration

[JsonPropertyName(\"hotelsRemaining\")]

####### 2.3.4.5.10.2.7 Implementation Notes

Must be defined in the primary constructor.

##### 2.3.4.5.11.0.0 Methods

*No items available*

##### 2.3.4.5.12.0.0 Events

*No items available*

##### 2.3.4.5.13.0.0 Implementation Notes

Pure data model with no associated logic.

#### 2.3.4.6.0.0.0 Class Name

##### 2.3.4.6.1.0.0 Class Name

DeckStates

##### 2.3.4.6.2.0.0 File Path

GameStateModels/DeckStates.cs

##### 2.3.4.6.3.0.0 Class Type

Record

##### 2.3.4.6.4.0.0 Inheritance

None

##### 2.3.4.6.5.0.0 Purpose

Represents the current shuffled order of the Chance and Community Chest card decks.

##### 2.3.4.6.6.0.0 Dependencies

*No items available*

##### 2.3.4.6.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.6.8.0.0 Technology Integration Notes

Uses lists of integers to represent the sequence of card IDs, ensuring the shuffled state can be saved and loaded perfectly.

##### 2.3.4.6.9.0.0 Validation Notes

Specification validated. Storing card order by ID is a robust method for preserving deck state.

##### 2.3.4.6.10.0.0 Properties

###### 2.3.4.6.10.1.0 Property Name

####### 2.3.4.6.10.1.1 Property Name

ChanceCardOrder

####### 2.3.4.6.10.1.2 Property Type

IReadOnlyList<int>

####### 2.3.4.6.10.1.3 Access Modifier

public

####### 2.3.4.6.10.1.4 Purpose

An immutable list of card IDs representing the current order of the Chance deck.

####### 2.3.4.6.10.1.5 Validation Attributes

*No items available*

####### 2.3.4.6.10.1.6 Framework Specific Configuration

[JsonPropertyName(\"chanceCardOrder\")]

####### 2.3.4.6.10.1.7 Implementation Notes

The integer value corresponds to a predefined card definition managed by the Business Logic layer.

###### 2.3.4.6.10.2.0 Property Name

####### 2.3.4.6.10.2.1 Property Name

CommunityChestCardOrder

####### 2.3.4.6.10.2.2 Property Type

IReadOnlyList<int>

####### 2.3.4.6.10.2.3 Access Modifier

public

####### 2.3.4.6.10.2.4 Purpose

An immutable list of card IDs representing the current order of the Community Chest deck.

####### 2.3.4.6.10.2.5 Validation Attributes

*No items available*

####### 2.3.4.6.10.2.6 Framework Specific Configuration

[JsonPropertyName(\"communityChestCardOrder\")]

####### 2.3.4.6.10.2.7 Implementation Notes

The integer value corresponds to a predefined card definition managed by the Business Logic layer.

##### 2.3.4.6.11.0.0 Methods

*No items available*

##### 2.3.4.6.12.0.0 Events

*No items available*

##### 2.3.4.6.13.0.0 Implementation Notes

Pure data model with no associated logic.

#### 2.3.4.7.0.0.0 Class Name

##### 2.3.4.7.1.0.0 Class Name

GameMetadata

##### 2.3.4.7.2.0.0 File Path

GameStateModels/GameMetadata.cs

##### 2.3.4.7.3.0.0 Class Type

Record

##### 2.3.4.7.4.0.0 Inheritance

None

##### 2.3.4.7.5.0.0 Purpose

Contains metadata about the current game session, such as turn progression and timestamps.

##### 2.3.4.7.6.0.0 Dependencies

*No items available*

##### 2.3.4.7.7.0.0 Framework Specific Attributes

*No items available*

##### 2.3.4.7.8.0.0 Technology Integration Notes

A simple, immutable data record.

##### 2.3.4.7.9.0.0 Validation Notes

Specification validated as complete for its purpose.

##### 2.3.4.7.10.0.0 Properties

###### 2.3.4.7.10.1.0 Property Name

####### 2.3.4.7.10.1.1 Property Name

TurnNumber

####### 2.3.4.7.10.1.2 Property Type

int

####### 2.3.4.7.10.1.3 Access Modifier

public

####### 2.3.4.7.10.1.4 Purpose

The current turn number of the game.

####### 2.3.4.7.10.1.5 Validation Attributes

*No items available*

####### 2.3.4.7.10.1.6 Framework Specific Configuration

[JsonPropertyName(\"turnNumber\")]

####### 2.3.4.7.10.1.7 Implementation Notes

Must be defined in the primary constructor.

###### 2.3.4.7.10.2.0 Property Name

####### 2.3.4.7.10.2.1 Property Name

ActivePlayerId

####### 2.3.4.7.10.2.2 Property Type

Guid

####### 2.3.4.7.10.2.3 Access Modifier

public

####### 2.3.4.7.10.2.4 Purpose

The PlayerId of the player whose turn it currently is.

####### 2.3.4.7.10.2.5 Validation Attributes

*No items available*

####### 2.3.4.7.10.2.6 Framework Specific Configuration

[JsonPropertyName(\"activePlayerId\")]

####### 2.3.4.7.10.2.7 Implementation Notes

Must be defined in the primary constructor.

###### 2.3.4.7.10.3.0 Property Name

####### 2.3.4.7.10.3.1 Property Name

SessionTimestamp

####### 2.3.4.7.10.3.2 Property Type

DateTime

####### 2.3.4.7.10.3.3 Access Modifier

public

####### 2.3.4.7.10.3.4 Purpose

The UTC timestamp when the game session was last saved.

####### 2.3.4.7.10.3.5 Validation Attributes

*No items available*

####### 2.3.4.7.10.3.6 Framework Specific Configuration

[JsonPropertyName(\"sessionTimestamp\")]

####### 2.3.4.7.10.3.7 Implementation Notes

Must be stored in UTC format for consistency across different time zones.

##### 2.3.4.7.11.0.0 Methods

*No items available*

##### 2.3.4.7.12.0.0 Events

*No items available*

##### 2.3.4.7.13.0.0 Implementation Notes

Pure data model with no associated logic.

### 2.3.5.0.0.0.0 Interface Specifications

*No items available*

### 2.3.6.0.0.0.0 Enum Specifications

*No items available*

### 2.3.7.0.0.0.0 Dto Specifications

*No items available*

### 2.3.8.0.0.0.0 Configuration Specifications

#### 2.3.8.1.0.0.0 Configuration Name

##### 2.3.8.1.1.0.0 Configuration Name

MonopolyTycoon.Domain.Models.csproj

##### 2.3.8.1.2.0.0 File Path

MonopolyTycoon.Domain.Models.csproj

##### 2.3.8.1.3.0.0 Purpose

Defines the project as a .NET 8 class library, enabling modern C# features and ensuring compatibility with the solution.

##### 2.3.8.1.4.0.0 Framework Base Class

XML (MSBuild format)

##### 2.3.8.1.5.0.0 Configuration Sections

- {'section_name': 'PropertyGroup', 'properties': [{'property_name': 'TargetFramework', 'property_type': 'XML Element', 'default_value': 'net8.0', 'required': 'true', 'description': 'Specifies the target framework as .NET 8.'}, {'property_name': 'Nullable', 'property_type': 'XML Element', 'default_value': 'enable', 'required': 'true', 'description': 'Enables nullable reference types for the project to enforce null-safety contracts.'}, {'property_name': 'ImplicitUsings', 'property_type': 'XML Element', 'default_value': 'enable', 'required': 'true', 'description': 'Enables global using directives for common namespaces, reducing code boilerplate.'}, {'property_name': 'RootNamespace', 'property_type': 'XML Element', 'default_value': 'MonopolyTycoon.Domain.Models', 'required': 'true', 'description': 'Defines the root namespace for the project.'}]}

##### 2.3.8.1.6.0.0 Validation Requirements

The project file must be a valid MSBuild XML file targeting the \"Microsoft.NET.Sdk\".

##### 2.3.8.1.7.0.0 Validation Notes

Specification validated. Ensures project is configured with modern .NET 8 standards, including null-safety.

#### 2.3.8.2.0.0.0 Configuration Name

##### 2.3.8.2.1.0.0 Configuration Name

Directory.Build.props

##### 2.3.8.2.2.0.0 File Path

Directory.Build.props

##### 2.3.8.2.3.0.0 Purpose

Provides centralized, solution-wide MSBuild properties to ensure all projects, including this one, share consistent settings.

##### 2.3.8.2.4.0.0 Framework Base Class

XML (MSBuild format)

##### 2.3.8.2.5.0.0 Configuration Sections

- {'section_name': 'PropertyGroup', 'properties': [{'property_name': 'LangVersion', 'property_type': 'XML Element', 'default_value': 'latest', 'required': 'true', 'description': 'Ensures the project uses the latest available C# language version features.'}, {'property_name': 'GenerateDocumentationFile', 'property_type': 'XML Element', 'default_value': 'true', 'required': 'false', 'description': 'Enables the generation of XML documentation files for better IntelliSense in consuming projects.'}]}

##### 2.3.8.2.6.0.0 Validation Requirements

This file should be placed in a root directory above the project folder to be automatically included by MSBuild.

##### 2.3.8.2.7.0.0 Validation Notes

Specification validated. This file correctly centralizes build configuration, promoting consistency.

#### 2.3.8.3.0.0.0 Configuration Name

##### 2.3.8.3.1.0.0 Configuration Name

.editorconfig

##### 2.3.8.3.2.0.0 File Path

.editorconfig

##### 2.3.8.3.3.0.0 Purpose

Enforces consistent coding styles, formatting, and naming conventions across the entire codebase.

##### 2.3.8.3.4.0.0 Framework Base Class

INI format

##### 2.3.8.3.5.0.0 Configuration Sections

- {'section_name': '[*.cs]', 'properties': [{'property_name': 'indent_style', 'property_type': 'string', 'default_value': 'space', 'required': 'true', 'description': 'Specifies the indentation style.'}, {'property_name': 'indent_size', 'property_type': 'int', 'default_value': '4', 'required': 'true', 'description': 'Specifies the indentation size.'}]}

##### 2.3.8.3.6.0.0 Validation Requirements

Must be a valid .editorconfig file placed at the root of the repository.

##### 2.3.8.3.7.0.0 Validation Notes

Specification validated. Enforces code style at the repository level, ensuring maintainability.

### 2.3.9.0.0.0.0 Dependency Injection Specifications

*No items available*

### 2.3.10.0.0.0.0 External Integration Specifications

*No items available*

## 2.4.0.0.0.0.0 Component Count Validation

| Property | Value |
|----------|-------|
| Total Classes | 7 |
| Total Interfaces | 0 |
| Total Enums | 0 |
| Total Dtos | 0 |
| Total Configurations | 3 |
| Total External Integrations | 0 |
| Grand Total Components | 10 |
| Phase 2 Claimed Count | 10 |
| Phase 2 Actual Count | 10 |
| Validation Added Count | 0 |
| Final Validated Count | 10 |

