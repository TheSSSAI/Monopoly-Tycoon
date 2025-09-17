#nullable enable

using System.Collections.Generic;
using System.Text.Json.Serialization;
using MonopolyTycoon.Domain.Models.GameStateModels;

/// <summary>
/// Represents the state of all ownable properties on the game board within a game session.
/// This record is an immutable container for the collection of individual property states.
/// </summary>
/// <param name="Properties">
/// An immutable dictionary mapping a property's tile index (0-39) to its current state.
/// The use of IReadOnlyDictionary ensures that the collection of properties cannot be
/// modified after the BoardState has been created, promoting predictable state management.
/// Efficiently allows looking up the state of any property by its unique board position.
/// </param>
public record BoardState
(
    [property: JsonPropertyName("properties")]
    IReadOnlyDictionary<int, PropertyState> Properties
);