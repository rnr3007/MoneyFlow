namespace MoneyFlow.Models
{
    public class CreateDataView<T>
    {
        public T Data { get; set; }

        public UploadFileForm UploadFileForm { get; set; }

        public CreateDataView()
        {
            Data = default;
            UploadFileForm = new UploadFileForm();
        }

        public CreateDataView(T data, UploadFileForm uploadFileForm = default)
        {
            Data = data;
            UploadFileForm = uploadFileForm ?? new UploadFileForm();
        }
    }
}