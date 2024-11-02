namespace ComputerECommerce.Models
{
    public class ShoppingCartItem
    {
        public string Id { get; set; }
        public string ShoppingCartId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}