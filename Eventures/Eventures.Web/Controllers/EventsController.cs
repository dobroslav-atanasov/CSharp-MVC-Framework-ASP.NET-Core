namespace Eventures.Web.Controllers
{
    using System.Collections.Generic;
    using AutoMapper;
    using Filters;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Models;
    using Services.Interfaces;
    using ViewModels.Events;

    public class EventsController : Controller
    {
        private readonly IEventsService eventsService;
        private readonly IMapper mapper;
        private readonly ILogger<EventsController> logger;

        public EventsController(IEventsService eventsService, IMapper mapper, ILogger<EventsController> logger)
        {
            this.eventsService = eventsService;
            this.mapper = mapper;
            this.logger = logger;
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
            this.eventsService.CreateEvent(model.Name, model.Place, model.Start, model.End, model.TotalTickets, model.PricePerTicket);
            this.logger.LogInformation($"Event created: {model.Name}", model);
            return this.RedirectToAction("All", "Events");
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult All()
        {
            var events = this.eventsService.GetAllEvents();
            var eventViewModels = this.mapper.Map<Event[], IEnumerable<EventViewModel>>(events);
            return this.View(eventViewModels);
        }
    }
}