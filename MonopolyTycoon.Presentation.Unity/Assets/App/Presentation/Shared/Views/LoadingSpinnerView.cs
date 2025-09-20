using UnityEngine;
using UnityEngine.UI;

namespace MonopolyTycoon.Presentation.Shared.Views
{
    /// <summary>
    /// A simple view component to control the visibility and animation
    /// of a loading spinner UI element.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class LoadingSpinnerView : MonoBehaviour
    {
        [SerializeField]
        private Image spinnerImage;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            Hide();
        }

        public void Show()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            // Add animation logic if needed, e.g., starting a rotation coroutine
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            // Stop animation logic
        }

        private void Update()
        {
            // Simple rotation animation
            if (_canvasGroup.alpha > 0 && spinnerImage != null)
            {
                spinnerImage.transform.Rotate(0, 0, -180f * Time.deltaTime);
            }
        }
    }
}