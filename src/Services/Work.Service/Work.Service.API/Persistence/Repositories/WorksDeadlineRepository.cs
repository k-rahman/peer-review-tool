
using System.Threading.Tasks;
using Work.Service.API.Domain.Repositories;
using Work.Service.API.Persistence.Contexts;

namespace Work.Service.API.Persistence.Repositories
{
        public class WorksDeadlineRepository : BaseRepository, IWorksDeadlineRepository
        {

                public WorksDeadlineRepository(WorkContext context) : base(context)
                {
                }

                public async Task<Domain.Models.WorksDeadline> GetByIdAsync(int id)
                {
                        return await _context.WorksDealines.FindAsync(id);
                }
                public async System.Threading.Tasks.Task InsertAsync(Domain.Models.WorksDeadline worksDeadline)
                {
                        await _context.WorksDealines.AddAsync(worksDeadline);
                }
        }
}