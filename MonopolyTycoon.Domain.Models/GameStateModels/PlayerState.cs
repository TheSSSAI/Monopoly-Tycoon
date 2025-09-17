using System.Text.Json.Serialization;

namespace MonopolyTycoon.Domain.Models.GameStateModels;

/// <summary>
/// Defines the difficulty level for an AI opponent.
/// As per requirements REQ-1-004 and REQ-1-030.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AiDifficulty
{
    Easy,
    Medium,
    Hard
}

/// <summary>
/// Defines the possible states a player can be in during the game.
/// As per requirement REQ-1-031.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PlayerStatus
{
    /// <summary>
    /// The player is active in the game and can take normal turns.
    /// </summary>
    Active,

    /// <summary>
    /// The player is currently in jail.
    /// </summary>
    InJail,

    /// <summary>
    /// The player has been eliminated from the game.
    /// </summary>
    Bankrupt
}


/// <summary>
/// Represents the detailed state of a single player in a game session.
/// This record is immutable and serves as a data contract for serialization and game logic.
/// Fulfills requirement REQ-1-031.
/// </summary>
/// <param name="PlayerId">A unique identifier for the player within the game session.</param>
/// <param name="PlayerName">The display name of the player.</param>
/// <param name="IsHuman">A flag indicating if the player is controlled by a human or AI.</param>
/// <param name="AiDifficulty">The difficulty level if the player is an AI opponent. Null for human players.</param>
/// <param name="TokenId">An identifier for the player's selected game token.</param>
/// <param name="Cash">The player's current cash balance.</param>
/// <param name="CurrentPosition">The player's current position on the board, represented by a tile index (0-39).</param>
/// <param name="Status">The player's current status (e.g., Active, InJail).</param>
/// <param name="JailTurnsRemaining">The number of turns remaining for the player to get out of jail. Only relevant if Status is InJail.</param>
/// <param name="GetOutOfJailCards">The number of 'Get Out of Jail Free' cards the player holds.</param>
public record PlayerState
(
    [property: JsonPropertyName("playerId")] Guid PlayerId,
    [property: JsonPropertyName("playerName")] string PlayerName,
    [property: JsonPropertyName("isHuman")] bool IsHuman,
    [property: JsonPropertyName("aiDifficulty")] AiDifficulty? AiDifficulty,
    [property: JsonPropertyName("tokenId")] int TokenId,
    [property: JsonPropertyName("cash")] int Cash,
    [property: JsonPropertyName("currentPosition")] int CurrentPosition,
    [property: JsonPropertyName("status")] PlayerStatus Status,
    [property: JsonPropertyName("jailTurnsRemaining")] int JailTurnsRemaining,
    [property: JsonPropertyName("getOutOfJailCards")] int GetOutOfJailCards
);