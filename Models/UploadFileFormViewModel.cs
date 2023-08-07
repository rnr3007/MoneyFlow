using Microsoft.AspNetCore.Http;

namespace MoneyFlow.Models
{
    public class UploadFileFormViewModel
    {
        public IFormFile FormFile { get; set; }

        public string FileUrl { get; set; }
    }
}