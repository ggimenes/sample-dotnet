using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SampleDotnet.DDD.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.DDD
{
    public class Notification : INotification
    {
        private readonly List<IDomainEvent> _events = new List<IDomainEvent>();
        private readonly List<LogEntry> _logs = new List<LogEntry>();

        public IEnumerable<IDomainEvent> Events => _events;
        public IEnumerable<ILogEntry> Logs => _logs;
        public ValidationResult ValidationResult => new ValidationResult();

        public bool HasErrors => ValidationResult.HasErrors;

        public void Error(string message)
        {
            ValidationResult.AddError(message);
        }

        public void Event(IDomainEvent domainEvent)
        {
            _events.Add(domainEvent);
        }

        public string GetValidationErrors()
        {
            return ValidationResult.ToString();
        }

        public void LogDebug(object sender, string message, object additionalData = null)
        {
            var entry = new LogEntry()
            {
                Sender = sender,
                Message = message,
                AdditionalData = additionalData,
                LogLevel = LogLevel.Debug,
                Date = DateTime.UtcNow
            };

            _logs.Add(entry);
        }

        public void LogError(object sender, string message, object additionalData = null)
        {
            var entry = new LogEntry()
            {
                Sender = sender,
                Message = message,
                AdditionalData = additionalData,
                LogLevel = LogLevel.Error,
                Date = DateTime.UtcNow
            };

            _logs.Add(entry);
        }

        public void LogError(object sender, string message, Exception exception, object additionalData = null)
        {
            var entry = new LogEntry()
            {
                Sender = sender,
                Message = message,
                AdditionalData = additionalData,
                LogLevel = LogLevel.Error,
                Date = DateTime.UtcNow,
                Exception = exception
            };

            _logs.Add(entry);
        }

        public void LogInfo(object sender, string message, object additionalData = null)
        {
            var entry = new LogEntry()
            {
                Sender = sender,
                Message = message,
                AdditionalData = additionalData,
                LogLevel = LogLevel.Information,
                Date = DateTime.UtcNow
            };

            _logs.Add(entry);
        }

        public void LogTrace(object sender, string message, object additionalData = null)
        {
            var entry = new LogEntry()
            {
                Sender = sender,
                Message = message,
                AdditionalData = additionalData,
                LogLevel = LogLevel.Trace,
                Date = DateTime.UtcNow
            };

            _logs.Add(entry);
        }

        public void LogWarning(object sender, string message, object additionalData = null)
        {
            var entry = new LogEntry()
            {
                Sender = sender,
                Message = message,
                AdditionalData = additionalData,
                LogLevel = LogLevel.Warning,
                Date = DateTime.UtcNow
            };

            _logs.Add(entry);
        }
    }
}
