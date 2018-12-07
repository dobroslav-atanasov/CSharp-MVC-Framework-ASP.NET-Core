namespace Eventures.Tests
{
    using System;
    using System.Linq;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Services;
    using Xunit;

    public class EventServiceTests
    {
        [Fact]
        public void CreateEventShouldReturnsCorrectCountUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<EventuresDbContext>()
                .UseInMemoryDatabase("Eventures_Database_1")
                .Options;

            var context = new EventuresDbContext(options);

            var service = new EventService(context);

            service.CreateEvent("Event", "Sofia", DateTime.UtcNow, DateTime.UtcNow, 100, 25.5M);

            var count = context.Events.Count();
            Assert.Equal(1, count);
        }

        [Fact]
        public void GetAllEventsShouldReturnsCorrectCountUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<EventuresDbContext>()
                .UseInMemoryDatabase("Eventures_Database_2")
                .Options;

            var context = new EventuresDbContext(options);
                       
            var service = new EventService(context);
            service.CreateEvent("Event", "Sofia", DateTime.UtcNow, DateTime.UtcNow, 100, 25.5M);

            var events = service.GetAllEvents();

            Assert.Equal(1, events.Count());
        }
        
        [Fact]
        public void GetEventByIdShouldReturnsCorrectEventUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<EventuresDbContext>()
                .UseInMemoryDatabase("Eventures_Database_3")
                .Options;

            var context = new EventuresDbContext(options);

            var service = new EventService(context);
            var eventInDb = new Event
            {
                Name = "Event",
                Place = "Sofia",
                Start = DateTime.UtcNow,
                End = DateTime.UtcNow,
                TotalTickets = 100,
                PricePerTicket = 25.5M
            };

            context.Events.Add(eventInDb);
            context.SaveChanges();
            var @event = service.GetEventById(3);

            Assert.Equal(3, @event.Id);
        }


        [Fact]
        public void GetTotalTicketsByEventShouldReturnsCorrectTotalTicketsUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<EventuresDbContext>()
                .UseInMemoryDatabase("Eventures_Database_4")
                .Options;

            var context = new EventuresDbContext(options);

            var service = new EventService(context);
            var eventInDb = new Event
            {
                Name = "Event",
                Place = "Sofia",
                Start = DateTime.UtcNow,
                End = DateTime.UtcNow,
                TotalTickets = 100,
                PricePerTicket = 25.5M
            };

            context.Events.Add(eventInDb);
            context.SaveChanges();

            var totalTickets = service.GetTotalTicketsByEvent(1);

            Assert.Equal(100, totalTickets);
        }
    }
}