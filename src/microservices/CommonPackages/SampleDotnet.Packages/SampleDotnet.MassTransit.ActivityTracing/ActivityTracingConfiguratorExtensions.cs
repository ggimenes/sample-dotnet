using MassTransit;

namespace SampleDotnet.MassTransit.ActivityTracing
{
    public static class ActivityTracingConfiguratorExtensions
    {
        public static void PropagateActivityTracingContext(this IBusFactoryConfigurator value, IGlobalTraceInterceptor? globalTraceInterceptor = null)
        {
            value.ConfigurePublish(configurator => configurator.AddPipeSpecification(new ActivityTracingPipeSpecification(globalTraceInterceptor)));
            value.ConfigureSend(configurator => configurator.AddPipeSpecification(new ActivityTracingPipeSpecification(globalTraceInterceptor)));
            value.AddPipeSpecification(new ActivityTracingPipeSpecification(globalTraceInterceptor));
        }
    }
}
