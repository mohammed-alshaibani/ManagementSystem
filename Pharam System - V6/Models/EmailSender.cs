using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace Pharam_System___V6.Models
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var smtpClient = new SmtpClient("smtp.gamil.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("mom20175@gmail.com", "tbnvoetcesvhbqgd"),
                EnableSsl = true,
            };
            return smtpClient.SendMailAsync("email",email, subject, htmlMessage);
        }
    }
}
