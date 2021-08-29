using SampleDotnet.DDD;
using SampleDotnet.DDD.Abstractions;
using System;

namespace SampleDotnet.Store.Domain.Checkouts.Orders
{
    public class Payment : Entity
    {
        // author's remarks: could have many different payment methods. Accepting just credit for sake of simplicity

        public PaymentMethod PaymentMethod { get; private set; } = PaymentMethod.Credit;
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public DateTime Expiration { get; private set; }
        public int SecurityCode { get; private set; }
        public decimal Value { get; private set; }

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

        private void Validate()
        {
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
}
