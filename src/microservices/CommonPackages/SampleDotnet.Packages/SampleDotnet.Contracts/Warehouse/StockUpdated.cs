using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Contracts.Warehouse
{
    public class StockUpdated
    {
        public Guid CorrelationId { get; set; }
    }
}
