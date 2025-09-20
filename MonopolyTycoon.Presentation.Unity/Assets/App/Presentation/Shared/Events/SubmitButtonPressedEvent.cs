namespace MonopolyTycoon.Presentation.Shared.Events
{
    /// <summary>
    /// A high-level, semantic event published by the InputController when a
    /// generic "Submit" or "Confirm" action is performed by the user (e.g., pressing Enter).
    /// Context-aware presenters can subscribe to this to trigger their primary action.
    /// </summary>
    public class SubmitButtonPressedEvent
    {
        // This event carries no data; its publication is the signal.
    }
}