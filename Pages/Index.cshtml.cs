using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using ComputerECommerce.Models;
using System.Linq;

namespace ComputerECommerce.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ProductService productService;
        public List<Product> Products { get; set; } = new List<Product>();
        
        [BindProperty(SupportsGet = true)]
        public string PriceRange { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; } = string.Empty;

        public IndexModel(ProductService productService)
        {
            this.productService = productService;
        }

        public async Task OnGetAsync()
        {
            var products = await Task.Run(() => productService.GetAllProducts());

            products = FilterProducts(products, PriceRange);
            products = SortProducts(products, SortOrder);

            Products = products;
        }

        private List<Product> FilterProducts(List<Product> products, string priceRange)
        {
            if (string.IsNullOrEmpty(priceRange))
            {
                return products;
            }

            var ranges = priceRange.Split('-');
            if (ranges.Length == 2 && decimal.TryParse(ranges[0], out var minPrice) && decimal.TryParse(ranges[1], out var maxPrice))
            {
                return products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
            }
            else if (priceRange == "800+")
            {
                return products.Where(p => p.Price > 800).ToList();
            }

            return products;
        }

        private List<Product> SortProducts(List<Product> products, string sortOrder)
        {
            return sortOrder switch
            {
                "name_asc" => products.OrderBy(p => p.Name).ToList(),
                "name_desc" => products.OrderByDescending(p => p.Name).ToList(),
                "price_asc" => products.OrderBy(p => p.Price).ToList(),
                "price_desc" => products.OrderByDescending(p => p.Price).ToList(),
                _ => products,
            };
        }
    }
}