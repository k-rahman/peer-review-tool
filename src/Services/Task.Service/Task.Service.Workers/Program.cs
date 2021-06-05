using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.EventBus.RabbitMQ.MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Task.Service.Workers.Interfaces;
using Task.Service.Workers.Services;
using Task.Service.Workers.Settings;

namespace Task.Service.Workers
{
        public class Program
        {
                public static void Main(string[] args)
                {
                        CreateHostBuilder(args).Build().Run();
                }

                public static IHostBuilder CreateHostBuilder(string[] args) =>
                    Host.CreateDefaultBuilder(args)
                        .ConfigureServices((hostContext, services) =>
                        {
                                services.AddHostedService<PublishedDateHostedService>();
                                services.AddScoped<IScopedProcessingService, ScopedPublishedDateService>();

                                services.Configure<ConnectionStringsOptions>(
                                        hostContext.Configuration.GetSection(ConnectionStringsOptions.ConnectionStrings)
                                );

                                services.Configure<PublishedDateOptions>(
                                        hostContext.Configuration.GetSection(PublishedDateOptions.CheckTimes)
                                );

                                services.AddMassTransitWithRabbitMq(hostContext.Configuration);
                        });
        }
}
