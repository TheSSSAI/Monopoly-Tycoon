using System;
using System.Collections.Generic;
using MonopolyTycoon.Presentation.Events;
using Microsoft.Extensions.Logging;

namespace MonopolyTycoon.Presentation.Core
{
    /// <summary>
    /// A simple, thread-safe, in-process event bus for decoupled communication
    /// between different parts of the presentation layer.
    /// </summary>
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, Delegate> _handlers = new();
        private readonly object _lock = new();
        private readonly ILogger<EventBus> _logger;

        public EventBus(ILogger<EventBus> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Subscribe<T>(Action<T> handler) where T : IEvent
        {
            if (handler == null)
            {
                _logger.LogWarning("Attempted to subscribe with a null handler for event type {EventType}.", typeof(T).Name);
                return;
            }

            lock (_lock)
            {
                var eventType = typeof(T);
                if (_handlers.TryGetValue(eventType, out var existingHandler))
                {
                    _handlers[eventType] = Delegate.Combine(existingHandler, handler);
                }
                else
                {
                    _handlers[eventType] = handler;
                }
            }
            _logger.LogDebug("Handler subscribed for event type {EventType}.", typeof(T).Name);
        }

        public void Unsubscribe<T>(Action<T> handler) where T : IEvent
        {
            if (handler == null)
            {
                _logger.LogWarning("Attempted to unsubscribe with a null handler for event type {EventType}.", typeof(T).Name);
                return;
            }

            lock (_lock)
            {
                var eventType = typeof(T);
                if (_handlers.TryGetValue(eventType, out var existingHandler))
                {
                    var newHandler = Delegate.Remove(existingHandler, handler);
                    if (newHandler == null)
                    {
                        _handlers.Remove(eventType);
                    }
                    else
                    {
                        _handlers[eventType] = newHandler;
                    }
                    _logger.LogDebug("Handler unsubscribed for event type {EventType}.", typeof(T).Name);
                }
                else
                {
                    _logger.LogWarning("Attempted to unsubscribe from event type {EventType} with no subscribers registered.", typeof(T).Name);
                }
            }
        }

        public void Publish<T>(T eventToPublish) where T : IEvent
        {
            if (eventToPublish == null)
            {
                _logger.LogError("Attempted to publish a null event of type {EventType}.", typeof(T).Name);
                return;
            }
            
            _logger.LogInformation("Publishing event of type {EventType}.", typeof(T).Name);

            Delegate handlerDelegate;
            lock (_lock)
            {
                _handlers.TryGetValue(typeof(T), out handlerDelegate);
            }

            if (handlerDelegate == null)
            {
                _logger.LogWarning("No subscribers found for event of type {EventType}.", typeof(T).Name);
                return;
            }

            // Invocation is outside the lock to avoid deadlocks if a handler tries to subscribe/unsubscribe.
            // This is safe because delegates are immutable.
            var handlers = handlerDelegate.GetInvocationList();
            foreach (var handler in handlers)
            {
                try
                {
                    // This dynamic invoke is necessary because we can't cast to a specific Action<T>
                    // without knowing T at compile time within this loop.
                    // This is a common pattern for simple event buses.
                    if (handler is Action<T> action)
                    {
                        action(eventToPublish);
                    }
                    else
                    {
                        // Fallback for more complex delegate types if needed, though our IEventBus is simple.
                        handler.DynamicInvoke(eventToPublish);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred in a subscriber for event {EventType}.", typeof(T).Name);
                }
            }
        }
    }
}