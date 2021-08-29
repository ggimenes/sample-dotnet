using Microsoft.Extensions.Logging;
using SampleDotnet.DDD.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.DDD
{
    public class LogEntry : ILogEntry
    {
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public object Sender { get; set; }
        public object AdditionalData { get; set; }
        public LogLevel LogLevel { get; set; }
        public Exception Exception { get; set; }
    }
}
