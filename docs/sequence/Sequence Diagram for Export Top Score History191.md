sequenceDiagram
    participant "TopScoresView" as TopScoresView
    participant "StatisticsService" as StatisticsService
    participant "StatisticsRepository" as StatisticsRepository
    participant "FileSystemRepository" as FileSystemRepository

    activate TopScoresView
    TopScoresView->>TopScoresView: 1. User clicks 'Export' button. The view invokes a native OS file save dialog.
    TopScoresView-->>TopScoresView: Returns selected file path or null if cancelled.
    activate StatisticsService
    TopScoresView->>StatisticsService: 2. Request to export top scores to the selected file path.
    StatisticsService-->>TopScoresView: Task<ExportResult> indicating success or failure and a user-facing message.
    activate StatisticsRepository
    StatisticsService->>StatisticsRepository: 3. Request the list of top 10 scores.
    StatisticsRepository-->>StatisticsService: Task<List<TopScore>> containing the top 10 score records.
    StatisticsRepository->>StatisticsRepository: 4. Execute optimized query against SQLite DB and map results.
    StatisticsRepository-->>StatisticsRepository: Returns a DbDataReader with the query results.
    StatisticsRepository->>StatisticsRepository: 5. Map DbDataReader rows to a List<TopScore> DTOs.
    StatisticsRepository-->>StatisticsRepository: Populated List<TopScore>.
    StatisticsService->>StatisticsService: 6. Format the list of TopScore objects into a human-readable string.
    StatisticsService-->>StatisticsService: Formatted string content for the .txt file.
    activate FileSystemRepository
    StatisticsService->>FileSystemRepository: 7. Request to write the formatted string to the specified file.
    FileSystemRepository-->>StatisticsService: Task representing the completion of the file write operation.
    StatisticsService->>TopScoresView: 8. Return the final result of the export operation.
    TopScoresView->>TopScoresView: 9. Display a success or failure notification to the user.

    note over TopScoresView: The initial file path selection (Seq 1) is a critical branching point. If the user cancels the fi...

    deactivate FileSystemRepository
    deactivate StatisticsRepository
    deactivate StatisticsService
    deactivate TopScoresView
