using SampleDotnet.Contracts.Store.Checkouts.Orders;
using SampleDotnet.DDD.Abstractions;
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

        public async Task<Guid> SubmitOrder(SubmitOrderCommand request)
        {
            var order = _orderBuilder
                .FromCommand(request)
                .Build();

            if (_notificationHandler.Notification.HasErrors)
                return await Task.FromResult(Guid.Empty);

            order.Accept();

            await _orderRepository.Add(order);

            await _notificationHandler.DispatchAndFlush(request.CorrelationId);

            return order.Id;
        }
    }
}
