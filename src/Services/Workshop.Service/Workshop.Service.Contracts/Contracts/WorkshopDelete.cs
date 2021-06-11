using System;

namespace Workshop.Service.Contracts
{
        public interface WorkshopDeleted
        {
                int Id { get; }
                Guid Uid { get; }
        }
}