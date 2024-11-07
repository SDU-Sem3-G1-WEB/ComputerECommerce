using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ComputerECommerce.Pages;

public class FaqModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    public FaqModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}
