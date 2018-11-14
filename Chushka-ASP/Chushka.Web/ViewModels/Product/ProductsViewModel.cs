namespace Chushka.Web.ViewModels.Product
{
    using System.Collections.Generic;

    public class ProductsViewModel
    {
        public int Count { get; set; }

        public int Rows => this.Count % 5;

        public IEnumerable<DisplayProductViewModel> Products { get; set; }
    }
}