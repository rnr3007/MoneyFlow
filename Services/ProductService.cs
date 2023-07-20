using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoneyFlow.Context;
using MoneyFlow.Models;
using Microsoft.AspNetCore.Http;

namespace MoneyFlow.Services
{
    public class ProductService
    {
        private readonly UserContext _dbContext;

        public ProductService(UserContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetProducts()
        {
            return _dbContext.Products.ToList();;
        }
    }
}