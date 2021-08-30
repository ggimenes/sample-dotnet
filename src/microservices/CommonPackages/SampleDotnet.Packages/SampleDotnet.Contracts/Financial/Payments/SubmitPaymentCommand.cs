using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Contracts.Financial.Payments
{
    public class SubmitPaymentCommand
    {
        public Guid CorrelationId { get; set; }

        public string PaymentMethod { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public DateTime Expiration { get; set; }
        public int SecurityCode { get; set; }
        public decimal Value { get; set; }
    }
}
