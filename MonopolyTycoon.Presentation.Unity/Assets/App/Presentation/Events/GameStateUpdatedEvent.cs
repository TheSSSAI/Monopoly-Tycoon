// GameStateDto is assumed to be defined in a lower-level, referenceable assembly 
// like MonopolyTycoon.Application.Contracts. This event serves as a wrapper for it.
// using MonopolyTycoon.Application.Contracts.DataTransferObjects;

namespace MonopolyTycoon.Presentation.Events
{
    /// <summary>
    /// Event published whenever the authoritative game state changes.
    /// UI components subscribe to this event to refresh their views.
    /// This follows the event-driven pattern for UI updates to decouple
    /// the presentation layer from the application logic that triggers state changes.
    /// </summary>
    public readonly struct GameStateUpdatedEvent
    {
        // In a real project, this would be a DTO from the Application layer.
        // For generation purposes, we define a placeholder object.
        // public GameStateDto NewState { get; }
        // public GameStateUpdatedEvent(GameStateDto newState)
        // {
        //     NewState = newState;
        // }
    }
}