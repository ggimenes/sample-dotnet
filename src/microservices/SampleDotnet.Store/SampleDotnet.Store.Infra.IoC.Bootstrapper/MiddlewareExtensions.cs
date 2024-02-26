using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SampleDotnet.Store.Infra.IoC.Bootstrapper
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
