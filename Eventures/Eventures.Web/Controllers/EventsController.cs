namespace Eventures.Web.Controllers
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using AutoMapper;
    using Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models;
    using Services.Interfaces;
    using ViewModels.Events;
    using ViewModels.Orders;


    public class EventsController : Controller
    {
        private readonly IEventsService eventsService;
        private readonly IMapper mapper;
        private readonly ILogger<EventsController> logger;
        private readonly IOrdersService ordersService;

        public EventsController(IEventsService eventsService, IMapper mapper, ILogger<EventsController> logger, IOrdersService ordersService)
        {
            this.eventsService = eventsService;
            this.mapper = mapper;
            this.logger = logger;
            this.ordersService = ordersService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return this.View();
        }

        [ServiceFilter(typeof(EventsLogActionFilter))]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CreateViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            this.eventsService.CreateEvent(model.Name, model.Place, model.Start, model.End, model.TotalTickets, model.PricePerTicket);
            this.logger.LogInformation($"Event created: {model.Name}", model);
            return this.RedirectToAction("All", "Events");
        }

        [Authorize]
        public IActionResult All()
        {
            var events = this.eventsService.GetAllEvents();
            var eventViewModels = this.mapper.Map<Event[], IEnumerable<EventViewModel>>(events);
            this.ViewData["Events"] = eventViewModels;
            return this.View();
        }
        
        [Authorize]
        [HttpPost]
        public IActionResult OrderTickets(CreateOrderViewModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            this.ordersService.OrderTickets(model.EventId, userId, model.Tickets);
            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult MyEvents()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orders = this.ordersService.GetMyOrders(userId);
            var ordersViewModel = this.mapper.Map<Order[], IEnumerable<MyOrderViewModel>>(orders);
            return this.View(ordersViewModel);
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