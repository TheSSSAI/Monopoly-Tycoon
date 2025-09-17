sequenceDiagram
    participant "Human Player" as HumanPlayer
    participant "Presentation Layer (UI)" as PresentationLayerUI
    participant "Application Services Layer" as ApplicationServicesLayer
    participant "Business Logic (Domain) Layer" as BusinessLogicDomainLayer

    activate PresentationLayerUI
    HumanPlayer->>PresentationLayerUI: 1. Clicks 'Trade' button on main HUD during pre-roll phase.
    activate ApplicationServicesLayer
    PresentationLayerUI->>ApplicationServicesLayer: 2. Requests data needed to populate the trading interface.
    ApplicationServicesLayer-->>PresentationLayerUI: Returns TradeUIDataDto with lists of tradable assets for all players.
    HumanPlayer->>PresentationLayerUI: 3. Selects AI opponent and constructs the trade offer by selecting properties, cash, and cards from both panels.
    HumanPlayer->>PresentationLayerUI: 4. Clicks 'Propose Trade' button.
    PresentationLayerUI->>ApplicationServicesLayer: 5. Submits the trade proposal for processing.
    ApplicationServicesLayer-->>PresentationLayerUI: Returns TradeProposalResultDto indicating outcome (Accepted, Declined, Invalid).
    activate BusinessLogicDomainLayer
    ApplicationServicesLayer->>BusinessLogicDomainLayer: 6. Validates the trade against core business rules.
    BusinessLogicDomainLayer-->>ApplicationServicesLayer: Returns true if valid.
    ApplicationServicesLayer->>BusinessLogicDomainLayer: 7. Forwards the validated offer to the AI for evaluation.
    BusinessLogicDomainLayer-->>ApplicationServicesLayer: Returns TradeDecision enum (Accepted/Declined).
    ApplicationServicesLayer->>BusinessLogicDomainLayer: 8. [Conditional: IF Accepted] Executes the asset exchange.
    BusinessLogicDomainLayer-->>ApplicationServicesLayer: Returns void on success.
    PresentationLayerUI->>PresentationLayerUI: 9. Displays outcome notification ('Trade Accepted' or 'Trade Declined/Invalid').
    PresentationLayerUI->>PresentationLayerUI: 10. [Conditional: IF Accepted] Refreshes UI elements (HUD, Property Management Screen) to reflect new asset ownership and cash balances.
    PresentationLayerUI->>HumanPlayer: 11. Closes trade UI and returns control to the player for their turn.

    note over BusinessLogicDomainLayer: The AI evaluation logic within the AIBehaviorTreeExecutor is the most complex part of this sequen...
    note over BusinessLogicDomainLayer: The RuleEngine.ExecuteTrade method must be atomic. If any part of the asset transfer fails, the e...

    deactivate BusinessLogicDomainLayer
    deactivate ApplicationServicesLayer
    deactivate PresentationLayerUI
