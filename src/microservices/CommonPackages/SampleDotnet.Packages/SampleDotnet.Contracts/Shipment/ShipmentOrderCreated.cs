using SampleDotnet.DDD.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Contracts.Shipment
{
    public class ShipmentOrderCreated : IDomainEvent
    {
        public Guid CorrelationId { get; set; }
    }
}
