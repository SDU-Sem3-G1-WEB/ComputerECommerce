using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ComputerECommerce.Models;
using Services;
using System.Diagnostics;


namespace ComputerECommerce.Pages.Admin.Products
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public ProductDto ProductDto {get; set;}= new ProductDto();
        public Product Product { get; set; } = new Product();
        private readonly CategoryService categoryService;
        private readonly ProductService productService;
        private readonly IWebHostEnvironment env;
        public List<Category> Categories { get; set; } = new List<Category>();

        public EditModel(IWebHostEnvironment env, CategoryService categoryService, ProductService productService)
        {
            this.env = env;
            this.categoryService = categoryService;
            this.productService = productService;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            await Protect();
            Categories = categoryService.GetAllCategories();

            Product = productService.GetProductById(id)!;

            if(Product == null)
            {
                return NotFound();
            }

            ProductDto = new ProductDto
            {
                Id = Product.Id,
                Name = Product.Name!,
                Manufacturer = Product.Manufacturer!,
                Description = Product.Description!,
                Price = Product.Price,
                Quantity = Product.Quantity,
                CategoryId = Product.CategoryId
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await Protect();
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Invalid data");
                return Page();
            }
            
            var product = productService.GetProductById(id);


            if (product == null)
            {
                Debug.WriteLine("Product not found");
                return NotFound();
            }

            string? newFileName = product.Image;
            if (ProductDto.Image != null)
            {
                newFileName = Guid.NewGuid() + ProductDto.Image!.FileName;
                string filePath = Path.Combine(env.WebRootPath, "images", "products", newFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ProductDto.Image.CopyTo(fileStream);
                }
                string oldFilePath = Path.Combine(env.WebRootPath, product.Image!);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
                
                newFileName = "/images/products/" + newFileName;
            }

            productService.UpdateProduct(id, ProductDto.Name, ProductDto.Manufacturer, ProductDto.Description, ProductDto.Price, newFileName!, ProductDto.Quantity, ProductDto.CategoryId);

            return RedirectToPage("/Admin/Products/Index");
        }
        private async Task Protect()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                // Redirect to login if user is not logged in
                Response.Redirect("/Account/Login");
                await Task.CompletedTask;
                return;
            }

            var userType = HttpContext.Session.GetString("UserType");
            if (userType != "ADMIN")
            {
                // Redirect to unauthorized page if user is not an admin
                Response.Redirect("/Unauthorised");
                await Task.CompletedTask;
            }
        }
    }
}