using AutoMapper;
using Emailing.Service.API.Models;

namespace Emailing.Service.API.Mappings
{
	public class EventToModelProfile : Profile
	{
		public EventToModelProfile()
		{
			CreateMap<Workshop.Service.Contracts.Models.Participant, Participant>();
		}
	}
}