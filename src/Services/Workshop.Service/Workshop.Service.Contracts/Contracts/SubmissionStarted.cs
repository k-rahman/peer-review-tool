using System;
using System.Collections.Generic;
using Workshop.Service.Contracts.Models;

namespace Workshop.Service.Contracts
{
        public interface SubmissionStarted
        {
                int Id { get; }
                Guid Uid { get; }
                public DateTimeOffset SubmissionStart { get; }
                string InstructorId { get; }
                List<Participant> Participants { get; }
        }
}