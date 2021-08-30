using MassTransit;
using SampleDotnet.Contracts.Security.Anti_Fraud;
using System.Threading.Tasks;

namespace SampleDotnet.Security.Consumers
{
    public class ValidateFraudConsumer : IConsumer<ValidateFraudCommand>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ValidateFraudConsumer(IPublishEndpoint publishEndpoint)
        {
            this._publishEndpoint = publishEndpoint;
        }
        public async Task Consume(ConsumeContext<ValidateFraudCommand> context)
        {
            if (context.Message.CardNumber == "5555555555554444" ||
               context.Message.CardNumber == "9999999999999999")
                await _publishEndpoint.Publish(new PaymentValidated() { CorrelationId = context.Message.CorrelationId });
            else
                await _publishEndpoint.Publish(new FraudDetected() { CorrelationId = context.Message.CorrelationId });
        }
    }
}
