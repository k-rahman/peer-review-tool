using AutoMapper;
using Task.Service.Contracts;
using Work.Service.API.Resources;

namespace Work.Service.API.Mapping
{
        public class EventToModelProfile : Profile
        {
                public EventToModelProfile()
                {
                        CreateMap<TaskCreated, Domain.Models.WorksDeadline>();
                }
        }
}