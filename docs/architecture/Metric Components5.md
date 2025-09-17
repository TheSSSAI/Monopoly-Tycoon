# 1 System Overview

## 1.1 Analysis Date

2025-06-13

## 1.2 Technology Stack

- Unity Engine
- .NET 8
- C#
- SQLite
- Serilog

## 1.3 Monitoring Components

- Application Logging System (Serilog)
- Runtime Performance Profiler (Unity Profiler API)

## 1.4 Requirements

- REQ-1-014 (Performance: FPS)
- REQ-1-015 (Performance: Load Times)
- REQ-1-034 (Player Statistics)
- REQ-1-088 (Data Integrity)
- REQ-1-023 (Error Handling)

## 1.5 Environment

production

# 2.0 Standard System Metrics Selection

## 2.1 Hardware Utilization Metrics

### 2.1.1 gauge

#### 2.1.1.1 Name

client.cpu.usage.percentage

#### 2.1.1.2 Type

🔹 gauge

#### 2.1.1.3 Unit

percent

#### 2.1.1.4 Description

Measures the CPU utilization of the game process on the client's machine.

#### 2.1.1.5 Collection

##### 2.1.1.5.1 Interval

10s

##### 2.1.1.5.2 Method

periodic_sampling

#### 2.1.1.6.0 Thresholds

##### 2.1.1.6.1 Warning

> 80%

##### 2.1.1.6.2 Critical

> 95% for 1 minute

#### 2.1.1.7.0 Justification

To diagnose performance issues (e.g., FPS drops per REQ-1-014) related to CPU bottlenecks on user hardware.

### 2.1.2.0.0 gauge

#### 2.1.2.1.0 Name

client.memory.working_set.bytes

#### 2.1.2.2.0 Type

🔹 gauge

#### 2.1.2.3.0 Unit

bytes

#### 2.1.2.4.0 Description

Measures the working set memory of the game process on the client's machine.

#### 2.1.2.5.0 Collection

##### 2.1.2.5.1 Interval

10s

##### 2.1.2.5.2 Method

periodic_sampling

#### 2.1.2.6.0 Thresholds

##### 2.1.2.6.1 Warning

> 3GB

##### 2.1.2.6.2 Critical

> 3.5GB

#### 2.1.2.7.0 Justification

To identify memory leaks or excessive memory usage that could lead to crashes or poor performance, impacting REQ-1-014.

## 2.2.0.0.0 Runtime Metrics

### 2.2.1.0.0 counter

#### 2.2.1.1.0 Name

dotnet.gc.collections.count

#### 2.2.1.2.0 Type

🔹 counter

#### 2.2.1.3.0 Unit

collections

#### 2.2.1.4.0 Description

Counts the number of Garbage Collection (GC) runs for Gen 0, 1, and 2.

#### 2.2.1.5.0 Technology

.NET

#### 2.2.1.6.0 Collection

##### 2.2.1.6.1 Interval

60s

##### 2.2.1.6.2 Method

periodic_sampling

#### 2.2.1.7.0 Criticality

medium

### 2.2.2.0.0 histogram

#### 2.2.2.1.0 Name

dotnet.gc.pause.duration.seconds

#### 2.2.2.2.0 Type

🔹 histogram

#### 2.2.2.3.0 Unit

seconds

#### 2.2.2.4.0 Description

Measures the duration of GC pauses, which can cause frame stuttering.

#### 2.2.2.5.0 Technology

.NET

#### 2.2.2.6.0 Collection

##### 2.2.2.6.1 Interval

on_event

##### 2.2.2.6.2 Method

event_based

#### 2.2.2.7.0 Criticality

high

## 2.3.0.0.0 Request Response Metrics

*No items available*

## 2.4.0.0.0 Availability Metrics

### 2.4.1.0.0 gauge

#### 2.4.1.1.0 Name

app.session.duration.seconds

#### 2.4.1.2.0 Type

🔹 gauge

#### 2.4.1.3.0 Unit

seconds

#### 2.4.1.4.0 Description

Measures the duration of a single user gameplay session, from application start to graceful exit.

#### 2.4.1.5.0 Calculation

Timestamp on exit - Timestamp on start

#### 2.4.1.6.0 Sla Target

N/A

