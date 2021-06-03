using Review.Service.API.Persistence.Contexts;

namespace Review.Service.API.Persistence.Repositories
{
        public abstract class BaseRepository
        {
                protected readonly ReviewContext _context;

                public BaseRepository(ReviewContext context)
                {
                        _context = context;
                }
        }
}