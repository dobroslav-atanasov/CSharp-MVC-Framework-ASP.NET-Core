namespace Eventures.Services
{
    using System;
    using System.Linq;
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

        public void CreateEvent(string name, string place, DateTime start, DateTime end, int totalTickets, decimal pricePerTicket)
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
        }

        public Event[] GetAllEvents()
        {
            var events = this.context.Events.ToArray();

            return events;
        }
    }
}