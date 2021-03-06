using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Review.Service.API.Domain.Repositories;
using Review.Service.API.Persistence.Contexts;

namespace Review.Service.API.Persistence.Repositories
{
        public class SubmissionRepository : BaseRepository, ISubmissionRepository
        {

                public SubmissionRepository(ReviewContext context) : base(context)
                {
                }

                public async Task<Domain.Models.Submission> GetByIdAsync(int id)
                {
                        return await _context.Submissions.FindAsync(id);
                }

                public IEnumerable<Domain.Models.Submission> GetByWorkshopUid(Guid workshopUid)
                {
                        return _context.Submissions.Where(s => s.WorkshopUid == workshopUid).ToList();
                }

                public async Task InsertAsync(Domain.Models.Submission submission)
                {
                        await _context.Submissions.AddAsync(submission);
                }
        }
}