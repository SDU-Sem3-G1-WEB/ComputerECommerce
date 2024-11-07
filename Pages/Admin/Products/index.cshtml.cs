using ComputerECommerce.Data;
using ComputerECommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComputerECommerce.Pages.Admin.Products
{
    public class IndexModel : PageModel
    {
        private readonly DataContext context;
        public List<Product> Products = new List<Product>();
        public List<Category> Categories = new List<Category>();
        public IndexModel(DataContext context)
        {
            this.context = context;
            // Constructor code
        }
        public void OnGet()
        {
            Products = context.Products.ToList();
            Categories = context.Categories.ToList();
        }
    }
}