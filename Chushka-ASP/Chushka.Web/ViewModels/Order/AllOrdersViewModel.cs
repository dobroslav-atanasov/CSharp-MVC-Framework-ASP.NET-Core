namespace Chushka.Web.ViewModels.Order
{
    using System.Collections.Generic;

    public class AllOrdersViewModel
    {
        public IEnumerable<OrderViewModel> AllOrders { get; set; }
    }
}