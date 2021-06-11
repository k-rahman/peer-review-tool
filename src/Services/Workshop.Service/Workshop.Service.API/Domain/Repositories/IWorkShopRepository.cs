using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Workshop.Service.API.Domain.Repositories
{
        public interface IWorkshopRepository
        {
                Task<IEnumerable<Domain.Models.Workshop>> GetAsync();
                Task<IEnumerable<Domain.Models.Workshop>> GetByInstructorIdAsync(string id);
                Task<IEnumerable<Domain.Models.Workshop>> GetByParticipantIdAsync(string id);
                Task<Domain.Models.Workshop> GetByIdAsync(int id);
                Task<Domain.Models.Workshop> GetByUidAsync(Guid uid);
                System.Threading.Tasks.Task InsertAsync(Domain.Models.Workshop workshop);
                void Delete(Domain.Models.Workshop workshop);
        }
}