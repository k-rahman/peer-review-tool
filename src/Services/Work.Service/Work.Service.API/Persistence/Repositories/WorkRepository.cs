using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Work.Service.API.Domain.Repositories;
using Work.Service.API.Persistence.Contexts;

namespace Work.Service.API.Persistence.Repositories
{
        public class WorkRepository : BaseRepository, IWorkRepository
        {

                public WorkRepository(WorkContext context) : base(context)
                {
                }

                public async Task<IEnumerable<Domain.Models.Work>> GetAsync()
                {
                        return await _context.Works.Include(work => work.WorksDeadline).ToListAsync();
                }

                public async Task<Domain.Models.Work> GetByIdAsync(int id)
                {
                        return await _context.Works.Include(work => work.WorksDeadline)
                                                .SingleOrDefaultAsync(work => work.Id == id);
                }
        }
}