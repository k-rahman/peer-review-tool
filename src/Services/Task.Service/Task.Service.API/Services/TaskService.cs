using System.Collections.Generic;
using System.Threading.Tasks;
using Task.Service.API.Domain.Repositories;
using Task.Service.API.Domain.Services;

namespace Task.Service.API.Services
{
  public class TaskService : ITaskService
  {
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
      _taskRepository = taskRepository;

    }
    public async Task<IEnumerable<Domain.Models.Task>> ListAsync()
    {
      return await _taskRepository.ListAsync();
    }
  }
}