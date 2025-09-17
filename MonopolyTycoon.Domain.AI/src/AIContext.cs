using MonopolyTycoon.Domain.Actions;
using MonopolyTycoon.Domain.Entities;
using System;
using System.Diagnostics.CodeAnalysis;

namespace MonopolyTycoon.Domain.AI
{
    /// <summary>
    /// A transient context object created for each AI decision-making call.
    /// It holds all necessary data for the behavior tree nodes to perform their logic,
    /// serving as a shared "blackboard" for a single tree execution ("tick").
    /// This avoids passing multiple parameters to every node.
    /// </summary>
    public class AIContext
    {
        /// <summary>
        /// Gets the current, read-only state of the entire game.
        /// Behavior tree nodes use this to evaluate conditions and make decisions.
        /// </summary>
        public GameState GameState { get; }

        /// <summary>
        /// Gets the unique identifier of the AI player whose turn is being evaluated.
        /// </summary>
        public Guid SelfPlayerId { get; }

        /// <summary>
        /// Gets the configuration object defining the AI's strategy and difficulty level.
        /// This drives the strategic choices made by the behavior tree nodes.
        /// </summary>
        public AIParameters Parameters { get; }

        /// <summary>
        /// Gets the <see cref="PlayerState"/> of the AI player currently being evaluated.
        /// This is a convenience property derived from the GameState.
        /// </summary>
        [NotNull]
        public PlayerState Self { get; }

        /// <summary>
        /// Gets or sets the action decided upon by a behavior tree action node.
        /// This property is written to by a successful action node and is the
        /// primary output of the behavior tree execution. It is nullable, as
        /// no action may have been decided yet.
        /// </summary>
        public PlayerAction? ResultAction { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AIContext"/> class.
        /// </summary>
        /// <param name="gameState">The current state of the game.</param>
        /// <param name="selfPlayerId">The ID of the AI player making the decision.</param>
        /// <param name="parameters">The parameters dictating the AI's behavior.</param>
        /// <exception cref="ArgumentNullException">Thrown if gameState or parameters are null.</exception>
        /// <exception cref="ArgumentException">Thrown if the AI player with selfPlayerId is not found in the gameState.</exception>
        public AIContext(GameState gameState, Guid selfPlayerId, AIParameters parameters)
        {
            GameState = gameState ?? throw new ArgumentNullException(nameof(gameState));
            SelfPlayerId = selfPlayerId;
            Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            
            // Find the player state for the current AI player for easy access.
            PlayerState? selfPlayerState = null;
            foreach (var player in GameState.PlayerStates)
            {
                if (player.PlayerId == selfPlayerId)
                {
                    selfPlayerState = player;
                    break;
                }
            }
            
            if (selfPlayerState == null)
            {
                throw new ArgumentException($"AI player with ID {selfPlayerId} not found in the provided GameState.", nameof(selfPlayerId));
            }
            
            Self = selfPlayerState;

            // ResultAction starts as null and is set by an action node in the behavior tree.
            ResultAction = null;
        }
    }
}