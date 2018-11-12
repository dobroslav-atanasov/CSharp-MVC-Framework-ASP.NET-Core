namespace Chushka.Services.Contracts
{
    using Models;

    public interface IUserService
    {
        bool IsUserExists(string username);

        void AddUser(string username, string password, string fullName, string email);

        User GetUserByUsername(string username);

        User GetUserByUsernameAndPassword(string username, string password);
    }
}