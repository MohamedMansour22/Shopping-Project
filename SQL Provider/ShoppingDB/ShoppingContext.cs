using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SQL_Provider.ShoppingDB
{
    public class ShoppingContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        private readonly IConfiguration configuration;

        public ShoppingContext(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
                var conn = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(conn);
            }
        }
    }
}
