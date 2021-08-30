using SampleDotnet.Contracts.Financial.Payments;
using SampleDotnet.Security.AppService.Payments.DTOs;
using System;
using System.Threading.Tasks;

namespace SampleDotnet.Security.AppService.Checkouts.Orders
{
    public interface IPaymentAppService
    {
        Task<SubmitPaymentResponseDTO> ReceivePayment(SubmitPaymentCommand request);
        Task<Guid> ApprovePayment(ApprovePaymentCommand request);
        Task<Guid> RefundPayment(RequestRefundCommand request);
        Task<Guid> CancelPayment(CancelPaymentCommand request);
    }
}