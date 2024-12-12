using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace ComputerECommerce.Pages.Admin.Products
{
    public class DeleteModel : PageModel
    {
        private readonly ProductService productService;
        private readonly IWebHostEnvironment env;

        public DeleteModel(IWebHostEnvironment env, ProductService productService)
        {
            this.env = env;
            this.productService = productService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            await Protect();
            var product = productService.GetProductById(id);

            string filePath = env.WebRootPath + product!.Image;

            if(System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            productService.DeleteProduct(id);

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