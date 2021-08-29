using SampleDotnet.Contracts.Store.Checkouts.Orders;
using System;
using System.Threading.Tasks;

namespace SampleDotnet.Store.AppService.Checkouts.Orders
{
    public interface IOrderAppService
    {
        Task<Guid> SubmitOrder(SubmitOrderCommand request);
    }
}