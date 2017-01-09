using System.Threading.Tasks;

namespace Shadow_Arena.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
