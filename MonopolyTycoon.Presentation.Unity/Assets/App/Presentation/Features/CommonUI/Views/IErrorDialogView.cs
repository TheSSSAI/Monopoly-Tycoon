using System;
using MonopolyTycoon.Presentation.Core;

namespace MonopolyTycoon.Presentation.Features.CommonUI.Views
{
    /// <summary>
    /// Defines the contract for the view that displays an unhandled exception to the user.
    /// Fulfills requirement REQ-1-023.
    /// </summary>
    public interface IErrorDialogView : IView
    {
        /// <summary>
        /// Event triggered when the user clicks the close button on the dialog.
        /// The presenter subscribes to this to terminate the application.
        /// </summary>
        event Action OnCloseClicked;

        /// <summary>
        /// Displays the error dialog with specific information.
        /// </summary>
        /// <param name="errorId">The unique error identifier that correlates to a log entry.</param>
        /// <param name="logPath">The absolute file path to the logs directory.</param>
        void ShowError(string errorId, string logPath);
    }
}