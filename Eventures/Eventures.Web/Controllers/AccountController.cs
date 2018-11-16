namespace Eventures.Web.Controllers
{
    using System.Linq;
    using AutoMapper;
    using Eventures.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Account;

    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, IMapper mapper)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            var user = this.mapper.Map<User>(model);

            var result = this.userManager.CreateAsync(user, model.Password).Result;
            if (result.Succeeded)
            {
                if (!this.userManager.Users.Any())
                {
                    this.userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    this.userManager.AddToRoleAsync(user, "User");
                }
                this.signInManager.SignInAsync(user, false).Wait();
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var user = this.userManager.Users.FirstOrDefault(u => u.UserName == model.Username);
            var result = this.signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, true).Result;
            if (result.Succeeded)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        public IActionResult Logout()
        {
            this.signInManager.SignOutAsync();
            return this.RedirectToAction("Index", "Home");
        }
    }
}