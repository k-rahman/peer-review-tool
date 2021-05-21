using System;

namespace Task.Service.API.Events
{
        public record TaskCreated(
                int Id,
                string Name,
                DateTimeOffset Published,
                int InstructorId
                );
}