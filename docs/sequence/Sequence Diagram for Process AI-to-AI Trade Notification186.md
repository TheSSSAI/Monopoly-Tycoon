sequenceDiagram
    participant "TradeOrchestrationService" as TradeOrchestrationService
    participant "RuleEngine" as RuleEngine
    participant "InProcessEventBus" as InProcessEventBus
    participant "NotificationHandler" as NotificationHandler

    activate RuleEngine
    TradeOrchestrationService->>RuleEngine: 1. Invoke trade execution for validated AI-to-AI offer.
    RuleEngine-->>TradeOrchestrationService: Return result of trade execution.
    RuleEngine->>RuleEngine: 2. Atomically update GameState: transfer cash, properties, and cards between AI players.
    TradeOrchestrationService->>InProcessEventBus: 3. Publish domain event upon successful trade execution.
    activate NotificationHandler
    InProcessEventBus->>NotificationHandler: 4. Asynchronously dispatch event to subscribed UI handler.
    NotificationHandler->>NotificationHandler: 5. Format event data into a human-readable notification string.
    NotificationHandler->>NotificationHandler: 6. Invoke HUD manager to display the temporary, non-intrusive notification.

    note over InProcessEventBus: The TradeCompletedEvent schema is critical. It must contain player names, a list of properties/ca...
    note over NotificationHandler: The entire notification process from event publication to UI display must be asynchronous and dec...

    deactivate NotificationHandler
    deactivate RuleEngine
