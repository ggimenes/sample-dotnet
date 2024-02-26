using System;

namespace SampleDotnet.Contracts.Warehouse
{
    public class StockUpdated
    {
        public Guid CorrelationId { get; set; }
    }
}
