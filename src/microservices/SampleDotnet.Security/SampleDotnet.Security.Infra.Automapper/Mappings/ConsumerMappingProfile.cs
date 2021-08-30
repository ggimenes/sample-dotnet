using AutoMapper;
using SampleDotnet.Contracts.Financial.Payments;
using SampleDotnet.Contracts.Security.Anti_Fraud;

namespace SampleDotnet.Security.Infra.Automapper
{
    public class ConsumerMappingProfile : Profile
    {
        public ConsumerMappingProfile()
        {
            CreateMap<PaymentAccepted, ApprovePaymentCommand>();
            CreateMap<RefundApplyed, CancelPaymentCommand>();
            CreateMap<FraudDetected, RequestRefundCommand>();
        }
    }
}
