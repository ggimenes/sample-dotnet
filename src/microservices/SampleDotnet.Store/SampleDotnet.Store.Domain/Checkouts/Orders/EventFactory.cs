using SampleDotnet.Contracts.Store.Checkouts.Orders;
using SampleDotnet.DDD.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleDotnet.Store.Domain.Checkouts.Orders
{
    public static class EventFactory
    {
        public static OrderAccepted CreateOrderAccepted(Order order)
        {
            return new OrderAccepted
            {
                Items = order.Items.Select(orderItem => new OrderAccepted.OrderItem
                {
                    Id = orderItem.Id,
                    ProductId = orderItem.ProductId,
                    Quantity = orderItem.Quantity,
                    Value = orderItem.Value
                }),
                CreatedAt = order.CreatedAt,
                CustomerId = order.CustomerId
            };
        }
    }
}
