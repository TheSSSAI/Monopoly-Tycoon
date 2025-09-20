using MonopolyTycoon.Presentation.Shared.Views;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonopolyTycoon.Presentation.Shared
{
    public class ModalDialogView : MonoBehaviour, IModalDialogView
    {
        [Header("UI References")]
        [SerializeField]
        private TextMeshProUGUI titleText;
        [SerializeField]
        private TextMeshProUGUI messageText;
        [SerializeField]
        private Button primaryButton;
        [SerializeField]
        private TextMeshProUGUI primaryButtonText;
        [SerializeField]
        private Button secondaryButton;
        [SerializeField]
        private TextMeshProUGUI secondaryButtonText;
        [SerializeField]
        private Button tertiaryButton;
        [SerializeField]
        private TextMeshProUGUI tertiaryButtonText;

        private CanvasGroup _canvasGroup;
        private Action _primaryAction;
        private Action _secondaryAction;
        private Action _tertiaryAction;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            if (_canvasGroup == null)
            {
                _canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }

            primaryButton.onClick.AddListener(OnPrimaryClicked);
            secondaryButton.onClick.AddListener(OnSecondaryClicked);
            tertiaryButton.onClick.AddListener(OnTertiaryClicked);
        }

        private void OnDestroy()
        {
            primaryButton.onClick.RemoveAllListeners();
            secondaryButton.onClick.RemoveAllListeners();
            tertiaryButton.onClick.RemoveAllListeners();
        }

        public void Configure(DialogViewModel viewModel)
        {
            titleText.text = viewModel.Title;
            messageText.text = viewModel.Message;

            ConfigureButton(primaryButton, primaryButtonText, viewModel.PrimaryButton, ref _primaryAction);
            ConfigureButton(secondaryButton, secondaryButtonText, viewModel.SecondaryButton, ref _secondaryAction);
            ConfigureButton(tertiaryButton, tertiaryButtonText, viewModel.TertiaryButton, ref _tertiaryAction);
        }

        public void Show()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        private void ConfigureButton(Button button, TextMeshProUGUI buttonText, DialogButtonViewModel buttonViewModel, ref Action action)
        {
            if (buttonViewModel != null)
            {
                button.gameObject.SetActive(true);
                buttonText.text = buttonViewModel.Text;
                action = buttonViewModel.Action;
            }
            else
            {
                button.gameObject.SetActive(false);
                action = null;
            }
        }

        private void OnPrimaryClicked()
        {
            _primaryAction?.Invoke();
            Hide();
        }

        private void OnSecondaryClicked()
        {
            _secondaryAction?.Invoke();
            Hide();
        }

        private void OnTertiaryClicked()
        {
            _tertiaryAction?.Invoke();
            Hide();
        }
    }
}