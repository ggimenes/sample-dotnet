using AutoMapper;
using MassTransit;
using SampleDotnet.Contracts.Financial.Payments;
using SampleDotnet.Contracts.Security.Anti_Fraud;
using SampleDotnet.Contracts.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Financial.Consumers
{
    public class FraudDetectedConsumer : IConsumer<FraudDetected>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public FraudDetectedConsumer(IPublishEndpoint publishEndpoint)
        {
            this._publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<FraudDetected> context)
        {
            await _publishEndpoint.Publish(new StockReleased() { CorrelationId = context.Message.CorrelationId });
        }
    }
}
