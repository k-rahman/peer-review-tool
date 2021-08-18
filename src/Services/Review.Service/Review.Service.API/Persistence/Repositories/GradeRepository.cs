using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

                public async Task<IEnumerable<Grade>> GetGradesByReviewId(int reviewId)
                {
                        return await _context.Grades
                                        .Include(g => g.Criterion)
                                        .Where(g => g.ReviewId == reviewId)
                                        .ToListAsync();
                }

                public async Task InsertAsync(Grade grade)
                {
                        await _context.Grades.AddAsync(grade);
                }
        }
}