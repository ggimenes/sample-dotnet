using SampleDotnet.Contracts.Store.Checkouts.Orders;
using System.Linq;

namespace SampleDotnet.Store.Domain.Checkouts.Orders
{
    public static class EventFactory
    {
        public static OrderAccepted CreateOrderAccepted(Order order, Payment payment)
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
                CustomerId = order.CustomerId,
                PaymentMethod = payment.PaymentMethod.ToString(),
                CardName = payment.CardName,
                CardNumber = payment.CardNumber,
                CardExpiration = payment.Expiration,
                SecurityCode = payment.SecurityCode
            };
        }
    }
}
