using System;
using System.Collections;
using System.Collections.Generic;

namespace SampleDotnet.DDD.Abstractions
{
    public interface INotification
    {
        IEnumerable<IDomainEvent> Events { get; }
        IEnumerable<ILogEntry> Logs { get; }
        bool HasErrors { get; }
        void Error(string message);
        void Event(IDomainEvent domainEvent);
        void LogError(object sender, string message, object additionalData = null);
        void LogError(object sender, string message, Exception exception, object additionalData = null);
        void LogWarning(object sender, string message, object additionalData = null);
        void LogInfo(object sender, string message, object additionalData = null);
        void LogDebug(object sender, string message, object additionalData = null);
        void LogTrace(object sender, string message, object additionalData = null);
        string GetValidationErrorsFormatted();
        IEnumerable<string> GetValidationErrors();
        INotification Combine(INotification b);        
    }
}
