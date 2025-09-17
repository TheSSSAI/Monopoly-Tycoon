sequenceDiagram
    participant "Player" as Player
    participant "SettingsUIController" as SettingsUIController
    participant "AudioManager" as AudioManager
    participant "GameSessionService" as GameSessionService
    participant "StatisticsRepository" as StatisticsRepository
    participant "SaveGameRepository" as SaveGameRepository

    activate SettingsUIController
    Player->>SettingsUIController: 1. 1. Clicks 'Settings' button during gameplay.
    SettingsUIController->>SettingsUIController: 2. 2. Pauses game simulation and displays Settings UI overlay.
    Player->>SettingsUIController: 3. 3. Adjusts 'Music Volume' slider.
    activate AudioManager
    SettingsUIController->>AudioManager: 4. 4. Sets new music volume.
    AudioManager-->>SettingsUIController: 5. void
    Player->>SettingsUIController: 6. 6. Clicks 'Reset All Statistics' button.
    SettingsUIController->>SettingsUIController: 7. 7. Displays a confirmation modal dialog.
    Player->>SettingsUIController: 8. 7.1. Clicks 'Confirm' on modal dialog.
    activate GameSessionService
    SettingsUIController->>GameSessionService: 9. 8. Requests to reset player statistics.
    GameSessionService-->>SettingsUIController: 13. Returns operation result.
    activate StatisticsRepository
    GameSessionService->>StatisticsRepository: 10. 9. Deletes all player statistics records.
    StatisticsRepository-->>GameSessionService: 12. Returns success or failure.
    StatisticsRepository->>StatisticsRepository: 11. 10. Executes SQL DELETE FROM PlayerStatistics;.
    StatisticsRepository-->>StatisticsRepository: 11. DB operation completes.
    GameSessionService->>SettingsUIController: 14. 14. Displays success or failure notification to user.
    Player->>SettingsUIController: 15. 15. Clicks 'Delete All Save Files' button and confirms.
    SettingsUIController->>GameSessionService: 16. 16. Requests to delete all save files.
    GameSessionService-->>SettingsUIController: 21. Returns operation result.
    activate SaveGameRepository
    GameSessionService->>SaveGameRepository: 17. 17. Deletes all save game files.
    SaveGameRepository-->>GameSessionService: 20. Returns success or failure.
    SaveGameRepository->>SaveGameRepository: 18. 18. Iterates through save directory and deletes each file.
    SaveGameRepository-->>SaveGameRepository: 19. All files deleted.
    SettingsUIController->>SettingsUIController: 22. 22. Displays success/failure notification and closes settings menu.

    note over GameSessionService: The Result type returned from services is a custom discriminated union or similar pattern to clea...

    deactivate SaveGameRepository
    deactivate StatisticsRepository
    deactivate GameSessionService
    deactivate AudioManager
    deactivate SettingsUIController
