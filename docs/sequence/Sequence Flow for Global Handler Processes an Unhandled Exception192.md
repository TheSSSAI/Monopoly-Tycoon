# 1 Overview

## 1.1 Diagram Id

SEQ-EH-001

## 1.2 Name

Global Handler Processes an Unhandled Exception

## 1.3 Description

An unexpected error occurs anywhere in the application that is not caught by a local handler. A global exception handler intercepts the error, generates a unique correlation ID, logs detailed diagnostic information to a local file, and presents a user-friendly error dialog containing the ID.

## 1.4 Type

🔹 ErrorHandling

## 1.5 Purpose

To prevent the application from crashing unexpectedly, provide a graceful failure mode, and give the user clear instructions for reporting the bug, as required for a polished product.

## 1.6 Complexity

Medium

## 1.7 Priority

🚨 Critical

## 1.8 Frequency

OnDemand

## 1.9 Participants

- REPO-PRES-001
- REPO-IL-006
- REPO-AA-004

## 1.10 Key Interactions

- An unhandled exception is thrown in any layer.
- A globally registered exception handler catches the exception.
- The handler generates a unique error ID (Correlation ID).
- It calls the Error method on the ILogger interface, passing the exception and the error ID.
- The Infrastructure Layer's LoggingService (Serilog wrapper) writes a detailed ERROR-level log entry to the local JSON log file.
- The global handler then instructs the Presentation Layer to display a modal error dialog.
- The dialog shows a user-friendly message, the unique error ID, and instructions on how to find the log file for support purposes.

## 1.11 Triggers

- Any unexpected, unhandled runtime error (e.g., NullReferenceException, InvalidOperationException).

## 1.12 Outcomes

- Application crash is prevented.
- A detailed error log is written to the file system.
- The user is informed about the error gracefully and empowered to help with debugging.

## 1.13 Business Rules

- The error dialog must provide a unique ID that correlates to a specific log entry (REQ-1-023).
- No personally identifiable information (other than profile name) shall be written to logs (REQ-1-022).

## 1.14 Error Scenarios

- The logging mechanism itself fails (e.g., cannot write to the log file).

## 1.15 Integration Points

- Local File System

# 2.0 Details

## 2.1 Diagram Id

SEQ-EH-001

## 2.2 Name

Global Handler Processes an Unhandled Exception

## 2.3 Description

Provides a detailed technical specification for the global exception handling mechanism. When an unhandled exception propagates to the top of the call stack, this sequence prevents an application crash by intercepting the exception, logging detailed diagnostic information with a unique correlation ID to a local file, and presenting a modal dialog to the user with actionable information for support.

## 2.4 Participants

### 2.4.1 Cross-Cutting Concern

#### 2.4.1.1 Repository Id

GlobalExceptionHandler

#### 2.4.1.2 Display Name

GlobalExceptionHandler

#### 2.4.1.3 Type

🔹 Cross-Cutting Concern

#### 2.4.1.4 Technology

C# AppDomain.CurrentDomain.UnhandledException handler

#### 2.4.1.5 Order

1

#### 2.4.1.6 Style

| Property | Value |
|----------|-------|
| Shape | actor |
| Color | #FF6347 |
| Stereotype | <<Cross-Cutting>> |

### 2.4.2.0 Interface

#### 2.4.2.1 Repository Id

REPO-AA-004

#### 2.4.2.2 Display Name

ILogger

#### 2.4.2.3 Type

🔹 Interface

#### 2.4.2.4 Technology

C# Interface defined in MonopolyTycoon.Application.Abstractions

#### 2.4.2.5 Order

2

#### 2.4.2.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #87CEEB |
| Stereotype | <<Interface>> |

### 2.4.3.0 Service

#### 2.4.3.1 Repository Id

REPO-IL-006

#### 2.4.3.2 Display Name

LoggingService

#### 2.4.3.3 Type

🔹 Service

#### 2.4.3.4 Technology

Serilog implementation of ILogger

#### 2.4.3.5 Order

3

#### 2.4.3.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #3CB371 |
| Stereotype | <<Service>> |

### 2.4.4.0 Infrastructure

#### 2.4.4.1 Repository Id

LocalFileSystem

#### 2.4.4.2 Display Name

Local File System

#### 2.4.4.3 Type

🔹 Infrastructure

#### 2.4.4.4 Technology

Windows OS File API

#### 2.4.4.5 Order

4

