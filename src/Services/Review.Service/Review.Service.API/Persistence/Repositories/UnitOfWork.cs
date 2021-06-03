using Review.Service.API.Domain.Repositories;
using Review.Service.API.Persistence.Contexts;

namespace Review.Service.API.Persistence.Repositories
{
        public class UnitOfWork : IUnitOfWork
        {
                private readonly ReviewContext _context;

                public UnitOfWork(ReviewContext context)
                {
                        _context = context;
                }

                public async System.Threading.Tasks.Task CompleteAsync()
                {
                        await _context.SaveChangesAsync();
                }
        }
}