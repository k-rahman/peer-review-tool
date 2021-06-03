using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task.Service.API.Domain.Models;
using Task.Service.API.Domain.Repositories;
using Task.Service.API.Persistence.Contexts;

namespace Task.Service.API.Persistence.Repositories
{
        public class TaskRepository : BaseRepository, ITaskRepository
        {

                public TaskRepository(TaskContext context) : base(context)
                {
                }

                public async Task<IEnumerable<Domain.Models.Task>> GetAsync()
                {
                        return await _context.Tasks
                                        .Include(task => task.Criteria)
                                        .Include(task => task.Participants)
                                        .ToListAsync();
                }

                public async Task<IEnumerable<Domain.Models.Task>> GetByInstructorIdAsync(string id)
                {
                        return await _context.Tasks
                                        .Include(task => task.Criteria)
                                        .Include(task => task.Participants)
                                        .Where(task => task.InstructorId == id)
                                        .ToListAsync();
                }
                public async Task<IEnumerable<Domain.Models.Task>> GetByParticipantIdAsync(string id)
                {
                        return await _context.Tasks
                                        .Include(task => task.Criteria)
                                        .Include(task => task.Participants)
                                        .Where(task => task.Participants.FirstOrDefault(p => p.auth0Id == id).auth0Id == id)
                                        .ToListAsync();
                }

                public async Task<Domain.Models.Task> GetByIdAsync(int id)
                {
                        return await _context.Tasks
                                        .Include(task => task.Criteria)
                                        .Include(task => task.Participants)
                                        .SingleOrDefaultAsync(task => task.Id == id);
                }
                public async Task<Domain.Models.Task> GetByUidAsync(Guid uid)
                {
                        return await _context.Tasks
                                        .Include(task => task.Criteria)
                                        .Include(task => task.Participants)
                                        .SingleOrDefaultAsync(task => task.Uid == uid);
                }

                public async System.Threading.Tasks.Task InsertAsync(Domain.Models.Task task)
                {
                        await _context.Tasks.AddAsync(task);
                }

                public void Delete(Domain.Models.Task task)
                {
                        _context.Tasks.Remove(task);
                }
        }
}