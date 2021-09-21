using System.Collections.Generic;
using System.Linq;
using GreenPipes;
using MassTransit;

namespace SampleDotnet.MassTransit.ActivityTracing
{
    public class ActivityTracingPipeSpecification : IPipeSpecification<ConsumeContext>, IPipeSpecification<PublishContext>, IPipeSpecification<SendContext>
    {
        private readonly IGlobalTraceInterceptor? _globalTraceInterceptor;

        public ActivityTracingPipeSpecification(IGlobalTraceInterceptor? globalTraceInterceptor = null)
        {
            _globalTraceInterceptor = globalTraceInterceptor;
        }

        public IEnumerable<ValidationResult> Validate()
        {
            return Enumerable.Empty<ValidationResult>();
        }

        public void Apply(IPipeBuilder<ConsumeContext> builder)
        {
            builder.AddFilter(new ActivityTracingConsumeFilter(_globalTraceInterceptor));
        }

        public void Apply(IPipeBuilder<PublishContext> builder)
        {
            builder.AddFilter(new OpenTracingPublishFilter(_globalTraceInterceptor));
        }

        public void Apply(IPipeBuilder<SendContext> builder)
        {
            builder.AddFilter(new OpenTracingPublishFilter(_globalTraceInterceptor));
        }
    }
}
