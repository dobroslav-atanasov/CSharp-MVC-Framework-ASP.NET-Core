namespace Fdmc.Controllers
{
    using System.Linq;
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.CatViewModels;

    public class HomeController : Controller
    {
        private readonly FdmcDbContext context;

        public HomeController(FdmcDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var allCats = new AllCatsViewModel
            {
                Cats = this.context.Cats.ToList()
            };

            return this.View(allCats);
        }
    }
}