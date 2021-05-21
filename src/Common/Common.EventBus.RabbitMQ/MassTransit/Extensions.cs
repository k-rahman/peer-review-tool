using System;
using System.Reflection;
using Common.EventBus.RabbitMQ.Settings;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.EventBus.RabbitMQ.MassTransit
{
        public static class Extensions
        {
                public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services, IConfiguration config)
                {
                        services.AddMassTransit(configure =>
                        {
                                configure.AddConsumers(Assembly.GetEntryAssembly());

                                configure.UsingRabbitMq((context, configurator) =>
                                {
                                        var rabbitMQOptions = config.GetSection(RabbitMQOptions.EventBusConnection).Get<RabbitMQOptions>();

                                        configurator.Host(rabbitMQOptions.Host);

                                        configurator.UseMessageRetry(retryConfigurator =>
                                        {
                                                retryConfigurator.Interval(3, TimeSpan.FromSeconds(5));
                                        });
                                });
                        });

                        services.AddMassTransitHostedService();

                        return services;
                }
        }
}