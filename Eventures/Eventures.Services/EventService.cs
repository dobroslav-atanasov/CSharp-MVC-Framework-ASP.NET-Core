namespace Eventures.Services
{
    using System;
    using Data;
    using Interfaces;
    using Models;

    public class EventService : IEventsService
    {
        private readonly EventuresDbContext context;

        public EventService(EventuresDbContext context)
        {
            this.context = context;
        }

        public Event CreateEvent(string name, string place, DateTime start, DateTime end, int totalTickets, decimal pricePerTicket)
        {
            var @event = new Event
            {
                Name = name,
                Place = place,
                Start = start,
                End = end,
                TotalTickets = totalTickets,
                PricePerTicket = pricePerTicket
            };

            this.context.Events.Add(@event);
            this.context.SaveChanges();

            return @event;
        }
    }
}