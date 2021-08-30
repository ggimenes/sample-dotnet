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
    public class CancelPaymentConsumer : IConsumer<CancelPaymentCommand>
    {
        private readonly IPaymentAppService _paymentAppService;

        public CancelPaymentConsumer(IPaymentAppService paymentAppService)
        {
            this._paymentAppService = paymentAppService;
        }
        public Task Consume(ConsumeContext<CancelPaymentCommand> context)
        {
            return _paymentAppService.CancelPayment(context.Message);
        }
    }
}
