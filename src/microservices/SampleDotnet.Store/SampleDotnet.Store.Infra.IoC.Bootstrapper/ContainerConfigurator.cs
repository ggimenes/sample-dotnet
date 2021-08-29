using Microsoft.Extensions.DependencyInjection;
using SampleDotnet.DDD;
using SampleDotnet.DDD.Abstractions;
using SampleDotnet.DDD.Data.MongoDb;
using SampleDotnet.Store.AppService.Checkouts.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Store.Infra.IoC.Bootstrapper
{
    public class ContainerConfigurator
    {
        public IServiceCollection AddServices(IServiceCollection services)
        {
            // AppService
            services.AddTransient<IOrderBuilder, OrderBuilder>();
            services.AddTransient<IOrderAppService, OrderAppService>();

            // Domain
            services.AddScoped<INotification, Notification>();
            services.AddScoped<INotificationHandler, NotificationHandler>();

            // Data
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}
