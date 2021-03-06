using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workshop.Service.API.Domain.Models;
using Workshop.Service.API.Domain.Repositories;
using Workshop.Service.API.Persistence.Contexts;

namespace Workshop.Service.API.Persistence.Repositories
{
        public class WorkshopRepository : BaseRepository, IWorkshopRepository
        {

                public WorkshopRepository(WorkshopContext context) : base(context)
                {
                }

                public async Task<IEnumerable<Domain.Models.Workshop>> GetAsync()
                {
                        return await _context.Workshops
                                        .Include(workshop => workshop.Criteria)
                                        .Include(workshop => workshop.Participants)
                                        .ToListAsync();
                }

                public async Task<IEnumerable<Domain.Models.Workshop>> GetByInstructorIdAsync(string id)
                {
                        return await _context.Workshops
                                        .Include(workshop => workshop.Criteria)
                                        .Include(workshop => workshop.Participants)
                                        .Where(workshop => workshop.InstructorId == id)
                                        .OrderByDescending(workshop => workshop.Id)
                                        .ToListAsync();
                }

                public IEnumerable<Domain.Models.Workshop> GetByParticipantId(string id)
                {
                        return _context.Workshops
                                        .Include(workshop => workshop.Criteria)
                                        .Include(workshop => workshop.Participants)
                                        .Where(workshop => workshop.Participants.FirstOrDefault(p => p.Auth0Id == id).Auth0Id == id)
                                        .OrderBy(workshop => workshop.SubmissionStart)
                                        .AsEnumerable()
                                        .Where(workshop => workshop.Published < DateTimeOffset.Now) // DateTimeOffset.Now can't be evalutated to a query, use AsEnumerable() to load it from server then evaluate on client side
                                        .ToList();
                }

                public async Task<Domain.Models.Workshop> GetByIdAsync(int id)
                {
                        return await _context.Workshops
                                        .Include(workshop => workshop.Criteria)
                                        .Include(workshop => workshop.Participants)
                                        .SingleOrDefaultAsync(workshop => workshop.Id == id);
                }

                public async Task<Domain.Models.Workshop> GetByUidAsync(Guid uid)
                {
                        return await _context.Workshops
                                        .Include(workshop => workshop.Criteria)
                                        .Include(workshop => workshop.Participants)
                                        .SingleOrDefaultAsync(workshop => workshop.Uid == uid);
                }

                public async Task InsertAsync(Domain.Models.Workshop workshop)
                {
                        await _context.Workshops.AddAsync(workshop);
                }

                public void Delete(Domain.Models.Workshop workshop)
                {
                        _context.Workshops.Remove(workshop);
                }
        }
}