using System.ComponentModel.DataAnnotations;

namespace ComputerECommerce.Models
{
    public class TicketDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Topic { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public void Clear()
        {
            Name = "";
            Topic = "";
            Email = "";
            Description = "";
            CategoryId = 0;
        }
    }
}