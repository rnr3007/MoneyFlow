using System;
using System.Data;
using System.Diagnostics;
using System.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneyFlow.Models;
using MoneyFlow.Models.ViewModels;
using MoneyFlow.Services;
using iv = MoneyFlow.Utils.Validator.InputValidator;
using fu = MoneyFlow.Utils.FileUtilites;

namespace MoneyFlow.Controllers
{
    [Route("/product")]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        private readonly ILogger<ProductController> _logger;

        private readonly static string baseUrl = Startup.StaticConfiguration.GetSection("BASE_URL").Value;

        public ProductController(ILogger<ProductController> logger, ProductService productService)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Products(string keyword, string page, string limit)
        {
            try
            {
                ViewData["Title"] = "Produk";

                var tableView = await _productService.GetProductsAsync(keyword, 
                    iv.GetValidIntegerFromString(page, 1), 
                    iv.GetValidIntegerFromString(limit, 10)
                );

                ViewData["keyword"] = tableView.PaginationView.SearchKeyword;
                ViewData["page"] = tableView.PaginationView.ChoosenPage;
                ViewData["limit"] = tableView.PaginationView.LimitData;

                return View(tableView);
            } catch (Exception)
            {
                return View();
            }
        }

        [HttpGet("create")]
        public IActionResult CreateProduct()
        {
            ViewData["Title"] = "Tambah Produk";

            CreateDataFormViewModel<Product> createProductView = new CreateDataFormViewModel<Product>(
                new Product()
            );

            return View(createProductView);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct(CreateDataFormViewModel<Product> createDataView, IFormFile formFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _productService.CreateProduct(createDataView.Data, formFile);
                    return Redirect($"{baseUrl}/product");
                }
                return View(createDataView);
            }
            catch (Exception e)
            {
                Type exceptionType = e.GetType();
                if (exceptionType == typeof(SecurityException))
                {
                    Response.Cookies.Delete("TokenBearer");
                    return Redirect($"{baseUrl}/user/login");
                }
                Console.WriteLine(e);
                return View(createDataView);
            }
        }

        [HttpGet("delete/{productId}")]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            try 
            {
                await _productService.DeleteProduct(productId);
                return Redirect($"{baseUrl}/product");
            } catch (Exception e)
            {   
                Type exceptionType = e.GetType();
                if (exceptionType != typeof(DataException))
                {
                    Console.WriteLine(e);
                }
                return Redirect($"{baseUrl}/product");
            }
        }

        [HttpGet("update/{productId}")]
        public async Task<IActionResult> UpdateProduct(string productId)
        {
            try
            {
                ViewData["Title"] = "Ubah Produk";
                Product product = _productService.GetProduct(productId);
                UploadFileFormViewModel fileFormView = new UploadFileFormViewModel();
                fileFormView.FileUrl = product.ImageUrl;
                fileFormView.FileData = await fu.GetFileByte(product.ImageUrl);
                return View(new CreateDataFormViewModel<Product>(
                    product,
                    fileFormView
                ));
            }
            catch (Exception)
            {
                return Redirect($"{baseUrl}/product");
            }
        }

        [HttpPost("update/{productId}")]
        public async Task<IActionResult> UpdateProduct(string productId, CreateDataFormViewModel<Product> createDataView, IFormFile formFile)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    await _productService.UpdateProduct(productId, createDataView.Data, formFile);
                    return Redirect($"{baseUrl}/product");
                }
                UploadFileFormViewModel fileFormView = new UploadFileFormViewModel();
                fileFormView.FileUrl = createDataView.Data.ImageUrl;
                return View(new CreateDataFormViewModel<Product>(
                    createDataView.Data,
                    fileFormView
                ));
            } catch (Exception e)
            {
                Type exceptionType = e.GetType();
                if (exceptionType != typeof(DataException) || exceptionType != typeof(SecurityException))
                {
                    Console.WriteLine(e);
                }
                if (exceptionType == typeof(SecurityException))
                {
                    Response.Cookies.Delete("TokenBearer");
                    return Redirect($"{baseUrl}/user/login");
                }
                UploadFileFormViewModel fileFormView = new UploadFileFormViewModel();
                fileFormView.FileUrl = createDataView.Data.ImageUrl;
                return View(new CreateDataFormViewModel<Product>(
                    createDataView.Data,
                    fileFormView
                ));
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("/test")]
        public async Task<IActionResult> Test()
        {
            var test = await fu.GetFileByte("UserData/0669c07e-a061-4be4-b483-d447c9831829/product/0be2d2a7-bafe-478d-b8ef-406a23477bf2.png");
            return File(test, "image/jpeg");
        }
    }
}
