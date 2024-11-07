using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ComputerECommerce.Data; // Adjust the namespace according to your project structure
using ComputerECommerce.Models; // Adjust the namespace according to your project structure
using System.Threading.Tasks;

namespace ComputerECommerce.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly DataContext _context;

        public ProductDetailsModel(DataContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products
                                    .FirstOrDefaultAsync(p => p.Id == id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}