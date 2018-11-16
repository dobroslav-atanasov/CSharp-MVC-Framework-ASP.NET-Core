namespace Eventures.Services.Interfaces
{
    using System;
    using Models;

    public interface IEventsService
    {
        Event CreateEvent(string name, string place, DateTime start, DateTime end, int totalTickets, decimal pricePerTicket);
    }
}