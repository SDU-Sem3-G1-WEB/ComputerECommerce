using DataAccess;
using ComputerECommerce.Models;

namespace Services
{
    public class ShoppingCartService
    {
        private readonly ShoppingCartRepository shoppingCartRepository;

        public ShoppingCartService(ShoppingCartRepository shoppingCartRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public void AddShoppingCart(int userId)
        {
            shoppingCartRepository.InsertShoppingCart(userId);
        }

        public void AddShoppingCartItem(int shoppingCartId, int productId, int quantity)
        {
            shoppingCartRepository.InsertShoppingCartItem(shoppingCartId, productId, quantity);
        }

        public void UpdateShoppingCartItemQuantity(int shoppingCartItemId, int quantity)
        {
            shoppingCartRepository.EditShoppingCartItemQuantity(shoppingCartItemId, quantity);
        }

        public void RemoveShoppingCart(int shoppingCartId)
        {
            shoppingCartRepository.DeleteShoppingCart(shoppingCartId);
        }

        public void RemoveShoppingCartItem(int shoppingCartItemId)
        {
            shoppingCartRepository.DeleteShoppingCartItem(shoppingCartItemId);
        }

        public ShoppingCart GetShoppingCart(int userId)
        {
            return shoppingCartRepository.GetShoppingCart(userId);
        }

        public List<ShoppingCartItem> GetShoppingCartItems(int shoppingCartId)
        {
            return shoppingCartRepository.GetShoppingCartItems(shoppingCartId);
        }
    }
}