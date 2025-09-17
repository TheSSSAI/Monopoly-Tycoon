sequenceDiagram
    participant "Presentation Layer" as PresentationLayer
    participant "Application Services Layer" as ApplicationServicesLayer
    participant "Business Logic (Domain) Layer" as BusinessLogicDomainLayer

    activate ApplicationServicesLayer
    PresentationLayer->>ApplicationServicesLayer: 1. PropertyActionService.DeclinePurchase(playerId, propertyId)
    activate BusinessLogicDomainLayer
    ApplicationServicesLayer->>BusinessLogicDomainLayer: 2. RuleEngine.InitiateAuction(propertyId, decliningPlayerId)
    BusinessLogicDomainLayer-->>ApplicationServicesLayer: AuctionState
    ApplicationServicesLayer->>ApplicationServicesLayer: 3. TurnManagementService.BeginAuctionLoop(auctionState)
    ApplicationServicesLayer->>PresentationLayer: 4. AuctionUIController.DisplayAuction(auctionDetails)
    ApplicationServicesLayer->>BusinessLogicDomainLayer: 5. AIBehaviorTreeExecutor.DetermineBid(aiPlayerId, propertyId, currentHighestBid)
    BusinessLogicDomainLayer-->>ApplicationServicesLayer: int bidAmount
    ApplicationServicesLayer->>PresentationLayer: 6. AuctionUIController.UpdateHighestBid(playerName, newBidAmount)
    PresentationLayer->>ApplicationServicesLayer: 7. TurnManagementService.SubmitHumanBid(humanPlayerId, bidAmount)
    ApplicationServicesLayer->>ApplicationServicesLayer: 8. TurnManagementService.EndAuctionLoop()
    ApplicationServicesLayer-->>ApplicationServicesLayer: AuctionResult
    ApplicationServicesLayer->>BusinessLogicDomainLayer: 9. RuleEngine.FinalizeAuctionSale(winnerId, propertyId, finalAmount)
    BusinessLogicDomainLayer-->>ApplicationServicesLayer: TransactionResult
    ApplicationServicesLayer->>PresentationLayer: 10. AuctionUIController.ShowResultAndClose(auctionResult)

    note over ApplicationServicesLayer: Interactions 5, 6, and 7 occur within a loop managed by the TurnManagementService. The loop proce...
    note over ApplicationServicesLayer: If AuctionResult has no winner (all players passed), the call to FinalizeAuctionSale (Step 9) is ...

    deactivate BusinessLogicDomainLayer
    deactivate ApplicationServicesLayer
