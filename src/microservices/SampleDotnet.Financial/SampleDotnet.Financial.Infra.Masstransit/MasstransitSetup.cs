using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SampleDotnet.MasstransitConfiguration;
using System;
using System.Security.Authentication;
using SampleDotnet.Financial.Consumers;

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

                config.AddConsumer<SubmitPaymentConsumer>();
                config.AddConsumer<ApprovePaymentConsumer>();
                config.AddConsumer<RequestRefundConsumer>();
                config.AddConsumer<CancelPaymentConsumer>();

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

                    cfg.ReceiveEndpoint("re.financial.submit.payment",e =>
                    {
                        e.ConcurrentMessageLimit = masstransitConfig.OrderStateMachine.ConcurrentMessageLimit;
                        e.PrefetchCount = masstransitConfig.OrderStateMachine.PrefetchCount;

                        e.ConfigureConsumer<SubmitPaymentConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("re.financial.approve.payment", e =>
                    {
                        e.ConcurrentMessageLimit = masstransitConfig.OrderStateMachine.ConcurrentMessageLimit;
                        e.PrefetchCount = masstransitConfig.OrderStateMachine.PrefetchCount;

                        e.ConfigureConsumer<ApprovePaymentConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("re.financial.request.refund", e =>
                    {
                        e.ConcurrentMessageLimit = masstransitConfig.OrderStateMachine.ConcurrentMessageLimit;
                        e.PrefetchCount = masstransitConfig.OrderStateMachine.PrefetchCount;

                        e.ConfigureConsumer<RequestRefundConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("re.financial.cancel.payment", e =>
                    {
                        e.ConcurrentMessageLimit = masstransitConfig.OrderStateMachine.ConcurrentMessageLimit;
                        e.PrefetchCount = masstransitConfig.OrderStateMachine.PrefetchCount;

                        e.ConfigureConsumer<CancelPaymentConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();

            return services;
        }
    }
}
