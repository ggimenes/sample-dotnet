using System;

namespace SampleDotnet.DDD.Abstractions
{
    public interface IDomainEvent
    {
        Guid CorrelationId { get; set; }
    }
}
