using System.Threading.Tasks;
using Submission.Service.API.Domain.Repositories;
using Submission.Service.API.Persistence.Contexts;

namespace Submission.Service.API.Persistence.Repositories
{
        public class UnitOfWork : IUnitOfWork
        {
                private readonly SubmissionContext _context;

                public UnitOfWork(SubmissionContext context)
                {
                        _context = context;
                }

                public async System.Threading.Tasks.Task CompleteAsync()
                {
                        await _context.SaveChangesAsync();
                }
        }
}