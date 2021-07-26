using System;
using System.Collections.Generic;
using Workshop.Service.Contracts.Models;

namespace Workshop.Service.Contracts
{
        public interface ReviewStarted
        {
                int Id { get; }
                Guid Uid { get; }
                public DateTimeOffset ReviewStart { get; }
                public int NumberOfReviews { get; }
                string InstructorId { get; }
                string Instructor { get; }
                List<Participant> Participants { get; }
        }
}