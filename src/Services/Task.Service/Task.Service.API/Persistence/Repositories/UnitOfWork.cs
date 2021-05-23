using Task.Service.API.Domain.Repositories;
using Task.Service.API.Persistence.Contexts;

namespace Task.Service.API.Persistence.Repositories
{
        public class UnitOfWork : IUnitOfWork
        {
                private readonly TaskContext _context;

                public UnitOfWork(TaskContext context)
                {
                        _context = context;
                }

                public async System.Threading.Tasks.Task CompleteAsync()
                {
                        await _context.SaveChangesAsync();
                }
        }
}