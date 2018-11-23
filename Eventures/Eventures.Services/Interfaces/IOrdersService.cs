namespace Eventures.Services.Interfaces
{
    public interface IOrdersService
    {
        void OrderTickets(int eventId, string username, int tickets);
    }
}