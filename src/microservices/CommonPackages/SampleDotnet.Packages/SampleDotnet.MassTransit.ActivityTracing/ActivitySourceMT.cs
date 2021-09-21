using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SampleDotnet.MassTransit.ActivityTracing
{
    public static class ActivitySourceMT
    {
        public static ActivitySource Instance = new ActivitySource(
                "MassTransit",
                "7.2.0");
    }
}
