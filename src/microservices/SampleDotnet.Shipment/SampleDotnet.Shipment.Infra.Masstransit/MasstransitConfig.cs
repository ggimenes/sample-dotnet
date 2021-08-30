using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Shipment.Infra.Masstransit
{
    public class MasstransitConfig
    {
        public OrderStateMachineConfig OrderStateMachine { get; set; }

        public class OrderStateMachineConfig
        {
            public int PrefetchCount { get; set; }
            public int PartitionCount { get; set; }
            public int ConcurrentMessageLimit { get; set; }
        }
    }

}
