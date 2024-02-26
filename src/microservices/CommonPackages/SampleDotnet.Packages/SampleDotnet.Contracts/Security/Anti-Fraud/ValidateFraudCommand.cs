using SampleDotnet.DDD.Abstractions;
using System;

namespace SampleDotnet.Contracts.Security.Anti_Fraud
{
    public class ValidateFraudCommand : IDomainEvent
    {
        public Guid CorrelationId { get; set; }
        public string CardNumber { get; set; }
    }
}
