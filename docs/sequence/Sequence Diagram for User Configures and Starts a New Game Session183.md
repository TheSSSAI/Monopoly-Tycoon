sequenceDiagram
    actor "User" as User
    participant "Presentation Layer" as PresentationLayer
    participant "Application Services Layer" as ApplicationServicesLayer
    participant "Infrastructure Layer" as InfrastructureLayer
    participant "Domain Layer" as DomainLayer

    activate PresentationLayer
    User->>PresentationLayer: 1. Enters player name and selects game configuration (AI count, difficulties, token).
    User->>PresentationLayer: 2. Clicks 'Start Game' button.
    PresentationLayer->>PresentationLayer: 3. ValidateGameConfiguration(viewModel)
    PresentationLayer-->>PresentationLayer: bool isValid
    PresentationLayer->>PresentationLayer: 4. DisplayLoadingOverlay('Creating game...')
    activate ApplicationServicesLayer
    PresentationLayer->>ApplicationServicesLayer: 5. StartNewGameAsync(gameSetupDto)
    ApplicationServicesLayer-->>PresentationLayer: Task<StartGameResult>
    activate InfrastructureLayer
    ApplicationServicesLayer->>InfrastructureLayer: 6. IStatisticsRepository.GetOrCreateProfileAsync(playerName)
    InfrastructureLayer-->>ApplicationServicesLayer: Task<PlayerProfile>
    InfrastructureLayer->>InfrastructureLayer: 6.1. SELECT * FROM PlayerProfiles WHERE Name = @name
    InfrastructureLayer-->>InfrastructureLayer: DbDataReader (0 or 1 row)
    InfrastructureLayer->>InfrastructureLayer: 6.2. [IF NOT EXISTS] INSERT INTO PlayerProfiles ...
    InfrastructureLayer-->>InfrastructureLayer: int rowsAffected
    activate DomainLayer
    ApplicationServicesLayer->>DomainLayer: 7. new GameState(gameSetupDto, playerProfile)
    DomainLayer-->>ApplicationServicesLayer: GameState instance
    DomainLayer->>DomainLayer: 8. InitializeGame()
    ApplicationServicesLayer->>ApplicationServicesLayer: 9. SetCurrentSession(gameState)
    PresentationLayer->>PresentationLayer: 10. HideLoadingOverlay()
    PresentationLayer->>PresentationLayer: 11. LoadSceneAsync('GameBoardScene')

    note over ApplicationServicesLayer: The call from GameSessionService (REPO-AS-005) to StatisticsRepository (REPO-IL-006) is decoupled...
    note over DomainLayer: All game rule logic for initial setup (starting cash, property states, card deck order) is encaps...

    deactivate DomainLayer
    deactivate InfrastructureLayer
    deactivate ApplicationServicesLayer
    deactivate PresentationLayer
