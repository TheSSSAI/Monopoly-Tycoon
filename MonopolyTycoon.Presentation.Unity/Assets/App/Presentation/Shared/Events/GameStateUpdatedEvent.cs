namespace MonopolyTycoon.Presentation.Shared.Events
{
    /// <summary>
    /// An event published on the in-process event bus whenever the authoritative
    /// GameState changes. UI components subscribe to this to refresh their displays.
    /// </summary>
    public class GameStateUpdatedEvent
    {
        /// <summary>
        /// A read-only representation of the new game state.
        /// This should be a DTO projected from the Application layer's GameState
        /// to prevent the Presentation layer from directly accessing domain models.
        /// </summary>
        public object NewGameState { get; }

        public GameStateUpdatedEvent(object newGameState)
        {
            NewGameState = newGameState;
        }
    }
}