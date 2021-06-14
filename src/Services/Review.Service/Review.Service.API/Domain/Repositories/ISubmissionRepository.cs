using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Review.Service.API.Domain.Repositories
{
        public interface ISubmissionRepository
        {
                Task<Domain.Models.Submission> GetByIdAsync(int id);
                IEnumerable<Domain.Models.Submission> GetByWorkshopUid(Guid workshopUid);
                Task InsertAsync(Domain.Models.Submission submission);
        }
}