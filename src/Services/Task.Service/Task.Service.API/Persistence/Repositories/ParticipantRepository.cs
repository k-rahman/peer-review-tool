using System.Linq;
using Task.Service.API.Domain.Models;
using Task.Service.API.Domain.Repositories;
using Task.Service.API.Persistence.Contexts;

namespace Task.Service.API.Persistence.Repositories
{
        public class ParticipantRepository : BaseRepository, IParticipantRepository
        {

                public ParticipantRepository(TaskContext context) : base(context)
                {
                }

                public Participant GetByEmail(string email)
                {
                        return _context.Participants.Where(participant => participant.Email == email).SingleOrDefault();
                }
        }
}