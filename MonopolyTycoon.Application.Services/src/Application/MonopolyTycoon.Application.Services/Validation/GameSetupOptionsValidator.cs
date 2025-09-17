using FluentValidation;
using MonopolyTycoon.Application.Services.DTOs;

namespace MonopolyTycoon.Application.Services.Validation;

/// <summary>
/// Validator for the <see cref="GameSetupOptions"/> DTO using FluentValidation.
/// This class enforces the business rules required for creating a new game session.
/// The rules are derived from requirements REQ-1-007, REQ-1-029, and REQ-1-032.
/// </summary>
public class GameSetupOptionsValidator : AbstractValidator<GameSetupOptions>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GameSetupOptionsValidator"/> class
    /// and defines the validation rules.
    /// </summary>
    public GameSetupOptionsValidator()
    {
        // Rule for Human Player Name (REQ-1-032)
        RuleFor(x => x.HumanPlayerName)
            .NotEmpty().WithMessage("Player name is required.")
            .Length(3, 16).WithMessage("Player name must be between 3 and 16 characters.")
            .Matches("^[a-zA-Z0-9]+$").WithMessage("Player name can only contain letters and numbers.");

        // Rule for the list of AI Opponents (REQ-1-007, REQ-1-029)
        RuleFor(x => x.AiOpponents)
            .NotNull().WithMessage("AI opponent configuration cannot be null.")
            .NotEmpty().WithMessage("At least one AI opponent must be configured.")
            .Must(opponents => opponents.Count >= 1 && opponents.Count <= 3)
            .WithMessage("You must configure between 1 and 3 AI opponents.");

        // Rule for each individual AI opponent in the list (REQ-1-030)
        RuleForEach(x => x.AiOpponents).ChildRules(opponent =>
        {
            opponent.RuleFor(o => o.Difficulty)
                .IsInEnum().WithMessage("A valid difficulty must be selected for each AI opponent.");
        });
    }
}