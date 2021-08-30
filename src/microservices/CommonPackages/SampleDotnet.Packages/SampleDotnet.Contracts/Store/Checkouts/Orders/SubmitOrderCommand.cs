using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Contracts.Store.Checkouts.Orders
{
    public class SubmitOrderCommand
    {
        public DateTime CreatedAt { get; set; }
        public Guid CustomerId { get; set; }
        public IEnumerable<OrderItem> Items { get; set; }
        public SubmitOrderCommand.PaymentInfo Payment { get; set; }

        public class OrderItem
        {
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal Value { get; set; }
        }

        public class PaymentInfo
        {
            public string PaymentMethod { get; set; }
            public string CardName { get; set; }
            public string CardNumber { get; set; }
            public DateTime Expiration { get; set; }
            public int SecurityCode { get; set; }
            public decimal Value { get; set; }
        }
    }
}
