using SampleDotnet.DDD.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Contracts.Financial.Payments
{
    public class PaymentCanceled : IDomainEvent
    {
        public Guid CorrelationId { get; set; }
        public Guid PaymentId { get; set; }
    }
}
