# 1 Overview

## 1.1 Diagram Id

SEQ-CF-011

## 1.2 Name

Log Economic Transaction for Auditability

## 1.3 Description

A key economic transaction, such as a rent payment, property purchase, or trade, occurs. The system logs this event in a structured JSON format with specific, queryable properties to create a verifiable audit trail for debugging and analysis.

## 1.4 Type

🔹 ComplianceFlow

## 1.5 Purpose

To create a detailed, machine-readable audit trail of all economic activity, fulfilling the auditability requirement (REQ-1-028) for debugging and analysis.

## 1.6 Complexity

Low

## 1.7 Priority

🟡 Medium

## 1.8 Frequency

OnDemand

## 1.9 Participants

- BusinessLogicLayer
- InfrastructureLayer

## 1.10 Key Interactions

- The RuleEngine processes a transaction (e.g., rent payment).
- After successfully updating the GameState, it invokes the ILogger.
- It provides a structured log message at the INFO level with a message template.
- The log entry includes custom properties: TurnNumber, InvolvedPlayers (e.g., ['player1_id', 'player2_id']), TransactionType ('RentPayment'), TransactionValue (28), and PropertyID ('boardwalk_id').
- The LoggingService (Infrastructure) writes this structured event to the JSON log file.

## 1.11 Triggers

- Completion of any key economic transaction (property purchase, rent, tax, trade, etc.).

## 1.12 Outcomes

- A structured log entry is appended to the current log file, detailing the transaction.

## 1.13 Business Rules

- Key economic transactions must be logged at the INFO level (REQ-1-028).
- The log must include turn number, player IDs, transaction type, and relevant values (REQ-1-028).

## 1.14 Error Scenarios

- Logging service is unavailable or fails to write to disk, which is handled silently to not interrupt gameplay.

## 1.15 Integration Points

- LoggingService (Serilog)

# 2.0 Details

## 2.1 Diagram Id

SEQ-CF-011

## 2.2 Name

Implementation: Log Economic Transaction for Auditability

## 2.3 Description

Provides the technical sequence for logging a key economic transaction to a structured JSON file. This sequence is triggered by the GameEngine after successfully processing a financial action. It uses a dependency-injected ILogger interface to call the LoggingService, which formats and writes an immutable audit record. This fulfills REQ-1-028 by creating a verifiable audit trail for debugging and analysis, adhering to the 'Compliance as Code' pattern.

## 2.4 Participants

### 2.4.1 Engine

#### 2.4.1.1 Repository Id

game-engine-001

#### 2.4.1.2 Display Name

GameEngine (Business Logic)

#### 2.4.1.3 Type

🔹 Engine

#### 2.4.1.4 Technology

.NET 8, C#

#### 2.4.1.5 Order

1

#### 2.4.1.6 Style

| Property | Value |
|----------|-------|
| Shape | actor |
| Color | #1168bd |
| Stereotype | Domain |

### 2.4.2.0 Service

#### 2.4.2.1 Repository Id

logging-service-101

#### 2.4.2.2 Display Name

LoggingService (Infrastructure)

#### 2.4.2.3 Type

🔹 Service

#### 2.4.2.4 Technology

Serilog, .NET 8

#### 2.4.2.5 Order

2

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #6c757d |
| Stereotype | Infrastructure |

## 2.5.0.0 Interactions

### 2.5.1.0 InternalProcessing

#### 2.5.1.1 Source Id

game-engine-001

#### 2.5.1.2 Target Id

game-engine-001

#### 2.5.1.3 Message

1. [Internal] Processes and finalizes economic transaction (e.g., rent payment), updating the in-memory GameState.

#### 2.5.1.4 Sequence Number

1

#### 2.5.1.5 Type

🔹 InternalProcessing

#### 2.5.1.6 Is Synchronous

✅ Yes

#### 2.5.1.7 Return Message



#### 2.5.1.8 Has Return

❌ No

#### 2.5.1.9 Is Activation

✅ Yes

#### 2.5.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | N/A |
| Method | ExecuteTransaction() |
| Parameters | TransactionDetails object |
| Authentication | N/A |
| Error Handling | Handled by core game logic. |
| Performance | Part of the standard game loop. |

#### 2.5.1.11 Nested Interactions

*No items available*

### 2.5.2.0 MethodInvocation

#### 2.5.2.1 Source Id

game-engine-001

#### 2.5.2.2 Target Id

logging-service-101

#### 2.5.2.3 Message

2. Invokes logger to create immutable audit record of the transaction.

#### 2.5.2.4 Sequence Number

2

#### 2.5.2.5 Type

🔹 MethodInvocation

#### 2.5.2.6 Is Synchronous

✅ Yes

#### 2.5.2.7 Return Message

2.2. Control returns to GameEngine.

#### 2.5.2.8 Has Return

✅ Yes

#### 2.5.2.9 Is Activation

✅ Yes

#### 2.5.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-process method call (via ILogger interface) |
| Method | void Information(string messageTemplate, params ob... |
| Parameters | messageTemplate: 'Economic transaction completed: ... |
| Authentication | N/A (internal call) |
| Error Handling | Caller (GameEngine) MUST wrap this call in a try-c... |
| Performance | Target latency < 1ms to avoid impacting game loop ... |

#### 2.5.2.11 Nested Interactions

- {'sourceId': 'logging-service-101', 'targetId': 'logging-service-101', 'message': '2.1. [Internal] Formats log event with all context properties (Timestamp, Level, etc.) into a structured JSON string and appends it to the rolling log file on disk.', 'sequenceNumber': 2.1, 'type': 'InternalProcessing', 'isSynchronous': True, 'returnMessage': '', 'hasReturn': False, 'isActivation': True, 'technicalDetails': {'protocol': 'File I/O', 'method': 'Serilog.Sinks.File.Write()', 'parameters': 'Formatted JSON log entry', 'authentication': 'N/A (OS-level file permissions)', 'errorHandling': 'Internal to Serilog sink. May throw IOException on critical failure, which must be caught by the caller.', 'performance': 'Buffered write operation to minimize direct disk I/O latency.'}}

## 2.6.0.0 Notes

### 2.6.1.0 Content

#### 2.6.1.1 Content



```
Audit Log Structure (REQ-1-028):
The resulting JSON log entry must contain these specific, queryable properties:
- TurnNumber: int
- InvolvedPlayerIDs: string[]
- TransactionType: string (enum: Purchase, Rent, Tax, Trade, etc.)
- TransactionValue: decimal
- PropertyID: string (if applicable)
```

#### 2.6.1.2 Position

bottom-right

#### 2.6.1.3 Participant Id

logging-service-101

#### 2.6.1.4 Sequence Number

2.1

### 2.6.2.0 Content

#### 2.6.2.1 Content



```
Compliance Requirement (REQ-1-022):
The LoggingService must be configured to NOT enrich logs with any PII (e.g., machine name, OS user name) other than the user-provided profile name which is explicitly passed as part of the transaction data.
```

#### 2.6.2.2 Position

top-right

#### 2.6.2.3 Participant Id

*Not specified*

#### 2.6.2.4 Sequence Number

*Not specified*

## 2.7.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | The logging process must adhere to the data privac... |
| Performance Targets | The logging call must be highly performant and asy... |
| Error Handling Strategy | The primary error handling strategy is resilience.... |
| Testing Considerations | Unit tests should verify that for a given transact... |
| Monitoring Requirements | As an offline application, there is no real-time m... |
| Deployment Considerations | The Serilog configuration (log path, retention pol... |