#### 2.4.4.6 Style

| Property | Value |
|----------|-------|
| Shape | database |
| Color | #999999 |
| Stereotype | <<Infrastructure>> |

### 2.4.5.0 Controller

#### 2.4.5.1 Repository Id

REPO-PRES-001

#### 2.4.5.2 Display Name

ViewManager

#### 2.4.5.3 Type

🔹 Controller

#### 2.4.5.4 Technology

Unity Engine C# Script

#### 2.4.5.5 Order

5

#### 2.4.5.6 Style

| Property | Value |
|----------|-------|
| Shape | component |
| Color | #4682B4 |
| Stereotype | <<Controller>> |

## 2.5.0.0 Interactions

### 2.5.1.0 Exception Handling

#### 2.5.1.1 Source Id

GlobalExceptionHandler

#### 2.5.1.2 Target Id

GlobalExceptionHandler

#### 2.5.1.3 Message

1. Catch(UnhandledExceptionEventArgs args)

#### 2.5.1.4 Sequence Number

1

#### 2.5.1.5 Type

🔹 Exception Handling

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
| Protocol | In-Process |
| Method | GlobalExceptionHandler.OnUnhandledException |
| Parameters | object sender, UnhandledExceptionEventArgs args |
| Authentication | N/A |
| Error Handling | This is the final catch block for the application. |
| Performance | Must execute rapidly to prevent the OS from flaggi... |

### 2.5.2.0 Data Generation

#### 2.5.2.1 Source Id

GlobalExceptionHandler

#### 2.5.2.2 Target Id

GlobalExceptionHandler

#### 2.5.2.3 Message

2. Generate unique error identifier

#### 2.5.2.4 Sequence Number

2

#### 2.5.2.5 Type

🔹 Data Generation

#### 2.5.2.6 Is Synchronous

✅ Yes

#### 2.5.2.7 Return Message

correlationId

#### 2.5.2.8 Has Return

✅ Yes

#### 2.5.2.9 Is Activation

❌ No

#### 2.5.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | Guid.NewGuid() |
| Parameters | none |
| Authentication | N/A |
| Error Handling | Extremely unlikely to fail. |
| Performance | Negligible latency. |

### 2.5.3.0 Logging

#### 2.5.3.1 Source Id

GlobalExceptionHandler

#### 2.5.3.2 Target Id

REPO-AA-004

#### 2.5.3.3 Message

3. Error(exception, messageTemplate, correlationId)

#### 2.5.3.4 Sequence Number

3

#### 2.5.3.5 Type

🔹 Logging

#### 2.5.3.6 Is Synchronous

✅ Yes

#### 2.5.3.7 Return Message



#### 2.5.3.8 Has Return

❌ No

#### 2.5.3.9 Is Activation

✅ Yes

