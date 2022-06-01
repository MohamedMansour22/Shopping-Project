using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_Provider.ShoppingDB
{
    public class ShoppingContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
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
