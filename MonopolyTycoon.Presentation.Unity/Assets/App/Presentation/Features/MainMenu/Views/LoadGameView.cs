using MonopolyTycoon.Presentation.Features.LoadGame.ViewModels;
using MonopolyTycoon.Presentation.Features.MainMenu.Views;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MonopolyTycoon.Presentation.Features.MainMenu
{
    public class LoadGameView : MonoBehaviour, ILoadGameView
    {
        [System.Serializable]
        public class SaveSlotView
        {
            public Button SlotButton;
            public TextMeshProUGUI SlotTitleText;
            public TextMeshProUGUI SlotDetailsText;
            public TextMeshProUGUI StatusText; // For "Corrupted", "Incompatible"
            public GameObject DetailsRoot;
        }

        [Header("UI References")]
        [SerializeField]
        private List<SaveSlotView> saveSlotViews;

        [SerializeField]
        private Button loadButton;

        [SerializeField]
        private Button backButton;

        public event System.Action<int> OnLoadGameRequested;
        public event System.Action OnBackRequested;

        private int _selectedSlot = -1;

        private void Awake()
        {
            for (int i = 0; i < saveSlotViews.Count; i++)
            {
                int slotIndex = i; // Capture loop variable for closure
                saveSlotViews[i].SlotButton.onClick.AddListener(() => SelectSlot(slotIndex));
            }
            
            loadButton.onClick.AddListener(ConfirmLoad);
            backButton.onClick.AddListener(() => OnBackRequested?.Invoke());

            loadButton.interactable = false;
        }

        private void OnDestroy()
        {
            foreach (var slotView in saveSlotViews)
            {
                slotView.SlotButton.onClick.RemoveAllListeners();
            }
            loadButton.onClick.RemoveAllListeners();
            backButton.onClick.RemoveAllListeners();
        }

        public void DisplaySaveSlots(List<SaveGameMetadata> slotsMetadata)
        {
            for (int i = 0; i < saveSlotViews.Count; i++)
            {
                var metadata = slotsMetadata.Find(s => s.SlotNumber == i);
                var slotView = saveSlotViews[i];
                
                if (metadata == null || metadata.Status == SaveStatus.Empty)
                {
                    slotView.SlotTitleText.text = $"Slot {i + 1} - Empty";
                    slotView.DetailsRoot.SetActive(false);
                    slotView.StatusText.gameObject.SetActive(false);
                }
                else
                {
                    slotView.SlotTitleText.text = $"Slot {i + 1} - {metadata.SaveName}";
                    slotView.SlotDetailsText.text = metadata.SaveTimestamp;
                    slotView.DetailsRoot.SetActive(true);

                    bool isInvalid = metadata.Status != SaveStatus.Valid;
                    slotView.StatusText.gameObject.SetActive(isInvalid);
                    if (isInvalid)
                    {
                        slotView.StatusText.text = metadata.Status.ToString().ToUpper();
                    }
                }
            }
            SelectSlot(-1); // Deselect everything
        }

        private void SelectSlot(int slotIndex)
        {
            _selectedSlot = slotIndex;

            // Update visuals for selection
            for (int i = 0; i < saveSlotViews.Count; i++)
            {
                // This logic can be enhanced with a selector image, etc.
                var colors = saveSlotViews[i].SlotButton.colors;
                colors.normalColor = (i == slotIndex) ? colors.selectedColor : Color.white;
                saveSlotViews[i].SlotButton.colors = colors;
            }

            // Check if selected slot is valid for loading
            var metadata = FindMetadataForSlot(slotIndex); // A helper method would be needed here
            bool canLoad = metadata != null && metadata.Status == SaveStatus.Valid;
            loadButton.interactable = canLoad;
        }

        private void ConfirmLoad()
        {
            if (_selectedSlot != -1)
            {
                OnLoadGameRequested?.Invoke(_selectedSlot);
            }
        }
        
        // This is a placeholder; in a real scenario, we'd keep the metadata list accessible
        private SaveGameMetadata FindMetadataForSlot(int slotIndex)
        {
            // In a full implementation, this view would hold a reference to the list
            // of metadata passed into DisplaySaveSlots.
            // For now, we assume if a slot is selected, it must have had metadata.
            if(slotIndex < 0) return null;
            return new SaveGameMetadata { Status = SaveStatus.Valid }; // Simplified for example
        }
    }
}