using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MoneyFlow.Constants;
using fu = MoneyFlow.Utils.FileUtilites;
using MoneyFlow.Models;
using MoneyFlow.Services;
using iv = MoneyFlow.Utils.Validator.InputValidator;
using Microsoft.AspNetCore.Authorization;

namespace MoneyFlow.Controllers
{
    [Authorize]
    [Route(UriPath.FILE)]
    public class FileController : Controller
    {
        private readonly ILogger<FileController> _logger;
        
        private readonly FileService _fileService;

        private readonly ExpenseService _expenseService;

        public FileController(ILogger<FileController> logger, FileService fileService, ExpenseService expenseService)
        {
            _expenseService = expenseService;
            _fileService = fileService;
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

                file = await fu.GetFileByte($"UserData/{User.FindFirst(MiscConstants.USER_ID_CLAIM).Value}/{purpose}/{fileName}");
                
                switch (fileType)
                {
                    case "image":
                        mimeType = "image/jpeg";
                        break;
                    case "pdf":
                        return View("PdfView", new PdfView(
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

        [HttpGet(UriPath.EXCEL_DOWNLOAD)]
        public async Task<IActionResult> DownloadExcel(string purpose)
        {
            byte[] file = new byte[0];
            try 
            {
                file = await _fileService.RetrieveExcel(
                    User.FindFirst(MiscConstants.USER_ID_CLAIM).Value,
                    "expense"
                );
                return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            } catch (Exception)
            {
                return File(file, "text/plain");
            }
        }

        [HttpPost(UriPath.INSERT_FROM_EXCEL)]
        public async Task<IActionResult> InsertFromExcel(IFormFile formFile, string purpose)
        {
            try
            {
                if (formFile == null || formFile.Length == 0 || !iv.IsFileValid(formFile, new string[]{"excel"}))
                {
                    throw new Exception(ErrorMessage.FIELD_EMPTY);
                }

                if (purpose == GeneralConstants.PURPOSE_EXPENSE)
                {
                    await _expenseService.InsertFromExcel(
                        User.FindFirst(MiscConstants.USER_ID_CLAIM).Value,
                        formFile
                    );
                    return Redirect(UriPath.EXPENSE_LIST);
                }
                return Redirect(UriPath.NOT_FOUND);
            } catch (Exception e)
            {
                Console.WriteLine(e);
                Type eType = e.GetType();
                return Redirect(UriPath.ERROR);
            }
            
        }
    }
}