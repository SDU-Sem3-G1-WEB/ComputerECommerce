using ComputerECommerce.Models;

namespace DataAccess
{
    public class CategoryRepository
    {
        private readonly DbAccess dbAccess;

        public CategoryRepository(DbAccess dbAccess)
        {
            this.dbAccess = dbAccess;
        }

        #region Insert Methods

        public void InsertCategory(string name, string description, int? parentCategoryId)
        {
            var sql = "INSERT INTO categories (C_NAME, C_DESCRIPTION, C_PARENT_ID) VALUES (@name, @description, @parentCategoryId)";
            dbAccess.ExecuteNonQuery(sql, ("@name", name), ("@description", description), ("@parentCategoryId", parentCategoryId ?? (object)DBNull.Value));
        }

        #endregion

        #region Edit Methods

        public void EditCategoryName(int categoryId, string name)
        {
            var sql = "UPDATE categories SET C_NAME = @name WHERE C_ID = @categoryId";
            dbAccess.ExecuteNonQuery(sql, ("@name", name), ("@categoryId", categoryId));
        }
        public void EditCategoryDescription(int categoryId, string description)
        {
            var sql = "UPDATE categories SET C_DESCRIPTION = @description WHERE C_ID = @categoryId";
            dbAccess.ExecuteNonQuery(sql, ("@description", description), ("@categoryId", categoryId));
        }
        public void EditCategoryParentId(int categoryId, int? parentCategoryId)
        {
            var sql = "UPDATE categories SET C_PARENT_ID = @parentCategoryId WHERE C_ID = @categoryId";
            dbAccess.ExecuteNonQuery(sql, ("@parentCategoryId", parentCategoryId ?? (object)DBNull.Value), ("@categoryId", categoryId));
        }

        #endregion

        #region Delete Methods

        public void DeleteCategory(int categoryId)
        {
            var sql = "DELETE FROM categories WHERE C_ID = @categoryId";
            dbAccess.ExecuteNonQuery(sql, ("@categoryId", categoryId));
        }

        #endregion

        #region Get Methods

        public List<Category> GetAllCategories()
        {
            var sql = "SELECT * FROM categories";
            List<Category> categories = new List<Category>();

            using (var cmd = dbAccess.dbDataSource.CreateCommand(sql))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            ParentCategoryId = reader.IsDBNull(3) ? null : (int?)reader.GetInt32(3)
                        });
                    }
                }
            }
            return categories;
        }

        #endregion
    }
}