#### 2.5.3.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process DI |
| Method | ILogger.Error(Exception ex, string template, param... |
| Parameters | Exception from args, 'Unhandled exception. ErrorID... |
| Authentication | N/A |
| Error Handling | Call is wrapped in a try/catch block to handle log... |
| Performance | Should be non-blocking or have minimal blocking ti... |

#### 2.5.3.11 Nested Interactions

##### 2.5.3.11.1 Method Invocation

###### 2.5.3.11.1.1 Source Id

REPO-AA-004

###### 2.5.3.11.1.2 Target Id

REPO-IL-006

###### 2.5.3.11.1.3 Message

3.1. [DI Resolution] Invoke concrete implementation

###### 2.5.3.11.1.4 Sequence Number

4

###### 2.5.3.11.1.5 Type

🔹 Method Invocation

###### 2.5.3.11.1.6 Is Synchronous

✅ Yes

###### 2.5.3.11.1.7 Return Message



###### 2.5.3.11.1.8 Has Return

❌ No

###### 2.5.3.11.1.9 Is Activation

✅ Yes

###### 2.5.3.11.1.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | LoggingService.Error(...) |
| Parameters | Inherited from parent call |
| Authentication | N/A |
| Error Handling | Exceptions are propagated back to the GlobalExcept... |
| Performance | Dependent on Serilog sink performance. |

##### 2.5.3.11.2.0 File I/O

###### 2.5.3.11.2.1 Source Id

REPO-IL-006

###### 2.5.3.11.2.2 Target Id

LocalFileSystem

###### 2.5.3.11.2.3 Message

3.2. Write structured error log to file

###### 2.5.3.11.2.4 Sequence Number

5

###### 2.5.3.11.2.5 Type

🔹 File I/O

###### 2.5.3.11.2.6 Is Synchronous

✅ Yes

###### 2.5.3.11.2.7 Return Message

Write status

###### 2.5.3.11.2.8 Has Return

✅ Yes

###### 2.5.3.11.2.9 Is Activation

✅ Yes

###### 2.5.3.11.2.10 Technical Details

| Property | Value |
|----------|-------|
| Protocol | OS API Call |
| Method | Serilog.Sinks.File.Write() via System.IO |
| Parameters | JSON-formatted log event including exception detai... |
| Authentication | OS-level file permissions. |
| Error Handling | Can throw IOException, UnauthorizedAccessException... |
| Performance | Typically fast, but can be slow on heavily loaded ... |

### 2.5.4.0.0.0 UI Update

#### 2.5.4.1.0.0 Source Id

GlobalExceptionHandler

#### 2.5.4.2.0.0 Target Id

REPO-PRES-001

#### 2.5.4.3.0.0 Message

4. ShowErrorDialog(dto)

#### 2.5.4.4.0.0 Sequence Number

6

#### 2.5.4.5.0.0 Type

🔹 UI Update

#### 2.5.4.6.0.0 Is Synchronous

✅ Yes

#### 2.5.4.7.0.0 Return Message



#### 2.5.4.8.0.0 Has Return

❌ No

#### 2.5.4.9.0.0 Is Activation

✅ Yes

#### 2.5.4.10.0.0 Technical Details

| Property | Value |
|----------|-------|
| Protocol | In-Process |
| Method | ViewManager.ShowErrorDialog(ErrorDialogDTO dto) |
| Parameters | dto containing a sanitized message, the correlatio... |
| Authentication | N/A |
| Error Handling | Assumes the UI thread is available. If the UI laye... |
| Performance | Must be responsive to display the dialog immediate... |

#### 2.5.4.11.0.0 Nested Interactions

- {'sourceId': 'REPO-PRES-001', 'targetId': 'REPO-PRES-001', 'message': '4.1. Instantiate and display modal dialog with error details', 'sequenceNumber': 7, 'type': 'UI Rendering', 'isSynchronous': True, 'returnMessage': '', 'hasReturn': False, 'isActivation': False, 'technicalDetails': {'protocol': 'Unity Engine API', 'method': 'GameObject.Instantiate(), Text.SetText()', 'parameters': 'UI Prefab, DTO contents.', 'authentication': 'N/A', 'errorHandling': 'N/A', 'performance': 'UI rendering must be performant.'}}

## 2.6.0.0.0.0 Notes

### 2.6.1.0.0.0 Content

#### 2.6.1.1.0.0 Content

The GlobalExceptionHandler must be registered at application startup to ensure it can catch exceptions from any thread.

#### 2.6.1.2.0.0 Position

top-left

#### 2.6.1.3.0.0 Participant Id

GlobalExceptionHandler

#### 2.6.1.4.0.0 Sequence Number

1

### 2.6.2.0.0.0 Content

#### 2.6.2.1.0.0 Content

REQ-1-023: The correlationId generated in step 2 is passed to both the logger (step 3) and the UI dialog (step 4), creating a verifiable link for user support.

#### 2.6.2.2.0.0 Position

bottom-right

#### 2.6.2.3.0.0 Participant Id

REPO-PRES-001

#### 2.6.2.4.0.0 Sequence Number

6

### 2.6.3.0.0.0 Content

#### 2.6.3.1.0.0 Content

REQ-1-022: PII sanitization must occur within the GlobalExceptionHandler before calling ILogger or ShowErrorDialog. The raw Exception object should not be passed directly to the UI.

#### 2.6.3.2.0.0 Position

middle-left

#### 2.6.3.3.0.0 Participant Id

GlobalExceptionHandler

#### 2.6.3.4.0.0 Sequence Number

3

## 2.7.0.0.0.0 Implementation Guidance

| Property | Value |
|----------|-------|
| Security Requirements | Exception details must be sanitized to prevent lea... |
| Performance Targets | The entire handling sequence, from catch to dialog... |
| Error Handling Strategy | The primary strategy is to prevent an application ... |
| Testing Considerations | Unit tests should trigger mock exceptions to verif... |
| Monitoring Requirements | As an offline application, the monitoring 'system'... |
| Deployment Considerations | The handler must be registered on application star... |

