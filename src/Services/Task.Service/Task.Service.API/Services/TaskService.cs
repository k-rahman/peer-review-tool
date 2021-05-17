using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Task.Service.API.Domain.Repositories;
using Task.Service.API.Domain.Services;
using Task.Service.API.Domain.Services.Communication;
using Task.Service.API.Resources;

namespace Task.Service.API.Services
{
        public class TaskService : ITaskService
        {
                private readonly IMapper _mapper;
                private readonly ITaskRepository _taskRepository;
                private readonly IUnitOfWork _unitOfWork;

                public TaskService(IMapper mapper, ITaskRepository taskRepository, IUnitOfWork unitOfWork)
                {
                        _mapper = mapper;
                        _taskRepository = taskRepository;
                        _unitOfWork = unitOfWork;

                }
                public async Task<IEnumerable<TaskResource>> GetAsync()
                {
                        var result = await _taskRepository.GetAsync();
                        var tasks = _mapper.Map<IEnumerable<Domain.Models.Task>, IEnumerable<TaskResource>>(result);
                        return tasks;
                }

                public async Task<TaskResource> GetByIdAsync(Guid id)
                {
                        var result = await _taskRepository.GetByIdAsync(id);
                        return _mapper.Map<Domain.Models.Task, TaskResource>(result);
                }

                public async Task<TaskResponse> InsertAsync(SaveTaskResource resource)
                {
                        var task = _mapper.Map<SaveTaskResource, Domain.Models.Task>(resource);

                        task.Created = DateTimeOffset.Now;
                        task.Modified = DateTimeOffset.Now;

                        try
                        {
                                await _taskRepository.InsertAsync(task);
                                await _unitOfWork.CompleteAsync();

                                var taskResource = _mapper.Map<Domain.Models.Task, TaskResource>(task);

                                return new TaskResponse(taskResource);
                        }
                        catch (Exception ex)
                        {
                                // Do some logging stuff
                                return new TaskResponse($"An error occurred when saving the task: {ex.Message}");
                        }
                }

                public async Task<TaskResponse> UpdateAsync(Guid id, SaveTaskResource task)
                {
                        var existingTask = await _taskRepository.GetByIdAsync(id);

                        if (existingTask == null)
                                return new TaskResponse($"Task with Id {id} was not found.");

                        _mapper.Map<SaveTaskResource, Domain.Models.Task>(task, existingTask);

                        existingTask.Modified = DateTimeOffset.Now;

                        try
                        {
                                await _unitOfWork.CompleteAsync();

                                var updatedTask = _mapper.Map<Domain.Models.Task, TaskResource>(existingTask);

                                return new TaskResponse(updatedTask);
                        }
                        catch (Exception ex)
                        {
                                // Do some logging stuff
                                return new TaskResponse($"An error occurred when updating the task: {ex.Message}");
                        }
                }

                public async Task<TaskResponse> DeleteAsync(Guid id)
                {
                        var existingTask = await _taskRepository.GetByIdAsync(id);

                        if (existingTask == null)
                                return new TaskResponse("Task was not found");

                        try
                        {
                                _taskRepository.Delete(existingTask);
                                await _unitOfWork.CompleteAsync();

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