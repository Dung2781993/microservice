﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Db
{
    //Using for DB access
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}
