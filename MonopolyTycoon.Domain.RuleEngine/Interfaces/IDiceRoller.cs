using MonopolyTycoon.Domain.RuleEngine.Models;

namespace MonopolyTycoon.Domain.RuleEngine.Interfaces;

/// <summary>
/// Defines the public contract for the dice rolling service.
/// </summary>
/// <remarks>
/// Implementations of this interface are required to be thread-safe and use a 
/// cryptographically secure random number generator to fulfill the fairness and
/// unpredictability requirements of REQ-1-042.
/// </remarks>
public interface IDiceRoller
{
    /// <summary>
    /// Generates and returns the result of rolling two independent, six-sided dice.
    /// </summary>
    /// <returns>A <see cref="DiceRoll"/> value object containing the results of the two dice.</returns>
    /// <remarks>
    /// This method should be exception-free under normal operating conditions.
    /// The randomness must be sourced from a cryptographically strong provider.
    /// </remarks>
    DiceRoll Roll();
}