using Workshop.Service.API.Domain.Models;

namespace Workshop.Service.API.Domain.Repositories
{
        public interface IParticipantRepository
        {
                Participant GetById(string id);
                Participant GetByEmail(string email);
        }
}