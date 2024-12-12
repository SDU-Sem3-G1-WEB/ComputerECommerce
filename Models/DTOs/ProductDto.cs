using System.ComponentModel.DataAnnotations;

namespace ComputerECommerce.Models
{
    public class ProductDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Manufacturer { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        public IFormFile? Image { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public void Clear()
        {
            Name = string.Empty;
            Manufacturer = string.Empty;
            Description = string.Empty;
            Price = 0;
            Image = null;
            Quantity = 0;
            CategoryId = 0;
        }
    }
}