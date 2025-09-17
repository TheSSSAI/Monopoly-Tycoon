namespace MonopolyTycoon.Domain.RuleEngine.Models
{
    /// <summary>
    /// Represents the outcome of a rule validation check.
    /// This is an immutable value object.
    /// </summary>
    public readonly record struct ValidationResult
    {
        /// <summary>
        /// Gets a value indicating whether the validation was successful.
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Gets the descriptive reason for a validation failure.
        /// Returns <see cref="string.Empty"/> if the validation was successful.
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// Private constructor to enforce creation via static factory methods.
        /// </summary>
        private ValidationResult(bool isValid, string errorMessage)
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Represents a successful validation result.
        /// </summary>
        public static ValidationResult Success => new(true, string.Empty);

        /// <summary>
        /// Creates a failed validation result with a specific error message.
        /// </summary>
        /// <param name="errorMessage">The reason for the validation failure. Must not be null or whitespace.</param>
        /// <returns>A new instance of <see cref="ValidationResult"/> representing a failure.</returns>
        /// <exception cref="System.ArgumentException">Thrown if errorMessage is null or whitespace.</exception>
        public static ValidationResult Failure(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                throw new System.ArgumentException("An error message must be provided for a failed validation result.", nameof(errorMessage));
            }

            return new ValidationResult(false, errorMessage);
        }
    }
}