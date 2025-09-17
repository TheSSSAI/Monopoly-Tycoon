using MonopolyTycoon.Presentation.Features.GameBoard.Presenters;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace MonopolyTycoon.Presentation.Features.GameBoard.Views
{
    public class HUDView : MonoBehaviour, IHUDView
    {
        [Header("Player Panel References")]
        [SerializeField] private List<PlayerHUDPanel> _playerPanels;

        [Inject]
        private void Construct(HUDPresenter presenter)
        {
            presenter.SetView(this);
        }

        public void Initialize(int playerCount)
        {
            for (int i = 0; i < _playerPanels.Count; i++)
            {
                _playerPanels[i].gameObject.SetActive(i < playerCount);
            }
        }

        public void UpdatePlayerName(int playerIndex, string name)
        {
            if (IsValidPanelIndex(playerIndex))
            {
                _playerPanels[playerIndex].SetPlayerName(name);
            }
        }

        public void UpdatePlayerCash(int playerIndex, string formattedCash)
        {
            if (IsValidPanelIndex(playerIndex))
            {
                _playerPanels[playerIndex].SetCashAmount(formattedCash);
            }
        }

        public void UpdatePlayerToken(int playerIndex, Sprite tokenSprite)
        {
            if (IsValidPanelIndex(playerIndex))
            {
                _playerPanels[playerIndex].SetTokenIcon(tokenSprite);
            }
        }

        public void SetActivePlayer(int activePlayerIndex)
        {
            for (int i = 0; i < _playerPanels.Count; i++)
            {
                if (IsValidPanelIndex(i))
                {
                    _playerPanels[i].SetTurnIndicator(i == activePlayerIndex);
                }
            }
        }

        public void SetPlayerBankrupt(int playerIndex)
        {
            if (IsValidPanelIndex(playerIndex))
            {
                _playerPanels[playerIndex].SetBankruptState();
            }
        }

        private bool IsValidPanelIndex(int index)
        {
            return index >= 0 && index < _playerPanels.Count;
        }
    }
}