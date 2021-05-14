using System.Collections.Generic;
using System.Threading.Tasks;

namespace Task.Service.API.Domain.Services
{
  public interface ITaskService
  {
    Task<IEnumerable<Domain.Models.Task>> ListAsync();
  }
}