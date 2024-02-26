using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SampleDotnet.DDD;
using SampleDotnet.DDD.Abstractions;
using SampleDotnet.DDD.Data.MongoDb;
using SampleDotnet.Store.AppService.Checkouts.Orders;
using SampleDotnet.Store.Domain.Checkouts.Orders;
using SampleDotnet.Store.Infra.Masstransit;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using SampleDotnet.MassTransit.ActivityTracing;

namespace SampleDotnet.Store.Infra.IoC.Bootstrapper
{
    public class ContainerConfigurator
    {
        public IServiceCollection AddServices(IServiceCollection services, IConfiguration configuration)
        {
            // OpenTelemetry
            AddOpenTelemetry(services);

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

        private static void AddOpenTelemetry(IServiceCollection services)
        {
            var builder = services.AddOpenTelemetry();
            builder.WithTracing((builder) => builder
                .AddAspNetCoreInstrumentation()
                .AddOtlpExporter()
                .AddSource(ActivitySourceMT.Instance.Name)
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("SampleDotnet.Store.Api")));
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
