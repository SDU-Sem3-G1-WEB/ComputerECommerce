using DataAccess;
using ComputerECommerce.Models;

namespace Services
{
    public class ProductService
    {
        private readonly ProductRepository productRepository;

        public ProductService(ProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public void AddProduct(string name, string manufacturer, string description, decimal price, string image, int quantity, int categoryId)
        {
            productRepository.InsertProduct(name, manufacturer, description, price, image, quantity, categoryId);
        }

        public void UpdateProduct(int productId, string name, string manufacturer, string description, decimal price, string image, int quantity, int categoryId)
        {
            productRepository.EditProduct(productId, name, manufacturer, description, price, image, quantity, categoryId);
        }

        public void UpdateProductName(int productId, string name)
        {
            productRepository.EditProductName(productId, name);
        }

        public void UpdateProductManufacturer(int productId, string manufacturer)
        {
            productRepository.EditProductManufacturer(productId, manufacturer);
        }

        public void UpdateProductDescription(int productId, string description)
        {
            productRepository.EditProductDescription(productId, description);
        }

        public void UpdateProductPrice(int productId, decimal price)
        {
            productRepository.EditProductPrice(productId, price);
        }

        public void UpdateProductImage(int productId, string image)
        {
            productRepository.EditProductImage(productId, image);
        }

        public void UpdateProductQuantity(int productId, int quantity)
        {
            productRepository.EditProductQuantity(productId, quantity);
        }

        public void UpdateProductCategory(int productId, int categoryId)
        {
            productRepository.EditProductCategoryId(productId, categoryId);
        }

        public void DeleteProduct(int productId)
        {
            productRepository.DeleteProduct(productId);
        }
        
        public List<Product> GetAllProducts()
        {
            return productRepository.GetAllProducts();
        }

        public Product? GetProductById(int productId)
        {
            return productRepository.GetProductById(productId);
        }
    }
}