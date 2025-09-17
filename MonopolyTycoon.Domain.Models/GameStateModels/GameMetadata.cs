using System;
using System.Text.Json.Serialization;

namespace MonopolyTycoon.Domain.Models.GameStateModels;

/// <summary>
/// Contains metadata about the current game session, such as turn progression,
/// the active player, and timestamps for saving.
/// This record is immutable and a component of the GameState.
/// </summary>
/// <param name="TurnNumber">The current turn number of the game, starting from 1.</param>
/// <param name="ActivePlayerId">The PlayerId of the player whose turn it currently is.</param>
/// <param name="SessionTimestamp">The UTC timestamp when the game session was last saved.</param>
public record GameMetadata
(
    [property: JsonPropertyName("turnNumber")] int TurnNumber,
    [property: JsonPropertyName("activePlayerId")] Guid ActivePlayerId,
    [property: JsonPropertyName("sessionTimestamp")] DateTime SessionTimestamp
);