using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SampleDotnet.MasstransitConfiguration;
using SampleDotnet.Financial.Workflows.Checkouts.Orders;
using System;
using System.Security.Authentication;
using MassTransit.MongoDbIntegration.Saga;
using SampleDotnet.Contracts.Store.Checkouts.Orders;
using SampleDotnet.Contracts.Financial.Payments;
using SampleDotnet.Contracts.Security.Anti_Fraud;
using SampleDotnet.Contracts.Shipment;
using SampleDotnet.Contracts.Warehouse;

namespace SampleDotnet.Financial.Infra.Masstransit
{
    public class MasstransitSetup
    {
        public IServiceCollection Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqConfig>(configuration.GetSection("RabbitMq"));
            services.Configure<MasstransitConfig>(configuration.GetSection("Masstransit"));

            services.AddTransient<IConfigureReceiveEndpoint, GlobalReceiveEndpointConfigurator>();

            services.AddMassTransit(config =>
            {
                config.SetKebabCaseEndpointNameFormatter();

                config.AddSagaStateMachine<OrderStateMachine, OrderState>()
                    .MongoDbRepository(configuration.GetSection("ConnectionString").Value, r => { });

                config.AddActivitiesFromNamespaceContaining<OrderAcceptedActivity>();

                config.UsingRabbitMq((context, cfg) =>
                {
                    var rabbitConfig = context.GetRequiredService<IOptions<RabbitMqConfig>>()?.Value;
                    var masstransitConfig = context.GetRequiredService<IOptions<MasstransitConfig>>()?.Value;

                    cfg.Host(rabbitConfig.Hostname, rabbitConfig.Port, rabbitConfig.VirtualHost, h =>
                    {
                        h.Username(rabbitConfig.Username);
                        h.Password(rabbitConfig.Password);

                        if (rabbitConfig.UseSsl)
                            h.UseSsl(s =>
                            {
                                s.Protocol = SslProtocols.Tls12;
                            });

                        h.ConfigureBatchPublish(x =>
                        {
                            x.Timeout = TimeSpan.FromMilliseconds(50);
                        });

                        h.PublisherConfirmation = true;
                    });

                    cfg.ReceiveEndpoint("re.store.order",e =>
                    {
                        e.ConcurrentMessageLimit = masstransitConfig.OrderStateMachine.ConcurrentMessageLimit;
                        e.PrefetchCount = masstransitConfig.OrderStateMachine.PrefetchCount;

                        e.ConfigureSaga<OrderState>(context, c =>
                        {
                            var partition = c.CreatePartitioner(masstransitConfig.OrderStateMachine.PartitionCount);

                            c.Message<OrderAccepted>(x => x.UsePartitioner(partition, m => m.Message.CorrelationId));
                            c.Message<PaymentAccepted>(x => x.UsePartitioner(partition, m => m.Message.CorrelationId));
                            c.Message<PaymentApproved>(x => x.UsePartitioner(partition, m => m.Message.CorrelationId));
                            c.Message<RefundApplyed>(x => x.UsePartitioner(partition, m => m.Message.CorrelationId));
                            c.Message<PaymentCanceled>(x => x.UsePartitioner(partition, m => m.Message.CorrelationId));
                            c.Message<FraudDetected>(x => x.UsePartitioner(partition, m => m.Message.CorrelationId));
                            c.Message<PaymentValidated>(x => x.UsePartitioner(partition, m => m.Message.CorrelationId));
                            c.Message<ShipmentOrderCreated>(x => x.UsePartitioner(partition, m => m.Message.CorrelationId));
                            c.Message<StockReserved>(x => x.UsePartitioner(partition, m => m.Message.CorrelationId));
                            c.Message<StockReleased>(x => x.UsePartitioner(partition, m => m.Message.CorrelationId));
                            c.Message<StockUpdated>(x => x.UsePartitioner(partition, m => m.Message.CorrelationId));
                        });
                    });
                });
            });

            services.AddMassTransitHostedService();

            return services;
        }
    }
}
