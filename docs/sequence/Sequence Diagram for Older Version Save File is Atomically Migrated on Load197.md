sequenceDiagram
    participant "Presentation Layer" as PresentationLayer
    participant "GameSessionService" as GameSessionService
    participant "GameSaveRepository" as GameSaveRepository
    participant "LoggingService" as LoggingService
    participant "LoggingService" as LoggingService

    activate GameSessionService
    PresentationLayer->>GameSessionService: 1. User initiates game load for a specific slot.
    GameSessionService-->>PresentationLayer: Returns loaded GameState or an error status to UI.
    activate GameSaveRepository
    GameSessionService->>GameSaveRepository: 2. Requests the game state from the specified save slot.
    GameSaveRepository-->>GameSessionService: Returns a fully deserialized and validated GameState object.
    GameSaveRepository->>GameSaveRepository: 3. Reads raw save file content and peeks at version metadata.
    GameSaveRepository-->>GameSaveRepository: Returns raw JSON string and detected version.
    GameSaveRepository->>GameSaveRepository: 4. [Conditional] If fileVersion < currentAppVersion, migration is required.
    GameSaveRepository->>LoggingService: 5. Logs warning about version mismatch and initiates migration.
    activate LoggingService
    GameSaveRepository->>LoggingService: 6. Delegates the migration process for the raw data.
    LoggingService-->>GameSaveRepository: Returns the migrated raw JSON string upon success.
    LoggingService->>LoggingService: 7. Creates a temporary backup of the original save file.
    LoggingService->>LoggingService: 8. [Loop] Applies version-specific data transformation steps in memory.
    LoggingService-->>LoggingService: Returns transformed data for the next step.
    LoggingService->>LoggingService: 8.1. [Failure] If any step throws an exception, catch it.
    LoggingService->>LoggingService: 8.2. Restore the original file from the temporary backup.
    LoggingService->>LoggingService: 8.3. Clean up the backup file.
    LoggingService->>GameSaveRepository: 8.4. Throw DataMigrationException to signal failure.
    LoggingService->>LoggingService: 9. Deletes the temporary backup file upon successful migration.
    GameSaveRepository->>GameSaveRepository: 10. Atomically overwrites the original file with the migrated data.
    GameSaveRepository->>GameSaveRepository: 11. Deserializes the newly migrated JSON data into the GameState object.
    GameSaveRepository-->>GameSaveRepository: The fully hydrated GameState object.

    note over LoggingService: Atomic Operation Guarantee (REQ-1-100): The entire migration process is wrapped in a try/catch/fi...

    deactivate LoggingService
    deactivate GameSaveRepository
    deactivate GameSessionService
