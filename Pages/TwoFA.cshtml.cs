using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ComputerECommerce.Data;
using ComputerECommerce.Services;

namespace ComputerECommerce.Pages
{
    public class TwoFAModel : PageModel
    {
        private readonly IEmailSenderService _emailSender;
        private readonly DataContext _context;
        public static string ConfirmationCode { get; private set; }

        public TwoFAModel(IEmailSenderService emailSender, DataContext context)
        {
            _emailSender = emailSender;
            _context = context;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            public string? ConfirmationCode { get; set; }
        }
        public static string? UserRole { get; private set; }
        public void OnGet()
        {
            ConfirmationCode = new Random().Next(100000, 999999).ToString();
            _emailSender.SendEmailAsync(LoginModel.Username, "Your confirmation code is "+ ConfirmationCode, "Enter this code to log in into your ComputerECommerce account: " + ConfirmationCode);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Input.ConfirmationCode != ConfirmationCode)
            {
                ModelState.AddModelError(string.Empty, "Invalid confirmation code.");
                return Page();
            }

            var user = _context.Users
            .FirstOrDefault(u => u.Email == LoginModel.Username);
            UserRole = user.Role;

            return RedirectToPage("./Index");
        }
    }
}