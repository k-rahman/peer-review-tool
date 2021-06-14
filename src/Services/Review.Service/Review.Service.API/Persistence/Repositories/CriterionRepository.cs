using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Review.Service.API.Domain.Models;
using Review.Service.API.Domain.Repositories;
using Review.Service.API.Persistence.Contexts;

namespace Review.Service.API.Persistence.Repositories
{
        public class CriterionRepository : BaseRepository, ICriterionRepository
        {

                public CriterionRepository(ReviewContext context) : base(context)
                {
                }

                public async Task<Criterion> GetByIdAsync(int id)
                {
                        return await _context.Criteria.FindAsync(id);
                }
                public IEnumerable<Criterion> GetByWorkshopUid(Guid workshopUid)
                {
                        return _context.Criteria.Where(c => c.WorkshopUid == workshopUid).ToList();
                }
                public async System.Threading.Tasks.Task InsertAsync(Criterion criterion)
                {
                        await _context.Criteria.AddAsync(criterion);
                }
        }
}