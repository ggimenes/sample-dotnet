using Automatonymous;
using System;

namespace SampleDotnet.Store.Workflows.Checkouts.Orders
{
    public class OrderState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public byte[] RowVersion { get; set; }
    }
}