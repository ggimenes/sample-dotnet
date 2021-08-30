using SampleDotnet.Contracts.Financial.Payments;
using SampleDotnet.Financial.AppService.Payments.DTOs;
using System;
using System.Threading.Tasks;

namespace SampleDotnet.Financial.AppService.Checkouts.Orders
{
    public interface IPaymentAppService
    {
        Task<SubmitPaymentResponseDTO> ReceivePayment(SubmitPaymentCommand request);
        Task<Guid> ApprovePayment(ApprovePaymentCommand request);
        Task<Guid> RefundPayment(RequestRefundCommand request);
        Task<Guid> CancelPayment(CancelPaymentCommand request);
    }
}