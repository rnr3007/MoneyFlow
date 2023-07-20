using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoneyFlow.Context;
using MoneyFlow.Models;
using MoneyFlow.Services;

namespace MoneyFlow.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        private readonly UserContext _context;

        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, ProductService productService)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("/product")]
        public IActionResult ProductsAsync()
        {
            var products = _productService.GetProducts();
            if (products == null)
            {
                return View();
            }
            return View(products);
        }

        [HttpGet("/product/create")]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
