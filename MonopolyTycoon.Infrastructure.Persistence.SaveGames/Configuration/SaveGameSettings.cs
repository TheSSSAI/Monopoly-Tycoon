namespace MonopolyTycoon.Infrastructure.Persistence.SaveGames.Configuration;

/// <summary>
/// Provides strongly-typed access to configuration values for the save game persistence layer.
/// This class is designed to be populated from a configuration source (e.g., appsettings.json)
/// using the .NET Options Pattern. It helps in constructing user-specific application data paths.
/// </summary>
public sealed class SaveGameSettings
{
    /// <summary>
    /// Represents the configuration section path in appsettings.json.
    /// </summary>
    public const string SectionName = "Persistence:SaveGames";

    /// <summary>
    /// The company name used in creating the application data directory path.
    /// E.g., %APPDATA%/{CompanyName}/{AppName}
    /// </summary>
    public string CompanyName { get; set; } = "MonopolyTycoon";

    /// <summary>
    /// The application name used in creating the application data directory path.
    /// E.g., %APPDATA%/{CompanyName}/{AppName}
    /// </summary>
    public string AppName { get; set; } = "MonopolyTycoon";
}