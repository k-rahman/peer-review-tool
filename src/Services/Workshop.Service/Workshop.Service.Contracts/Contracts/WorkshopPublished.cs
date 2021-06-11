using System;
using System.Collections.Generic;
using Workshop.Service.Contracts.Models;

namespace Workshop.Service.Contracts
{
        public interface WorkshopPublished
        {
                int Id { get; }
                Guid Uid { get; }
                public DateTimeOffset Published { get; set; }
                string InstructorId { get; }
                List<Participant> Participants { get; }
        }
}