using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ComputerECommerce.Data;
using Microsoft.AspNetCore.Http;
using ComputerECommerce.Pages;

public class LoginModel : PageModel
{
    private readonly DataContext _context;

    public LoginModel(DataContext context)
    {
        _context = context;
    }

    [BindProperty]
    public InputModel Input { get; set; }
    public static string Username { get; private set; }

    public class InputModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public void OnGet()
    {
        TwoFAModel.UserRole = null;
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = _context.Users
            .FirstOrDefault(u => u.Email == Input.Username && u.Password == Input.Password);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid E-Mail or Password.");
            return Page();
        }

        Username = user.Email;

        return RedirectToPage("./TwoFA");
    }
}