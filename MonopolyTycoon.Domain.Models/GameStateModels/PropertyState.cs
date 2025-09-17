using System;
using System.Text.Json.Serialization;

namespace MonopolyTycoon.Domain.Models.GameStateModels;

/// <summary>
/// Represents the dynamic state of a single ownable property on the game board.
/// This record is immutable and is a component of the BoardState.
/// </summary>
/// <param name="OwnerId">The PlayerId of the owner. A null value indicates the property is unowned by any player.</param>
/// <param name="HouseCount">The number of houses on the property. As per game rules, a value of 5 represents a hotel.</param>
/// <param name="IsMortgaged">A flag indicating if the property is currently mortgaged.</param>
public record PropertyState
(
    [property: JsonPropertyName("ownerId")] Guid? OwnerId,
    [property: JsonPropertyName("houseCount")] int HouseCount,
    [property: JsonPropertyName("isMortgaged")] bool IsMortgaged
);