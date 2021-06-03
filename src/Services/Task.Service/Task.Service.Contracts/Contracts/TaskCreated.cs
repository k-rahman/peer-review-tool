using System;
using System.Collections.Generic;
using Task.Service.Contracts.Models;

namespace Task.Service.Contracts
{
        public interface TaskCreated
        {
                int Id { get; }
                Guid Uid { get; }
                DateTimeOffset SubmissionStart { get; }
                DateTimeOffset SubmissionEnd { get; }
                DateTimeOffset ReviewStart { get; }
                DateTimeOffset ReviewEnd { get; }
                int InstructorId { get; }
                List<Criterion> Criteria { get; }
        }
}