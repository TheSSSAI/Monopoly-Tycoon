using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using MonopolyTycoon.Application.Abstractions;
using MonopolyTycoon.Presentation.Shared.Events;
using System;

namespace MonopolyTycoon.Presentation.Core
{
    /// <summary>
    /// Centralized handler for Unity's Input System.
    /// It translates low-level input actions into high-level, semantic application events.
    /// This decouples the rest of the application from the specifics of the input device.
    /// </summary>
    public class InputController : MonoBehaviour
    {
        private IEventBus _eventBus;
        private PlayerInputActions _playerInputActions;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            _playerInputActions.UI.Enable();
            _playerInputActions.UI.Submit.performed += OnSubmitPerformed;
            _playerInputActions.UI.Cancel.performed += OnCancelPerformed;
            // Add other subscriptions as needed (e.g., Navigate)
        }

        private void OnDisable()
        {
            _playerInputActions.UI.Submit.performed -= OnSubmitPerformed;
            _playerInputActions.UI.Cancel.performed -= OnCancelPerformed;
            _playerInputActions.UI.Disable();
        }

        private void OnSubmitPerformed(InputAction.CallbackContext context)
        {
            // Instead of handling logic here, we publish a semantic event
            // that interested systems (like a UI presenter) can subscribe to.
            _eventBus.Publish(new SubmitButtonPressedEvent());
        }
        
        private void OnCancelPerformed(InputAction.CallbackContext context)
        {
            // Publish a cancel event, useful for closing menus or dialogs
            // with the 'Escape' key.
            _eventBus.Publish(new CancelButtonPressedEvent());
        }
    }
}