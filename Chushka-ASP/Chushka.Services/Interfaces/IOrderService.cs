namespace Chushka.Services.Contracts
{
    using System.Collections.Generic;
    using Data;
    using Models;

    public interface IOrderService
    {
        List<Order> GetAllOrders();
    }
}