using Ecommerce.Api.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrderAsync(int customerId);
    }
}
