using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Review.Service.API.Domain.Repositories
{
	public interface IReviewRepository
	{
		Task<Domain.Models.Review> GetByIdAsync(int id);
		Task<IEnumerable<Domain.Models.Review>> GetParticipantReviewsByWorkshopUidAsync(Guid workshopUid, string participantId);
		Task<IEnumerable<Domain.Models.Review>> GetInstructorReviewsSummaryByWorkshopUidAsync(Guid workshopUid);
		Task<IEnumerable<Domain.Models.Review>> GetParticipantReviewsSummaryByWorkshopUidAsync(Guid workshopUid, string participantId);
		Task InsertAsync(Domain.Models.Review review);
	}
}