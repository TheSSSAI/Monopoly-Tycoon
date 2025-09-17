sequenceDiagram
    participant "Player (Human)" as PlayerHuman
    participant "Presentation Layer (UI)" as PresentationLayerUI
    participant "Application Services" as ApplicationServices
    participant "Domain Layer" as DomainLayer

    activate PresentationLayerUI
    PlayerHuman->>PresentationLayerUI: 1. Clicks 'Manage Properties' button on HUD
    PresentationLayerUI->>PresentationLayerUI: 2. ViewManager displays PropertyManagement screen
    activate ApplicationServices
    PresentationLayerUI->>ApplicationServices: 3. PropertyManagementViewModel requests current player asset data
    ApplicationServices-->>PresentationLayerUI: Returns PlayerAssetViewModel DTO
    activate DomainLayer
    ApplicationServices->>DomainLayer: 4. Retrieves current PlayerState from GameState
    DomainLayer-->>ApplicationServices: PlayerState object
    PresentationLayerUI->>PresentationLayerUI: 5. Populates UI with player's properties and cash from ViewModel
    PlayerHuman->>PresentationLayerUI: 6. Selects a valid property and clicks 'Build House'
    PresentationLayerUI->>ApplicationServices: 7. Sends BuildHouseRequest to PropertyActionService
    ApplicationServices-->>PresentationLayerUI: Returns BuildHouseResult DTO
    ApplicationServices->>DomainLayer: 8. Validates action with RuleEngine
    DomainLayer-->>ApplicationServices: Returns successful ValidationResult
    ApplicationServices->>DomainLayer: 9. Applies state change to GameState
    DomainLayer-->>ApplicationServices: Returns updated PlayerState
    DomainLayer->>DomainLayer: 10. Publishes GameStateUpdated event
    ApplicationServices->>PresentationLayerUI: 11. Confirms success to UI controller
    PresentationLayerUI->>PresentationLayerUI: 12. Updates PropertyManagementViewModel and triggers UI refresh
    PresentationLayerUI->>PresentationLayerUI: 13. GameBoardPresenter, listening for GameStateUpdated event, adds house model to board

    note over PresentationLayerUI: The Presentation Layer uses the Model-View-ViewModel (MVVM) pattern. The ViewModel requests data,...
    note over DomainLayer: The RuleEngine is a stateless service. It takes the current game state and a proposed action, and...
    note over ApplicationServices: Error Scenario: If the RuleEngine returns a validation failure (e.g., InsufficientFunds), the Pro...

    deactivate DomainLayer
    deactivate ApplicationServices
    deactivate PresentationLayerUI
