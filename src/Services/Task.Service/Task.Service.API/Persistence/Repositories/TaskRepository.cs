using System;
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

                public async Task<IEnumerable<Domain.Models.Task>> GetAsync()
                {
                        return await _context.Tasks.Include(task => task.Criteria).ToListAsync();
                }

                public async Task<Domain.Models.Task> GetByIdAsync(int Id)
                {
                        return await _context.Tasks.Include(task => task.Criteria)
                                                .SingleOrDefaultAsync(task => task.Id == Id);
                }

                public async System.Threading.Tasks.Task InsertAsync(Domain.Models.Task task)
                {
                        await _context.Tasks.AddAsync(task);
                }

                public void Update(Domain.Models.Task task)
                {
                        _context.Tasks.Update(task);
                }

                public void Delete(Domain.Models.Task task)
                {
                        _context.Tasks.Remove(task);
                }
        }
}