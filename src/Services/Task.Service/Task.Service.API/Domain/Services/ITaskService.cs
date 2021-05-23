using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task.Service.API.Domain.Services.Communication;
using Task.Service.API.Resources;

namespace Task.Service.API.Domain.Services
{
        public interface ITaskService
        {
                Task<IEnumerable<TaskResource>> GetAsync();
                Task<TaskResource> GetByIdAsync(int id);
                Task<TaskResource> GetByUidAsync(Guid uid);
                Task<TaskResponse> InsertAsync(SaveTaskResource task);
                Task<TaskResponse> UpdateAsync(int id, SaveTaskResource task);
                Task<TaskResponse> DeleteAsync(int id);
        }
}