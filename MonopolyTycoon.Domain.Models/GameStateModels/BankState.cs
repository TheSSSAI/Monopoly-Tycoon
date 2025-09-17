using System.Text.Json.Serialization;

namespace MonopolyTycoon.Domain.Models.GameStateModels;

/// <summary>
/// Represents the state of the game's central bank, specifically the supply
/// of available houses and hotels for purchase.
/// This record is immutable and a component of the overall GameState.
/// Fulfills requirement REQ-1-055.
/// </summary>
/// <param name="HousesRemaining">The number of house tokens remaining in the bank's supply (initially 32).</param>
/// <param name="HotelsRemaining">The number of hotel tokens remaining in the bank's supply (initially 12).</param>
public record BankState
(
    [property: JsonPropertyName("housesRemaining")] int HousesRemaining,
    [property: JsonPropertyName("hotelsRemaining")] int HotelsRemaining
);