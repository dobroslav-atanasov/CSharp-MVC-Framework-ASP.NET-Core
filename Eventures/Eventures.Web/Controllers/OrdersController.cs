namespace Eventures.Web.Controllers
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Interfaces;
    using ViewModels.Orders;

    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;
        private readonly IMapper mapper;

        public OrdersController(IOrdersService ordersService, IMapper mapper)
        {
            this.ordersService = ordersService;
            this.mapper = mapper;
        }
        
        [Authorize]
        [HttpPost]
        public IActionResult OrderTickets(CreateOrderViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("All", "Events", model);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            this.ordersService.OrderTickets(model.EventId, userId, model.Tickets);
            return this.RedirectToAction("Index", "Home");
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult AllOrders()
        {
            var orders = this.ordersService.GetAllOrders();
            var ordersViewModel = this.mapper.Map<Order[], IEnumerable<AllOrdersViewModel>>(orders);
            return this.View(ordersViewModel);
        }
    }
}