using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Submission.Service.API.Domain.Repositories
{
	public interface ISubmissionRepository
	{
		Task<IEnumerable<Domain.Models.Submission>> GetSubmissionsByWorkshopUid(Guid workshopUid);
		Task<Domain.Models.Submission> GetAuthorSubmissionByWorkshopUidAsync(Guid workshopUid, string authorId);
		Task<Domain.Models.Submission> GetByIdAsync(int id);
		Task InsertAsync(Domain.Models.Submission submission);
	}
}