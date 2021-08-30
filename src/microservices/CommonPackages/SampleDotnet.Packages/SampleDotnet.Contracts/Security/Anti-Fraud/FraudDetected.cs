using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Contracts.Security.Anti_Fraud
{
    public class FraudDetected
    {
        public Guid CorrelationId { get; set; }
    }
}
