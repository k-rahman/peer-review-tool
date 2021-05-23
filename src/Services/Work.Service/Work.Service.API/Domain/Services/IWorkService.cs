using System.Collections.Generic;
using System.Threading.Tasks;
using Work.Service.API.Domain.Services.Communication;
using Work.Service.API.Resources;

namespace Work.Service.API.Domain.Services
{
        public interface IWorkService
        {
                Task<IEnumerable<WorkResource>> GetAsync();
                Task<WorkResource> GetByIdAsync(int id);
                Task<WorkResponse> UpdateAsync(int id, SaveWorkResource work);
        }
}