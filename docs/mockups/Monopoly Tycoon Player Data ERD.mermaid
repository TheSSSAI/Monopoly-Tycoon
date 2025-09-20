erDiagram
    PlayerProfile {
        Guid profileId PK
        VARCHAR(16) displayName UK
        DateTime createdAt
        DateTime updatedAt
    }

    PlayerStatistic {
        Guid playerStatisticId PK
        Guid profileId FK, UK "One-to-one extension"
        INT totalGamesPlayed
        INT totalWins
    }

    GameResult {
        Guid gameResultId PK
        Guid profileId FK
        BOOLEAN didHumanWin
        INT gameDurationSeconds
        DateTime endTimestamp
    }

    GameParticipant {
        Guid gameParticipantId PK
        Guid gameResultId FK
        VARCHAR(16) participantName
        BOOLEAN isHuman
        DECIMAL finalNetWorth
    }

    SavedGame {
        Guid savedGameId PK
        Guid profileId FK
        INT slotNumber
        VARCHAR(100) saveName
        DateTime saveTimestamp
    }

    PlayerProfile ||--|| PlayerStatistic : "has"
    PlayerProfile }o--|| GameResult : "plays"
    PlayerProfile }o--|| SavedGame : "creates"
    GameResult }o--|| GameParticipant : "includes"