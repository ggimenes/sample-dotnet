using MassTransit;
using SampleDotnet.DDD.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.MasstransitConfiguration
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventDispatcher(IPublishEndpoint publishEndpoint)
        {
            this._publishEndpoint = publishEndpoint;
        }

        public async Task Dispatch(IEnumerable<IDomainEvent> domainEvents)
        {
            foreach (var dEvent in domainEvents)
            {
                await _publishEndpoint.Publish(dEvent, dEvent.GetType());
            }
        }
    }
}
