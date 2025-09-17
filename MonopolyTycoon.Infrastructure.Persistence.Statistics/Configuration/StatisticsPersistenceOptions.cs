using System;
using System.IO;

namespace MonopolyTycoon.Infrastructure.Persistence.Statistics.Configuration
{
    /// <summary>
    /// Defines the configuration settings for the statistics persistence repository.
    /// These settings are loaded from appsettings.json via the Options Pattern.
    /// </summary>
    public class StatisticsPersistenceOptions
    {
        /// <summary>
        /// Configuration section name in appsettings.json.
        /// </summary>
        public const string SectionName = "Persistence:Statistics";

        /// <summary>
        /// The full path to the SQLite database file.
        /// Defaults to a standard location within the user's APPDATA folder.
        /// </summary>
        public string DatabaseFilePath { get; set; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "MonopolyTycoon",
            "stats.db");

        /// <summary>
        /// The path to the directory where database backups will be stored.
        /// Defaults to a 'backups' subdirectory within the application's APPDATA folder.
        /// </summary>
        public string BackupDirectoryPath { get; set; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "MonopolyTycoon",
            "backups");

        /// <summary>
        /// The number of recent database backups to retain. Older backups will be automatically deleted.
        /// This directly supports requirement REQ-1-089.
        /// </summary>
        public int BackupRetentionCount { get; set; } = 3;
    }
}