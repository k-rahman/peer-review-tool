using System;
using System.Threading.Tasks;

namespace Submission.Service.API.Domain.Repositories
{
        public interface ISubmissionRepository
        {
                Task<Domain.Models.Submission> GetAuthorSubmissionByWorkshopUidAsync(Guid workshopUid, string authorId);
                Task<Domain.Models.Submission> GetByIdAsync(int id);
        }
}