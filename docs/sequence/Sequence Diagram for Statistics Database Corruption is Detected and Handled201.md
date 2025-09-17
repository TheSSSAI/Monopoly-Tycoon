sequenceDiagram
    actor "User" as User
    participant "Presentation Layer" as PresentationLayer
    participant "Application Services Layer" as ApplicationServicesLayer
    participant "Infrastructure Layer (StatisticsRepository)" as InfrastructureLayerStatisticsRepository
    participant "SQLite Engine" as SQLiteEngine
    participant "File System" as FileSystem
    participant "Logging Service" as LoggingService

    activate PresentationLayer
    User->>PresentationLayer: 1. Starts the Monopoly Tycoon application.
    activate ApplicationServicesLayer
    PresentationLayer->>ApplicationServicesLayer: 2. Requests application initialization to load user profile and stats.
    ApplicationServicesLayer-->>PresentationLayer: Returns after initialization completes or fails gracefully.
    activate InfrastructureLayerStatisticsRepository
    ApplicationServicesLayer->>InfrastructureLayerStatisticsRepository: 3. Requests player statistics, triggering database access.
    InfrastructureLayerStatisticsRepository-->>ApplicationServicesLayer: PlayerStatistics DTO or throws UnrecoverableDataException.
    activate SQLiteEngine
    InfrastructureLayerStatisticsRepository->>SQLiteEngine: 4. Attempts to open connection to primary statistics database file.
    SQLiteEngine-->>InfrastructureLayerStatisticsRepository: Throws SqliteException due to file corruption.
    SQLiteEngine->>InfrastructureLayerStatisticsRepository: 5. throw new SqliteException("Database disk image is malformed", 11)
    InfrastructureLayerStatisticsRepository->>LoggingService: 6. Logs the initial corruption detection event.
    InfrastructureLayerStatisticsRepository->>InfrastructureLayerStatisticsRepository: 7. LOOP [For each backup file from newest to oldest (max 3)]
    InfrastructureLayerStatisticsRepository->>FileSystem: 8. Attempts to restore by copying backup file over corrupt primary.
    FileSystem-->>InfrastructureLayerStatisticsRepository: Success or throws IOException.
    InfrastructureLayerStatisticsRepository->>SQLiteEngine: 9. Re-attempts to open connection with restored file.
    SQLiteEngine-->>InfrastructureLayerStatisticsRepository: Success or throws SqliteException.
    InfrastructureLayerStatisticsRepository->>LoggingService: 10. [IF FAILED] Log warning that backup is also corrupt.
    InfrastructureLayerStatisticsRepository->>LoggingService: 11. [IF SUCCESS] Log successful recovery and break loop.
    InfrastructureLayerStatisticsRepository->>LoggingService: 12. [IF LOOP COMPLETES] Logs that all recovery attempts have failed.
    InfrastructureLayerStatisticsRepository->>ApplicationServicesLayer: 13. throw new UnrecoverableDataException(...)
    ApplicationServicesLayer->>PresentationLayer: 14. Signals UI to show data corruption dialog and await user decision.
    PresentationLayer-->>ApplicationServicesLayer: User choice (e.g., Reset or Quit).
    User->>PresentationLayer: 15. Chooses to reset statistics.
    PresentationLayer->>ApplicationServicesLayer: 16. Invokes the reset functionality based on user choice.
    ApplicationServicesLayer-->>PresentationLayer: Returns upon completion.
    ApplicationServicesLayer->>InfrastructureLayerStatisticsRepository: 17. Commands the repository to delete old data and re-initialize.
    InfrastructureLayerStatisticsRepository-->>ApplicationServicesLayer: Task completes.
    InfrastructureLayerStatisticsRepository->>FileSystem: 18. Deletes the corrupt database file and all its backups.
    InfrastructureLayerStatisticsRepository->>SQLiteEngine: 19. Creates a new, empty database file and initializes its schema.
    InfrastructureLayerStatisticsRepository->>LoggingService: 20. Logs that user statistics have been reset.

    note over InfrastructureLayerStatisticsRepository: Recovery Logic (REQ-1-089) The Infrastructure Layer is solely responsible for the multi-stage bac...
    note over PresentationLayer: Graceful Degradation (REQ-1-023) When automated recovery fails, control is passed to the user. Th...

    deactivate SQLiteEngine
    deactivate InfrastructureLayerStatisticsRepository
    deactivate ApplicationServicesLayer
    deactivate PresentationLayer
