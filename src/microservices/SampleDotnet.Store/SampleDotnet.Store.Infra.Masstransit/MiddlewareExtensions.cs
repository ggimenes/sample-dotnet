using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleDotnet.DDD.Abstractions;
using SampleDotnet.MasstransitConfiguration;
using SampleDotnet.Store.Infra.Masstransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Store.Infra.Masstransit
{
    public static class MiddlewareExtensions
    {
        public static IServiceCollection AddStoreMasstransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEventDispatcher, EventDispatcher>();

            new MasstransitSetup()
                .Configure(services, configuration);

            return services;
        }
    }
}
