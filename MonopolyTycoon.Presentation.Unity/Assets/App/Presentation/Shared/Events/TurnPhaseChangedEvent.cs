namespace MonopolyTycoon.Presentation.Shared.Events
{
    /// <summary>
    /// An event published when the active player's turn transitions to a new phase.
    /// This allows the HUD and other UI components to update context-sensitive controls.
    /// </summary>
    public class TurnPhaseChangedEvent
    {
        public string PlayerId { get; }
        public TurnPhase NewPhase { get; }

        public TurnPhaseChangedEvent(string playerId, TurnPhase newPhase)
        {
            PlayerId = playerId;
            NewPhase = newPhase;
        }
    }

    /// <summary>
    /// Represents the distinct phases within a single player's turn.
    /// See REQ-1-038.
    /// </summary>
    public enum TurnPhase
    {
        PreTurn,
        PreRollManagement,
        AwaitingRoll,
        Movement,
        Action,
        PostRoll
    }
}