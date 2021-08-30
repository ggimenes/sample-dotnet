using MassTransit;
using SampleDotnet.Contracts.Warehouse;
using System.Threading.Tasks;

namespace SampleDotnet.Warehouse.Consumers
{
    public class ReserveStockConsumer : IConsumer<ReserveStockCommand>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ReserveStockConsumer(IPublishEndpoint publishEndpoint)
        {
            this._publishEndpoint = publishEndpoint;
        }
        public async Task Consume(ConsumeContext<ReserveStockCommand> context)
        {
            await _publishEndpoint.Publish(new StockReserved() { CorrelationId = context.Message.CorrelationId });
        }
    }
}
