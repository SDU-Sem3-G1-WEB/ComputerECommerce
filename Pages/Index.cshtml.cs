using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ComputerECommerce.Data;
using ComputerECommerce.Models;

namespace ComputerECommerce.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DataContext _context;

        public IndexModel(DataContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; }
        public string PriceRange { get; set; }
        public string SortOrder { get; set; }

        public async Task OnGetAsync(string priceRange, string sortOrder)
        {
            PriceRange = priceRange;
            SortOrder = sortOrder;

            var products = from p in _context.Products
                        select p;

            // Apply filtering based on the price range
            if (!string.IsNullOrEmpty(PriceRange))
            {
                var ranges = PriceRange.Split('-');
                if (ranges.Length == 2 && decimal.TryParse(ranges[0], out var minPrice) && decimal.TryParse(ranges[1], out var maxPrice))
                {
                    products = products.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
                }
                else if (PriceRange == "800+")
                {
                    products = products.Where(p => p.Price > 800);
                }
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(SortOrder))
            {
                switch (SortOrder)
                {
                    case "name_asc":
                        products = products.OrderBy(p => p.Name);
                        break;
                    case "name_desc":
                        products = products.OrderByDescending(p => p.Name);
                        break;
                    case "price_asc":
                        products = products.OrderBy(p => p.Price);
                        break;
                    case "price_desc":
                        products = products.OrderByDescending(p => p.Price);
                        break;
                }
            }

            Products = await products.ToListAsync();
        }


        public bool IsProductInPriceRange(Product product)
        {
            if (string.IsNullOrEmpty(PriceRange))
            {
                return true;
            }

            var ranges = PriceRange.Split('-');
            if (ranges.Length == 2)
            {
                if (decimal.TryParse(ranges[0], out var minPrice) && decimal.TryParse(ranges[1], out var maxPrice))
                {
                    return product.Price >= minPrice && product.Price <= maxPrice;
                }
            }
            else if (PriceRange == "800+")
            {
                return product.Price > 800;
            }

            return false;
        }
    }
}