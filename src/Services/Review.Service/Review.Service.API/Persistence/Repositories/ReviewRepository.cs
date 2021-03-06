using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Review.Service.API.Domain.Repositories;
using Review.Service.API.Persistence.Contexts;
using Review.Service.API.Resources;

namespace Review.Service.API.Persistence.Repositories
{
        public class ReviewRespository : BaseRepository, IReviewRepository
        {

                public ReviewRespository(ReviewContext context) : base(context)
                {
                }

                public async Task<IEnumerable<Domain.Models.Review>> GetParticipantReviewsByWorkshopUidAsync(Guid workshopUid, string participantId)
                {
                        return await _context.Reviews
                                .Include(r => r.Submission)
                                .Include(r => r.Criteria)
                                .Where(r => r.Submission.WorkshopUid == workshopUid && r.ReviewerId == participantId)
                                .ToListAsync();
                }

                public async Task<IEnumerable<Domain.Models.Review>> GetInstructorReviewsSummaryByWorkshopUidAsync(Guid workshopUid)
                {
                        return await _context.Reviews
                                .Include(r => r.Submission)
                                .Include(r => r.Criteria)
                                .Where(r => r.Submission.WorkshopUid == workshopUid)
                                .ToListAsync();
                }

                public async Task<IEnumerable<Domain.Models.Review>> GetParticipantReviewsSummaryByWorkshopUidAsync(Guid workshopUid, string participantId)
                {
                        return await _context.Reviews
                                .Include(r => r.Submission)
                                .Include(r => r.Criteria)
                                .Where(r => r.Submission.WorkshopUid == workshopUid && r.Submission.AuthorId == participantId)
                                .ToListAsync();
                }

                public async Task<Domain.Models.Review> GetByIdAsync(int id)
                {
                        return await _context.Reviews
                                .Include(r => r.Submission)
                                .Include(r => r.Criteria)
                                .Where(r => r.Id == id)
                                .SingleOrDefaultAsync();
                }

                public async Task InsertAsync(Domain.Models.Review review)
                {
                        await _context.Reviews.AddAsync(review);
                }
        }
}