# 1 Strategies

## 1.1 Retry

### 1.1.1 Type

🔹 Retry

### 1.1.2 Configuration

#### 1.1.2.1 Retry Attempts

2

#### 1.1.2.2 Retry Intervals

##### 1.1.2.2.1 Interval1

2s

##### 1.1.2.2.2 Interval2

5s

#### 1.1.2.3.0 Error Handling Rules

- VersionCheckNetworkError

## 1.2.0.0.0 Fallback

### 1.2.1.0.0 Type

🔹 Fallback

### 1.2.2.0.0 Configuration

#### 1.2.2.1.0 Error Handling Rules

- ConfigurationFileError
- LocalizationFileError
- RulebookFileError

#### 1.2.2.2.0 Fallback Response

Use hardcoded default values, log a non-critical warning, and continue application execution.

## 1.3.0.0.0 Fallback

### 1.3.1.0.0 Type

🔹 Fallback

### 1.3.2.0.0 Configuration

#### 1.3.2.1.0 Error Handling Rules

- SaveFileCorruptionError
- SaveVersionIncompatibleError

#### 1.3.2.2.0 Fallback Response

Mark the specific save slot as 'Unusable' or 'Incompatible' in the Load Game UI as per REQ-1-088.

## 1.4.0.0.0 Fallback

### 1.4.1.0.0 Type

🔹 Fallback

### 1.4.2.0.0 Configuration

#### 1.4.2.1.0 Error Handling Rules

- StatisticsDatabaseCorruptionError

#### 1.4.2.2.0 Fallback Response

Attempt to restore from the most recent backup. If restoration fails, notify the user and offer to reset statistics to a default state.

## 1.5.0.0.0 DeadLetter

### 1.5.1.0.0 Type

🔹 DeadLetter

### 1.5.2.0.0 Configuration

#### 1.5.2.1.0 Dead Letter Queue

User-facing modal error dialog and detailed ERROR log entry.

#### 1.5.2.2.0 Error Handling Rules

- FileIOError
- DataAccessError
- UnexpectedApplicationError

# 2.0.0.0.0 Monitoring

## 2.1.0.0.0 Error Types

- FileIOError
- DataAccessError
- SaveFileCorruptionError
- SaveVersionIncompatibleError
- StatisticsDatabaseCorruptionError
- ConfigurationFileError
- LocalizationFileError
- RulebookFileError
- VersionCheckNetworkError
- UnexpectedApplicationError

## 2.2.0.0.0 Alerting

All errors are logged to a structured JSON file in %APPDATA%/MonopolyTycoon/logs with a unique ID (REQ-1-019). Critical, unrecoverable errors (FileIOError, DataAccessError, UnexpectedApplicationError) trigger a user-facing modal dialog with the unique error ID and instructions to locate the log file for support purposes (REQ-1-023). Non-critical errors (ConfigurationFileError, VersionCheckNetworkError) are logged without interrupting the user.

