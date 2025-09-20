using System;
using MonopolyTycoon.Presentation.Shared.Services;

namespace MonopolyTycoon.Presentation.Shared.Views
{
    /// <summary>
    /// Contract for a generic, reusable modal dialog view.
    /// </summary>
    public interface IModalDialogView
    {
        /// <summary>
        /// Configures the dialog's content and button actions.
        /// </summary>
        /// <param name="definition">The data object defining the dialog's text and button labels.</param>
        /// <param name="onConfirm">The action to invoke when the confirm button is clicked.</param>
        /// <param name="onCancel">The action to invoke when the cancel button is clicked. Can be null.</param>
        void Configure(DialogDefinition definition, Action onConfirm, Action onCancel);
        
        /// <summary>
        /// Shows the dialog view.
        /// </summary>
        void Show();
        
        /// <summary>
        /// Hides the dialog view.
        /// </summary>
        void Hide();
    }
}