### 2.4.2.0.0 counter

#### 2.4.2.1.0 Name

app.crashes.count

#### 2.4.2.2.0 Type

🔹 counter

#### 2.4.2.3.0 Unit

crashes

#### 2.4.2.4.0 Description

Counts unhandled exceptions that terminate the application. Inferred by a session starting but not ending gracefully.

#### 2.4.2.5.0 Calculation

Incremented when an unhandled exception is caught by the global exception handler (REQ-1-023).

#### 2.4.2.6.0 Sla Target

N/A

## 2.5.0.0.0 Scalability Metrics

*No items available*

# 3.0.0.0.0 Application Specific Metrics Design

## 3.1.0.0.0 Transaction Metrics

### 3.1.1.0.0 counter

#### 3.1.1.1.0 Name

game.session.started

#### 3.1.1.2.0 Type

🔹 counter

#### 3.1.1.3.0 Unit

sessions

#### 3.1.1.4.0 Description

Counts every time a new game is started from the setup screen.

#### 3.1.1.5.0 Business Context

Initial user action to play the game.

#### 3.1.1.6.0 Dimensions

- aiOpponentCount
- aiDifficultyProfile

#### 3.1.1.7.0 Collection

##### 3.1.1.7.1 Interval

on_event

##### 3.1.1.7.2 Method

event_based

#### 3.1.1.8.0 Aggregation

##### 3.1.1.8.1 Functions

- sum

##### 3.1.1.8.2 Window

lifetime

### 3.1.2.0.0 counter

#### 3.1.2.1.0 Name

game.session.completed

#### 3.1.2.2.0 Type

🔹 counter

#### 3.1.2.3.0 Unit

sessions

#### 3.1.2.4.0 Description

Counts every time a game session is completed (win or loss).

#### 3.1.2.5.0 Business Context

Measures player retention through a full game loop. Feeds into Player Statistics (REQ-1-034).

#### 3.1.2.6.0 Dimensions

- result[win|loss]

#### 3.1.2.7.0 Collection

##### 3.1.2.7.1 Interval

on_event

##### 3.1.2.7.2 Method

event_based

#### 3.1.2.8.0 Aggregation

##### 3.1.2.8.1 Functions

- sum

##### 3.1.2.8.2 Window

lifetime

### 3.1.3.0.0 histogram

#### 3.1.3.1.0 Name

game.turn.processing.duration.seconds

#### 3.1.3.2.0 Type

🔹 histogram

#### 3.1.3.3.0 Unit

seconds

#### 3.1.3.4.0 Description

Measures the time taken to process a single player turn.

#### 3.1.3.5.0 Business Context

Monitors the core game loop for performance degradation.

#### 3.1.3.6.0 Dimensions

- playerType[human|ai]
- aiDifficulty

#### 3.1.3.7.0 Collection

##### 3.1.3.7.1 Interval

on_event

##### 3.1.3.7.2 Method

event_based

#### 3.1.3.8.0 Aggregation

##### 3.1.3.8.1 Functions

- avg
- p95
- max

##### 3.1.3.8.2 Window

session

## 3.2.0.0.0 Cache Performance Metrics

*No items available*

## 3.3.0.0.0 External Dependency Metrics

### 3.3.1.0.0 histogram

#### 3.3.1.1.0 Name

storage.save_game.duration.seconds

#### 3.3.1.2.0 Type

🔹 histogram

#### 3.3.1.3.0 Unit

seconds

#### 3.3.1.4.0 Description

Measures the latency of serializing and writing a save game file.

#### 3.3.1.5.0 Dependency

Local File System

#### 3.3.1.6.0 Circuit Breaker Integration

❌ No

#### 3.3.1.7.0 Sla

##### 3.3.1.7.1 Response Time

N/A

##### 3.3.1.7.2 Availability

N/A

### 3.3.2.0.0 histogram

#### 3.3.2.1.0 Name

storage.load_game.duration.seconds

#### 3.3.2.2.0 Type

🔹 histogram

#### 3.3.2.3.0 Unit

seconds

#### 3.3.2.4.0 Description

Measures the latency of reading and deserializing a save game file. Directly tracks REQ-1-015.

#### 3.3.2.5.0 Dependency

Local File System

#### 3.3.2.6.0 Circuit Breaker Integration

