using System;

namespace Task.Service.API.Events
{
        public record TaskCreated(
                int Id,
                Guid Uid,
                DateTimeOffset SubmissionStart,
                DateTimeOffset SubmissionEnd,
                DateTimeOffset ReviewStart,
                DateTimeOffset ReviewEnd,
                int InstructorId
                );
}