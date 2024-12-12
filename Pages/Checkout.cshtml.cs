using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using ComputerECommerce.Models;

namespace ComputerECommerce.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly ShoppingCartService shoppingCartService;
        private readonly OrderService orderService;
        private readonly ProductService productService;
        [BindProperty]
        public string AddressLine { get; set; } = string.Empty;
        [BindProperty]
        public string City { get; set; } = string.Empty;
        [BindProperty]
        public string PostalCode { get; set; } = string.Empty;
        [BindProperty]
        public string Country { get; set; } = string.Empty;

        public CheckoutModel(ShoppingCartService shoppingCartService, OrderService orderService, ProductService productService)
        {
            this.shoppingCartService = shoppingCartService;
            this.orderService = orderService;
            this.productService = productService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await Protect();
            var userId = HttpContext.Session.GetInt32("UserId");

            var shoppingCart = shoppingCartService.GetShoppingCart(userId!.Value);
            var shoppingCartItems = shoppingCartService.GetShoppingCartItems(shoppingCart.Id);

            if (shoppingCartItems.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Your cart is empty.");
                return Page();
            }

            var orderDate = DateTime.Now;
            var totalPrice = shoppingCartItems.Sum(item => 
            {
                var product = productService.GetProductById(item.ProductId);
                return item.Quantity * product!.Price;
            });
            string orderStatus = OrderStatus.Pending.ToString();

            orderService.AddOrder(userId.Value, orderDate, totalPrice, orderStatus, AddressLine, City, PostalCode, Country);
            var order = orderService.GetUserOrders(userId.Value).Last();

            foreach (var item in shoppingCartItems)
            {
                var product = productService.GetProductById(item.ProductId);
                orderService.AddOrderItem(order.Id, item.ProductId, item.Quantity, product!.Price);
            }
            
            shoppingCartService.RemoveShoppingCart(shoppingCart.Id);

            return RedirectToPage("/OrderConfirmation", new { orderId = order.Id });
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
            var shoppingCart = shoppingCartService.GetShoppingCart(userId.Value);
            if(shoppingCart == null)
            {
                // Redirect to index if user has no shopping cart
                Response.Redirect("/Index");
                await Task.CompletedTask;
                return;
            }
        }
    }
}