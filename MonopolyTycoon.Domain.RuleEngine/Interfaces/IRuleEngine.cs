using MonopolyTycoon.Domain.Core;
using MonopolyTycoon.Domain.Core.Abstractions;
using MonopolyTycoon.Domain.RuleEngine.Models;

namespace MonopolyTycoon.Domain.RuleEngine.Interfaces;

/// <summary>
/// Defines the public contract for the game's rule validation and state transition engine.
/// This is the primary interface consumed by the Application Services layer.
/// Implementations must be stateless and thread-safe.
/// </summary>
/// <remarks>
/// This service is the core of the game's logic, enforcing all official Monopoly rules as per REQ-1-003.
/// It operates using a functional approach, taking the current game state and an action, and returning
/// either a validation result or a new, transformed game state, without mutating the original state.
/// This stateless design is critical for ensuring testability (REQ-1-025).
/// </remarks>
public interface IRuleEngine
{
    /// <summary>
    /// Validates if a proposed player action is legal according to the official Monopoly rules,
    /// given the current game state.
    /// </summary>
    /// <param name="state">The current, immutable state of the game to validate against.</param>
    /// <param name="action">The proposed action to be validated.</param>
    /// <returns>
    /// A <see cref="ValidationResult"/> object indicating whether the action is valid.
    /// If invalid, the <see cref="ValidationResult.ErrorMessage"/> property will contain the reason.
    /// </returns>
    /// <remarks>
    /// This method must be a pure function with no side effects.
    /// Implementations should not throw exceptions for predictable, invalid game rule violations;
    /// they must return a result object with <see cref="ValidationResult.IsValid"/> set to false.
    /// </remarks>
    ValidationResult ValidateAction(GameState state, PlayerAction action);

    /// <summary>
    /// Applies a player action to the game state and returns the new, resulting state.
    /// This method assumes the action has already been validated.
    /// </summary>
    /// <param name="state">The current, immutable game state.</param>
    /// <param name="action">The action to apply.</param>
    /// <returns>A new <see cref="GameState"/> instance representing the state after the action is applied.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="state"/> or <paramref name="action"/> is null.</exception>
    /// <exception cref="Exceptions.RuleEngineInvariantException">
    /// Thrown if an invalid action is provided that violates a core game invariant. This indicates a logic
    /// error in the calling layer (e.g., the Application Service), which should have validated the action first.
    /// </exception>
    /// <remarks>
    /// This method must be a pure function. It must not mutate the input <paramref name="state"/> object.
    /// It should create a deep copy of the state, apply the changes, and return the new instance.
    /// </remarks>
    GameState ApplyAction(GameState state, PlayerAction action);
}