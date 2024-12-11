using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ComputerECommerce.Data;
using BCrypt.Net;

public class LoginModel : PageModel
{
    private readonly DataContext _context;

    public LoginModel(DataContext context)
    {
        _context = context;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public static string UserRole { get; private set; }

    public class InputModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public void OnGet()
    {
        UserRole = null;
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = _context.Users.SingleOrDefault(u => u.Email == Input.Email);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Email not found.");
            return Page();
        }

        var credentials = _context.UserCredentials.SingleOrDefault(c => c.hashedEmail == Input.Email);
        if (credentials == null || !BCrypt.Net.BCrypt.Verify(Input.Password, credentials.hashedPassword))
        {
            ModelState.AddModelError(string.Empty, "Incorrect password.");
            return Page();
        }

        UserRole = user.Role;

        return RedirectToPage("./Index");
    }
}