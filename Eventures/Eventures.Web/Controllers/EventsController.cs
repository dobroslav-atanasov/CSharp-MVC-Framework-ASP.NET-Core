namespace Eventures.Web.Controllers
{
    using System.Collections.Generic;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Interfaces;
    using ViewModels.Events;

    public class EventsController : Controller
    {
        private readonly IEventsService eventsService;
        private readonly IMapper mapper;

        public EventsController(IEventsService eventsService, IMapper mapper)
        {
            this.eventsService = eventsService;
            this.mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CreateViewModel model)
        {
            this.eventsService.CreateEvent(model.Name, model.Place, model.Start, model.End, model.TotalTickets, model.PricePerTicket);
            return this.RedirectToAction("Index", "Home");
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