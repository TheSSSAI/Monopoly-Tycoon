sequenceDiagram
    actor "Client / Game Loop" as ClientGameLoop
    participant "Application Services" as ApplicationServices
    participant "Domain Logic" as DomainLogic
    participant "Infrastructure" as Infrastructure

    activate ApplicationServices
    ClientGameLoop->>ApplicationServices: 1. Start next turn. TurnManagementService.ProcessNextTurnAsync()
    ApplicationServices->>ApplicationServices: 2. Identify current player is AI. Invoke AIService.ExecuteTurnAsync(gameState).
    ApplicationServices-->>ApplicationServices: Return: Task (signals turn completion)
    ApplicationServices->>Infrastructure: 3. Log start of AI turn. ILogger.Information("Executing turn for {PlayerName}")
    activate DomainLogic
    ApplicationServices->>DomainLogic: 4. Request pre-roll action. AIBehaviorTreeExecutor.GetNextActionAsync(gameState, 'PreRoll')
    DomainLogic-->>ApplicationServices: Return: AIAction (e.g., BuildHouseAction) or null
    DomainLogic->>Infrastructure: 4.1. Load AI config. JsonConfig.Get<AIParams>(aiDifficulty)
    Infrastructure-->>DomainLogic: Return: AIParams object (cached on first load)
    Infrastructure->>DomainLogic: 4.2. Return AI Behavior Parameters
    DomainLogic->>Infrastructure: 4.3. Log decision path. ILogger.Debug("Node {NodeName} evaluated to {Result}")
    DomainLogic->>ApplicationServices: 5. Return proposed action (BuildHouseAction)
    ApplicationServices->>ApplicationServices: 6. Dispatch action. PropertyActionService.ExecuteBuildActionAsync(gameState, action)
    ApplicationServices-->>ApplicationServices: Return: ActionResult (Success or Failure with reason)
    ApplicationServices->>DomainLogic: 7. Validate action. RuleEngine.ValidateBuildAction(gameState, action)
    DomainLogic-->>ApplicationServices: Return: ValidationResult (contains IsValid and ErrorMessage)
    DomainLogic->>ApplicationServices: 8. Return ValidationResult.Success
    ApplicationServices->>DomainLogic: 9. Apply changes to GameState object (e.g., gameState.Bank.Houses--, player.Cash -= cost)
    ApplicationServices->>Infrastructure: 10. Log transaction audit. ILogger.Information("Transaction: {Type}...")
    ApplicationServices->>ApplicationServices: 11. Loop back to step 4 until no more pre-roll actions are proposed.
    ApplicationServices->>ApplicationServices: 12. Complete turn (roll dice, move, land on space action) using same pattern (request action -> validate -> execute).
    ApplicationServices->>ClientGameLoop: 13. Notify Turn Complete (event publication or callback)

    note over DomainLogic: Validation failures from the RuleEngine are not exceptions. They are expected outcomes that the A...

    deactivate DomainLogic
    deactivate ApplicationServices
