namespace MonopolyTycoon.Infrastructure.Persistence.Statistics.Schema
{
    /// <summary>
    /// Provides a static, centralized definition of the SQL DDL statements
    /// required to create the entire statistics database schema. This class is the
    /// single source of truth for the database structure, ensuring it aligns with
    /// the Monopoly Tycoon Player Data ERD.
    /// </summary>
    public static class DatabaseSchema
    {
        public const string CreatePlayerProfileTable = @"
            CREATE TABLE IF NOT EXISTS PlayerProfile (
                profileId           TEXT NOT NULL PRIMARY KEY,
                displayName         TEXT NOT NULL UNIQUE,
                createdAt           TEXT NOT NULL,
                updatedAt           TEXT NOT NULL
            );";

        public const string CreatePlayerStatisticTable = @"
            CREATE TABLE IF NOT EXISTS PlayerStatistic (
                playerStatisticId   TEXT NOT NULL PRIMARY KEY,
                profileId           TEXT NOT NULL UNIQUE,
                totalGamesPlayed    INTEGER NOT NULL DEFAULT 0,
                totalWins           INTEGER NOT NULL DEFAULT 0,
                FOREIGN KEY (profileId) REFERENCES PlayerProfile(profileId) ON DELETE CASCADE
            );";

        public const string CreateGameResultTable = @"
            CREATE TABLE IF NOT EXISTS GameResult (
                gameResultId        TEXT NOT NULL PRIMARY KEY,
                profileId           TEXT NOT NULL,
                didHumanWin         INTEGER NOT NULL,
                gameDurationSeconds INTEGER NOT NULL,
                endTimestamp        TEXT NOT NULL,
                FOREIGN KEY (profileId) REFERENCES PlayerProfile(profileId) ON DELETE CASCADE
            );";

        public const string CreateGameParticipantTable = @"
            CREATE TABLE IF NOT EXISTS GameParticipant (
                gameParticipantId   TEXT NOT NULL PRIMARY KEY,
                gameResultId        TEXT NOT NULL,
                participantName     TEXT NOT NULL,
                isHuman             INTEGER NOT NULL,
                finalNetWorth       REAL NOT NULL,
                FOREIGN KEY (gameResultId) REFERENCES GameResult(gameResultId) ON DELETE CASCADE
            );";
        
        public static readonly string[] CreateIndexes =
        [
            // Index to optimize the retrieval of Top 10 scores as per REQ-1-091.
            "CREATE INDEX IF NOT EXISTS IX_GameParticipant_FinalNetWorth ON GameParticipant(finalNetWorth DESC);",

            // Index to optimize lookups by display name as per REQ-1-032.
            "CREATE UNIQUE INDEX IF NOT EXISTS IX_PlayerProfile_DisplayName ON PlayerProfile(displayName);"
        ];

        /// <summary>
        /// An array containing all table creation scripts in the correct dependency order.
        /// </summary>
        public static readonly string[] CreateTableStatements =
        [
            CreatePlayerProfileTable,
            CreatePlayerStatisticTable,
            CreateGameResultTable,
            CreateGameParticipantTable
        ];
    }
}