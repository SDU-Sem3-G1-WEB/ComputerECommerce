using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using System.Text;
using System.Security.Cryptography;

namespace ComputerECommerce.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserService userService;
        private readonly UserCredentialsService userCredentialsService;
        private readonly string fixedSalt;

        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        public string RepeatPassword { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public RegisterModel(UserService userService, UserCredentialsService userCredentialsService)
        {
            this.userService = userService;
            this.userCredentialsService = userCredentialsService;
            fixedSalt = userCredentialsService.GetFixedSalt();
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Password != RepeatPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return Page();
            }

            if (userService.DoesEmailExist(Email))
            {
                ErrorMessage = "Email already exists.";
                return Page();
            }

            // Hash the email and password
            string emailHash = BCrypt.Net.BCrypt.HashPassword(Email, fixedSalt);
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(Password, fixedSalt);

            byte[] emailHashBytes = System.Text.Encoding.UTF8.GetBytes(emailHash);
            byte[] passwordHashBytes = System.Text.Encoding.UTF8.GetBytes(passwordHash);

            // Add user credentials
            userCredentialsService.AddUserCredentials(emailHashBytes, passwordHashBytes);

            // Add user
            userService.AddUser(Username, Email, 2);

            SuccessMessage = "Registration successful. Please log in.";
            return RedirectToPage("/Account/Login");
        }
    }
}