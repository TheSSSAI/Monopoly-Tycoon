using System;

namespace MonopolyTycoon.Presentation.Shared.Services
{
    /// <summary>
    /// Contract for an in-process event bus for decoupled communication
    /// between different parts of the presentation layer.
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Subscribes a handler to an event of a specific type.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event to subscribe to.</typeparam>
        /// <param name="handler">The action to execute when the event is published.</param>
        void Subscribe<TEvent>(Action<TEvent> handler);

        /// <summary>
        /// Unsubscribes a handler from an event of a specific type.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event to unsubscribe from.</typeparam>
        /// <param name="handler">The action to remove.</param>
        void Unsubscribe<TEvent>(Action<TEvent> handler);

        /// <summary>
        /// Publishes an event to all subscribed handlers.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event to publish.</typeparam>
        /// <param name="eventData">The event object containing data.</param>
        void Publish<TEvent>(TEvent eventData);
    }
}