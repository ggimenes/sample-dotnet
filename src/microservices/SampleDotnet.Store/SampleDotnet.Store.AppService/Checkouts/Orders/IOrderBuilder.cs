using SampleDotnet.Contracts.Store.Checkouts.Orders;
using SampleDotnet.Store.Domain.Checkouts.Orders;

namespace SampleDotnet.Store.AppService.Checkouts.Orders
{
    public interface IOrderBuilder
    {
        Order Build();
        OrderBuilder FromCommand(SubmitOrderCommand submitOrderCommand);
    }
}