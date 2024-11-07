using ComputerECommerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComputerECommerce.Pages.CustomerSupport
{
    public class FaqModel : PageModel
    {
        private readonly DataContext _context;

        public FaqModel(DataContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }
    }
}
