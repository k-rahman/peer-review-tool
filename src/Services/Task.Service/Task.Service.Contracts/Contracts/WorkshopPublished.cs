using System;
using System.Collections.Generic;
using Task.Service.Contracts.Models;

namespace Task.Service.Contracts
{
        public interface WorkshopPublished
        {
                int Id { get; }
                Guid Uid { get; }
                int InstructorId { get; }
                List<Participant> Participants { get; }
        }
}