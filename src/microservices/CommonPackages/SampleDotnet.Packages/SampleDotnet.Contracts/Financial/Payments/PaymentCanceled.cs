using SampleDotnet.DDD.Abstractions;
using System;

namespace SampleDotnet.Contracts.Financial.Payments
{
    public class PaymentCanceled : IDomainEvent
    {
        public Guid CorrelationId { get; set; }
        public Guid PaymentId { get; set; }
    }
}
