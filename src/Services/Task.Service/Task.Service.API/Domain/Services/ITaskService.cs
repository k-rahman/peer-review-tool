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
                Task<IEnumerable<TaskResource>> GetByInstructorIdAsync(string id);
                Task<IEnumerable<TaskResource>> GetByParticipantIdAsync(string id);
                Task<TaskResource> GetByIdAsync(int id);
                Task<TaskResource> GetByUidAsync(Guid uid);
                Task<TaskResponse> InsertAsync(SaveTaskResource task, string instructorId);
                Task<TaskResponse> UpdateAsync(int id, SaveTaskResource task);
                Task<TaskResponse> DeleteAsync(int id);
        }
}