sequenceDiagram
    participant "Human Player" as HumanPlayer
    participant "PropertyManagementUIController" as PropertyManagementUIController
    participant "PropertyActionService" as PropertyActionService
    participant "GameState" as GameState
    participant "GameState" as GameState

    activate PropertyManagementUIController
    HumanPlayer->>PropertyManagementUIController: 1. 1. Clicks 'Manage Properties' button on HUD during Pre-Roll Phase.
    PropertyManagementUIController->>GameState: 2. 2. Fetches current player and board state data for rendering.
    GameState-->>PropertyManagementUIController: 3. Returns PlayerAssetDataDTO.
    PropertyManagementUIController->>PropertyManagementUIController: 4. 4. Renders Property Management view with owned properties, cash, and dynamically enabled/disabled action buttons based on game rules.
    HumanPlayer->>PropertyManagementUIController: 5. 5. Selects a property and clicks 'Build House' button.
    activate PropertyActionService
    PropertyManagementUIController->>PropertyActionService: 6. 6. Submits build request.
    PropertyActionService-->>PropertyManagementUIController: 13. Returns ActionResult { IsSuccess: true }.
    activate GameState
    PropertyActionService->>GameState: 7. 7. Requests validation of the build action from the Rule Engine.
    GameState-->>PropertyActionService: 8. Returns RuleValidationResult { IsValid: true }.
    PropertyActionService->>GameState: 7. 7a. [ALT: Validation Fails] RuleEngine returns RuleValidationResult { IsValid: false, Reason: UnevenBuilding }.
    PropertyActionService->>PropertyManagementUIController: 7. 7b. [ALT] Returns ActionResult { IsSuccess: false, Error: 'Building must be even' }.
    PropertyActionService->>GameState: 9. 9. Mutates GameState: debits player cash, increments property house count, decrements bank house supply.
    PropertyActionService->>PropertyActionService: 10. 10. Publishes GameStateUpdated event to in-process event bus.
    PropertyManagementUIController->>PropertyManagementUIController: 11. 11. [Subscriber] Receives GameStateUpdated event.
    PropertyManagementUIController->>PropertyManagementUIController: 12. 12. Re-fetches state and re-renders view to reflect changes (updated cash, new house icon, updated button states).


    deactivate GameState
    deactivate PropertyActionService
    deactivate PropertyManagementUIController
