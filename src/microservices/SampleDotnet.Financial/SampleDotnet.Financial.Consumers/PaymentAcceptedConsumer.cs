using AutoMapper;
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
    public class PaymentAcceptedConsumer : IConsumer<PaymentAccepted>
    {
        private readonly IPaymentAppService _paymentAppService;
        private readonly IMapper _mapper;

        public PaymentAcceptedConsumer(IPaymentAppService paymentAppService, IMapper mapper)
        {
            this._paymentAppService = paymentAppService;
            this._mapper = mapper;
        }
        public Task Consume(ConsumeContext<PaymentAccepted> context)
        {
            var command = _mapper.Map<ApprovePaymentCommand>(context.Message);
            // author's remarks: could be replaced by an inter-process event with MediatR and handlers, for example
            return _paymentAppService.ApprovePayment(command);
        }
    }
}
