using System;

namespace SampleDotnet.DDD.Abstractions
{
    public class DomainEvent
    {
        Guid CorrelationId { get; set; }
    }
}
