using MongoDbGenericRepository.Models;
using SampleDotnet.DDD;
using SampleDotnet.DDD.Abstractions;
using System;

namespace SampleDotnet.Security.Domain.Payments
{
    public class Payment : Entity, IDocument
    {
        // author's remarks: could have many different payment methods. Accepting just credit for sake of simplicity

        public PaymentMethod PaymentMethod { get; private set; } = PaymentMethod.Credit;
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public DateTime Expiration { get; private set; }
        public int SecurityCode { get; private set; }
        public decimal Value { get; private set; }
        public PaymentStatus Status { get; set; }
        public int Version { get; set; }

        public INotification Notification { get; set; } = new Notification();

        private Payment()
        {

        }

        public static Payment Create(string cardName, string cardNumber, DateTime expiration, int securityCode, decimal value)
        {
            var payment = new Payment();

            payment.CardName = cardName;
            payment.CardNumber = cardNumber;
            payment.Expiration = expiration;
            payment.SecurityCode = securityCode;
            payment.Value = value;

            payment.Validate();

            return payment;
        }

        public void Accept()
        {
            if (Notification.HasErrors)
                return;

            Status = PaymentStatus.Accepted;

            Notification.Event(EventFactory.CreatePaymentAccepted(this));
        }

        public void Process()
        {
            if (Notification.HasErrors)
                return;

            // rules to process the payment

            Status = PaymentStatus.Approved;

            Notification.Event(EventFactory.CreatePaymentApproved(this));
        }

        public void Refund()
        {
            if (Notification.HasErrors)
                return;

            Status = PaymentStatus.RefundApplyed;

            Notification.Event(EventFactory.CreateRefundApplyed(this));
        }

        public void Cancel()
        {
            if (Notification.HasErrors)
                return;

            Status = PaymentStatus.Canceled;

            Notification.Event(EventFactory.CreatePaymentCanceled(this));
        }

        private void Validate()
        {
            // Card validation could be done in depth, differs from Store validation for payments.
            // The object struture also could be very different, because this is another bounded context
            bool cardValid = CardNumber == "5555555555554444";
            if (!cardValid)
                Notification.Error("The card is invalid");

            if (string.IsNullOrWhiteSpace(CardName))
                Notification.Error("The field 'CardName' is required");

            if (string.IsNullOrWhiteSpace(CardNumber))
                Notification.Error("The field 'CardNumber' is required");

            if (Expiration == new DateTime())
                Notification.Error("The field 'Expiration' is required");

            if (SecurityCode <= 0)
                Notification.Error("The field 'SecurityCode' is required");

            if (Value <= 0)
                Notification.Error("The field 'Value' is required");
        }
    }

    public enum PaymentMethod
    {
        None = 0,
        Credit
    }

    public enum PaymentStatus
    {
        None = 0,
        Accepted,
        Approved,
        RefundApplyed,
        Canceled
    }
}
