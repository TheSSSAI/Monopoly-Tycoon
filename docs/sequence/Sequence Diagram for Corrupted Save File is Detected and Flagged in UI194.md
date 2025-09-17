sequenceDiagram
    actor "User" as User
    participant "LoadGameUIController" as LoadGameUIController
    participant "GameSessionService" as GameSessionService
    participant "GameSaveRepository" as GameSaveRepository
    participant "LoggingService" as LoggingService

    activate LoadGameUIController
    User->>LoadGameUIController: 1. User clicks the 'Load Game' button from the Main Menu.
    activate GameSessionService
    LoadGameUIController->>GameSessionService: 2. Request list of all save game slots with their metadata.
    GameSessionService-->>LoadGameUIController: Returns a list of SaveGameMetadata objects.
    activate GameSaveRepository
    GameSessionService->>GameSaveRepository: 3. Invoke repository to list all available save files and validate their integrity.
    GameSaveRepository-->>GameSessionService: Returns a list of SaveGameMetadata, each with a status (Valid, Corrupted, Empty).
    GameSaveRepository->>GameSaveRepository: 4. Loop through each potential save slot (e.g., 1-5).
    GameSaveRepository->>GameSaveRepository: 5. ALT: Checksum Validation Fails for a save file.
    activate LoggingService
    GameSaveRepository->>LoggingService: 5.1. Log the data corruption event at ERROR level for diagnostics.
    GameSaveRepository->>GameSaveRepository: 5.2. Create SaveGameMetadata object with status 'Corrupted'.
    GameSaveRepository->>GameSessionService: 6. Return List<SaveGameMetadata> to service layer.
    GameSessionService->>LoadGameUIController: 7. Forward List<SaveGameMetadata> to UI controller.
    LoadGameUIController->>LoadGameUIController: 8. Iterate through the returned metadata and render the UI for each slot.
    LoadGameUIController->>LoadGameUIController: 9. If metadata.Status is 'Corrupted', disable the 'Load' button, apply a visual indicator (e.g., gray out, warning icon), and set a tooltip explaining the file is unusable.

    note over GameSaveRepository: The checksum validation logic is critical. It involves reading the file, separating the stored ch...
    note over LoadGameUIController: The user-facing communication is key for this error handling sequence. The UI must clearly and pa...

    deactivate LoggingService
    deactivate GameSaveRepository
    deactivate GameSessionService
    deactivate LoadGameUIController
