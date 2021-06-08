using AutoMapper;
using Emailing.Service.API.Models;

namespace Emailing.Service.API.Mappings
{
        public class EventToModelProfile : Profile
        {
                public EventToModelProfile()
                {
                        CreateMap<Task.Service.Contracts.Models.Participant, Participant>();
                }
        }
}