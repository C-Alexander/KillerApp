using Microsoft.AspNetCore.Http;
using Shadow_Arena.Models;

namespace Shadow_Arena
{
    public interface ILoginManager
    {
        bool IsLoggedIn(ISession session);
        bool Login(ISession httpContextSession, LoginViewModel player);
    }
}