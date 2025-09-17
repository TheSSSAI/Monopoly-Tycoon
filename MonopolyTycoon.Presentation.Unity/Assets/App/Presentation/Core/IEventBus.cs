using System;

namespace MonopolyTycoon.Presentation.Core
{
    /// <summary>
    /// Represents a message or event that can be published through the event bus.
    /// This is a marker interface.
    /// </summary>
    public interface IEvent { }

    /// <summary>
    /// Defines the contract for a global, in-process event bus.
    /// This allows for decoupled communication between different parts of the application,
    /// primarily between the Application/Domain layers and the Presentation layer.
    /// It implements the Observer (Publish-Subscribe) pattern.
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Subscribes a handler to a specific event type.
        /// </summary>
        /// <typeparam name="T">The type of event to subscribe to. Must implement IEvent.</typeparam>
        /// <param name="handler">The action to execute when an event of type T is published.</param>
        void Subscribe<T>(Action<T> handler) where T : class, IEvent;

        /// <summary>
        /// Unsubscribes a handler from a specific event type.
        /// </summary>
        /// <typeparam name="T">The type of event to unsubscribe from. Must implement IEvent.</typeparam>
        /// <param name="handler">The handler action to remove.</param>
        void Unsubscribe<T>(Action<T> handler) where T : class, IEvent;

        /// <summary>
        /// Publishes an event to all subscribed handlers.
        /// </summary>
        /// <typeparam name="T">The type of event being published. Must implement IEvent.</typeparam>
        /// <param name="eventToPublish">The event instance to publish.</param>
        void Publish<T>(T eventToPublish) where T : class, IEvent;
    }
}