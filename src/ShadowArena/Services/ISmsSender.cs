using System.Threading.Tasks;

namespace Shadow_Arena.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
