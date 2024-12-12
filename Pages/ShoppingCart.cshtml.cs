using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using ComputerECommerce.Models;

namespace ComputerECommerce.Pages
{

    public class ShoppingCartModel : PageModel
    {
        private readonly ShoppingCartService shoppingCartService;
        private readonly ProductService productService;
        public List<ShoppingCartItemViewModel> ShoppingCartItems { get; set; } = new List<ShoppingCartItemViewModel>();
        public decimal TotalPrice { get; set; }

        public ShoppingCartModel(ShoppingCartService shoppingCartService, ProductService productService)
        {
            this.shoppingCartService = shoppingCartService;
            this.productService = productService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await Protect();
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                var shoppingCart = shoppingCartService.GetShoppingCart(userId.Value);
                var items = shoppingCartService.GetShoppingCartItems(shoppingCart.Id);

                foreach (var item in items)
                {
                    var product = productService.GetProductById(item.ProductId);
                    ShoppingCartItems.Add(new ShoppingCartItemViewModel
                    {
                        Product = product,
                        Quantity = item.Quantity
                    });
                    TotalPrice += product!.Price * item.Quantity;
                }
            }
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
        }

        public async Task<IActionResult> OnPostAddAsync(int id)
        {
            await Protect();
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                var shoppingCart = shoppingCartService.GetShoppingCart(userId.Value);
                var shoppingCartItems = shoppingCartService.GetShoppingCartItems(shoppingCart.Id);
                var shoppingCartItem = shoppingCartItems.Find(x => x.ProductId == id);
                var product = productService.GetProductById(id);
                if (shoppingCartItem != null)
                {
                    shoppingCartService.UpdateShoppingCartItemQuantity(shoppingCartItem.Id, shoppingCartItem.Quantity + 1);
                }
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveAsync(int id)
        {
            await Protect();
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
            {
                var shoppingCart = shoppingCartService.GetShoppingCart(userId.Value);
                var shoppingCartItems = shoppingCartService.GetShoppingCartItems(shoppingCart.Id);
                var shoppingCartItem = shoppingCartItems.Find(x => x.ProductId == id);
                if (shoppingCartItem != null)
                {
                    if (shoppingCartItem.Quantity > 1)
                    {
                        shoppingCartService.UpdateShoppingCartItemQuantity(shoppingCartItem.Id, shoppingCartItem.Quantity - 1);
                    }
                    else
                    {
                        shoppingCartService.RemoveShoppingCartItem(shoppingCartItem.Id);
                    }
                }
            }
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostCheckoutAsync()
        {
            await Protect();
            return RedirectToPage("/Checkout");
        }
    }
    public class ShoppingCartItemViewModel
    {
        public Product? Product { get; set; }
        public int Quantity { get; set; }
    }
}
