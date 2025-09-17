//-----------------------------------------------------------------------
// <copyright file="AIParameters.cs" company="MonopolyTycoon">
//     Copyright (c) MonopolyTycoon. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace MonopolyTycoon.Domain.AI
{
    /// <summary>
    /// Represents the configuration parameters that define an AI opponent's behavior and difficulty.
    /// This object is typically deserialized from an external JSON file, allowing for tunable AI strategies
    /// without recompiling the application.
    /// </summary>
    /// <remarks>
    /// This record directly supports requirements REQ-1-063, REQ-1-004, and REQ-1-064 by externalizing
    /// the parameters that govern AI decision-making.
    /// </remarks>
    public record AIParameters
    {
        /// <summary>
        /// Gets the friendly name of the difficulty level (e.g., "Easy", "Medium", "Hard").
        /// This is used for logging and potentially for selecting behavior tree scripts.
        /// </summary>
        public required string DifficultyLevel { get; init; }

        /// <summary>
        /// Gets the AI's willingness to take financial risks.
        /// A higher value (e.g., 0.8) means the AI is more likely to spend aggressively,
        /// while a lower value (e.g., 0.2) implies a more conservative playstyle.
        /// Expected range: 0.0 to 1.0.
        /// </summary>
        public required float RiskTolerance { get; init; }

        /// <summary>
        /// Gets the AI's priority for developing owned monopolies.
        /// A higher value (e.g., 0.9) means the AI will prioritize building houses and hotels
        /// as soon as it has a monopoly and sufficient funds. A lower value may cause the AI
        /// to hoard cash or prioritize acquiring other properties first.
        /// Expected range: 0.0 to 1.0.
        /// </summary>
        public required float BuildingAggressiveness { get; init; }

        /// <summary>
        /// Gets the AI's general willingness to propose and accept trades.
        /// A higher value (e.g., 0.75) indicates the AI will more frequently seek trades
        /// and may accept deals that are only slightly in its favor. A lower value
        /// means the AI will only consider highly advantageous trades.
        /// Expected range: 0.0 to 1.0.
        /// </summary>
        public required float TradeWillingness { get; init; }

        /// <summary>
        /// Gets the minimum amount of cash the AI will attempt to keep on hand.
        /// The AI will be reluctant to make purchases or trades that would drop its cash
        /// balance below this threshold, preventing it from easily going bankrupt from
        /// an unlucky rent payment. This value increases with difficulty.
        /// </summary>
        public required int MinimumCashReserve { get; init; }
    }
}