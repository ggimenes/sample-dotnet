using AutoMapper;
using MassTransit;
using SampleDotnet.Contracts.Financial.Payments;
using SampleDotnet.Contracts.Security.Anti_Fraud;
using SampleDotnet.Financial.AppService.Checkouts.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Financial.Consumers
{
    public class FraudDetectedConsumer : IConsumer<FraudDetected>
    {
        private readonly IPaymentAppService _paymentAppService;
        private readonly IMapper _mapper;

        public FraudDetectedConsumer(IPaymentAppService paymentAppService, IMapper mapper)
        {
            this._paymentAppService = paymentAppService;
            this._mapper = mapper;
        }
        public Task Consume(ConsumeContext<FraudDetected> context)
        {
            var command = _mapper.Map<RequestRefundCommand>(context.Message);
            return _paymentAppService.RefundPayment(command);
        }
    }
}
