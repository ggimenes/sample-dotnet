﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Contracts.Store.Checkouts.Orders
{
    public class SubmitOrderCommand
    {
        public Guid CorrelationId { get; set; } = Guid.NewGuid();

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