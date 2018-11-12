﻿namespace Chushka.Models
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public User()
        {
            this.Orders = new List<Order>();
        }

        public string FullName { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}