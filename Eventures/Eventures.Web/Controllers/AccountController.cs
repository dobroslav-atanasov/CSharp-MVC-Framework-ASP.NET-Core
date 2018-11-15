using Microsoft.AspNetCore.Mvc;

namespace Eventures.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return this.View();
        }
    }
}