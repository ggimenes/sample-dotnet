using SampleDotnet.Contracts.Financial.Payments;
using SampleDotnet.Contracts.Store.Checkouts.Orders;
using SampleDotnet.DDD.Abstractions;
using SampleDotnet.Financial.AppService.Payments.DTOs;
using SampleDotnet.Financial.Domain.Payments;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SampleDotnet.Financial.AppService.Checkouts.Orders
{
    public class PaymentAppService : IPaymentAppService
    {
        private readonly INotificationHandler _notificationHandler;
        private readonly IRepository<Payment> _paymentRepository;

        public PaymentAppService(
            INotificationHandler notificationHandler,
            IRepository<Payment> paymentRepository)
        {
            this._notificationHandler = notificationHandler;
            this._paymentRepository = paymentRepository;
        }

        public async Task<SubmitPaymentResponseDTO> ReceivePayment(SubmitPaymentCommand request)
        {        
            Payment payment = CreatePayment(request);

            _notificationHandler.Notification += payment.Notification;
            if (_notificationHandler.Notification.HasErrors)
                return default;

            // could check customer balance and others validations about payment flow

            payment.Accept();

            _notificationHandler.Notification += payment.Notification;
            if (_notificationHandler.Notification.HasErrors)
                return default;

            await _paymentRepository.Add(payment);

            await _notificationHandler.DispatchAndFlush(request.CorrelationId);

            return CreateResponse(request.CorrelationId, payment);
        }

        public async Task<Guid> ApprovePayment(RequestRefundCommand request)
        {
            Payment payment = await _paymentRepository.FindById(request.PaymentId);            

            payment.Process();

            _notificationHandler.Notification += payment.Notification;
            if (_notificationHandler.Notification.HasErrors)
                return default;

            await _paymentRepository.Update(payment);

            await _notificationHandler.DispatchAndFlush(request.CorrelationId);

            return payment.Id;
        }

        public async Task<Guid> RefundPayment(RequestRefundCommand request)
        {
            Payment payment = await _paymentRepository.FindById(request.PaymentId);

            payment.Refund();

            _notificationHandler.Notification += payment.Notification;
            if (_notificationHandler.Notification.HasErrors)
                return default;

            await _paymentRepository.Update(payment);

            await _notificationHandler.DispatchAndFlush(request.CorrelationId);

            return payment.Id;
        }

        public async Task<Guid> CancelPayment(CancelPaymentCommand request)
        {
            Payment payment = await _paymentRepository.FindById(request.PaymentId);

            payment.Cancel();

            _notificationHandler.Notification += payment.Notification;
            if (_notificationHandler.Notification.HasErrors)
                return default;

            await _paymentRepository.Update(payment);

            await _notificationHandler.DispatchAndFlush(request.CorrelationId);

            return payment.Id;
        }


        private SubmitPaymentResponseDTO CreateResponse(Guid correlationId, Payment payment)
        {
            return new SubmitPaymentResponseDTO()
            {
                CorrelationId = correlationId,
                OrderId = payment.Id
            };
        }

        private Payment CreatePayment(SubmitPaymentCommand request)
        {
            return Payment.Create(
                request.CardName,
                request.CardNumber,
                request.Expiration,
                request.SecurityCode,
                request.Value);
        }
    }
}
