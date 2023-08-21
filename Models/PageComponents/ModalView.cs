namespace MoneyFlow.Models
{
    public class ModalView<T>
    {
        public string ModalId { get; set; }

        public string ActionUrl { get; set; }

        public string ConfirmationMessage { get; set; }

        public T Data { get; set; }

        public string SubmitId { get; set; }

        public ModalView(string modalId, string actionUrl = "", string confirmMessage = "", T data = default)
        {
            ModalId = modalId;
            ActionUrl = actionUrl;
            ConfirmationMessage = confirmMessage;
            Data = data;
        }
    }
}