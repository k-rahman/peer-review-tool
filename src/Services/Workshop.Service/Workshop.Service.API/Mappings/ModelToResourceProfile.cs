using System.Linq;
using AutoMapper;
using Workshop.Service.API.Domain.Models;
using Workshop.Service.API.Resources;

namespace Workshop.Service.API.Mappings
{
	public class ModelToResourceProfile : Profile
	{
		public ModelToResourceProfile()
		{
			CreateMap<Domain.Models.Workshop, WorkshopResource>();

			CreateMap<Domain.Models.Workshop, InstructorWorkshopResource>();
			CreateMap<Domain.Models.Participant, ParticipantResource>();
			// .ForMember(r => r.Participants, opt => opt.MapFrom(t => t.Participants.Select(participant => new { id = participant.Auth0Id, name = participant.Name })));


			CreateMap<Criterion, CriterionResource>();
		}
	}
}