using MonopolyTycoon.Presentation.Events;
using MonopolyTycoon.Presentation.Shared.Services;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MonopolyTycoon.Presentation.Core
{
    /// <summary>
    /// Acts as a central hub for capturing and interpreting user input via Unity's New Input System.
    /// It translates low-level input actions into high-level, semantic application events published
    /// on the event bus. This decouples game and UI logic from the specific input source.
    /// It is designed as a persistent singleton.
    /// </summary>
    public class InputController : MonoBehaviour
    {
        public static InputController Instance { get; private set; }

        private IEventBus _eventBus;
        private PlayerInputActions _playerInputActions;

        // Using a DI framework like VContainer or Zenject, this would be injected.
        // For this example, we'll assume it's injected via a method from the composition root.
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            _playerInputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            _playerInputActions.Enable();
            SubscribeToInputActions();
        }

        private void OnDisable()
        {
            UnsubscribeFromInputActions();
            _playerInputActions.Disable();
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        private void SubscribeToInputActions()
        {
            if (_playerInputActions == null) return;

            // UI Actions
            _playerInputActions.UI.Cancel.performed += OnCancelPerformed;
            _playerInputActions.UI.Submit.performed += OnSubmitPerformed;

            // Gameplay Actions
            _playerInputActions.Gameplay.Pause.performed += OnPausePerformed;
        }

        private void UnsubscribeFromInputActions()
        {
            if (_playerInputActions == null) return;
            
            // UI Actions
            _playerInputActions.UI.Cancel.performed -= OnCancelPerformed;
            _playerInputActions.UI.Submit.performed -= OnSubmitPerformed;
            
            // Gameplay Actions
            _playerInputActions.Gameplay.Pause.performed -= OnPausePerformed;
        }

        /// <summary>
        /// Called when the 'Pause' action is triggered (e.g., Escape key).
        /// Publishes a semantic event to request a pause/resume action.
        /// </summary>
        private void OnPausePerformed(InputAction.CallbackContext context)
        {
            _eventBus?.Publish(new PauseToggleRequestedEvent());
        }

        /// <summary>
        /// Called when the 'Submit' action is triggered (e.g., Enter key).
        /// Useful for confirming actions in UI without a mouse.
        /// </summary>
        private void OnSubmitPerformed(InputAction.CallbackContext context)
        {
            _eventBus?.Publish(new UISubmitRequestedEvent());
        }

        /// <summary>
        /// Called when the 'Cancel' action is triggered (e.g., Escape key).
        /// This is often used to close menus or go back in the UI.
        /// Note: This is distinct from the Pause action, which may be the same key but is context-dependent.
        /// The presenters will decide how to interpret this event.
        /// </summary>
        private void OnCancelPerformed(InputAction.CallbackContext context)
        {
            _eventBus?.Publish(new UICancelRequestedEvent());
        }
    }
}