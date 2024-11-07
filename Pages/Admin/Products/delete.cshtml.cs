using System.Diagnostics;
using ComputerECommerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComputerECommerce.Pages.Admin.Products
{
    public class DeleteModel : PageModel
    {
        private readonly DataContext context;
        private readonly IWebHostEnvironment env;
        public DeleteModel(IWebHostEnvironment env, DataContext context)
        {
            this.env = env;
            this.context = context;
        }
        public void OnGet(int? id)
        {
            if(id == null)
            {
                Debug.WriteLine("Product Id is null");
                Response.Redirect("/Admin/Products/Index");
                return;
            }
            var product = context.Products.Find(id);
            if(product == null)
            {
                Debug.WriteLine("Product not found in context");
                Response.Redirect("/Admin/Products/Index");
                return;
            }
            string filePath = Path.Combine(env.WebRootPath, "products", product.Image);
            if(System.IO.File.Exists(filePath))
            {
                Debug.WriteLine("Deleting file: " + filePath);
                System.IO.File.Delete(filePath);
            }

            context.Products.Remove(product);

            context.SaveChanges();

            Response.Redirect("/Admin/Products/Index");
        }
    }
}