using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoneyFlow.Context;
using MoneyFlow.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Data;
using MoneyFlow.Constants;

namespace MoneyFlow.Services
{
    public class ProductService
    {
        private readonly UserContext _dbContext;

        private readonly IHttpContextAccessor _httpContext;

        private HttpContext HttpContext => _httpContext.HttpContext;

        public ProductService(UserContext dbContext, IHttpContextAccessor httpContext)
        {
            _dbContext = dbContext;
            _httpContext = httpContext;
        }

        public List<Product> GetProducts()
        {
            return _dbContext.Products.ToList();;
        }

        public async Task CreateProduct(Product product)
        {
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProduct(string productId)
        {
            var productData = _dbContext.Products.FirstOrDefault(x => x.Id == productId) 
                ?? throw new DataException(ErrorMessage.PRODUCT_NOT_FOUND);
            _dbContext.Products.Remove(productData);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProduct(string productId, Product product)
        {
            var productData = _dbContext.Products.FirstOrDefault(x => x.Id == productId) 
                ?? throw new DataException(ErrorMessage.PRODUCT_NOT_FOUND);
            productData.Name = product.Name;
            productData.Price = product.Price;
            productData.ImageUrl = product.ImageUrl;
            productData.ProductType = product.ProductType;
            productData.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
        }
    }
}