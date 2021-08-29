using Microsoft.Extensions.Logging;
using System;

namespace SampleDotnet.DDD.Abstractions
{
    public interface ILogEntry
    {
        object AdditionalData { get; set; }
        DateTime Date { get; set; }
        LogLevel LogLevel { get; set; }
        string Message { get; set; }
        object Sender { get; set; }
        public Exception Exception { get; set; }
    }
}