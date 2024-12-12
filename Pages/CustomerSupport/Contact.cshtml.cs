using ComputerECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;

namespace ComputerECommerce.Pages.CustomerSupport
{
    public class ContactModel : PageModel
    {
        private readonly IEmailService emailService;

        [BindProperty]
        public TicketDto TicketDto { get; set; } = new TicketDto();
        public List<Category> Categories { get; set; } = new List<Category>();
        public string ErrorMessage { get; set; } = string.Empty;
        public string SuccessMessage { get; set; } = string.Empty;

        public ContactModel(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        public void OnGet()
        {
            Categories = new List<Category>
            {
                new Category { Id = 1, Name = "General Inquiry" },
                new Category { Id = 2, Name = "Technical Support" },
                new Category { Id = 3, Name = "Billing" },
                new Category { Id = 4, Name = "Other" },
            };
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please provide all required fields";
                return Page();
            }

            var emailBody = $"Name: {TicketDto.Name}\nEmail: {TicketDto.Email}\nCategory: {TicketDto.CategoryId}\nDescription: {TicketDto.Description}";
            var emailSubject = "New Customer Support Ticket";

            var result = emailService.SendEmail("support@example.com", emailSubject, emailBody);

            if (result)
            {
                SuccessMessage = "Your message has been sent successfully.";
                ModelState.Clear();
                TicketDto.Clear();
                return RedirectToPage("/CustomerSupport/Faq");
            }
            else
            {
                ErrorMessage = "There was an error sending your message. Please try again later.";
                return Page();
            }
        }
    }

    public interface IEmailService
    {
        bool SendEmail(string to, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        public bool SendEmail(string to, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.example.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("username", "password"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("noreply@example.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false,
                };
                mailMessage.To.Add(to);

                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}