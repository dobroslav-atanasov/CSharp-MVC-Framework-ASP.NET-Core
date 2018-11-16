namespace Eventures.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Events;

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