using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ComputerECommerce.Models
{
    public class ProductDto
    {
        [Required]
        public string Name { get; set; } ="";
        public string Description { get; set; } ="";
        [Required]
        public decimal Price { get; set; }
        public IFormFile? ImageFile { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string CategoryId { get; set; } = String.Empty;

        public void Clear()
        {
            Name = "";
            Description = "";
            Price = 0;
            ImageFile = null;
            Quantity = 0;
            CategoryId = String.Empty;
        }
    }
}