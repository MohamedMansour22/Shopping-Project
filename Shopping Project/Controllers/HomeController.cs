using Business.Entites.Results;
using Business.Processor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQL_Provider.Enums;

namespace Shopping_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private ProductProcessor productProcessor;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("GetProducts") , Authorize(Roles = Roles.Buyer)]
        public IEnumerable<ProductResult> GetProducts()
        {
            productProcessor = new ProductProcessor(_configuration);
            var products = productProcessor.GetProducts();

            return products;
        }

        [HttpPost("PostProducts") , Authorize(Roles = Roles.Seller)]
        public async Task<ActionResult<string>> PostProducts(ProductParamerters productParamerters)
        {
            string currentUsername = User.Identity.Name;

            productProcessor = new ProductProcessor(_configuration);
            var products = productProcessor.PostProducts(productParamerters, currentUsername);

            return Ok();
        }
    }
}