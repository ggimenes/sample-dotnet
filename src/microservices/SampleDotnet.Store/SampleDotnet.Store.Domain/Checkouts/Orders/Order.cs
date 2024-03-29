﻿using SampleDotnet.DDD;
using SampleDotnet.DDD.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using SampleDotnet.Repository.Abstractions;

namespace SampleDotnet.Store.Domain.Checkouts.Orders
{
    public class Order : Entity, IDocumentModel
    {
        private List<OrderItem> _items = new List<OrderItem>();

        public BaseNotification Notification { get; set; } = new Notification();

        public IEnumerable<OrderItem> Items { get => _items; private set => _items = value.ToList(); }
        public DateTime CreatedAt { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal Total { get; private set; }
        public Payment Payment { get; set; }
        public int Version { get; set; }

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

            Total += value * quantity;
        }

        public void Accept(Payment payment)
        {
            if (Notification.HasErrors)
                return;

            if (Total != payment.Value)
            {
                Notification.Error("The paid value differs from order total value");
                return;
            }

            Payment = payment;

            Notification.Event(EventFactory.CreateOrderAccepted(this, payment));
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
