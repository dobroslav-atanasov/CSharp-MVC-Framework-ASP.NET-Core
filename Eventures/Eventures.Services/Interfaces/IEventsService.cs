using Eventures.Models;
using System;

namespace Eventures.Services.Interfaces
{
    public interface IEventsService
    {
        Event CreateEvent(string name, string place, DateTime start, DateTime end, int totalTickets, decimal pricePerTicket);
    }
}
