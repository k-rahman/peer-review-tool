using System;
using System.Linq;
using System.Threading.Tasks;
using Submission.Service.API.Domain.Models;
using Submission.Service.API.Domain.Repositories;
using Submission.Service.API.Persistence.Contexts;

namespace Submission.Service.API.Persistence.Repositories
{
        public class SubmissionDeadlinesRepository : BaseRepository, ISubmissionDeadlinesRepository
        {

                public SubmissionDeadlinesRepository(SubmissionContext context) : base(context)
                {
                }

                public async Task<SubmissionDeadlines> GetByIdAsync(int id)
                {
                        return await _context.SubmissionsDeadlines.FindAsync(id);
                }

                public async Task InsertAsync(SubmissionDeadlines submissionDeadline)
                {
                        await _context.SubmissionsDeadlines.AddAsync(submissionDeadline);
                }

                public SubmissionDeadlines GetByWorkshopUid(Guid workshopUid)
                {
                        return _context.SubmissionsDeadlines.SingleOrDefault(submissionDeadline => submissionDeadline.Uid == workshopUid);
                }
        }
}