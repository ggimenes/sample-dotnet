using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.MassTransit.ActivityTracing
{
    public interface IGlobalTraceInterceptor
    {
        Task Intercept(object context, Activity traceActivity);
    }
}
