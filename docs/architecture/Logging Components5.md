# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Technology Stack

- .NET 8
- C#
- Unity Engine
- Serilog
- Microsoft.Data.Sqlite
- System.Text.Json

## 1.3 Monitoring Requirements

- REQ-1-018
- REQ-1-019
- REQ-1-020
- REQ-1-021
- REQ-1-022
- REQ-1-023
- REQ-1-028

## 1.4 System Architecture

Monolithic, offline single-player game with a Layered Architecture (Presentation, Application Services, Business Logic, Infrastructure).

## 1.5 Environment

production

# 2.0 Log Level And Category Strategy

## 2.1 Default Log Level

INFO

## 2.2 Environment Specific Levels

*No items available*

## 2.3 Component Categories

### 2.3.1 Component

#### 2.3.1.1 Component

AIBehaviorTreeExecutor

#### 2.3.1.2 Category

🔹 AI

#### 2.3.1.3 Log Level

DEBUG

#### 2.3.1.4 Verbose Logging

✅ Yes

#### 2.3.1.5 Justification

REQ-1-019 explicitly requires logging AI decision-making processes at the DEBUG level for analysis and tuning.

### 2.3.2.0 Component

#### 2.3.2.1 Component

GameEngine

#### 2.3.2.2 Category

🔹 GameLogic

#### 2.3.2.3 Log Level

INFO

#### 2.3.2.4 Verbose Logging

❌ No

#### 2.3.2.5 Justification

REQ-1-019 requires logging key game events at the INFO level. This includes rule enforcement and state changes.

### 2.3.3.0 Component

#### 2.3.3.1 Component

StatisticsRepository, GameSaveRepository

#### 2.3.3.2 Category

🔹 DataAccess

#### 2.3.3.3 Log Level

WARN

#### 2.3.3.4 Verbose Logging

❌ No

#### 2.3.3.5 Justification

INFO level logging for every data access is too verbose for a local application. WARN and ERROR levels are sufficient to capture persistence issues.

### 2.3.4.0 Component

#### 2.3.4.1 Component

GlobalExceptionHandler

#### 2.3.4.2 Category

🔹 Application

#### 2.3.4.3 Log Level

ERROR

#### 2.3.4.4 Verbose Logging

❌ No

#### 2.3.4.5 Justification

REQ-1-019 and REQ-1-023 require capturing unhandled exceptions and faults at the ERROR level.

## 2.4.0.0 Sampling Strategies

*No items available*

## 2.5.0.0 Logging Approach

### 2.5.1.0 Structured

✅ Yes

### 2.5.2.0 Format

JSON

### 2.5.3.0 Standard Fields

- Timestamp
- Level
- MessageTemplate
- Exception
- CorrelationId

### 2.5.4.0 Custom Fields

- TurnNumber
- PlayerIDs
- TransactionType
- TransactionValue
- PropertyID

# 3.0.0.0 Log Aggregation Architecture

## 3.1.0.0 Collection Mechanism

### 3.1.1.0 Type

🔹 library

### 3.1.2.0 Technology

Serilog

### 3.1.3.0 Configuration

#### 3.1.3.1 Sink

Serilog.Sinks.File

#### 3.1.3.2 Formatter

Serilog.Formatting.Json.JsonFormatter

### 3.1.4.0 Justification

REQ-1-018 explicitly mandates the use of the Serilog framework. As a monolithic, offline application, direct library integration is the only necessary mechanism.

## 3.2.0.0 Strategy

