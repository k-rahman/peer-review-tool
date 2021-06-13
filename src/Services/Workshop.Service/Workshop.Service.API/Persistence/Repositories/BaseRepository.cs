using Workshop.Service.API.Persistence.Contexts;

namespace Workshop.Service.API.Persistence.Repositories
{
        public abstract class BaseRepository
        {
                protected readonly WorkshopContext _context;

                public BaseRepository(WorkshopContext context)
                {
                        _context = context;
                }
        }
}