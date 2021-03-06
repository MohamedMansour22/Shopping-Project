using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entites.Results
{
    public class ProductResult
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Sub_Category { get; set; }
        public DateTime DateAdded { get; set; }
        public string SellerName { get; set; }

    }
}
