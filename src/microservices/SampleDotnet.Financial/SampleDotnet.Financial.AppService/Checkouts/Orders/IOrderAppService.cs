using SampleDotnet.Contracts.Store.Checkouts.Orders;
using SampleDotnet.Financial.AppService.Checkouts.Orders.DTOs;
using System;
using System.Threading.Tasks;

namespace SampleDotnet.Financial.AppService.Checkouts.Orders
{
    public interface IOrderAppService
    {
        Task<SubmitOrderResponseDTO> SubmitOrder(SubmitOrderCommand request);
    }
}