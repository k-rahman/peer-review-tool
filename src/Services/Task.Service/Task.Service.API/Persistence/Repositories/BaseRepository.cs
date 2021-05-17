using Task.Service.API.Persistence.Context;

namespace Task.Service.API.Persistence.Repositories
{
        public abstract class BaseRepository
        {
                protected readonly TaskContext _context;

                public BaseRepository(TaskContext context)
                {
                        _context = context;
                }
        }
}