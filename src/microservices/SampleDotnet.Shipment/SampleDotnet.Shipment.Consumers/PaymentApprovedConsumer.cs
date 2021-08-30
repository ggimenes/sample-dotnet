using MassTransit;
using SampleDotnet.Contracts.Financial.Payments;
using SampleDotnet.Contracts.Shipment;
using System.Threading.Tasks;

namespace SampleDotnet.Financial.Consumers
{
    public class PaymentApprovedConsumer : IConsumer<PaymentApproved>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PaymentApprovedConsumer(IPublishEndpoint publishEndpoint)
        {
            this._publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<PaymentApproved> context)
        {
            await _publishEndpoint.Publish(new ShipmentOrderCreated() { CorrelationId = context.Message.CorrelationId });
        }
    }
}
