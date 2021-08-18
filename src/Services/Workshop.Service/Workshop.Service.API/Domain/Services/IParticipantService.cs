using System.Threading.Tasks;
using Workshop.Service.API.Domain.Models;
using Workshop.Service.API.Resources;

namespace Workshop.Service.API.Domain.Services
{
	public interface IParticipantService
	{
		Participant GetByEmail(string email);

		Task UpdateParticipant(SaveParticipantResource participant, string participantId);

	}
}