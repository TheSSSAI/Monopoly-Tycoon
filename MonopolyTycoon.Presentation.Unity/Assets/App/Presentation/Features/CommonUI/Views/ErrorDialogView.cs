using MonopolyTycoon.Application.Contracts;
using MonopolyTycoon.Presentation.Core;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace MonopolyTycoon.Presentation.Features.CommonUI.Views
{
    public class ErrorDialogView : MonoBehaviour, IErrorDialogView
    {
        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI _errorIdText;
        [SerializeField] private TextMeshProUGUI _logPathText;
        [SerializeField] private Button _closeButton;
        
        [Inject]
        private readonly ILoggerAdapter<ErrorDialogView> _logger;

        public event Action OnCloseRequested;

        private void Awake()
        {
            if (_closeButton == null || _errorIdText == null || _logPathText == null)
            {
                _logger.LogError("ErrorDialogView is missing required UI component references.");
                gameObject.SetActive(false);
                return;
            }
            _closeButton.onClick.AddListener(HandleCloseClick);
        }

        public void Show(string errorId, string logPath)
        {
            if (string.IsNullOrEmpty(errorId) || string.IsNullOrEmpty(logPath))
            {
                _logger.LogError("Cannot show error dialog with null or empty parameters.");
                // Show a generic error instead
                _errorIdText.text = "ID: UNKNOWN";
                _logPathText.text = "Log path could not be determined.";
            }
            else
            {
                _errorIdText.text = $"Error ID: {errorId}";
                _logPathText.text = $"Log files can be found at: {logPath}";
            }
            
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void HandleCloseClick()
        {
            _logger.LogInformation("Close button clicked on error dialog.");
            OnCloseRequested?.Invoke();
        }

        private void OnDestroy()
        {
            if (_closeButton != null)
            {
                _closeButton.onClick.RemoveListener(HandleCloseClick);
            }
        }
    }
}