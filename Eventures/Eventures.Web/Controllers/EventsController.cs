namespace Eventures.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
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
    using X.PagedList;

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
        public IActionResult All(int? page)
        {
            var events = this.eventsService.GetAllEvents().OrderBy(x => x.Name);

            var viewModels = new List<EventViewModel>();
            foreach (var @event in events)
            {
                var viewModel = this.mapper.Map<EventViewModel>(@event);
                viewModels.Add(viewModel);
            }

            var pageNumber = page ?? 1;
            var itemsOnPage = viewModels.ToPagedList(pageNumber, 1);

            this.ViewBag.ViewModels = itemsOnPage;

            return this.View();
        }
        
        [Authorize]
        public IActionResult MyEvents()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var orders = this.ordersService.GetMyOrders(userId);
            var ordersViewModel = this.mapper.Map<Order[], IEnumerable<MyOrderViewModel>>(orders);
            return this.View(ordersViewModel);
        }
    }
}