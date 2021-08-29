using SampleDotnet.DDD;
using SampleDotnet.DDD.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Store.Domain.Checkouts.Orders
{
    public class OrderItem
    {
        public INotification Notification { get; set; } = new Notification();
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Value { get; private set; }

        public OrderItem(Guid productId, int quantity, decimal value)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
            Value = value;

            Validate();
        }

        private void Validate()
        {
            // author's remarks: could be replaced by a fluent validation api
            if (Quantity <= 0)
                Notification.Error("The field 'Quantity' must be greater than 0");

            if (Value <= 0)
                Notification.Error("The field 'Value' must be greater than 0");

            if (ProductId == Guid.Empty)
                Notification.Error("The field 'ProductId' is required");
        }
    }
}
