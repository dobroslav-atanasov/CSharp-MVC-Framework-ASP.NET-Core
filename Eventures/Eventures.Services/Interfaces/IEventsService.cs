namespace Eventures.Services.Interfaces
{
    using System;
    using Models;

    public interface IEventsService
    {
        void CreateEvent(string name, string place, DateTime start, DateTime end, int totalTickets, decimal pricePerTicket);

        Event[] GetAllEvents();
    }
}