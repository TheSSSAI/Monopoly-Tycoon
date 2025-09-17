sequenceDiagram
    participant "Human Player" as HumanPlayer
    participant "SetupScreenUI" as SetupScreenUI
    participant "GameSessionService" as GameSessionService
    participant "PlayerProfileRepository" as PlayerProfileRepository

    activate SetupScreenUI
    HumanPlayer->>SetupScreenUI: 1. Enters profile name into input field.
    SetupScreenUI->>SetupScreenUI: 2. ValidateProfileName(profileName)
    SetupScreenUI-->>SetupScreenUI: isValid: bool, errorMessage: string
    HumanPlayer->>SetupScreenUI: 3. Clicks 'Start Game' button.
    activate GameSessionService
    SetupScreenUI->>GameSessionService: 4. StartNewGameAsync(profileName, gameSettings)
    GameSessionService-->>SetupScreenUI: Task<GameSession>
    activate PlayerProfileRepository
    GameSessionService->>PlayerProfileRepository: 5. GetOrCreateProfileAsync(displayName)
    PlayerProfileRepository-->>GameSessionService: Task<PlayerProfile>
    PlayerProfileRepository->>PlayerProfileRepository: 6. Query: SELECT * FROM PlayerProfiles WHERE DisplayName = @name LIMIT 1
    PlayerProfileRepository-->>PlayerProfileRepository: PlayerProfile row or NULL
    PlayerProfileRepository->>PlayerProfileRepository: 7. Command: INSERT INTO PlayerProfiles (ProfileId, DisplayName, ...) VALUES (@id, @name, ...)
    PlayerProfileRepository-->>PlayerProfileRepository: Rows affected (1)
    GameSessionService->>GameSessionService: 8. InitializeGameState(humanProfile, gameSettings)
    GameSessionService-->>GameSessionService: GameState
    SetupScreenUI->>SetupScreenUI: 9. TransitionToGameBoardScene()

    note over SetupScreenUI: Real-time validation (step 2) occurs on every keystroke in the UI to provide immediate feedback, ...
    note over PlayerProfileRepository: The database interaction in step 7 is a conditional 'Create'. The profile is only created if it d...

    deactivate PlayerProfileRepository
    deactivate GameSessionService
    deactivate SetupScreenUI
