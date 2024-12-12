using ComputerECommerce.Models;

namespace DataAccess
{
    public class ShoppingCartRepository
    {
        private readonly DbAccess dbAccess;

        public ShoppingCartRepository(DbAccess dbAccess)
        {
            this.dbAccess = dbAccess;
        }

        #region Insert Methods

        public void InsertShoppingCart(int userId)
        {
            var sql = "INSERT INTO shopping_carts (U_ID) VALUES (@userId)";
            dbAccess.ExecuteNonQuery(sql, ("@userId", userId));
        }

        public void InsertShoppingCartItem(int shoppingCartId, int productId, int quantity)
        {
            var sql = "INSERT INTO shopping_cart_items (SC_ID, P_ID, SCI_QUANTITY) VALUES (@shoppingCartId, @productId, @quantity)";
            dbAccess.ExecuteNonQuery(sql, ("@shoppingCartId", shoppingCartId), ("@productId", productId), ("@quantity", quantity));
        }

        #endregion

        #region Edit Methods

        public void EditShoppingCartItemQuantity(int shoppingCartItemId, int quantity)
        {
            var sql = "UPDATE shopping_cart_items SET SCI_QUANTITY = @quantity WHERE SCI_ID = @shoppingCartItemId";
            dbAccess.ExecuteNonQuery(sql, ("@quantity", quantity), ("@shoppingCartItemId", shoppingCartItemId));
        }

        #endregion

        #region Delete Methods

        public void DeleteShoppingCart(int shoppingCartId)
        {
            DeleteAllShoppingCartItems(shoppingCartId);

            var sql = "DELETE FROM shopping_carts WHERE SC_ID = @shoppingCartId";
            dbAccess.ExecuteNonQuery(sql, ("@shoppingCartId", shoppingCartId));
        }

        public void DeleteShoppingCartItem(int shoppingCartItemId)
        {
            var sql = "DELETE FROM shopping_cart_items WHERE SCI_ID = @shoppingCartItemId";
            dbAccess.ExecuteNonQuery(sql, ("@shoppingCartItemId", shoppingCartItemId));
        }

        public void DeleteAllShoppingCartItems(int shoppingCartId)
        {
            var sql = "DELETE FROM shopping_cart_items WHERE SC_ID = @shoppingCartId";
            dbAccess.ExecuteNonQuery(sql, ("@shoppingCartId", shoppingCartId));
        }

        #endregion

        #region Get Methods

    public ShoppingCart GetShoppingCart(int userId)
    {
        var sql = "SELECT SC_ID, U_ID FROM shopping_carts WHERE U_ID = @userId";

        ShoppingCart shoppingCart = new ShoppingCart();

        using (var cmd = dbAccess.dbDataSource.CreateCommand(sql))
        {
            cmd.Parameters.AddWithValue("@userId", userId);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    shoppingCart = new ShoppingCart
                    {
                        Id = reader.GetInt32(0),
                        UserId = reader.GetInt32(1)
                    };
                }
            }
        }
        return shoppingCart;
    }

        public List<ShoppingCartItem> GetShoppingCartItems(int shoppingCartId)
        {
            var sql = "SELECT SCI_ID, SC_ID, P_ID, SCI_QUANTITY FROM shopping_cart_items WHERE SC_ID = @shoppingCartId";

            List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>();

            using (var cmd = dbAccess.dbDataSource.CreateCommand(sql))
            {
                cmd.Parameters.AddWithValue("@shoppingCartId", shoppingCartId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        shoppingCartItems.Add(new ShoppingCartItem
                        {
                            Id = reader.GetInt32(0),
                            ShoppingCartId = reader.GetInt32(1),
                            ProductId = reader.GetInt32(2),
                            Quantity = reader.GetInt32(3)
                        });
                    }
                }
            }
            return shoppingCartItems;
        }

        #endregion
    }
}