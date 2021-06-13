using System;
using System.Collections.Generic;
using Workshop.Service.Contracts.Models;

namespace Workshop.Service.Contracts
{
        public interface ReviewEnding
        {
                int Id { get; }
                Guid Uid { get; }
                public DateTimeOffset ReviewEnd { get; }
                string InstructorId { get; }
                List<Participant> Participants { get; }
        }
}