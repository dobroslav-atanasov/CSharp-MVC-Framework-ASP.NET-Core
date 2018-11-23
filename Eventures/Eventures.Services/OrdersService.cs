namespace Eventures.Services
{
    using System;
    using Data;
    using Interfaces;
    using Microsoft.AspNetCore.Identity;
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
    }
}