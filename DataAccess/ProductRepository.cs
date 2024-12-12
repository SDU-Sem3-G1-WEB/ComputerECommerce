using ComputerECommerce.Models;

namespace DataAccess
{
    public class ProductRepository
    {
        private readonly DbAccess dbAccess;

        public ProductRepository(DbAccess dbAccess)
        {
            this.dbAccess = dbAccess;
        }

        #region Insert Methods

        public void InsertProduct(string name, string manufacturer, string description, decimal price, string image, int quantity, int categoryId)
        {
            var sql = "INSERT INTO products (P_NAME, P_MANUFACTURER, P_DESCRIPTION, P_PRICE, P_IMAGE, P_QUANTITY, C_ID) VALUES (@name, @manufacturer, @description, @price, @image, @quantity, @categoryId)";
            dbAccess.ExecuteNonQuery(sql, 
                ("@name", name), 
                ("@manufacturer", manufacturer), 
                ("@description", description), 
                ("@price", price), 
                ("@image", image), 
                ("@quantity", quantity), 
                ("@categoryId", categoryId));
        }

        #endregion

        #region Edit Methods

        public void EditProduct(int productId, string name, string manufacturer, string description, decimal price, string image, int quantity, int categoryId)
        {
            var sql = "UPDATE products SET P_NAME = @name, P_MANUFACTURER = @manufacturer, P_DESCRIPTION = @description, P_PRICE = @price, P_IMAGE = @image, P_QUANTITY = @quantity, C_ID = @categoryId WHERE P_ID = @productId";
            
            dbAccess.ExecuteNonQuery(sql, 
                ("@name", name), 
                ("@manufacturer", manufacturer), 
                ("@description", description), 
                ("@price", price), 
                ("@image", image), 
                ("@quantity", quantity), 
                ("@categoryId", categoryId), 
                ("@productId", productId));
        }

        public void EditProductName(int productId, string name)
        {
            var sql = "UPDATE products SET P_NAME = @name WHERE P_ID = @productId";
            dbAccess.ExecuteNonQuery(sql, ("@name", name), ("@productId", productId));
        }

        public void EditProductManufacturer(int productId, string manufacturer)
        {
            var sql = "UPDATE products SET P_MANUFACTURER = @manufacturer WHERE P_ID = @productId";
            dbAccess.ExecuteNonQuery(sql, ("@manufacturer", manufacturer), ("@productId", productId));
        }

        public void EditProductDescription(int productId, string description)
        {
            var sql = "UPDATE products SET P_DESCRIPTION = @description WHERE P_ID = @productId";
            dbAccess.ExecuteNonQuery(sql, ("@description", description), ("@productId", productId));
        }

        public void EditProductPrice(int productId, decimal price)
        {
            var sql = "UPDATE products SET P_PRICE = @price WHERE P_ID = @productId";
            dbAccess.ExecuteNonQuery(sql, ("@price", price), ("@productId", productId));
        }

        public void EditProductImage(int productId, string image)
        {
            var sql = "UPDATE products SET P_IMAGE = @image WHERE P_ID = @productId";
            dbAccess.ExecuteNonQuery(sql, ("@image", image), ("@productId", productId));
        }

        public void EditProductQuantity(int productId, int quantity)
        {
            var sql = "UPDATE products SET P_QUANTITY = @quantity WHERE P_ID = @productId";
            dbAccess.ExecuteNonQuery(sql, ("@quantity", quantity), ("@productId", productId));
        }

        public void EditProductCategoryId(int productId, int categoryId)
        {
            var sql = "UPDATE products SET C_ID = @categoryId WHERE P_ID = @productId";
            dbAccess.ExecuteNonQuery(sql, ("@categoryId", categoryId), ("@productId", productId));
        }

        #endregion

        #region Delete Methods

        public void DeleteProduct(int Id)
        {
            var sql = "DELETE FROM products WHERE P_ID = @Id";
            dbAccess.ExecuteNonQuery(sql, ("@Id", Id));
        }

        #endregion

        #region Get Methods

        public List<Product> GetAllProducts()
        {
            var sql = "SELECT P_ID, P_NAME, P_MANUFACTURER, P_DESCRIPTION, P_PRICE, P_IMAGE, P_QUANTITY, C_ID FROM products";

            List<Product> products = new List<Product>();

            using (var cmd = dbAccess.dbDataSource.CreateCommand(sql))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Manufacturer = reader.GetString(2),
                            Description = reader.GetString(3),
                            Price = reader.GetDecimal(4),
                            Image = reader.GetString(5),
                            Quantity = reader.GetInt32(6),
                            CategoryId = reader.GetInt32(7)
                        });
                    }
                }
            }
            return products;
        }
        
        public Product? GetProductById(int productId)
        {
            var sql = "SELECT P_ID, P_NAME, P_MANUFACTURER, P_DESCRIPTION, P_PRICE, P_IMAGE, P_QUANTITY, C_ID FROM products WHERE P_ID = @productId";

            using (var cmd = dbAccess.dbDataSource.CreateCommand(sql))
            {
                cmd.Parameters.AddWithValue("productId", productId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Product
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Manufacturer = reader.GetString(2),
                            Description = reader.GetString(3),
                            Price = reader.GetDecimal(4),
                            Image = reader.GetString(5),
                            Quantity = reader.GetInt32(6),
                            CategoryId = reader.GetInt32(7)
                        };
                    }
                }
            }
            return null;
        }

        #endregion
    }
}