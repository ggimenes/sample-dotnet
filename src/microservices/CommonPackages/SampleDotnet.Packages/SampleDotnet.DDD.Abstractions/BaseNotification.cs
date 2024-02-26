using System;
using System.Collections.Generic;

namespace SampleDotnet.DDD.Abstractions
{
    public abstract class BaseNotification : INotification
    {
        public abstract IEnumerable<IDomainEvent> Events { get; }
        public abstract IEnumerable<ILogEntry> Logs { get; }
        public abstract bool HasErrors { get; }

        public abstract INotification Combine(INotification b);
        public abstract void Error(string message);
        public abstract void Event(IDomainEvent domainEvent);
        public abstract IEnumerable<string> GetValidationErrors();
        public abstract string GetValidationErrorsFormatted();
        public abstract void LogDebug(object sender, string message, object additionalData = null);
        public abstract void LogError(object sender, string message, object additionalData = null);
        public abstract void LogError(object sender, string message, Exception exception, object additionalData = null);
        public abstract void LogInfo(object sender, string message, object additionalData = null);
        public abstract void LogTrace(object sender, string message, object additionalData = null);
        public abstract void LogWarning(object sender, string message, object additionalData = null);

        public static BaseNotification operator +(BaseNotification a, BaseNotification b)
        {
            return (BaseNotification)a.Combine(b);
        }
    }
}
