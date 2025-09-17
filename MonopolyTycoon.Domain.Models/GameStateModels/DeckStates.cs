using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MonopolyTycoon.Domain.Models.GameStateModels;

/// <summary>
/// Represents the current, shuffled order of the Chance and Community Chest card decks.
/// Storing the order of card identifiers allows the game state to be saved and restored perfectly.
/// This record is immutable and a component of the GameState.
/// Fulfills requirement REQ-1-047.
/// </summary>
/// <param name="ChanceCardOrder">An immutable list of card IDs representing the current order of the Chance deck.</param>
/// <param name="CommunityChestCardOrder">An immutable list of card IDs representing the current order of the Community Chest deck.</param>
public record DeckStates
(
    [property: JsonPropertyName("chanceCardOrder")] IReadOnlyList<int> ChanceCardOrder,
    [property: JsonPropertyName("communityChestCardOrder")] IReadOnlyList<int> CommunityChestCardOrder
);