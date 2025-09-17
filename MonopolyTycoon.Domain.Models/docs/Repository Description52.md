# 1 Id

REPO-DM-001

# 2 Name

MonopolyTycoon.Domain.Models

# 3 Description

This foundational repository contains the Plain Old C# Object (POCO) data models that form the canonical representation of the game state. It was extracted from the original `MonopolyTycoon.Domain` repository to serve as a shared, dependency-free contract library. It includes core entities like `GameState`, `PlayerState`, and property definitions, as specified in REQ-1-041 and REQ-1-031. By isolating these models, any other component in the system can reference the core data structures without inheriting the entire domain's business logic. This separation is critical for decoupling, allowing the rule engine, application services, and persistence layers to operate on a common, stable set of objects. It has zero dependencies on other project code, maximizing its reusability and establishing a stable foundation for the entire solution's architecture.

# 4 Type

🔹 Model Library

# 5 Namespace

MonopolyTycoon.Domain.Models

# 6 Output Path

src/domain/MonopolyTycoon.Domain.Models

# 7 Framework

.NET 8

# 8 Language

C#

# 9 Technology

.NET Class Library

# 10 Thirdparty Libraries

*No items available*

# 11 Layer Ids

- business_logic_layer

# 12 Dependencies

*No items available*

# 13 Requirements

## 13.1 Requirement Id

### 13.1.1 Requirement Id

REQ-1-031

## 13.2.0 Requirement Id

### 13.2.1 Requirement Id

REQ-1-041

# 14.0.0 Generate Tests

✅ Yes

# 15.0.0 Generate Documentation

✅ Yes

# 16.0.0 Architecture Style

Layered Architecture

# 17.0.0 Architecture Map

- GameState
- PlayerState

# 18.0.0 Components Map

*No items available*

# 19.0.0 Requirements Map

- REQ-1-031
- REQ-1-041

# 20.0.0 Dependency Contracts

*No data available*

# 21.0.0 Exposed Contracts

## 21.1.0 Public Interfaces

### 21.1.1 Interface

#### 21.1.1.1 Interface

GameState (Class)

#### 21.1.1.2 Methods

*No items available*

#### 21.1.1.3 Events

*No items available*

#### 21.1.1.4 Properties

- List<PlayerState> PlayerStates
- BoardState BoardState
- BankState BankState
- DeckStates DeckStates
- GameMetadata GameMetadata

#### 21.1.1.5 Consumers

- REPO-DR-002
- REPO-DA-003
- REPO-AS-005
- REPO-IP-SG-008
- REPO-IP-ST-009
- REPO-PU-010

### 21.1.2.0 Interface

#### 21.1.2.1 Interface

PlayerState (Class)

#### 21.1.2.2 Methods

*No items available*

#### 21.1.2.3 Events

*No items available*

#### 21.1.2.4 Properties

- Guid PlayerId
- string PlayerName
- bool IsHuman
- int Cash

#### 21.1.2.5 Consumers

- REPO-DR-002
- REPO-DA-003
- REPO-AS-005
- REPO-PU-010

# 22.0.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | N/A - This is a library of passive data structures... |
| Event Communication | N/A - Models are passed as event payloads but do n... |
| Data Flow | These models are the primary data transfer objects... |
| Error Handling | N/A - Contains no executable logic that can fail. |
| Async Patterns | N/A |

# 23.0.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Models must remain as pure POCOs. Avoid adding any... |
| Performance Considerations | Ensure models are efficiently serializable by Syst... |
| Security Considerations | N/A |
| Testing Approach | Unit tests should focus on validating default cons... |

# 24.0.0.0 Scope Boundaries

## 24.1.0.0 Must Implement

- A comprehensive `GameState` class that fully encapsulates all data needed to save and load a game, as per REQ-1-041.
- A detailed `PlayerState` class containing all player-specific attributes defined in REQ-1-031.

## 24.2.0.0 Must Not Implement

- Any business logic or rule validation; models are for data representation only.
- Any dependencies on other layers, especially UI or Infrastructure.
- Any methods that perform I/O or have side effects.

## 24.3.0.0 Extension Points

- New properties can be added to models to support new game features, but this must be managed with a data migration strategy.

## 24.4.0.0 Validation Rules

- Models themselves do not contain validation; validation is the responsibility of the services that consume them.

