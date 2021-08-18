using System;
using System.Linq;
using System.Threading.Tasks;
using Review.Service.API.Domain.Models;
using Review.Service.API.Domain.Repositories;
using Review.Service.API.Persistence.Contexts;

namespace Review.Service.API.Persistence.Repositories
{
	public class ReviewDeadlinesRepository : BaseRepository, IReviewDeadlinesRepository
	{

		public ReviewDeadlinesRepository(ReviewContext context) : base(context)
		{
		}

		public async Task<ReviewDeadlines> GetByIdAsync(int id)
		{
			return await _context.ReviewsDeadlines.FindAsync(id);
		}

		public async Task InsertAsync(ReviewDeadlines reviewDeadlines)
		{
			await _context.ReviewsDeadlines.AddAsync(reviewDeadlines);
		}

		public ReviewDeadlines GetByWorkshopUid(Guid workshopUid)
		{
			return _context.ReviewsDeadlines.SingleOrDefault(reviewDeadlines => reviewDeadlines.Uid == workshopUid);
		}
	}
}