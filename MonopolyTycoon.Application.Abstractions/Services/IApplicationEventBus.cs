using System;

namespace MonopolyTycoon.Application.Abstractions.Services
{
    /// <summary>
    /// Defines a contract for an in-process event bus (Mediator/Observer pattern).
    /// This enables decoupled communication between application layers, primarily for
    /// Application Services to notify the Presentation Layer of game state changes
    /// without a direct reference.
    /// </summary>
    public interface IApplicationEventBus
    {
        /// <summary>
        /// Publishes an event to all subscribed handlers for the event's type.
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