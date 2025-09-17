using System;
using System.Threading.Tasks;
using MonopolyTycoon.Application.Abstractions;
using MonopolyTycoon.Presentation.Core;
using MonopolyTycoon.Presentation.Features.MainMenu.Views;
using VContainer;

namespace MonopolyTycoon.Presentation.Features.MainMenu.Presenters
{
    public class LoadGamePresenter : IDisposable
    {
        private readonly ILoadGameView view;
        private readonly IViewManager viewManager;
        private readonly IGameSessionService gameSessionService;

        private int selectedSlot = -1;

        [Inject]
        public LoadGamePresenter(ILoadGameView view, IViewManager viewManager, IGameSessionService gameSessionService)
        {
            this.view = view;
            this.viewManager = viewManager;
            this.gameSessionService = gameSessionService;
        }

        public async void Initialize()
        {
            view.OnLoadClicked += HandleLoadClicked;
            view.OnBackClicked += HandleBackClicked;
            view.OnSlotSelected += HandleSlotSelected;

            await PopulateSaveSlots();
        }

        private async Task PopulateSaveSlots()
        {
            try
            {
                view.SetLoadingState(true);
                var saveFilesMetadata = await gameSessionService.GetSaveGameMetadataAsync();

                view.ClearSlots();
                foreach (var metadata in saveFilesMetadata)
                {
                    // Fulfills REQ-1-088: Check the integrity and version compatibility
                    // The Application service handles the logic and provides a simple status.
                    // The Presenter just translates this status to the View.
                    view.AddSaveSlot(
                        metadata.SlotNumber,
                        metadata.Status.ToString(), // "Valid", "Corrupted", "Incompatible"
                        metadata.SaveTimestamp.ToString("yyyy-MM-dd HH:mm:ss"),
                        $"Turn: {metadata.TurnNumber}",
                        metadata.Status == SaveGameStatus.Valid);
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error populating save slots: {e.Message}");
                await viewManager.ShowErrorDialogAsync("Error Loading Saves", "Could not retrieve save game information.");
            }
            finally
            {
                view.SetLoadingState(false);
                view.SetLoadButtonInteractable(false);
            }
        }
        
        private void HandleSlotSelected(int slotNumber)
        {
            selectedSlot = slotNumber;
            view.SetLoadButtonInteractable(selectedSlot != -1);
        }

        private async void HandleLoadClicked()
        {
            if (selectedSlot < 0) return;

            try
            {
                await viewManager.ShowLoadingScreenAsync($"Loading game from slot {selectedSlot + 1}...");
                await gameSessionService.LoadGameAsync(selectedSlot);
                await viewManager.LoadSceneAsync("GameBoardScene");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load game from slot {selectedSlot}: {ex.Message}");
                await viewManager.ShowErrorDialogAsync("Load Failed", $"Could not load game from slot {selectedSlot + 1}. The file may be corrupt or incompatible.");
            }
            finally
            {
                await viewManager.HideLoadingScreenAsync();
            }
        }

        private void HandleBackClicked()
        {
            viewManager.CloseView(this);
        }

        public void Dispose()
        {
            view.OnLoadClicked -= HandleLoadClicked;
            view.OnBackClicked -= HandleBackClicked;
            view.OnSlotSelected -= HandleSlotSelected;
        }
    }
}