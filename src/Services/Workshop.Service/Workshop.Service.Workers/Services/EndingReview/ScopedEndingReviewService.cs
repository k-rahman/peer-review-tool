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
using System.Text;

namespace Workshop.Service.Workers.Services
{
        internal class ScopedEndingReviewService : IScopedEndingReviewService
        {
                private readonly ILogger<ScopedEndingReviewService> _logger;
                private readonly ConnectionStringsOptions _connectionStrings;
                private readonly PollDateOptions _pollDateOptions;
                private readonly IPublishEndpoint _publishEndpoint;

                public ScopedEndingReviewService(
                        ILogger<ScopedEndingReviewService> logger,
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

                public async Task CheckEndingReviews(CancellationToken stoppingToken)
                {
                        _logger.LogInformation(" -----[ Check for workshops with ending review phase background task ending at: {time} ]----- ", DateTimeOffset.Now);

                        while (!stoppingToken.IsCancellationRequested)
                        {
                                _logger.LogInformation(" -----[ Check for workshops with ending review phase background task is running at: {time} ]----- ", DateTimeOffset.Now);

                                CheckWorkshopsWithEndingReview();

                                await Task.Delay(_pollDateOptions.CheckTime, stoppingToken);
                        }

                        _logger.LogDebug(" -----[ Check for workshops with ending review phase background task is stopping at: {time} ]----- ", DateTimeOffset.Now);
                }
                private void CheckWorkshopsWithEndingReview()
                {
                        _logger.LogInformation(" -----[ EndingReview background task is checking for workshops with ending review phase at: {time} ]----- ", DateTimeOffset.Now);

                        var workshops = GetWorkshopsWithEndingReview();

                        foreach (var workshop in workshops)
                        {
                                _logger.LogInformation(" -----[ Publishing integration event: {integrationEvent} from {ServiceName} ]----- ", nameof(ReviewEnding), nameof(this.CheckEndingReviews));

                                _publishEndpoint.Publish<ReviewEnding>(new
                                {
                                        workshop.Id,
                                        workshop.Uid,
                                        workshop.ReviewEnd,
                                        workshop.InstructorId,
                                        workshop.Participants
                                });
                        }
                }

                private IEnumerable<WorkshopModel> GetWorkshopsWithEndingReview()
                {
                        // check for review ending in 121 to 120 mins
                        var sql = @"SELECT t.id, t.uid, t.review_end ""ReviewEnd"", t.instructor_id AS ""InstructorId"", 
					p.auth0_id AS ""Auth0Id"", p.email FROM workshop_participants tp 
					INNER JOIN workshops t ON t.id = tp.workshop_id 
					INNER JOIN participants p ON p.id = tp.participant_id
					WHERE(EXTRACT(EPOCH FROM review_end) - EXTRACT(EPOCH FROM now())) / 60 >= 120
					AND (EXTRACT(EPOCH FROM review_end) - EXTRACT(EPOCH FROM now())) / 60 <= 121";

                        var workshops = new Dictionary<int, WorkshopModel>();
                        var workshopsWithEndingReview = new List<WorkshopModel>();
                        using (var conn = new NpgsqlConnection(_connectionStrings.WorkshopWorkers))
                        {
                                try
                                {
                                        conn.Open();
                                        workshopsWithEndingReview =
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
                                                _logger.LogInformation(" ----[ Fetched {0} Workshops with ending review phase from database ] ----- ", workshops.Count());
                                        else
                                                _logger.LogInformation(" ----[ No Workshops with ending review phase yet! ] ----- ");

                                }
                                catch (SqlException exception)
                                {
                                        _logger.LogCritical(exception, "FATAL ERROR: Database connections could not be opened: {Message}", exception.Message);
                                }
                        }

                        return workshopsWithEndingReview;
                }
        }
}