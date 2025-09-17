# 1 Id

REPO-IL-006

# 2 Name

MonopolyTycoon.Infrastructure.Logging

# 3 Description

A dedicated, cross-cutting library responsible for all application logging. It was extracted from the monolithic `MonopolyTycoon.Infrastructure` repository to centralize and standardize logging throughout the solution. It contains the configuration and setup for Serilog, as required by REQ-1-018, including sinks for structured JSON file output, rolling file policies (REQ-1-021), and PII filtering (REQ-1-022). By making this a separate, foundational library, all other components can easily add a dependency for logging without needing to know the implementation details. This ensures consistent log formats and policies across the entire application and isolates the Serilog third-party dependency. It provides a concrete implementation of the `ILogger` interface.

# 4 Type

🔹 Cross-Cutting Library

# 5 Namespace

MonopolyTycoon.Infrastructure.Logging

# 6 Output Path

src/infrastructure/MonopolyTycoon.Infrastructure.Logging

# 7 Framework

.NET 8

# 8 Language

C#

# 9 Technology

Serilog

# 10 Thirdparty Libraries

- Serilog
- Serilog.Sinks.File

# 11 Layer Ids

- infrastructure_layer

# 12 Dependencies

- REPO-AA-004

# 13 Requirements

## 13.1 Requirement Id

### 13.1.1 Requirement Id

REQ-1-018

## 13.2.0 Requirement Id

### 13.2.1 Requirement Id

REQ-1-019

## 13.3.0 Requirement Id

### 13.3.1 Requirement Id

REQ-1-020

## 13.4.0 Requirement Id

### 13.4.1 Requirement Id

REQ-1-021

## 13.5.0 Requirement Id

### 13.5.1 Requirement Id

REQ-1-022

# 14.0.0 Generate Tests

❌ No

# 15.0.0 Generate Documentation

✅ Yes

# 16.0.0 Architecture Style

Layered Architecture

# 17.0.0 Architecture Map

- LoggingService

# 18.0.0 Components Map

- logging-service-101

# 19.0.0 Requirements Map

- REQ-1-018
- REQ-1-019

# 20.0.0 Dependency Contracts

## 20.1.0 Repo-Aa-004

### 20.1.1 Required Interfaces

- {'interface': 'ILogger', 'methods': [], 'events': [], 'properties': []}

### 20.1.2 Integration Pattern

Implements the ILogger interface from the abstractions library.

### 20.1.3 Communication Protocol

N/A

# 21.0.0 Exposed Contracts

## 21.1.0 Public Interfaces

- {'interface': 'ILogger (Implementation)', 'methods': ['void Information(string messageTemplate, params object[] propertyValues)', 'void Warning(string messageTemplate, params object[] propertyValues)', 'void Error(Exception ex, string messageTemplate)'], 'events': [], 'properties': [], 'consumers': ['REPO-AS-005', 'REPO-IC-007', 'REPO-IP-SG-008', 'REPO-IP-ST-009', 'REPO-PU-010']}

# 22.0.0 Integration Patterns

| Property | Value |
|----------|-------|
| Dependency Injection | Provides a static `LoggerFactory` class to build a... |
| Event Communication | N/A |
| Data Flow | Receives log event data from all other parts of th... |
| Error Handling | Logging framework's own internal errors can be wri... |
| Async Patterns | Configured to use an asynchronous file sink to min... |

# 23.0.0 Technology Guidance

| Property | Value |
|----------|-------|
| Framework Specific | The logger configuration should be centralized in ... |
| Performance Considerations | The logger must be configured with an async, buffe... |
| Security Considerations | Must implement filtering or use a custom destructu... |
| Testing Approach | N/A - Configuration can be manually verified. Test... |

# 24.0.0 Scope Boundaries

## 24.1.0 Must Implement

- All logging configuration as specified in requirements REQ-1-018 to REQ-1-022.
- Structured JSON output to a rolling file.
- A concrete implementation of the `ILogger` interface.

## 24.2.0 Must Not Implement

- Any business logic or other infrastructure concerns.
- The decision of what to log; it only provides the mechanism.

## 24.3.0 Extension Points

- New Serilog sinks could be added (e.g., for a debug console) by modifying the central configuration.

## 24.4.0 Validation Rules

*No items available*

