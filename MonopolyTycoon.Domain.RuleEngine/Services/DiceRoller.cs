using MonopolyTycoon.Domain.RuleEngine.Interfaces;
using MonopolyTycoon.Domain.RuleEngine.Models;
using System.Security.Cryptography;

namespace MonopolyTycoon.Domain.RuleEngine.Services
{
    /// <summary>
    /// Provides a cryptographically secure method for rolling two six-sided dice,
    /// ensuring fairness and unpredictability as required by REQ-1-042.
    /// </summary>
    public class DiceRoller : IDiceRoller
    {
        private static readonly RandomNumberGenerator _rng;

        static DiceRoller()
        {
            _rng = RandomNumberGenerator.Create();
        }

        /// <summary>
        /// Generates and returns the result of rolling two six-sided dice using a
        /// cryptographically secure random number generator.
        /// </summary>
        /// <returns>A <see cref="DiceRoll"/> value object containing the results of the two dice.</returns>
        public DiceRoll Roll()
        {
            // RandomNumberGenerator.GetInt32 is inclusive for the lower bound and exclusive for the upper bound.
            // So GetInt32(1, 7) will return an integer from 1 to 6.
            int die1 = RandomNumberGenerator.GetInt32(1, 7);
            int die2 = RandomNumberGenerator.GetInt32(1, 7);

            return new DiceRoll(die1, die2);
        }
    }
}