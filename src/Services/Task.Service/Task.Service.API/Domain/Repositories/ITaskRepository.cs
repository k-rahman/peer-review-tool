using System.Collections.Generic;
using System.Threading.Tasks;

namespace Task.Service.API.Domain.Repositories
{
  public interface ITaskRepository
  {
    Task<IEnumerable<Domain.Models.Task>> ListAsync();
  }
}