sequenceDiagram
    participant "GameSessionService" as GameSessionService
    participant "StatisticsRepository" as StatisticsRepository
    participant "LoggingService" as LoggingService
    participant "PresentationLayer" as PresentationLayer

    activate StatisticsRepository
    GameSessionService->>StatisticsRepository: 1. 1. InitializeDatabaseAsync()
    StatisticsRepository-->>GameSessionService: Returns Task on success, or throws UnrecoverableDataException on failure.
    StatisticsRepository->>LoggingService: 2. 2. Log(INFO, 'Initializing statistics database...')
    StatisticsRepository->>StatisticsRepository: 3. 3. Attempt to open SQLite connection. Catches SqliteException.
    StatisticsRepository-->>StatisticsRepository: Connection object on success, SqliteException on corruption.
    StatisticsRepository->>LoggingService: 4. 4. [on Exception] Log(WARN, 'Primary database corrupt. Starting recovery...')
    StatisticsRepository->>StatisticsRepository: 5. 5. [loop: for each backup file from newest to oldest (3 attempts)]
    StatisticsRepository->>LoggingService: 6. 5.1. Log(INFO, 'Attempting restore from {BackupFile}...')
    StatisticsRepository->>StatisticsRepository: 7. 5.2. Atomically replace corrupt DB with backup file.
    StatisticsRepository->>StatisticsRepository: 8. 5.3. Re-attempt to open SQLite connection.
    StatisticsRepository->>LoggingService: 9. 5.4. [alt: Restore Succeeded OR Restore Failed]
    StatisticsRepository->>LoggingService: 12. 6. [if loop completed without success] Log(ERROR, 'All recovery attempts failed. Data is unrecoverable.')
    StatisticsRepository->>GameSessionService: 13. 7. [if unrecoverable] throw new UnrecoverableDataException('Statistics database is corrupt.')
    activate PresentationLayer
    GameSessionService->>PresentationLayer: 14. 8. [on Exception] DisplayDataCorruptionDialog(type: 'Statistics')
    PresentationLayer->>GameSessionService: 15. 9. [alt: User clicks 'Reset Data' OR 'Exit']
    PresentationLayer->>GameSessionService: 16. 9.1. [if Reset] RequestStatisticsReset()
    GameSessionService->>StatisticsRepository: 17. 9.1.1. ResetStatisticsDataAsync()
    PresentationLayer->>PresentationLayer: 18. 9.2. [if Exit] QuitApplication()

    note over StatisticsRepository: The file replacement operation (Step 5.2) MUST be atomic to prevent a failed copy from leaving th...
    note over PresentationLayer: The user-facing dialog is the final step in the recovery process. Its purpose is to clearly commu...

    deactivate PresentationLayer
    deactivate StatisticsRepository
