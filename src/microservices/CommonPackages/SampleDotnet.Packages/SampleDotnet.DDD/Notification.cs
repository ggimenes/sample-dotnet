﻿using Microsoft.Extensions.Logging;
using SampleDotnet.DDD.Abstractions;
using System;
using System.Collections.Generic;

namespace SampleDotnet.DDD
{
    public class Notification : BaseNotification
    {
        private readonly List<IDomainEvent> _events = new List<IDomainEvent>();
        private readonly List<ILogEntry> _logs = new List<ILogEntry>();

        public override IEnumerable<IDomainEvent> Events => _events;
        public override IEnumerable<ILogEntry> Logs => _logs;
        public ValidationResult ValidationResult { get; private set; } = new ValidationResult();

        public override bool HasErrors => ValidationResult.HasErrors;

        public override void Error(string message)
        {
            ValidationResult.AddError(message);
        }

        public override void Event(IDomainEvent domainEvent)
        {
            _events.Add(domainEvent);
        }

        public override string GetValidationErrorsFormatted()
        {
            return ValidationResult.ToString();
        }

        public override IEnumerable<string> GetValidationErrors()
        {
            return ValidationResult.Errors;
        }

        public override void LogDebug(object sender, string message, object additionalData = null)
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

        public override void LogError(object sender, string message, object additionalData = null)
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

        public override void LogError(object sender, string message, Exception exception, object additionalData = null)
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

        public override void LogInfo(object sender, string message, object additionalData = null)
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

        public override void LogTrace(object sender, string message, object additionalData = null)
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

        public override void LogWarning(object sender, string message, object additionalData = null)
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

        public override INotification Combine(INotification b)
        {
            _events.AddRange(b.Events);
            _logs.AddRange(b.Logs);
            ValidationResult.Errors.AddRange(b.GetValidationErrors());

            return this;
        }

        public static Notification operator +(Notification a, Notification b)
        {
            a._events.AddRange(b.Events);
            a._logs.AddRange(b.Logs);
            a.ValidationResult += b.ValidationResult;

            return a;
        }
    }
}
