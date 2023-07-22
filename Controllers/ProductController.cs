using System;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneyFlow.Models;
using MoneyFlow.Models.ViewModels;
using MoneyFlow.Services;
using iv = MoneyFlow.Utils.Validator.InputValidator;

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
        public IActionResult Products(string keyword, string page, string limit)
        {
            try
            {
                ViewData["Title"] = "Produk";
                ViewData["searchKeyword"] = keyword ?? "";

                var productsObject = _productService.GetProducts(keyword, 
                    iv.GetValidIntegerFromString(page, 1), 
                    iv.GetValidIntegerFromString(limit, 10)
                );

                ViewData["page"] = productsObject.PaginationView.ChoosenPage;
                ViewData["limit"] = productsObject.PaginationView.LimitData;
                return View(productsObject);
            } catch (Exception)
            {
                return View();
            }
        }

        [HttpGet("create")]
        public IActionResult CreateProduct()
        {
            ViewData["Title"] = "Tambah Produk";
            return View();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _productService.CreateProduct(product);
                    return Redirect($"{baseUrl}/product");
                }
                return View(product);
            }
            catch (Exception)
            {
                return View(product);
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
        public IActionResult UpdateProduct(string productId)
        {
            try
            {
                ViewData["Title"] = "Ubah Produk";
                Product product = _productService.GetProduct(productId);
                return View(product);
            }
            catch (Exception)
            {
                return Redirect($"{baseUrl}/product");
            }
        }

        [HttpPost("update/{productId}")]
        public async Task<IActionResult> UpdateProduct(string productId, Product product)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    await _productService.UpdateProduct(productId, product);
                    return Redirect($"{baseUrl}/product");
                }
                return View(product);
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
