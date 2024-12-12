using ComputerECommerce.Models;

namespace DataAccess
{
    public class OrderRepository
    {
        private readonly DbAccess dbAccess;

        public OrderRepository(DbAccess dbAccess)
        {
            this.dbAccess = dbAccess;
        }

        #region Insert Methods

        public void InsertOrder(int userId, DateTime orderDate, decimal price, string status, string addressLine, string city, string postalCode, string country)
        {
            var sql = "INSERT INTO orders (U_ID, O_DATE, O_PRICE, O_STATUS, O_ADDRESS_LINE, O_CITY, O_POSTAL_CODE, O_COUNTRY) VALUES (@userId, @orderDate, @price, @status, @addressLine, @city, @postalCode, @country)";
            dbAccess.ExecuteNonQuery(sql, 
                ("@userId", userId), 
                ("@orderDate", orderDate), 
                ("@price", price), 
                ("@status", status), 
                ("@addressLine", addressLine), 
                ("@city", city), 
                ("@postalCode", postalCode), 
                ("@country", country));
        }
        public void InsertOrderItem(int orderId, int productId, int quantity, decimal price)
        {
            var sql = "INSERT INTO order_items (O_ID, P_ID, O_QUANTITY, O_PRICE) VALUES (@orderId, @productId, @quantity, @price)";
            dbAccess.ExecuteNonQuery(sql, 
                ("@orderId", orderId), 
                ("@productId", productId), 
                ("@quantity", quantity), 
                ("@price", price));
        }

        #endregion

        #region Edit Methods

        public void EditOrderDate(int orderId, DateTime orderDate)
        {
            var sql = "UPDATE orders SET O_DATE = @orderDate WHERE O_ID = @orderId";
            dbAccess.ExecuteNonQuery(sql, ("@orderDate", orderDate), ("@orderId", orderId));
        }
        public void EditOrderPrice(int orderId, decimal price)
        {
            var sql = "UPDATE orders SET O_PRICE = @price WHERE O_ID = @orderId";
            dbAccess.ExecuteNonQuery(sql, ("@price", price), ("@orderId", orderId));
        }
        public void EditOrderStatus(int orderId, OrderStatus status)
        {
            var sql = "UPDATE orders SET OS_ID = @status WHERE O_ID = @orderId";
            dbAccess.ExecuteNonQuery(sql, ("@status", status), ("@orderId", orderId));
        }
        public void EditOrderAddressLine(int orderId, string addressLine)
        {
            var sql = "UPDATE orders SET O_ADDRESS_LINE = @addressLine WHERE O_ID = @orderId";
            dbAccess.ExecuteNonQuery(sql, ("@addressLine", addressLine), ("@orderId", orderId));
        }
        public void EditOrderCity(int orderId, string city)
        {
            var sql = "UPDATE orders SET O_CITY = @city WHERE O_ID = @orderId";
            dbAccess.ExecuteNonQuery(sql, ("@city", city), ("@orderId", orderId));
        }
        public void EditOrderPostalCode(int orderId, string postalCode)
        {
            var sql = "UPDATE orders SET O_POSTAL_CODE = @postalCode WHERE O_ID = @orderId";
            dbAccess.ExecuteNonQuery(sql, ("@postalCode", postalCode), ("@orderId", orderId));
        }
        public void EditOrderCountry(int orderId, string country)
        {
            var sql = "UPDATE orders SET O_COUNTRY = @country WHERE O_ID = @orderId";
            dbAccess.ExecuteNonQuery(sql, ("@country", country), ("@orderId", orderId));
        }

        #endregion

        #region Delete Methods

        public void DeleteOrder(int id)
        {
            var sql = "DELETE FROM orders WHERE O_ID = @id";
            dbAccess.ExecuteNonQuery(sql, ("@id", id));
        }

        #endregion

        #region Get Methods

        public List<Order> GetUserOrders(int userId)
        {
            var sql = "SELECT O_ID, U_ID, O_DATE, O_PRICE, O_STATUS, O_ADDRESS_LINE, O_CITY, O_POSTAL_CODE, O_COUNTRY FROM orders WHERE U_ID = @userId";

            List<Order> Orders = new List<Order>();
            
            using (var cmd = dbAccess.dbDataSource.CreateCommand(sql))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order()
                        {
                            Id = reader.GetInt32(0),
                            UserId = reader.GetInt32(1),
                            OrderDate = reader.GetDateTime(2),
                            Price = reader.GetDecimal(3),
                            Status = Enum.Parse<OrderStatus>(reader.GetString(4)),
                            AddressLine = reader.GetString(5),
                            City = reader.GetString(6),
                            PostalCode = reader.GetString(7),
                            Country = reader.GetString(8)

                        };
                        Orders.Add(order);
                    }
                }
            }
            return Orders;
        }
        public List<OrderItem> GetUserOrderItems(int orderId)
        {
            var sql = "SELECT * FROM order_items WHERE O_ID = @orderId";

            List<OrderItem> OrderItems = new List<OrderItem>();
            
            using (var cmd = dbAccess.dbDataSource.CreateCommand(sql))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        OrderItem orderItem = new OrderItem()
                        {
                            Id = reader.GetInt32(0),
                            OrderId = reader.GetInt32(1),
                            ProductId = reader.GetInt32(2),
                            Quantity = reader.GetInt32(3),
                            Price = reader.GetDecimal(4)
                        };
                        OrderItems.Add(orderItem);
                    }
                }
            }
            return OrderItems;
        }

        #endregion

    }
}