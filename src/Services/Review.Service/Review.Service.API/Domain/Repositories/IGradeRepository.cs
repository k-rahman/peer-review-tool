using System.Collections.Generic;
using System.Threading.Tasks;
using Review.Service.API.Domain.Models;

namespace Review.Service.API.Domain.Repositories
{
	public interface IGradeRepository
	{
		Task<IEnumerable<Grade>> GetGradesByReviewId(int reviewId);
		Task InsertAsync(Grade grade);
	}
}