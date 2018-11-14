namespace Chushka.Models
{
    using System.Collections.Generic;
    using Enums;

    public class Product
    {
        public Product()
        {
            this.Orders = new List<Order>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public ProductType Type { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<Order> Orders { get; set; }
    }
}