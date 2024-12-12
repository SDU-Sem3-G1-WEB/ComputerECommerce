using DataAccess;
using ComputerECommerce.Models;

namespace Services
{
    public class CategoryService
    {
        private readonly CategoryRepository categoryRepository;

        public CategoryService(CategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public void AddCategory(string name, string description, int? parentCategoryId)
        {
            categoryRepository.InsertCategory(name, description, parentCategoryId);
        }

        public void UpdateCategoryName(int categoryId, string name)
        {
            categoryRepository.EditCategoryName(categoryId, name);
        }

        public void UpdateCategoryDescription(int categoryId, string description)
        {
            categoryRepository.EditCategoryDescription(categoryId, description);
        }

        public void UpdateCategoryParentId(int categoryId, int? parentCategoryId)
        {
            categoryRepository.EditCategoryParentId(categoryId, parentCategoryId);
        }

        public void DeleteCategory(int categoryId)
        {
            categoryRepository.DeleteCategory(categoryId);
        }
        
        public List<Category> GetAllCategories()
        {
            return categoryRepository.GetAllCategories();
        }
    }
}