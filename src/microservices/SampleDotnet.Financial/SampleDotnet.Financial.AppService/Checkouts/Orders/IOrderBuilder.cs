using SampleDotnet.Contracts.Store.Checkouts.Orders;
using SampleDotnet.Financial.Domain.Checkouts.Orders;

namespace SampleDotnet.Financial.AppService.Checkouts.Orders
{
    public interface IOrderBuilder
    {
        Order Build();
        OrderBuilder FromCommand(SubmitOrderCommand submitOrderCommand);
    }
}