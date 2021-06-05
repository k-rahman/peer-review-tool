using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using Dapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;
using Task.Service.Contracts;
using Task.Service.Workers.Interfaces;
using Task.Service.Workers.Models;
using Task.Service.Workers.Settings;

namespace Task.Service.Workers.Services
{
        internal class ScopedPublishedDateService : IScopedProcessingService
        {
                private readonly ILogger<ScopedPublishedDateService> _logger;
                private readonly ConnectionStringsOptions _connectionStrings;
                private readonly PublishedDateOptions _publishedDateOptions;
                private readonly IPublishEndpoint _publishEndpoint;

                public ScopedPublishedDateService(
                        ILogger<ScopedPublishedDateService> logger,
                        IOptions<ConnectionStringsOptions> connectionStringsOptions,
                        IOptions<PublishedDateOptions> publishedDateOptions,
                        IPublishEndpoint publishEndpoint
                 )
                {
                        _logger = logger;
                        _connectionStrings = connectionStringsOptions.Value;
                        _publishedDateOptions = publishedDateOptions.Value;
                        _publishEndpoint = publishEndpoint;
                }

                public async System.Threading.Tasks.Task DoWork(CancellationToken stoppingToken)
                {
                        _logger.LogInformation(" -----[ Check for published workshops background task started at: {time} ]----- ", DateTimeOffset.Now);

                        while (!stoppingToken.IsCancellationRequested)
                        {
                                _logger.LogInformation(" -----[ Check for published workshops background task is running at: {time} ]----- ", DateTimeOffset.Now);

                                CheckPublishedTasks();

                                await System.Threading.Tasks.Task.Delay(_publishedDateOptions.CheckTime, stoppingToken);
                        }

                        _logger.LogDebug(" -----[ Check for published workshops background task is stopping at: {time} ]----- ", DateTimeOffset.Now);
                }
                private void CheckPublishedTasks()
                {
                        _logger.LogInformation(" -----[ Now at: {time} background task is checking for published tasks ]----- ", DateTimeOffset.Now);

                        var workshops = GetPublishedWorkshops();

                        foreach (var workshop in workshops)
                        {
                                _logger.LogInformation(" -----[ Publishing integration event: {integrationEvent} from {ServiceName} ]----- ", nameof(WorkshopPublished), nameof(this.CheckPublishedTasks));

                                _publishEndpoint.Publish<WorkshopPublished>(new
                                {
                                        workshop.id,
                                        workshop.Uid,
                                        workshop.InstructorId,
                                        workshop.Participants
                                });
                        }
                }

                private IEnumerable<Workshop> GetPublishedWorkshops()
                {
                        IEnumerable<Workshop> workshops = new List<Workshop>();

                        using (var conn = new NpgsqlConnection(_connectionStrings.Default))
                        {
                                try
                                {
                                        conn.Open();
                                        workshops = conn.Query<Workshop>(
                                                @"SELECT t.id, uid, instructor_id, auth0_id, email FROM task_participants tp
                                                        INNER JOIN tasks t ON t.id = tp.task_id
                                                        INNER JOIN participants p ON p.id = tp.participant_id
                                                        WHERE (EXTRACT(EPOCH FROM now()) - EXTRACT(EPOCH FROM published)) / 60 >= 0 AND 
                                                        (EXTRACT(EPOCH FROM now()) - EXTRACT(EPOCH FROM published)) / 60 <= 1"
                                        );

                                        _logger.LogInformation(" ----[ Fetched {0} Published Workshops from database ] ----- ", workshops.Count());
                                }
                                catch (SqlException exception)
                                {
                                        _logger.LogCritical(exception, "FATAL ERROR: Database connections could not be opened: {Message}", exception.Message);
                                }
                        }

                        return workshops;
                }
        }
}