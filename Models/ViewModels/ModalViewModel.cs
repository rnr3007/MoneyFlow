using System.Collections.Generic;

namespace MoneyFlow.Models.ViewModels
{
    public class ModalViewModel<T>
    {
        public string ModalId { get; set; }

        public string ActionUrl { get; set; }

        public string ConfirmationMessage { get; set; }

        public T Data { get; set; }

        public ModalViewModel(string modalId, string actionUrl = "", string confirmMessage = "", T data = default)
        {
            ModalId = modalId;
            ActionUrl = actionUrl;
            ConfirmationMessage = confirmMessage;
            Data = data;
        }
    }
}