using Business.Entites.Results;
using Business.Processor;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet(Name = "GetProducts")]
        public IEnumerable<ProductResult> Get()
        {
            productProcessor = new ProductProcessor(_configuration);
            var products = productProcessor.GetProducts();

            return products;
        }
    }
}