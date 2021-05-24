using System.Collections.Generic;
using System.Threading.Tasks;

namespace Work.Service.API.Domain.Repositories
{
        public interface IWorkRepository
        {
                Task<IEnumerable<Domain.Models.Work>> GetAsync();
                Task<Domain.Models.Work> GetByIdAsync(int id);
        }
}