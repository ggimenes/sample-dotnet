using System;

namespace SampleDotnet.Contracts.Financial.Payments
{
    public class RequestRefundCommand
    {
        public Guid CorrelationId { get; set; }
        public Guid PaymentId { get; set; }
    }
}
