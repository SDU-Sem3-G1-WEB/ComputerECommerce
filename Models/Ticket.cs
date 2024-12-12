namespace ComputerECommerce.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Topic { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public DateTime TicketDate { get; set; }
        public int CategoryId { get; set; }
    }
}