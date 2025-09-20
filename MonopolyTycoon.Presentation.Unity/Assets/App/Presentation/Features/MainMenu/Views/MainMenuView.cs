using MonopolyTycoon.Presentation.Features.MainMenu.Views;
using UnityEngine;
using UnityEngine.UI;

namespace MonopolyTycoon.Presentation.Features.MainMenu
{
    public class MainMenuView : MonoBehaviour, IMainMenuView
    {
        [Header("UI References")]
        [SerializeField]
        private Button newGameButton;

        [SerializeField]
        private Button loadGameButton;

        [SerializeField]
        private Button settingsButton;

        [SerializeField]
        private Button quitButton;

        public event System.Action OnNewGameClicked;
        public event System.Action OnLoadGameClicked;
        public event System.Action OnSettingsClicked;
        public event System.Action OnQuitClicked;

        private void Awake()
        {
            // Wire up button events to the public events
            newGameButton.onClick.AddListener(() => OnNewGameClicked?.Invoke());
            loadGameButton.onClick.AddListener(() => OnLoadGameClicked?.Invoke());
            settingsButton.onClick.AddListener(() => OnSettingsClicked?.Invoke());
            quitButton.onClick.AddListener(() => OnQuitClicked?.Invoke());
        }

        private void OnDestroy()
        {
            // Clean up listeners to prevent memory leaks
            newGameButton.onClick.RemoveAllListeners();
            loadGameButton.onClick.RemoveAllListeners();
            settingsButton.onClick.RemoveAllListeners();
            quitButton.onClick.RemoveAllListeners();
        }

        public void SetButtonInteractable(string buttonName, bool isInteractable)
        {
            switch (buttonName.ToLower())
            {
                case "newgame":
                    newGameButton.interactable = isInteractable;
                    break;
                case "loadgame":
                    loadGameButton.interactable = isInteractable;
                    break;
                case "settings":
                    settingsButton.interactable = isInteractable;
                    break;
                case "quit":
                    quitButton.interactable = isInteractable;
                    break;
                default:
                    Debug.LogWarning($"[MainMenuView] Unknown button name: {buttonName}");
                    break;
            }
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}