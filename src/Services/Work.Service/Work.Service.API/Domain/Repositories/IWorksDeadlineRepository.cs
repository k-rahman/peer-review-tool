using System.Collections.Generic;
using System.Threading.Tasks;

namespace Work.Service.API.Domain.Repositories
{
        public interface IWorksDeadlineRepository
        {
                Task<Domain.Models.WorksDeadline> GetByIdAsync(int id);
                System.Threading.Tasks.Task InsertAsync(Domain.Models.WorksDeadline worksDeadline);
        }
}