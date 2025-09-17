namespace MonopolyTycoon.Domain.RuleEngine.Models
{
    /// <summary>
    /// Represents the result of rolling two six-sided dice.
    /// This is an immutable value object that ensures its values are always valid.
    /// </summary>
    public readonly record struct DiceRoll
    {
        /// <summary>
        /// The result of the first die (1-6).
        /// </summary>
        public int Die1 { get; }

        /// <summary>
        /// The result of the second die (1-6).
        /// </summary>
        public int Die2 { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiceRoll"/> struct.
        /// </summary>
        /// <param name="die1">The value of the first die.</param>
        /// <param name="die2">The value of the second die.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if either die value is not between 1 and 6.</exception>
        public DiceRoll(int die1, int die2)
        {
            if (die1 < 1 || die1 > 6)
            {
                throw new System.ArgumentOutOfRangeException(nameof(die1), "Die value must be between 1 and 6.");
            }

            if (die2 < 1 || die2 > 6)
            {
                throw new System.ArgumentOutOfRangeException(nameof(die2), "Die value must be between 1 and 6.");
            }

            Die1 = die1;
            Die2 = die2;
        }

        /// <summary>
        /// Gets the sum of both dice.
        /// </summary>
        public int Total => Die1 + Die2;

        /// <summary>
        /// Gets a value indicating whether both dice have the same value.
        /// </summary>
        public bool IsDoubles => Die1 == Die2;
    }
}