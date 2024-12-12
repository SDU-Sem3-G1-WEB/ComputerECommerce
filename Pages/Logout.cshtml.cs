using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComputerECommerce.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly LoginStateService loginStateService;

        public LogoutModel(LoginStateService loginStateService)
        {
            this.loginStateService = loginStateService;
        }

        public IActionResult OnPost()
        {
            // Clear the session
            HttpContext.Session.Clear();

            // Update the login state
            loginStateService.IsLoggedIn = false;

            return RedirectToPage("/Index");
        }
    }
}