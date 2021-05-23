using System.Threading.Tasks;
using Work.Service.API.Domain.Repositories;
using Work.Service.API.Persistence.Contexts;

namespace Work.Service.API.Persistence.Repositories
{
        public class UnitOfWork : IUnitOfWork
        {
                private readonly WorkContext _context;

                public UnitOfWork(WorkContext context)
                {
                        _context = context;
                }

                public async Task CompleteAsync()
                {
                        await _context.SaveChangesAsync();
                }
        }
}