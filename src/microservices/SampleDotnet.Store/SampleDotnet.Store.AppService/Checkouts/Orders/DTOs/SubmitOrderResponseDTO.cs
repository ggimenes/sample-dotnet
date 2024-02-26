using System;

namespace SampleDotnet.Store.AppService.Checkouts.Orders.DTOs
{
    public class SubmitOrderResponseDTO
    {
        public Guid CorrelationId { get; set; }
        public Guid OrderId { get; set; }
    }
}
