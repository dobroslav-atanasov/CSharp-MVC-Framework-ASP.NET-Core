namespace Fdmc.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using ViewModels.CatViewModels;

    public class CatController : Controller
    {
        private readonly ICatService catService;

        public CatController(ICatService catService)
        {
            this.catService = catService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(CreateCatViewModel model)
        {
            if (this.catService.IsCatExists(model.Name))
            {
                return this.View();
            }

            this.catService.AddCat(model);
            return this.Redirect("/");
        }

        public IActionResult Details(int id)
        {
            var cat = this.catService.GetCat(id);

            return this.View(cat);
        }
    }
}