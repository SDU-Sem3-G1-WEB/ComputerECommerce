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
        [BindProperty]
        private string ErrorMessage { get; set; } = String.Empty;
        private string SuccessMessage { get; set; } = String.Empty;
        public TicketDto TicketDto { get; set; } = new TicketDto();
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
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please provide all required fields";
                return;
            }
            Ticket ticket = new Ticket
            {
                Id = Guid.NewGuid().ToString(),
                Name = TicketDto.Name,
                Topic = TicketDto.Topic,
                Email = TicketDto.Email,
                CategoryId = TicketDto.CategoryId,
                Description = TicketDto.Description,
                TicketDate = DateTime.Now.ToUniversalTime().ToString(),
            };

            _context.Tickets.Add(ticket);
            _context.SaveChanges();

            TicketDto.Clear();

            ModelState.Clear();

            SuccessMessage = "Ticket sent successfully";

            Response.Redirect("/CustomerSupport/Faq");
        }
    }
}
