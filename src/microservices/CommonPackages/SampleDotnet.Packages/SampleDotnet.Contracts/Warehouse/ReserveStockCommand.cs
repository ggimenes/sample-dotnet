using System;

namespace SampleDotnet.Contracts.Warehouse
{
    public class ReserveStockCommand
    {
        public Guid CorrelationId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
