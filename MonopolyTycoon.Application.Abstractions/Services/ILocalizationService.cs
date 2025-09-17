namespace MonopolyTycoon.Application.Abstractions.Services
{
    /// <summary>
    /// Defines a contract for retrieving localized user-facing strings.
    /// This decouples the application and presentation layers from the specifics of how
    /// localization resources are stored and managed.
    /// </summary>
    public interface ILocalizationService
    {
        /// <summary>
        /// Retrieves a localized string for the specified key.
        /// </summary>
        /// <param name="key">The key of the string to retrieve (e.g., "MainMenu.NewGameButton").</param>
        /// <returns>The localized string for the current language, or a placeholder if the key is not found.</returns>
        string GetString(string key);

        /// <summary>
        /// Retrieves and formats a localized string for the specified key using the provided arguments.
        /// </summary>
        /// <param name="key">The key of the formatted string to retrieve (e.g., "RentPayment.Notification").</param>
        /// <param name="args">The arguments to format into the string.</param>
        /// <returns>The formatted, localized string.</returns>
        string GetString(string key, params object[] args);
    }
}