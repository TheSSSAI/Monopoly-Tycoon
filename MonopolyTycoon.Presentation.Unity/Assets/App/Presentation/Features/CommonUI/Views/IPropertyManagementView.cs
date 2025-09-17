using System;
using System.Collections.Generic;
using MonopolyTycoon.Presentation.Core;

namespace MonopolyTycoon.Presentation.Features.CommonUI.Views
{
    /// <summary>
    /// Defines the contract for the property management view, which allows players
    /// to manage their assets.
    /// Fulfills requirement REQ-1-074 and supports User Story US-052.
    /// </summary>
    public interface IPropertyManagementView : IView
    {
        /// <summary>
        /// Event fired when the user requests to build a house on a property.
        /// The payload is the ID of the property.
        /// </summary>
        event Action<int> OnBuildHouseClicked;

        /// <summary>
        /// Event fired when the user requests to sell a house from a property.
        /// The payload is the ID of the property.
        /// </summary>
        event Action<int> OnSellHouseClicked;

        /// <summary>
        /// Event fired when the user requests to mortgage a property.
        /// The payload is the ID of the property.
        /// </summary>
        event Action<int> OnMortgageClicked;

        /// <summary>
        /// Event fired when the user requests to unmortgage a property.
        /// The payload is the ID of the property.
        /// </summary>
        event Action<int> OnUnmortgageClicked;
        
        /// <summary>
        /// Event fired when the user requests to initiate a trade.
        /// </summary>
        event Action OnInitiateTradeClicked;

        /// <summary>
        /// Event fired when the user closes the property management view.
        /// </summary>
        event Action OnCloseViewClicked;
        
        /// <summary>
        /// Populates the view with the player's current assets and cash.
        /// </summary>
        /// <param name="viewModel">The data required to render the view.</param>
        void SetData(PropertyManagementViewModel viewModel);
    }

    /// <summary>
    /// ViewModel containing all necessary data to display the property management screen.
    /// </summary>
    public class PropertyManagementViewModel
    {
        public string PlayerCashFormatted { get; set; }
        public List<PropertyGroupViewModel> PropertyGroups { get; set; }
    }

    /// <summary>
    /// Represents a color group of properties.
    /// </summary>
    public class PropertyGroupViewModel
    {
        public string GroupName { get; set; }
        public List<PropertyViewModel> Properties { get; set; }
    }

    /// <summary>
    /// Represents a single property to be displayed in the UI.
    /// </summary>
    public class PropertyViewModel
    {
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public int HouseCount { get; set; }
        public bool IsMortgaged { get; set; }
        public bool CanBuildHouse { get; set; }
        public bool CanSellHouse { get; set; }
        public bool CanMortgage { get; set; }
        public bool CanUnmortgage { get; set; }
        public string BuildCostFormatted { get; set; }
        public string MortgageValueFormatted { get; set; }
    }
}