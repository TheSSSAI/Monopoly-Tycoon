#nullable enable

using System.Text.Json.Serialization;

namespace MonopolyTycoon.Domain.Models.GameStateModels;

/// <summary>
/// Represents the comprehensive, serializable state of a single game session.
/// This record acts as the aggregate root for all game data, directly fulfilling REQ-1-041.
/// It is designed to be immutable and is the canonical data structure for a game save file.
/// </summary>
/// <param name="GameStateId">Unique identifier for the game state instance. Corresponds to a SavedGame record in the relational schema.</param>
/// <param name="GameVersion">The application version that created this state, crucial for data migration logic as per REQ-1-090.</param>
/// <param name="PlayerStates">An immutable list containing the state of every player in the game.</param>
/// <param name="BoardState">Encapsulates the state of all properties on the game board.</param>
/// <param name="BankState">Represents the state of the game's bank, such as available houses and hotels.</param>
/// <param name="DeckStates">Represents the current order of cards in the Chance and Community Chest decks.</param>
/// <param name="GameMetadata">Contains metadata about the game session, such as the current turn number and active player.</param>
public record GameState
{
    [JsonPropertyName("gameStateId")]
    public Guid GameStateId { get; init; }

    [JsonPropertyName("gameVersion")]
    public string GameVersion { get; init; } = string.Empty;

    [JsonPropertyName("playerStates")]
    public IReadOnlyList<PlayerState> PlayerStates { get; init; } = [];

    [JsonPropertyName("boardState")]
    public BoardState BoardState { get; init; } = new();

    [JsonPropertyName("bankState")]
    public BankState BankState { get; init; } = new();

    [JsonPropertyName("deckStates")]
    public DeckStates DeckStates { get; init; } = new();

    [JsonPropertyName("gameMetadata")]
    public GameMetadata GameMetadata { get; init; } = new();

    /// <summary>
    /// Default constructor for serialization and initialization.
    /// </summary>
    public GameState() { }

    /// <summary>
    /// Primary constructor for creating a new GameState.
    /// Ensures all properties are initialized.
    /// </summary>
    [JsonConstructor]
    public GameState(
        Guid gameStateId,
        string gameVersion,
        IReadOnlyList<PlayerState> playerStates,
        BoardState boardState,
        BankState bankState,
        DeckStates deckStates,
        GameMetadata gameMetadata)
    {
        GameStateId = gameStateId;
        GameVersion = gameVersion;
        PlayerStates = playerStates;
        BoardState = boardState;
        BankState = bankState;
        DeckStates = deckStates;
        GameMetadata = gameMetadata;
    }
}