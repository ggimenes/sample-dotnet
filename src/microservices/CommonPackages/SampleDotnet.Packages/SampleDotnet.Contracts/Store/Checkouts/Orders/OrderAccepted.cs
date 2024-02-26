using SampleDotnet.DDD.Abstractions;
using System;
using System.Collections.Generic;

namespace SampleDotnet.Contracts.Store.Checkouts.Orders
{
    public class OrderAccepted : IDomainEvent
    {
        public Guid CorrelationId { get; set; }

        public DateTime CreatedAt { get; set; }
        public Guid CustomerId { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
        public decimal TotalOrder { get; set; }
        public string PaymentMethod { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public DateTime CardExpiration { get; set; }
        public int SecurityCode { get; set; }
        public decimal TotalPayment { get; set; }

        public class OrderItem
        {
            public Guid Id { get; set; }
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal Value { get; set; }
        }
    }
}
