using MonopolyTycoon.Domain.Entities;
using MonopolyTycoon.Presentation.Core;

namespace MonopolyTycoon.Presentation.Events
{
    /// <summary>
    /// Event published on the IEventBus whenever a significant change to the game state occurs.
    /// UI presenters subscribe to this to know when to refresh their views.
    /// The GameState object is expected to be from the Domain layer but is referenced here
    /// for simplicity in the presentation layer. In a stricter setup, this would be a DTO.
    /// </summary>
    public class GameStateUpdatedEvent : IEvent
    {
        /// <summary>
        /// A read-only snapshot of the new game state.
        /// </summary>
        public GameState NewGameState { get; }

        /// <summary>
        /// A description of what triggered the update (e.g., "RentPaid", "TradeCompleted").
        /// This can help subscribers apply more targeted updates.
        /// </summary>
        public string ChangeContext { get; }

        public GameStateUpdatedEvent(GameState newGameState, string changeContext)
        {
            NewGameState = newGameState;
            ChangeContext = changeContext;
        }
    }
}