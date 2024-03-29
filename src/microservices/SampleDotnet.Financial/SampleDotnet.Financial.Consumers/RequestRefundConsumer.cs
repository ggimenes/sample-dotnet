﻿using AutoMapper;
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
    public class RefundApplyedConsumer : IConsumer<RefundApplyed>
    {
        private readonly IPaymentAppService _paymentAppService;
        private readonly IMapper _mapper;

        public RefundApplyedConsumer(IPaymentAppService paymentAppService, IMapper mapper)
        {
            this._paymentAppService = paymentAppService;
            this._mapper = mapper;
        }
        public Task Consume(ConsumeContext<RefundApplyed> context)
        {
            var command = _mapper.Map<CancelPaymentCommand>(context.Message);
            return _paymentAppService.CancelPayment(command);
        }
    }
}
