using RealEstateApp.Services.ServiceInterfaces;
using System.Net;
using System.Net.Mail;

namespace RealEstateApp.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string fromAddress, string toAddress, string subject, string message)
        {
            MailMessage mailMessage = new MailMessage(fromAddress, toAddress, subject, message);
            mailMessage.IsBodyHtml = true;

            using SmtpClient client = new SmtpClient(_configuration["SMTP:Host"], int.Parse(_configuration["SMTP:Port"]!))
            {
                Credentials = new NetworkCredential(_configuration["SMTP:Username"], _configuration["SMTP:Password"])
            };
            await client.SendMailAsync(mailMessage);
        }
    }
}
