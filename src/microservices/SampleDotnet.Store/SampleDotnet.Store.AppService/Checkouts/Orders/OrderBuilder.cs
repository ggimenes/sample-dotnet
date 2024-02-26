using SampleDotnet.Contracts.Store.Checkouts.Orders;
using SampleDotnet.DDD;
using SampleDotnet.DDD.Abstractions;
using SampleDotnet.Store.Domain.Checkouts.Orders;
using System;
using System.Linq;

namespace SampleDotnet.Store.AppService.Checkouts.Orders
{
    public class OrderBuilder : IOrderBuilder
    {
        private Order _order;
        private BaseNotification _notification;

        public OrderBuilder(INotification notification)
        {
            _notification = (BaseNotification)notification;
        }
        public OrderBuilder FromCommand(SubmitOrderCommand submitOrderCommand)
        {
            if (submitOrderCommand is null)
                throw new ArgumentNullException(nameof(submitOrderCommand));

            if (submitOrderCommand.Items?.Count() == 0)
            {
                _notification.Error("The field 'Items' is required");
                return this;
            }

            // author's remarks: Dto to domain maps when being more strict with DDD rules usually can't be done
            // using AutoMapper, otherwise the domain will suffer with lack of semantical methods and lack of immutability constraints

            var firstItem = submitOrderCommand.Items.First();
            _order = Order.CreateOrder(
                submitOrderCommand.CreatedAt,
                submitOrderCommand.CustomerId,
                firstItem.ProductId,
                firstItem.Quantity,
                firstItem.Value);

            submitOrderCommand.Items
                .Skip(1)
                .ToList()
                .ForEach(x => _order.AddItem(
                    x.ProductId,
                    x.Quantity,
                    x.Value));

            _notification += _order.Notification;

            return this;
        }

        public Order Build()
        {
            return _order;
        }
    }
}
