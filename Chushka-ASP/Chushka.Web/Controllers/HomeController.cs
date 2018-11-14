namespace Chushka.Web.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using Chushka.Models;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Contracts;
    using ViewModels.Product;

    public class HomeController : Controller
    {
        private readonly IProductService productService;

        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult Index()
        {
            var products = this.productService.GetAllProducts();
            var displayProducts = new ProductsViewModel
            {
                Count = products.Count,
                Products = this.ExtractDisplayProducts(products)
            };
            return View(displayProducts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        private List<DisplayProductViewModel> ExtractDisplayProducts(List<Product> products)
        {
            var list = new List<DisplayProductViewModel>();
            foreach (var product in products)
            {
                var model = new DisplayProductViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description
                };

                if (model.Description.Length > 50)
                {
                    model.Description = $"{model.Description.Substring(0, 50)}...";
                }

                list.Add(model);
            }

            return list;
        }
    }
}