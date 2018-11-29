namespace Eventures.Web.Controllers
{
    using System.Collections.Generic;
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Interfaces;
    using ViewModels.Accounts;

    public class AccountsController : Controller
    {
        private readonly IAccountsService accountsService;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public AccountsController(IAccountsService accountsService, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            this.accountsService = accountsService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var isRegister = this.accountsService.Register(model.Username, model.Password, model.ConfirmPassword, model.Email,
                model.FirstName, model.LastName, model.UniqueCitizenNumber).GetAwaiter().GetResult();

            if (!isRegister)
            {
                return this.View();
            }

            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var isLogin = this.accountsService.Login(model.Username, model.Password, model.RememberMe);

            if (!isLogin)
            {
                return this.View();
            }

            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            this.accountsService.Logout();
            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult AllUsers()
        {
            var users = this.accountsService.GetAllUsers();
            var userViewModels = this.mapper.Map<List<UserViewModel>>(users);
            this.ViewData["Users"] = userViewModels;
            return this.View();
        }

        [HttpPost]
        public IActionResult PromoteUser(UserIdViewModel model)
        {
            this.accountsService.PromoteUser(model.Id);

            return this.RedirectToAction("AllUsers", "Accounts");
        }


        [HttpPost]
        public IActionResult DemoteUser(UserIdViewModel model)
        {
            this.accountsService.DemoteUser(model.Id);

            return this.RedirectToAction("AllUsers", "Accounts");
        }
    }
}