using System.Collections.Generic;
using System.Threading.Tasks;
using Work.Service.API.Domain.Services.Communication;

namespace Work.Service.API.Domain.Repositories
{
        public interface IWorkRepository
        {
                Task<IEnumerable<Domain.Models.Work>> GetAsync();
                Task<Domain.Models.Work> GetByIdAsync(int id);
        }
}