using System.Threading.Tasks;
using Review.Service.API.Domain.Models;
using Review.Service.API.Domain.Repositories;
using Review.Service.API.Persistence.Contexts;

namespace Review.Service.API.Persistence.Repositories
{
        public class GradeRepository : BaseRepository, IGradeRepository
        {

                public GradeRepository(ReviewContext context) : base(context)
                {
                }

                public async Task<Grade> GetByIdAsync(int id)
                {
                        return await _context.Grades.FindAsync(id);
                }

                public async Task InsertAsync(Grade grade)
                {
                        await _context.Grades.AddAsync(grade);
                }
        }
}