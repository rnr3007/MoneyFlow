using System;
using System.Data;
using System.Diagnostics;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MoneyFlow.Constants;
using fu = MoneyFlow.Utils.FileUtilites;
using MoneyFlow.Models.ViewModels;

namespace MoneyFlow.Controllers
{
    [Route(UriPath.FILE)]
    public class FileController : Controller
    {
        private readonly ILogger<FileController> _logger;

        private readonly static string baseUrl = Startup.StaticConfiguration.GetSection("BASE_URL").Value;

        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
        }

        [HttpGet(UriPath.GET_FILE)]
        public async Task<IActionResult> GetFile(string fileType, string purpose, string fileName)
        {
            string mimeType = "text/plain";
            byte[] file = new byte[0];
            try
            {
                if (string.IsNullOrWhiteSpace(fileType) || string.IsNullOrWhiteSpace(purpose) || string.IsNullOrWhiteSpace(fileName))
                {
                    throw new Exception(ErrorMessage.REQUIRED_DATA_EMPTY);
                }

                file = await fu.GetFileByte($"UserData/{Request.Headers["userId"]}/{purpose}/{fileName}");
                
                switch (fileType)
                {
                    case "image":
                        mimeType = "image/jpeg";
                        break;
                    case "pdf":
                        return View("PdfView", new PdfViewModel(
                            purpose,
                            file
                        ));
                    case "excel":
                        mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        break;
                }
                return File(file, mimeType);
            } catch (Exception)
            {
                return File(file, mimeType);
            }
        }

        [HttpGet(UriPath.PDF_VIEW)]
        public IActionResult PdfView()
        {
            return View();
        }
    }
}