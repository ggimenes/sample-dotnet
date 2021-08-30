using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleDotnet.DDD.Abstractions;
using SampleDotnet.MasstransitConfiguration;
using SampleDotnet.Security.Infra.Masstransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Security.Infra.Masstransit
{
    public static class MiddlewareExtensions
    {
        public static IServiceCollection AddSecurityMasstransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEventDispatcher, EventDispatcher>();

            new MasstransitSetup()
                .Configure(services, configuration);

            return services;
        }
    }
}
