using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MonopolyTycoon.Application.Common.Interfaces;
using MonopolyTycoon.Domain.Entities;
using MonopolyTycoon.Infrastructure.Persistence.Statistics.Configuration;
using MonopolyTycoon.Infrastructure.Persistence.Statistics.Exceptions;
using MonopolyTycoon.Infrastructure.Persistence.Statistics.Schema;
using System.Data;

namespace MonopolyTycoon.Infrastructure.Persistence.Statistics;

public class SqliteStatisticsRepository : IStatisticsRepository, IPlayerProfileRepository
{
    private readonly StatisticsPersistenceOptions _options;
    private readonly ILogger<SqliteStatisticsRepository> _logger;
    private readonly string _connectionString;

    public SqliteStatisticsRepository(IOptions<StatisticsPersistenceOptions> options, ILogger<SqliteStatisticsRepository> logger)
    {
        _options = options.Value;
        _logger = logger;
        _connectionString = new SqliteConnectionStringBuilder { DataSource = _options.DatabaseFilePath, Mode = SqliteOpenMode.ReadWriteCreate }.ConnectionString;

        Directory.CreateDirectory(Path.GetDirectoryName(_options.DatabaseFilePath)!);
    }

    public async Task InitializeDatabaseAsync()
    {
        _logger.LogInformation("Initializing statistics database at '{DatabasePath}'...", _options.DatabaseFilePath);
        Directory.CreateDirectory(_options.BackupDirectoryPath);

        bool dbExists = File.Exists(_options.DatabaseFilePath);

        try
        {
            await using var connection = await GetOpenConnectionAsync();
            if (!dbExists || new FileInfo(_options.DatabaseFilePath).Length == 0)
            {
                _logger.LogInformation("Database file not found or is empty. Creating new schema...");
                await CreateSchemaAsync(connection);
            }
        }
        catch (SqliteException ex) when (ex.SqliteErrorCode == 11) // SQLITE_CORRUPT
        {
            _logger.LogWarning(ex, "Database corruption detected. Attempting recovery from backups.");
            if (!await AttemptRecoveryAsync())
            {
                _logger.LogError("All database recovery attempts failed. Data is unrecoverable.");
                throw new DataCorruptionException("Statistics database is corrupt and could not be recovered from backups.", ex);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred during database initialization.");
            throw new DataAccessLayerException("Failed to initialize the statistics database.", ex);
        }

        await CreateBackupAsync();
        await PruneOldBackupsAsync();
        _logger.LogInformation("Database initialization complete.");
    }

    public async Task<PlayerProfile> GetOrCreateProfileAsync(string displayName)
    {
        try
        {
            await using var connection = await GetOpenConnectionAsync();

            var existingProfile = await GetProfileByNameAsync(connection, displayName);
            if (existingProfile != null)
            {
                return existingProfile;
            }

            var newProfile = new PlayerProfile
            {
                ProfileId = Guid.NewGuid(),
                DisplayName = displayName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO PlayerProfile (ProfileId, DisplayName, CreatedAt, UpdatedAt) VALUES (@ProfileId, @DisplayName, @CreatedAt, @UpdatedAt);";
            command.Parameters.AddWithValue("@ProfileId", newProfile.ProfileId.ToString());
            command.Parameters.AddWithValue("@DisplayName", newProfile.DisplayName);
            command.Parameters.AddWithValue("@CreatedAt", newProfile.CreatedAt);
            command.Parameters.AddWithValue("@UpdatedAt", newProfile.UpdatedAt);

            try
            {
                await command.ExecuteNonQueryAsync();
                
                // Also create the associated statistics record
                await CreateInitialPlayerStatisticAsync(connection, newProfile.ProfileId);

                return newProfile;
            }
            catch (SqliteException ex) when (ex.SqliteErrorCode == 19) // SQLITE_CONSTRAINT
            {
                _logger.LogWarning("Race condition detected while creating profile for '{DisplayName}'. Re-querying.", displayName);
                // Another process created it between our SELECT and INSERT. Re-query to get the existing one.
                var profileAfterRace = await GetProfileByNameAsync(connection, displayName);
                if (profileAfterRace == null)
                {
                    // This is an unexpected state
                    throw new DataAccessLayerException($"Failed to retrieve profile '{displayName}' after a constraint violation.", ex);
                }
                return profileAfterRace;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting or creating profile for '{DisplayName}'.", displayName);
            throw new DataAccessLayerException($"An error occurred while getting or creating the profile for '{displayName}'.", ex);
        }
    }

    public async Task UpdatePlayerStatisticsAsync(GameResult gameResult, IEnumerable<GameParticipant> participants)
    {
        await using var connection = await GetOpenConnectionAsync();
        await using var transaction = connection.BeginTransaction();

        try
        {
            // 1. Insert GameResult
            await using var resultCommand = connection.CreateCommand();
            resultCommand.Transaction = transaction;
            resultCommand.CommandText = "INSERT INTO GameResult (GameResultId, ProfileId, DidHumanWin, GameDurationSeconds, EndTimestamp) VALUES (@GameResultId, @ProfileId, @DidHumanWin, @GameDurationSeconds, @EndTimestamp);";
            resultCommand.Parameters.AddWithValue("@GameResultId", gameResult.GameResultId.ToString());
            resultCommand.Parameters.AddWithValue("@ProfileId", gameResult.ProfileId.ToString());
            resultCommand.Parameters.AddWithValue("@DidHumanWin", gameResult.DidHumanWin);
            resultCommand.Parameters.AddWithValue("@GameDurationSeconds", gameResult.GameDurationSeconds);
            resultCommand.Parameters.AddWithValue("@EndTimestamp", gameResult.EndTimestamp);
            await resultCommand.ExecuteNonQueryAsync();

            // 2. Insert GameParticipants
            foreach (var participant in participants)
            {
                await using var participantCommand = connection.CreateCommand();
                participantCommand.Transaction = transaction;
                participantCommand.CommandText = "INSERT INTO GameParticipant (GameParticipantId, GameResultId, ParticipantName, IsHuman, FinalNetWorth) VALUES (@GameParticipantId, @GameResultId, @ParticipantName, @IsHuman, @FinalNetWorth);";
                participantCommand.Parameters.AddWithValue("@GameParticipantId", participant.GameParticipantId.ToString());
                participantCommand.Parameters.AddWithValue("@GameResultId", gameResult.GameResultId.ToString());
                participantCommand.Parameters.AddWithValue("@ParticipantName", participant.ParticipantName);
                participantCommand.Parameters.AddWithValue("@IsHuman", participant.IsHuman);
                participantCommand.Parameters.AddWithValue("@FinalNetWorth", participant.FinalNetWorth);
                await participantCommand.ExecuteNonQueryAsync();
            }

            // 3. Update PlayerStatistic
            await using var statsCommand = connection.CreateCommand();
            statsCommand.Transaction = transaction;
            statsCommand.CommandText = "UPDATE PlayerStatistic SET TotalGamesPlayed = TotalGamesPlayed + 1, TotalWins = TotalWins + @WinIncrement WHERE ProfileId = @ProfileId;";
            statsCommand.Parameters.AddWithValue("@WinIncrement", gameResult.DidHumanWin ? 1 : 0);
            statsCommand.Parameters.AddWithValue("@ProfileId", gameResult.ProfileId.ToString());
            await statsCommand.ExecuteNonQueryAsync();

            await transaction.CommitAsync();
            _logger.LogInformation("Successfully updated statistics for profile {ProfileId} after game {GameResultId}.", gameResult.ProfileId, gameResult.GameResultId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to update player statistics transactionally for profile {ProfileId}. Rolling back.", gameResult.ProfileId);
            await transaction.RollbackAsync();
            throw new DataAccessLayerException("Failed to save game result and update statistics.", ex);
        }
    }

    public async Task<PlayerStatistic?> GetPlayerStatsAsync(Guid profileId)
    {
        try
        {
            await using var connection = await GetOpenConnectionAsync();
            await using var command = connection.CreateCommand();
            command.CommandText = "SELECT PlayerStatisticId, ProfileId, TotalGamesPlayed, TotalWins FROM PlayerStatistic WHERE ProfileId = @ProfileId;";
            command.Parameters.AddWithValue("@ProfileId", profileId.ToString());

            await using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return MapToPlayerStatistic(reader);
            }
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving statistics for profile {ProfileId}.", profileId);
            throw new DataAccessLayerException($"An error occurred while retrieving statistics for profile '{profileId}'.", ex);
        }
    }
    
    public async Task<PlayerProfile?> GetProfileByIdAsync(Guid profileId)
    {
        try
        {
            await using var connection = await GetOpenConnectionAsync();
            await using var command = connection.CreateCommand();
            command.CommandText = "SELECT ProfileId, DisplayName, CreatedAt, UpdatedAt FROM PlayerProfile WHERE ProfileId = @ProfileId;";
            command.Parameters.AddWithValue("@ProfileId", profileId.ToString());

            await using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return MapToPlayerProfile(reader);
            }
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving profile by ID {ProfileId}.", profileId);
            throw new DataAccessLayerException($"An error occurred while retrieving profile by ID '{profileId}'.", ex);
        }
    }

    public async Task<List<GameParticipant>> GetTopScoresAsync()
    {
        var topScores = new List<GameParticipant>();
        try
        {
            await using var connection = await GetOpenConnectionAsync();
            await using var command = connection.CreateCommand();
            // This retrieves the top 10 winning game results for human players, ordered by net worth
            command.CommandText = @"
                SELECT p.GameParticipantId, p.GameResultId, p.ParticipantName, p.IsHuman, p.FinalNetWorth
                FROM GameParticipant p
                JOIN GameResult r ON p.GameResultId = r.GameResultId
                WHERE p.IsHuman = 1 AND r.DidHumanWin = 1
                ORDER BY p.FinalNetWorth DESC
                LIMIT 10;";
            
            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                topScores.Add(MapToGameParticipant(reader));
            }

            return topScores;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving top scores.");
            throw new DataAccessLayerException("An error occurred while retrieving top scores.", ex);
        }
    }

    public async Task ResetStatisticsDataAsync(Guid profileId)
    {
        await using var connection = await GetOpenConnectionAsync();
        await using var transaction = connection.BeginTransaction();
        try
        {
            // 1. Delete GameParticipants associated with the profile's games
            await using var deleteParticipantsCmd = connection.CreateCommand();
            deleteParticipantsCmd.Transaction = transaction;
            deleteParticipantsCmd.CommandText = "DELETE FROM GameParticipant WHERE GameResultId IN (SELECT GameResultId FROM GameResult WHERE ProfileId = @ProfileId);";
            deleteParticipantsCmd.Parameters.AddWithValue("@ProfileId", profileId.ToString());
            await deleteParticipantsCmd.ExecuteNonQueryAsync();

            // 2. Delete GameResults
            await using var deleteResultsCmd = connection.CreateCommand();
            deleteResultsCmd.Transaction = transaction;
            deleteResultsCmd.CommandText = "DELETE FROM GameResult WHERE ProfileId = @ProfileId;";
            deleteResultsCmd.Parameters.AddWithValue("@ProfileId", profileId.ToString());
            await deleteResultsCmd.ExecuteNonQueryAsync();

            // 3. Reset PlayerStatistic record
            await using var resetStatsCmd = connection.CreateCommand();
            resetStatsCmd.Transaction = transaction;
            resetStatsCmd.CommandText = "UPDATE PlayerStatistic SET TotalGamesPlayed = 0, TotalWins = 0 WHERE ProfileId = @ProfileId;";
            resetStatsCmd.Parameters.AddWithValue("@ProfileId", profileId.ToString());
            await resetStatsCmd.ExecuteNonQueryAsync();
            
            await transaction.CommitAsync();
            _logger.LogInformation("Successfully reset all statistics for profile {ProfileId}.", profileId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to reset statistics for profile {ProfileId}. Rolling back.", profileId);
            await transaction.RollbackAsync();
            throw new DataAccessLayerException($"Failed to reset statistics for profile '{profileId}'.", ex);
        }
    }

    private async Task<bool> AttemptRecoveryAsync()
    {
        var backupFiles = Directory.GetFiles(_options.BackupDirectoryPath)
            .Select(f => new FileInfo(f))
            .OrderByDescending(f => f.CreationTime)
            .ToList();

        if (!backupFiles.Any())
        {
            _logger.LogWarning("No backups available to attempt recovery.");
            return false;
        }

        foreach (var backupFile in backupFiles)
        {
            _logger.LogInformation("Attempting to restore from backup: {BackupFile}", backupFile.Name);
            try
            {
                // Atomic replacement
                File.Delete(_options.DatabaseFilePath);
                File.Copy(backupFile.FullName, _options.DatabaseFilePath);

                // Verify the restored file
                await using var connection = await GetOpenConnectionAsync();
                await using var command = connection.CreateCommand();
                command.CommandText = "PRAGMA integrity_check;";
                var result = await command.ExecuteScalarAsync() as string;
                if (result?.Equals("ok", StringComparison.OrdinalIgnoreCase) == true)
                {
                    _logger.LogInformation("Successfully restored and verified database from backup: {BackupFile}", backupFile.Name);
                    return true;
                }
                else
                {
                     _logger.LogWarning("Restored backup file '{BackupFile}' is also corrupt. Integrity check returned: {Result}", backupFile.Name, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to restore from backup '{BackupFile}'. Trying next available backup.", backupFile.Name);
            }
        }

        return false;
    }

    private async Task CreateBackupAsync()
    {
        if (!File.Exists(_options.DatabaseFilePath)) return;

        try
        {
            string backupFileName = $"stats_backup_{DateTime.UtcNow:yyyyMMddHHmmssfff}.db";
            string backupFilePath = Path.Combine(_options.BackupDirectoryPath, backupFileName);
            
            // This is an atomic copy, not a true atomic backup but sufficient for this context.
            File.Copy(_options.DatabaseFilePath, backupFilePath, true); 
            _logger.LogInformation("Successfully created database backup: {BackupFile}", backupFileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create database backup.");
        }
    }

    private async Task PruneOldBackupsAsync()
    {
        try
        {
            var backupFiles = Directory.GetFiles(_options.BackupDirectoryPath)
                .Select(f => new FileInfo(f))
                .OrderByDescending(f => f.CreationTime)
                .ToList();

            if (backupFiles.Count > _options.BackupRetentionCount)
            {
                var filesToDelete = backupFiles.Skip(_options.BackupRetentionCount);
                foreach (var file in filesToDelete)
                {
                    file.Delete();
                    _logger.LogInformation("Pruned old backup: {BackupFile}", file.Name);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to prune old backups.");
        }
        await Task.CompletedTask;
    }

    private async Task CreateSchemaAsync(SqliteConnection connection)
    {
        _logger.LogInformation("Executing database schema creation scripts.");
        await using var transaction = connection.BeginTransaction();
        try
        {
            var ddlCommands = new[]
            {
                DatabaseSchema.CreatePlayerProfileTable,
                DatabaseSchema.CreatePlayerStatisticTable,
                DatabaseSchema.CreateGameResultTable,
                DatabaseSchema.CreateGameParticipantTable
            }.Concat(DatabaseSchema.CreateIndexes);

            foreach (var ddl in ddlCommands)
            {
                await using var command = connection.CreateCommand();
                command.Transaction = transaction;
                command.CommandText = ddl;
                await command.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();
            _logger.LogInformation("Database schema created successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to create database schema. Rolling back.");
            await transaction.RollbackAsync();
            throw new DataAccessLayerException("Could not create the database schema.", ex);
        }
    }
    
    private async Task CreateInitialPlayerStatisticAsync(SqliteConnection connection, Guid profileId)
    {
        await using var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO PlayerStatistic (PlayerStatisticId, ProfileId, TotalGamesPlayed, TotalWins) VALUES (@PlayerStatisticId, @ProfileId, 0, 0);";
        command.Parameters.AddWithValue("@PlayerStatisticId", Guid.NewGuid().ToString());
        command.Parameters.AddWithValue("@ProfileId", profileId.ToString());
        await command.ExecuteNonQueryAsync();
    }

    private async Task<PlayerProfile?> GetProfileByNameAsync(SqliteConnection connection, string displayName)
    {
        await using var command = connection.CreateCommand();
        command.CommandText = "SELECT ProfileId, DisplayName, CreatedAt, UpdatedAt FROM PlayerProfile WHERE DisplayName = @DisplayName;";
        command.Parameters.AddWithValue("@DisplayName", displayName);

        await using var reader = await command.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return MapToPlayerProfile(reader);
        }
        return null;
    }

    private async Task<SqliteConnection> GetOpenConnectionAsync()
    {
        var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }

    private PlayerProfile MapToPlayerProfile(IDataReader reader) => new()
    {
        ProfileId = Guid.Parse(reader.GetString(reader.GetOrdinal("ProfileId"))),
        DisplayName = reader.GetString(reader.GetOrdinal("DisplayName")),
        CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
        UpdatedAt = reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
    };

    private PlayerStatistic MapToPlayerStatistic(IDataReader reader) => new()
    {
        PlayerStatisticId = Guid.Parse(reader.GetString(reader.GetOrdinal("PlayerStatisticId"))),
        ProfileId = Guid.Parse(reader.GetString(reader.GetOrdinal("ProfileId"))),
        TotalGamesPlayed = reader.GetInt32(reader.GetOrdinal("TotalGamesPlayed")),
        TotalWins = reader.GetInt32(reader.GetOrdinal("TotalWins"))
    };

    private GameParticipant MapToGameParticipant(IDataReader reader) => new()
    {
        GameParticipantId = Guid.Parse(reader.GetString(reader.GetOrdinal("GameParticipantId"))),
        GameResultId = Guid.Parse(reader.GetString(reader.GetOrdinal("GameResultId"))),
        ParticipantName = reader.GetString(reader.GetOrdinal("ParticipantName")),
        IsHuman = reader.GetBoolean(reader.GetOrdinal("IsHuman")),
        FinalNetWorth = reader.GetDecimal(reader.GetOrdinal("FinalNetWorth"))
    };
}