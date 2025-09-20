using System;

namespace MonopolyTycoon.Presentation.Shared.Views
{
    /// <summary>
    /// Defines the contract for a reusable modal dialog view component.
    /// This is used for confirmations, error messages, and simple choices.
    /// </summary>
    public interface IModalDialogView
    {
        /// <summary>
        /// Configures and displays the modal dialog.
        /// </summary>
        /// <param name="definition">An object containing all the text and configuration for the dialog.</param>
        /// <param name="onResult">An action that is invoked with the result when the user clicks a button.</param>
        void Show(DialogDefinition definition, Action<DialogResult> onResult);
    }

    /// <summary>
    /// A data object defining the content and behavior of a modal dialog.
    /// </summary>
    public class DialogDefinition
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string ConfirmButtonText { get; set; } = "OK";
        public string CancelButtonText { get; set; }
        public string NeutralButtonText { get; set; }
    }

    /// <summary>
    /// Represents the user's choice from a modal dialog.
    /// </summary>
    public enum DialogResult
    {
        None,
        Confirm,
        Cancel,
        Neutral
    }
}