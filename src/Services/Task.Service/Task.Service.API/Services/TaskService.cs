using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using RestSharp;
using Task.Service.API.Domain.Models;
using Task.Service.API.Domain.Repositories;
using Task.Service.API.Domain.Services;
using Task.Service.API.Domain.Services.Communication;
using Task.Service.API.Resources;
using Task.Service.API.Utils;
using Task.Service.Contracts;

namespace Task.Service.API.Services
{
        public class TaskService : ITaskService
        {
                private readonly IMapper _mapper;
                private readonly ITaskRepository _taskRepository;
                private readonly IParticipantRepository _participantRepository;
                private readonly IUnitOfWork _unitOfWork;
                private readonly IPublishEndpoint _publishEndpoint;
                private readonly ManagementApiAccessTokenClient _apiAccessTokenClient;
                private record _participant(string user_id);

                public TaskService(
                        IMapper mapper,
                        ITaskRepository taskRepository,
                        IParticipantRepository participantRepository,
                        IUnitOfWork unitOfWork,
                        IPublishEndpoint publishEndpoint,
                        ManagementApiAccessTokenClient apiAccessTokenClient
                        )
                {
                        _mapper = mapper;
                        _taskRepository = taskRepository;
                        _participantRepository = participantRepository;
                        _unitOfWork = unitOfWork;
                        _publishEndpoint = publishEndpoint;
                        _apiAccessTokenClient = apiAccessTokenClient;
                }
                public async Task<IEnumerable<TaskResource>> GetAsync()
                {
                        var result = await _taskRepository.GetAsync();
                        var tasks = _mapper.Map<IEnumerable<Domain.Models.Task>, IEnumerable<TaskResource>>(result);
                        return tasks;
                }

                public async Task<IEnumerable<TaskResource>> GetByInstructorIdAsync(string id)
                {
                        var result = await _taskRepository.GetByInstructorIdAsync(id);
                        var tasks = _mapper.Map<IEnumerable<Domain.Models.Task>, IEnumerable<TaskResource>>(result);
                        return tasks;
                }
                public async Task<IEnumerable<TaskResource>> GetByParticipantIdAsync(string id)
                {
                        var result = await _taskRepository.GetByParticipantIdAsync(id);
                        var tasks = _mapper.Map<IEnumerable<Domain.Models.Task>, IEnumerable<TaskResource>>(result);
                        return tasks;
                }

                public async Task<TaskResource> GetByIdAsync(int id)
                {
                        var result = await _taskRepository.GetByIdAsync(id);
                        return _mapper.Map<Domain.Models.Task, TaskResource>(result);
                }

                public async Task<TaskResource> GetByUidAsync(Guid uid)
                {
                        var result = await _taskRepository.GetByUidAsync(uid);
                        return _mapper.Map<Domain.Models.Task, TaskResource>(result);
                }

                public async Task<TaskResponse> InsertAsync(SaveTaskResource resource, string instructorId)
                {

                        var task = _mapper.Map<SaveTaskResource, Domain.Models.Task>(resource);
                        var managementApiAccessToken = await _apiAccessTokenClient.GetApiToken();
                        var emails = new List<string>();
                        char[] delimiters = new char[] { ';', ',' };
                        var participants = new List<string>();

                        var client = new RestClient("https://peer-review-tool.eu.auth0.com/api/v2/users");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("authorization", $"Bearer {managementApiAccessToken}");


                        // create accounts using emails in "ParticipantsEmails" from "SaveTaskResource"
                        using (var reader = new StreamReader(resource.ParticipantsEmails.OpenReadStream()))
                        {
                                while (reader.Peek() >= 0)
                                {
                                        var line = reader.ReadLine();
                                        emails.Add(line.Split(delimiters)[0]);
                                }
                        };

                        emails.ForEach(email =>
                        {
                                // use email to check if participant already exists in our database
                                var existingParticipant = _participantRepository.GetByEmail(email);

                                // if it exists, add to task participants
                                if (existingParticipant != null)
                                {
                                        task.Participants.Add(existingParticipant);
                                }
                                // if it doesn't exists, create an account and add it to task participants
                                else
                                {
                                        var password = RandomPasswordGenerator.GeneratePassword(16, 1);
                                        var user = new
                                        {
                                                email = email,
                                                password = password,
                                                connection = "Username-Password-Authentication"
                                        };

                                        request.AddJsonBody(user);
                                        IRestResponse response = client.Execute(request);

                                        if (response.IsSuccessful)
                                        {
                                                var participant = System.Text.Json.JsonSerializer.Deserialize<_participant>(response.Content);
                                                task.Participants.Add(new Participant { auth0Id = participant.user_id, email = email });
                                        }
                                }
                        });


                        task.Uid = Guid.NewGuid();
                        task.Created = DateTimeOffset.Now;
                        task.InstructorId = instructorId;

                        try
                        {
                                await _taskRepository.InsertAsync(task);
                                await _unitOfWork.CompleteAsync();

                                await _publishEndpoint.Publish<TaskCreated>(new
                                {
                                        task.Id,
                                        task.Uid,
                                        task.SubmissionStart,
                                        task.SubmissionEnd,
                                        task.ReviewStart,
                                        task.ReviewEnd,
                                        task.InstructorId,
                                        task.Criteria
                                });

                                var taskResource = _mapper.Map<Domain.Models.Task, TaskResource>(task);

                                return new TaskResponse(taskResource);
                        }
                        catch (Exception ex)
                        {
                                // Do some logging stuff
                                return new TaskResponse($"An error occurred when saving the task: {ex.Message}");
                        }
                }

                public async Task<TaskResponse> UpdateAsync(int id, SaveTaskResource task)
                {
                        var existingTask = await _taskRepository.GetByIdAsync(id);

                        if (existingTask == null)
                                return new TaskResponse($"Task with Id {id} was not found.");

                        _mapper.Map<SaveTaskResource, Domain.Models.Task>(task, existingTask);

                        existingTask.Modified = DateTimeOffset.Now;

                        try
                        {
                                await _unitOfWork.CompleteAsync();

                                await _publishEndpoint.Publish<TaskUpdated>(new
                                {
                                        existingTask.Id,
                                        existingTask.Uid,
                                        existingTask.SubmissionStart,
                                        existingTask.SubmissionEnd,
                                        existingTask.ReviewStart,
                                        existingTask.ReviewEnd
                                });

                                var updatedTask = _mapper.Map<Domain.Models.Task, TaskResource>(existingTask);

                                return new TaskResponse(updatedTask);
                        }
                        catch (Exception ex)
                        {
                                // Do some logging stuff
                                return new TaskResponse($"An error occurred when updating the task: {ex.Message}");
                        }
                }

                public async Task<TaskResponse> DeleteAsync(int id)
                {
                        var existingTask = await _taskRepository.GetByIdAsync(id);

                        if (existingTask == null)
                                return new TaskResponse("Task was not found");

                        try
                        {
                                _taskRepository.Delete(existingTask);
                                await _unitOfWork.CompleteAsync();

                                await _publishEndpoint.Publish<TaskDeleted>(new
                                {
                                        existingTask.Id,
                                        existingTask.Uid
                                });

                                var removedTask = _mapper.Map<Domain.Models.Task, TaskResource>(existingTask);

                                return new TaskResponse(removedTask);
                        }
                        catch (Exception ex)
                        {
                                // Do some logging stuff
                                return new TaskResponse($"An error occurred when updating the task: {ex.Message}");
                        }
                }
        }
}