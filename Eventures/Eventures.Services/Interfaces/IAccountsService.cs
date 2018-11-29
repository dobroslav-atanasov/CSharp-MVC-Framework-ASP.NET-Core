namespace Eventures.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface IAccountsService
    {
        Task<bool> Register(string username, string password, string confirmPassword, string email, string firstName, string lastName, string uniqueCitizenNumber);

        bool Login(string username, string password, bool rememberMe);

        void Logout();

        ICollection<User> GetAllUsers();

        void PromoteUser(string userId);

        void DemoteUser(string userId);
    }
}