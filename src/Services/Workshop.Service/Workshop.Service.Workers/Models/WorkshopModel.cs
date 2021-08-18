using System;
using System.Collections.Generic;

namespace Workshop.Service.Workers.Models
{
        public class WorkshopModel
        {
                public int Id { get; set; }
                public Guid Uid { get; set; }
                public int NumberOfReviews { get; set; }
                public DateTimeOffset SubmissionStart { get; set; }
                public DateTimeOffset SubmissionEnd { get; set; }
                public DateTimeOffset ReviewStart { get; set; }
                public DateTimeOffset ReviewEnd { get; set; }
                public DateTimeOffset Published { get; set; }
                public string InstructorId { get; set; }
                public string Instructor { get; set; }
                public ICollection<ParticipantModel> Participants { get; set; }
        }
}