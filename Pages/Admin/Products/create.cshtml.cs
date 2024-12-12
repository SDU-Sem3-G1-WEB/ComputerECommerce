using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ComputerECommerce.Models;
using Services;

namespace ComputerECommerce.Pages.Admin.Products
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public ProductDto ProductDto { get; set; } = new ProductDto();
        private readonly IWebHostEnvironment env;
        private readonly CategoryService categoryService;
        private readonly ProductService productService;
        private readonly UserPermissionService userPermissionService;
        public string ErrorMessage { get; set; } = string.Empty;
        public string SuccessMessage { get; set; } = string.Empty;
        public List<Category> Categories { get; set; } = new List<Category>();

        public CreateModel(IWebHostEnvironment env, CategoryService categoryService, ProductService productService, UserPermissionService userPermissionService)
        {
            this.env = env;
            this.categoryService = categoryService;
            this.productService = productService;
            this.userPermissionService = userPermissionService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await Protect();
            Categories =  categoryService.GetAllCategories();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await Protect();
            if (ProductDto.Image == null)
            {
                ModelState.AddModelError("ProductDto.ImageFile", "Image is required");
            }
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please provide all required fields";
                return Page();
            }
            string fileName = Guid.NewGuid() + ProductDto.Image!.FileName;
            string filePath = Path.Combine(env.WebRootPath, "images", "products", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                ProductDto.Image.CopyTo(fileStream);
            }

            fileName = "/images/products/" + fileName;

            productService.AddProduct(ProductDto.Name, ProductDto.Manufacturer, ProductDto.Description, ProductDto.Price, fileName, ProductDto.Quantity, ProductDto.CategoryId);

            ProductDto.Clear();

            ModelState.Clear();

            SuccessMessage = "Product created successfully";
            return Page();
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