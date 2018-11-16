using AutoMapper;
using Eventures.Models;
using Eventures.Web.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Eventures.Web.Controllers
{
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
            //var user = new User
            //{
            //    UserName = model.Username,
            //    Email = model.Email,
            //    FirstName = model.FirstName,
            //    LastName = model.LastName,
            //    UniqueCitizenNumber = model.UniqueCitizenNumber
            //};

            var user = mapper.Map<User>(model);

            var result = this.userManager.CreateAsync(user, model.Password).Result;
            if (result.Succeeded)
            {
                //if (this.userManager.Users.Count() == 1)
                //{
                //    var roleResult = this.userManager.AddToRoleAsync(user, "Admin").Result;
                //    if (roleResult.Errors.Any())
                //    {
                //        return this.View();
                //    }
                //}
                //else
                //{
                //    var roleResult = this.userManager.AddToRoleAsync(user, "User").Result;
                //    if (roleResult.Errors.Any())
                //    {
                //        return this.View();
                //    }
                //}

                this.signInManager.SignInAsync(user, isPersistent: false);
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
            var result = this.signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: true).Result;
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