using System.ComponentModel.DataAnnotations;

namespace ComputerECommerce.Models
{
    public class Ticket
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string TicketDate { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}