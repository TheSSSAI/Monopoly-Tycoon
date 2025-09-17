sequenceDiagram
    participant "PropertyManagementUIController" as PropertyManagementUIController
    participant "PropertyActionService" as PropertyActionService
    participant "GameState" as GameState
    participant "GameState" as GameState
    participant "LoggingService" as LoggingService

    activate PropertyManagementUIController
    PropertyManagementUIController->>PropertyManagementUIController: 1. User clicks 'Build House' button for a specific property.
    activate PropertyActionService
    PropertyManagementUIController->>PropertyActionService: 2. Request to build a house on the selected property.
    PropertyActionService-->>PropertyManagementUIController: Returns result indicating success and updated state.
    PropertyActionService->>GameState: 3. Get current game state to perform validation.
    GameState-->>PropertyActionService: Returns a deep copy or immutable view of the current GameState.
    activate GameState
    PropertyActionService->>GameState: 4. Validate build action and compute resulting state.
    GameState-->>PropertyActionService: Returns a result object with the new GameState on success.
    GameState->>GameState: 4.1. Verify player owns a full monopoly for the property.
    GameState-->>GameState: boolean
    GameState->>GameState: 4.2. Enforce 'Even Building' rule (BR-001).
    GameState-->>GameState: boolean
    GameState->>GameState: 4.3. Check for sufficient player cash and bank supply.
    GameState-->>GameState: boolean
    PropertyActionService->>GameState: 5. Commit the new game state.
    activate LoggingService
    PropertyActionService->>LoggingService: 6. Log successful build transaction for audit.
    PropertyManagementUIController->>PropertyManagementUIController: 7. Update UI to reflect new state.

    note over PropertyActionService: Alternative UI Update: The PropertyActionService could publish a GameStateUpdated event. The Prop...
    note over GameState: The RuleEngine is designed to be stateless. It receives the current state, validates an action, a...

    deactivate LoggingService
    deactivate GameState
    deactivate PropertyActionService
    deactivate PropertyManagementUIController
