using System;

namespace Task.Service.Contracts
{
        public interface TaskDeleted
        {
                int Id { get; }
                Guid Uid { get; }
        }
}