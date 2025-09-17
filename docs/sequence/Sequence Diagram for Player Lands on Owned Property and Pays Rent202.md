sequenceDiagram
    participant "Presentation Layer" as PresentationLayer
    participant "TurnManagementService" as TurnManagementService
    participant "RuleEngine" as RuleEngine
    participant "GameState" as GameState

    activate TurnManagementService
    PresentationLayer->>TurnManagementService: 1. Player movement animation completes. Signals the end of the movement phase.
    activate RuleEngine
    TurnManagementService->>RuleEngine: 2. Requests the Domain Layer to process the rent transaction for the current player.
    RuleEngine-->>TurnManagementService: Returns a result object indicating the outcome (RentPaid, NoRentDue, or BankruptcyRequired).
    activate GameState
    RuleEngine->>GameState: 3. Reads property data, owner status, and development level from the GameState.
    GameState-->>RuleEngine: Property, Owner, and Renter state objects.
    RuleEngine->>RuleEngine: 4. Calculates rent based on rules: checks for mortgage, monopoly, development level, and property type (e.g., Railroads).
    RuleEngine-->>RuleEngine: Calculated rent amount.
    RuleEngine->>GameState: 5. Applies the transaction: debits renter's cash and credits owner's cash directly within the GameState.
    TurnManagementService->>PresentationLayer: 6. Notifies the UI about the successful rent payment event.
    PresentationLayer->>PresentationLayer: 7. Updates the HUD with new cash values for both players and plays a visual/audio effect for the transaction.

    note over TurnManagementService: Audit Log: The TurnManagementService is responsible for logging the successful transaction at the...
    note over RuleEngine: Bankruptcy Path: If the RuleEngine determines the renter has insufficient funds (Step 4), it retu...
    note over RuleEngine: No Rent Path: If the property is mortgaged, the RuleEngine determines this in Step 3 and immediat...

    deactivate GameState
    deactivate RuleEngine
    deactivate TurnManagementService
