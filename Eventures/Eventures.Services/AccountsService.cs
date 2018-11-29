namespace Eventures.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Models;

    public class AccountsService : IAccountsService
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly EventuresDbContext context;

        public AccountsService(SignInManager<User> signInManager, UserManager<User> userManager, EventuresDbContext context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.context = context;
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

        public ICollection<User> GetAllUsers()
        {
            var users = this.context
                .Users
                .ToList();

            return users;
        }

        public void PromoteUser(string userId)
        {
            var user = this.userManager.Users.FirstOrDefault(u => u.Id == userId);

            var role = this.userManager.GetRolesAsync(user).GetAwaiter().GetResult();

            var userRole = this.context.UserRoles.FirstOrDefault(ur => ur.UserId == userId);
            this.context.UserRoles.Remove(userRole);

            this.userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
        }

        public void DemoteUser(string userId)
        {
            var user = this.userManager.Users.FirstOrDefault(u => u.Id == userId);

            var role = this.userManager.GetRolesAsync(user).GetAwaiter().GetResult();

            var userRole = this.context.UserRoles.FirstOrDefault(ur => ur.UserId == userId);
            this.context.UserRoles.Remove(userRole);

            this.userManager.AddToRoleAsync(user, "User").GetAwaiter().GetResult();
        }
    }
}