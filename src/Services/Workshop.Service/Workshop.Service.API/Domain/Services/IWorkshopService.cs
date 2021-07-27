using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Workshop.Service.API.Domain.Services.Communication;
using Workshop.Service.API.Resources;

namespace Workshop.Service.API.Domain.Services
{
        public interface IWorkshopService
        {
                Task<IEnumerable<WorkshopResource>> GetAsync();
                Task<IEnumerable<WorkshopResource>> GetByInstructorIdAsync(string id);
                IEnumerable<WorkshopResource> GetByParticipantId(string id);
                Task<WorkshopResource> GetByIdAsync(int id);
                Task<WorkshopResource> GetByUidAsync(Guid uid);
                Task<WorkshopResponse> InsertAsync(SaveWorkshopResource workshop, string instructorId);
                Task<WorkshopResponse> UpdateAsync(int id, SaveWorkshopResource workshop);
                Task<WorkshopResponse> DeleteAsync(int id);
        }
}