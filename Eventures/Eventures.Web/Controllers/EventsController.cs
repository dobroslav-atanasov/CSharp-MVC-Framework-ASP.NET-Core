using Eventures.Web.ViewModels.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventures.Web.Controllers
{
    public class EventsController : Controller
    {
        //[Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateViewModel model)
        {
            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult All()
        {
            return this.View();
        }
    }
}