❌ No

#### 3.3.2.7.0 Sla

##### 3.3.2.7.1 Response Time

10s

##### 3.3.2.7.2 Availability

N/A

### 3.3.3.0.0 histogram

#### 3.3.3.1.0 Name

storage.statistics_db.write.duration.seconds

#### 3.3.3.2.0 Type

🔹 histogram

#### 3.3.3.3.0 Unit

seconds

#### 3.3.3.4.0 Description

Measures the latency of writing game results to the SQLite database.

#### 3.3.3.5.0 Dependency

SQLite Database

#### 3.3.3.6.0 Circuit Breaker Integration

❌ No

#### 3.3.3.7.0 Sla

##### 3.3.3.7.1 Response Time

N/A

##### 3.3.3.7.2 Availability

N/A

## 3.4.0.0.0 Error Metrics

### 3.4.1.0.0 counter

#### 3.4.1.1.0 Name

app.exceptions.unhandled.count

#### 3.4.1.2.0 Type

🔹 counter

#### 3.4.1.3.0 Unit

exceptions

#### 3.4.1.4.0 Description

Counts the number of unhandled exceptions caught by the global handler, triggering the error dialog from REQ-1-023.

#### 3.4.1.5.0 Error Types

- all

#### 3.4.1.6.0 Dimensions

- exceptionType

#### 3.4.1.7.0 Alert Threshold

N/A (Local)

### 3.4.2.0.0 counter

#### 3.4.2.1.0 Name

storage.save_file.corrupted.count

#### 3.4.2.2.0 Type

🔹 counter

#### 3.4.2.3.0 Unit

files

#### 3.4.2.4.0 Description

Counts the number of times a save file fails checksum validation upon loading, as per REQ-1-088.

#### 3.4.2.5.0 Error Types

- DataCorruption

#### 3.4.2.6.0 Dimensions

*No items available*

#### 3.4.2.7.0 Alert Threshold

N/A (Local)

### 3.4.3.0.0 counter

#### 3.4.3.1.0 Name

storage.data_migration.failures.count

#### 3.4.3.2.0 Type

🔹 counter

#### 3.4.3.3.0 Unit

migrations

#### 3.4.3.4.0 Description

Counts failures in the automatic data migration process for old save files or statistics databases (REQ-1-090).

#### 3.4.3.5.0 Error Types

- DataMigrationError

#### 3.4.3.6.0 Dimensions

- dataType[save|stats]
- fromVersion
- toVersion

#### 3.4.3.7.0 Alert Threshold

N/A (Local)

## 3.5.0.0.0 Throughput And Latency Metrics

- {'name': 'client.rendering.fps', 'type': 'summary', 'unit': 'frames_per_second', 'description': "Measures the client's frame rate to ensure a smooth gameplay experience. Directly tracks REQ-1-014.", 'percentiles': ['avg', 'min'], 'buckets': [], 'slaTargets': {'p95': '>= 60', 'p99': '>= 45'}}

# 4.0.0.0.0 Business Kpi Identification

## 4.1.0.0.0 Critical Business Metrics

### 4.1.1.0.0 counter

#### 4.1.1.1.0 Name

player.stats.total_games_played

#### 4.1.1.2.0 Type

🔹 counter

#### 4.1.1.3.0 Unit

games

#### 4.1.1.4.0 Description

Total number of games completed by the player. Per REQ-1-034.

#### 4.1.1.5.0 Business Owner

Product

#### 4.1.1.6.0 Calculation

SUM(game.session.completed) WHERE profileId = current

#### 4.1.1.7.0 Reporting Frequency

on_demand

#### 4.1.1.8.0 Target

N/A

### 4.1.2.0.0 gauge

#### 4.1.2.1.0 Name

player.stats.win_loss_ratio

#### 4.1.2.2.0 Type

🔹 gauge

#### 4.1.2.3.0 Unit

ratio

#### 4.1.2.4.0 Description

Player's win to loss ratio. Per REQ-1-034.

#### 4.1.2.5.0 Business Owner

Product

#### 4.1.2.6.0 Calculation

SUM(game.session.completed WHERE result='win') / SUM(game.session.completed WHERE result='loss')

#### 4.1.2.7.0 Reporting Frequency

