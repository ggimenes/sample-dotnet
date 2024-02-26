using MassTransit;
using Microsoft.Extensions.Logging;
using SampleDotnet.Contracts.Financial.Payments;
using SampleDotnet.Contracts.Security.Anti_Fraud;
using SampleDotnet.Contracts.Store.Checkouts.Orders;
using SampleDotnet.Contracts.Warehouse;
using System;
using System.Threading.Tasks;

namespace SampleDotnet.Store.Workflows.Checkouts.Orders
{
    public class OrderAcceptedActivity : IStateMachineActivity<OrderState, OrderAccepted>
    {
        private readonly ILogger<OrderAcceptedActivity> _logger;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public OrderAcceptedActivity(ILogger<OrderAcceptedActivity> logger, ISendEndpointProvider sendEndpointProvider)
        {
            _logger = logger;
            this._sendEndpointProvider = sendEndpointProvider;
        }

        public void Accept(StateMachineVisitor visitor)
        {
            visitor.Visit(this);
        }

        public async Task Execute(BehaviorContext<OrderState, OrderAccepted> context, IBehavior<OrderState, OrderAccepted> next)
        {
            var endpointSubmitPayment = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:submit-payment"));
            var endpointValidateFraud = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:validate-fraud"));
            var endpointReserveStock = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:reserve-stock"));

            await endpointSubmitPayment.Send(new SubmitPaymentCommand { CorrelationId = context.Instance.CorrelationId });
            await endpointValidateFraud.Send(new ValidateFraudCommand { CorrelationId = context.Instance.CorrelationId });
            await endpointReserveStock.Send(new ReserveStockCommand { CorrelationId = context.Instance.CorrelationId });

            _logger.LogInformation($"CorrelationId: {context.Instance.CorrelationId} - Commands SubmitPaymentCommand, ValidateFraudCommand and ReserveStockCommand has been sent.");
            
            await next.Execute(context).ConfigureAwait(false);
        }      

        public Task Faulted<TException>(BehaviorExceptionContext<OrderState, OrderAccepted, TException> context, IBehavior<OrderState, OrderAccepted> next) where TException : Exception
        {
            return next.Faulted(context);
        }

        public void Probe(ProbeContext context)
        {
            context.CreateScope("order-accepted-activity");
        }
    }
}
