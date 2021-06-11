using System;
using System.Collections.Generic;
using Workshop.Service.Contracts.Models;

namespace Workshop.Service.Contracts
{
        public interface WorkshopCreated
        {
                int Id { get; }
                Guid Uid { get; }
                DateTimeOffset SubmissionStart { get; }
                DateTimeOffset SubmissionEnd { get; }
                DateTimeOffset ReviewStart { get; }
                DateTimeOffset ReviewEnd { get; }
                string InstructorId { get; }
                List<Criterion> Criteria { get; }
        }
}