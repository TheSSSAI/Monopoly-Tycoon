namespace MonopolyTycoon.Application.Abstractions.Services
{
    /// <summary>
    /// Defines a contract for an in-process event bus, enabling a decoupled publish-subscribe
    /// communication pattern between different parts of the application. This is primarily
    /// used to allow Application Services to notify the Presentation layer of state changes
    /// without creating a direct dependency.
    /// </summary>
    public interface IApplicationEventBus
    {
        /// <summary>
        /// Publishes an event to all subscribed handlers for that event type.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event being published.</typeparam>
        /// <param name="anEvent">The event object to publish.</param>
        void Publish<TEvent>(TEvent anEvent) where TEvent : class;

        /// <summary>
        /// Subscribes a handler to a specific event type.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event to subscribe to.</typeparam>
        /// <param name="handler">The callback method (handler) to invoke when an event of type TEvent is published.</param>
        void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : class;

        /// <summary>
        /// Unsubscribes a handler from a specific event type.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event to unsubscribe from.</typeparam>
        /// <param name="handler">The handler to remove.</param>
        void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : class;
    }
}