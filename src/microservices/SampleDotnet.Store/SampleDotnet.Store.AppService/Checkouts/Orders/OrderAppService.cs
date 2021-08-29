using SampleDotnet.Contracts.Store.Checkouts.Orders;
using SampleDotnet.DDD.Abstractions;
using SampleDotnet.Store.AppService.Checkouts.Orders.DTOs;
using SampleDotnet.Store.Domain.Checkouts.Orders;
using System;
using System.Threading.Tasks;

namespace SampleDotnet.Store.AppService.Checkouts.Orders
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

            await _notificationHandler.DispatchAndFlush(request.CorrelationId);

            return CreateResponse(request, order);
        }

        private SubmitOrderResponseDTO CreateResponse(SubmitOrderCommand request, Order order)
        {
            return new SubmitOrderResponseDTO()
            {
                CorrelationId = request.CorrelationId,
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
