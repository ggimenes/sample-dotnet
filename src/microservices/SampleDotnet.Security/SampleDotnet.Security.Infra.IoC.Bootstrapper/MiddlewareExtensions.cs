using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Security.Infra.IoC.Bootstrapper
{
    public static class MiddlewareExtensions
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
        {
            return new ContainerConfigurator()
                .AddServices(services, configuration);
        }
    }
}
