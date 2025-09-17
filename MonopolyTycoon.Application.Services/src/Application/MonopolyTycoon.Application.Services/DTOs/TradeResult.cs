namespace MonopolyTycoon.Application.Services.DTOs
{
    /// <summary>
    /// Represents the possible outcomes of a trade proposal after evaluation.
    /// This is returned by the Application layer to the Presentation layer.
    /// </summary>
    public enum TradeResult
    {
        /// <summary>
        /// The trade was accepted by the target player and has been successfully executed.
        /// </summary>
        Accepted,

        /// <summary>
        /// The trade was declined by the target player.
        /// </summary>
        Declined,

        /// <summary>
        /// The trade was invalid according to game rules (e.g., proposed at the wrong time, involved invalid assets).
        /// </summary>
        Invalid
    }
}