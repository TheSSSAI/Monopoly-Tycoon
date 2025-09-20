using System;
using System.Collections.Generic;

namespace MonopolyTycoon.Presentation.Features.PropertyManagement.Views
{
    // DTOs for the Property Management View
    public record PropertyCardViewModel(string PropertyId, string Name, bool IsMortgaged, int HouseCount, bool HasHotel, bool CanBuild, bool CanSell, bool CanMortgage, bool CanUnmortgage);
    public record PlayerAssetViewModel(string PlayerCash, IReadOnlyList<PropertyCardViewModel> Properties);

    /// <summary>
    /// Contract for the Property Management view.
    /// Fulfills requirements of US-052.
    /// </summary>
    public interface IPropertyManagementView
    {
        event Action<string> OnBuildHouseRequested;
        event Action<string> OnSellHouseRequested;
        event Action<string> OnMortgageRequested;
        event Action<string> OnUnmortgageRequested;
        event Action OnCloseRequested;

        /// <summary>
        /// Displays the player's assets and updates the state of all action buttons.
        /// </summary>
        void DisplayAssets(PlayerAssetViewModel viewModel);

        /// <summary>
        /// Shows an error or informational message to the user.
        /// </summary>
        void ShowMessage(string message);

        /// <summary>
        /// Shows the property management view.
        /// </summary>
        void Show();

        /// <summary>
        /// Hides the property management view.
        /// </summary>
        void Hide();
    }
}