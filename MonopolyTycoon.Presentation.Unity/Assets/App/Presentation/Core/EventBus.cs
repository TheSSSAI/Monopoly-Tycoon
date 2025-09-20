using MonopolyTycoon.Presentation.Shared.Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MonopolyTycoon.Presentation.Core
{
    /// <summary>
    /// A simple, in-memory, synchronous event bus for decoupling components within the Presentation layer.
    /// This allows different UI presenters and services to communicate without holding direct references to each other.
    /// </summary>
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, object> _subscribers = new();

        public void Subscribe<TEvent>(Action<TEvent> handler)
        {
            var eventType = typeof(TEvent);
            if (!_subscribers.TryGetValue(eventType, out var handlers))
            {
                handlers = new Action<TEvent>(handler);
            }
            else
            {
                handlers = (Action<TEvent>)handlers + handler;
            }
            _subscribers[eventType] = handlers;
        }

        public void Unsubscribe<TEvent>(Action<TEvent> handler)
        {
            var eventType = typeof(TEvent);
            if (_subscribers.TryGetValue(eventType, out var handlers))
            {
                handlers = (Action<TEvent>)handlers - handler;
                if (handlers == null)
                {
                    _subscribers.Remove(eventType);
                }
                else
                {
                    _subscribers[eventType] = handlers;
                }
            }
        }

        public void Publish<TEvent>(TEvent eventToPublish)
        {
            var eventType = typeof(TEvent);
            if (_subscribers.TryGetValue(eventType, out var handlers))
            {
                try
                {
                    ((Action<TEvent>)handlers)?.Invoke(eventToPublish);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"[EventBus] Error while publishing event {eventType.Name}: {ex}");
                }
            }
        }
    }
}