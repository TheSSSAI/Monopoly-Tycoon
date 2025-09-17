sequenceDiagram
    actor "User" as User
    participant "LoadGameUIController" as LoadGameUIController
    participant "GameSessionService" as GameSessionService
    participant "SaveGameRepository" as SaveGameRepository
    participant "LoggingService" as LoggingService

    activate LoadGameUIController
    User->>LoadGameUIController: 1. Clicks 'Load Game' button
    activate GameSessionService
    LoadGameUIController->>GameSessionService: 2. Request list of available save games
    GameSessionService-->>LoadGameUIController: Returns list of SaveGameMetadata DTOs
    activate SaveGameRepository
    GameSessionService->>SaveGameRepository: 3. Delegate request to fetch save game metadata
    SaveGameRepository-->>GameSessionService: Returns list of SaveGameMetadata DTOs
    SaveGameRepository->>SaveGameRepository: 4. loop: For each physical save file slot
    SaveGameRepository->>SaveGameRepository: 5. Read file content from disk: 'save_slot_X.json'
    SaveGameRepository-->>SaveGameRepository: Returns file bytes
    SaveGameRepository->>SaveGameRepository: 6. Compute checksum of file content and compare with stored checksum
    SaveGameRepository-->>SaveGameRepository: Returns boolean match result
    SaveGameRepository->>SaveGameRepository: 7. alt: [Checksum Mismatch]
    activate LoggingService
    SaveGameRepository->>LoggingService: 8. Log high severity data integrity error
    SaveGameRepository->>SaveGameRepository: 9. Create SaveGameMetadata DTO with status 'Corrupted'
    SaveGameRepository->>SaveGameRepository: 10. end loop
    SaveGameRepository->>GameSessionService: 11. Return list of all save file metadata
    GameSessionService->>LoadGameUIController: 12. Return list of all save file metadata
    LoadGameUIController->>LoadGameUIController: 13. loop: For each SaveGameMetadata DTO in the returned list
    LoadGameUIController->>LoadGameUIController: 14. Update UI for the corresponding save slot based on its status
    LoadGameUIController->>User: 15. Displays fully populated Load Game menu with one slot marked as unusable

    note over SaveGameRepository: The checksum validation is the core error detection mechanism required by REQ-1-088. This prevent...
    note over LoadGameUIController: The user is informed of the data corruption via a passive UI state change. This is a form of grac...

    deactivate LoggingService
    deactivate SaveGameRepository
    deactivate GameSessionService
    deactivate LoadGameUIController
