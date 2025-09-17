erDiagram
    PlayerProfile {
        Guid profileId PK
    }
    PlayerStatistic {
        Guid playerStatisticId PK
    }
    GameResult {
        Guid gameResultId PK
    }
    GameParticipant {
        Guid gameParticipantId PK
    }
    SavedGame {
        Guid savedGameId PK
    }

    PlayerProfile ||--|| PlayerStatistic : "has one"
    PlayerProfile ||--o{ GameResult : "has many"
    PlayerProfile ||--o{ SavedGame : "has many"
    GameResult ||--|{ GameParticipant : "has one or more"