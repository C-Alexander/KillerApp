namespace Shadow_Arena.Services
{
    public interface IHashing
    {
        string GetHashedPassword(string password);
    }
}