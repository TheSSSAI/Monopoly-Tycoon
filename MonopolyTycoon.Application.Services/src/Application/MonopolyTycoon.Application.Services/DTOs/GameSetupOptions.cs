namespace MonopolyTycoon.Application.Services.DTOs
{
    /// <summary>
    /// Represents the difficulty levels for an AI opponent.
    /// As per REQ-1-010, REQ-1-030.
    /// </summary>
    public enum AiDifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }

    /// <summary>
    /// Data Transfer Object for configuring an individual AI opponent in a new game.
    /// </summary>
    /// <param name="Difficulty">The selected difficulty level for the AI opponent.</param>
    public record AiOpponentConfiguration(AiDifficultyLevel Difficulty);

    /// <summary>
    /// Data Transfer Object that encapsulates all user-configurable options for starting a new game.
    /// This DTO is passed from the Presentation layer to the Application layer.
    /// Fulfills requirements: REQ-1-030, REQ-1-011, REQ-1-014.
    /// </summary>
    /// <param name="HumanPlayerName">The name chosen by the human player for their profile. Must be 3-16 characters.</param>
    /// <param name="HumanPlayerTokenId">The unique identifier for the token selected by the human player.</param>
    /// <param name="AiOpponents">A list of configurations for each AI opponent in the game. Must contain 1 to 3 items.</param>
    public record GameSetupOptions(
        string HumanPlayerName,
        string HumanPlayerTokenId,
        List<AiOpponentConfiguration> AiOpponents);
}