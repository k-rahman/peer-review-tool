using System;

namespace Workshop.Service.Contracts
{
        public interface WorkshopUpdated
        {
                int Id { get; }
                Guid Uid { get; }
                DateTimeOffset SubmissionStart { get; }
                DateTimeOffset SubmissionEnd { get; }
                DateTimeOffset ReviewStart { get; }
                DateTimeOffset ReviewEnd { get; }
        }
}