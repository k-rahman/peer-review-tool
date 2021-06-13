using System.Linq;
using Workshop.Service.API.Domain.Models;
using Workshop.Service.API.Domain.Repositories;
using Workshop.Service.API.Persistence.Contexts;

namespace Workshop.Service.API.Persistence.Repositories
{
        public class ParticipantRepository : BaseRepository, IParticipantRepository
        {

                public ParticipantRepository(WorkshopContext context) : base(context)
                {
                }

                public Participant GetByEmail(string email)
                {
                        return _context.Participants.Where(participant => participant.Email == email).SingleOrDefault();
                }
        }
}