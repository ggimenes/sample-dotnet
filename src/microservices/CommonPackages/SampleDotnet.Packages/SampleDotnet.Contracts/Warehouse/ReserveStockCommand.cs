using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Contracts.Warehouse
{
    public class ReserveStockCommand
    {
        public Guid CorrelationId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
