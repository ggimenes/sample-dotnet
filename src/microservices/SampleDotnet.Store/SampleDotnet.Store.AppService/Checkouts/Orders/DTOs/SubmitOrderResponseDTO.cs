using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Store.AppService.Checkouts.Orders.DTOs
{
    public class SubmitOrderResponseDTO
    {
        public Guid CorrelationId { get; set; }
        public Guid OrderId { get; set; }
    }
}
