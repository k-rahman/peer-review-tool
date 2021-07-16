using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Submission.Service.API.Domain.Repositories;
using Submission.Service.API.Persistence.Contexts;

namespace Submission.Service.API.Persistence.Repositories
{
	public class SubmissionRepository : BaseRepository, ISubmissionRepository
	{

		public SubmissionRepository(SubmissionContext context) : base(context)
		{
		}
		public async Task<IEnumerable<Domain.Models.Submission>> GetSubmissionsByWorkshopUid(Guid workshopUid)
		{
			return await _context.Submissions
														.Where(submission => submission.WorkshopUid == workshopUid)
														.ToListAsync();
		}

		public async Task<Domain.Models.Submission> GetAuthorSubmissionByWorkshopUidAsync(Guid workshopUid, string authorId)
		{
			return await _context.Submissions
														.Where(submission => submission.AuthorId == authorId && submission.WorkshopUid == workshopUid)
														.SingleOrDefaultAsync();
			// 	.Include(submission => submission.SubmissionDeadlines)
			// .Where(submission => submission.AuthorId == authorId && submission.SubmissionDeadlines.Uid == workshopUid)
			// .SingleOrDefaultAsync();
		}

		public async Task<Domain.Models.Submission> GetByIdAsync(int id)
		{
			return await _context.Submissions.SingleOrDefaultAsync(submission => submission.Id == id);
			// return await _context.Submissions.Include(submission => submission.SubmissionDeadlines)
			//                                 .SingleOrDefaultAsync(submission => submission.Id == id);
		}

		public async Task InsertAsync(Domain.Models.Submission submission)
		{
			await _context.Submissions.AddAsync(submission);
		}
	}
}