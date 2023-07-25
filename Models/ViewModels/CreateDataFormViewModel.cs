namespace MoneyFlow.Models.ViewModels
{
    public class CreateDataFormViewModel<T>
    {
        public T Data { get; set; }

        public UploadFileFormViewModel UploadFileForm { get; set; }

        public CreateDataFormViewModel()
        {
            Data = default;
            UploadFileForm = new UploadFileFormViewModel();
        }

        public CreateDataFormViewModel(T data, UploadFileFormViewModel uploadFileForm = default)
        {
            Data = data;
            UploadFileForm = uploadFileForm ?? new UploadFileFormViewModel();
        }
    }
}