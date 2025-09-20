using MonopolyTycoon.Application.Abstractions;
using MonopolyTycoon.Application.DataObjects;
using MonopolyTycoon.Presentation.Features.MainMenu.Views;
using System;
using System.Linq;
using VContainer.Unity;

namespace MonopolyTycoon.Presentation.Features.MainMenu.Presenters
{
    public class LoadGamePresenter : IInitializable, IDisposable
    {
        private readonly ILoadGameView _view;
        private readonly IGameSessionService _gameSessionService;
        private readonly IViewManager _viewManager;

        public LoadGamePresenter(ILoadGameView view, IGameSessionService gameSessionService, IViewManager viewManager)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _gameSessionService = gameSessionService ?? throw new ArgumentNullException(nameof(gameSessionService));
            _viewManager = viewManager ?? throw new ArgumentNullException(nameof(viewManager));
        }

        public void Initialize()
        {
            _view.OnLoadGameClicked += HandleLoadGameClicked;
            _view.OnBackClicked += HandleBackClicked;
            PopulateSaveSlots();
        }

        public void Dispose()
        {
            _view.OnLoadGameClicked -= HandleLoadGameClicked;
            _view.OnBackClicked -= HandleBackClicked;
        }

        private async void PopulateSaveSlots()
        {
            _view.ShowLoading(true);
            try
            {
                var metadataList = await _gameSessionService.GetAllSaveGameMetadataAsync();

                // US-062 AC-003: All save slots are displayed as 'Empty'
                var viewModels = Enumerable.Range(1, 5).Select(slotNumber =>
                {
                    var data = metadataList.FirstOrDefault(m => m.SlotNumber == slotNumber);
                    if (data == null)
                    {
                        return new ViewModels.SaveGameMetadata
                        {
                            SlotNumber = slotNumber,
                            Status = ViewModels.SaveStatus.Empty
                        };
                    }
                    
                    // US-063 & REQ-1-088: Handle corrupted and incompatible files
                    return new ViewModels.SaveGameMetadata
                    {
                        SlotNumber = data.SlotNumber,
                        SaveTimestamp = data.SaveTimestamp.ToString("g"), // General short date/time
                        PlayerName = data.PlayerName,
                        TurnNumber = data.TurnNumber,
                        Status = data.Status switch
                        {
                            SaveFileStatus.Valid => ViewModels.SaveStatus.Valid,
                            SaveFileStatus.Corrupted => ViewModels.SaveStatus.Corrupted,
                            SaveFileStatus.IncompatibleVersion => ViewModels.SaveStatus.Incompatible,
                            _ => ViewModels.SaveStatus.Empty
                        }
                    };
                }).ToList();

                _view.DisplaySaveSlots(viewModels);
            }
            catch (Exception ex)
            {
                // In a real app, inject ILogger and log this exception
                Debug.LogError($"Failed to populate save slots: {ex.Message}");
                _view.ShowError("Could not retrieve save games. Please check the log for details.");
            }
            finally
            {
                _view.ShowLoading(false);
            }
        }

        private async void HandleLoadGameClicked(int slotNumber)
        {
            _view.ShowLoading(true);
            try
            {
                var success = await _gameSessionService.LoadGameAsync(slotNumber);
                if (success)
                {
                    // US-062: ...the game transitions to the main gameplay scene...
                    await _viewManager.ShowScreen(Screen.GameBoard);
                }
                else
                {
                    _view.ShowError($"Failed to load game from slot {slotNumber}.");
                    PopulateSaveSlots(); // Refresh the view in case status changed
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error loading game from slot {slotNumber}: {ex.Message}");
                _view.ShowError("An unexpected error occurred while loading the game.");
            }
            finally
            {
                // This might not be reached if scene transition is immediate
                _view.ShowLoading(false);
            }
        }

        private void HandleBackClicked()
        {
            _viewManager.ShowScreen(Screen.MainMenu);
        }
    }
}