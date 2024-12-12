using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ComputerECommerce.Models;
using Services;

namespace ComputerECommerce.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly ProductService productService;
        private readonly ShoppingCartService shoppingCartService;
        public Product? Product { get; set; }
        public ProductDetailsModel(ProductService productService, ShoppingCartService shoppingCartService)
        {
            this.productService = productService;
            this.shoppingCartService = shoppingCartService;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await Task.Run(() => productService.GetProductById(id));

            if (Product == null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int id, int quantity)
        {
            await Protect();
            var userId = HttpContext.Session.GetInt32("UserId");
            shoppingCartService.AddShoppingCart(userId!.Value);
            var shoppingCart = shoppingCartService.GetShoppingCart(userId.Value);
            if (shoppingCart == null)
            {
                shoppingCartService.AddShoppingCart(userId.Value);                
            }
            shoppingCart = shoppingCartService.GetShoppingCart(userId.Value);
            var shoppingCartItems = shoppingCartService.GetShoppingCartItems(shoppingCart.Id);
            if (shoppingCartItems.Exists(x => x.ProductId == id))
            {
                var shoppingCartItem = shoppingCartItems.Find(x => x.ProductId == id);
                shoppingCartService.UpdateShoppingCartItemQuantity(shoppingCartItem!.Id,  quantity);
            }
            else
            {
                shoppingCartService.AddShoppingCartItem(shoppingCart.Id, id, quantity);
            }
            
            return RedirectToPage("/ShoppingCart");
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
        }
    }
}