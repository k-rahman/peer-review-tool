using System;
using System.Collections.Generic;

namespace Task.Service.Contracts.Models
{
        public interface Criterion
        {
                int Id { get; }
                string Description { get; }
                int MaxPoints { get; }
        }
}