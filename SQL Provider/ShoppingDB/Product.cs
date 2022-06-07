using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_Provider.ShoppingDB
{
    public class Product
    {
        public  Guid ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Sub_Category { get; set; }
        public DateTime DateAdded { get; set; }
        public Guid SellerID { get; set; }

        [ForeignKey("SellerID")]
        public User User { get; set; }

    }
}
