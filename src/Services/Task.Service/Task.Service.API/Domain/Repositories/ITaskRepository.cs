using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Task.Service.API.Domain.Repositories
{
        public interface ITaskRepository
        {
                Task<IEnumerable<Domain.Models.Task>> GetAsync();
                Task<Domain.Models.Task> GetByIdAsync(int id);
                Task<Domain.Models.Task> GetByUidAsync(Guid uid);
                System.Threading.Tasks.Task InsertAsync(Domain.Models.Task task);
                void Delete(Domain.Models.Task task);
        }
}