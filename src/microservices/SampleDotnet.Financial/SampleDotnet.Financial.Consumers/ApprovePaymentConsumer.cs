using MassTransit;
using SampleDotnet.Contracts.Financial.Payments;
using SampleDotnet.Financial.AppService.Checkouts.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Financial.Consumers
{
    public class ApprovePaymentConsumer : IConsumer<RequestRefundCommand>
    {
        private readonly IPaymentAppService _paymentAppService;

        public ApprovePaymentConsumer(IPaymentAppService paymentAppService)
        {
            this._paymentAppService = paymentAppService;
        }
        public Task Consume(ConsumeContext<RequestRefundCommand> context)
        {
            return _paymentAppService.ApprovePayment(context.Message);
        }
    }
}
