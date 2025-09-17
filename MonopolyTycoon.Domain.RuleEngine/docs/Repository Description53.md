# 1 Id

REPO-DR-002

# 2 Name

MonopolyTycoon.Domain.RuleEngine

# 3 Description

This repository contains the pure, stateless implementation of the official Monopoly game rules, as mandated by REQ-1-003. It was extracted from the original `MonopolyTycoon.Domain` to isolate the complex, critical business logic from other domain concerns like AI. Its primary component, the `RuleEngine`, takes the current `GameState` and a proposed action, validates it, and returns the resulting state changes. It is completely technology-agnostic, with no dependencies on Unity, databases, or file I/O. This strict isolation makes it highly testable, ensuring the 70% unit test coverage requirement (REQ-1-025) can be met and validated independently of the rest of the system. It also contains the cryptographically secure dice roller (REQ-1-042), forming the logical core of the game's mechanics.

# 4 Type

🔹 Business Logic

# 5 Namespace

MonopolyTycoon.Domain.RuleEngine

# 6 Output Path

src/domain/MonopolyTycoon.Domain.RuleEngine

# 7 Framework

.NET 8

# 8 Language

C#

# 9 Technology

.NET Class Library

# 10 Thirdparty Libraries

- NUnit (for testing)

# 11 Layer Ids

- business_logic_layer

# 12 Dependencies

- REPO-DM-001

# 13 Requirements

## 13.1 Requirement Id

### 13.1.1 Requirement Id

REQ-1-003

## 13.2.0 Requirement Id

### 13.2.1 Requirement Id

REQ-1-025

## 13.3.0 Requirement Id

### 13.3.1 Requirement Id

REQ-1-042

## 13.4.0 Requirement Id

### 13.4.1 Requirement Id

REQ-1-054

# 14.0.0 Generate Tests

✅ Yes

# 15.0.0 Generate Documentation

✅ Yes

# 16.0.0 Architecture Style

Layered Architecture

# 17.0.0 Architecture Map

- RuleEngine
- DiceRoller

# 18.0.0 Components Map

- game-engine-001

# 19.0.0 Requirements Map

- REQ-1-003
- REQ-1-025
- REQ-1-042

# 20.0.0 Dependency Contracts

## 20.1.0 Repo-Dm-001

### 20.1.1 Required Interfaces

- {'interface': 'GameState', 'methods': [], 'events': [], 'properties': ['PlayerStates', 'BoardState']}

### 20.1.2 Integration Pattern

Direct project reference for compile-time access to domain model classes.

### 20.1.3 Communication Protocol

In-memory object references passed as method parameters.

# 21.0.0 Exposed Contracts

## 21.1.0 Public Interfaces

### 21.1.1 Interface

#### 21.1.1.1 Interface

IRuleEngine

#### 21.1.1.2 Methods

- ValidationResult ValidateAction(GameState state, PlayerAction action)
- GameState ApplyAction(GameState state, PlayerAction action)

#### 21.1.1.3 Events

*No items available*

#### 21.1.1.4 Properties

*No items available*

#### 21.1.1.5 Consumers

- REPO-AS-005

### 21.1.2.0 Interface

#### 21.1.2.1 Interface

IDiceRoller

#### 21.1.2.2 Methods

- DiceRoll Roll()

#### 21.1.2.3 Events

*No items available*

#### 21.1.2.4 Properties

*No items available*

#### 21.1.2.5 Consumers

- REPO-AS-005

# 22.0.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | The RuleEngine and DiceRoller are stateless servic... |
| Event Communication | N/A. This repository is a pure function that retur... |
| Data Flow | Follows a functional approach: receives a GameStat... |
| Error Handling | Returns explicit `ValidationResult` objects for il... |
| Async Patterns | N/A. Rule validation is a synchronous, CPU-bound o... |

# 23.0.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | Must remain free of any framework-specific (e.g., ... |
| Performance Considerations | Rule validation algorithms must be highly efficien... |
| Security Considerations | The dice roller implementation must use `System.Se... |
| Testing Approach | Heavy emphasis on unit testing with NUnit is requi... |

# 24.0.0.0 Scope Boundaries

## 24.1.0.0 Must Implement

- All gameplay mechanics as defined by the official 2024 US Monopoly rulebook (REQ-1-003).
- Stateless validation and state transition functions.
- A secure dice rolling mechanism (REQ-1-042).

## 24.2.0.0 Must Not Implement

- Game flow orchestration or turn management (this is an Application Service responsibility).
- Player input handling.
- Any form of I/O, including logging or data persistence.
- AI decision-making logic.

## 24.3.0.0 Extension Points

- New rules (e.g., for house rules, though out of scope for V1) could be added via a strategy pattern.

## 24.4.0.0 Validation Rules

- All validation logic for player actions (e.g., 'even build' rule REQ-1-054) must be contained within this component.

