namespace RealEstateApp.Services.ServiceInterfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string fromAddress, string toAddress, string subject, string message);
    }
}
