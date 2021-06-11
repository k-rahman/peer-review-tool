using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Workshop.Service.Workers.Interfaces;

namespace Workshop.Service.Workers.Services
{
        public class StartedSubmissionHostedService : BackgroundService
        {
                private readonly ILogger<StartedSubmissionHostedService> _logger;
                public IServiceProvider _services { get; }

                public StartedSubmissionHostedService(
                        IServiceProvider services,
                        ILogger<StartedSubmissionHostedService> logger
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
                                        .GetRequiredService<IScopedStartedSubmissionService>();

                                await scopedProcessingService.CheckStartedSubmissions(stoppingToken);
                        }
                }

        }
}