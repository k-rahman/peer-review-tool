using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using AutoMapper;
using MassTransit;
using RestSharp;
using Task.Service.API.Domain.Repositories;
using Task.Service.API.Domain.Services;
using Task.Service.API.Domain.Services.Communication;
using Task.Service.API.Resources;
using Task.Service.Contracts;

namespace Task.Service.API.Services
{
        public class TaskService : ITaskService
        {
                private readonly IMapper _mapper;
                private readonly ITaskRepository _taskRepository;
                private readonly IUnitOfWork _unitOfWork;
                private readonly IPublishEndpoint _publishEndpoint;

                public TaskService(IMapper mapper, ITaskRepository taskRepository, IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint)
                {
                        _mapper = mapper;
                        _taskRepository = taskRepository;
                        _unitOfWork = unitOfWork;
                        _publishEndpoint = publishEndpoint;
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
                        var client = new RestClient("https://peer-review-tool.eu.auth0.com/oauth/token");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("content-type", "application/json");
                        request.AddParameter("application/json", "{\"client_id\":\"qGZpmZNT3i6q42ioFUT8Y0NyT7RqDs5a\",\"client_secret\":\"DGiiQaVdHvEHmPpxq1kKT1yKbQbFPu0ED9zNGEXD60h0oPTP9LH_L3DWG2b9V0Ru\",\"audience\":\"https://peer-review-tool.eu.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);

                        // var re = JsonConvert.(response.Content);
                        var result = JsonSerializer.Deserialize<Response>(response.Content);

                        // var client = new AuthenticationApiClient("https://peer-review-tool.eu.auth0.com");
                        // var client_credentials = new ClientCredentialsTokenRequest() { Audience = "https://peer-review-tool.eu.auth0.com/api/v2/", ClientId = "ydPIXFxKbaD7gklx81XrFCjSfLozwpob", ClientSecret = "cHQ3I0ICZgsVErYr7F5c9jkZKM7ca3Ne8abNwN5hMJwHI5fS47xZ2YgYe-z_AdLo" };
                        // try
                        // {
                        //         var token = await client.GetTokenAsync(client_credentials);
                        // }
                        // catch (Exception e)
                        // {

                        // };


                        // object result = JsonConvert.DeserializeObject(response.Content);

                        // create accounts using emails in "ParticipantsEmails" from "SaveTaskResource"
                        using (var reader = new StreamReader(resource.ParticipantsEmails.OpenReadStream()))
                        {
                                while (reader.Peek() >= 0)
                                {
                                        var line = reader.ReadLine();
                                        char[] delimiters = new char[] { ';', ',' };
                                        var email = line.Split(delimiters)[0];
                                        var user = new
                                        {
                                                email = email,
                                                password = "XWoshogongfu1234@",
                                                connection = "Username-Password-Authentication"
                                        };

                                        var newClient = new RestClient("https://peer-review-tool.eu.auth0.com/api/v2/users");
                                        var newRequest = new RestRequest(Method.POST);
                                        newRequest.AddHeader("content-type", "application/json");
                                        newRequest.AddHeader("authorization", $"Bearer {result.access_token}");
                                        newRequest.AddJsonBody(user);
                                        IRestResponse newResponse = newClient.Execute(newRequest);

                                        // var managementApiClient =
                                        //         new ManagementApiClient(
                                        //                 result.access_token,
                                        //                 "https://peer-review-tool.eu.auth0.com"
                                        //                 );
                                        // var newUser = new UserCreateRequest() { Email = email, Password = "XWoshogongfu1234@", Connection = "Username=Password-Authentication" };

                                        // await managementApiClient.Users.CreateAsync(newUser);

                                }
                        };
                        var task = _mapper.Map<SaveTaskResource, Domain.Models.Task>(resource);

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

        public record Response(string access_token);
}