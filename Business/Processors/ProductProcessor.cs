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
            var Users = context.Users;
            
            List<ProductResult> results = new List<ProductResult>();
            foreach (var product in products)
            {
                ProductResult productResult = new ProductResult();
                productResult.ID = product.ID;
                productResult.Name = product.Name;
                productResult.Brand = product.Brand;
                productResult.Price = product.Price;
                productResult.Quantity = product.Quantity;
                productResult.Description = product.Description;
                productResult.Category = product.Category;
                productResult.Sub_Category = product.Sub_Category;
                productResult.DateAdded = product.DateAdded;
                productResult.SellerName = Users.Where(us => us.ID == product.SellerID).FirstOrDefault().UserName;
                results.Add(productResult);
            }
            return results;
        }
        
        public bool PostProducts(ProductParamerters productParamerters, string currentUsername)
        {
            ShoppingContext context = new ShoppingContext(_configuration);
            var currentUserID = context.Users.Where(us => us.UserName == currentUsername).FirstOrDefault().ID;
            Product product = new Product();

            product.ID = Guid.NewGuid();
            product.Name = productParamerters.Name;
            product.Quantity = productParamerters.Quantity;
            product.Price = productParamerters.Price;
            product.Brand = productParamerters.Brand;
            product.Description = productParamerters.Description;
            product.Category = productParamerters.Category;
            product.Sub_Category = productParamerters.Sub_Category;
            product.DateAdded = DateTime.Now;
            product.SellerID = currentUserID;

            context.Products.Add(product);
            context.SaveChanges();

            return true;
        }
    }
}
