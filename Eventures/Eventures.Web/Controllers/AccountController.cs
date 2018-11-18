namespace Eventures.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using ViewModels.Account;

    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            var isRegister = this.accountService.Register(model.Username, model.Password, model.ConfirmPassword, model.Email,
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
            var isLogin = this.accountService.Login(model.Username, model.Password, model.RememberMe);

            if (!isLogin)
            {
                return this.View();
            }

            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            this.accountService.Logout();
            return this.RedirectToAction("Index", "Home");
        }
    }
}