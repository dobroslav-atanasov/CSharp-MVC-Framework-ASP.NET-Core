namespace Eventures.Services.Interfaces
{
    using Models;

    public interface IOrdersService
    {
        void OrderTickets(int eventId, string username, int tickets);

        Order[] GetMyEvents(string userId);
    }
}