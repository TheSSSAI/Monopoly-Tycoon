namespace MonopolyTycoon.Presentation.Shared.Services
{
    /// <summary>
    /// Provides a strongly-typed identifier for each major UI screen in the application.
    /// Used by the IViewManager to identify which screen to load and display.
    /// </summary>
    public enum Screen
    {
        /// <summary>
        /// Represents no screen or an uninitialized state.
        /// </summary>
        None = 0,

        /// <summary>
        /// The main menu screen, shown on application startup.
        /// </summary>
        MainMenu = 1,

        /// <summary>
        /// The new game configuration screen.
        /// </summary>
        GameSetup = 2,

        /// <summary>
        /// The main game screen with the HUD and 3D board.
        /// </summary>
        GameBoard = 3,

        /// <summary>
        /// The application settings screen.
        /// </summary>
        Settings = 4,

        /// <summary>
        /// The screen for managing owned properties (building, mortgaging, etc.).
        /// </summary>
        PropertyManagement = 5,

        /// <summary>
        /// The screen for loading a previously saved game.
        /// </summary>
        LoadGame = 6,

        /// <summary>
        /// The screen for displaying historical player statistics.
        /// </summary>
        PlayerStats = 7,

        /// <summary>
        /// The screen shown at the end of a game, summarizing results.
        /// </summary>
        GameSummary = 8
    }
}