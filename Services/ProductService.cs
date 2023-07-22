using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoneyFlow.Context;
using MoneyFlow.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Data;
using MoneyFlow.Constants;
using MoneyFlow.Models.ViewModels;

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

        public Product GetProduct(string productId)
        {
            var productData = _dbContext.Products.FirstOrDefault(x => x.Id == productId)
                ?? throw new DataException(ErrorMessage.PRODUCT_NOT_FOUND);
            return productData;
        }

        public TableViewModel<Product> GetProducts(string keyword, int page, int limit)
        {
            var searchKeyword = keyword ?? "";
            int totalProduct = _dbContext.Products.Where(x =>(
                x.Name.Contains(searchKeyword)
            )).Count();
            PaginationViewModel paginationViewModel = new PaginationViewModel(
                page,
                limit,
                totalProduct,
                $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/product"
            );
            var products = _dbContext.Products
                .Where(x => x.Name.Contains(searchKeyword))
                .Skip(paginationViewModel.LimitData * (paginationViewModel.ChoosenPage - 1))
                .Take(paginationViewModel.LimitData)
                .ToList();
            return new TableViewModel<Product>(
                products,
                paginationViewModel
            );
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