using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
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

                public async Task<TaskResponse> InsertAsync(SaveTaskResource resource)
                {
                        var task = _mapper.Map<SaveTaskResource, Domain.Models.Task>(resource);

                        task.Uid = Guid.NewGuid();
                        task.Created = DateTimeOffset.Now;

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
                                        task.InstructorId
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