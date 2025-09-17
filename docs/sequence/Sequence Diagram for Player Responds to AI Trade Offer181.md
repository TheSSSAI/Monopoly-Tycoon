sequenceDiagram
    participant "AIBehaviorTreeExecutor" as AIBehaviorTreeExecutor
    participant "TradeOrchestrationService" as TradeOrchestrationService
    participant "InProcessEventBus" as InProcessEventBus
    participant "TradeUIController" as TradeUIController
    participant "InputHandler" as InputHandler
    participant "RuleEngine & GameState" as RuleEngineGameState
    participant "HUDController" as HUDController

    activate TradeOrchestrationService
    AIBehaviorTreeExecutor->>TradeOrchestrationService: 1. 1. ProposeAITrade(proposal)
    TradeOrchestrationService-->>AIBehaviorTreeExecutor: void
    TradeOrchestrationService->>InProcessEventBus: 2. 2. Publish(AITradeOfferReceivedEvent)
    activate TradeUIController
    InProcessEventBus->>TradeUIController: 3. 3. [Subscribed] HandleAITradeOffer(event)
    TradeUIController->>TradeUIController: 4. 4. RenderTradeModalDialog(event.Proposal)
    TradeUIController-->>TradeUIController: void
    InputHandler->>TradeUIController: 5. 5. User clicks 'Accept' button
    TradeUIController->>TradeOrchestrationService: 6. 6. RespondToAITrade(tradeId, ACCEPTED)
    TradeOrchestrationService-->>TradeUIController: bool success
    activate RuleEngineGameState
    TradeOrchestrationService->>RuleEngineGameState: 7. 7. ExecuteTrade(proposal)
    RuleEngineGameState-->>TradeOrchestrationService: void
    RuleEngineGameState->>RuleEngineGameState: 8. 8. Update PlayerState objects (cash, properties)
    RuleEngineGameState-->>RuleEngineGameState: void
    TradeOrchestrationService->>InProcessEventBus: 9. 9. Publish(GameStateUpdatedEvent)
    activate HUDController
    InProcessEventBus->>HUDController: 10. 10. [Subscribed] HandleGameStateUpdate(event)
    HUDController->>HUDController: 11. 11. Refresh Player HUDs (cash, property indicators)
    HUDController-->>HUDController: void

    note over InProcessEventBus: The InProcessEventBus is critical for decoupling the Application Services Layer from the Presenta...
    note over RuleEngineGameState: The RuleEngine acts as the sole authority for mutating the GameState. Services request changes, b...

    deactivate HUDController
    deactivate RuleEngineGameState
    deactivate TradeUIController
    deactivate TradeOrchestrationService
