namespace ComputerECommerce.Models
{
    public class ShoppingCart
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}