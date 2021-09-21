using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using Newtonsoft.Json;

namespace SampleDotnet.MassTransit.ActivityTracing
{
    public class ActivityTracingConsumeFilter : IFilter<ConsumeContext>
    {
        private readonly IGlobalTraceInterceptor? _globalTraceInterceptor;

        public ActivityTracingConsumeFilter(IGlobalTraceInterceptor? globalTraceInterceptor = null)
        {
            _globalTraceInterceptor = globalTraceInterceptor;
        }

        public void Probe(ProbeContext context)
        { }

        public async Task Send(ConsumeContext context, IPipe<ConsumeContext> next)
        {
            var operationName = $"Consuming Message: {context.DestinationAddress.GetExchangeName()}";

            string? parentActivityId = Activity.Current != null ? Activity.Current.Id : null;
            using (var activity = ActivitySourceMT.Instance.StartActivity(operationName, ActivityKind.Consumer))
            {
                if (activity is null)
                {
                    await next.Send(context);
                    return;
                }

                if (!context.Headers.TryGetHeader(Constants.TraceParentHeaderName, out var requestId))
                {
                    context.Headers.TryGetHeader(Constants.RequestIdHeaderName, out requestId);
                }

                if (!string.IsNullOrEmpty(requestId?.ToString()))
                {
                    // This is the magic
                    activity.SetParentId(requestId?.ToString());

                    if (context.Headers.TryGetHeader(Constants.TraceStateHeaderName, out var traceState))
                    {
                        activity.TraceStateString = traceState?.ToString();
                    }
                }

                if (parentActivityId is not null) activity.SetParentId(parentActivityId);

                activity
                    .AddTag("message-types", string.Join(", ", context.SupportedMessageTypes))
                    .AddTag("source-host-masstransit-version", context.Host.MassTransitVersion)
                    .AddTag("source-host-process-id", context.Host.ProcessId)
                    .AddTag("source-host-framework-version", context.Host.FrameworkVersion)
                    .AddTag("source-host-machine", context.Host.MachineName)
                    .AddTag("input-address", context.ReceiveContext.InputAddress.ToString())
                    .AddTag("destination-address", context.DestinationAddress?.ToString())
                    .AddTag("source-address", context.SourceAddress?.ToString())
                    .AddTag("initiator-id", context.InitiatorId?.ToString())
                    .AddTag("message-id", context.MessageId?.ToString());                

                if (_globalTraceInterceptor != null) await _globalTraceInterceptor.Intercept(context, activity);

                await next.Send(context);
            }
        }      
    }
}
