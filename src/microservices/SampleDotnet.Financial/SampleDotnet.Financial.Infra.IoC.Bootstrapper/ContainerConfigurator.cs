using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SampleDotnet.DDD;
using SampleDotnet.DDD.Abstractions;
using SampleDotnet.DDD.Data.MongoDb;
using SampleDotnet.Financial.AppService.Checkouts.Orders;
using SampleDotnet.Financial.Domain.Payments;
using SampleDotnet.Financial.Infra.Automapper;
using SampleDotnet.Financial.Infra.Masstransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.Financial.Infra.IoC.Bootstrapper
{
    public class ContainerConfigurator
    {
        public IServiceCollection AddServices(IServiceCollection services, IConfiguration configuration)
        {
            // Automapper
            services.AddAutoMapper(typeof(ConsumerMappingProfile));
            AutoMapperConfiguration.RegisterMappings();

            // Masstransit            
            services.AddStoreMasstransit(configuration);

            // MongoDb
            AddMongoDb(services, configuration);

            // AppService
            services.AddTransient<IPaymentAppService, PaymentAppService>();

            // Domain
            services.AddScoped<INotification, Notification>();
            services.AddScoped<INotificationHandler, NotificationHandler>();

            // Data
            services.AddTransient(typeof(IRepository<Payment>), typeof(MongoRepository<Payment>));

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
