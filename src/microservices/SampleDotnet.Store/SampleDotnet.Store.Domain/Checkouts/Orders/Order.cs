using SampleDotnet.DDD;
using SampleDotnet.DDD.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Store.Domain.Checkouts.Orders
{
    public class Order : Entity
    {
        private List<OrderItem> _items;

        public Notification Notification { get; set; } = new Notification();

        public IEnumerable<OrderItem> Items { get => _items; private set => _items = value.ToList(); }
        public DateTime CreatedAt { get; private set; }
        public Guid CustomerId { get; private set; }

        private Order()
        {

        }

        public static Order CreateOrder(DateTime createdAt, Guid customerId, Guid productId, int quantity, decimal value)
        {
            var order = new Order();
            order.CreatedAt = createdAt;
            order.CustomerId = customerId;

            order.Validate();

            order.AddItem(productId, quantity, value);

            return order;
        }

        public void AddItem(Guid productId, int quantity, decimal value)
        {
            var item = new OrderItem(productId, quantity, value);
            Notification += item.Notification;
            _items.Add(item);
        }

        public void Accept()
        {
            if (!Notification.HasErrors)
                Notification.Event(EventFactory.CreateOrderAccepted(this));
        }

        private void Validate()
        {
            if (CreatedAt == new DateTime())
                Notification.Error("The field 'CreatedAt' is required");

            if (CustomerId == Guid.Empty)
                Notification.Error("The field 'CustomerId' is required");
        }
    }
}
