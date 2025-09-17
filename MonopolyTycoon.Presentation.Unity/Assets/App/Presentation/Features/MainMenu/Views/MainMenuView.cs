using MonopolyTycoon.Presentation.Features.MainMenu.Presenters;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace MonopolyTycoon.Presentation.Features.MainMenu.Views
{
    public class MainMenuView : MonoBehaviour, IMainMenuView
    {
        [Header("UI References")]
        [SerializeField] private TMP_InputField _playerNameInput;
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _loadGameButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private TextMeshProUGUI _validationErrorText;

        public event Action OnNewGameClicked;
        public event Action OnLoadGameClicked;
        public event Action OnSettingsClicked;
        public event Action OnQuitClicked;
        public event Action<string> OnPlayerNameChanged;

        [Inject]
        private void Construct(MainMenuPresenter presenter)
        {
            // The presenter is injected to establish the link.
            // In a pure MVP, the CompositionRoot would do this wiring.
            // VContainer's `Inject` on MonoBehaviour serves a similar purpose.
            presenter.SetView(this);
        }

        private void Awake()
        {
            _newGameButton.onClick.AddListener(() => OnNewGameClicked?.Invoke());
            _loadGameButton.onClick.AddListener(() => OnLoadGameClicked?.Invoke());
            _settingsButton.onClick.AddListener(() => OnSettingsClicked?.Invoke());
            _quitButton.onClick.AddListener(() => OnQuitClicked?.Invoke());
            _playerNameInput.onValueChanged.AddListener((name) => OnPlayerNameChanged?.Invoke(name));
        }
        
        public string GetPlayerName() => _playerNameInput.text;

        public void SetNewGameButtonEnabled(bool isEnabled)
        {
            _newGameButton.interactable = isEnabled;
        }

        public void SetValidationError(string message)
        {
            _validationErrorText.text = message;
            _validationErrorText.gameObject.SetActive(!string.IsNullOrEmpty(message));
        }

        private void OnDestroy()
        {
            _newGameButton.onClick.RemoveAllListeners();
            _loadGameButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _quitButton.onClick.RemoveAllListeners();
            _playerNameInput.onValueChanged.RemoveAllListeners();
        }
    }
}