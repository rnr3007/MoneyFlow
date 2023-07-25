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
using fu = MoneyFlow.Utils.FileUtilites;
using System.Security;
using System.IO;

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

        public async Task<TableViewModel<DataViewModel<Product, byte[]>>> GetProductsAsync(string keyword, int page, int limit)
        {
            keyword = keyword ?? "";
            int totalProduct = _dbContext.Products.Where(x =>(
                x.Name.Contains(keyword)
            )).Count();

            PaginationViewModel paginationViewModel = new PaginationViewModel(
                page,
                limit,
                totalProduct,
                keyword,
                $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/product"
            );

            if (totalProduct == 0) 
            {
                return new TableViewModel<DataViewModel<Product, byte[]>>(
                    new List<DataViewModel<Product, byte[]>>(),
                    paginationViewModel
                );
            }

            List<Product> products = _dbContext.Products
                .Where(x => x.Name.Contains(keyword))
                .Skip(paginationViewModel.LimitData * (paginationViewModel.ChoosenPage - 1))
                .Take(paginationViewModel.LimitData)
                .ToList();
            
            List<DataViewModel<Product, byte[]>> resultProducts = new List<DataViewModel<Product, byte[]>>();
            foreach (Product product in products)
            {
                resultProducts.Add(
                    new DataViewModel<Product, byte[]>(
                        product,
                        await fu.GetFileByte(product.ImageUrl)
                    )
                );
            }

            return new TableViewModel<DataViewModel<Product, byte[]>>(
                resultProducts,
                paginationViewModel
            );
        }

        public async Task CreateProduct(Product product, IFormFile formFile)
        {   
            if (formFile != null)
            {
                if (HttpContext.Items.TryGetValue("id", out var ownerId))
                {
                    product.ImageUrl = await fu.SaveFile("product", formFile, (string)ownerId);
                } else 
                {
                    throw new SecurityException(ErrorMessage.USER_NOT_FOUND);
                }
            }
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

        public async Task UpdateProduct(string productId, Product product, IFormFile formFile)
        {
            var productData = _dbContext.Products.FirstOrDefault(x => x.Id == productId) 
                ?? throw new DataException(ErrorMessage.PRODUCT_NOT_FOUND);

            productData.Name = product.Name;
            productData.Price = product.Price;
            if (formFile == null)
            {
                productData.ImageUrl = "";
            } else
            {
                if (HttpContext.Items.TryGetValue("id", out var ownerId))
                {
                    productData.ImageUrl = await fu.UpdateFile("product", formFile, productData.ImageUrl, (string)ownerId);
                } else 
                {
                    throw new SecurityException(ErrorMessage.USER_NOT_FOUND);
                }
            }
            productData.ProductType = product.ProductType;
            productData.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
        }
    }
}