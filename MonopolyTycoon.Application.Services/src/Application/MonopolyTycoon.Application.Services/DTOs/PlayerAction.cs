namespace MonopolyTycoon.Application.Services.DTOs
{
    /// <summary>
    /// Abstract base record for any action a player can perform during the game.
    /// Specific actions (e.g., BuildHouseAction, ProposeTradeAction) should inherit from this record.
    /// This serves as a common contract for passing actions from the Presentation layer to the Application services.
    /// </summary>
    /// <param name="PlayerId">The unique identifier of the player performing the action.</param>
    public abstract record PlayerAction(Guid PlayerId);
}