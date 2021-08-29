﻿using Microsoft.Extensions.Logging;
using SampleDotnet.DDD.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.DDD
{
    public class NotificationHandler : INotificationHandler
    {
        private readonly IEventDispatcher _eventDispatcher;
        private readonly ILoggerFactory _factory;

        public INotification Notification { get; set; }

        public NotificationHandler(IEventDispatcher eventDispatcher, ILoggerFactory factory)
        {
            this._eventDispatcher = eventDispatcher;
            this._factory = factory;
        }

        public async Task DispatchAndFlush(Guid correlationId)
        {
            FlushLogs();

            await DispatchEvents(correlationId);
        }

        private async Task DispatchEvents(Guid correlationId)
        {
            Notification.Events
                            .ToList()
                            .ForEach(x => x.CorrelationId = correlationId);

            await _eventDispatcher.Dispatch(Notification.Events);
        }

        private void FlushLogs()
        {
            foreach (var log in Notification.Logs)
            {
                var logger = _factory.CreateLogger(log.Sender.GetType().FullName);
                logger.Log(log.LogLevel, new EventId(), log.AdditionalData, log.Exception, (s, ex) => log.Message);
            }
        }
    }
}