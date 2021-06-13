using System;
using System.Threading.Tasks;
using Submission.Service.API.Domain.Models;

namespace Submission.Service.API.Domain.Repositories
{
        public interface ISubmissionDeadlinesRepository
        {
                Task<SubmissionDeadlines> GetByIdAsync(int id);
                Task InsertAsync(SubmissionDeadlines submissionDeadlines);
                SubmissionDeadlines GetByWorkshopUid(Guid workshopUid);
        }
}