on_demand

#### 4.1.2.8.0 Target

N/A

### 4.1.3.0.0 gauge

#### 4.1.3.1.0 Name

player.stats.average_game_duration.seconds

#### 4.1.3.2.0 Type

🔹 gauge

#### 4.1.3.3.0 Unit

seconds

#### 4.1.3.4.0 Description

Average time the player spends in a single game. Per REQ-1-034.

#### 4.1.3.5.0 Business Owner

Product

#### 4.1.3.6.0 Calculation

AVG(game.session.duration.seconds) over all completed games for the profile.

#### 4.1.3.7.0 Reporting Frequency

on_demand

#### 4.1.3.8.0 Target

N/A

## 4.2.0.0.0 User Engagement Metrics

*No items available*

## 4.3.0.0.0 Conversion Metrics

*No items available*

## 4.4.0.0.0 Operational Efficiency Kpis

*No items available*

## 4.5.0.0.0 Revenue And Cost Metrics

*No items available*

## 4.6.0.0.0 Customer Satisfaction Indicators

*No items available*

# 5.0.0.0.0 Collection Interval Optimization

## 5.1.0.0.0 Sampling Frequencies

### 5.1.1.0.0 Metric Category

#### 5.1.1.1.0 Metric Category

Client Performance (CPU/Memory)

#### 5.1.1.2.0 Interval

10s

#### 5.1.1.3.0 Justification

Provides sufficient granularity to correlate with performance drops without significant client-side overhead.

#### 5.1.1.4.0 Resource Impact

low

### 5.1.2.0.0 Metric Category

#### 5.1.2.1.0 Metric Category

Game Transactions (Start/End/Turn)

#### 5.1.2.2.0 Interval

on_event

#### 5.1.2.3.0 Justification

Events are discrete and should be captured as they happen.

#### 5.1.2.4.0 Resource Impact

low

### 5.1.3.0.0 Metric Category

#### 5.1.3.1.0 Metric Category

Rendering (FPS)

#### 5.1.3.2.0 Interval

per_frame

#### 5.1.3.3.0 Justification

FPS is a real-time measurement requiring high-frequency sampling to be meaningful.

#### 5.1.3.4.0 Resource Impact

low

## 5.2.0.0.0 High Frequency Metrics

- {'name': 'client.rendering.fps', 'interval': 'per_frame', 'criticality': 'high', 'costJustification': 'Directly measures a critical non-functional requirement (REQ-1-014).'}

## 5.3.0.0.0 Cardinality Considerations

- {'metricName': 'game.session.started', 'estimatedCardinality': 'low', 'dimensionStrategy': "aiDifficultyProfile is a combined string like '1-Easy,2-Hard' to keep cardinality low.", 'mitigationApproach': 'N/A'}

## 5.4.0.0.0 Aggregation Periods

### 5.4.1.0.0 Metric Type

#### 5.4.1.1.0 Metric Type

Player Statistics

#### 5.4.1.2.0 Periods

- lifetime

#### 5.4.1.3.0 Retention Strategy

Stored in local SQLite DB indefinitely until user reset (REQ-1-080).

### 5.4.2.0.0 Metric Type

#### 5.4.2.1.0 Metric Type

Performance Histograms

#### 5.4.2.2.0 Periods

- session

#### 5.4.2.3.0 Retention Strategy

In-memory only; not persisted.

## 5.5.0.0.0 Collection Methods

### 5.5.1.0.0 Method

#### 5.5.1.1.0 Method

real-time

#### 5.5.1.2.0 Applicable Metrics

- client.rendering.fps

#### 5.5.1.3.0 Implementation

Unity Profiler API or custom frame timer.

#### 5.5.1.4.0 Performance

Minimal overhead.

### 5.5.2.0.0 Method

#### 5.5.2.1.0 Method

batch

#### 5.5.2.2.0 Applicable Metrics

- player.stats.*

#### 5.5.2.3.0 Implementation

At the end of a game, results are written to the SQLite database in a single transaction.

#### 5.5.2.4.0 Performance

Negligible impact as it occurs during a scene transition.

# 6.0.0.0.0 Aggregation Method Selection

## 6.1.0.0.0 Statistical Aggregations

