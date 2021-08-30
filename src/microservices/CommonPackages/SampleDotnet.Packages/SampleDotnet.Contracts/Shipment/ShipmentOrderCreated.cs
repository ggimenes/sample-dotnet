using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Contracts.Shipment
{
    public class ShipmentOrderCreated
    {
        public Guid CorrelationId { get; set; }
    }
}
