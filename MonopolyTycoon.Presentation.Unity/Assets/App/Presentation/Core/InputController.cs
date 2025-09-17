using UnityEngine;
using UnityEngine.InputSystem;
using MonopolyTycoon.Presentation.Core;
using MonopolyTycoon.Presentation.Events;
using VContainer;

namespace MonopolyTycoon.Presentation.Core
{
    /// <summary>
    /// A persistent singleton responsible for capturing raw input from Unity's Input System
    /// and translating it into semantic events on the global IEventBus.
    /// This decouples specific input hardware from the UI/game logic that consumes the actions.
    /// It requires a PlayerInput component attached to the same GameObject.
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]
    public class InputController : MonoBehaviour
    {
        private IEventBus eventBus;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        // These methods are invoked by the PlayerInput component via "Send Messages" broadcast.
        // Their names must match the Action names in the Input Action Asset.

        public void OnNavigate(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                eventBus.Publish(new NavigateEvent { Direction = context.ReadValue<Vector2>() });
            }
        }

        public void OnSubmit(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                eventBus.Publish(new SubmitEvent());
            }
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                eventBus.Publish(new CancelEvent());
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                eventBus.Publish(new PauseEvent());
            }
        }
    }

    #region Input Event DTOs

    // These simple structs act as event data carriers on the event bus.

    public struct NavigateEvent : IEvent
    {
        public Vector2 Direction;
    }

    public struct SubmitEvent : IEvent { }

    public struct CancelEvent : IEvent { }

    public struct PauseEvent : IEvent { }

    #endregion
}