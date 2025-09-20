using System;
using System.Collections.Generic;

namespace MonopolyTycoon.Presentation.Features.PropertyManagement.Views
{
    /// <summary>
    /// Defines the contract for the Property Management screen's View component.
    /// </summary>
    public interface IPropertyManagementView
    {
        #region Events

        /// <summary>
        /// Fired when the user requests to build a house on a specific property.
        /// </summary>
        event Action<int> OnBuildHouseRequested;

        /// <summary>
        /// Fired when the user requests to build a hotel on a specific property.
        /// </summary>
        event Action<int> OnBuildHotelRequested;

        /// <summary>
        /// Fired when the user requests to sell a house from a specific property.
        /// </summary>
        event Action<int> OnSellHouseRequested;
        
        /// <summary>
        /// Fired when the user requests to sell a hotel from a specific property.
        /// </summary>
        event Action<int> OnSellHotelRequested;

        /// <summary>
        /// Fired when the user requests to mortgage a specific property.
        /// </summary>
        event Action<int> OnMortgageRequested;

        /// <summary>
        /// Fired when the user requests to unmortgage a specific property.
        /// </summary>
        event Action<int> OnUnmortgageRequested;

        /// <summary>
        /// Fired when the user requests to close the property management view.
        /// </summary>
        event Action OnCloseViewRequested;

        #endregion

        #region Methods

        /// <summary>
        /// Populates the UI with the player's properties, cash, and dynamically
        /// enables/disables action buttons based on the provided view model.
        /// </summary>
        /// <param name="viewModel">The data model containing all information to render the view.</param>
        void DisplayAssets(PlayerAssetViewModel viewModel);

        #endregion
    }

    /// <summary>
    /// ViewModel for the entire property management screen.
    /// </summary>
    public class PlayerAssetViewModel
    {
        public string FormattedCash { get; set; }
        public List<PropertyCardViewModel> Properties { get; set; }
    }

    /// <summary>
    /// ViewModel for a single property card displayed in the UI.
    /// Contains both display data and state for enabling/disabling actions.
    /// </summary>
    public class PropertyCardViewModel
    {
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public string ColorGroup { get; set; }
        public int HouseCount { get; set; }
        public bool HasHotel { get; set; }
        public bool IsMortgaged { get; set; }

        // Action states
        public bool CanBuildHouse { get; set; }
        public bool CanBuildHotel { get; set; }
        public bool CanSellBuilding { get; set; }
        public bool CanMortgage { get; set; }
        public bool CanUnmortgage { get; set; }
        public string ReasonForDisabledAction { get; set; }
    }
}