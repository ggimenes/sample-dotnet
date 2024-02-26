using SampleDotnet.DDD.Abstractions;
using System;

namespace SampleDotnet.Contracts.Shipment
{
    public class ShipmentOrderCreated : IDomainEvent
    {
        public Guid CorrelationId { get; set; }
    }
}
