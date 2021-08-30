using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SampleDotnet.DDD;
using SampleDotnet.DDD.Abstractions;
using SampleDotnet.DDD.Data.MongoDb;
using SampleDotnet.Store.AppService.Checkouts.Orders;
using SampleDotnet.Store.Domain.Checkouts.Orders;
using SampleDotnet.Store.Infra.Masstransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Store.Infra.IoC.Bootstrapper
{
    public class ContainerConfigurator
    {
        public IServiceCollection AddServices(IServiceCollection services, IConfiguration configuration)
        {
            // Masstransit            
            services.AddStoreMasstransit(configuration);

            // MongoDb
            AddMongoDb(services, configuration);

            // AppService
            services.AddTransient<IOrderBuilder, OrderBuilder>();
            services.AddTransient<IOrderAppService, OrderAppService>();

            // Domain
            services.AddScoped<INotification, Notification>();
            services.AddScoped<INotificationHandler, NotificationHandler>();

            // Data
            services.AddTransient(typeof(IRepository<Order>), typeof(MongoRepository<Order>));

            return services;
        }

        private void AddMongoDb(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMongoDatabase>(x =>
            {
                string connectionString = configuration.GetSection("Connectionstring").Value;
                var url = MongoUrl.Create(connectionString);
                var client = new MongoClient(connectionString);
                
                var mongoDbDatabase = client.GetDatabase(url.DatabaseName);

                return mongoDbDatabase;
            });            
        }
    }
}
