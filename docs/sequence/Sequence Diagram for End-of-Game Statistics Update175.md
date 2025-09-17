sequenceDiagram
    participant "ApplicationServicesLayer" as ApplicationServicesLayer
    participant "InfrastructureLayer (StatisticsRepository)" as InfrastructureLayerStatisticsRepository
    participant "SQLite Database" as SQLiteDatabase

    activate ApplicationServicesLayer
    ApplicationServicesLayer->>ApplicationServicesLayer: 1. 1. GameEnded event is handled by GameSessionService
    ApplicationServicesLayer->>ApplicationServicesLayer: 2. 1.1. Transform final GameState into GameResult DTO
    ApplicationServicesLayer-->>ApplicationServicesLayer: gameResult: A DTO containing aggregated results (winner, duration, net worths, etc.).
    activate InfrastructureLayerStatisticsRepository
    ApplicationServicesLayer->>InfrastructureLayerStatisticsRepository: 3. 2. UpdatePlayerStatisticsAsync(gameResult)
    InfrastructureLayerStatisticsRepository-->>ApplicationServicesLayer: Task (indicating completion)
    InfrastructureLayerStatisticsRepository->>InfrastructureLayerStatisticsRepository: 4. 2.1. Open connection and begin transaction
    InfrastructureLayerStatisticsRepository-->>InfrastructureLayerStatisticsRepository: dbTransaction
    InfrastructureLayerStatisticsRepository->>SQLiteDatabase: 5. 2.2. UPDATE PlayerStatistics table for human player
    SQLiteDatabase-->>InfrastructureLayerStatisticsRepository: Rows Affected
    InfrastructureLayerStatisticsRepository->>SQLiteDatabase: 6. 2.3. INSERT record into GameHistory table
    SQLiteDatabase-->>InfrastructureLayerStatisticsRepository: New Row ID
    InfrastructureLayerStatisticsRepository->>InfrastructureLayerStatisticsRepository: 7. 2.4. [Conditional] Check if win qualifies for Top 10 Scores
    InfrastructureLayerStatisticsRepository-->>InfrastructureLayerStatisticsRepository: isHighScore: boolean
    InfrastructureLayerStatisticsRepository->>SQLiteDatabase: 8. 2.5. [Conditional] Update TopScores table
    SQLiteDatabase-->>InfrastructureLayerStatisticsRepository: Rows Affected
    InfrastructureLayerStatisticsRepository->>SQLiteDatabase: 9. 2.6. Commit Transaction
    SQLiteDatabase-->>InfrastructureLayerStatisticsRepository: Success

    note over InfrastructureLayerStatisticsRepository: The entire database operation (steps 2.1-2.6) MUST be wrapped in a try/catch block. The catch blo...

    deactivate InfrastructureLayerStatisticsRepository
    deactivate ApplicationServicesLayer
