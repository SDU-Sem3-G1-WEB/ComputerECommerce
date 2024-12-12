using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComputerECommerce.Pages
{
    public class OrderConfirmationModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int OrderId { get; set; }

        public void OnGet()
        {
            
        }
    }
}