using System.Data;
using ComputerECommerce.Data;
using ComputerECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ComputerECommerce.Pages.Admin.Products
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public ProductDto ProductDto { get; set; } = new ProductDto();
        private readonly DataContext context;
        private readonly IWebHostEnvironment env;
        public string ErrorMessage { get; set; } = String.Empty;
        public string SuccessMessage { get; set; } = String.Empty;

        public CreateModel(IWebHostEnvironment env, DataContext context)
        {
            this.env = env;
            this.context = context;
        }
        public void OnGet()
        {
        }
        public void OnPost()
        {
            if (ProductDto.ImageFile == null)
            {
                ModelState.AddModelError("ProductDto.ImageFile", "Image is required");
            }
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please provide all required fields";
                return;
            }
            string fileName = Guid.NewGuid() + ProductDto.ImageFile!.FileName;
            string filePath = Path.Combine(env.WebRootPath, "products", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                ProductDto.ImageFile.CopyTo(fileStream);
            }
            Product product = new Product
            {
                Name = ProductDto.Name,
                Description = ProductDto.Description,
                Price = ProductDto.Price,
                Quantity = ProductDto.Quantity,
                Image = fileName,
                //CategoryId = ProductDto.Category.Id
            };

            //context.Products.Add(product);
            //context.SaveChanges();

            ProductDto.Clear();

            ModelState.Clear();

            SuccessMessage = "Product created successfully";
        }
    }
}