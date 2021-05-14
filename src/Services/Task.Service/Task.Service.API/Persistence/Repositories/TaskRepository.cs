using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task.Service.API.Domain.Repositories;
using Task.Service.API.Persistence.Context;

namespace Task.Service.API.Persistence.Repositories
{
        public class TaskRepository : BaseRepository, ITaskRepository
        {
                public TaskRepository(TaskContext context) : base(context)
                {
                }

                public async Task<IEnumerable<Domain.Models.Task>> ListAsync()
                {
                        return await _context.Tasks.ToListAsync();
                }
        }
}