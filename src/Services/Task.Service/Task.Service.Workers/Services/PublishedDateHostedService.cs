using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Task.Service.Workers.Settings;
using Task.Service.Workers.Models;
using Microsoft.Extensions.DependencyInjection;
using Task.Service.Workers.Interfaces;

namespace Task.Service.Workers.Services
{
        public class PublishedDateHostedService : BackgroundService
        {
                private readonly ILogger<PublishedDateHostedService> _logger;
                public IServiceProvider _services { get; }

                public PublishedDateHostedService(
                        IServiceProvider services,
                        ILogger<PublishedDateHostedService> logger
                    )
                {
                        _services = services;
                        _logger = logger;
                }

                protected override async System.Threading.Tasks.Task ExecuteAsync(CancellationToken stoppingToken)
                {
                        _logger.LogInformation(" -----[ Consume Scoped Service Hosted Service is running ]----- ");

                        await DoWork(stoppingToken);
                }

                private async System.Threading.Tasks.Task DoWork(CancellationToken stoppingToken)
                {
                        _logger.LogInformation(" ----[ Consuming Scoped Service Hosted Service is doing work ]----- ");

                        using (var scope = _services.CreateScope())
                        {
                                var scopedProcessingService =
                                    scope.ServiceProvider
                                        .GetRequiredService<IScopedProcessingService>();

                                await scopedProcessingService.DoWork(stoppingToken);
                        }
                }

        }
}

// await _publishEndpoint.Publish<TaskCreated>(new
// {
//         task.Id,
//         task.Uid,
//         task.SubmissionStart,
//         task.SubmissionEnd,
//         task.ReviewStart,
//         task.ReviewEnd,
//         task.InstructorId,
//         task.Criteria
// });