- {'metricName': 'game.turn.processing.duration.seconds', 'aggregationFunctions': ['avg', 'p95', 'max'], 'windows': ['session'], 'justification': 'Average shows typical performance, P95 identifies frequent stalls, Max catches worst-case scenarios.'}

## 6.2.0.0.0 Histogram Requirements

- {'metricName': 'storage.load_game.duration.seconds', 'buckets': ['0.5', '1', '2', '5', '10', '15'], 'percentiles': ['avg', 'p99'], 'accuracy': 'High accuracy needed to validate the 10-second requirement in REQ-1-015.'}

## 6.3.0.0.0 Percentile Calculations

- {'metricName': 'client.rendering.fps', 'percentiles': ['avg', 'min'], 'algorithm': 'Simple min/avg calculation over a 1-second window.', 'accuracy': 'Sufficient for user-facing display and validation of REQ-1-014.'}

## 6.4.0.0.0 Metric Types

### 6.4.1.0.0 player.stats.total_games_played

#### 6.4.1.1.0 Name

player.stats.total_games_played

#### 6.4.1.2.0 Implementation

counter

#### 6.4.1.3.0 Reasoning

This is a monotonically increasing value representing total count.

#### 6.4.1.4.0 Resets Handling

Can be reset by user action per REQ-1-080.

### 6.4.2.0.0 player.stats.win_loss_ratio

#### 6.4.2.1.0 Name

player.stats.win_loss_ratio

#### 6.4.2.2.0 Implementation

gauge

#### 6.4.2.3.0 Reasoning

This value can go up or down and represents a calculated state, not a count.

#### 6.4.2.4.0 Resets Handling

Recalculated after each game or reset by user.

## 6.5.0.0.0 Dimensional Aggregation

- {'metricName': 'game.turn.processing.duration.seconds', 'dimensions': ['playerType', 'aiDifficulty'], 'aggregationStrategy': 'Aggregated per session to analyze if a specific AI difficulty is causing performance issues.', 'cardinalityImpact': 'Low, max of 1 + 3*3 = 10 combinations.'}

## 6.6.0.0.0 Derived Metrics

- {'name': 'app.crash.rate.percentage', 'calculation': '(app.crashes.count / app.session.started) * 100', 'sourceMetrics': ['app.crashes.count', 'app.session.started'], 'updateFrequency': 'on_demand'}

# 7.0.0.0.0 Storage Requirements Planning

## 7.1.0.0.0 Retention Periods

### 7.1.1.0.0 Metric Type

#### 7.1.1.1.0 Metric Type

Player Statistics (SQLite)

#### 7.1.1.2.0 Retention Period

infinite

#### 7.1.1.3.0 Justification

User's historical data should be kept unless they explicitly delete it (REQ-1-080).

#### 7.1.1.4.0 Compliance Requirement

N/A

### 7.1.2.0.0 Metric Type

#### 7.1.2.1.0 Metric Type

Session Performance Metrics (In-Memory)

#### 7.1.2.2.0 Retention Period

single_session

#### 7.1.2.3.0 Justification

These metrics are for real-time diagnostics and are not required to be persisted.

#### 7.1.2.4.0 Compliance Requirement

N/A

## 7.2.0.0.0 Data Resolution

- {'timeRange': 'lifetime', 'resolution': 'per_game', 'queryPerformance': 'High, as the SQLite database is small and local.', 'storageOptimization': 'Data is already aggregated per game, which is the lowest level of granularity needed.'}

## 7.3.0.0.0 Downsampling Strategies

*No items available*

## 7.4.0.0.0 Storage Performance

| Property | Value |
|----------|-------|
| Write Latency | < 500ms for end-of-game statistics update. |
| Query Latency | < 100ms for displaying statistics screen. |
| Throughput Requirements | N/A (single user) |
| Scalability Needs | N/A (local database) |

## 7.5.0.0.0 Query Optimization

- {'queryPattern': 'Fetch Top 10 Wins by Net Worth', 'optimizationStrategy': 'Create a database index on the GameResult table on (profileId, didHumanWin, humanFinalNetWorth).', 'indexingRequirements': ['GameResult(humanFinalNetWorth DESC)']}

## 7.6.0.0.0 Cost Optimization

