sequenceDiagram
    participant "TurnManagementService" as TurnManagementService
    participant "GameState" as GameState
    participant "GameState" as GameState

    activate GameState
    TurnManagementService->>GameState: 1. ResolveDebt(debtorPlayerId, creditor, debtAmount)
    GameState-->>TurnManagementService: returns BankruptcyResult
    GameState->>GameState: 2. DetermineIfBankrupt(debtorPlayerId, debtAmount)
    GameState-->>GameState: returns bool
    GameState->>GameState: 5. [ALT: Creditor is Player vs. Bank]
    GameState->>GameState: 10. GameState.UpdatePlayerStatus(debtorPlayerId, PlayerStatus.Bankrupt)
    TurnManagementService->>TurnManagementService: 11. ProcessBankruptcyResult(result)
    TurnManagementService->>TurnManagementService: 12. RemovePlayerFromTurnOrder(result.BankruptPlayerId)
    TurnManagementService->>TurnManagementService: 13. [LOOP: For each property returned to bank]

    note over GameState: Business Rule Enforcement: The RuleEngine is the sole authority on bankruptcy determination and a...
    note over GameState: Atomicity Requirement: The asset transfer process within the GameState (steps 7 and 9) must be tr...
    note over TurnManagementService: Audit Trail: A successful bankruptcy must be logged at the INFO level with key details: Debtor, C...

    deactivate GameState
