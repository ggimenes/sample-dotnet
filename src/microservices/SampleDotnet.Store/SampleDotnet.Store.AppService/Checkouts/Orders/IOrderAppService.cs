using SampleDotnet.Contracts.Store.Checkouts.Orders;
using SampleDotnet.Store.AppService.Checkouts.Orders.DTOs;
using System;
using System.Threading.Tasks;

namespace SampleDotnet.Store.AppService.Checkouts.Orders
{
    public interface IOrderAppService
    {
        Task<SubmitOrderResponseDTO> SubmitOrder(SubmitOrderCommand request);
    }
}