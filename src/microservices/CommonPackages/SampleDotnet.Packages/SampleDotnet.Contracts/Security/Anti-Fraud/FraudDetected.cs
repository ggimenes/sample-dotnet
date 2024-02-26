using SampleDotnet.DDD.Abstractions;
using System;

namespace SampleDotnet.Contracts.Security.Anti_Fraud
{
    public class FraudDetected : IDomainEvent
    {
        public Guid CorrelationId { get; set; }
    }
}
