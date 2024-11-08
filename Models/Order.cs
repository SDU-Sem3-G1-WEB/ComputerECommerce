namespace ComputerECommerce.Models
{
    public class Order
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}