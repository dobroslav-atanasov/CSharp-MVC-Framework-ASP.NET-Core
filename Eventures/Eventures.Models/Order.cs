namespace Eventures.Models
{
    using System;

    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderedOn { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public string CustomerId { get; set; }
        public User Customer { get; set; }

        public int TicketsCount { get; set; }
    }
}