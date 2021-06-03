using System;
using System.Collections.Generic;
using System.Linq;
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

                public async Task<Domain.Models.Work> GetAuthorWorkByTaskAsync(Guid taskUid, int authorId)
                {
                        return await _context.Works
                                .Include(work => work.WorksDeadline)
                                .Where(work => work.AuthorId == authorId && work.WorksDeadline.Uid == taskUid)
                                .SingleOrDefaultAsync();
                }

                public async Task<Domain.Models.Work> GetByIdAsync(int id)
                {
                        return await _context.Works.Include(work => work.WorksDeadline)
                                                .SingleOrDefaultAsync(work => work.Id == id);
                }
        }
}