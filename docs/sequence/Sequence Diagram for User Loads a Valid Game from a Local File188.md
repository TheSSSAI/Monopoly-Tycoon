sequenceDiagram
    participant "Presentation Layer" as PresentationLayer
    participant "Application Services" as ApplicationServices
    participant "Infrastructure Layer" as InfrastructureLayer
    participant "Domain Model" as DomainModel

    activate ApplicationServices
    PresentationLayer->>ApplicationServices: 1. User selects save slot 3. Triggers call to load the game.
    ApplicationServices-->>PresentationLayer: Returns success/failure result to UI for scene transition.
    activate InfrastructureLayer
    ApplicationServices->>InfrastructureLayer: 2. Request loading of game state from persistence.
    InfrastructureLayer-->>ApplicationServices: Returns the deserialized GameState object.
    InfrastructureLayer->>InfrastructureLayer: 2.1. Read file content from '%APPDATA%/MonopolyTycoon/saves/save_3.json'.
    InfrastructureLayer->>InfrastructureLayer: 2.2. Validate file integrity using stored checksum (REQ-1-088).
    InfrastructureLayer->>InfrastructureLayer: 2.3. Check gameVersion property for compatibility (REQ-1-090).
    InfrastructureLayer->>InfrastructureLayer: 2.4. [Opt] If version is old, invoke DataMigrationManager to upgrade JSON data.
    InfrastructureLayer-->>InfrastructureLayer: Returns migrated JSON data.
    InfrastructureLayer->>DomainModel: 2.5. Deserialize valid JSON into GameState object.
    DomainModel-->>InfrastructureLayer: Populated GameState object.
    ApplicationServices->>DomainModel: 3. Set the loaded GameState as the current active session.
    PresentationLayer->>PresentationLayer: 4. Transition to main game scene and render the loaded state.

    note over ApplicationServices: The call from Application Services to the Infrastructure Layer is mediated by the ISaveGameReposi...
    note over PresentationLayer: Error Handling: If GameSaveRepository.LoadAsync throws any exception, GameSessionService catches ...

    deactivate InfrastructureLayer
    deactivate ApplicationServices
