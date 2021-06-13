using AutoMapper;
using Workshop.Service.API.Domain.Models;
using Workshop.Service.API.Resources;

namespace Workshop.Service.API.Mappings
{
        public class ResourceToModelProfile : Profile
        {
                public ResourceToModelProfile()
                {
                        CreateMap<SaveWorkshopResource, Domain.Models.Workshop>();
                        CreateMap<SaveCriterionResource, Criterion>();
                }
        }
}