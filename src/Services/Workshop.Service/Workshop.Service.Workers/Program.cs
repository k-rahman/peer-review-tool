using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.EventBus.RabbitMQ.MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Workshop.Service.Workers.Interfaces;
using Workshop.Service.Workers.Services;
using Workshop.Service.Workers.Settings;

namespace Workshop.Service.Workers
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
                                services.AddHostedService<PublishedWorkshopHostedService>();
                                services.AddHostedService<StartedSubmissionHostedService>();
                                services.AddHostedService<StartedReviewHostedService>();
                                services.AddHostedService<EndingSubmissionHostedService>();
                                services.AddHostedService<EndingReviewHostedService>();

                                services.AddScoped<IScopedPublishedWorkshopService, ScopedPublishedDateService>();
                                services.AddScoped<IScopedStartedSubmissionService, ScopedStartedSubmissionService>();
                                services.AddScoped<IScopedStartedReviewService, ScopedStartedReviewService>();
                                services.AddScoped<IScopedEndingSubmissionService, ScopedEndingSubmissionService>();
                                services.AddScoped<IScopedEndingReviewService, ScopedEndingReviewService>();

                                services.Configure<ConnectionStringsOptions>(
                                        hostContext.Configuration.GetSection(ConnectionStringsOptions.ConnectionStrings)
                                );

                                services.Configure<PollDateOptions>(
                                        hostContext.Configuration.GetSection(PollDateOptions.CheckTimes)
                                );

                                services.AddMassTransitWithRabbitMq(hostContext.Configuration);
                        });
        }
}