| Property | Value |
|----------|-------|
| Approach | local |
| Reasoning | The application is designed to be fully offline (R... |
| Local Retention | 7 days or 50 MB, whichever is smaller, as per REQ-... |

## 3.3.0.0 Shipping Methods

*No items available*

## 3.4.0.0 Buffering And Batching

| Property | Value |
|----------|-------|
| Buffer Size | Default (handled by Serilog sink) |
| Batch Size | 0 |
| Flush Interval | Default (handled by Serilog sink) |
| Backpressure Handling | Not applicable for local file sink. |

## 3.5.0.0 Transformation And Enrichment

- {'transformation': 'Add CorrelationId', 'purpose': 'To link log entries to user-facing error dialogs, fulfilling REQ-1-023.', 'stage': 'collection'}

## 3.6.0.0 High Availability

| Property | Value |
|----------|-------|
| Required | ❌ |
| Redundancy | None |
| Failover Strategy | Not applicable for a local, single-instance applic... |

# 4.0.0.0 Retention Policy Design

## 4.1.0.0 Retention Periods

- {'logType': 'All Application Logs', 'retentionPeriod': '7 Days', 'justification': 'REQ-1-021 specifies a rolling file policy based on time (7 days) or size (50 MB).', 'complianceRequirement': 'None'}

## 4.2.0.0 Compliance Requirements

- {'regulation': 'Custom PII Policy (REQ-1-022)', 'applicableLogTypes': ['All Application Logs'], 'minimumRetention': 'Not applicable', 'specialHandling': 'No PII may be logged, with the sole exception of the user-provided profile name.'}

## 4.3.0.0 Volume Impact Analysis

| Property | Value |
|----------|-------|
| Estimated Daily Volume | Low (< 10 MB) |
| Storage Cost Projection | Negligible (local storage, capped at 50 MB per REQ... |
| Compression Ratio | Not applicable, compression not required. |

## 4.4.0.0 Storage Tiering

### 4.4.1.0 Hot Storage

| Property | Value |
|----------|-------|
| Duration | 7 Days |
| Accessibility | immediate |
| Cost | low |

### 4.4.2.0 Warm Storage

| Property | Value |
|----------|-------|
| Duration | N/A |
| Accessibility | N/A |
| Cost | N/A |

### 4.4.3.0 Cold Storage

| Property | Value |
|----------|-------|
| Duration | N/A |
| Accessibility | N/A |
| Cost | N/A |

## 4.5.0.0 Compression Strategy

| Property | Value |
|----------|-------|
| Algorithm | None |
| Compression Level | N/A |
| Expected Ratio | N/A |

## 4.6.0.0 Anonymization Requirements

*No items available*

# 5.0.0.0 Search Capability Requirements

## 5.1.0.0 Essential Capabilities

- {'capability': 'Keyword and exact match search by CorrelationId', 'performanceRequirement': 'User-dependent (manual search in text editor/viewer)', 'justification': 'REQ-1-023 requires the ability to correlate a user-facing error ID with a specific log entry for troubleshooting.'}

## 5.2.0.0 Performance Characteristics

| Property | Value |
|----------|-------|
| Search Latency | N/A |
| Concurrent Users | 1 |
| Query Complexity | simple |
| Indexing Strategy | None |

## 5.3.0.0 Indexed Fields

*No items available*

## 5.4.0.0 Full Text Search

### 5.4.1.0 Required

❌ No

### 5.4.2.0 Fields

*No items available*

### 5.4.3.0 Search Engine

N/A

### 5.4.4.0 Relevance Scoring

❌ No

## 5.5.0.0 Correlation And Tracing

### 5.5.1.0 Correlation Ids

- CorrelationId

### 5.5.2.0 Trace Id Propagation

Not applicable (monolithic application).

### 5.5.3.0 Span Correlation

❌ No

### 5.5.4.0 Cross Service Tracing

❌ No

## 5.6.0.0 Dashboard Requirements

*No items available*

# 6.0.0.0 Storage Solution Selection

## 6.1.0.0 Selected Technology

### 6.1.1.0 Primary

Local File System

### 6.1.2.0 Reasoning

Explicitly required by REQ-1-020 for this offline application. The log file is stored at `%APPDATA%/MonopolyTycoon/logs`.

### 6.1.3.0 Alternatives

*No items available*

## 6.2.0.0 Scalability Requirements

| Property | Value |
|----------|-------|
| Expected Growth Rate | None (capped at 50 MB) |
| Peak Load Handling | Not applicable |
| Horizontal Scaling | ❌ |

## 6.3.0.0 Cost Performance Analysis

*No items available*

## 6.4.0.0 Backup And Recovery

| Property | Value |
|----------|-------|
| Backup Frequency | None |
| Recovery Time Objective | N/A |
| Recovery Point Objective | N/A |
| Testing Frequency | N/A |

## 6.5.0.0 Geo Distribution

### 6.5.1.0 Required

❌ No

### 6.5.2.0 Regions

*No items available*

### 6.5.3.0 Replication Strategy

N/A

## 6.6.0.0 Data Sovereignty

*No items available*

# 7.0.0.0 Access Control And Compliance

## 7.1.0.0 Access Control Requirements

- {'role': 'Operating System User', 'permissions': ['read', 'write'], 'logTypes': ['All Application Logs'], 'justification': 'Logs are stored on the local file system; access is governed by OS-level permissions for the `%APPDATA%` directory.'}

## 7.2.0.0 Sensitive Data Handling

- {'dataType': 'PII', 'handlingStrategy': 'exclude', 'fields': ['machine name', 'user account name', 'IP address'], 'complianceRequirement': 'REQ-1-022'}

## 7.3.0.0 Encryption Requirements

### 7.3.1.0 In Transit

| Property | Value |
|----------|-------|
| Required | ❌ |
| Protocol | N/A |
| Certificate Management | N/A |

### 7.3.2.0 At Rest

| Property | Value |
|----------|-------|
| Required | ❌ |
| Algorithm | N/A |
| Key Management | N/A |

## 7.4.0.0 Audit Trail

| Property | Value |
|----------|-------|
| Log Access | ❌ |
| Retention Period | N/A |
| Audit Log Location | N/A |
| Compliance Reporting | ❌ |

## 7.5.0.0 Regulatory Compliance

*No items available*

## 7.6.0.0 Data Protection Measures

- {'measure': 'PII Exclusion', 'implementation': 'Code reviews and static analysis to ensure no PII other than the profile name is passed to the logger.', 'monitoringRequired': False}

# 8.0.0.0 Project Specific Logging Config

## 8.1.0.0 Logging Config

### 8.1.1.0 Level

🔹 INFO (Default), DEBUG (For AI)

### 8.1.2.0 Retention

7 days or 50 MB

### 8.1.3.0 Aggregation

Local File Only

### 8.1.4.0 Storage

Local File System

### 8.1.5.0 Configuration

#### 8.1.5.1 Serilog Config

WriteTo.File(path, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7, fileSizeLimitBytes: 52428800, rollOnFileSizeLimit: true, formatter: new JsonFormatter())

## 8.2.0.0 Component Configurations

### 8.2.1.0 Component

#### 8.2.1.1 Component

AIBehaviorTreeExecutor

#### 8.2.1.2 Log Level

DEBUG

#### 8.2.1.3 Output Format

JSON

#### 8.2.1.4 Destinations

- local_file

#### 8.2.1.5 Sampling

##### 8.2.1.5.1 Enabled

❌ No

##### 8.2.1.5.2 Rate

N/A

#### 8.2.1.6.0 Custom Fields

- AI_ID
- DecisionNode
- EvaluationScore

### 8.2.2.0.0 Component

#### 8.2.2.1.0 Component

GameEngine (Economic Transactions)

#### 8.2.2.2.0 Log Level

INFO

#### 8.2.2.3.0 Output Format

JSON

#### 8.2.2.4.0 Destinations

- local_file

#### 8.2.2.5.0 Sampling

##### 8.2.2.5.1 Enabled

❌ No

##### 8.2.2.5.2 Rate

N/A

#### 8.2.2.6.0 Custom Fields

- TurnNumber
- PlayerIDs
- TransactionType
- TransactionValue

## 8.3.0.0.0 Metrics

### 8.3.1.0.0 Custom Metrics

*No data available*

## 8.4.0.0.0 Alert Rules

*No items available*

# 9.0.0.0.0 Implementation Priority

## 9.1.0.0.0 Component

### 9.1.1.0.0 Component

LoggingService (Serilog Wrapper)

### 9.1.2.0.0 Priority

🔴 high

### 9.1.3.0.0 Dependencies

*No items available*

### 9.1.4.0.0 Estimated Effort

Low

### 9.1.5.0.0 Risk Level

low

## 9.2.0.0.0 Component

### 9.2.1.0.0 Component

Global Exception Handler

### 9.2.2.0.0 Priority

🔴 high

### 9.2.3.0.0 Dependencies

- LoggingService

### 9.2.4.0.0 Estimated Effort

Low

### 9.2.5.0.0 Risk Level

medium

## 9.3.0.0.0 Component

### 9.3.1.0.0 Component

Instrumentation of Core Components

### 9.3.2.0.0 Priority

🟡 medium

### 9.3.3.0.0 Dependencies

- LoggingService

### 9.3.4.0.0 Estimated Effort

Medium

### 9.3.5.0.0 Risk Level

low

# 10.0.0.0.0 Risk Assessment

## 10.1.0.0.0 Risk

### 10.1.1.0.0 Risk

Insufficient logging detail for complex bug reproduction.

### 10.1.2.0.0 Impact

medium

### 10.1.3.0.0 Probability

medium

### 10.1.4.0.0 Mitigation

Ensure key state variables are included in DEBUG and ERROR level logs. The detailed economic transaction audit trail (REQ-1-028) helps mitigate this for financial bugs.

### 10.1.5.0.0 Contingency Plan

If a bug cannot be reproduced with logs, rely on the predefined game state files (REQ-1-027) to manually reproduce the scenario in a debug environment.

## 10.2.0.0.0 Risk

### 10.2.1.0.0 Risk

Logging impacts game performance, violating FPS requirements (REQ-1-014).

### 10.2.2.0.0 Impact

high

### 10.2.3.0.0 Probability

low

### 10.2.4.0.0 Mitigation

Use a high-performance logging library (Serilog). Avoid excessive logging in performance-critical loops (e.g., rendering updates). Default log level is INFO, with DEBUG only for specific, non-rendering components like AI.

### 10.2.5.0.0 Contingency Plan

Implement runtime log level switching (via a hidden key or config file change) to disable verbose logging if performance issues are reported by users.

# 11.0.0.0.0 Recommendations

## 11.1.0.0.0 Category

### 11.1.1.0.0 Category

🔹 Implementation

### 11.1.2.0.0 Recommendation

Use Serilog 'Enrichers' to consistently add context properties like `CorrelationId` to all log events originating from a specific operation or request.

### 11.1.3.0.0 Justification

This avoids manually adding the `CorrelationId` to every log statement, reducing code duplication and ensuring the context is never missed, which is critical for fulfilling REQ-1-023.

### 11.1.4.0.0 Priority

🔴 high

### 11.1.5.0.0 Implementation Notes

Create a middleware or service method wrapper that establishes a `LogContext` with the CorrelationId at the beginning of an operation.

## 11.2.0.0.0 Category

### 11.2.1.0.0 Category

🔹 Compliance

### 11.2.2.0.0 Recommendation

Implement a dedicated, sanitized logging facade for the Presentation (UI) Layer to prevent accidental logging of user input or other PII.

### 11.2.3.0.0 Justification

To strictly enforce REQ-1-022, the UI layer, which handles user input like profile names, poses the highest risk for accidental PII logging. A facade can enforce exclusion rules before data is passed to the core logger.

### 11.2.4.0.0 Priority

🟡 medium

### 11.2.5.0.0 Implementation Notes

The facade would only expose methods that take primitives or sanitized DTOs, preventing developers from logging complex UI objects directly.

