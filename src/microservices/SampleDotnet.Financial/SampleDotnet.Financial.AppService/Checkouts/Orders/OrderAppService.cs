using SampleDotnet.Contracts.Store.Checkouts.Orders;
using SampleDotnet.DDD.Abstractions;
using SampleDotnet.Financial.AppService.Checkouts.Orders.DTOs;
using SampleDotnet.Financial.Domain.Checkouts.Orders;
using System;
using System.Threading.Tasks;

namespace SampleDotnet.Financial.AppService.Checkouts.Orders
{
    public class OrderAppService : IOrderAppService
    {
        private readonly INotificationHandler _notificationHandler;
        private readonly IOrderBuilder _orderBuilder;
        private readonly IRepository<Order> _orderRepository;

        public OrderAppService(
            INotificationHandler notificationHandler,
            IOrderBuilder orderBuilder,
            IRepository<Order> orderRepository)
        {
            this._notificationHandler = notificationHandler;
            this._orderBuilder = orderBuilder;
            this._orderRepository = orderRepository;
        }

        public async Task<SubmitOrderResponseDTO> SubmitOrder(SubmitOrderCommand request)
        {            
            var order = _orderBuilder
                .FromCommand(request)
                .Build();

            Payment payment = CreatePayment(request);

            _notificationHandler.Notification += order.Notification;
            _notificationHandler.Notification += payment.Notification;
            if (_notificationHandler.Notification.HasErrors)
                return default;

            order.Accept(payment);

            _notificationHandler.Notification += order.Notification;
            if (_notificationHandler.Notification.HasErrors)
                return default;

            await _orderRepository.Add(order);

            // author's remarks: deferring domain events launching only after data has been stored

            var correlationId = await _notificationHandler.DispatchAndFlush();

            return CreateResponse(correlationId, order);
        }

        private SubmitOrderResponseDTO CreateResponse(Guid correlationId, Order order)
        {
            return new SubmitOrderResponseDTO()
            {
                CorrelationId = correlationId,
                OrderId = order.Id
            };
        }

        private Payment CreatePayment(SubmitOrderCommand request)
        {
            return Payment.Create(
                request.Payment.CardName,
                request.Payment.CardNumber,
                request.Payment.Expiration,
                request.Payment.SecurityCode,
                request.Payment.Value);
        }
    }
}
