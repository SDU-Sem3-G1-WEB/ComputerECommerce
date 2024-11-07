using ComputerECommerce.Data;
using ComputerECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ComputerECommerce.Pages.Admin.Products
{
    public class EditModel : PageModel
    {
        private readonly DataContext context;
        private readonly IWebHostEnvironment env;
        [BindProperty]
        public ProductDto ProductDto {get; set;}= new ProductDto();
        public Product Product { get; set; } = new Product();
        public List<Category> Categories { get; set; } = new List<Category>();
        public EditModel(IWebHostEnvironment environment, DataContext context)
        {
            this.context = context;
            this.env = environment;
        }
        public void OnGet(string? id)
        {
            Categories = context.Categories.ToList();
            if (id == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }
            var product = context.Products.Find(id);
            if (product == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }
            ProductDto.Name = product.Name;
            ProductDto.Description = product.Description;
            ProductDto.Price = product.Price;
            ProductDto.Quantity = product.Quantity;
            //ProductDto.Category = product.Category;

            Product = product;
        }

        public void OnPost(int? id)
        {
            if(id == null)
            {
                Response.Redirect("/Admin/Products/Index");
                return;
            }
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Invalid data");
                return;
            }
            var product = context.Products.Find(id);
            if (product == null)
            {
                Debug.WriteLine("Product not found");
                return;
            }

            string newFileName = product.Image;
            if (ProductDto.ImageFile != null)
            {
                newFileName = Guid.NewGuid() + ProductDto.ImageFile!.FileName;
                string filePath = Path.Combine(env.WebRootPath, "products", newFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ProductDto.ImageFile.CopyTo(fileStream);
                }
                string oldFilePath = Path.Combine(env.WebRootPath, "products", product.Image);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            product.Name = ProductDto.Name;
            product.Description = ProductDto.Description;
            product.Price = ProductDto.Price;
            product.Quantity = ProductDto.Quantity;
            product.Image = newFileName;
            product.CategoryId = "Laptop"; //temporary

            context.SaveChanges();

            Product = product;

            Response.Redirect("/Admin/Products/Index");
        }
    }
}