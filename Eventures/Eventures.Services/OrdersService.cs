namespace Eventures.Services
{
    using System;
    using System.Linq;
    using Data;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class OrdersService : IOrdersService
    {
        private readonly EventuresDbContext context;
        private readonly IEventsService eventsService;

        public OrdersService(EventuresDbContext context, IEventsService eventsService)
        {
            this.context = context;
            this.eventsService = eventsService;
        }

        public void OrderTickets(int eventId, string userId, int tickets)
        {
            var @event = this.eventsService.GetEventById(eventId);

            var order = new Order
            {
                OrderedOn = DateTime.UtcNow,
                EventId = @event.Id,
                CustomerId = userId,
                TicketsCount = tickets
            };

            this.context.Orders.Add(order);
            this.context.SaveChanges();
        }

        public Order[] GetMyOrders(string userId)
        {
            var orders = this.context
                .Orders
                .Where(o => o.CustomerId == userId)
                .Include(o => o.Event)
                .ToArray();

            return orders;
        }

        public Order[] GetAllOrders()
        {
            var orders = this.context
                .Orders
                .Include(o => o.Event)
                .Include(o => o.Customer)
                .ToArray();

            return orders;
        }
    }
}