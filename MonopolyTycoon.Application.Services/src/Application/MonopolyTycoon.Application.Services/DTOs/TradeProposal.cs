namespace MonopolyTycoon.Application.Services.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a complete trade offer between two players.
    /// This is used to communicate a proposed trade from the Presentation layer to the
    /// Application layer for validation and processing.
    /// Fulfills requirements: REQ-1-059, REQ-1-060.
    /// </summary>
    /// <param name="OfferingPlayerId">The unique identifier of the player initiating the trade.</param>
    /// <param name="TargetPlayerId">The unique identifier of the player receiving the trade offer.</param>
    /// <param name="PropertiesOffered">A list of unique property identifiers offered by the initiating player.</param>
    /// <param name="CashOffered">The amount of cash offered by the initiating player.</param>
    /// <param name="GetOutOfJailCardsOffered">The number of 'Get Out of Jail Free' cards offered.</param>
    /// <param name="PropertiesRequested">A list of unique property identifiers requested from the target player.</param>
    /// <param name="CashRequested">The amount of cash requested from the target player.</param>
    /// <param name="GetOutOfJailCardsRequested">The number of 'Get Out of Jail Free' cards requested.</param>
    public record TradeProposal(
        Guid OfferingPlayerId,
        Guid TargetPlayerId,
        List<string> PropertiesOffered,
        decimal CashOffered,
        int GetOutOfJailCardsOffered,
        List<string> PropertiesRequested,
        decimal CashRequested,
        int GetOutOfJailCardsRequested);
}