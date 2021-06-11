using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Submission.Service.API.Domain.Services.Communication;
using Submission.Service.API.Resources;

namespace Submission.Service.API.Domain.Services
{
        public interface ISubmissionDeadlinesService
        {
                SubmissionDeadlinesResource GetSubmissionDeadlines(Guid workshopUid);
        }
}