using Business.Entites.Results;
using Microsoft.Extensions.Configuration;
using SQL_Provider.ShoppingDB;

namespace Business.Processor
{
    public class ProductProcessor
    {
        private readonly IConfiguration _configuration;
        public ProductProcessor(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<ProductResult> GetProducts()
        {
            ShoppingContext context = new ShoppingContext(_configuration);
            var products = context.Products.ToList();
            
            List<ProductResult> results = new List<ProductResult>();
            foreach (var product in products)
            {
                ProductResult productResult = new ProductResult();
                productResult.Id = product.ID;
                productResult.Name = product.Name;
                productResult.Quantity = product.Quantity;
                productResult.Price = product.Price;
                results.Add(productResult);
            }
            return results;
        }
    }
}
