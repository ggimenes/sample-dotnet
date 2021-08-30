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
    public class SubmitPaymentConsumer : IConsumer<SubmitPaymentCommand>
    {
        private readonly IPaymentAppService _paymentAppService;

        public SubmitPaymentConsumer(IPaymentAppService paymentAppService)
        {
            this._paymentAppService = paymentAppService;
        }
        public Task Consume(ConsumeContext<SubmitPaymentCommand> context)
        {
            return _paymentAppService.ReceivePayment(context.Message);
        }
    }
}
