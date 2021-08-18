using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Submission.Service.API.Domain.Services.Communication;
using Submission.Service.API.Resources;

namespace Submission.Service.API.Domain.Services
{
	public interface ISubmissionService
	{
		Task<IEnumerable<SubmissionResource>> GetSubmissionsByWorkshopUid(Guid workshopUid);
		Task<SubmissionResource> GetAuthorSubmissionByWorkshopUidAsync(Guid workshopUid, string authorId);
		Task<SubmissionResource> GetByIdAsync(int id);
		Task<SubmissionResponse> InsertAsync(Guid workshopUid, SaveSubmissionResource submission, string authorId);
		Task<SubmissionResponse> UpdateAsync(int id, SaveSubmissionResource submission);
	}
}