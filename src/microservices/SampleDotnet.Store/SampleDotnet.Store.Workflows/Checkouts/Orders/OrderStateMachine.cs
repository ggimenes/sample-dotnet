using Automatonymous;
using MassTransit.Saga;
using Microsoft.Extensions.Logging;
using SampleDotnet.Contracts.Financial.Payments;
using SampleDotnet.Contracts.Security.Anti_Fraud;
using SampleDotnet.Contracts.Shipment;
using SampleDotnet.Contracts.Store.Checkouts.Orders;
using SampleDotnet.Contracts.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Store.Workflows.Checkouts.Orders
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        private readonly ILogger<OrderStateMachine> _logger;

        // author's remarks: Workflow management. Could be replaced by AWS Step Functions,
        // Netflix Conductor, Uber Cadence, Camunda Zeebe, Azure Logic Apps, and so on.

        public OrderStateMachine(ILogger<OrderStateMachine> logger)
        {
            this._logger = logger;

            Event(() => OrderAccepted, x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => PaymentAccepted, x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => PaymentApproved, x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => RefundApplyed, x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => PaymentCanceled, x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => FraudDetected, x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => PaymentValidated, x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => ShipmentOrderCreated, x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => StockReserved, x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => StockReleased, x => x.CorrelateById(m => m.Message.CorrelationId));
            Event(() => StockUpdated, x => x.CorrelateById(m => m.Message.CorrelationId));

            InstanceState(x => x.CurrentState);

            Initially(
               When(OrderAccepted)
                   .Then(x => LogEvent(x))
                   .Then(x => x.Instance.CreatedAt = DateTime.UtcNow)
                   .Activity(x => x.OfType<OrderAcceptedActivity>())
                   .TransitionTo(Started));

            CompositeEvent(() => PaymentCompleted, x => x.PaymentPhaseEventReady,
                PaymentApproved,
                PaymentValidated,
                StockReserved);

            During(Started,
                When(PaymentCompleted)
                   .Then(x => LogEvent(x))
                   .TransitionTo(PaymentProcessed));

            CompositeEvent(() => WorkflowCompleted, x => x.WorkflowPhaseEventReady,
                ShipmentOrderCreated,
                StockUpdated);

            During(PaymentProcessed,
                When(WorkflowCompleted)
                   .Then(x => LogEvent(x))
                   .Finalize());

            // error flow
            During(Started,
                When(FraudDetected)
                   .Then(x => LogEvent(x))
                   .TransitionTo(AbortPaymentStarted));

            CompositeEvent(() => PaymentAborted, x => x.PaymentAbortEventReady,
                RefundApplyed,
                StockReleased);

            During(AbortPaymentStarted,
                When(PaymentAborted)
                   .Then(x => LogEvent(x))
                   .TransitionTo(CancelPaymentStarted));

            During(CancelPaymentStarted,
                When(PaymentCanceled)
                   .Then(x => LogEvent(x))
                   .Finalize());            
        }       

        public State Started { get; private set; }
        public State PaymentProcessed { get; private set; }
        public State AbortPaymentStarted { get; private set; }
        public State CancelPaymentStarted { get; private set; }

        public Event<OrderAccepted> OrderAccepted { get; private set; }
        public Event<PaymentAccepted> PaymentAccepted { get; private set; }
        public Event<PaymentApproved> PaymentApproved { get; private set; }
        public Event<RefundApplyed> RefundApplyed { get; private set; }
        public Event<PaymentCanceled> PaymentCanceled { get; private set; }
        public Event<FraudDetected> FraudDetected { get; private set; }
        public Event<PaymentValidated> PaymentValidated { get; private set; }
        public Event<ShipmentOrderCreated> ShipmentOrderCreated { get; private set; }
        public Event<StockReserved> StockReserved { get; private set; }
        public Event<StockReleased> StockReleased { get; private set; }
        public Event<StockUpdated> StockUpdated { get; private set; }
        public Event PaymentCompleted { get; private set; }
        public Event WorkflowCompleted { get; private set; }
        public Event PaymentAborted { get; private set; }

        private void LogEvent<TInstance>(BehaviorContext<TInstance> x) where TInstance : SagaStateMachineInstance
        {
            _logger.LogInformation($"CorrelationId: {x.Instance.CorrelationId} - Event {x.Event.Name} received");
        }

        private void LogEvent<TInstance, TData>(BehaviorContext<TInstance, TData> x) where TInstance : SagaStateMachineInstance
        {
            _logger.LogInformation($"CorrelationId: {x.Instance.CorrelationId} - Event {x.Event.Name} received");
        }
    }
}
