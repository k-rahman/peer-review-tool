using System.Linq;
using AutoMapper;
using Task.Service.API.Domain.Models;
using Task.Service.API.Resources;

namespace Task.Service.API.Mapping
{
        public class ModelToResourceProfile : Profile
        {
                public ModelToResourceProfile()
                {
                        CreateMap<Domain.Models.Task, TaskResource>()
                        .ForMember(r => r.Participants, opt => opt.MapFrom(t => t.Participants.Select(participant => participant.Id)));

                        CreateMap<Criterion, CriterionResource>();
                }
        }
}