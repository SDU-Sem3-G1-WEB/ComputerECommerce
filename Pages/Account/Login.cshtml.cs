using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Services;
using System.Text;
using System.Security.Cryptography;

namespace ComputerECommerce.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserCredentialsService userCredentialsService;
        private readonly UserService userService;
        private readonly UserPermissionService userPermissionService;
        private readonly LoginStateService loginStateService;
        private readonly string fixedSalt;

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }

        public LoginModel(UserCredentialsService userCredentialsService, UserService userService, UserPermissionService userPermissionService, LoginStateService loginStateService)
        {
            this.userCredentialsService = userCredentialsService;
            this.userService = userService;
            this.userPermissionService = userPermissionService;
            this.loginStateService = loginStateService;
            fixedSalt = userCredentialsService.GetFixedSalt();
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("SessionId") != null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Hash the email and password
                string emailHash = BCrypt.Net.BCrypt.HashPassword(Email, fixedSalt);
                string hashedEmailHex = userCredentialsService.ConvertToHex(emailHash);
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(Password, fixedSalt);
                string hashedPasswordHex = userCredentialsService.ConvertToHex(passwordHash);

            // Validate credentials
            if (userCredentialsService.ValidateCredentials(hashedEmailHex, hashedPasswordHex) && userService.GetUser(Email) != null)
            {
                var user = userService.GetUser(Email);
                if (user != null)
                {
                    string sessionId = Guid.NewGuid().ToString();
                    HttpContext.Session.SetString("SessionId", sessionId);
                    HttpContext.Session.SetInt32("UserId", user.UserID);
                    HttpContext.Session.SetString("Email", Email);
                    var userType = userService.GetUserType(user.UserID);
                    HttpContext.Session.SetString("UserType", userType);
                    HttpContext.Session.SetString("IsLoggedIn", "true");

                    loginStateService.IsLoggedIn = true;
                    userPermissionService.SetUser(user);

                    return RedirectToPage("/Index");
                }
                else
                {
                    ErrorMessage = "Session Storage not found.";
                    return Page();
                }
            }
            else
            {
                ErrorMessage = "Invalid email or password.";
                return Page();
            }
        }
    }
}