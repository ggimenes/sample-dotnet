using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Financial.AppService.Payments.DTOs
{
    public class SubmitPaymentResponseDTO
    {
        public Guid CorrelationId { get; set; }
        public Guid OrderId { get; set; }
    }
}
