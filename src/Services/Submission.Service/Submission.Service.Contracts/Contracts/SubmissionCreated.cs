using System;

namespace Submission.Service.Contracts
{
        public interface SubmissionCreated
        {
                int Id { get; }
                string content { get; }
                string AuthorId { get; }
                string Author { get; }
                Guid WorkshopUid { get; }
        }
}