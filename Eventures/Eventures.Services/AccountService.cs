namespace Eventures.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Models;

    public class AccountService : IAccountService
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AccountService(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<bool> Register(string username, string password, string confirmPassword, string email, string firstName, string lastName, string uniqueCitizenNumber)
        {
            if (password != confirmPassword)
            {
                return false;
            }

            var user = new User
            {
                UserName = username,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                UniqueCitizenNumber = uniqueCitizenNumber
            };

            var result = this.userManager.CreateAsync(user, password).Result;

            if (result.Succeeded)
            {
                if (this.userManager.Users.Count() == 1)
                {
                    await this.userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    await this.userManager.AddToRoleAsync(user, "User");
                }

                this.signInManager.SignInAsync(user, false).Wait();
            }

            return result.Succeeded;
        }

        public bool Login(string username, string password, bool rememberMe)
        {
            var result = this.signInManager.PasswordSignInAsync(username, password, rememberMe, true).Result;

            return result.Succeeded;
        }

        public void Logout()
        {
            this.signInManager.SignOutAsync().Wait();
        }
    }
}