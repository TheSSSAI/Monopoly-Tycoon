sequenceDiagram
    participant "Presentation Layer (UI Controller)" as PresentationLayerUIController
    participant "Application Service (GameSessionService)" as ApplicationServiceGameSessionService
    participant "Domain Model (GameState)" as DomainModelGameState
    participant "Repository Abstraction (ISaveGameRepository)" as RepositoryAbstractionISaveGameRepository
    participant "Infrastructure (GameSaveRepository)" as InfrastructureGameSaveRepository

    activate ApplicationServiceGameSessionService
    PresentationLayerUIController->>ApplicationServiceGameSessionService: 1. 1. RequestSaveGameAsync(saveSlot: int)
    ApplicationServiceGameSessionService-->>PresentationLayerUIController: 8. return Task<bool> (saveSuccess)
    ApplicationServiceGameSessionService->>DomainModelGameState: 2. 2. GetCurrentGameState()
    DomainModelGameState-->>ApplicationServiceGameSessionService: currentGameState: GameState
    activate RepositoryAbstractionISaveGameRepository
    ApplicationServiceGameSessionService->>RepositoryAbstractionISaveGameRepository: 3. 3. SaveAsync(gameState: GameState, slot: int)
    RepositoryAbstractionISaveGameRepository-->>ApplicationServiceGameSessionService: 7. return Task<bool> (writeSuccess)
    activate InfrastructureGameSaveRepository
    RepositoryAbstractionISaveGameRepository->>InfrastructureGameSaveRepository: 4. 4. [DI Resolution] SaveAsync is invoked on the concrete implementation
    InfrastructureGameSaveRepository->>InfrastructureGameSaveRepository: 4.1. 4.1. Serialize GameState to JSON
    InfrastructureGameSaveRepository-->>InfrastructureGameSaveRepository: jsonString: string
    InfrastructureGameSaveRepository->>InfrastructureGameSaveRepository: 4.2. 4.2. Calculate SHA256 checksum of JSON string
    InfrastructureGameSaveRepository-->>InfrastructureGameSaveRepository: checksum: string
    InfrastructureGameSaveRepository->>InfrastructureGameSaveRepository: 4.3. 4.3. Write versioned JSON and checksum to file
    InfrastructureGameSaveRepository-->>InfrastructureGameSaveRepository: Task<bool>
    InfrastructureGameSaveRepository->>RepositoryAbstractionISaveGameRepository: 5. 5. return Task.FromResult(true)
    RepositoryAbstractionISaveGameRepository->>ApplicationServiceGameSessionService: 6. 6. Awaited task completes
    ApplicationServiceGameSessionService->>PresentationLayerUIController: 7. 7. Awaited task completes, returning success status
    PresentationLayerUIController->>PresentationLayerUIController: 8. 8. DisplaySaveResult(success: bool)

    note over PresentationLayerUIController: Business Rule Check (REQ-1-085): The 'Save Game' button in the UI (REPO-PRES-001) must be disable...
    note over InfrastructureGameSaveRepository: The Infrastructure Layer (REPO-IL-006) must be completely unaware of any game rules. Its sole res...

    deactivate InfrastructureGameSaveRepository
    deactivate RepositoryAbstractionISaveGameRepository
    deactivate ApplicationServiceGameSessionService
