sequenceDiagram
    participant "Domain Layer" as DomainLayer
    participant "Application Services Layer" as ApplicationServicesLayer
    participant "Infrastructure Layer" as InfrastructureLayer
    participant "Presentation Layer" as PresentationLayer

    activate DomainLayer
    ApplicationServicesLayer->>DomainLayer: 1. RuleEngine processes player bankruptcy, which fulfills the game's win/loss condition.
    DomainLayer-->>ApplicationServicesLayer: Returns GameEndResult DTO containing winner, final player states, and game duration.
    activate ApplicationServicesLayer
    ApplicationServicesLayer->>ApplicationServicesLayer: 2. GameSessionService receives GameEndResult and initiates the game finalization process.
    activate InfrastructureLayer
    ApplicationServicesLayer->>InfrastructureLayer: 3. Invokes persistence logic via IStatisticsRepository interface (defined in REPO-AA-004).
    InfrastructureLayer-->>ApplicationServicesLayer: Task completes, indicating success or failure of the database transaction.
    InfrastructureLayer->>InfrastructureLayer: 3.1. Begins a new SQLite transaction to ensure atomicity of all subsequent writes.
    InfrastructureLayer->>InfrastructureLayer: 3.2. Inserts a new record into the GameResults table with summary data.
    InfrastructureLayer->>InfrastructureLayer: 3.3. Updates the aggregate PlayerStatistics table for the human player.
    InfrastructureLayer->>InfrastructureLayer: 3.4. Commits the transaction, making all data changes permanent.
    activate PresentationLayer
    ApplicationServicesLayer->>PresentationLayer: 4. Commands the ViewManager to display the Game Summary screen with final stats.

    note over InfrastructureLayer: Business Rule Enforcement: The atomicity of the statistics update is guaranteed by wrapping all d...
    note over ApplicationServicesLayer: Architectural Pattern: The Application Service Layer (REPO-AS-005) acts as an orchestrator, decou...
    note over InfrastructureLayer: Audit Trail: The successful completion of interaction 3.2 creates an auditable record of the comp...

    deactivate PresentationLayer
    deactivate InfrastructureLayer
    deactivate ApplicationServicesLayer
    deactivate DomainLayer
