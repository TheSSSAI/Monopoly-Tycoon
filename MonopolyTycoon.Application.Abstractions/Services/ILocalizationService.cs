namespace MonopolyTycoon.Application.Abstractions.Services
{
    /// <summary>
    /// Defines the contract for a service that provides localized strings.
    /// This decouples the presentation layer from the specifics of how localization
    /// resources are stored and retrieved (e.g., from JSON files).
    /// Fulfills requirements: REQ-1-084.
    /// </summary>
    public interface ILocalizationService
    {
        /// <summary>
        /// Retrieves a localized string for the specified key.
        /// </summary>
        /// <param name="key">The unique key for the desired string (e.g., "MainMenu.NewGameButton.Text").</param>
        /// <returns>The localized string for the current language, or the key itself if not found.</returns>
        string GetString(string key);

        /// <summary>
        /// Retrieves and formats a localized string for the specified key using the provided arguments.
        /// </summary>
        /// <param name="key">The unique key for the desired formatted string (e.g., "Notification.PlayerPaidRent").</param>
        /// <param name="args">The objects to format into the string.</param>
        /// <returns>The formatted, localized string, or the key itself if not found.</returns>
        string GetString(string key, params object[] args);
    }
}