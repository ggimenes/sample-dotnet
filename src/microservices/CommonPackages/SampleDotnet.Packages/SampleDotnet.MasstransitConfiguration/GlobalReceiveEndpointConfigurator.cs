using MassTransit;
using System;

namespace SampleDotnet.MasstransitConfiguration
{
    public class GlobalReceiveEndpointConfigurator : IConfigureReceiveEndpoint
    {
        public void Configure(string name, IReceiveEndpointConfigurator configurator)
        {
            configurator.UseInMemoryOutbox();
            configurator.UseMessageRetry(x => x.Exponential(
                3,
                TimeSpan.FromMilliseconds(50),
                TimeSpan.FromMilliseconds(1000),
                TimeSpan.FromMilliseconds(200)));

        }
    }
}
