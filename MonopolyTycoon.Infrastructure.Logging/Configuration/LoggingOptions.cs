using System.ComponentModel.DataAnnotations;

namespace MonopolyTycoon.Infrastructure.Logging.Configuration
{
    /// <summary>
    /// Provides strongly-typed configuration options for the logging library,
    /// intended to be populated from appsettings.json via the .NET IOptions pattern.
    /// This class directly supports the configuration requirements REQ-1-019, REQ-1-020, and REQ-1-021.
    /// </summary>
    public sealed class LoggingOptions
    {
        /// <summary>
        /// The configuration section name in appsettings.json.
        /// </summary>
        public const string SectionName = "Logging:Serilog";

        /// <summary>
        /// The directory where log files will be stored. This path is relative to the
        /// user's APPDATA folder, fulfilling REQ-1-020.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(1)]
        public string LogFileDirectory { get; set; } = "MonopolyTycoon/logs";

        /// <summary>
        /// The maximum size of a log file in bytes before a new file is created.
        /// Fulfills part of REQ-1-021.
        /// </summary>
        [Range(1048576, 104857600, ErrorMessage = "FileSizeLimitBytes must be between 1MB and 100MB.")]
        public long FileSizeLimitBytes { get; set; } = 10 * 1024 * 1024; // 10 MB

        /// <summary>
        /// The maximum number of log files to retain. Older files will be automatically deleted.
        /// Fulfills part of REQ-1-021.
        /// </summary>
        [Range(1, 100, ErrorMessage = "RetainedFileCountLimit must be between 1 and 100.")]
        public int RetainedFileCountLimit { get; set; } = 7;

        /// <summary>
        /// The minimum level for log events to be written to the sink.
        /// Valid values are: Verbose, Debug, Information, Warning, Error, Fatal.
        /// </summary>
        [Required]
        public string MinimumLevel { get; set; } = "Information";
    }
}