sequenceDiagram
    participant "Presentation Layer (UI)" as PresentationLayerUI
    participant "Application Services Layer" as ApplicationServicesLayer
    participant "Infrastructure Layer" as InfrastructureLayer

    activate PresentationLayerUI
    PresentationLayerUI->>PresentationLayerUI: 1. User clicks 'Player Stats' button on the main menu.
    activate ApplicationServicesLayer
    PresentationLayerUI->>ApplicationServicesLayer: 2. Request player statistics for the current profile.
    ApplicationServicesLayer-->>PresentationLayerUI: Returns PlayerStatisticsDto containing aggregated stats and top scores, or null if not found.
    activate InfrastructureLayer
    ApplicationServicesLayer->>InfrastructureLayer: 3. Invoke repository to fetch statistics data from the database.
    InfrastructureLayer-->>ApplicationServicesLayer: Returns a data transfer object with player statistics.
    InfrastructureLayer->>InfrastructureLayer: 4. Execute SQL queries against local SQLite database.
    InfrastructureLayer-->>InfrastructureLayer: Returns database rows for stats and top scores.
    InfrastructureLayer->>InfrastructureLayer: 5. Map database results to PlayerStatisticsDto.
    PresentationLayerUI->>PresentationLayerUI: 6. [Success Path] Populate UI fields with data from the returned PlayerStatisticsDto.
    PresentationLayerUI->>PresentationLayerUI: 7. [Failure Path] Display a user-friendly error message.

    note over ApplicationServicesLayer: Dependency Injection: The Application Services Layer depends on the IStatisticsRepository interfa...
    note over InfrastructureLayer: Data Transfer Objects (DTOs): The repository is responsible for mapping raw database entities int...

    deactivate InfrastructureLayer
    deactivate ApplicationServicesLayer
    deactivate PresentationLayerUI
