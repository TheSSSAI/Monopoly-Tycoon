using MonopolyTycoon.Application.Contracts;
using MonopolyTycoon.Presentation.Features.MainMenu.Presenters;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace MonopolyTycoon.Presentation.Features.MainMenu.Views
{
    public class LoadGameView : MonoBehaviour, ILoadGameView
    {
        [Header("UI References")]
        [SerializeField] private Transform _slotsContainer;
        [SerializeField] private GameObject _saveSlotPrefab;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private GameObject _noSavesMessage;

        private List<SaveSlotView> _instantiatedSlots = new();
        private int _selectedSlot = -1;
        
        public event Action<int> OnLoadGameRequested;
        public event Action OnBackRequested;

        [Inject]
        private void Construct(LoadGamePresenter presenter)
        {
            presenter.SetView(this);
        }

        private void Awake()
        {
            _loadButton.onClick.AddListener(HandleLoadClick);
            _backButton.onClick.AddListener(() => OnBackRequested?.Invoke());
            _loadButton.interactable = false;
        }

        public void DisplaySaveSlots(IEnumerable<SaveGameMetadata> saveSlots)
        {
            ClearSlots();
            bool hasSaves = false;
            
            foreach (var slotData in saveSlots)
            {
                var slotInstance = Instantiate(_saveSlotPrefab, _slotsContainer);
                var saveSlotView = slotInstance.GetComponent<SaveSlotView>();
                if (saveSlotView != null)
                {
                    saveSlotView.Initialize(slotData, HandleSlotSelected);
                    _instantiatedSlots.Add(saveSlotView);
                    if (slotData.Status != SaveStatus.Empty)
                    {
                        hasSaves = true;
                    }
                }
            }

            if (_noSavesMessage != null)
            {
                _noSavesMessage.SetActive(!hasSaves);
            }
        }

        private void HandleSlotSelected(int slotNumber, SaveStatus status)
        {
            _selectedSlot = slotNumber;
            _loadButton.interactable = status == SaveStatus.Valid;

            foreach (var slot in _instantiatedSlots)
            {
                slot.SetSelected(slot.SlotNumber == slotNumber);
            }
        }
        
        private void HandleLoadClick()
        {
            if (_selectedSlot != -1)
            {
                OnLoadGameRequested?.Invoke(_selectedSlot);
            }
        }
        
        private void ClearSlots()
        {
            foreach (Transform child in _slotsContainer)
            {
                Destroy(child.gameObject);
            }
            _instantiatedSlots.Clear();
            _selectedSlot = -1;
            _loadButton.interactable = false;
        }

        private void OnDestroy()
        {
            _loadButton.onClick.RemoveAllListeners();
            _backButton.onClick.RemoveAllListeners();
        }
    }
}