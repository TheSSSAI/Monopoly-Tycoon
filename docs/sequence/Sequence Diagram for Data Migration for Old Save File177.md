sequenceDiagram
    participant "GameSessionService" as GameSessionService
    participant "GameSaveRepository" as GameSaveRepository
    participant "DataMigrationManager" as DataMigrationManager
    participant "LoggingService" as LoggingService

    activate GameSaveRepository
    GameSessionService->>GameSaveRepository: 1. LoadGameAsync(slotNumber)
    GameSaveRepository-->>GameSessionService: Task<GameState>
    GameSaveRepository->>LoggingService: 2. Information("Attempting to load and validate save slot {slot}", slotNumber)
    GameSaveRepository->>GameSaveRepository: 3. Read file and check version property
    GameSaveRepository-->>GameSaveRepository: isLegacy: bool, rawContent: byte[]
    activate DataMigrationManager
    GameSaveRepository->>DataMigrationManager: 4. MigrateSaveDataAsync(rawContent, sourceVersion)
    DataMigrationManager-->>GameSaveRepository: Task<byte[]>
    DataMigrationManager->>LoggingService: 5. Information("Starting data migration from v{source} to v{target}")
    DataMigrationManager->>DataMigrationManager: 6. Execute atomic file migration operation
    DataMigrationManager-->>DataMigrationManager: migratedData: byte[]
    DataMigrationManager->>LoggingService: 6.1. Error(ex, "Migration failed. Initiating rollback.") OR Information("Migration Succeeded.")
    DataMigrationManager->>GameSaveRepository: 7. Return migratedData
    GameSaveRepository->>GameSaveRepository: 8. Deserialize<GameState>(migratedData)
    GameSaveRepository-->>GameSaveRepository: gameState: GameState
    GameSaveRepository->>GameSessionService: 9. Return gameState

    note over DataMigrationManager: The atomicity of the migration is the most critical aspect. The backup-migrate-overwrite-delete s...

    deactivate DataMigrationManager
    deactivate GameSaveRepository
