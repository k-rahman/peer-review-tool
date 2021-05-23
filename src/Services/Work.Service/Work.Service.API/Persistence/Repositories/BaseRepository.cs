using Work.Service.API.Persistence.Contexts;

namespace Work.Service.API.Persistence.Repositories
{
        public abstract class BaseRepository
        {
                protected readonly WorkContext _context;

                public BaseRepository(WorkContext context)
                {
                        _context = context;
                }
        }
}