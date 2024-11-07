using System.Data;
using ComputerECommerce.Data;
using ComputerECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ComputerECommerce.Pages.CustomerSupport
{
    public class ContactModel : PageModel
    {
        private readonly DataContext _context;
        public List<Category> Categories { get; set; } = new List<Category>();
        public ContactModel(DataContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Categories = _context.Categories.ToList();
        }
        public void OnPost()
        {

        }
    }
}