- {'strategy': 'Local Storage Only', 'implementation': "All metrics are stored on the user's machine in a small SQLite file or kept in memory.", 'expectedSavings': '100% savings compared to any cloud-based telemetry solution.', 'tradeoffs': 'No central visibility into application performance or user behavior.'}

# 8.0.0.0.0 Project Specific Metrics Config

## 8.1.0.0.0 Standard Metrics

### 8.1.1.0.0 summary

#### 8.1.1.1.0 Name

client.rendering.fps

#### 8.1.1.2.0 Type

🔹 summary

#### 8.1.1.3.0 Unit

frames_per_second

#### 8.1.1.4.0 Collection

##### 8.1.1.4.1 Interval

1s

##### 8.1.1.4.2 Method

periodic_sampling

#### 8.1.1.5.0 Thresholds

##### 8.1.1.5.1 Warning

< 60

##### 8.1.1.5.2 Critical

< 45

#### 8.1.1.6.0 Dimensions

*No items available*

### 8.1.2.0.0 histogram

#### 8.1.2.1.0 Name

storage.load_game.duration.seconds

#### 8.1.2.2.0 Type

🔹 histogram

#### 8.1.2.3.0 Unit

seconds

#### 8.1.2.4.0 Collection

##### 8.1.2.4.1 Interval

on_event

##### 8.1.2.4.2 Method

event_based

#### 8.1.2.5.0 Thresholds

##### 8.1.2.5.1 Warning

> 8s

##### 8.1.2.5.2 Critical

> 10s

#### 8.1.2.6.0 Dimensions

*No items available*

### 8.1.3.0.0 counter

#### 8.1.3.1.0 Name

app.crashes.count

#### 8.1.3.2.0 Type

🔹 counter

#### 8.1.3.3.0 Unit

crashes

#### 8.1.3.4.0 Collection

##### 8.1.3.4.1 Interval

on_event

##### 8.1.3.4.2 Method

event_based

#### 8.1.3.5.0 Thresholds

##### 8.1.3.5.1 Warning

N/A

##### 8.1.3.5.2 Critical

N/A

#### 8.1.3.6.0 Dimensions

- exceptionType

## 8.2.0.0.0 Custom Metrics

### 8.2.1.0.0 gauge

#### 8.2.1.1.0 Name

player.stats.win_loss_ratio

#### 8.2.1.2.0 Description

The human player's lifetime win to loss ratio, persisted locally. Per REQ-1-034.

#### 8.2.1.3.0 Calculation

totalWins / totalLosses

#### 8.2.1.4.0 Type

🔹 gauge

#### 8.2.1.5.0 Unit

ratio

#### 8.2.1.6.0 Business Context

Primary KPI for player skill and engagement.

#### 8.2.1.7.0 Collection

##### 8.2.1.7.1 Interval

on_event

##### 8.2.1.7.2 Method

batch

#### 8.2.1.8.0 Alerting

##### 8.2.1.8.1 Enabled

❌ No

##### 8.2.1.8.2 Conditions

*No items available*

### 8.2.2.0.0 histogram

#### 8.2.2.1.0 Name

ai.decision.duration.seconds

#### 8.2.2.2.0 Description

Measures the 'thinking time' for an AI player's turn to identify performance issues in the AI logic.

#### 8.2.2.3.0 Calculation

End timestamp minus start timestamp of AIBehaviorTreeExecutor process.

#### 8.2.2.4.0 Type

🔹 histogram

#### 8.2.2.5.0 Unit

seconds

#### 8.2.2.6.0 Business Context

Technical performance metric for AI.

#### 8.2.2.7.0 Collection

##### 8.2.2.7.1 Interval

on_event

##### 8.2.2.7.2 Method

event_based

#### 8.2.2.8.0 Alerting

##### 8.2.2.8.1 Enabled

❌ No

##### 8.2.2.8.2 Conditions

*No items available*

## 8.3.0.0.0 Dashboard Metrics

### 8.3.1.0.0 Dashboard

#### 8.3.1.1.0 Dashboard

Player Statistics Screen

#### 8.3.1.2.0 Metrics

- player.stats.total_games_played
- player.stats.win_loss_ratio
- player.stats.average_game_duration.seconds

#### 8.3.1.3.0 Refresh Interval

on_load

#### 8.3.1.4.0 Audience

