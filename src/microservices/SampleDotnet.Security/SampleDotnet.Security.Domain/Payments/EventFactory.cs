using SampleDotnet.Contracts.Financial.Payments;
using SampleDotnet.Contracts.Store.Checkouts.Orders;
using SampleDotnet.DDD.Abstractions;
using SampleDotnet.Security.Domain.Payments;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleDotnet.Security.Domain.Payments
{
    public static class EventFactory
    {
        public static PaymentAccepted CreatePaymentAccepted(Payment payment)
        {
            return new PaymentAccepted
            {
                PaymentId = payment.Id
            };
        }

        public static PaymentApproved CreatePaymentApproved(Payment payment)
        {
            return new PaymentApproved
            {
                PaymentId = payment.Id
            };
        }

        public static PaymentCanceled CreatePaymentCanceled(Payment payment)
        {
            return new PaymentCanceled
            {
                PaymentId = payment.Id
            };
        }

        public static RefundApplyed CreateRefundApplyed(Payment payment)
        {
            return new RefundApplyed
            {
                PaymentId = payment.Id
            };
        }
    }
}
