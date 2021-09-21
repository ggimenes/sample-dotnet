using System.Diagnostics;
using System.Threading.Tasks;
using GreenPipes;
using System.Linq;
using MassTransit.RabbitMqTransport;
using System;
using MassTransit.Internals.Reflection;
using System.Reflection;
using Newtonsoft.Json;
using MassTransit;

namespace SampleDotnet.MassTransit.ActivityTracing
{
    public class OpenTracingPublishFilter : IFilter<PublishContext>, IFilter<SendContext>
    {
        private readonly IGlobalTraceInterceptor? _globalTraceInterceptor;

        public OpenTracingPublishFilter(IGlobalTraceInterceptor? globalTraceInterceptor = null)
        {
            _globalTraceInterceptor = globalTraceInterceptor;
        }

        public async Task Send(SendContext context, IPipe<SendContext> next)
        {
            string? parentActivityId = Activity.Current != null ? Activity.Current.Id : null;
            using (var activity = ActivitySourceMT.Instance.StartActivity(
                $"{Constants.ProducerActivityName}:{context.DestinationAddress?.GetExchangeName()}",
                ActivityKind.Producer))
            {
                if (activity is null)
                {
                    await next.Send(context);
                    return;
                }

                InjectHeaders(activity, context);

                if (parentActivityId is not null) activity.SetParentId(parentActivityId);                

                activity
                    .AddTag("destination-address", context.DestinationAddress?.ToString())
                    .AddTag("source-address", context.SourceAddress?.ToString())
                    .AddTag("initiator-id", context.InitiatorId?.ToString())
                    .AddTag("message-id", context.MessageId?.ToString());

                if (_globalTraceInterceptor != null) await _globalTraceInterceptor.Intercept(context, activity);

                await next.Send(context);
            }
        }

        public void Probe(ProbeContext context)
        { }

        public async Task Send(PublishContext context, IPipe<PublishContext> next)
        {
            string? parentActivityId = Activity.Current != null ? Activity.Current.Id : null;
            using (var activity = ActivitySourceMT.Instance.StartActivity(
                $"{Constants.PublishProducerActivityName}:{context.DestinationAddress?.GetExchangeName()}",
                ActivityKind.Producer))
            {
                if (activity is null)
                {
                    await next.Send(context);
                    return;
                }

                InjectHeaders(activity, context);

                if (parentActivityId is not null) activity.SetParentId(parentActivityId);


                activity
                    .AddTag("destination-address", context.DestinationAddress?.ToString())
                    .AddTag("source-address", context.SourceAddress?.ToString())
                    .AddTag("initiator-id", context.InitiatorId?.ToString())
                    .AddTag("message-id", context.MessageId?.ToString());              

                if (_globalTraceInterceptor != null) await _globalTraceInterceptor.Intercept(context, activity);

                await next.Send(context);
            }

        }

        private static void InjectHeaders(
            Activity activity,
            SendContext context)
        {
            if (activity.IdFormat == ActivityIdFormat.W3C)
            {
                if (!context.Headers.TryGetHeader(Constants.TraceParentHeaderName, out _))
                {
                    context.Headers.Set(Constants.TraceParentHeaderName, activity.Id);
                    if (activity.TraceStateString != null)
                    {
                        context.Headers.Set(Constants.TraceStateHeaderName, activity.TraceStateString);
                    }
                }
            }
            else
            {
                if (!context.Headers.TryGetHeader(Constants.RequestIdHeaderName, out _))
                {
                    context.Headers.Set(Constants.RequestIdHeaderName, activity.Id);
                }
            }
        }
    }
}