Player

### 8.3.2.0.0 Dashboard

#### 8.3.2.1.0 Dashboard

Top Scores Screen

#### 8.3.2.2.0 Metrics

- game.session.completed (filtered by win and sorted by net worth)

#### 8.3.2.3.0 Refresh Interval

on_load

#### 8.3.2.4.0 Audience

Player

# 9.0.0.0.0 Implementation Priority

## 9.1.0.0.0 Component

### 9.1.1.0.0 Component

Performance Metrics (FPS, Load Time)

### 9.1.2.0.0 Priority

🔴 high

### 9.1.3.0.0 Dependencies

- Runtime Performance Profiler

### 9.1.4.0.0 Estimated Effort

Low

### 9.1.5.0.0 Risk Level

low

## 9.2.0.0.0 Component

### 9.2.1.0.0 Component

Error & Crash Metrics

### 9.2.2.0.0 Priority

🔴 high

### 9.2.3.0.0 Dependencies

- Application Logging System (Serilog)

### 9.2.4.0.0 Estimated Effort

Medium

### 9.2.5.0.0 Risk Level

medium

## 9.3.0.0.0 Component

### 9.3.1.0.0 Component

Player Statistics (Business KPIs)

### 9.3.2.0.0 Priority

🟡 medium

### 9.3.3.0.0 Dependencies

- StatisticsRepository (SQLite)

### 9.3.4.0.0 Estimated Effort

Medium

### 9.3.5.0.0 Risk Level

low

## 9.4.0.0.0 Component

### 9.4.1.0.0 Component

Game Loop & AI Performance Metrics

### 9.4.2.0.0 Priority

🟢 low

### 9.4.3.0.0 Dependencies

- Runtime Performance Profiler

### 9.4.4.0.0 Estimated Effort

Low

### 9.4.5.0.0 Risk Level

low

# 10.0.0.0.0 Risk Assessment

## 10.1.0.0.0 Risk

### 10.1.1.0.0 Risk

No central telemetry.

### 10.1.2.0.0 Impact

high

### 10.1.3.0.0 Probability

high

### 10.1.4.0.0 Mitigation

This is by design (REQ-1-008, REQ-1-098). The risk is to the business/developer, not the user. Mitigation is to rely on user-submitted log files for bug reports. The error dialog (REQ-1-023) is designed to facilitate this.

### 10.1.5.0.0 Contingency Plan

N/A

## 10.2.0.0.0 Risk

### 10.2.1.0.0 Risk

Performance metrics collection (profiling) impacts game performance.

### 10.2.2.0.0 Impact

medium

### 10.2.3.0.0 Probability

low

### 10.2.4.0.0 Mitigation

Use lightweight sampling for performance metrics. Make the most detailed profiling (e.g., AI decision timing) a feature toggled only in debug builds.

### 10.2.5.0.0 Contingency Plan

Disable all non-essential metrics collection in production builds if performance impact is measured.

# 11.0.0.0.0 Recommendations

## 11.1.0.0.0 Category

### 11.1.1.0.0 Category

🔹 Implementation

### 11.1.2.0.0 Recommendation

Implement metrics collection as a thin wrapper around the existing logging and profiling components. Do not introduce a new, complex metrics library.

### 11.1.3.0.0 Justification

The system is offline and local. The needs are simple and can be met by structured logs (for events/errors) and direct measurement (for performance), avoiding unnecessary complexity and dependencies.

### 11.1.4.0.0 Priority

🔴 high

### 11.1.5.0.0 Implementation Notes

Create a `TelemetryService` that abstracts calls to Serilog and the Unity Profiler API.

## 11.2.0.0.0 Category

### 11.2.1.0.0 Category

🔹 Scope

### 11.2.2.0.0 Recommendation

Strictly adhere to collecting ONLY the metrics defined. Avoid adding any form of remote telemetry or analytics.

### 11.2.3.0.0 Justification

The project's requirements explicitly forbid network connectivity and data collection (REQ-1-008, REQ-1-098). Adding any would be a major scope creep and violation of user privacy promises.

### 11.2.4.0.0 Priority

🔴 high

### 11.2.5.0.0 Implementation Notes

Ensure no network-related libraries (analytics SDKs, etc.) are included in the final build.

