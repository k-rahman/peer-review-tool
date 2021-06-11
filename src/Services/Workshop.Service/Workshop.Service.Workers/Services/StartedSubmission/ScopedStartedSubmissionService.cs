using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;
using Workshop.Service.Workers.Interfaces;
using Workshop.Service.Workers.Models;
using Workshop.Service.Workers.Settings;
using Workshop.Service.Contracts;

namespace Workshop.Service.Workers.Services
{
        internal class ScopedStartedSubmissionService : IScopedStartedSubmissionService
        {
                private readonly ILogger<ScopedStartedSubmissionService> _logger;
                private readonly ConnectionStringsOptions _connectionStrings;
                private readonly PollDateOptions _pollDateOptions;
                private readonly IPublishEndpoint _publishEndpoint;

                public ScopedStartedSubmissionService(
                        ILogger<ScopedStartedSubmissionService> logger,
                        IOptions<ConnectionStringsOptions> connectionStringsOptions,
                        IOptions<PollDateOptions> pollDateOptions,
                        IPublishEndpoint publishEndpoint
                 )
                {
                        _logger = logger;
                        _connectionStrings = connectionStringsOptions.Value;
                        _pollDateOptions = pollDateOptions.Value;
                        _publishEndpoint = publishEndpoint;
                }

                public async Task CheckStartedSubmissions(CancellationToken stoppingToken)
                {
                        _logger.LogInformation(" -----[ Check for workshops with started submission phase background task started at: {time} ]----- ", DateTimeOffset.Now);

                        while (!stoppingToken.IsCancellationRequested)
                        {
                                _logger.LogInformation(" -----[ Check for workshops with started submission phase background task is running at: {time} ]----- ", DateTimeOffset.Now);

                                CheckWorkshopsWithStartedSubmission();

                                await Task.Delay(_pollDateOptions.CheckTime, stoppingToken);
                        }

                        _logger.LogDebug(" -----[ Check for workshops with started submission phase background task is stopping at: {time} ]----- ", DateTimeOffset.Now);
                }
                private void CheckWorkshopsWithStartedSubmission()
                {
                        _logger.LogInformation(" -----[ SubmissionStarted background task is checking for workshops with started submission phase at: {time} ]----- ", DateTimeOffset.Now);

                        var workshops = GetWorkshopsWithStartedSubmission();

                        foreach (var workshop in workshops)
                        {
                                _logger.LogInformation(" -----[ Publishing integration event: {integrationEvent} from {ServiceName} ]----- ", nameof(SubmissionStarted), nameof(this.CheckWorkshopsWithStartedSubmission));

                                _publishEndpoint.Publish<SubmissionStarted>(new
                                {
                                        workshop.Id,
                                        workshop.Uid,
                                        workshop.SubmissionStart,
                                        workshop.InstructorId,
                                        workshop.Participants
                                });
                        }
                }

                private IEnumerable<WorkshopModel> GetWorkshopsWithStartedSubmission()
                {
                        // check for submission started 1 min ago
                        var sql = @"SELECT t.id, t.uid, t.submission_start ""SubmissionStart"", 
                                        t.instructor_id AS ""InstructorId"", p.auth0_id AS ""Auth0Id"", p.email FROM workshop_participants tp
                                        INNER JOIN workshops t ON t.id = tp.workshop_id
                                        INNER JOIN participants p ON p.id = tp.participant_id
                                        WHERE(EXTRACT(EPOCH FROM now()) - EXTRACT(EPOCH FROM submission_start)) / 60 >= 0 AND
                                        (EXTRACT(EPOCH FROM now()) - EXTRACT(EPOCH FROM submission_start)) / 60 <= 1";

                        var workshops = new Dictionary<int, WorkshopModel>();
                        var workshopsWithStartedSubmission = new List<WorkshopModel>();
                        using (var conn = new NpgsqlConnection(_connectionStrings.Default))
                        {
                                try
                                {
                                        conn.Open();
                                        workshopsWithStartedSubmission =
                                                conn.Query<WorkshopModel, ParticipantModel, WorkshopModel>(
                                                        sql,
                                                        (workshop, participant) =>
                                                        {
                                                                WorkshopModel newWorkshop;

                                                                if (!workshops.TryGetValue(workshop.Id, out newWorkshop))
                                                                {
                                                                        newWorkshop = workshop;
                                                                        newWorkshop.Participants = new List<ParticipantModel>();
                                                                        workshops.Add(workshop.Id, newWorkshop);
                                                                }

                                                                newWorkshop.Participants.Add(participant);
                                                                return newWorkshop;
                                                        }, splitOn: "id, Auth0Id"
                                        ).Distinct().ToList();

                                        if (workshops.Count() > 0)
                                                _logger.LogInformation(" ----[ Fetched {0} Workshops with started submission phase from database ] ----- ", workshops.Count());
                                        else
                                                _logger.LogInformation(" ----[ No Workshops with started submission phase yet! ] ----- ");

                                }
                                catch (SqlException exception)
                                {
                                        _logger.LogCritical(exception, "FATAL ERROR: Database connections could not be opened: {Message}", exception.Message);
                                }
                        }

                        return workshopsWithStartedSubmission;
                }
        }
}