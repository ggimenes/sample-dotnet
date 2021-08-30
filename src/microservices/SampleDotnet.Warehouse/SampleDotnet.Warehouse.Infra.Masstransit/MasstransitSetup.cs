﻿using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SampleDotnet.MasstransitConfiguration;
using System;
using System.Security.Authentication;
using SampleDotnet.Warehouse.Consumers;
using SampleDotnet.Financial.Consumers;

namespace SampleDotnet.Warehouse.Infra.Masstransit
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

                config.AddConsumer<FraudDetectedConsumer>();
                config.AddConsumer<PaymentApprovedConsumer>();
                config.AddConsumer<ReserveStockConsumer>();

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

                    cfg.ReceiveEndpoint(e =>
                    {
                        e.ConcurrentMessageLimit = masstransitConfig.OrderStateMachine.ConcurrentMessageLimit;
                        e.PrefetchCount = masstransitConfig.OrderStateMachine.PrefetchCount;

                        e.ConfigureConsumer<FraudDetectedConsumer>(context);
                    });

                    cfg.ReceiveEndpoint(e =>
                    {
                        e.ConcurrentMessageLimit = masstransitConfig.OrderStateMachine.ConcurrentMessageLimit;
                        e.PrefetchCount = masstransitConfig.OrderStateMachine.PrefetchCount;

                        e.ConfigureConsumer<PaymentApprovedConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("reserve-stock", e =>
                    {
                        e.ConcurrentMessageLimit = masstransitConfig.OrderStateMachine.ConcurrentMessageLimit;
                        e.PrefetchCount = masstransitConfig.OrderStateMachine.PrefetchCount;

                        e.ConfigureConsumer<ReserveStockConsumer>(context);
                    });                    
                });
            });

            services.AddMassTransitHostedService();

            return services;
        }
    }
}
