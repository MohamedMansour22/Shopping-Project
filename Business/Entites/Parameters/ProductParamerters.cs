using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Business.Entites.Results
{
    public class ProductParamerters
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [StringLength(300, MinimumLength = 20)]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        public string Sub_Category { get; set; }
        [JsonIgnore]
        public DateTime DateAdded { get; set; }

    }
}
