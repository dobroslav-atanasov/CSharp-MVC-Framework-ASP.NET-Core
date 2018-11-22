namespace Eventures.Web.ViewModels.Orders
{
    using System;
    using Models;

    public class CreateOrderViewModel
    {
        public DateTime OrderedOn { get; set; }

        public int EventId { get; set; }

        public User Customer { get; set; }

        public int Tickets { get; set; }
    }
}