sequenceDiagram
    participant "GameEngine (Business Logic)" as GameEngineBusinessLogic
    participant "LoggingService (Infrastructure)" as LoggingServiceInfrastructure

    activate GameEngineBusinessLogic
    GameEngineBusinessLogic->>GameEngineBusinessLogic: 1. 1. [Internal] Processes and finalizes economic transaction (e.g., rent payment), updating the in-memory GameState.
    activate LoggingServiceInfrastructure
    GameEngineBusinessLogic->>LoggingServiceInfrastructure: 2. 2. Invokes logger to create immutable audit record of the transaction.
    LoggingServiceInfrastructure-->>GameEngineBusinessLogic: 2.2. Control returns to GameEngine.
    LoggingServiceInfrastructure->>LoggingServiceInfrastructure: 2.1. 2.1. [Internal] Formats log event with all context properties (Timestamp, Level, etc.) into a structured JSON string and appends it to the rolling log file on disk.

    note over LoggingServiceInfrastructure: Audit Log Structure (REQ-1-028): The resulting JSON log entry must contain these specific, querya...

    deactivate LoggingServiceInfrastructure
    deactivate GameEngineBusinessLogic
