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

                public async System.Threading.Tasks.Task CompleteAsync()
                {
                        await _context.SaveChangesAsync();
                }
        }
}