using Microsoft.AspNetCore.Http;

namespace MoneyFlow.Models.ViewModels
{
    public class UploadFileFormViewModel
    {
        public IFormFile FormFile { get; set; }

        public byte[] FileData { get; set; }

        public string FileUrl { get; set; }
    }
}