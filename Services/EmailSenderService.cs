using System.Net;
using System.Net.Mail;

namespace ComputerECommerce.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("computer.ecommerce4@gmail.com", "ptrp tkiw mimg dnqh")
            };

            try
            {
            return client.SendMailAsync(
                new MailMessage(from: "computer.ecommerce4@gmail.com",
                                to: email,
                                subject,
                                message
                                ));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
