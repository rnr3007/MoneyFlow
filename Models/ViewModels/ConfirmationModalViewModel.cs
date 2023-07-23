using System.Collections.Generic;

namespace MoneyFlow.Models.ViewModels
{
    public class ConfirmationModalViewModel<T>
    {
        public string ModalType { get; set; }

        public string ModalId { get; set; }

        public string ActionUrl { get; set; }

        public string ConfirmationMessage { get; set; }

        public T Data { get; set; }

        public ConfirmationModalViewModel(string modalType, string modalId, string actionUrl, string confirmMessage, T data = default)
        {
            ModalType = modalType;
            ModalId = modalId;
            ActionUrl = actionUrl;
            ConfirmationMessage = confirmMessage;
            Data = data;
        }
    }
}