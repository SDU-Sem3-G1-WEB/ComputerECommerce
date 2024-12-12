using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ComputerECommerce.Models;
using Services;

namespace ComputerECommerce.Pages.Admin.Products
{
    public class IndexModel : PageModel
    {
        private readonly ProductService productService;
        private readonly CategoryService categoryService;
        public List<Product> Products = new List<Product>();
        public List<Category> Categories = new List<Category>();

        public IndexModel(ProductService productService, CategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await Protect();
            Products = productService.GetAllProducts();
            Categories = categoryService.GetAllCategories();
            return Page();
        }
        public string GetCategoryForProduct(int categoryId)
        {
            foreach (var category in Categories)
            {
                if (category.Id == categoryId)
                {
                    return category.Name!;
                }
            }
            return "Unknown Category";
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