using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Workshop.Service.Workers.Interfaces;

namespace Workshop.Service.Workers.Services
{
        public class StartedReviewHostedService : BackgroundService
        {
                private readonly ILogger<StartedReviewHostedService> _logger;
                public IServiceProvider _services { get; }

                public StartedReviewHostedService(
                        IServiceProvider services,
                        ILogger<StartedReviewHostedService> logger
                    )
                {
                        _services = services;
                        _logger = logger;
                }

                protected override async Task ExecuteAsync(CancellationToken stoppingToken)
                {
                        _logger.LogInformation(" -----[ Consume Scoped Service Hosted Service is running ]----- ");

                        await DoWork(stoppingToken);
                }

                private async Task DoWork(CancellationToken stoppingToken)
                {
                        _logger.LogInformation(" ----[ Consuming Scoped Service Hosted Service is doing work ]----- ");

                        using (var scope = _services.CreateScope())
                        {
                                var scopedProcessingService =
                                    scope.ServiceProvider
                                        .GetRequiredService<IScopedStartedReviewService>();

                                await scopedProcessingService.CheckStartedReviews(stoppingToken);
                        }
                }

        }
}