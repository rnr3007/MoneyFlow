using System;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneyFlow.Models;
using MoneyFlow.Services;

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
        public IActionResult ProductsAsync()
        {
            var products = _productService.GetProducts();
            if (products == null)
            {
                return View();
            }
            return View(products);
        }

        [HttpGet("create")]
        public IActionResult CreateProduct()
        {
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
            Console.WriteLine(productId);
            try {
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
