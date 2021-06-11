using Submission.Service.API.Persistence.Contexts;

namespace Submission.Service.API.Persistence.Repositories
{
        public abstract class BaseRepository
        {
                protected readonly SubmissionContext _context;

                public BaseRepository(SubmissionContext context)
                {
                        _context = context;
                }
        }
}