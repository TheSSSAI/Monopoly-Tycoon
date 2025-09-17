sequenceDiagram
    actor "User" as User
    participant "Presentation Layer" as PresentationLayer
    participant "Application Services Layer" as ApplicationServicesLayer
    participant "Domain Layer" as DomainLayer

    activate PresentationLayer
    User->>PresentationLayer: 1. Selects 'Auction' option from Property Purchase dialog.
    activate ApplicationServicesLayer
    PresentationLayer->>ApplicationServicesLayer: 2. PropertyActionService.InitiateAuction(propertyId, startingPlayerId)
    ApplicationServicesLayer->>PresentationLayer: 3. AuctionUIManager.DisplayAuction(auctionState)
    activate DomainLayer
    ApplicationServicesLayer->>DomainLayer: 4. AIBehaviorTreeExecutor.GetAuctionBid(auctionState, playerState)
    DomainLayer-->>ApplicationServicesLayer: BidDecision { amount: decimal, pass: bool }
    User->>PresentationLayer: 5. Submits bid or passes via Auction UI.
    PresentationLayer->>ApplicationServicesLayer: 6. AuctionService.ProcessPlayerBid(playerId, bidDecision)
    ApplicationServicesLayer->>ApplicationServicesLayer: 7. Check for auction end condition (1 active bidder remaining).
    ApplicationServicesLayer-->>ApplicationServicesLayer: isAuctionOver: bool
    ApplicationServicesLayer->>DomainLayer: 8. RuleEngine.ExecutePropertyPurchase(winnerId, propertyId, finalBid)
    DomainLayer-->>ApplicationServicesLayer: TransactionResult { success: bool, error: string }
    ApplicationServicesLayer->>PresentationLayer: 9. AuctionUIManager.CloseAuction(auctionResult)

    note over ApplicationServicesLayer: Bidding Loop (Steps 4-7): The AuctionService in the Application Services Layer (REPO-AS-005) mana...

    deactivate DomainLayer
    deactivate ApplicationServicesLayer
    deactivate PresentationLayer
