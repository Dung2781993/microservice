﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider ordersProvider;

        public OrdersController(IOrdersProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrderAsync(int customerId)
        {
            var result = await ordersProvider.GetOrderAsync(customerId);
            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            }
            return NotFound();
        }
    }
}