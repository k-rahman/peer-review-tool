using System;
using System.Threading.Tasks;
using Review.Service.API.Domain.Models;

namespace Review.Service.API.Domain.Repositories
{
	public interface IReviewDeadlinesRepository
	{
		Task<ReviewDeadlines> GetByIdAsync(int id);
		Task InsertAsync(ReviewDeadlines reviewDeadlines);
		ReviewDeadlines GetByWorkshopUid(Guid workshopUid);
	}
}