using Workshop.Service.API.Domain.Repositories;
using Workshop.Service.API.Persistence.Contexts;
using System.Threading.Tasks;

namespace Workshop.Service.API.Persistence.Repositories
{
        public class UnitOfWork : IUnitOfWork
        {
                private readonly WorkshopContext _context;

                public UnitOfWork(WorkshopContext context)
                {
                        _context = context;
                }

                public async Task CompleteAsync()
                {
                        await _context.SaveChangesAsync();
                }
        }
}