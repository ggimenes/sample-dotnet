using SampleDotnet.Contracts.Store.Checkouts.Orders;
using SampleDotnet.DDD.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleDotnet.Contracts.Store.Checkouts.Orders
{
    public class OrderAccepted : IDomainEvent
    {
        public Guid CorrelationId { get; set; }

        public DateTime CreatedAt { get; set; }
        public Guid CustomerId { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }

        public class OrderItem
        {
            public Guid Id { get; set; }
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal Value { get; set; }
        }
    }
}
