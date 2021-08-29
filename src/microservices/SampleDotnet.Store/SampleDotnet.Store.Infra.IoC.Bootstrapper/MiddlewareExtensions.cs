using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Store.Infra.IoC.Bootstrapper
{
    public static class MiddlewareExtensions
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            return new ContainerConfigurator()
                .AddServices(services);
        }
    }
}
