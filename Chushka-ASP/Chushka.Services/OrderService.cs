namespace Chushka.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class OrderService : IOrderService
    {
        private readonly ChushkaDbContext context;

        public OrderService(ChushkaDbContext context)
        {
            this.context = context;
        }

        public List<Order> GetAllOrders()
        {
            var orders = context.Orders
                .Where(o => o.IsDeleted == false)
                .Include(o => o.Product)
                .Include(o => o.Client)
                .ToList();

            return orders;
        }
    }
}