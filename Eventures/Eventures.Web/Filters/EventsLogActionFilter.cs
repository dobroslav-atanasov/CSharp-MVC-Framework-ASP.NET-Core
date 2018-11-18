namespace Eventures.Web.Filters
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using ViewModels.Events;

    public class EventsLogActionFilter : ActionFilterAttribute
    {
        private readonly ILogger logger;
        private CreateViewModel model;

        public EventsLogActionFilter(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<EventsLogActionFilter>();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this.model = context.ActionArguments.Values.OfType<CreateViewModel>().Single();

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (this.model != null)
            {
                var user = context.HttpContext.User.Identity.Name;
                var eventName = this.model.Name;
                var start = this.model.Start;
                var end = this.model.End;
                this.logger.LogInformation($"[{DateTime.UtcNow}] Administrator {user} create event {eventName} ({start} / {end}).");
            }

            base.OnActionExecuted(context);
        }
    }
}