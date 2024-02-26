using Automatonymous;
using MassTransit;
using MassTransit.Saga;
using System;

namespace SampleDotnet.Store.Workflows.Checkouts.Orders
{
    public class OrderState :
        SagaStateMachineInstance,
        ISagaVersion
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public int Version { get; set; }
        public int PaymentPhaseEventReady { get; set; }
        public int WorkflowPhaseEventReady { get; set; }
        public int PaymentAbortEventReady { get; set; }
    }
}